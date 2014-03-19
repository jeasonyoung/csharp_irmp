//================================================================================
//  FileName: SSOCallResult.cs
//  Desc:单点登录的调用结果返回类。
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
using System.Xml.Serialization;

using iPower;
namespace iPower.IRMP.SSO
{
    /// <summary>
    /// 单点登录的调用结果返回类。
    /// </summary>
    [Serializable]
    public class SSOCallResult : CallResult
    {
        #region 成员变量，构造函数。
        string ticket = string.Empty;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SSOCallResult()
            : this(-1, "未知结果。")
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="resultCode">结果值。</param>
        /// <param name="resultMessage">结果信息。</param>
        public SSOCallResult(int resultCode, string resultMessage)
            : this(resultCode, string.Empty, resultMessage)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="resultCode">结果值。</param>
        /// <param name="ticket">票据。</param>
        /// <param name="resultMessage">结果信息。</param>
        public SSOCallResult(int resultCode, string ticket, string resultMessage)
            : base(resultCode, resultMessage)
        {
            this.ticket = ticket;
        }
        #endregion

        /// <summary>
        /// 获取或设置票据串。
        /// </summary>
        public string Ticket
        {
            get { return this.ticket; }
            set { this.ticket = value; }
        }
    }
}
