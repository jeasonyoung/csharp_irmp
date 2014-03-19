//================================================================================
//  FileName: IWFElement.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/24
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

using System.Collections.Generic;

using iPower.IRMP.Flow.Design.Designer;
using iPower.IRMP.Flow.Design.ElementShape;
namespace iPower.IRMP.Flow.Design.Elements
{
    /// <summary>
    /// 元素状态枚举。
    /// </summary>
    public enum WFElementState
    {
        /// <summary>
        /// 焦点。
        /// </summary>
        Focus = 0,
        /// <summary>
        /// 失去焦点。
        /// </summary>
        UnFocus,
        /// <summary>
        /// 选中。
        /// </summary>
        Selected
    }
    /// <summary>
    /// 元素类型枚举。
    /// </summary>
    public enum WFElementType
    {
        /// <summary>
        /// 开始。
        /// </summary>
        Start = 0,
        /// <summary>
        /// 步骤。
        /// </summary>
        Step,
        /// <summary>
        /// 条件。
        /// </summary>
        Condition,
        /// <summary>
        /// 连线。
        /// </summary>
        Line,
        /// <summary>
        /// 结束。
        /// </summary>
        Finish
    }
    /// <summary>
    /// 元素热点类型枚举。
    /// </summary>
    public enum WFHotspotType
    {
        /// <summary>
        /// 左。
        /// </summary>
        Left = 0,
        /// <summary>
        /// 上。
        /// </summary>
        Top,
        /// <summary>
        /// 右。
        /// </summary>
        Right,
        /// <summary>
        /// 下。
        /// </summary>
        Bottom
    }
    /// <summary>
    /// 元素坐标点类型枚举。
    /// </summary>
    public enum WFPointType
    {
        /// <summary>
        /// 精确坐标点。
        /// </summary>
        Precision = 0,
        /// <summary>
        /// 偏移坐标点。
        /// </summary>
        Excursion
    }
    /// <summary>
    /// WF元素接口。
    /// </summary>
    public interface IWFElement
    {
        /// <summary>
        /// 
        /// </summary>
        IContainer Container { get; set; }
        /// <summary>
        /// 
        /// </summary>
        WFElementState WFState { get; }
        /// <summary>
        /// 
        /// </summary>
        WFElementType WFType { get; }
        /// <summary>
        /// 
        /// </summary>
        int MaxBeginWFLines { get; }
        /// <summary>
        /// 
        /// </summary>
        List<UIElement> BeginWFLines { get; }
        /// <summary>
        /// 
        /// </summary>
        List<UIElement> EndWFLines { get; }
        /// <summary>
        /// 
        /// </summary>
        int MaxEndWFLines { get; }
        /// <summary>
        /// 
        /// </summary>
        string UniqueID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int ZIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        /// <returns></returns>
        bool BeginWFLinesAdd(UIElement wfElement);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        void BeginWFLinesRemove(UIElement wfElement);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        /// <returns></returns>
        bool EndWFLinesAdd(UIElement wfElement);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        void EndWFLinesRemove(UIElement wfElement);
        /// <summary>
        /// 
        /// </summary>
        void SetFocus();
        /// <summary>
        /// 
        /// </summary>
        void SetUnFocus();
        /// <summary>
        /// 
        /// </summary>
        void SetSelected();
        /// <summary>
        /// 
        /// </summary>
        void InitXY();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        void SetXY(Point mousePoint);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="wfPointType"></param>
        /// <param name="sender"></param>
        void ShowShadow(Point point, WFPointType wfPointType, object sender);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Point GetShadow();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <param name="sender"></param>
        void ShowMe(Point mousePoint, object sender);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Point GetMe();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <returns></returns>
        bool CheckPoint(Point mousePoint);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool IsInside(Point point, double x, double y);
        /// <summary>
        /// 
        /// </summary>
        Point Location { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfHotspotType"></param>
        /// <returns></returns>
        Point GetHotspot(WFHotspotType wfHotspotType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfHotspotType"></param>
        /// <returns></returns>
        Point GetShadowHotspot(WFHotspotType wfHotspotType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        WFHotspotType GetNearHotspot(Point point);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool PointIsInside(Point point);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void MoveShadow(double x, double y);
        /// <summary>
        /// 
        /// </summary>
        void Move();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        String ToXmlString();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        UIElement Clone();
    }
}
