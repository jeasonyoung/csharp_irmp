//================================================================================
//  FileName: ShapeStep.xaml.cs
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

using iPower.IRMP.Flow.Design.Elements;
namespace iPower.IRMP.Flow.Design.ElementShape
{
    /// <summary>
    /// 步骤图形元素。
    /// </summary>
    public partial class ShapeStep : UserControl, IWFShape
    {
        #region 成员变量，构造函数。
        WFElementState wfState = WFElementState.Focus;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ShapeStep()
        {
            InitializeComponent();
        }
        #endregion

        #region 辅助函数。
        void SetHotspotStyle(Color color, double opacity)
        {
            this.hotspotLeft.Fill = new SolidColorBrush(color);
            this.hotspotLeft.Opacity = opacity;

            this.hotspotRight.Fill = new SolidColorBrush(color);
            this.hotspotRight.Opacity = opacity;

            this.hotspotTop.Fill = new SolidColorBrush(color);
            this.hotspotTop.Opacity = opacity;

            this.hotspotBottom.Fill = new SolidColorBrush(color);
            this.hotspotBottom.Opacity = opacity;
        }
        #endregion

        #region IWFShape 成员

        public WFElementState WFState
        {
            get { return this.wfState; }
        }

        public void SetFocus()
        {
            if (this.wfState != WFElementState.Focus)
            {
                this.SetHotspotStyle(Colors.Blue, 1.0);

                this.hotspotLeft.Visibility = this.hotspotRight.Visibility = Visibility.Visible;
                this.hotspotTop.Visibility = this.hotspotBottom.Visibility = Visibility.Visible;

                this.wfState = WFElementState.Focus;
            }
        }

        public void SetUnFocus()
        {
            if (this.wfState != WFElementState.UnFocus)
            {
                this.hotspotLeft.Visibility = this.hotspotRight.Visibility = Visibility.Collapsed;
                this.hotspotTop.Visibility = this.hotspotBottom.Visibility = Visibility.Collapsed;

                this.wfState = WFElementState.UnFocus;
            }
        }

        public void SetSelected()
        {
            if (this.wfState != WFElementState.Selected)
            {
                this.SetHotspotStyle(Colors.Red, 0.5);

                this.hotspotLeft.Visibility = this.hotspotRight.Visibility = Visibility.Visible;
                this.hotspotTop.Visibility = this.hotspotBottom.Visibility = Visibility.Visible;

                this.wfState = WFElementState.Selected;
            }
        }

        public void SetTitle(string title)
        {
            this.txtbStepTitle.Text = title;
        }

        public string GetTitle()
        {
            return this.txtbStepTitle.Text;
        }

        public void Fill(Color color, double opacity)
        {
            this.rectStep.Fill = new SolidColorBrush(color);
            this.rectStep.Opacity = opacity;
        }

        #endregion
    }
}
