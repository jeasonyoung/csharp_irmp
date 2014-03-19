//================================================================================
//  FileName:ModulePresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-10 17:07:50
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================


using System;
using System.Collections.Generic;
using System.Text;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Service
{
    /// <summary>
    /// 模块UI接口。
    /// </summary>
    public interface IModuleView : IBaseView
    {
    }
    /// <summary>
    /// 模块行为基础类。
    /// </summary>
    /// <typeparam name="T">视图接口。</typeparam>
    public class ModulePresenter<T> : BasePresenter<T, ModuleConfiguration>
        where T : IModuleView
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public ModulePresenter(T view)
            : base(view)
        {
        }
        #endregion

        #region 重载。
        protected override ModuleConfiguration CreateModuleConfiguration()
        {
            return ModuleConfiguration.ModuleConfig;
        }
        #endregion
    }
}
