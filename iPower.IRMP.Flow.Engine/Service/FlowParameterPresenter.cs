//================================================================================
// FileName: FlowParameterPresenter.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Service
{
	///<summary>
	/// IFlowParameterView接口。
	///</summary>
	public interface IFlowParameterView: IModuleView
	{
        /// <summary>
        /// 获取所属流程ID。
        /// </summary>
        string ProcessID { get; }
        /// <summary>
        /// 绑定所属流程数据。
        /// </summary>
        /// <param name="data"></param>
        void BindProcess(IListControlsData data);
        /// <summary>
        /// 绑定所属步骤数据。
        /// </summary>
        /// <param name="data"></param>
        void BindStep(IListControlsData data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
	}
    /// <summary>
    /// 
    /// </summary>
    public interface IFlowParameterListView : IFlowParameterView
    {
        /// <summary>
        /// 获取参数名称。
        /// </summary>
        string ParameterName { get; }
       
        /// <summary>
        /// 获取所属步骤ID。
        /// </summary>
        string StepID { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IFlowParameterEditView : IFlowParameterView
    {
        /// <summary>
        /// 获取参数ID。
        /// </summary>
        GUIDEx ParameterID { get; }
        /// <summary>
        /// 绑定参数类型。
        /// </summary>
        /// <param name="data"></param>
        void BindEnumParameterType(IListControlsData data);
        /// <summary>
        /// 设置所属流程。
        /// </summary>
        /// <param name="processID"></param>
        void SetProcess(GUIDEx processID);
    }

	///<summary>
	/// FlowParameterPresenter行为类。
	///</summary>
	public class FlowParameterPresenter: ModulePresenter<IFlowParameterView>
	{
		#region 成员变量，构造函数。
        FlowParameterEntity parameterEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public FlowParameterPresenter(IFlowParameterView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Parameter_ModuleID;
            this.parameterEntity = new FlowParameterEntity();
		}
		#endregion

        public DataTable ListDataSource
        {
            get
            {
                IFlowParameterListView listView = this.View as IFlowParameterListView;
                if (listView != null)
                {
                    DataTable dtSouce = this.parameterEntity.ListDataSource(listView.ParameterName, listView.ProcessID, listView.StepID);
                    dtSouce.Columns.Add("ParameterTypeName");

                    foreach (DataRow row in dtSouce.Rows)
                    {
                        row["ParameterTypeName"] = this.GetEnumMemberName(typeof(EnumParameterType), Convert.ToInt32(row["ParameterType"]));
                    }

                    return dtSouce;
                }
                return null;
            }
        }

        #region 重载。
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            IFlowParameterView pView = this.View as IFlowParameterView;
            if (pView != null)
            {
                pView.BindProcess(new FlowProcessEntity().BindProcess());
                this.BindStepData();
            }

            IFlowParameterEditView editView = this.View as IFlowParameterEditView;
            if (editView != null)
            {
                editView.BindEnumParameterType(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumParameterType), this.ModuleConfig));
            }
        }
        #endregion

        #region 数据操作函数。
        public void BindStepData()
        {
            IFlowParameterView pView = this.View as IFlowParameterView;
            if (pView != null)
                pView.BindStep(new FlowStepEntity().BindStep(pView.ProcessID));
        }
        ///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<FlowParameter>> handler)
		{
            IFlowParameterEditView editView = this.View as IFlowParameterEditView;
            if (editView != null && editView.ParameterID.IsValid)
            {
                FlowParameter data = new FlowParameter();
                data.ParameterID = editView.ParameterID;
                if (this.parameterEntity.LoadRecord(ref data))
                {
                    editView.SetProcess(new FlowStepEntity().FindProcessID(data.StepID));
                    handler(this, new EntityEventArgs<FlowParameter>(data));
                }
            }
		}
         /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateData(FlowParameter data)
        {
            bool result = false;
            if (data != null)
                result = this.parameterEntity.UpdateRecord(data);
            return result;
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="pri"></param>
        /// <returns></returns>
        public bool BatchDeleteData(StringCollection pri)
        {
            bool result = false;
            try
            {
                if (pri != null)
                {
                    result = this.parameterEntity.DeleteRecord(pri);
                }
            }
            catch (Exception e)
            {
                IFlowParameterListView listView = this.View as IFlowParameterListView;
                if (listView != null)
                {
                    listView.ShowMessage(e.Message);
                }
            }
            return result;
        }
		#endregion

	}

}
