//================================================================================
//  FileName:UserPickerFactory.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-15 16:14:06
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

using iPower.Utility;
namespace Yaesoft.IRMP.Flow.UserPickers
{
    /// <summary>
    /// 用户信息工厂接口。
    /// </summary>
    public interface IUserPickerFactory
    {
        /// <summary>
        /// 创建用户信息集合。
        /// </summary>
        /// <returns></returns>
        UserPickerEmployeeInfoCollection CreateEmployees();
        /// <summary>
        /// 创建角色信息集合。
        /// </summary>
        /// <returns></returns>
        UserPickerRoleInfoCollection CreateRoles();
        /// <summary>
        /// 创建岗位级别集合。
        /// </summary>
        /// <returns></returns>
        UserPickerRankInfoCollection CreateRanks();
        /// <summary>
        /// 创建岗位集合。
        /// </summary>
        /// <returns></returns>
        UserPickerPostInfoCollection CreatePosts();
    }
    /// <summary>
    /// 用户工厂。
    /// </summary>
    public class UserPickerFactory
    {
        #region 成员变量，构造函数。
        IUserPickerFactory pickerFactory = null;
        static object sync = new object();
        /// <summary>
        /// 构造函数。
        /// </summary>
        private UserPickerFactory(IUserPickerFactory factory)
        {
            this.pickerFactory = factory;
        }
        #endregion

        #region 静态函数。
        /// <summary>
        /// 创建实例对象。
        /// </summary>
        /// <param name="className">类全名称。</param>
        /// <param name="assemblyName">程序集。</param>
        /// <returns>实例对象。</returns>
        public static UserPickerFactory CreateInstance(string className, string assemblyName)
        {
            lock (UserPickerFactory.sync)
            {
                UserPickerFactory factory = null;
                if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(assemblyName))
                {
                    IUserPickerFactory picker = TypeHelper.Create(className, assemblyName) as IUserPickerFactory;
                    if (picker != null)
                    {
                        factory = new UserPickerFactory(picker);
                    }
                }
                return factory;
            }
        }
        #endregion

        #region 公共函数。
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserPickerEmployeeInfoCollection Employees()
        {
            return this.pickerFactory.CreateEmployees();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserPickerRoleInfoCollection Roles()
        {
            return this.pickerFactory.CreateRoles();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserPickerRankInfoCollection Ranks()
        {
            return this.pickerFactory.CreateRanks();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserPickerPostInfoCollection Posts()
        {
            return this.pickerFactory.CreatePosts();
        }
        #endregion

    }
}
