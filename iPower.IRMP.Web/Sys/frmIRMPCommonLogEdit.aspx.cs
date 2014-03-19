//================================================================================
// FileName: frmIRMPCommonLogEdit.aspx.cs
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
	///frmIRMPCommonLogEdit�б�ҳ���̨���롣
	///</summary>
	public partial class frmIRMPCommonLogEdit:ModuleBasePage,IIRMPCommonLogEditView
	{
		#region ��Ա���������캯����
		IRMPCommonLogPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmIRMPCommonLogEdit()
		{
			this.presenter = new IRMPCommonLogPresenter(this);

		}
		#endregion

		#region �¼�����
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
				this.presenter.InitializeComponent();
		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<IRMPCommonLog>>(delegate(object sender, EntityEventArgs<IRMPCommonLog> e)
            {
                if (e.Entity != null)
                {
                    this.txtSystemName.Text = e.Entity.SystemName;
                    this.txtRelationTable.Text = e.Entity.RelationTable;
                    this.txtCreateEmployeeName.Text = e.Entity.CreateEmployeeName;
                    this.txtCreateDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.CreateDate);
                    this.txtLogContext.Text = e.Entity.LogContext;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region IIRMPCommonLogEditView ��Ա

        public GUIDEx LogID
        {
            get { return this.RequestGUIEx("LogID"); }
        }

        #endregion
    }

}
