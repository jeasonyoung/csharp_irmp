//================================================================================
// FileName: OrgDepartmentPresenter.cs
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
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Service
{
	///<summary>
	/// IOrgDepartmentView�ӿڡ�
	///</summary>
	public interface IOrgDepartmentView: IModuleView
	{
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
        /// <summary>
        /// ���ϼ����š�
        /// </summary>
        /// <param name="data"></param>
        void BindParentDepartment(IListControlsTreeViewData data);
	}

    /// <summary>
    /// �б�ӿڡ�
    /// </summary>
    public interface IOrgDepartmentListView : IOrgDepartmentView
    {
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string DepartmentName
        {
            get;
        }
        /// <summary>
        /// ��ȡ�ϼ�����ID
        /// </summary>
        GUIDEx ParentDepartmentID
        {
            get;
        }
    }
    /// <summary>
    /// �༭�ӿڡ�
    /// </summary>
    public interface IOrgDepartmentEditView : IOrgDepartmentView
    {
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx DepartmentID { get; }
        /// <summary>
        /// �󶨲���״̬��
        /// </summary>
        /// <param name="data"></param>
        void BindDepartmentStatus(IListControlsData data);
    }
		
	///<summary>
	/// OrgDepartmentPresenter��Ϊ�ࡣ
	///</summary>
	public class OrgDepartmentPresenter: ModulePresenter<IOrgDepartmentView>
	{
		#region ��Ա���������캯����
        OrgDepartmentEntity orgDepartmentEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public OrgDepartmentPresenter(IOrgDepartmentView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Department_ModuleID;
            this.orgDepartmentEntity = new OrgDepartmentEntity();
		}
		#endregion

        #region ���ء�
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            this.View.BindParentDepartment(this.orgDepartmentEntity.Department);
            IOrgDepartmentEditView editView = this.View as IOrgDepartmentEditView;
            if (editView != null)
            {
                editView.BindDepartmentStatus(this.EnumDataSource(typeof(EnumStatus)));
            }

            base.PreViewLoadData();
        }
        #endregion

        #region ���ݲ���������
        /// <summary>
        /// �б����ݽӿڡ�
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IOrgDepartmentListView listView = this.View as IOrgDepartmentListView;
                if (listView != null)
                {
                    DataTable dtSource = this.orgDepartmentEntity.ListDataSource(listView.DepartmentName, listView.ParentDepartmentID);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("DepartmentStatusName", typeof(string));
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["DepartmentStatusName"] = this.GetEnumMemberName(typeof(EnumStatus), Convert.ToInt32(row["DepartmentStatus"]));
                        }
                        return dtSource.Copy();
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="primaryCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteData(StringCollection primaryCollection)
        {
            bool result = false;
            string str = string.Empty;
            foreach (string pri in primaryCollection)
            {
                result = this.orgDepartmentEntity.DeleteDepartment(pri, out str);
                if (!result)
                {
                    if (!string.IsNullOrEmpty(str))
                        this.View.ShowMessage(str);
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// ���沿����Ϣ���ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SaveEntityData(Domain.OrgDepartment data)
        {
            return this.orgDepartmentEntity.UpdateRecord(data);
        }

		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<Domain.OrgDepartment>> handler)
		{
            IOrgDepartmentEditView editView = this.View as IOrgDepartmentEditView;
            if (editView != null)
            {
                Domain.OrgDepartment data = new Domain.OrgDepartment();
                data.DepartmentID = editView.DepartmentID;
                if (this.orgDepartmentEntity.LoadRecord(ref data))
                {
                    editView.BindParentDepartment(this.orgDepartmentEntity.NotSelfGetOffSprings(data.DepartmentID));
                    handler(this, new EntityEventArgs<Domain.OrgDepartment>(data));
                }
            }
		}
		#endregion

	}

}
