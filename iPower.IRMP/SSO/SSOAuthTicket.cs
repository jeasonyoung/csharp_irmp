//================================================================================
//  FileName: SSOAuthTicket.cs
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
using System.Text;

using iPower;
namespace iPower.IRMP.SSO
{
    /// <summary>
    /// 单点登录票据类。
    /// </summary>
    [Serializable]
    public sealed class SSOAuthTicket : Ticket, IUser
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SSOAuthTicket()
            : this(string.Empty)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ticket">票据串。</param>
        public SSOAuthTicket(string ticket)
            : base(ticket)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="token">令牌。</param>
        /// <param name="userInfo">用户数据。</param>
        /// <param name="issueDate">票据发布时间。</param>
        /// <param name="expiration">票据过期时间。</param>
        public SSOAuthTicket(string token, IUser userInfo, DateTime issueDate, DateTime expiration)
            : base(1.0, "sha1", token, issueDate, expiration, string.Format("{0},{1}", userInfo.CurrentUserID, userInfo.CurrentUserName))
        {
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 获取票据版本。
        /// </summary>
        public override double Ver
        {
            get
            {
                return base.Ver;
            }
        }
        /// <summary>
        /// 获取算法标识。
        /// </summary>
        public override string AL
        {
            get
            {
                return base.AL;
            }
        }
        #endregion

        #region IUser 成员
        /// <summary>
        /// 获取用户ID。
        /// </summary>
        public GUIDEx CurrentUserID
        {
            get
            {
                string[] str = this.UserData.Split(',');
                if (str != null && str.Length == 2)
                {
                    return str[0];
                }
                return null;
            }
            set
            {
                this.UserData = string.Concat(value, ",", this.CurrentUserName);
            }
        }
        /// <summary>
        /// 获取用户姓名。
        /// </summary>
        public string CurrentUserName
        {
            get
            {
                string[] str = this.UserData.Split(',');
                if (str != null && str.Length == 2)
                {
                    return str[1];
                }
                return null;
            }
            set
            {
                this.UserData = string.Concat(this.CurrentUserID, ",", value);
            }
        }

        #endregion
    }
}
