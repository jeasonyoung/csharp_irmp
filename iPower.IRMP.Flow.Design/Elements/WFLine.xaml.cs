//================================================================================
//  FileName: WFLine.xaml.cs
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
using System.Text;
using iPower.IRMP.Flow.Design.Designer;
namespace iPower.IRMP.Flow.Design.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WFLine : UserControl,IWFElement
    {
        #region 成员变量，构造函数。
        double canvasLeft = 0, canvasWidth = 0, canvasTop = 0, canvasHeight = 0;
        List<UIElement> beginWFLines, endWFLines;
        string uniqueID;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public WFLine()
        {
            InitializeComponent();

            this.wfShape.UIContainer = this.cnContainer;
            this.wfShape.WFLine = this;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <param name="canvasWidth"></param>
        /// <param name="canvasHeight"></param>
        public WFLine(double canvasLeft, double canvasTop, double canvasWidth, double canvasHeight)
            : this()
        {
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;

            this.wfShape.SetRange(canvasLeft, canvasTop, canvasWidth, canvasHeight);
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 
        /// </summary>
        public UIElement BeginWFElement
        {
            get { return this.wfShape.BeginWFElement; }
            set { this.wfShape.BeginWFElement = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public WFHotspotType BeginWFElementHotspot
        {
            get { return this.wfShape.BeginWFElementHotspot; }
        }
        /// <summary>
        /// 
        /// </summary>
        public UIElement EndWFElement
        {
            get { return this.wfShape.EndWFElement; }
            set { this.wfShape.EndWFElement = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public WFHotspotType EndWFElementHotspot
        {
            get { return this.wfShape.EndWFElementHotspot; }
        }
        #endregion

        #region 事件处理。
        private void wfShape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Container.IsMultiSelected(this) && !this.Container.CtrlKeyIsPress)
                return;
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element != null)
            {
                this.Container.SetWFElementSeleted((UIElement)this);
                e.Handled = true;
            }
        }

        private void wfShape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region IWFElement 成员

        public IContainer Container
        {
            get
            {
                return this.wfShape.Container;
            }
            set
            {
                this.wfShape.Container = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public WFElementState WFState
        {
            get { return this.wfShape.WFState; }
        }
        /// <summary>
        /// 
        /// </summary>
        public WFElementType WFType
        {
            get { return WFElementType.Line; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MaxBeginWFLines
        {
            get { return 0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<UIElement> BeginWFLines
        {
            get
            {
                if (this.beginWFLines == null)
                    this.beginWFLines = new List<UIElement>();
                return this.beginWFLines;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<UIElement> EndWFLines
        {
            get
            {
                if (this.endWFLines == null)
                    this.endWFLines = new List<UIElement>();
                return this.endWFLines;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MaxEndWFLines
        {
            get { return 0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UniqueID
        {
            get
            {
                if (string.IsNullOrEmpty(this.uniqueID))
                    this.uniqueID = Guid.NewGuid().ToString().Replace("-", "");
                return this.uniqueID;
            }
            set
            {
                this.uniqueID = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get
            {
                return this.wfShape.GetTitle();
            }
            set
            {
                this.wfShape.SetTitle(value);
            }
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
        /// <param name="wfElement"></param>
        /// <returns></returns>
        public bool BeginWFLinesAdd(UIElement wfElement)
        {
            if ((wfElement != null) && (this.BeginWFLines.Count < this.MaxBeginWFLines) 
                && !this.BeginWFLines.Contains(wfElement) && !this.EndWFLines.Contains(wfElement))
            {
                this.BeginWFLines.Add(wfElement);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        public void BeginWFLinesRemove(UIElement wfElement)
        {
            if (wfElement != null && this.BeginWFLines.Contains(wfElement))
                this.BeginWFLines.Remove(wfElement);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        /// <returns></returns>
        public bool EndWFLinesAdd(UIElement wfElement)
        {
            if ((wfElement != null) && (this.EndWFLines.Count < this.MaxEndWFLines) &&
                !this.BeginWFLines.Contains(wfElement) && !this.EndWFLines.Contains(wfElement))
            {
                this.EndWFLines.Add(wfElement);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        public void EndWFLinesRemove(UIElement wfElement)
        {
            if ((wfElement != null) && this.EndWFLines.Contains(wfElement))
                this.EndWFLines.Remove(wfElement);
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetFocus()
        {
            this.wfShape.SetFocus();
            this.wfShape.Opacity = 1.0;
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetUnFocus()
        {
            this.wfShape.SetUnFocus();
            this.wfShape.Opacity = 0.7;
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetSelected()
        {
            this.wfShape.SetSelected();
            this.wfShape.Opacity = 1.0;
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitXY()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        public void SetXY(Point mousePoint)
        {
            this.wfShape.SetPreviousPoint(mousePoint);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="wfPointType"></param>
        /// <param name="sender"></param>
        public void ShowShadow(Point point, WFPointType wfPointType, object sender)
        {
            if ((this.BeginWFElement == null && this.EndWFElement == null) ||
                (this.BeginWFElement != null && this.EndWFElement != null && this.WFState == WFElementState.Focus))
                this.wfShape.ShowShadow(point, wfPointType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBegin"></param>
        /// <param name="pEnd"></param>
        public void ShowShadow(Point pBegin, Point pEnd)
        {
            this.wfShape.ShowShadow(pBegin, pEnd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void ShowShadow(PointCollection points)
        {
            this.wfShape.ShowShadow(points);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Point GetShadow()
        {
            return new Point(0, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <param name="sender"></param>
        public void ShowMe(Point mousePoint, object sender)
        {
            this.wfShape.ShowMe();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBegin"></param>
        /// <param name="pEnd"></param>
        public void ShowMe(Point pBegin, Point pEnd)
        {
            this.wfShape.ShowMe(pBegin, pEnd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void ShowMe(PointCollection points)
        {
            this.wfShape.ShowMe(points);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Point GetMe()
        {
            return new Point(0, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <returns></returns>
        public bool CheckPoint(Point mousePoint)
        {
            return this.wfShape.CheckPoints(this.wfShape.plShadow.Points);
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
            return this.wfShape.IsInside(point, x, y);
        }
        /// <summary>
        /// 
        /// </summary>
        public Point Location
        {
            get
            {
                return this.wfShape.plShadow.Points[0];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfHotspotType"></param>
        /// <returns></returns>
        public Point GetHotspot(WFHotspotType wfHotspotType)
        {
            Point point = new Point(0, 0);
            switch (wfHotspotType)
            {
                case WFHotspotType.Left:
                case WFHotspotType.Top:
                    point.X = this.wfShape.plShadow.Points[0].X;
                    point.Y = this.wfShape.plShadow.Points[0].Y;
                    break;
                case WFHotspotType.Right:
                case WFHotspotType.Bottom:
                    point.X = this.wfShape.plShadow.Points[3].X;
                    point.Y = this.wfShape.plShadow.Points[3].Y;
                    break;
            }
            return point;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfHotspotType"></param>
        /// <returns></returns>
        public Point GetShadowHotspot(WFHotspotType wfHotspotType)
        {
            return this.GetHotspot(wfHotspotType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public WFHotspotType GetNearHotspot(Point point)
        {
            return WFHotspotType.Left;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool PointIsInside(Point point)
        {
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Point GetBeign()
        {
            return this.wfShape.plShadow.Points[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Point GetEnd()
        {
            return this.wfShape.plShadow.Points[3];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBegin"></param>
        /// <param name="pEnd"></param>
        /// <returns></returns>
        public PointCollection GetPloyline(Point pBegin, Point pEnd)
        {
            return this.wfShape.GetPloyline(pBegin, pEnd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveShadow(double x, double y)
        {
            if ((this.BeginWFElement == null && this.EndWFElement == null) ||
                (this.BeginWFElement != null && this.EndWFElement != null && this.WFState == WFElementState.Focus))
            {
                Point pOld = this.GetBeign(), pNew = this.GetBeign();
                pNew.X += x;
                pNew.Y += y;

                PointCollection points = this.wfShape.GetPloyline(this.wfShape.plShadow.Points, pOld, pNew);
                if (this.wfShape.CheckPoints(points))
                    this.wfShape.plShadow.Points = points;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Move()
        {
            if ((this.BeginWFElement == null && this.EndWFElement == null)
                || (this.BeginWFElement != null && this.EndWFElement != null && this.WFState == WFElementState.Focus))
                this.wfShape.ShowMe();
        }

        public string ToXmlString()
        {
            string left = string.Empty, top = string.Empty;
            for (int i = 0; i < this.wfShape.plShadow.Points.Count; i++)
            {
                left += this.wfShape.plShadow.Points[i].X.ToString() + ",";
                top += this.wfShape.plShadow.Points[i].Y.ToString() + ",";
            }
            left = left.Trim(',');
            top = top.Trim(',');

            StringBuilder xml = new StringBuilder();
            xml.Append(@"        <WFElement ");
            xml.Append(@" UniqueID=""" + this.UniqueID + @"""");
            xml.Append(@" Title=""" + this.Title + @"""");
            xml.Append(@" WF11ElementType=""" + this.WFType + @"""");
            xml.Append(@" Left=""" + left + @"""");
            xml.Append(@" Top=""" + top + @"""");
            xml.Append(@" ZIndex=""" + this.ZIndex + @""">");
            xml.Append(Environment.NewLine);
            xml.Append(@"        </WFElement>");
            return xml.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UIElement Clone()
        {
            UIElement wfElement = new WFLine(this.canvasLeft, this.canvasTop, this.canvasWidth, this.canvasHeight);
            ((IWFElement)wfElement).Container = this.Container;
            ((WFLine)wfElement).wfShape.plShadow.Points.Clear();
            for (int i = 0; i < this.wfShape.plShadow.Points.Count; i++)
                ((WFLine)wfElement).wfShape.plShadow.Points.Add(this.wfShape.plShadow.Points[i]);
            Point point = this.wfShape.plShadow.Points[0];
            ((IWFElement)wfElement).SetXY(point);
            point.X += 10;
            point.Y += 10;
            ((IWFElement)wfElement).ShowShadow(point, WFPointType.Excursion, wfElement);
            ((IWFElement)wfElement).ShowMe(point, wfElement);

            return wfElement;
        }

        #endregion
    }
}
