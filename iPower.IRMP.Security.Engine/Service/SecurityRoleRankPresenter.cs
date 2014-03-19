//================================================================================
// FileName: SecurityRoleRankPresenter.cs
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
using System.Collections.Specialized;
using System.Text;
using System.Data;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Persistence;

using iPower.IRMP.Org;
namespace iPower.IRMP.Security.Engine.Service
{
	///<summary>
	/// ISecurityRoleRankView�ӿڡ�
	///</summary>
	public interface ISecurityRoleRankView: IModuleView
	{

	}
    /// <summary>
    /// ��ɫ��λ�����б�ӿڡ�
    /// </summary>
    public interface ISecurityRoleRankListView : ISecurityRoleRankView
    {
        /// <summary>
        /// ��ȡ��ɫ���ơ�
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// ��ȡ��λ�������ơ�
        /// </summary>
        string RankName { get; }
    }
    /// <summary>
    /// ��ɫ��λ����༭�ӿڡ�
    /// </summary>
    public interface ISecurityRoleRankEditView : ISecurityRoleRankView
    {
        /// <summary>
        /// ��ȡ��ɫID��
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// �󶨸�λ�������ݡ�
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindRank(IListControlsTreeViewData data);
        /// <summary>
        /// ѡ�С�
        /// </summary>
        /// <param name="selected"></param>
        void RankSelected(StringCollection selected);
    }
		
	///<summary>
	/// SecurityRoleRankPresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityRoleRankPresenter: ModulePresenter<ISecurityRoleRankView>
	{
		#region ��Ա���������캯����
        SecurityRoleRankEntity securityRoleRankEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SecurityRoleRankPresenter(ISecurityRoleRankView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RoleRank_ModuleID;
            this.securityRoleRankEntity = new SecurityRoleRankEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISecurityRoleRankListView listView = this.View as ISecurityRoleRankListView;
                if (listView != null)
                {
                    return this.securityRoleRankEntity.ListDataSource(listView.RoleName, listView.RankName);
                }
                return null;
            }
        }
        /// <summary>
        /// �������ݡ�
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISecurityRoleRankEditView editView = this.View as ISecurityRoleRankEditView;
            if (editView != null)
            {
                IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                if (facotry != null)
                {
                    editView.BindRank(new ListControlsTreeViewDataSource("RankName", "RankID", "ParentRankID", facotry.GetAllRank(null)));
                }
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
		{
            ISecurityRoleRankEditView editView = this.View as ISecurityRoleRankEditView;
            if (editView != null && editView.RoleID.IsValid)
            {
                iPower.IRMP.Security.Engine.Domain.SecurityRole role = new iPower.IRMP.Security.Engine.Domain.SecurityRole();
                role.RoleID = editView.RoleID;
                SecurityRoleEntity securityRoleEntity = new SecurityRoleEntity();
                if (securityRoleEntity.LoadRecord(ref role))
                {
                    handler(this, new EntityEventArgs<string[]>(new string[] { role.RoleID, role.RoleName }));
                    this.ChangeRole(role.RoleID);
                }
            }
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        public void ChangeRole(GUIDEx roleID)
        {
            ISecurityRoleRankEditView editView = this.View as ISecurityRoleRankEditView;
            if (editView != null)
            {
                editView.RankSelected(this.securityRoleRankEntity.GetRank(editView.RoleID));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="ranks"></param>
        /// <returns></returns>
        public bool UpdateRoleRank(GUIDEx role, StringCollection ranks)
        {
            return this.securityRoleRankEntity.UpdateRoleRank(role, ranks);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool BatchDeleteRoleRank(StringCollection roles)
        {
            return this.securityRoleRankEntity.BatchDeleteRoleRank(roles);
        }
		#endregion

	}

}
