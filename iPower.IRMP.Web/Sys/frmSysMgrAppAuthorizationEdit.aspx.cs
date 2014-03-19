//================================================================================
// FileName: frmSysMgrAppAuthorizationEdit.aspx.cs
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
	///frmSysMgrAppAuthorizationEdit�б�ҳ���̨���롣
	///</summary>
	public partial class frmSysMgrAppAuthorizationEdit:ModuleBasePage,ISysMgrAppAuthorizationEditView
	{
		#region ��Ա���������캯����
		SysMgrAppAuthorizationPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSysMgrAppAuthorizationEdit()
		{
			this.presenter = new SysMgrAppAuthorizationPresenter(this);

		}
		#endregion

		#region �¼�����
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
				this.presenter.InitializeComponent();
		}
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            try
            {
                SysMgrAppAuthorization data = new SysMgrAppAuthorization();
                data.AppAuthID = this.AppAuthID.IsValid ? this.AppAuthID : GUIDEx.New;

                data.SystemID = this.pbAppSystem.Value;
                data.SystemName = this.pbAppSystem.Text;

                data.AuthPwd = this.txtAuthPwd.Text.Trim();
                data.AuthStatus = int.Parse(this.ddlAuthStatus.SelectedValue);

                if (this.presenter.UpdateSysMgrAppAuthorization(data))
                    this.SaveData();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrAppAuthorization>>(delegate(object sender, EntityEventArgs<SysMgrAppAuthorization> e)
            {
                if (e.Entity != null)
                {
                    this.pbAppSystem.Text = e.Entity.SystemName;
                    this.pbAppSystem.Value = e.Entity.SystemID;

                    this.txtAuthPwd.Text = e.Entity.AuthPwd;

                    this.ddlAuthStatus.SelectedValue = e.Entity.AuthStatus.ToString();
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISysMgrAppAuthorizationEditView ��Ա

        public GUIDEx AppAuthID
        {
            get { return this.RequestGUIEx("AppAuthID"); }
        }

        public void BindAuthStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlAuthStatus, data);
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
