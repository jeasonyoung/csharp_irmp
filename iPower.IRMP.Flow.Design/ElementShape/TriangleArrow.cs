//================================================================================
//  FileName: TriangleArrow.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/26
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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace iPower.IRMP.Flow.Design.ElementShape
{
    /// <summary>
    /// 三角形箭头。
    /// </summary>
    public class TriangleArrow : Canvas
    {
        #region 成员变量，构造函数。
        int arrowLength = 10, arrowAngle = 20;
        Polygon polygonArrow;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public TriangleArrow()
        {
            this.polygonArrow = new Polygon();
            this.Children.Add(this.polygonArrow);
            this.SetAngleByPoint(new Point(0, 0), new Point(15, 0));
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取或设置箭头的长度。
        /// </summary>
        public int ArrowLenght
        {
            get { return this.arrowLength; }
            set { this.arrowLength = value; }
        }
        /// <summary>
        /// 获取或设置箭头与直线的夹角。
        /// </summary>
        public int ArrowAngle
        {
            get { return this.arrowAngle; }
            set { this.arrowAngle = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ZIndex
        {
            get
            {
                return (int)this.GetValue(Canvas.ZIndexProperty);
            }
            set
            {
                this.SetValue(Canvas.ZIndexProperty, value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public new double Opacity
        {
            get { return this.polygonArrow.Opacity; }
            set { this.polygonArrow.Opacity = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Brush Fill
        {
            get { return this.polygonArrow.Fill; }
            set { this.polygonArrow.Fill = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Brush Stroke
        {
            get { return this.polygonArrow.Stroke; }
            set { this.polygonArrow.Stroke = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double StrokeThickness
        {
            get { return this.polygonArrow.StrokeThickness; }
            set { this.polygonArrow.StrokeThickness = value; }
        }
        #endregion

        #region 辅助函数。
        void SetAngleByDegree(double degreeLeft, double degreeRight)
        {
            this.polygonArrow.Points.Clear();
            this.polygonArrow.Points.Add(new Point(0, 0));

            double angleSi = Math.PI * degreeLeft / 180.0;
            double x = Math.Sin(Math.PI * degreeLeft / 180.0);
            double y = Math.Sin(Math.PI * (90 - degreeLeft) / 180.0);

            x = -this.ArrowLenght * x;
            y = -this.ArrowLenght * y;

            this.polygonArrow.Points.Add(new Point(x, y));

            x = Math.Sin(Math.PI * degreeRight / 180.0);
            y = Math.Sin(Math.PI * (90 - degreeRight) / 180.0);

            x = this.ArrowLenght * x;
            y = -this.ArrowLenght * y;

            this.polygonArrow.Points.Add(new Point(x, y));
        }
        #endregion

        /// <summary>
        /// 根据直线的起始点和结束点的坐标设置箭头的旋转角度。
        /// </summary>
        /// <param name="beginPoint"></param>
        /// <param name="endPoint"></param>
        public void SetAngleByPoint(Point beginPoint, Point endPoint)
        {
            if (beginPoint != null && endPoint != null)
            {
                double x = endPoint.X - beginPoint.X;
                double y = endPoint.Y - beginPoint.Y;
                double angle = 0;

                if (y == 0)
                {
                    if (x > 0)
                        angle = -90;
                    else
                        angle = 90;
                }
                else
                    angle = Math.Atan(x / y) * 180 / Math.PI;

                if (endPoint.Y <= beginPoint.Y)
                    this.SetAngleByDegree(this.ArrowAngle + angle - 180, this.ArrowAngle - angle - 180);
                else
                    this.SetAngleByDegree(this.ArrowLenght + angle, this.ArrowLenght - angle);
            }
        }

    }
}
