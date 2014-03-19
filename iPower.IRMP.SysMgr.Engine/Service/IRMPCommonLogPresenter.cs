//================================================================================
// FileName: IRMPCommonLogPresenter.cs
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
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// IIRMPCommonLogView�ӿڡ�
	///</summary>
	public interface IIRMPCommonLogView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface IIRMPCommonLogListView : IIRMPCommonLogView
    {
        /// <summary>
        /// ��ȡϵͳ���ơ�
        /// </summary>
        string SystemName { get; }
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// ��ȡ����ʱ�䡣
        /// </summary>
        string CreateDate { get; }
        /// <summary>
        /// ��ȡ��־���ݡ�
        /// </summary>
        string LogContext { get; }
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface IIRMPCommonLogEditView : IIRMPCommonLogView
    {
        /// <summary>
        /// ��ȡ��־ID��
        /// </summary>
        GUIDEx LogID { get; }
    }
		
	///<summary>
	/// IRMPCommonLogPresenter��Ϊ�ࡣ
	///</summary>
	public class IRMPCommonLogPresenter: ModulePresenter<IIRMPCommonLogView>
	{
		#region ��Ա���������캯����
        IRMPCommonLogEntity iRMPCommonLogEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public IRMPCommonLogPresenter(IIRMPCommonLogView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.CommonLog_ModuleID;
            this.iRMPCommonLogEntity = new IRMPCommonLogEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IIRMPCommonLogListView listView = this.View as IIRMPCommonLogListView;
                if (listView != null)
                {
                    return this.iRMPCommonLogEntity.ListDataSource(listView.SystemName, listView.EmployeeName, listView.CreateDate, listView.LogContext);
                }
                return null;
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<IRMPCommonLog>> handler)
		{
            IIRMPCommonLogEditView editView = this.View as IIRMPCommonLogEditView;
            if (editView != null && editView.LogID.IsValid && handler != null)
            {
                IRMPCommonLog data = new IRMPCommonLog();
                data.LogID = editView.LogID;
                if (this.iRMPCommonLogEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<IRMPCommonLog>(data));
            }
		}
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteCommonLog(StringCollection priCollection)
        {
            return this.iRMPCommonLogEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
