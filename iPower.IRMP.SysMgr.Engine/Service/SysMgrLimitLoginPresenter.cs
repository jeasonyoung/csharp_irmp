//================================================================================
// FileName: SysMgrLimitLoginPresenter.cs
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
	/// ISysMgrLimitLoginView�ӿڡ�
	///</summary>
	public interface ISysMgrLimitLoginView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitLoginListView : ISysMgrLimitLoginView
    {
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitLoginEditView : ISysMgrLimitLoginView
    {
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx LimitID { get; }
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
		
	///<summary>
	/// SysMgrLimitLoginPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrLimitLoginPresenter: ModulePresenter<ISysMgrLimitLoginView>
	{
		#region ��Ա���������캯����
        SysMgrLimitLoginEntity sysMgrLimitLoginEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLimitLoginPresenter(ISysMgrLimitLoginView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitLogin_ModuleID;
            this.sysMgrLimitLoginEntity = new SysMgrLimitLoginEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ����Դ��
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrLimitLoginListView listView = this.View as ISysMgrLimitLoginListView;
                if (listView != null)
                {
                    return this.sysMgrLimitLoginEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
                }
                return null;
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitLogin>> handler)
		{
            ISysMgrLimitLoginEditView editView = this.View as ISysMgrLimitLoginEditView;
            if (editView != null&& editView.LimitID.IsValid)
            {
                SysMgrLimitLogin data = new SysMgrLimitLogin();
                data.LimitID = editView.LimitID;
                if (this.sysMgrLimitLoginEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<SysMgrLimitLogin>(data));
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitLogin(SysMgrLimitLogin data)
        {
            bool result = false;
            if (data != null)
            {
                try
                {
                    result = this.sysMgrLimitLoginEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    ISysMgrLimitLoginEditView editView = this.View as ISysMgrLimitLoginEditView;
                    if (editView != null)
                        editView.ShowMessage(e.Message);
                }
            }
            return result;
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteLimitLogin(StringCollection priCollection)
        {
            return this.sysMgrLimitLoginEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
