//================================================================================
//  FileName: FlowChartHandler.ashx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/18
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
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
    /// <summary>
    /// 流程实例图。
    /// </summary>
    [WebService(Namespace = "http://yaesoft.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FlowChartHandler : IHttpHandler, IFlowChartView
    {
        #region 成员变量，构造函数。
        FlowChartService pService = null;
        /// <summary>
        ///  构造函数。
        /// </summary>
        public FlowChartHandler()
        {
            this.pService = new FlowChartService(this);
        }
        #endregion

        #region IHttpHandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            if (context != null)
            {
                try
                {
                    context.Response.Clear();
                    context.Response.ContentType = "image/gif";
                    this.pService.DrawFlowChart(context.Response.OutputStream);
                }
                catch (Exception e)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(e.Message);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region InstanceFlowChartView 成员
        /// <summary>
        /// 获取图像宽度。
        /// </summary>
        public int Width
        {
            get
            {
                try
                {
                    string s = HttpContext.Current.Request["Width"];
                    if (!string.IsNullOrEmpty(s))
                        return int.Parse(s);
                }
                catch (Exception) { }
                return 720;

            }
        }
        /// <summary>
        /// 获取图像高度。
        /// </summary>
        public int Height
        {
            get
            {
                try
                {
                    string s = HttpContext.Current.Request["Height"];
                    if (!string.IsNullOrEmpty(s))
                        return int.Parse(s);
                }
                catch (Exception) { }
                return 540;
            }
        }
        /// <summary>
        /// 获取流程实例ID。
        /// </summary>
        public GUIDEx ProcessInstanceID
        {
            get
            {
                return HttpContext.Current.Request["ProcessInstanceID"];
            }
        }
        /// <summary>
        ///获取流程ID。
        /// </summary>
        public GUIDEx ProcessID
        {
            get
            {
                return HttpContext.Current.Request["ProcessID"];
            }
        }
        #endregion
    }
}
