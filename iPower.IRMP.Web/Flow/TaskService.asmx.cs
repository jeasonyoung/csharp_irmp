//================================================================================
//  FileName: TaskService.asmx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/17
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

using iPower.Platform.WebPart;
using iPower.IRMP.Flow.Engine;
namespace iPower.IRMP.Flow.Web
{
    /// <summary>
    /// 流程任务服务。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/",
                Name = "流程任务服务接口。",
                Description = "该接口为待办待阅提供服务接口。")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TaskService : System.Web.Services.WebService
    {
        #region 成员变量，构造函数。
        PendingWebPartData pendingWebPartData = null;
        BeReadWebPartData beReadWebPartData = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public TaskService()
        {
            this.pendingWebPartData = new PendingWebPartData();
            this.beReadWebPartData = new BeReadWebPartData();
        }
        #endregion

        #region 待办。
        /// <summary>
        /// 待办动态文本数据。
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "待办动态文本数据。")]
        public string PendingDynamicTextData(string employeeID)
        {
            return this.pendingWebPartData.DynamicTextData(employeeID);
        }
        /// <summary>
        /// 待办WebPart组件数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        [WebMethod(Description = "待办WebPart组件数据。")]
        public WebPartDataCollection PendingDataSource(string employeeID, string dataType)
        {
            return this.pendingWebPartData.DataSource(employeeID, dataType);
        }
        #endregion

        #region 待阅。
        /// <summary>
        /// 待阅动态文本数据。
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "待阅数据。")]
        public string BeReadDynamicTextData(string employeeID)
        {
            return this.beReadWebPartData.DynamicTextData(employeeID);
        }
        /// <summary>
        /// 待阅WebPart组件数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        [WebMethod(Description = "待阅WebPart组件数据。")]
        public WebPartDataCollection BeReadDataSource(string employeeID, string dataType)
        {
            return this.beReadWebPartData.DataSource(employeeID, dataType);
        }
        #endregion
    }
}
