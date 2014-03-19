//================================================================================
// FileName: frmSecurityActionEdit.aspx.cs
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
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
	///<summary>
	///frmSecurityActionEdit�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityActionEdit:ModuleBasePage,ISecurityActionEditView
	{
		#region ��Ա���������캯����
		SecurityActionPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityActionEdit()
		{
			this.presenter = new SecurityActionPresenter(this);

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
            SecurityAction data = new SecurityAction();
            data.ActionID = this.txtActionID.Text.Trim();
            data.ActionSign = this.txtActionSign.Text.Trim();
            data.ActionType = int.Parse(this.ddlActionType.SelectedValue);
            data.ActionName = this.txtActionName.Text.Trim();
            data.ActionDescription = this.txtActionDescription.Text.Trim();

            if (this.presenter.UpdateAction(data))
                this.SaveData();
		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SecurityAction>>(delegate(object sender, EntityEventArgs<SecurityAction> e)
            {
                if (e.Entity != null)
                {
                    this.txtActionID.Text = e.Entity.ActionID;
                    this.txtActionID.Enabled = false;

                    this.txtActionSign.Text = e.Entity.ActionSign;
                    this.ddlActionType.SelectedValue = e.Entity.ActionType.ToString();
                    this.txtActionName.Text = e.Entity.ActionName;
                    this.txtActionDescription.Text = e.Entity.ActionDescription;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISecurityActionEditView ��Ա

        public GUIDEx ActionID
        {
            get { return this.RequestGUIEx("ActionID"); }
        }
 
        public void BindActionType(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlActionType, data);
        }

        #endregion
    }

}
