//================================================================================
//  FileName: BeReadWebPartData.cs
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
using System.Text;

using iPower.Platform.WebPart;
namespace iPower.IRMP.Flow.Poxy
{
    /// <summary>
    ///  待阅WebPart数据源。
    /// </summary>
    public  class BeReadWebPartData : IWebPartData
    {
        #region 成员变量，构造函数。
        TaskServicePoxy taskServicePoxy = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public BeReadWebPartData()
        {
            this.taskServicePoxy = new TaskServicePoxy();
            this.taskServicePoxy.Url = ModuleConfiguration.ModuleConfig.TaskServiceURL;
        }
        #endregion

        #region IWebPartData 成员
        /// <summary>
        /// 获取WebPart组件数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public WebPartDataCollection DataSource(string employeeID, string dataType)
        {
            WebPartDataCollection collection = new WebPartDataCollection();
            Poxy.WebPartData[] wpds = this.taskServicePoxy.BeReadDataSource(employeeID, dataType);
            if (wpds != null && wpds.Length > 0)
            {
                foreach (Poxy.WebPartData wpd in wpds)
                {
                    iPower.Platform.WebPart.WebPartData data = new iPower.Platform.WebPart.WebPartData();
                    data.Title = wpd.Title;
                    data.Url = wpd.Url;
                    collection.Add(data);
                }
            }
            return collection;
        }
        /// <summary>
        /// 获取动态文本数据源。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string DynamicTextData(string employeeID)
        {
            return this.taskServicePoxy.BeReadDynamicTextData(employeeID);
        }

        #endregion
    }
}
