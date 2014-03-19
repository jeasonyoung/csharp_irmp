//================================================================================
//  FileName: NonBranchStepChart.cs
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
    /// 非分支步骤图形。
    /// </summary>
    internal class NonBranchStepChart : ProcessStepChart
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public NonBranchStepChart()
            : this(0f, 0f)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="x">起点x坐标。</param>
        /// <param name="y">起点y坐标。</param>
        public NonBranchStepChart(float x, float y)
            : base(x, y)
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="point">起点坐标。</param>
        public NonBranchStepChart(Point point)
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
                using (Pen p = new Pen(this.BorderColor, this.BorderWidth))
                {
                    graphics.DrawRectangle(p, this.X, this.Y, this.Width, this.Height);
                }

                using (SolidBrush brush = new SolidBrush(this.BackgroundColor))
                {
                    graphics.FillRectangle(brush, new RectangleF(this.X, this.Y, this.Width, this.Height));
                }

                this.DrawStepName(graphics);
            }
        }
        #endregion
    }
}
