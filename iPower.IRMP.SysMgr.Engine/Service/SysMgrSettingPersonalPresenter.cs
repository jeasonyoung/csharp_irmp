//================================================================================
// FileName: SysMgrSettingPersonalPresenter.cs
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
	/// ISysMgrSettingPersonalView接口。
	///</summary>
	public interface ISysMgrSettingPersonalView: IModuleView
	{
        /// <summary>
        /// 错误消息
        /// </summary>
        /// <param name="Msg"></param>
        void ShowMessage(string Msg);
	}
    /// <summary>
    /// 列表界面接口
    /// </summary>
    public interface ISysMgrSettingPersonalListView : ISysMgrSettingPersonalView
    {
        /// <summary>
        /// 用于检索的用户名
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// 编辑界面接口
    /// </summary>
    public interface ISysMgrSettingPersonalEditView : ISysMgrSettingPersonalView
    {
        /// <summary>
        /// PersonalSettingID
        /// </summary>
        GUIDEx PersonalSettingID { get; }
    }
	///<summary>
	/// SysMgrSettingPersonalPresenter行为类。
	///</summary>
	public class SysMgrSettingPersonalPresenter: ModulePresenter<ISysMgrSettingPersonalView>
	{
		#region 成员变量，构造函数。
        SysMgrSettingPersonalEntity sysMgrSettingPersonalEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrSettingPersonalPresenter(ISysMgrSettingPersonalView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.SettingPersonal_ModuleID;
            this.sysMgrSettingPersonalEntity = new SysMgrSettingPersonalEntity();
            this.sysMgrSettingPersonalEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取列表数据源。
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
		///编辑页面加载数据。
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
        /// 更新
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
        /// 批量删除数据
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
