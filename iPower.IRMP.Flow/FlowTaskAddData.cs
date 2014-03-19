//================================================================================
//  FileName: FlowTaskAddData.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/1/17
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

namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 流程任务附加数据类。
    /// </summary>
    [Serializable]
    public class FlowTaskAddData
    {
        /// <summary>
        /// 获取或设置当前步骤标识。
        /// </summary>
        public string StepSign { get; set; }
        /// <summary>
        /// 获取或设置下一步骤标识。
        /// </summary>
        public string ToStepSign { get; set; }
        /// <summary>
        /// 获取或设置推进该流程的相关人员（用户ID,用户名称），为空表示流程定义中的所有相关人员。
        /// </summary>
        public string[][] toDoEmployees { get; set; }
        /// <summary>
        /// 获取或设置流程待阅的相关人员（用户ID,用户名称），为空表示无待阅。
        /// </summary>
        public string[][] toViewEmployees { get; set; }
    }
}
