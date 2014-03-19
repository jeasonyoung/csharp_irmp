//================================================================================
//  FileName: AuthenticationProvider.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/13
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
using iPower.Cryptography;
using iPower.IRMP.SSO;
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine
{
    /// <summary>
    /// 提供单点登录身份认证。
    /// </summary>
    public class AuthenticationProvider : IAuthenticationProvider
    {
        #region 成员变量，构造函数。
        OrgEmployeeEntity orgEmployeeEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public AuthenticationProvider()
        {
            this.orgEmployeeEntity = new OrgEmployeeEntity();
        }
        #endregion

        #region IAuthenticationProvider 成员
        /// <summary>
        /// 验证用户账号和密码。
        /// </summary>
        /// <param name="userSign">用户登录账号。</param>
        /// <param name="password">用户登录密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果用户名和密码有效，则返回用户信息；否则返回null。</returns>
        public IUser UserAuthorizationVerification(string userSign, string password, out string err)
        {
            err = null;
            IUser userInfo = null;
            try
            {
                if (string.IsNullOrEmpty(userSign))
                    err = "用户账号为空！";
                else if (string.IsNullOrEmpty(password))
                    err = "用户密码为空！";
                else
                {
                   Domain.OrgEmployee orgEmployee = this.orgEmployeeEntity.GetEmployee(userSign);
                    if (orgEmployee == null)
                        err = "用户账号不存在！";
                    else
                    {
                        string encryptPassword = HashCrypto.Hash(password, "md5");
                        if (!string.IsNullOrEmpty(encryptPassword))
                            encryptPassword = HashCrypto.Hash(encryptPassword, "md5");
                        EnumStatus status = (EnumStatus)orgEmployee.EmployeeStatus;
                        if (status == EnumStatus.Stop)
                            err = "用户已经被停用！";
                        else if (orgEmployee.EmployeePassword != encryptPassword)
                            err = "用户密码不正确！";
                        else
                        {
                            UserInfo info = new UserInfo();
                            info.CurrentUserID = orgEmployee.EmployeeID;
                            info.CurrentUserName = orgEmployee.EmployeeName;
                            userInfo = info;
                            err = "验证用户成功。";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return userInfo;
        }
        /// <summary>
        /// 修改用户登录密码。
        /// </summary>
        /// <param name="userSign">用户登录账号。</param>
        /// <param name="oldPassword">用户旧密码。</param>
        /// <param name="newPassword">用户新密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果修改成功，则返回True；否则返回false。</returns>
        public bool ChangePassword(string userSign, string oldPassword, string newPassword, out string err)
        {
            bool result = false;
            err = null;
            try
            {
                if (string.IsNullOrEmpty(userSign))
                    err = "用户账号为空！";
                else if (string.IsNullOrEmpty(oldPassword))
                    err = "用户旧密码为空！";
                else if (string.IsNullOrEmpty(newPassword))
                    err = "用户新密码为空！";
                else
                {
                    string encryptOldPassword = HashCrypto.Hash(oldPassword, "md5");
                    if(!string.IsNullOrEmpty(encryptOldPassword))
                        encryptOldPassword = HashCrypto.Hash(encryptOldPassword, "md5");
                    Domain.OrgEmployee orgEmployee = this.orgEmployeeEntity.GetEmployee(userSign);
                    if (orgEmployee == null)
                        err = "用户账号不存在！";
                    else if (orgEmployee.EmployeePassword != encryptOldPassword)
                        err = "用户密码不正确！";
                    else
                    {
                        orgEmployee.EmployeePassword2 = orgEmployee.EmployeePassword;
                        orgEmployee.EmployeePassword = newPassword;
                        orgEmployee.PasswordDate2 = DateTime.Now;
                        result = this.orgEmployeeEntity.UpdateRecord(orgEmployee);
                        err = "修改用户密码成功。";
                    }
                }
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return result;
        }

        #endregion

        #region 内置类。
        class UserInfo : IUser
        {
            #region IUser 成员

            public GUIDEx CurrentUserID
            {
                get;
                set;
            }

            public string CurrentUserName
            {
                get;
                set;
            }

            #endregion
        }
        #endregion
    }
}
