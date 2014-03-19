//================================================================================
//  FileName: ModulePresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/24
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
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Service
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
    /// <typeparam name="T">UI视图接口。</typeparam>
    public class ModulePresenter<T> : BasePresenter<T, ModuleConfiguration>
        where T : IModuleView
    {

        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view">UI视图接口对象。</param>
        public ModulePresenter(T view)
            : base(view)
        {
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 获取模块配置实例。
        /// </summary>
        /// <returns></returns>
        protected override ModuleConfiguration CreateModuleConfiguration()
        {
            return ModuleConfiguration.ModuleConfig;
        }
        #endregion
    }
}
