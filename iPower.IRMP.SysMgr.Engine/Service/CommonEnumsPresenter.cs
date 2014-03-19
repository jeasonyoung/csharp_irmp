//================================================================================
// FileName: CommonEnumsPresenter.cs
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
using iPower.Data.ORM;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ICommonEnumsView接口。
	///</summary>
	public interface ICommonEnumsView: IModuleView
	{
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ICommonEnumsListView : ICommonEnumsView
    {
        /// <summary>
        /// 获取枚举名称。
        /// </summary>
        string EnumName { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ICommonEnumsEditView : ICommonEnumsView
    {
        /// <summary>
        /// 获取枚举全称。
        /// </summary>
        string FullEnumName { get; }
        /// <summary>
        /// 获取键名。
        /// </summary>
        string Member { get; }
    }
	///<summary>
	/// CommonEnumsPresenter行为类。
	///</summary>
	public class CommonEnumsPresenter: ModulePresenter<ICommonEnumsView>
	{
		#region 成员变量，构造函数。
        CommonEnumsEntity commonEnumsEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public CommonEnumsPresenter(ICommonEnumsView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.CommonEnums_ModuleID;
            this.commonEnumsEntity = new CommonEnumsEntity();
            this.commonEnumsEntity.DbEntityDataChangeLogEvent += new DbEntityDataChangeLogHandler(this.CreateCommonLog);
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取列表数据。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ICommonEnumsListView listView = this.View as ICommonEnumsListView;
                if (listView != null)
                {
                    DataTable dtSource = this.commonEnumsEntity.GetAllRecord(string.Format("EnumName like '%{0}%'", listView.EnumName), "EnumName,OrderNo");
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("FullEnumName");
                        string strFullEnumName = null;
                        foreach (DataRow row in dtSource.Rows)
                        {
                            strFullEnumName = Convert.ToString(row["EnumName"]);
                            if (!string.IsNullOrEmpty(strFullEnumName))
                            {
                                row["FullEnumName"] = strFullEnumName;
                                string[] strArr = strFullEnumName.Split('.');
                                if (strArr != null && strArr.Length > 0)
                                {
                                    row["EnumName"] = strArr[strArr.Length - 1];
                                }
                            }
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<CommonEnums>> handler)
		{
            ICommonEnumsEditView editView = this.View as ICommonEnumsEditView;
            if (editView != null && !string.IsNullOrEmpty(editView.FullEnumName) && !string.IsNullOrEmpty(editView.Member))
            {
                CommonEnums data = new CommonEnums();
                data.FullEnumName = editView.FullEnumName;
                data.Member = editView.Member;
                if (this.commonEnumsEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<CommonEnums>(data));
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateCommonEnums(CommonEnums data)
        {
            try
            {
                return this.commonEnumsEntity.UpdateRecord(data);
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
            return false;
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteCommonEnums(StringCollection priCollection)
        {
            bool result = false;
            try
            {
                if (priCollection != null && priCollection.Count > 0)
                {
                    foreach (string p in priCollection)
                    {
                        result = this.commonEnumsEntity.DeleteRecord(p);
                        if (!result)
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
            return result;
        }
		#endregion

	}

}
