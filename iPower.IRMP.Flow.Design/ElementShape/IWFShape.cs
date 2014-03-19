//================================================================================
//  FileName: IWFShape.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/25
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
using System.Windows.Media;
using iPower.IRMP.Flow.Design.Elements;
namespace iPower.IRMP.Flow.Design.ElementShape
{
    /// <summary>
    /// 控件接口。
    /// </summary>
    public interface IWFShape
    {
        /// <summary>
        /// 获取控件状态。
        /// </summary>
        WFElementState WFState
        {
            get;
        }
        /// <summary>
        /// 设置控件获得焦点状态。
        /// </summary>
        void SetFocus();
        /// <summary>
        /// 设置控件失去焦点状态。
        /// </summary>
        void SetUnFocus();
        /// <summary>
        /// 设置控件为待选。
        /// </summary>
        void SetSelected();
        /// <summary>
        /// 设置标题。
        /// </summary>
        /// <param name="title">标题。</param>
        void SetTitle(string title);
        /// <summary>
        /// 获取标题。
        /// </summary>
        ///<returns>标题。</returns>
        string GetTitle();
        /// <summary>
        /// 用颜色填充。
        /// </summary>
        /// <param name="color">颜色。</param>
        /// <param name="opacity">透明度。</param>
        void Fill(Color color, double opacity);
    }
}
