//================================================================================
// FileName: frmOrgDepartmentList.aspx.cs
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
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Org.Engine.Service;
namespace iPower.IRMP.Org.Web
{
	///<summary>
	///frmOrgDepartmentList�б�ҳ���̨���롣
	///</summary>
	public partial class frmOrgDepartmentList:ModuleBasePage,IOrgDepartmentListView
	{
		#region ��Ա���������캯����
		OrgDepartmentPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmOrgDepartmentList()
		{
			this.presenter = new OrgDepartmentPresenter(this);

		}
		#endregion

		#region �¼�����
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.presenter.InitializeComponent();
				this.lbTitle.Text = base.NavigationContent;
			}
		}
		protected void dgfrmOrgDepartmentList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmOrgDepartmentList.DataSource = this.presenter.ListDataSource;

		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.LoadData();

		}
		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteData())
			this.LoadData();

		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.LoadData();

		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
			this.dgfrmOrgDepartmentList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteData(this.dgfrmOrgDepartmentList.CheckedValue);

		}
		#endregion


        #region IOrgDepartmentListView ��Ա
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text.Trim(); }
        }
        /// <summary>
        /// ��ȡ�ϼ�����ID��
        /// </summary>
        public GUIDEx ParentDepartmentID
        {
            get { return this.ddlDepartmentID.SelectedValue; }
        }
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="content"></param>
        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void BindParentDepartment(IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlDepartmentID, data);
        }

        #endregion
    }

}
