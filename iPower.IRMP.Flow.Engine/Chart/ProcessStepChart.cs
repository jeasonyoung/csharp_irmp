//================================================================================
//  FileName: ProcessChart.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/20
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
    /// 流程步骤图形。
    /// </summary>
    internal abstract class ProcessStepChart : IDisposable
    {
        #region 成员变量，构造函数。
        Color backgroundColor = Color.White, foregroundColor = Color.Blue, borderColor = Color.Blue;
        float borderWidth = 2f;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ProcessStepChart()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="x">起点X坐标。</param>
        /// <param name="y">起点Y坐标。</param>
        public ProcessStepChart(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="point">起点坐标。</param>
        public ProcessStepChart(Point point)
        {
            if (point != null)
            {
                this.X = point.X;
                this.Y = point.Y;
            }
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取或设置步骤ID。
        /// </summary>
        public virtual string StepID { get; set; }
        /// <summary>
        /// 获取或设置步骤名称。
        /// </summary>
        public virtual string StepName { get; set; }
        /// <summary>
        /// 获取或设置前景色。
        /// </summary>
        public virtual Color ForegroundColor
        {
            get { return this.foregroundColor; }
            set { this.foregroundColor = value; }
        }
        /// <summary>
        /// 获取或设置背景色。
        /// </summary>
        public virtual Color BackgroundColor
        {
            get { return this.backgroundColor; }
            set { this.backgroundColor = value; }
        }
        /// <summary>
        /// 获取或设置边框颜色。
        /// </summary>
        public virtual Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }
        /// <summary>
        /// 获取边框宽度。
        /// </summary>
        public virtual float BorderWidth
        {
            get { return this.borderWidth; }
            set
            {
                if (value > 0)
                    this.borderWidth = value;
            }
        }
        /// <summary>
        /// 获取或设置起点X坐标。
        /// </summary>
        public virtual float X { get; set; }
        /// <summary>
        /// 获取或设置起点Y坐标。
        /// </summary>
        public virtual float Y { get; set; }
        /// <summary>
        /// 获取或设置宽度。
        /// </summary>
        public virtual float Width
        {
            get { return 95f; }
        }
        /// <summary>
        /// 获取或设置高度。
        /// </summary>
        public virtual float Height
        {
            get { return 55f; }
        }
        /// <summary>
        ///  获取中心点坐标。
        /// </summary>
        public virtual Point CenterPoint
        {
            get
            {
                return new Point((int)(this.X + this.Width / 2), (int)(this.Y + this.Height / 2));
            }
        }
        /// <summary>
        /// 获取顶部连接点坐标。
        /// </summary>
        public virtual Point TopPoint
        {
            get
            {
                return new Point((int)(this.X + this.Width / 2), (int)this.Y);
            }
        }
        /// <summary>
        /// 获取左边连接坐标。
        /// </summary>
        public virtual Point LeftPoint
        {
            get
            {
                return new Point((int)this.X, (int)(this.Y + (this.Height / 2)));
            }
        }
        /// <summary>
        /// 获取右边连接坐标。
        /// </summary>
        public virtual Point RightPoint
        {
            get
            {
                return new Point((int)(this.X + this.Width), (int)(this.Y + this.Height / 2));
            }
        }
        /// <summary>
        /// 获取底部连接坐标。
        /// </summary>
        public virtual Point BottomPoint
        {
            get
            {
                return new Point((int)(this.X + this.Width / 2), (int)(this.Y + this.Height));
            }
        }
        
        /// <summary>
        /// 获取内容字体文本布局。
        /// </summary>
        protected virtual StringFormat ContentFontStringFormat
        {
            get
            {
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;
                return drawFormat;
            }
        }
        #endregion
        #region 绘制图形。
        /// <summary>
        /// 绘制图形。
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void Draw(Graphics graphics);
        /// <summary>
        /// 绘制步骤名称。
        /// </summary>
        /// <param name="graphics"></param>
        protected virtual void DrawStepName(Graphics graphics)
        {
            if (!string.IsNullOrEmpty(this.StepName) && graphics != null)
            {
                float size = 10f;
                int len = this.StepName.Length;
                float s = (int)(this.Width / len);
                if (s < size)
                    size = s;
                if (this.GetType() == typeof(BranchStepChart))
                    size -= 2;
                using (Font f = new Font("Arial", size))
                {
                    using (SolidBrush brush = new SolidBrush(this.ForegroundColor))
                    {
                        using (StringFormat drawFormat = this.ContentFontStringFormat)
                        {
                            graphics.DrawString(this.StepName, f, brush, new PointF(this.X + this.Width / 2, this.Y + this.Height / 2), drawFormat);
                        }
                    }
                }
            }
        }
        #endregion

        #region IDisposable 成员
        /// <summary>
        /// 释放资源。
        /// </summary>
        public virtual void Dispose()
        {

        }

        #endregion
    }
}
