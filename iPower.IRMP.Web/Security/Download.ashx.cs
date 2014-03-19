//================================================================================
//  FileName: Download.ashx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/31
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
using System.Text;
using System.Xml;
using System.IO;

using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
    /// <summary>
    /// 下载委托。
    /// </summary>
    /// <param name="output"></param>
    public delegate void DownloadHandler(Stream output);
    /// <summary>
    /// 下载数据。
    /// </summary>
    public class Download : IHttpHandler
    {
        #region 成员变量，构造函数。
        string contentType = "application/OCTET-STREAM";
        string className = string.Empty, query = string.Empty;

        const string CONST_ContentType = "type";
        const string CONST_ClassName = "class";
        const string CONST_Query = "query";
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Download()
        {
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            this.contentType = context.Request[CONST_ContentType];
            this.className = context.Request[CONST_ClassName];
            this.query = context.Request[CONST_Query];
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            this.DownloadData(context.Response);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 下载。
        /// </summary>
        /// <param name="resp"></param>
        protected void DownloadData(HttpResponse resp)
        {
            string fileName = string.Empty;
            switch (this.className)
            {
                case "ExportAction":
                    fileName = string.Format("SecurityAction_{0:yyyyMMddHHmmss}.xml", DateTime.Now);
                    XmlDocument doc = SecurityActionPresenter.DownloadSecurityAction(this.query);
                    this.StartDownload(resp, fileName,
                        new DownloadHandler(delegate(Stream output)
                        {
                            doc.Save(output);
                        }));
                    return;
            }
        }
        /// <summary>
        /// 开始下载。
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="fileName"></param>
        /// <param name="handler"></param>
        protected void StartDownload(HttpResponse resp, string fileName, DownloadHandler handler)
        {
            if (resp != null && handler != null)
            {
                resp.Clear();
                resp.Buffer = true;
                resp.Charset = "gb2312";
                resp.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                resp.ContentEncoding = Encoding.GetEncoding("gb2312");//设置输出流为简体中文
                resp.ContentType = this.contentType;
                // output.Save(resp.OutputStream);
                handler(resp.OutputStream);
                resp.Flush();
                resp.End();
            }
        }

        #region 静态函数。
        /// <summary>
        /// 创建传值字符串。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="className"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string CreateDownloadQueryString(string type, string className, string query)
        {
            return string.Concat("?", CONST_ContentType, "=", type, "&", CONST_ClassName, "=", className, "&", CONST_Query, "=", query);
        }
        #endregion
    }
}
