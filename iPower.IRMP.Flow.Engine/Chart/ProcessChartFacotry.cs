//================================================================================
//  FileName: ProcessChartFacotry.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using iPower.IRMP.Flow;
using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Chart
{
    /// <summary>
    /// 流程图工厂类。
    /// </summary>
    public class ProcessChartFacotry
    {
        #region 成员变量，构造函数。
        Process process = null;
        ProcessStepChartCollection collection = null;
        Dictionary<Transition, Point[]> transitionPoints = null;

        float offsetX = 65f, offsetY = 85f;
        float x = 0f, y = 0f;
        float width = 0f, height = 0f;
        Color backgroundColor = Color.White, foregroundColor = Color.Blue, borderColor = Color.Blue, completeBackgroundColor = Color.GreenYellow;
        float borderWidth = 2f;

        List<GUIDEx> listInstanceStepIDs = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="process">流程数据。</param>
        /// <param name="width">图像宽度。</param>
        /// <param name="height">图像高度。</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public ProcessChartFacotry(Process process, float width, float height, float x, float y)
        {
            this.transitionPoints = new Dictionary<Transition, Point[]>();
            this.collection = new ProcessStepChartCollection();
            this.process = process;
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取或设置横向间隔。
        /// </summary>
        public float OffsetX
        {
            get { return this.offsetX; }
            set
            {
                if (value > 0)
                    this.offsetX = value;
            }
        }
        /// <summary>
        /// 获取或设置纵向间隔。
        /// 
        /// </summary>
        public float OffsetY
        {
            get { return this.offsetY; }
            set
            {
                if (value > 0)
                    this.offsetY = value;
            }
        }
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
        /// 获取或设置完成的背景色。
        /// </summary>
        public virtual Color CompleteBackgroundColor
        {
            get { return this.completeBackgroundColor; }
            set { this.completeBackgroundColor = value; }
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
        protected virtual float BorderWidth
        {
            get { return this.borderWidth; }
            set
            {
                if (value > 0)
                    this.borderWidth = value;
            }
        }
        #endregion

        #region 数据处理。
        /// <summary>
        /// 初始化数据。
        /// </summary>
        public void Init()
        {
            this.InitSteps();
            this.InitTransitionLines();
        }
        /// <summary>
        /// 设置流程实例ID。
        /// </summary>
        /// <param name="processInstanceID"></param>
        public void CreateProcessInstance(GUIDEx processInstanceID)
        {
            if (processInstanceID.IsValid)
            {
                this.listInstanceStepIDs = new FlowStepInstanceEntity().GetCompleteStepID(processInstanceID);
            }
        }
        /// <summary>
        /// 绘制图形。
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            if (g != null && this.collection.Count > 0)
            {
                bool isStepInstance = (this.listInstanceStepIDs != null) && (this.listInstanceStepIDs.Count > 0);
                //绘制图形。
                foreach (ProcessStepChart psc in this.collection)
                {
                    if (isStepInstance && this.listInstanceStepIDs.Contains(psc.StepID))
                        psc.BackgroundColor = this.CompleteBackgroundColor;
                    else
                        psc.BackgroundColor = this.BackgroundColor;
                    psc.ForegroundColor = this.ForegroundColor;
                    psc.BorderColor = this.BorderColor;
                    psc.BorderWidth = this.BorderWidth;

                    psc.Draw(g);
                }
                //绘制变迁线。
                if (this.transitionPoints != null && this.transitionPoints.Count > 0)
                {
                    Pen pen = new Pen(this.BorderColor, this.BorderWidth * 1.5f);
                    pen.EndCap = LineCap.ArrowAnchor;

                    foreach (KeyValuePair<Transition, Point[]> kvp in this.transitionPoints)
                    {
                        if (kvp.Key.IsComplete)
                            pen.Color = this.CompleteBackgroundColor;
                        else
                            pen.Color = this.BorderColor;
                        g.DrawLines(pen, kvp.Value);
                    }
                }
            }
        }
        #endregion

        #region 辅助函数。
        /// <summary>
        /// 初始化步骤。
        /// </summary>
        void InitSteps()
        {
            if (this.process != null && this.process.Steps != null)
            {
               
                //定义起点坐标。
                float x0 = this.x + this.OffsetX, y0 = this.y + this.OffsetY / 2;
                //起始步骤。
                Step[] stepFirst = this.process.Steps[EnumStepType.First];
                if (stepFirst != null && stepFirst.Length == 1)
                {
                    StartStepChart ssc = new StartStepChart(x0, y0);
                    ssc.StepID = stepFirst[0].StepID;
                    ssc.StepName = stepFirst[0].StepName;

                    float w = 0, h = 0;
                    if ((w = x0 + ssc.Width + this.OffsetX) <= this.width)
                        x0 = w;
                    else
                        throw new Exception(string.Format("图像设置宽度太小({0}px)，至少要有{1}px!", this.width, w));

                    if ((h = y0 + ssc.Height + this.OffsetY) > this.height)
                        throw new Exception(string.Format("图像设置高度太小({0}px)，至少要有{1}px!", this.height, h));

                    this.collection.Add(ssc);
                }
                else
                    throw new Exception("流程应该有且仅有一个起始步骤！");

                //中间步骤。
                Step[] stepMid = this.process.Steps[EnumStepType.Middle];
                if (stepMid != null && stepMid.Length > 0)
                {
                    TransitionCollection transitionCollection = this.process.Transitions;
                    for (int i = 0; i < stepMid.Length; i++)
                    {
                        Step step = stepMid[i];
                        if (step != null)
                        {
                            float w = 0, h = 0;
                            ProcessStepChart psc = null;
                            if (transitionCollection != null && transitionCollection.Count > 0)
                            {
                                TransitionCollection tc = transitionCollection.FindTransition(step.StepID);
                                if (tc != null && tc.Count > 1)
                                    psc = new BranchStepChart();
                            }
                            if (psc == null)
                                psc = new NonBranchStepChart();
                            psc.StepID = step.StepID;
                            psc.StepName = step.StepName;

                            //横向。
                            if ((w = x0 + psc.Width + this.OffsetX) <= this.width)
                            {
                                psc.X = x0;
                                psc.Y = y0;
                                x0 = w;
                            }
                            else
                            {
                                //换行。
                                ProcessStepChart p0 = this.collection[0];
                                if (p0 != null)
                                    x0 = p0.X;
                                else
                                    x0 = this.x + this.OffsetX;

                                if ((h = y0 + psc.Height + this.OffsetY) <= this.height)
                                     y0 = h;
                                 else
                                    throw new Exception("中间步骤高度越界！");
                                psc.X = x0;
                                psc.Y = y0;

                                x0 += psc.Width + this.OffsetX;
                            }

                            this.collection.Add(psc);
                        }
                    }
                }

                //终结步骤。
                Step[] stepLast = this.process.Steps[EnumStepType.Last];
                if (stepLast != null && stepLast.Length == 1)
                {
                    EndStepChart esc = new EndStepChart();
                    esc.StepID = stepLast[0].StepID;
                    esc.StepName = stepLast[0].StepName;

                    float w = 0, h = 0;
                    //横向。
                    if ((w = x0 + esc.Width + this.OffsetX) <= this.width)
                    {
                        esc.X = x0;
                        esc.Y = y0;
                        x0 = w;
                    }
                    else
                    {
                        //换行。
                        ProcessStepChart p0 = this.collection[0];
                        if (p0 != null)
                            x0 = p0.X;
                        else
                            x0 = this.x + this.OffsetX;

                        if ((h = y0 + esc.Height + this.OffsetY) <= this.height)
                            y0 = h;
                        else
                            throw new Exception("中间步骤高度越界！");
                        esc.X = x0;
                        esc.Y = y0;
                    }
                    this.collection.Add(esc);
                }
                else
                    throw new Exception("流程应该有且仅有一个终结步骤！");
            }
        }
        /// <summary>
        /// 初始化变迁数据。
        /// </summary>
        void InitTransitionLines()
        {
            if (this.process != null && this.process.Transitions != null)
            {
                this.transitionPoints = new Dictionary<Transition, Point[]>();
                foreach (Transition t in this.process.Transitions)
                {
                    if (t != null)
                    {
                        Point[] result = this.CreateTransitionPoints(t);
                        if (result != null && result.Length > 0)
                        {
                            this.transitionPoints.Add(t, result);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 创建连接线坐标。
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Point[] CreateTransitionPoints(Transition t)
        {
            Point[] result = null;
            if (t != null)
            {
                ProcessStepChart fromPsc = this.collection[t.FromStepID];
                ProcessStepChart toPsc = this.collection[t.ToStepID];
                List<Point> listPath = new List<Point>();
                if (fromPsc != null && toPsc != null)
                {
                    Point fc = fromPsc.CenterPoint;
                    Point tc = toPsc.CenterPoint;

                    //同行。
                    if (fc.Y == tc.Y)
                    {
                        #region 同行。
                        //横向相邻。
                        if (Math.Abs(fc.X - tc.X) == (fromPsc.Width / 2 + toPsc.Width / 2) + this.OffsetX)
                        {
                            if (fc.X < tc.X)//起点在左。
                            {
                                listPath.Add(fromPsc.RightPoint);
                                listPath.Add(toPsc.LeftPoint);
                            }
                            else//起点在右。
                            {
                                listPath.Add(fromPsc.LeftPoint);
                                listPath.Add(toPsc.RightPoint);
                            }
                        }
                        else//横向有间隔。
                        {
                            listPath.Add(fromPsc.BottomPoint);
                            listPath.Add(new Point((int)fromPsc.BottomPoint.X, (int)(fromPsc.BottomPoint.Y + this.OffsetY / 6)));
                            listPath.Add(new Point((int)toPsc.BottomPoint.X, (int)(toPsc.BottomPoint.Y + this.OffsetY / 6)));
                            listPath.Add(toPsc.BottomPoint);
                        }
                        #endregion
                    }
                    else if (fc.X == tc.X)//同列。
                    {
                        #region 同列。
                        //纵向相邻。
                        if (Math.Abs(fc.Y - tc.Y) == (fromPsc.Height / 2 + toPsc.Height / 2) + this.OffsetY)
                        {
                            #region 纵向相邻。
                            if (fc.Y < tc.Y)//起点在上。
                            {
                                listPath.Add(fromPsc.BottomPoint);
                                listPath.Add(toPsc.TopPoint);
                            }
                            else
                            {
                                listPath.Add(fromPsc.TopPoint);
                                listPath.Add(toPsc.BottomPoint);
                            }
                            #endregion
                        }
                        else
                        {
                            #region 纵向有间隔。
                            listPath.Add(fromPsc.RightPoint);
                            listPath.Add(new Point((int)(fromPsc.RightPoint.X + this.OffsetX / 5), fromPsc.RightPoint.Y));
                            listPath.Add(new Point((int)(toPsc.RightPoint.X + this.OffsetX / 5), toPsc.RightPoint.Y));
                            listPath.Add(toPsc.RightPoint);
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region 异行异列。
                        if (fc.X < tc.X)
                        {
                            #region 起点在左。
                            if (fc.Y < tc.Y)
                            {
                                #region 起点在左上。
                                if (Math.Abs(toPsc.LeftPoint.X - fromPsc.RightPoint.X) <= this.OffsetX)
                                {
                                    //横向无间隔。
                                    listPath.Add(fromPsc.RightPoint);
                                    listPath.Add(new Point((int)(fromPsc.RightPoint.X + this.OffsetX / 4), fromPsc.RightPoint.Y));
                                    listPath.Add(new Point((int)(fromPsc.RightPoint.X + this.OffsetX / 4), toPsc.LeftPoint.Y));
                                    listPath.Add(toPsc.LeftPoint);
                                }
                                else
                                {
                                    //横向有间隔。
                                    listPath.Add(fromPsc.RightPoint);
                                    listPath.Add(new Point((int)(fromPsc.RightPoint.X + this.OffsetX / 4), fromPsc.RightPoint.Y));
                                    listPath.Add(new Point((int)(fromPsc.RightPoint.X + this.OffsetX / 4), (int)(toPsc.TopPoint.Y + this.OffsetY / 4)));
                                    listPath.Add(new Point(toPsc.TopPoint.X, (int)(toPsc.TopPoint.Y + this.OffsetY / 4)));
                                    listPath.Add(toPsc.TopPoint);
                                }
                                #endregion
                            }
                            else
                            {
                                #region 起点在左下。
                                if (Math.Abs(toPsc.LeftPoint.X - fromPsc.RightPoint.X) <= this.OffsetX)
                                {
                                    //无间隔。
                                    listPath.Add(fromPsc.LeftPoint);
                                    listPath.Add(new Point((int)(fromPsc.LeftPoint.X - this.OffsetX / 4), fromPsc.LeftPoint.Y));
                                    listPath.Add(new Point((int)(fromPsc.LeftPoint.X - this.OffsetX / 4), toPsc.RightPoint.Y));
                                    listPath.Add(toPsc.RightPoint);
                                }
                                else
                                {
                                    //有间隔。
                                    listPath.Add(fromPsc.TopPoint);
                                    listPath.Add(new Point(fromPsc.TopPoint.X, (int)(fromPsc.TopPoint.Y - this.OffsetY / 4)));
                                    listPath.Add(new Point((int)(toPsc.RightPoint.X + this.OffsetX / 4), (int)(fromPsc.TopPoint.Y - this.OffsetY / 4)));
                                    listPath.Add(new Point((int)(toPsc.RightPoint.X + this.OffsetX / 4), toPsc.RightPoint.Y));
                                    listPath.Add(toPsc.RightPoint);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region 起点在右。
                            if (fc.Y < tc.Y)
                            {
                                #region 起点在右上。
                                if (Math.Abs(toPsc.TopPoint.Y - fromPsc.BottomPoint.Y) <= this.OffsetY)
                                {
                                    //纵向无间隔。
                                    listPath.Add(fromPsc.BottomPoint);
                                    listPath.Add(new Point(fromPsc.BottomPoint.X, (int)(fromPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(new Point(toPsc.TopPoint.X, (int)(fromPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(toPsc.TopPoint);
                                }
                                else
                                {
                                    //纵向有间隔。
                                    listPath.Add(fromPsc.BottomPoint);
                                    listPath.Add(new Point(fromPsc.BottomPoint.X, (int)(fromPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(new Point((int)(fromPsc.LeftPoint.X + this.OffsetX / 3), (int)(fromPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(new Point((int)(fromPsc.LeftPoint.X + this.OffsetX / 3), (int)(toPsc.TopPoint.Y - this.OffsetY / 3)));
                                    listPath.Add(new Point(toPsc.TopPoint.X, (int)(toPsc.TopPoint.Y - this.OffsetY / 3)));
                                    listPath.Add(toPsc.TopPoint);
                                }
                                #endregion
                            }
                            else
                            {
                                #region 起点在右下。
                                if (Math.Abs(fromPsc.TopPoint.Y - toPsc.BottomPoint.Y) <= this.OffsetY)
                                {
                                    //纵向无间隔。
                                    listPath.Add(fromPsc.TopPoint);
                                    listPath.Add(new Point(fromPsc.TopPoint.X, (int)(toPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(new Point(toPsc.BottomPoint.X, (int)(toPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(toPsc.BottomPoint);
                                }
                                else
                                {
                                    //纵向有间隔。
                                    listPath.Add(fromPsc.TopPoint);
                                    listPath.Add(new Point(fromPsc.TopPoint.X, (int)(fromPsc.TopPoint.Y - this.OffsetY / 3)));
                                    listPath.Add(new Point((int)(fromPsc.LeftPoint.X - this.OffsetX / 3), (int)(toPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(new Point(toPsc.BottomPoint.X, (int)(toPsc.BottomPoint.Y + this.OffsetY / 3)));
                                    listPath.Add(toPsc.BottomPoint);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                }

                if (listPath.Count > 0)
                {
                    result = new Point[listPath.Count];
                    listPath.CopyTo(result);
                }
            }
            return result;
        }
        #endregion
    }

    /// <summary>
    /// 流程步骤图形集合。
    /// </summary>
    internal class ProcessStepChartCollection : ICollection<ProcessStepChart>
    {
        #region 成员变量，构造函数。
        Dictionary<string, ProcessStepChart> dictionary = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ProcessStepChartCollection()
        {
            this.dictionary = new Dictionary<string, ProcessStepChart>();
        }
        #endregion

        #region 索引。
        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ProcessStepChart this[int index]
        {
            get
            {
                ProcessStepChart[] array = new ProcessStepChart[this.Count];
                this.dictionary.Values.CopyTo(array, 0);
                if (array != null && index < array.Length)
                    return array[index];
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public ProcessStepChart this[string stepID]
        {
            get
            {
                if (!string.IsNullOrEmpty(stepID))
                    return this.dictionary[stepID];
                return null;
            }
        }
        #endregion

        #region ICollection<ProcessStepChart> 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(ProcessStepChart item)
        {
            if (item != null)
            {
                if (!this.Contains(item))
                    this.dictionary.Add(item.StepID, item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.dictionary.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(ProcessStepChart item)
        {
            if (item != null)
                return this.dictionary.ContainsKey(item.StepID);
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(ProcessStepChart[] array, int arrayIndex)
        {
            this.dictionary.Values.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return this.dictionary.Count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(ProcessStepChart item)
        {
            if (item != null)
                return this.dictionary.Remove(item.StepID);
            return false;
        }

        #endregion

        #region IEnumerable<ProcessStepChart> 成员
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ProcessStepChart> GetEnumerator()
        {
            return this.dictionary.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (ProcessStepChart psc in this.dictionary.Values)
                yield return psc;
        }

        #endregion
    }
}
