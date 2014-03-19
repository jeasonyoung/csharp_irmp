//================================================================================
//  FileName: IAppRegister.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/30
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Text;

using iPower;
using iPower.Data;
namespace iPower.IRMP.Security
{
    /// <summary>
    /// 应用系统数据类。
    /// </summary>
    [Serializable]
    public class AppSystem
    {
        /// <summary>
        /// 获取或设置应用ID。
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 获取或设置应用名称。
        /// </summary>
        public string AppName { get; set; }
    }
    /// <summary>
    /// 应用系统数据集合。
    /// </summary>
    [Serializable]
    public class AppSystemCollection : DataCollection<AppSystem>
    {
        #region 索引。
        /// <summary>
        /// 获取系统数据。
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public AppSystem this[string appID]
        {
            get
            {
                if (!string.IsNullOrEmpty(appID))
                {
                    AppSystem app = this.Items.Find(new Predicate<AppSystem>(delegate(AppSystem data)
                    {
                        return data.AppID == appID;
                    }));
                    return app;
                }
                return null;
            }
        }
        #endregion
    }
}
