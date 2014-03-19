//================================================================================
//  FileName: FlowStepInstancePresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/17
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

using iPower;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Flow;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Service
{
    /// <summary>
    /// 流程步骤实例编辑。
    /// </summary>
    public interface IFlowStepInstanceEditView : IModuleView
    {
        /// <summary>
        /// 获取步骤实例ID。
        /// </summary>
        GUIDEx StepInstanceID { get; }
        /// <summary>
        /// 绑定步骤实例状态。
        /// </summary>
        /// <param name="data"></param>
        void BindInstanceStepStatus(IListControlsData data);
    }
    /// <summary>
    /// 流程步骤实例行为类。
    /// </summary>
    public class FlowStepInstancePresenter : ModulePresenter<IFlowStepInstanceEditView>
    {
        #region 成员变量，构造函数。
        FlowStepInstanceEntity flowStepInstanceEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public FlowStepInstancePresenter(IFlowStepInstanceEditView view)
            : base(view)
        {
            this.flowStepInstanceEntity = new FlowStepInstanceEntity();
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            IFlowStepInstanceEditView editView = this.View as IFlowStepInstanceEditView;
            if (editView != null)
            {
                editView.BindInstanceStepStatus(this.EnumDataSource(typeof(EnumInstanceStepStatus)));
            }
        }
        #endregion

        #region 数据操作。
        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<FlowStepInstance>> handler)
        {
             IFlowStepInstanceEditView editView = this.View as IFlowStepInstanceEditView;
             if (handler != null && editView != null && editView.StepInstanceID.IsValid)
             {
                 FlowStepInstance data = new FlowStepInstance();
                 data.StepInstanceID = editView.StepInstanceID;
                 if (this.flowStepInstanceEntity.LoadRecord(ref data))
                     handler(this, new EntityEventArgs<FlowStepInstance>(data));
             }
        }
        /// <summary>
        /// 更新步骤实例状态。
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStepInstanceStatus(EnumInstanceStepStatus status)
        {
            bool result = false;
            IFlowStepInstanceEditView editView = this.View as IFlowStepInstanceEditView;
            if (editView != null && editView.StepInstanceID.IsValid)
            {
                 FlowStepInstance data = new FlowStepInstance();
                 data.StepInstanceID = editView.StepInstanceID;
                 if (result = this.flowStepInstanceEntity.LoadRecord(ref data))
                 {
                     if (data.InstanceStepStatus != (int)status)
                     {
                         data.InstanceStepStatus = (int)status;
                         result = this.flowStepInstanceEntity.UpdateRecord(data);
                     }
                 }
            }
            return result;
        }
        #endregion
    }
}
