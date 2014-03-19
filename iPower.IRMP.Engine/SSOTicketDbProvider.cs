//================================================================================
//  FileName: SSOTicketDbProvider.cs
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
using iPower.IRMP.SSO;
using iPower.IRMP.Engine.Domain;
using iPower.IRMP.Engine.Persistence;
namespace iPower.IRMP.Engine
{
    /// <summary>
    /// 单点登录票据存储提供类。
    /// </summary>
    public class SSOTicketDbProvider : ISSOTicketDbProvider
    {
        #region 成员变量，构造函数。
        SSOTicketEntity sSOTicketEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SSOTicketDbProvider()
        {
            this.sSOTicketEntity = new SSOTicketEntity();
        }
        #endregion

        #region ISSOTicketDbProvider 成员
        /// <summary>
        /// 创建票据数据存储。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        public bool CreateTicket(SSOAuthTicket ticket, string clientIP)
        {
            bool result = false;
            if (ticket != null && ticket.HasValid)
            {
                string err = null;
                result = this.TicketVerification(ref ticket, clientIP, out err);
                if (!result)
                {
                    SSOTicket data = new SSOTicket();

                    data.Token = ticket.Token;

                    data.UserData = ticket.UserData;

                    data.IssueDate = ticket.IssueDate;
                    data.Expiration = ticket.Expiration;

                    data.IssueClientIP = clientIP;
                    data.RenewalCount = 0;
                    data.LastRenewalIP = string.Empty;

                    this.sSOTicketEntity.DestroyTicket(data.UserData);

                    result = this.sSOTicketEntity.UpdateRecord(data);
                }
            }
            return result;
        }
        /// <summary>
        /// 验证票据的存储。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns></returns>
        public bool TicketVerification(ref SSOAuthTicket ticket, string clientIP, out string err)
        {
            bool result = false;
            err = null;
            if (!ticket.HasValid)
                err = "票据无效。";
            else if (string.IsNullOrEmpty(clientIP))
                err = "客户端IP地址为空。";
            else
            {
                SSOTicket data = new SSOTicket();
                data.Token = ticket.Token;
                result = this.sSOTicketEntity.LoadRecord(ref data);
                if (!result)
                    err = "票据在数据库中不存在。";
                else if (!data.HasValid)
                    err = "数据库中的票据已无效。";
                else
                {
                    try
                    {
                        err = "票据有效。";
                        double interval = Math.Abs((ticket.Expiration - ticket.IssueDate).TotalSeconds);
                        if ((interval > 0) && (Math.Abs((data.Expiration - DateTime.Now).TotalSeconds) <= (interval / 3.0)))
                        {
                            DateTime newExpiration = data.Expiration.AddSeconds(interval);
                            ticket.Expiration = data.Expiration = newExpiration;
                            data.RenewalCount += 1;
                            data.LastRenewalIP = clientIP;
                            result = this.sSOTicketEntity.UpdateRecord(data);
                            err = "票据已续约。";
                        }
                    }
                    catch (Exception e)
                    {
                        err = e.Message;
                    }
                }
             }
            return result;
        }
        /// <summary>
        /// 销毁票据。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        public bool DestroyTicket(ref SSOAuthTicket ticket, string clientIP)
        {
            bool result = false;
            if (ticket.HasValid && !string.IsNullOrEmpty(clientIP))
            {
                DateTime expiration = DateTime.Now;
                SSOTicket data = new SSOTicket();
                data.Token = ticket.Token;
                if (this.sSOTicketEntity.LoadRecord(ref data))
                {
                    if (data.HasValid)
                    {
                        data.Expiration = expiration;
                        data.LastRenewalIP = clientIP;
                        result = this.sSOTicketEntity.UpdateRecord(data);
                    }
                }
                ticket.Expiration = expiration;
                if (!result)
                    result = true;
            }
            return result;
        }

        #endregion
    }
}