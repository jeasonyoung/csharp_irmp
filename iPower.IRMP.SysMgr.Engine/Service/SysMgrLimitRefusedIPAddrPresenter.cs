//================================================================================
// FileName: SysMgrLimitRefusedIPAddrPresenter.cs
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
	/// ISysMgrLimitRefusedIPAddrView�ӿڡ�
	///</summary>
	public interface ISysMgrLimitRefusedIPAddrView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitRefusedIPAddrListView : ISysMgrLimitRefusedIPAddrView
    {
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName
        {
            get;
        }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISysMgrLimitRefusedIPAddrEditView : ISysMgrLimitRefusedIPAddrView
    {
        /// <summary>
        /// ��ȡ�ܾ�ID��
        /// </summary>
        GUIDEx RefusedID { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
		
	///<summary>
	/// SysMgrLimitRefusedIPAddrPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrLimitRefusedIPAddrPresenter: ModulePresenter<ISysMgrLimitRefusedIPAddrView>
	{
		#region ��Ա���������캯����
        SysMgrLimitRefusedIPAddrEntity sysMgrLimitRefusedIPAddrEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLimitRefusedIPAddrPresenter(ISysMgrLimitRefusedIPAddrView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitRefusedIPAddr_ModuleID;
            this.sysMgrLimitRefusedIPAddrEntity = new SysMgrLimitRefusedIPAddrEntity();
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
                ISysMgrLimitRefusedIPAddrListView listView = this.View as ISysMgrLimitRefusedIPAddrListView;
                if (listView != null)
                {
                    DataTable dtSource = this.sysMgrLimitRefusedIPAddrEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
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
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitRefusedIPAddr>> handler)
		{
            ISysMgrLimitRefusedIPAddrEditView editView = this.View as ISysMgrLimitRefusedIPAddrEditView;
            if (editView != null && editView.RefusedID.IsValid)
            {
                SysMgrLimitRefusedIPAddr data = new SysMgrLimitRefusedIPAddr();
                data.RefusedID = editView.RefusedID;
                if (this.sysMgrLimitRefusedIPAddrEntity.LoadRecord(ref data))
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = "[ȫ��]";
                    handler(this, new EntityEventArgs<SysMgrLimitRefusedIPAddr>(data));
                }
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitRefusedIPAddr(SysMgrLimitRefusedIPAddr data)
        {
            if (data != null)
            {
                try
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = string.Empty;
                    return this.sysMgrLimitRefusedIPAddrEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    ISysMgrLimitRefusedIPAddrEditView editView = this.View as ISysMgrLimitRefusedIPAddrEditView;
                    if (editView != null)
                        editView.ShowMessage(e.Message);
                }
            }
            return false;
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteLimitRefusedIPAddr(StringCollection priCollection)
        {
            return this.sysMgrLimitRefusedIPAddrEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
