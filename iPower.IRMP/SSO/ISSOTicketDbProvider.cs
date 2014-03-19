//================================================================================
//  FileName: ISSOTicketProvider.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/9
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
    ///单点登录票据存储提供接口。
    /// </summary>
    public interface ISSOTicketDbProvider
    {
        /// <summary>
        /// 创建票据数据存储。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        bool CreateTicket(SSOAuthTicket ticket, string clientIP);
        /// <summary>
        /// 验证票据的存储。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns></returns>
        bool TicketVerification(ref SSOAuthTicket ticket, string clientIP, out string err);
        /// <summary>
        /// 销毁票据。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        bool DestroyTicket(ref SSOAuthTicket ticket, string clientIP);
    }
}
