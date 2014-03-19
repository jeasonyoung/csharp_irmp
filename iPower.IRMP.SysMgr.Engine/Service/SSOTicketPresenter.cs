//================================================================================
// FileName: SSOTicketPresenter.cs
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
	/// ISSOTicketView�ӿڡ�
	///</summary>
	public interface ISSOTicketView: IModuleView
	{
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
	}
    /// <summary>
    /// �б�ӿڡ�
    /// </summary>
    public interface ISSOTicketListView : ISSOTicketView
    {
        /// <summary>
        /// ��ȡ�û���Ϣ��
        /// </summary>
        string UserData { get; }
    }
    /// <summary>
    /// �༭�ӿڡ�
    /// </summary>
    public interface ISSOTicketEditView : ISSOTicketView
    {
        /// <summary>
        /// ��ȡ���ơ�
        /// </summary>
        GUIDEx Token { get; }
    }
	///<summary>
	/// SSOTicketPresenter��Ϊ�ࡣ
	///</summary>
	public class SSOTicketPresenter: ModulePresenter<ISSOTicketView>
	{
		#region ��Ա���������캯����
        SSOTicketEntity ssoTicketEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SSOTicketPresenter(ISSOTicketView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.SSOTicket_ModuleID;
            this.ssoTicketEntity = new SSOTicketEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б����ݡ�
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISSOTicketListView listView = this.View as ISSOTicketListView;
                if (listView != null)
                {
                    DataTable dtSource = this.ssoTicketEntity.ListDataSource(listView.UserData);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("HasValidName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["HasValidName"] = Convert.ToInt32(row["HasValid"]) > 0 ? "��Ч" : "��Ч";
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SSOTicket>> handler)
		{
            ISSOTicketEditView editView = this.View as ISSOTicketEditView;
            if (editView != null && editView.Token.IsValid && handler != null)
            {
                SSOTicket data = new SSOTicket();
                data.Token = editView.Token;
                if (this.ssoTicketEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<SSOTicket>(data));
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSSOTicket(SSOTicket data)
        {
            if (data != null)
            {
                try
                {
                    return this.ssoTicketEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    this.View.ShowMessage(e.Message);
                }
            }
            return false;
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteSSOTicket(StringCollection priCollection)
        {
            bool result = false;
            try
            {
                result = this.ssoTicketEntity.DeleteRecord(priCollection);
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
            return result;
        }
		#endregion

	}

}
