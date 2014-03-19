//================================================================================
//  FileName: SSOTicketDbProviderService.asmx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/16
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
using System.Web.Services;

using iPower;
using iPower.IRMP.SSO;
using iPower.IRMP.Engine;
namespace iPower.IRMP.Web
{
    /// <summary>
    /// 提供单点登录票据存储。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/",
                Name = "提供单点登录票据存储。",
                Description = "提供单点登录票据存储。")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SSOTicketDbProviderService : System.Web.Services.WebService
    {
        #region 成员变量，构造函数。
        SSOTicketDbProvider provider = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SSOTicketDbProviderService()
        {
            this.provider = new SSOTicketDbProvider();
        }
        #endregion

         /// <summary>
        /// 创建票据数据存储。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        [WebMethod(Description="创建票据数据存储。")]
        public CallResult CreateTicket(string ticket, string clientIP)
        {
            try
            {
                if (string.IsNullOrEmpty(ticket))
                    return new CallResult(-1, "票据为空。");
                else if (string.IsNullOrEmpty(clientIP))
                    return new CallResult(-1, "客户端IP地址为空。");
                else
                {
                    SSOAuthTicket authTicket = new SSOAuthTicket(ticket);
                    if (authTicket == null)
                        return new CallResult(-1, "票据格式不正确。");
                    else if (!authTicket.HasValid)
                        return new CallResult(-1, "票据无效。");
                    else
                    {
                        bool result = this.provider.CreateTicket(authTicket, clientIP);
                        return new CallResult(result ? 0 : -1, result ? "创建票据存储成功。" : "发生未知异常。");
                    }
                }
            }
            catch (Exception e)
            {
                return new CallResult(-1, e.Message);
            }
        }
        /// <summary>
        /// 验证票据的存储。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        [WebMethod(Description = " 验证票据的存储。")]
        public SSOCallResult TicketVerification(string ticket, string clientIP)
        {
            try
            {
                if (string.IsNullOrEmpty(ticket))
                    return new SSOCallResult(-1, "票据为空。");
                else if (string.IsNullOrEmpty(clientIP))
                    return new SSOCallResult(-1, "客户端IP地址为空。");
                else
                {
                    SSOAuthTicket authTicket = new SSOAuthTicket(ticket);
                    if (authTicket == null)
                        return new SSOCallResult(-1, "票据格式不正确。");
                    else if (!authTicket.HasValid)
                        return new SSOCallResult(-1, "票据无效。");
                    else
                    {
                        string err = null;
                        bool result = this.provider.TicketVerification(ref authTicket, clientIP, out err);
                        if (result)
                            return new SSOCallResult(0, authTicket.ToString(), err);
                        return new SSOCallResult(-1, err);
                    }
                }
            }
            catch (Exception e)
            {
                return new SSOCallResult(-1, e.Message);
            }
        }
        /// <summary>
        /// 销毁票据。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        [WebMethod(Description = "销毁票据。")]
        public SSOCallResult DestroyTicket(string ticket, string clientIP)
        {
            try
            {
                if (string.IsNullOrEmpty(ticket))
                    return new SSOCallResult(-1, "票据为空。");
                else if (string.IsNullOrEmpty(clientIP))
                    return new SSOCallResult(-1, "客户端IP地址为空。");
                else
                {
                    SSOAuthTicket authTicket = new SSOAuthTicket(ticket);
                    if (authTicket == null)
                        return new SSOCallResult(-1, "票据格式不正确。");
                    else if (!authTicket.HasValid)
                        return new SSOCallResult(-1, "票据无效。");
                    else
                    {
                        bool result = this.provider.DestroyTicket(ref authTicket, clientIP);
                        if (result)
                            return new SSOCallResult(0, authTicket.ToString(), "销毁票据成功。");
                        return new SSOCallResult(-1, "发生未知错误。");
                    }
                }
            }
            catch (Exception e)
            {
                return new SSOCallResult(-1, e.Message);
            }
        }
    }
}
