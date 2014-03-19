//================================================================================
// FileName: FlowParameterPresenter.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	/// IFlowParameterView�ӿڡ�
	///</summary>
	public interface IFlowParameterView: IModuleView
	{
        /// <summary>
        /// ��ȡ��������ID��
        /// </summary>
        string ProcessID { get; }
        /// <summary>
        /// �������������ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindProcess(IListControlsData data);
        /// <summary>
        /// �������������ݡ�
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
        /// ��ȡ�������ơ�
        /// </summary>
        string ParameterName { get; }
       
        /// <summary>
        /// ��ȡ��������ID��
        /// </summary>
        string StepID { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IFlowParameterEditView : IFlowParameterView
    {
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx ParameterID { get; }
        /// <summary>
        /// �󶨲������͡�
        /// </summary>
        /// <param name="data"></param>
        void BindEnumParameterType(IListControlsData data);
        /// <summary>
        /// �����������̡�
        /// </summary>
        /// <param name="processID"></param>
        void SetProcess(GUIDEx processID);
    }

	///<summary>
	/// FlowParameterPresenter��Ϊ�ࡣ
	///</summary>
	public class FlowParameterPresenter: ModulePresenter<IFlowParameterView>
	{
		#region ��Ա���������캯����
        FlowParameterEntity parameterEntity = null;
		///<summary>
		///���캯����
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

        #region ���ء�
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

        #region ���ݲ���������
        public void BindStepData()
        {
            IFlowParameterView pView = this.View as IFlowParameterView;
            if (pView != null)
                pView.BindStep(new FlowStepEntity().BindStep(pView.ProcessID));
        }
        ///<summary>
		///�༭ҳ��������ݡ�
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
        /// �������ݡ�
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
        /// ����ɾ�����ݡ�
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
