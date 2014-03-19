//================================================================================
//  FileName: ModuleBasePage.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/27
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
using System.Web;

using iPower.Platform.UI;

namespace iPower.IRMP.SysMgr.Web
{
    /// <summary>
    /// 模块模板页基础类。
    /// </summary>
    public class ModuleBaseMasterPage : BaseModuleMasterPage
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleBaseMasterPage()
        {
        }
        #endregion
    }
    /// <summary>
    /// 项目页面基类。
    /// </summary>
    public class ModuleBasePage : BaseModulePage
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleBasePage()
        {
        }
        #endregion
    }

    /// <summary>
    /// 用户控件基类
    /// </summary>
    public class ModuleBaseControl : BaseModuleControl
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleBaseControl()
        {
        }
        #endregion
    }
}
