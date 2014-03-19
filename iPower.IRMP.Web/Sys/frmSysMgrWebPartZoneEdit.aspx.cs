//================================================================================
// FileName: frmSysMgrWebPartZoneEdit.aspx.cs
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
	///frmSysMgrWebPartZoneEdit�б�ҳ���̨���롣
	///</summary>
    public partial class frmSysMgrWebPartZoneEdit : ModuleBasePage, ISysMgrWebPartZoneEditView
	{
		#region ��Ա���������캯����
		SysMgrWebPartZonePresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSysMgrWebPartZoneEdit()
		{
			this.presenter = new SysMgrWebPartZonePresenter(this);

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
                SysMgrWebPartZone data = new SysMgrWebPartZone();
                data.ZoneID = this.ZoneID.IsValid ? this.ZoneID : GUIDEx.New;
                data.ZoneLength = int.Parse(this.txtZoneLength.Text);
                data.ZoneName = this.txtZoneName.Text;
                data.Description = this.txtDescription.Text;
                data.AppAuthID = this.pbAppSystem.Value;
                data.ZoneMode = int.Parse(ddlZoneMode.SelectedValue);
                if (this.presenter.UpdateSysMgrWebPartZone(data))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrWebPartZone>>(delegate(object sender, EntityEventArgs<SysMgrWebPartZone> e)
            {
                if (e.Entity != null)
                {
                    this.txtZoneName.Text = e.Entity.ZoneName;
                    this.txtDescription.Text = e.Entity.Description;
                    this.txtZoneLength.Text = e.Entity.ZoneLength.ToString();
                    this.pbAppSystem.Value = e.Entity.AppAuthID;
                    this.ddlZoneMode.SelectedValue = e.Entity.ZoneLength.ToString();
                    this.pbAppSystem.Text = e.Entity.SystemName;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region ISysMgrWebPartZoneEditView ��Ա

        public void ShowMessage(string Msg)
        {
            this.errMessage.Message = Msg;
        }

        public GUIDEx ZoneID
        {
            get { return this.RequestGUIEx("ZoneID"); }
        }

        public string AppAuthID
        {
            get { return this.pbAppSystem.Value; }
        }

        public void BindZoneMode(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlZoneMode, data);
        }

        #endregion
    }

}
