//================================================================================
//  FileName: ShapeLine.xaml.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using iPower.IRMP.Flow.Design.Designer;
using iPower.IRMP.Flow.Design.Elements;
namespace iPower.IRMP.Flow.Design.ElementShape
{
    /// <summary>
    /// 连线元素。
    /// </summary>
    public partial class ShapeLine : UserControl,IWFShape
    {
        #region 成员变量，构造函数。
        double canvasLeft = 0, canvasWidth = 0, canvasTop = 0, canvasHeight = 0;
        UIElement uiContainter = null, beginWFElement = null, endWFElement = null;
        WFHotspotType beginWFElementHotspot, endWFElementHotspot;
        LineMoveType moveType = LineMoveType.Begin;
        WFElementState wfState = WFElementState.UnFocus;
        IContainer container = null;
        WFLine wfLine;
        Point previousPoint, mousePosition;
        bool trackingPointMouseMove = false, pointHadActualMove = false, linkWFElement = false;
        /// <summary>
        ///  构造函数。
        /// </summary>
        public ShapeLine()
        {
            InitializeComponent();

            PointCollection points = new PointCollection();
            points.Add(new Point(5, 5));
            points.Add(new Point(35, 5));
            points.Add(new Point(35, 45));
            points.Add(new Point(65, 45));
            this.plShadow.Points = points;
            this.ShowMe(points);
            this.SetFocus();
        }
        #endregion

