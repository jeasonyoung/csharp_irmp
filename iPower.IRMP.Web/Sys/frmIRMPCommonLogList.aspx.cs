//================================================================================
// FileName: frmIRMPCommonLogList.aspx.cs
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
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmIRMPCommonLogList�б�ҳ���̨���롣
	///</summary>
	public partial class frmIRMPCommonLogList:ModuleBasePage,IIRMPCommonLogListView
	{
		#region ��Ա���������캯����
		IRMPCommonLogPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmIRMPCommonLogList()
		{
			this.presenter = new IRMPCommonLogPresenter(this);

		}
		#endregion

		#region �¼�����
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.presenter.InitializeComponent();
				this.lbTitle.Text = base.NavigationContent;
                this.txtCreateDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
			}

		}
		protected void dgfrmIRMPCommonLogList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmIRMPCommonLogList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmIRMPCommonLogList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteCommonLog(this.dgfrmIRMPCommonLogList.CheckedValue);

		}
		#endregion
        
        #region IIRMPCommonLogListView ��Ա

        public string SystemName
        {
            get { return this.txtSystemName.Text.Trim(); }
        }

        public string EmployeeName
        {
            get { return this.txtEmployeeName.Text.Trim(); }
        }

        public string CreateDate
        {
            get
            {
                string strDate = this.txtCreateDate.Text.Trim();
                if (!string.IsNullOrEmpty(strDate))
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(strDate);
                        strDate = string.Format("{0:yyyy-MM-dd}", dt);
                    }
                    catch (Exception e)
                    {
                        this.ShowMessage(e.Message);
                    }
                }
                return strDate;
            }
        }

        public string LogContext
        {
            get { return this.txtLogContext.Text.Trim(); }
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
