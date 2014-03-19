//================================================================================
// FileName: frmSSOTicketEdit.aspx.cs
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmSSOTicketEdit列表页面后台代码。
	///</summary>
    public partial class frmSSOTicketEdit : ModuleBasePage, ISSOTicketEditView
    {
        #region 成员变量，构造函数。
        SSOTicketPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmSSOTicketEdit()
        {
            this.presenter = new SSOTicketPresenter(this);

        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Token.IsValid)
                {
                    SSOTicket data = new SSOTicket();
                    data.Token = this.Token;
                    data.UserData = this.txtUserData.Text;
                    data.IssueClientIP = this.txtIssueClientIP.Text;
                    data.IssueDate = DateTime.Parse(this.txtIssueDate.Text);
                    data.RenewalCount = int.Parse(this.txtRenewalCount.Text);
                    data.LastRenewalIP = this.txtLastRenewalIP.Text;
                    data.Expiration = DateTime.Parse(this.txtExpiration.Text);

                    if (this.presenter.UpdateSSOTicket(data))
                        this.SaveData();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SSOTicket>>(delegate(object sender, EntityEventArgs<SSOTicket> e)
            {
                if (e.Entity != null)
                {
                    this.txtToken.Text = e.Entity.Token;
                    this.txtUserData.Text = e.Entity.UserData;

                    this.txtIssueClientIP.Text = e.Entity.IssueClientIP;
                    this.txtIssueDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.IssueDate);

                    this.txtRenewalCount.Text = e.Entity.RenewalCount.ToString();

                    this.txtLastRenewalIP.Text = e.Entity.LastRenewalIP;
                    this.txtExpiration.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.Expiration);

                    this.txtHasValid.Text = e.Entity.HasValid ? "有效" : "无效";

                    this.txtToken.Enabled = this.txtUserData.Enabled = this.txtIssueClientIP.Enabled = this.txtIssueDate.Enabled = this.txtHasValid.Enabled = false;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion
        
        #region ISSOTicketEditView 成员

        public GUIDEx Token
        {
            get { return this.RequestGUIEx("Token"); }
        }

        #endregion

        #region ISSOTicketView 成员

        public void ShowMessage(string msg)
        {
            this.errMessage.Message = msg;
        }

        #endregion
    }

}