        #region 函数。
        /// <summary>
        /// 显示控件。
        /// </summary>
        /// <param name="points"></param>
        public void ShowMe(PointCollection points)
        {
            Point pBegin = points[0], pCenterB = points[1], pCenterC = points[2], pEnd = points[3];
            bool HaveAdjustment = false;
            //通过开始点对B点和C点进行重合调整。
            if (pBegin.Y == pCenterB.Y && Math.Abs(pBegin.X - pCenterB.X) <= ellipseBegin.Height / 2)
            {
                pCenterB = pBegin;
                pCenterC.X = pCenterB.X;
                HaveAdjustment = true;
            }
            else if (pBegin.X == pCenterB.X && Math.Abs(pBegin.Y - pCenterB.Y) <= ellipseBegin.Height / 2)
            {
                pCenterB = pBegin;
                pCenterC.Y = pCenterB.Y;
                HaveAdjustment = true;
            }
            //通过结束点对C点和B点进行重合调整。
            if (pCenterC.Y == pEnd.Y && Math.Abs(pCenterC.X - pEnd.X) <= ellipseEnd.Height / 2)
            {
                pCenterC = pEnd;
                pCenterB.X = pCenterC.X;
                HaveAdjustment = true;
            }
            else if (pCenterC.X == pEnd.X && Math.Abs(pCenterC.Y - pEnd.Y) <= ellipseEnd.Height / 2)
            {
                pCenterC = pEnd;
                pCenterB.Y = pCenterC.Y;
                HaveAdjustment = true;
            }
            //如果对B点和C点进行了重合调整，则对plShadow进行调整。
            if (HaveAdjustment == true)
            {
                this.plShadow.Points.Clear();
                this.plShadow.Points.Add(pBegin);
                this.plShadow.Points.Add(pCenterB);
                this.plShadow.Points.Add(pCenterC);
                this.plShadow.Points.Add(pEnd);
            }
            //设置起始圆点的坐标。
            Canvas.SetLeft(ellipseBegin, pBegin.X - ellipseBegin.Width / 2);
            Canvas.SetTop(ellipseBegin, pBegin.Y - ellipseBegin.Height / 2);
            //设置左边线的坐标。
            this.lineLeft.X1 = pBegin.X;
            this.lineLeft.Y1 = pBegin.Y;
            this.lineLeft.X2 = pCenterB.X;
            this.lineLeft.Y2 = pCenterB.Y;
            //设置中间线的坐标。
            this.lineCenter.X1 = pCenterB.X;
            this.lineCenter.Y1 = pCenterB.Y;
            this.lineCenter.X2 = pCenterC.X;
            this.lineCenter.Y2 = pCenterC.Y;
            //设置中间菱形点的坐标。
            Canvas.SetLeft(this.rectangleCenter, (pCenterB.X < pCenterC.X ? pCenterB.X : pCenterC.X) + Math.Abs(pCenterC.X - pCenterB.X) / 2);
            Canvas.SetTop(this.rectangleCenter, (pCenterB.Y < pCenterC.Y ? pCenterB.Y : pCenterC.Y) + Math.Abs(pCenterC.Y - pCenterB.Y) / 2);
            //调整高度要减去菱形的半径。
            Canvas.SetTop(this.rectangleCenter, Canvas.GetTop(this.rectangleCenter) - this.rectangleCenter.Height * Math.Sin(45) + 1);
            //设置右边线的坐标。
            this.lineRight.X1 = pCenterC.X;
            this.lineRight.Y1 = pCenterC.Y;
            this.lineRight.X2 = pEnd.X;
            this.lineRight.Y2 = pEnd.Y;
            //设置箭头的坐标。
            Canvas.SetLeft(this.arrowhead, pEnd.X);
            Canvas.SetTop(this.arrowhead, pEnd.Y);
            if (pEnd == pCenterC)
                this.arrowhead.SetAngleByPoint(pCenterB, pCenterC);
            else
                this.arrowhead.SetAngleByPoint(pCenterC, pEnd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <param name="canvasWidth"></param>
        /// <param name="canvasHeight"></param>
        public void SetRange(double canvasLeft, double canvasTop, double canvasWidth, double canvasHeight)
        {
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
        }
        /// <summary>
        /// 
        /// </summary>
        public UIElement UIContainer
        {
            get { return this.uiContainter; }
            set { this.uiContainter = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public UIElement BeginWFElement
        {
            get { return this.beginWFElement; }
            set { this.beginWFElement = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public WFHotspotType BeginWFElementHotspot
        {
            get { return this.beginWFElementHotspot; }
            set { this.beginWFElementHotspot = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public UIElement EndWFElement
        {
            get { return this.endWFElement; }
            set { this.endWFElement = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public WFHotspotType EndWFElementHotspot
        {
            get { return this.endWFElementHotspot; }
            set { this.endWFElementHotspot = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public IContainer Container
        {
            get { return this.container; }
            set { this.container = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public WFLine WFLine
        {
            get { return this.wfLine; }
            set { this.wfLine = value; }
        }
        /// <summary>
        /// 设置热点的颜色。
        /// </summary>
        /// <param name="color"></param>
        /// <param name="opacity"></param>
        void SetHotspotStyle(Color color, double opacity)
        {
            this.ellipseBegin.Fill = new SolidColorBrush(color);
            this.ellipseBegin.Opacity = opacity;

            this.rectangleCenter.Opacity = opacity;

            this.ellipseEnd.Fill = new SolidColorBrush(color);
            this.ellipseEnd.Opacity = opacity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        public void SetPreviousPoint(Point mousePoint)
        {
            this.previousPoint = mousePoint;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <param name="wfPointType"></param>
        public void ShowShadow(Point mousePoint, WFPointType wfPointType)
        {
            if (wfPointType == WFPointType.Excursion)
            {
                this.plShadow.Points = this.GetPloyline(this.plShadow.Points, this.previousPoint, mousePoint);
                this.previousPoint = mousePoint;
            }
            else
                this.plShadow.Points = this.GetMe();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBegin"></param>
        /// <param name="pEnd"></param>
        public void ShowShadow(Point pBegin, Point pEnd)
        {
            this.plShadow.Points = this.GetPloyline(pBegin, pEnd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void ShowShadow(PointCollection points)
        {
            this.plShadow.Points = points;
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowMe()
        {
            this.ShowMe(this.plShadow.Points);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBegin"></param>
        /// <param name="pEnd"></param>
        public void ShowMe(Point pBegin, Point pEnd)
        {
            this.plShadow.Points = this.GetPloyline(pBegin, pEnd);
            this.ShowMe(this.plShadow.Points);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsInside(Point point, double x, double y)
        {
            Point pBegin = this.plShadow.Points[0], pEnd = this.plShadow.Points[3];

            if (this.GetLineShape() == LineShape.Z)
            {
                if(((pEnd.X >= point.X && pBegin.X <= point.X +x) ||(pBegin.X >= point.X && pEnd.X <= point.X + x))
                    && ((pEnd.Y>=point.Y && pBegin.Y <point.Y+y) || (pBegin.Y > point.Y && pEnd.Y < point.Y + y)))
                    return true;
            }
            else
            {
                if(((pEnd.Y >= point.Y && pBegin.Y<= point.Y+y) ||(pBegin.Y >= point.Y && pEnd.Y <= point.Y+y))
                    &&((pEnd.X > point.X && pBegin.X <point.X +x) || (pBegin.X > point.X && pEnd.X < point.X + x)))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PointCollection GetMe()
        {
            PointCollection points = new PointCollection();
            Point pStart = new Point(), pCenterB = new Point(), pCenterC = new Point(), pEnd = new Point();
            pStart.X = Canvas.GetLeft(this.ellipseBegin) + this.ellipseBegin.Width / 2;
            pStart.Y = Canvas.GetTop(this.ellipseBegin) + this.ellipseBegin.Height / 2;

            if (this.IsStraightLine() == false)
            {
                pCenterB.X = this.lineCenter.X1;
                pCenterB.Y = this.lineCenter.Y1;

                pCenterC.X = this.lineCenter.X2;
                pCenterC.Y = this.lineCenter.Y2;
            }
            else
            {
                pCenterB.X = this.lineLeft.X2;
                pCenterB.Y = this.lineLeft.Y2;

                pCenterC.X = this.lineRight.X2;
                pCenterC.Y = this.lineRight.Y2;
            }

            pEnd.X = Canvas.GetLeft(this.ellipseEnd) + this.ellipseEnd.Width / 2;
            pEnd.Y = Canvas.GetTop(this.ellipseEnd) + this.ellipseEnd.Height / 2;

            points.Add(pStart);
            points.Add(pCenterB);
            points.Add(pCenterC);
            points.Add(pEnd);

            return points;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBegin"></param>
        /// <param name="pEnd"></param>
        /// <returns></returns>
        public PointCollection GetPloyline(Point pBegin, Point pEnd)
        {
            PointCollection points = new PointCollection();
            Point pCenterB = new Point(), pCenterC = new Point();
            if (this.GetPloylineShape(pBegin, pEnd) == LineShape.Z)
            {
                //Z型线。
                pCenterB.X = (pBegin.X < pEnd.X ? pBegin.X : pEnd.X) + Math.Abs(pEnd.X - pBegin.X) / 2;
                pCenterB.Y = pBegin.Y;

                pCenterC.X = (pBegin.X < pEnd.X ? pBegin.X : pEnd.X) + Math.Abs(pEnd.X - pBegin.X) / 2;
                pCenterC.Y = pEnd.Y;
            }
            else
            {
                //N型线。
                pCenterB.X = pBegin.X;
                pCenterB.Y = (pBegin.Y < pEnd.Y ? pBegin.Y : pEnd.Y) + Math.Abs(pEnd.Y - pBegin.Y) / 2;

                pCenterC.X = pEnd.X;
                pCenterC.Y = (pBegin.Y < pEnd.Y ? pBegin.Y : pEnd.Y) + Math.Abs(pEnd.Y - pBegin.Y) / 2;
            }

            points.Add(pBegin);
            points.Add(pCenterB);
            points.Add(pCenterC);
            points.Add(pEnd);

            return points;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCenter"></param>
        /// <returns></returns>
        PointCollection GetPloyline(Point pCenter)
        {
            PointCollection points = new PointCollection();
            Point pStart = new Point(Canvas.GetLeft(this.ellipseBegin) + this.ellipseBegin.Width / 2,
                                     Canvas.GetTop(this.ellipseBegin) + this.ellipseBegin.Height / 2), pCenterB, pCenterC, pEnd;
            if (this.GetLineShape() == LineShape.Z)
            {
                pCenterB = new Point(pCenter.X, Canvas.GetTop(this.ellipseBegin) + this.ellipseBegin.Height / 2);
                pCenterC = new Point(pCenter.X, Canvas.GetTop(this.ellipseEnd) + this.ellipseEnd.Height / 2);
            }
            else
            {
                pCenterB = new Point(Canvas.GetLeft(this.ellipseBegin) + this.ellipseBegin.Width / 2, pCenter.Y);
                pCenterC = new Point(Canvas.GetLeft(this.ellipseEnd) + this.ellipseEnd.Width / 2, pCenter.Y);
            }
            pEnd = new Point(Canvas.GetLeft(this.ellipseEnd) + this.ellipseEnd.Width / 2,
                            Canvas.GetTop(this.ellipseEnd) + this.ellipseEnd.Height / 2);

            points.Add(pStart);
            points.Add(pCenterB);
            points.Add(pCenterC);
            points.Add(pEnd);

            return points;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="pOld"></param>
        /// <param name="pNew"></param>
        /// <returns></returns>
        public PointCollection GetPloyline(PointCollection points, Point pOld, Point pNew)
        {
            double x = pNew.X - pOld.X;
            double y = pNew.Y - pOld.Y;

            PointCollection newPoints = new PointCollection();
            for (int i = 0; i < points.Count; i++)
            {
                newPoints.Add(new Point(points[i].X + x, points[i].Y + y));
            }
            return newPoints;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool CheckPoints(PointCollection points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < this.canvasLeft || points[i].X > this.canvasWidth)
                    return false;
                if (points[i].Y < this.canvasTop || points[i].Y > this.canvasHeight)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 判断型线，用于确定ployline的线型。
        /// </summary>
        /// <param name="pBegin"></param>
        /// <param name="pEnd"></param>
        /// <returns></returns>
        LineShape GetPloylineShape(Point pBegin, Point pEnd)
        {
            if (Math.Abs(pEnd.X - pBegin.X) > Math.Abs(pEnd.Y - pBegin.Y))
                return LineShape.Z;
            return LineShape.N;
        }
        /// <summary>
        /// 获取线型。
        /// </summary>
        /// <returns></returns>
        public LineShape GetLineShape()
        {
            if (this.lineCenter.X1 == this.lineCenter.X2)
                return LineShape.Z;
            return LineShape.N;
        }
        /// <summary>
        /// 判断是否为直线（水平直线，垂直直线）。
        /// </summary>
        /// <returns></returns>
        public bool IsStraightLine()
        {
            return (this.lineLeft.X1 == this.lineRight.X2) || (this.lineLeft.Y1 == this.lineRight.Y2);

        }
        #endregion

        #region 事件处理。
        private void Hotspot_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element != null)
            {
                if (element.Name == this.rectangleCenter.Name)
                {
                    if (this.GetLineShape() == LineShape.Z)
                        element.Cursor = Cursors.SizeWE;
                    else
                        element.Cursor = Cursors.SizeNS;
                }
                else
                    element.Cursor = Cursors.Hand;

            }
        }

        private void Hotspot_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.trackingPointMouseMove != true)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null)
                    element.Cursor = Cursors.Arrow;
            }
        }
        
        private void Hotspot_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.trackingPointMouseMove)
            {
                Point pStart, pEnd;
                switch (this.moveType)
                {
                    case LineMoveType.Begin:
                        pStart = new Point(e.GetPosition(this.UIContainer).X, e.GetPosition(this.UIContainer).Y);
                        pEnd = new Point(Canvas.GetLeft(this.ellipseEnd) + this.ellipseEnd.Width / 2,
                                        Canvas.GetTop(this.ellipseEnd) + this.ellipseEnd.Height / 2);
                        this.plShadow.Points = this.GetPloyline(pStart, pEnd);
                        break;
                    case LineMoveType.Center:
                        Point pCenter = e.GetPosition(this.UIContainer);
                        this.plShadow.Points = this.GetPloyline(pCenter);
                        break;
                    case LineMoveType.End:
                        pStart = new Point(Canvas.GetLeft(this.ellipseBegin) + this.ellipseBegin.Width / 2,
                                           Canvas.GetTop(this.ellipseBegin) + this.ellipseBegin.Height / 2);
                        pEnd = new Point(e.GetPosition(this.UIContainer).X, e.GetPosition(this.UIContainer).Y);
                        this.plShadow.Points = this.GetPloyline(pStart, pEnd);
                        break;
                    case LineMoveType.Line:
                        if (this.mousePosition != e.GetPosition(this.UIContainer))
                        {
                            this.plShadow.Points = this.GetPloyline(this.plShadow.Points, this.mousePosition, e.GetPosition(this.UIContainer));
                            this.mousePosition = e.GetPosition(this.UIContainer);
                        }
                        break;
                }
                if (this.CheckPoints(this.plShadow.Points) == true)
                    this.pointHadActualMove = true;
                else
                    this.pointHadActualMove = false;

                if (this.pointHadActualMove && this.moveType != LineMoveType.Center && this.moveType != LineMoveType.None)
                {
                    this.linkWFElement = true;
                    if (this.BeginWFElement != null)
                    {
                        ((IWFElement)this.BeginWFElement).BeginWFLinesRemove(this.WFLine);
                        ((IWFElement)this.BeginWFElement).SetUnFocus();
                    }
                    this.BeginWFElement = this.Container.GetWFElementByPoint(this.plShadow.Points[0]);
                    if (this.BeginWFElement != null)
                    {
                        if (((IWFElement)this.BeginWFElement).BeginWFLinesAdd(this.WFLine) == false)
                            this.BeginWFElement = null;
                        else
                            ((IWFElement)this.BeginWFElement).SetSelected();
                    }

                    if (this.EndWFElement != null)
                    {
                        ((IWFElement)this.EndWFElement).EndWFLinesRemove(this.WFLine);
                        ((IWFElement)this.EndWFElement).SetUnFocus();
                    }
                    this.EndWFElement = this.Container.GetWFElementByPoint(this.plShadow.Points[3]);
                    if (this.EndWFElement != null)
                    {
                        if (((IWFElement)this.EndWFElement).EndWFLinesAdd(this.WFLine) == false)
                            this.EndWFElement = null;
                        else
                            ((IWFElement)this.EndWFElement).SetSelected();
                    }
                }
                else
                    this.linkWFElement = false;
            }
        }

        private void Hotspot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Container.IsMultiSelected(this.WFLine))
                return;
            this.pointHadActualMove = this.trackingPointMouseMove = false;
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element != null)
            {
                this.moveType = LineMoveType.None;
                if (element.Name == this.ellipseBegin.Name)
                {
                    this.moveType = LineMoveType.Begin;
                    element.Cursor = Cursors.Hand;
                }
                else if (element.Name == this.rectangleCenter.Name)
                {
                    this.moveType = LineMoveType.Center;
                    if (this.GetLineShape() == LineShape.Z)
                        element.Cursor = Cursors.SizeWE;
                }
                else if (element.Name == this.ellipseEnd.Name)
                {
                    this.moveType = LineMoveType.End;
                    element.Cursor = Cursors.Hand;
                }
                else if (element.Name == this.lineLeft.Name ||
                    element.Name == this.lineCenter.Name ||
                    element.Name == this.lineRight.Name ||
                    element.Name == this.arrowhead.Name)
                {
                    this.moveType = LineMoveType.Line;
                    this.mousePosition = e.GetPosition(this.UIContainer);
                    element.Cursor = Cursors.Hand;
                }

                if (this.moveType != LineMoveType.None)
                {
                    this.trackingPointMouseMove = true;
                    element.CaptureMouse();
                }
            }
        }

        private void Hotspot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.trackingPointMouseMove)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null)
                {
                    element.Cursor = Cursors.Arrow;
                    element.ReleaseMouseCapture();
                }
                if (this.pointHadActualMove == true)
                {
                    if (this.linkWFElement == true && (this.BeginWFElement != null || this.EndWFElement != null))
                    {
                        Point pBegin, pEnd;
                        if (this.BeginWFElement != null)
                        {
                            this.BeginWFElementHotspot = ((IWFElement)this.BeginWFElement).GetNearHotspot(this.plShadow.Points[0]);
                            pBegin = ((IWFElement)this.BeginWFElement).GetHotspot(this.BeginWFElementHotspot);
                            ((IWFElement)this.BeginWFElement).SetUnFocus();
                        }
                        else
                            pBegin = this.plShadow.Points[0];
                        if (this.EndWFElement != null)
                        {
                            this.EndWFElementHotspot = ((IWFElement)this.EndWFElement).GetNearHotspot(this.plShadow.Points[3]);
                            pEnd = ((IWFElement)this.EndWFElement).GetHotspot(this.EndWFElementHotspot);
                            ((IWFElement)this.EndWFElement).SetUnFocus();
                        }
                        else
                            pEnd = this.plShadow.Points[3];
                        this.plShadow.Points = this.GetPloyline(pBegin, pEnd);
                    }
                    this.ShowMe(this.plShadow.Points);
                    e.Handled = true;
                    this.pointHadActualMove = false;
                }
                else
                    this.plShadow.Points = this.GetMe();
                this.trackingPointMouseMove = false;
            }
        }

        private void arrowhead_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Container.IsMultiSelected(this.WFLine))
                return;
            this.pointHadActualMove = this.trackingPointMouseMove = false;
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element != null)
            {
                this.moveType = LineMoveType.None;
                this.mousePosition = e.GetPosition(this.UIContainer);
                element.Cursor = Cursors.Hand;

                if (this.moveType != LineMoveType.None)
                {
                    this.trackingPointMouseMove = true;
                    element.CaptureMouse();
                }
            }
        }
        
        private void Line_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element != null)
                element.Cursor = Cursors.Hand;
        }

        private void Line_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.trackingPointMouseMove != true)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null)
                    element.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region IWFShape 成员
        /// <summary>
        /// 
        /// </summary>
        public WFElementState WFState
        {
            get { return this.wfState; }
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetFocus()
        {
            if (this.WFState != WFElementState.Focus)
            {
                this.SetHotspotStyle(Colors.Yellow, 1.0);
                this.ellipseBegin.Visibility = Visibility.Visible;
                this.rectangleCenter.Visibility = Visibility.Visible;
                this.ellipseEnd.Visibility = Visibility.Visible;
                //直线状态不显示中间点。
                if (this.IsStraightLine())
                    this.rectangleCenter.Visibility = Visibility.Collapsed;
                this.wfState = WFElementState.Focus;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetUnFocus()
        {
            if (this.WFState != WFElementState.UnFocus)
            {
                this.SetHotspotStyle(Colors.Red, 0.8);

                this.ellipseBegin.Visibility = Visibility.Collapsed;
                this.rectangleCenter.Visibility = Visibility.Collapsed;
                this.ellipseEnd.Visibility = Visibility.Collapsed;

                this.wfState = WFElementState.UnFocus;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetSelected()
        {
            if (this.WFState != WFElementState.Selected)
            {
                this.SetHotspotStyle(Colors.Red, 1.0);

                this.ellipseBegin.Visibility = Visibility.Visible;
                this.rectangleCenter.Visibility = Visibility.Visible;
                this.ellipseEnd.Visibility = Visibility.Visible;

                this.wfState = WFElementState.Selected;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string title)
        {
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return "连线";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="opacity"></param>
        public void Fill(Color color, double opacity)
        {
            this.lineLeft.Stroke = new SolidColorBrush(color);
            this.lineLeft.Opacity = opacity;

            this.lineCenter.Stroke = new SolidColorBrush(color);
            this.lineCenter.Opacity = opacity;

            this.lineRight.Stroke = new SolidColorBrush(color);
            this.lineRight.Opacity = opacity;

            this.arrowhead.Fill = new SolidColorBrush(color);
            this.arrowhead.Opacity = 1.0;
        }

        #endregion
    }
    /// <summary>
    /// 折线的线型（水平方向为Z型；垂直方向为N型）
    /// </summary>
    public enum LineShape
    {
        Z=0,
        N
    }
    /// <summary>
    /// 
    /// </summary>
    public enum LineMoveType
    {
        None = 0,
        Begin,
        Center,
        End,
        Line
    }
}
