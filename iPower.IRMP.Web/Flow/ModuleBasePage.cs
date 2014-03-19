//================================================================================
//  FileName:ModuleBasePage.cs
//  Desc:
//
//  Called by
//
//  Auth:iPowerYoung
//  Date:2010-12-10 17:25:58
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Web;

using iPower.Platform.UI;
namespace iPower.IRMP.Flow.Web
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
