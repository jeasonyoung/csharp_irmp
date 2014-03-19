//================================================================================
// FileName: FlowStepAuthorizePresenter.cs
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
	/// IFlowStepAuthorizeView接口。
	///</summary>
	public interface IFlowStepAuthorizeView: IModuleView
	{
        /// <summary>
        /// 绑定所属流程。
        /// </summary>
        /// <param name="data"></param>
        void BindProcess(IListControlsData data);
	}
    /// <summary>
    /// 
    /// </summary>
    public interface IFlowStepAuthorizeEditView : IFlowStepAuthorizeView
    {
        /// <summary>
        /// 获取授权ID。
        /// </summary>
        GUIDEx AuthorizeID { get; }
        /// <summary>
        /// 获取或设置所属流程。
        /// </summary>
        GUIDEx ProcessID { get; set; }
        /// <summary>
        /// 绑定授权步骤。
        /// </summary>
        /// <param name="data"></param>
        void BindStep(IListControlsData data);
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    public interface IFlowStepAuthorizeListView : IFlowStepAuthorizeView
    {
        /// <summary>
        /// 获取所属流程。
        /// </summary>
        GUIDEx ProcessID { get; }
        /// <summary>
        /// 获取步骤名称。
        /// </summary>
        string StepName { get; }
        /// <summary>
        /// 获取授权日期。
        /// </summary>
        string AuthorizeDate { get; }
        /// <summary>
        /// 获取用户ID。
        /// </summary>
        GUIDEx EmployeeID { get; }
    }
	///<summary>
	/// FlowStepAuthorizePresenter行为类。
	///</summary>
	public class FlowStepAuthorizePresenter: ModulePresenter<IFlowStepAuthorizeView>
	{
		#region 成员变量，构造函数。
        FlowStepAuthorizeEntity flowStepAuthorizeEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public FlowStepAuthorizePresenter(IFlowStepAuthorizeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.StepAuthorize_ModuleID;
            this.flowStepAuthorizeEntity = new FlowStepAuthorizeEntity();
		}
		#endregion

        /// <summary>
        /// 列表数据。
        /// </summary>
        public DataTable ListViewDataSource
        {
            get
            {
                IFlowStepAuthorizeListView listView = this.View as IFlowStepAuthorizeListView;
                if (listView != null)
                {
                    return this.flowStepAuthorizeEntity.ListDataSource(listView.ProcessID.IsValid ? listView.ProcessID.Value : string.Empty,
                                                                       listView.StepName,
                                                                       listView.EmployeeID.IsValid ? listView.EmployeeID.Value : string.Empty,
                                                                       listView.AuthorizeDate);
                }
                return null;
            }
        }

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            if (this.View != null)
            {
                this.View.BindProcess(new FlowProcessEntity().BindProcess());

                IFlowStepAuthorizeEditView editView = this.View as IFlowStepAuthorizeEditView;
                if (editView != null)
                {
                    this.ChangeProcess();
                }
            }
        }
        #endregion

        #region 数据操作函数。
        /// <summary>
        /// 改变所属流程。
        /// </summary>
        public void ChangeProcess()
        {
            IFlowStepAuthorizeEditView editView = this.View as IFlowStepAuthorizeEditView;
            if (editView != null)
            {
                editView.BindStep(new FlowStepEntity().BindStep(editView.ProcessID));
            }
        }
        /// <summary>
        /// 获取流程ID。
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public GUIDEx GetProcessID(GUIDEx stepID)
        {
            return new FlowStepEntity().FindProcessID(stepID);
        }
        ///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<FlowStepAuthorize>> handler)
		{
            IFlowStepAuthorizeEditView editView = this.View as IFlowStepAuthorizeEditView;
            if (editView != null && editView.AuthorizeID.IsValid && handler != null)
            {
                FlowStepAuthorize data = new FlowStepAuthorize();
                data.AuthorizeID = editView.AuthorizeID;
                if (this.flowStepAuthorizeEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<FlowStepAuthorize>(data));
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateData(FlowStepAuthorize data)
        {
            return this.flowStepAuthorizeEntity.UpdateRecord(data);
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="pri"></param>
        /// <returns></returns>
        public bool BatchDeteleDate(StringCollection pri)
        {
            return this.flowStepAuthorizeEntity.DeleteRecord(pri);
        }
		#endregion

	}

}
