//================================================================================
// FileName: SysMgrLinksPresenter.cs
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
	/// ISysMgrLinksView接口。
	///</summary>
	public interface ISysMgrLinksView: IModuleView
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
    public interface ISysMgrLinksListview : ISysMgrLinksView
    {
        /// <summary>
        /// 链接名称
        /// </summary>
        string LinkName { get; }
    }
    /// <summary>
    /// 编辑界面接口
    /// </summary>
    public interface ISysMgrLinksEditview : ISysMgrLinksView
    {
        /// <summary>
        /// 绑定链接方式
        /// </summary>
        /// <param name="data"></param>
        void BindLinkTarget(IListControlsData data);
        /// <summary>
        /// 绑定链接状态
        /// </summary>
        /// <param name="data"></param>
        void BindLinkStatus(IListControlsData data);
        /// <summary>
        /// LinkID
        /// </summary>
        GUIDEx LinkID { get; }
    }
	///<summary>
	/// SysMgrLinksPresenter行为类。
	///</summary>
	public class SysMgrLinksPresenter: ModulePresenter<ISysMgrLinksView>
	{
		#region 成员变量，构造函数。
        SysMgrLinksEntity sysMgrLinksEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrLinksPresenter(ISysMgrLinksView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Links_ModuleID;
            this.sysMgrLinksEntity = new SysMgrLinksEntity();
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取列表数据源
        /// </summary>
        public DataTable ListDataSource
        {
            get 
            {
                ISysMgrLinksListview ListView = this.View as ISysMgrLinksListview;
                if (ListView != null)
                {
                    DataTable dtSource = this.sysMgrLinksEntity.ListDataSource(ListView.LinkName);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("LinkTargetName");
                        dtSource.Columns.Add("LinkStatusName");
                        string strEmployeeName = string.Empty;
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["LinkTargetName"] = this.GetEnumMemberName(typeof(EnumLinkTarget), Convert.ToInt32(row["LinkTarget"]));
                            row["LinkStatusName"] = this.GetEnumMemberName(typeof(EnumLinkStatus), Convert.ToInt32(row["LinkStatus"]));
                            strEmployeeName = Convert.ToString(row["EmployeeName"]);
                            if (string.IsNullOrEmpty(strEmployeeName))
                                row["EmployeeName"] = "[全局]";
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
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLinks>> handler)
		{
            ISysMgrLinksEditview editview = this.View as ISysMgrLinksEditview;
            if (editview != null && editview.LinkID.IsValid)
            {
                SysMgrLinks data = new SysMgrLinks();
                data.LinkID = editview.LinkID;
                if (this.sysMgrLinksEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<SysMgrLinks>(data));
            }
		}
        /// <summary>
        /// 重载
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISysMgrLinksEditview editview = this.View as ISysMgrLinksEditview;
            if (editview != null)
            {
                editview.BindLinkTarget(this.EnumDataSource(typeof(EnumLinkTarget)));
                editview.BindLinkStatus(this.EnumDataSource(typeof(EnumLinkStatus)));
                return;
            }

        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrLinks(SysMgrLinks data)
        {
            ISysMgrLinksEditview editview = this.View as ISysMgrLinksEditview;
            if (editview != null && data != null)
            {
                try
                {
                    return this.sysMgrLinksEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    editview.ShowMessage(e.Message);
                }
            }
            return false;
        }
		#endregion
        /// <summary>
        /// 批量删除操作
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDleteSysMgrLinks(StringCollection priCollection)
        {
            bool result = false;
             if (priCollection != null && priCollection.Count > 0)
            {
                try
                {
                    result = this.sysMgrLinksEntity.DeleteRecord(priCollection);
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
