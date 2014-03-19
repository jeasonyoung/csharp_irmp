//================================================================================
//  FileName:FlowProcessChartDesignPresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-23 14:29:59
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;

using iPower.IRMP.Flow;
namespace iPower.IRMP.Flow.Engine.Service
{
    /// <summary>
    /// 流程图设计图接口。
    /// </summary>
    public interface IFlowProcessChartDesignView : IModuleView
    {
        /// <summary>
        /// 获取流程ID。
        /// </summary>
        GUIDEx ProcessID { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
    }
    /// <summary>
    /// 流程图行为类。
    /// </summary>
    public class FlowProcessChartDesignPresenter : ModulePresenter<IFlowProcessChartDesignView>
    {
        #region 成员变量，构造函数。
        //Process process = null;
        //FlowProcessEntity flowProcessEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public FlowProcessChartDesignPresenter(IFlowProcessChartDesignView view)
            : base(view)
        {
            this.View.SecurityID = ModuleConstants.Process_ModuleID;
            //this.flowProcessEntity = new FlowProcessEntity();
        }
        #endregion

        #region 属性。
        ///// <summary>
        ///// 获取当前流程名称。
        ///// </summary>
        //public string GetProcessName
        //{
        //    get
        //    {
        //        if (this.View.ProcessID.IsValid)
        //        {
        //           return this.flowProcessEntity.FindProcessName(this.View.ProcessID);
        //        }
        //        return string.Empty;
        //    }
        //}
        #endregion

        #region 重载
        //protected override void PreViewLoadData()
        //{
        //    base.PreViewLoadData();
        //    this.View.CurrentModuleTitle += " - " + this.GetProcessName;
        //}
        #endregion

        #region 数据处理。
        //public XmlDocument CreateChartData()
        //{
        //    XmlDocument doc = null;
        //    if (this.View != null && this.View.ProcessID.IsValid)
        //    {
        //        this.process = ModuleUtils.CreateProcess(this.View.ProcessID);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            process.Serializer(ms);
        //            ms.Position = 0;
        //            doc = new XmlDocument();
        //            doc.Load(ms);

        //            ms.Close();
        //        }
        //    }
        //    return doc;
        //}
        #endregion
    }
}
