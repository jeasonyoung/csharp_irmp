//================================================================================
//  FileName: InstanceFlowChartService.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/18
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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using iPower;
using iPower.IRMP.Flow;
using iPower.IRMP.Flow.Engine.Chart;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Service
{
    /// <summary>
    ///实例流程图接口。
    /// </summary>
    public interface IFlowChartView
    {
        /// <summary>
        /// 获取图像宽度。
        /// </summary>
        int Width { get; }
        /// <summary>
        /// 获取图像高度。
        /// </summary>
        int Height { get; }
        /// <summary>
        /// 获取流程ID。
        /// </summary>
        GUIDEx ProcessID { get; }
        /// <summary>
        /// 流程实例ID。
        /// </summary>
        GUIDEx ProcessInstanceID { get; }
    }
    /// <summary>
    /// 流程图服务。
    /// </summary>
    public class FlowChartService
    {
        #region 成员变量,构造函数。
        IFlowChartView view;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public FlowChartService(IFlowChartView view)
        {
            this.view = view;
        }
        #endregion

        #region 输出图形。
        /// <summary>
        /// 输出流程图。
        /// </summary>
        /// <param name="output"></param>
        public void DrawFlowChart(Stream output)
        {
            try
            {
                lock (this)
                {
                    if (output == null)
                        throw new ArgumentNullException("output", "输出流不存在！");
                    if (this.view == null && (!this.view.ProcessID.IsValid || !this.view.ProcessInstanceID.IsValid))
                        throw new ArgumentNullException("流程ID或流程实例ID为空！");

                    //获取流程定义。
                    Process p = this.LoadProcess();
                    if (p == null)
                        throw new Exception("流程不存在！");
                    //绘制流程图。
                    using (Bitmap img = new Bitmap(this.view.Width, this.view.Height))
                    {
                        using (Graphics g = Graphics.FromImage(img))
                        {
                            //清屏幕。
                            g.Clear(Color.White);
                            //定义起点坐标。
                            float x = 0f, y = 0f;
                            ProcessChartFacotry factory = new ProcessChartFacotry(p, img.Width, img.Height, x, y);
                            //初始化步骤。
                            factory.Init();
                            //流程实例ID。
                            factory.CreateProcessInstance(this.view.ProcessInstanceID);
                            //绘制图形。
                            factory.Draw(g);
                                                        
                            g.Dispose();
                        }
                        img.Save(output, ImageFormat.Gif);
                        img.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 辅助函数。
        Process LoadProcess()
        {
            Process p = null;
            if (this.view.ProcessInstanceID.IsValid)
            {
                FlowProcessInstance data = new FlowProcessInstance();
                data.ProcessInstanceID = this.view.ProcessInstanceID;
                if (new FlowProcessInstanceEntity().LoadRecord(ref data))
                {
                    p = Utils.DeSerializerDatabaseFormart<Process>(data.ProcessSerialization, data.Verify);
                }
            }
            if (p == null && this.view.ProcessID.IsValid)
            {
                p = ModuleUtils.CreateProcess(this.view.ProcessID);
            }
            return p;
        }
        #endregion

        #region 创建图形。
        ///// <summary>
        ///// 创建开始步骤。
        ///// </summary>
        ///// <param name="g"></param>
        ///// <param name="p"></param>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <param name="text"></param>
        //void CreateBeginStepEllipse(Graphics g,Pen p, int x, int y, string text)
        //{
        //    if (g != null && p != null)
        //    {
        //        double width = 200, height = 100;
        //        g.DrawEllipse(p, x, y, (int)width, (int)height);
        //        if (!string.IsNullOrEmpty(text))
        //        {
        //            Font f = new Font("Arial", 16);
        //            SolidBrush brush = new SolidBrush(Color.Black);
        //            PointF drawPoint = new PointF((float)x, (float)((y + height) / 2.0));
        //            g.DrawString(text, f, brush, drawPoint);
        //        }
        //    }
        //}
        #endregion
    }
}
