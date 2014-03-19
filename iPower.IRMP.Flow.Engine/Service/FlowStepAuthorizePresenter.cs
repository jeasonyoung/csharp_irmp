//================================================================================
// FileName: FlowStepAuthorizePresenter.cs
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
	/// IFlowStepAuthorizeView�ӿڡ�
	///</summary>
	public interface IFlowStepAuthorizeView: IModuleView
	{
        /// <summary>
        /// ���������̡�
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
        /// ��ȡ��ȨID��
        /// </summary>
        GUIDEx AuthorizeID { get; }
        /// <summary>
        /// ��ȡ�������������̡�
        /// </summary>
        GUIDEx ProcessID { get; set; }
        /// <summary>
        /// ����Ȩ���衣
        /// </summary>
        /// <param name="data"></param>
        void BindStep(IListControlsData data);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    public interface IFlowStepAuthorizeListView : IFlowStepAuthorizeView
    {
        /// <summary>
        /// ��ȡ�������̡�
        /// </summary>
        GUIDEx ProcessID { get; }
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string StepName { get; }
        /// <summary>
        /// ��ȡ��Ȩ���ڡ�
        /// </summary>
        string AuthorizeDate { get; }
        /// <summary>
        /// ��ȡ�û�ID��
        /// </summary>
        GUIDEx EmployeeID { get; }
    }
	///<summary>
	/// FlowStepAuthorizePresenter��Ϊ�ࡣ
	///</summary>
	public class FlowStepAuthorizePresenter: ModulePresenter<IFlowStepAuthorizeView>
	{
		#region ��Ա���������캯����
        FlowStepAuthorizeEntity flowStepAuthorizeEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public FlowStepAuthorizePresenter(IFlowStepAuthorizeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.StepAuthorize_ModuleID;
            this.flowStepAuthorizeEntity = new FlowStepAuthorizeEntity();
		}
		#endregion

        /// <summary>
        /// �б����ݡ�
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

        #region ���ء�
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

        #region ���ݲ���������
        /// <summary>
        /// �ı��������̡�
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
        /// ��ȡ����ID��
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public GUIDEx GetProcessID(GUIDEx stepID)
        {
            return new FlowStepEntity().FindProcessID(stepID);
        }
        ///<summary>
		///�༭ҳ��������ݡ�
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
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateData(FlowStepAuthorize data)
        {
            return this.flowStepAuthorizeEntity.UpdateRecord(data);
        }
        /// <summary>
        /// ����ɾ�����ݡ�
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
