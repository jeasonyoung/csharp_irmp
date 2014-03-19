//================================================================================
//  FileName: IContainer.cs
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
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.IO;

using iPower.IRMP.Flow.Design.Elements;
namespace iPower.IRMP.Flow.Design.Designer
{
    /// <summary>
    /// 容器接口。
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 获取或设置流程ID。
        /// </summary>
        string ProcessID
        {
            get;
            set;
        }
        /// <summary>
        /// 获取元素集合。
        /// </summary>
        List<UIElement> WFElements
        {
            get;
        }
        /// <summary>
        /// 获取选中的元素集合。
        /// </summary>
        List<UIElement> SelectedWFElements
        {
            get;
        }
        /// <summary>
        /// 获取需要移动失去焦点的线的元素集合。
        /// </summary>
        List<UIElement> NeedMoveUnFocusWFLines
        {
            get;
        }
        /// <summary>
        /// 复制元素集合。
        /// </summary>
        List<UIElement> CopyedWFElements
        {
            get;
        }
        /// <summary>
        /// 获取步骤元素集合。
        /// </summary>
        /// <returns></returns>
        List<UIElement> GetWFSteps();
        /// <summary>
        /// 添加需要移动失去焦点的线的元素。
        /// </summary>
        /// <param name="wfElement"></param>
        void NeedMoveUnFocusWFLineAdd(UIElement wfElement);
        /// <summary>
        /// 
        /// </summary>
        bool CtrlKeyIsPress
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfElement"></param>
        void SetWFElementSeleted(UIElement wfElement);
        /// <summary>
        /// 获取是否多选。
        /// </summary>
        /// <param name="wfElement"></param>
        /// <returns></returns>
        bool IsMultiSelected(UIElement wfElement);
        /// <summary>
        /// 获取坐标下的元素。
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        UIElement GetWFElementByPoint(Point point);
        /// <summary>
        /// 创建元素。
        /// </summary>
        /// <param name="wfType"></param>
        /// <param name="locations"></param>
        /// <returns></returns>
        UIElement CreateWFElement(WFElementType wfType, PointCollection locations);
        /// <summary>
        /// 
        /// </summary>
        double SimpleShapeLeft { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsMouseSelecting { get; }
        /// <summary>
        /// 
        /// </summary>
        void SetGridLines();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
        /// <summary>
        /// 序列化。
        /// </summary>
        /// <param name="stream"></param>
        Stream Serializer();
        /// <summary>
        /// 反序列化。
        /// </summary>
        /// <param name="stream"></param>
        void DeSerializer(Stream stream);
    }
}
