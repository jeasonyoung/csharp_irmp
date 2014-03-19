//================================================================================
// FileName: frmFlowParameterMapList.aspx.cs
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
// Copyright (C) 2009-2010 iPower Young Corporation
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

using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowParameterMapList�б�ҳ���̨���롣
	///</summary>
    public partial class frmFlowParameterMapList : ModuleBasePage, IFlowParameterMapListView
    {
        #region ��Ա���������캯����
        FlowParameterMapPresenter presenter = null;
        ///<summary>
        ///���캯����
        ///</summary>
        public frmFlowParameterMapList()
        {
            this.presenter = new FlowParameterMapPresenter(this);

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
        protected void dgfrmFlowParameterMapList_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmFlowParameterMapList.DataSource = this.presenter.ListViewDataSource;

        }
        protected void ddlProcess_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ChangeProcess();
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
           this.dgfrmFlowParameterMapList.InvokeBuildDataSource();
        }
        public override bool DeleteData()
        {
            return this.presenter.BatchDeleteData(this.dgfrmFlowParameterMapList.CheckedValue);

        }
        #endregion


        #region IFlowParameterMapListView ��Ա

        public string ParameterName
        {
            get { return this.txtParameterName.Text.Trim(); }
        }

        public GUIDEx TransitionID
        {
            get { return this.ddlTransitionID.SelectedValue; }
        }

        #endregion

        #region IFlowParameterMapView ��Ա

        public void BindTransition(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlTransitionID, data);
        }

        public GUIDEx ProcessID
        {
            get { return this.ddlProcess.SelectedValue; }
        }

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcess, data);
        }

        #endregion
    }

}
