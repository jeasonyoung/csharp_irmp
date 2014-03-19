//================================================================================
// FileName: frmCommonEnumsEdit.aspx.cs
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
	///frmCommonEnumsEdit�б�ҳ���̨���롣
	///</summary>
    public partial class frmCommonEnumsEdit : ModuleBasePage, ICommonEnumsEditView
    {
        #region ��Ա���������캯����
        CommonEnumsPresenter presenter = null;
        ///<summary>
        ///���캯����
        ///</summary>
        public frmCommonEnumsEdit()
        {
            this.presenter = new CommonEnumsPresenter(this);

        }
        #endregion
        #region �¼�����
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.presenter.InitializeComponent();
                //this.lbTitle.Text = base.CurrentModuleTitle;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonEnums data = new CommonEnums();
                data.FullEnumName = this.txtFullEnumName.Text.Trim();
                data.Member = this.txtMember.Text.Trim();
                data.MemberName = this.txtMemberName.Text.Trim();
                data.IntValue = int.Parse(this.txtIntValue.Text);
                data.OrderNo = int.Parse(this.txtOrderNo.Text);

                if (this.presenter.UpdateCommonEnums(data))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<CommonEnums>>(delegate(object sender, EntityEventArgs<CommonEnums> e)
            {
                if (e.Entity != null)
                {
                    this.txtEnumName.Text = e.Entity.EnumName;
                    this.txtFullEnumName.Text = e.Entity.FullEnumName;

                    this.txtMemberName.Text = e.Entity.MemberName;
                    this.txtMember.Text = e.Entity.Member;

                    this.txtIntValue.Text = e.Entity.IntValue.ToString();
                    this.txtOrderNo.Text = e.Entity.OrderNo.ToString();

                    this.txtFullEnumName.Enabled = this.txtEnumName.Enabled = this.txtMember.Enabled = this.txtIntValue.Enabled = false;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion


        #region ICommonEnumsEditView ��Ա

        public string FullEnumName
        {
            get { return this.Request["FullEnumName"]; }
        }

        public string Member
        {
            get { return this.Request["Member"]; }
        }

        #endregion

        #region ICommonEnumsView ��Ա

        public void ShowMessage(string message)
        {
            this.errMsg.Message = message;
        }

        #endregion
    }

}
