//================================================================================
//  FileName: WebPartFactory.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/30
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
using System.Data;

using iPower.Platform.WebPart;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine
{
    /// <summary>
    /// WebPart管理类。
    /// </summary>
    public class WebPartFactory: IWebPartMgr
    {
        #region 成员变量，构造函数。
        SysMgrWebPartPersonalEntity sysMgrWebPartPersonalEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public WebPartFactory()
        {
            this.sysMgrWebPartPersonalEntity = new SysMgrWebPartPersonalEntity();
        }
        #endregion

        #region IWebPartMgr 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneMode"></param>
        /// <param name="systemID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public WebPartQueryCollection QueryList(EnumWebPartAlignment zoneMode, string systemID, string employeeID)
        {
            lock (this)
            {
                #region 参数验证。
                if (string.IsNullOrEmpty(systemID) || string.IsNullOrEmpty(employeeID))
                    return null;
                #endregion

                EnumZoneMode mode = EnumZoneMode.Middle;
                #region 转换位置。
                switch (zoneMode)
                {
                    case EnumWebPartAlignment.Left:
                        mode = EnumZoneMode.Left;
                        break;
                    case EnumWebPartAlignment.Middle:
                        mode = EnumZoneMode.Middle;
                        break;
                    case EnumWebPartAlignment.Right:
                        mode = EnumZoneMode.Right;
                        break;
                }
                #endregion

                WebPartQueryCollection collection = null;
                const string sql = "exec spSysMgrWebPartFactory '{0}','{1}',{2}";
                DataTable dtSource = this.sysMgrWebPartPersonalEntity.DatabaseAccess.ExecuteDataset(string.Format(sql, systemID, employeeID, (int)mode)).Tables[0];
                if (dtSource != null)
                {
                    collection = new WebPartQueryCollection();
                    collection.InitAssignment(dtSource);
                }
                return collection;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personalWebPartID"></param>
        /// <returns></returns>
        public WebPartPropertyCollection QueryProperties(string personalWebPartID)
        {
            lock (this)
            {
                WebPartPropertyCollection collection = null;
                if (!string.IsNullOrEmpty(personalWebPartID))
                {
                    const string sql = "exec spSysMgrWebPartProperties '{0}'";
                    DataTable dtSource = this.sysMgrWebPartPersonalEntity.DatabaseAccess.ExecuteDataset(string.Format(sql, personalWebPartID)).Tables[0];
                    if (dtSource != null)
                    {
                        collection = new WebPartPropertyCollection();
                        collection.InitAssignment(dtSource);
                    }
                }
                return collection;
            }
        }

        #endregion
    }
}
