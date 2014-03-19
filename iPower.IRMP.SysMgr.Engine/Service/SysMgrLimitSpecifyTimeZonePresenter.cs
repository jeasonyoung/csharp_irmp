//================================================================================
// FileName: SysMgrLimitSpecifyTimeZonePresenter.cs
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
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrLimitSpecifyTimeZoneView�ӿڡ�
	///</summary>
	public interface ISysMgrLimitSpecifyTimeZoneView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitSpecifyTimeZoneListView : ISysMgrLimitSpecifyTimeZoneView
    {
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// �߽����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitSpecifyTimeZoneEditView : ISysMgrLimitSpecifyTimeZoneView
    {
        /// <summary>
        /// 
        /// </summary>
        GUIDEx ZoneID { get; }
        /// <summary>
        /// ����Ȩ״̬��
        /// </summary>
        /// <param name="data"></param>
        void BindAuthStatus(IListControlsData data);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
		
	///<summary>
	/// SysMgrLimitSpecifyTimeZonePresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrLimitSpecifyTimeZonePresenter: ModulePresenter<ISysMgrLimitSpecifyTimeZoneView>
	{
		#region ��Ա���������캯����
        SysMgrLimitSpecifyTimeZoneEntity sysMgrLimitSpecifyTimeZoneEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLimitSpecifyTimeZonePresenter(ISysMgrLimitSpecifyTimeZoneView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitSpecifyTimeZone_ModuleID;
            this.sysMgrLimitSpecifyTimeZoneEntity = new SysMgrLimitSpecifyTimeZoneEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ����Դ��
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrLimitSpecifyTimeZoneListView listView = this.View as ISysMgrLimitSpecifyTimeZoneListView;
                if (listView != null)
                {
                    DataTable dtSource = this.sysMgrLimitSpecifyTimeZoneEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("AuthStatusName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["AuthStatusName"] = this.GetEnumMemberName(typeof(EnumAuthStatus), Convert.ToInt32(row["AuthStatus"]));
                            string empId = Convert.ToString(row["EmployeeID"]);
                            if (string.IsNullOrEmpty(empId))
                            {
                                row["EmployeeName"] = "[ȫ��]";
                            }
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
        /// <summary>
        /// ���ؼ��ء�
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISysMgrLimitSpecifyTimeZoneEditView editView = this.View as ISysMgrLimitSpecifyTimeZoneEditView;
            if (editView != null)
            {
                editView.BindAuthStatus(this.EnumDataSource(typeof(EnumAuthStatus)));
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitSpecifyTimeZone>> handler)
		{
            ISysMgrLimitSpecifyTimeZoneEditView editView = this.View as ISysMgrLimitSpecifyTimeZoneEditView;
            if (editView != null && editView.ZoneID.IsValid)
            {
                SysMgrLimitSpecifyTimeZone data = new SysMgrLimitSpecifyTimeZone();
                data.ZoneID = editView.ZoneID;
                if (this.sysMgrLimitSpecifyTimeZoneEntity.LoadRecord(ref data))
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = "[ȫ��]";
                    handler(this, new EntityEventArgs<SysMgrLimitSpecifyTimeZone>(data));
                }
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitSpecifyTimeZone(SysMgrLimitSpecifyTimeZone data)
        {
            bool result = false;
            if (data != null)
            {
                try
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = string.Empty;

                    if (data.EndTime <= data.StartTime)
                        throw new Exception("��ʼʱ��Ӧ�����ڽ���ʱ��!");
                    else
                        result = this.sysMgrLimitSpecifyTimeZoneEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    ISysMgrLimitSpecifyTimeZoneEditView editView = this.View as ISysMgrLimitSpecifyTimeZoneEditView;
                    if (editView != null)
                        editView.ShowMessage(e.Message);
                }
            }
            return result;
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteLimitSpecifyTimeZone(StringCollection priCollection)
        {
            return this.sysMgrLimitSpecifyTimeZoneEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
