//================================================================================
// FileName: frmSecurityRoleRightEdit.aspx.cs
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
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
	
using iPower;
using iPower.Web.TreeView;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
	///<summary>
	///frmSecurityRoleRightEdit列表页面后台代码。
	///</summary>
	public partial class frmSecurityRoleRightEdit:ModuleBasePage,ISecurityRoleRightEditView
	{
		#region 成员变量，构造函数。
		SecurityRoleRightPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSecurityRoleRightEdit()
		{
			this.presenter = new SecurityRoleRightPresenter(this);

		}
		#endregion

		#region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void pbRole_OnTextChanged(object sender, EventArgs e)
        {
            this.presenter.ChangeRole(this.pbRole.Value);
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            if (this.presenter.BatchSaveRoleRight(this.pbRole.Value, this.tvRole.CheckedValue))
                this.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<string[]>>(delegate(object sender, EntityEventArgs<string[]> e)
            {
                if (e.Entity != null && e.Entity.Length == 2)
                {
                    this.pbRole.Value = e.Entity[0];
                    this.pbRole.Text = e.Entity[1];
                    this.pbRole.Enabled = false;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISecurityRoleRightEditView 成员

        public GUIDEx RoleID
        {
            get { return this.RequestGUIEx("RoleID"); }
        }

        public void BindModuleRight(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.tvRole, data);
        }

        public void SelectedRight(StringCollection rightCollection)
        {
            if (rightCollection != null && rightCollection.Count > 0)
            {
                this.TreeViewNodeSelected(this.tvRole.Items, rightCollection);
                this.tvRole.DataBind();
            }
        }

        #endregion

        #region 辅助函数。
        void TreeViewNodeSelected(TreeViewNodeCollection tvCollection, StringCollection rightCollection)
        {
            if (tvCollection != null && rightCollection != null && rightCollection.Count > 0)
            {
                foreach (TreeViewNode node in tvCollection)
                {
                    foreach (string rid in rightCollection)
                    {
                        if (node.Value.EndsWith(rid))
                        {
                            node.Checked = true;
                            break;
                        }
                    }

                    if (node.Childs.Count > 0)
                        this.TreeViewNodeSelected(node.Childs, rightCollection);
                }
            }
        }
        #endregion
    }

}
