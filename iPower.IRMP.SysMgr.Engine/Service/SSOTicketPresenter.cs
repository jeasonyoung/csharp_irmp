//================================================================================
// FileName: SSOTicketPresenter.cs
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
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISSOTicketView接口。
	///</summary>
	public interface ISSOTicketView: IModuleView
	{
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
	}
    /// <summary>
    /// 列表接口。
    /// </summary>
    public interface ISSOTicketListView : ISSOTicketView
    {
        /// <summary>
        /// 获取用户信息。
        /// </summary>
        string UserData { get; }
    }
    /// <summary>
    /// 编辑接口。
    /// </summary>
    public interface ISSOTicketEditView : ISSOTicketView
    {
        /// <summary>
        /// 获取令牌。
        /// </summary>
        GUIDEx Token { get; }
    }
	///<summary>
	/// SSOTicketPresenter行为类。
	///</summary>
	public class SSOTicketPresenter: ModulePresenter<ISSOTicketView>
	{
		#region 成员变量，构造函数。
        SSOTicketEntity ssoTicketEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SSOTicketPresenter(ISSOTicketView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.SSOTicket_ModuleID;
            this.ssoTicketEntity = new SSOTicketEntity();
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取列表数据。
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
                            row["HasValidName"] = Convert.ToInt32(row["HasValid"]) > 0 ? "有效" : "无效";
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
		///<summary>
		///编辑页面加载数据。
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
        /// 更新数据。
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
        /// 批量删除数据。
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
