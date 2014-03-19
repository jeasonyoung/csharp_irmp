//================================================================================
// FileName: SysMgrSettingPersonalPresenter.cs
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
	/// ISysMgrSettingPersonalView�ӿڡ�
	///</summary>
	public interface ISysMgrSettingPersonalView: IModuleView
	{
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="Msg"></param>
        void ShowMessage(string Msg);
	}
    /// <summary>
    /// �б����ӿ�
    /// </summary>
    public interface ISysMgrSettingPersonalListView : ISysMgrSettingPersonalView
    {
        /// <summary>
        /// ���ڼ������û���
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// �༭����ӿ�
    /// </summary>
    public interface ISysMgrSettingPersonalEditView : ISysMgrSettingPersonalView
    {
        /// <summary>
        /// PersonalSettingID
        /// </summary>
        GUIDEx PersonalSettingID { get; }
    }
	///<summary>
	/// SysMgrSettingPersonalPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrSettingPersonalPresenter: ModulePresenter<ISysMgrSettingPersonalView>
	{
		#region ��Ա���������캯����
        SysMgrSettingPersonalEntity sysMgrSettingPersonalEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrSettingPersonalPresenter(ISysMgrSettingPersonalView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.SettingPersonal_ModuleID;
            this.sysMgrSettingPersonalEntity = new SysMgrSettingPersonalEntity();
            this.sysMgrSettingPersonalEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
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
                ISysMgrSettingPersonalListView ListView = this.View as ISysMgrSettingPersonalListView;
                if (ListView != null)
                {
                    return this.sysMgrSettingPersonalEntity.ListDataSource(ListView.EmployeeName);
                }
                return null;
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrSettingPersonal>> handler)
		{
            ISysMgrSettingPersonalEditView EditView = this.View as ISysMgrSettingPersonalEditView;
            if (EditView != null && EditView.PersonalSettingID.IsValid)
            {
                SysMgrSettingPersonal data = new SysMgrSettingPersonal();
                data.PersonalSettingID = EditView.PersonalSettingID;
                if (this.sysMgrSettingPersonalEntity.LoadRecord(ref data))
                {
                    SysMgrSetting sysMgrSetting = new SysMgrSetting();
                    sysMgrSetting.SettingID = data.SettingID;
                    if (new SysMgrSettingEntity().LoadRecord(ref sysMgrSetting))
                    {
                        data.SettingSign = sysMgrSetting.SettingSign;
                    }
                    handler(this, new EntityEventArgs<SysMgrSettingPersonal>(data));
                }
            }
		}

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrSettingPersonal(SysMgrSettingPersonal data)
        {
            if (data != null)
            {
                try
                {
                    return this.sysMgrSettingPersonalEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    this.View.ShowMessage(e.Message);
                }
            }
            return false;
        }
		#endregion

        /// <summary>
        /// ����ɾ������
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteSysMgrSettingPersonal(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                try
                {
                    result = this.sysMgrSettingPersonalEntity.DeleteRecord(priCollection);
                }
                catch (Exception e)
                {
                    this.View.ShowMessage(e.Message);
                }
            }
            return result;
        }

	}

}
