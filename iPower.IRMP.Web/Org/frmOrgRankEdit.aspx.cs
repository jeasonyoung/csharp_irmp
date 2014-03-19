//================================================================================
// FileName: frmOrgRankEdit.aspx.cs
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
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Service;
namespace iPower.IRMP.Org.Web
{
	///<summary>
	///frmOrgRankEdit�б�ҳ���̨���롣
	///</summary>
	public partial class frmOrgRankEdit:ModuleBasePage,IOrgRankEditView
	{
		#region ��Ա���������캯����
		OrgRankPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmOrgRankEdit()
		{
			this.presenter = new OrgRankPresenter(this);

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
            Domain.OrgRank data = new Domain.OrgRank();
            data.RankID = this.RankID.IsValid ? this.RankID : GUIDEx.New;
            data.ParentRankID = this.ddlParentRankID.SelectedValue;
            data.RankName = this.txtRankName.Text.Trim();
            data.RankDescription = this.txtRankDescription.Text.Trim();

            if (this.presenter.UpdateRankData(data))
                base.SaveData();
		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<Domain.OrgRank>>(delegate(object sender, EntityEventArgs<Domain.OrgRank> e)
            {
                if (e.Entity != null)
                {
                    this.ddlParentRankID.SelectedValue = e.Entity.ParentRankID;

                    this.txtRankName.Text = e.Entity.RankName;
                    this.txtRankDescription.Text = e.Entity.RankDescription;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region IOrgRankEditView ��Ա

        public GUIDEx RankID
        {
            get { return this.RequestGUIEx("RankID"); }
        }

        #endregion

        #region IOrgRankView ��Ա

        public void BindParentRank(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentRankID, data);

        }

        #endregion
    }

}
