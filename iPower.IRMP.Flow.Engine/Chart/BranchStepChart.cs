//================================================================================
//  FileName: BranchStepChart.cs
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
    /// 分支步骤图形。
    /// </summary>
    internal class BranchStepChart : ProcessStepChart
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public BranchStepChart()
            : this(0f, 0f)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="x">起点x坐标。</param>
        /// <param name="y">起点y坐标。</param>
        public BranchStepChart(float x, float y)
            : base(x, y)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="point">起点坐标。</param>
        public BranchStepChart(Point point)
            : base(point)
        {
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 绘制图形。
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            if (graphics != null)
            {
                Point[] points = new Point[] { this.LeftPoint, this.TopPoint, this.RightPoint, this.BottomPoint };
                using (Pen p = new Pen(this.BorderColor, this.BorderWidth))
                {
                    graphics.DrawPolygon(p, points);
                }

                using (SolidBrush brush = new SolidBrush(this.BackgroundColor))
                {
                    graphics.FillPolygon(brush, points);
                }

                this.DrawStepName(graphics);
            }
        }
        #endregion
    }
}
