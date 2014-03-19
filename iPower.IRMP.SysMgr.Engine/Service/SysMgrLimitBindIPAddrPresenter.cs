//================================================================================
// FileName: SysMgrLimitBindIPAddrPresenter.cs
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
	/// ISysMgrLimitBindIPAddrView�ӿڡ�
	///</summary>
	public interface ISysMgrLimitBindIPAddrView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitBindIPAddrListView : ISysMgrLimitBindIPAddrView
    {
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitBindIPAddrEditView : ISysMgrLimitBindIPAddrView
    {
        /// <summary>
        ///��ȡ��ID��
        /// </summary>
        GUIDEx BindID { get; }
        /// <summary>
        /// ��ʾ���ݡ�
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
	///<summary>
	/// SysMgrLimitBindIPAddrPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrLimitBindIPAddrPresenter: ModulePresenter<ISysMgrLimitBindIPAddrView>
	{
		#region ��Ա���������캯����
        SysMgrLimitBindIPAddrEntity sysMgrLimitBindIPAddrEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLimitBindIPAddrPresenter(ISysMgrLimitBindIPAddrView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitBindIPAddr_ModuleID;
            this.sysMgrLimitBindIPAddrEntity = new SysMgrLimitBindIPAddrEntity();
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
                ISysMgrLimitBindIPAddrListView listView = this.View as ISysMgrLimitBindIPAddrListView;
                if (listView != null)
                {
                    DataTable dtSource = this.sysMgrLimitBindIPAddrEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
                    if (dtSource != null)
                    {
                        foreach (DataRow row in dtSource.Rows)
                        {
                            string strEmp = Convert.ToString(row["EmployeeName"]);
                            if (string.IsNullOrEmpty(strEmp))
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
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitBindIPAddr>> handler)
		{
            ISysMgrLimitBindIPAddrEditView editView = this.View as ISysMgrLimitBindIPAddrEditView;
            if (editView != null && editView.BindID.IsValid)
            {
                SysMgrLimitBindIPAddr data = new SysMgrLimitBindIPAddr();
                data.BindID = editView.BindID;
                if (this.sysMgrLimitBindIPAddrEntity.LoadRecord(ref data))
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = "[ȫ��]";
                    handler(this, new EntityEventArgs<SysMgrLimitBindIPAddr>(data));
                }
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitBindIPAddr(SysMgrLimitBindIPAddr data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = string.Empty;
                    return this.sysMgrLimitBindIPAddrEntity.UpdateRecord(data);
                }
            }
            catch (Exception e)
            {
                ISysMgrLimitBindIPAddrEditView editView = this.View as ISysMgrLimitBindIPAddrEditView;
                if (editView != null)
                    editView.ShowMessage(e.Message);
            }
            return result;
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteLimitBindIPAddr(StringCollection priCollection)
        {
            if (priCollection != null && priCollection.Count > 0)
            {
                return this.sysMgrLimitBindIPAddrEntity.DeleteRecord(priCollection);
            }
            return false;
        }
		#endregion

	}

}
