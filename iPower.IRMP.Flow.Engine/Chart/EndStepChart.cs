//================================================================================
//  FileName: EndStepChart.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/21
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
using System.Drawing;

namespace iPower.IRMP.Flow.Engine.Chart
{
    /// <summary>
    /// 结束步骤图形。
    /// </summary>
    internal class EndStepChart : StartStepChart
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public EndStepChart()
            : this(0f, 0f)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="x">起点x坐标。</param>
        /// <param name="y">起点y坐标。</param>
        public EndStepChart(float x, float y)
            : base(x, y)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="point">起点坐标。</param>
        public EndStepChart(Point point)
            : base(point)
        {
        }
        #endregion
    }
}
