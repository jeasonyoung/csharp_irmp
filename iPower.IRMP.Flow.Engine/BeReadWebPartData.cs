//================================================================================
//  FileName: BeReadWebPartData.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/30
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using iPower.Utility;
using iPower.Platform.WebPart;

using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine
{
    /// <summary>
    /// 待阅WebPart数据源。
    /// </summary>
    [Serializable]
    public class BeReadWebPartData　: IWebPartData
    {
        #region 成员变量，构造函数。
        static Hashtable Cache = Hashtable.Synchronized(new Hashtable());
        FlowInstanceTaskEntity flowInstanceTaskEntity = null;
        FlowStepInstanceEntity flowStepInstanceEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public BeReadWebPartData()
        {
            this.flowInstanceTaskEntity = new FlowInstanceTaskEntity();
            this.flowStepInstanceEntity = new FlowStepInstanceEntity();
        }
        #endregion

        #region IWebPartData 成员
        /// <summary>
        /// 获取动态文本数据源。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string DynamicTextData(string employeeID)
        {
            return "待阅";
        }
        /// <summary>
        /// 获取WebPart组件数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public WebPartDataCollection DataSource(string employeeID, string dataType)
        {
            WebPartDataCollection collection = new WebPartDataCollection();
            if (!string.IsNullOrEmpty(employeeID))
            {
                List<FlowInstanceTask> listTask = this.flowInstanceTaskEntity.GetFlowInstanceTask(employeeID, EnumTaskCategory.BeRead);
                if (listTask != null && listTask.Count > 0)
                {
                    string processInstanceName = string.Empty, stepInstanceName = string.Empty;
                    foreach (FlowInstanceTask fit in listTask)
                    {
                        if (this.flowStepInstanceEntity.GetInstanceStepName(fit.StepInstanceID, out processInstanceName, out stepInstanceName))
                        {
                            WebPartData data = new WebPartData();
                            data.Title = string.Format("[{0}]-{1}", processInstanceName, stepInstanceName);
                            data.Url = fit.URL;
                            collection.Add(data);
                        }
                    }
                }
            }
            return collection;
        }

        #endregion
    }
}