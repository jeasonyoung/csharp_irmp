//================================================================================
// FileName: FlowProcessInstancePresenter.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Service
{
	///<summary>
	/// IFlowProcessInstanceView接口。
	///</summary>
	public interface IFlowProcessInstanceView: IModuleView
	{
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
	}
    /// <summary>
    /// 流程实例管理列表界面接口。
    /// </summary>
    public interface IFlowProcessInstanceListView : IFlowProcessInstanceView
    {
        /// <summary>
        /// 获取流程实例名称。
        /// </summary>
        string ProcessInstanceName { get; }
    }
    /// <summary>
    /// 流程实例管理编辑界面接口。
    /// </summary>
    public interface IFlowProcessInstanceEditView : IFlowProcessInstanceView
    {
        /// <summary>
        /// 获取流程实例ID。
        /// </summary>
        GUIDEx ProcessInstanceID { get; }
        /// <summary>
        /// 绑定流程实例状态。
        /// </summary>
        /// <param name="data"></param>
        void BindFlowInstanceStatus(IListControlsData data);
        /// <summary>
        /// 绑定流程履历数据。
        /// </summary>
        /// <param name="dataSource"></param>
        void BindProcessResumes(object dataSource);
    }
		
	///<summary>
	/// FlowProcessInstancePresenter行为类。
	///</summary>
	public class FlowProcessInstancePresenter: ModulePresenter<IFlowProcessInstanceView>
	{
		#region 成员变量，构造函数。
        FlowProcessInstanceEntity flowProcessInstanceEntity = null;
        FlowStepInstanceEntity flowStepInstanceEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public FlowProcessInstancePresenter(IFlowProcessInstanceView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.ProcessInstance_ModuleID;
            this.flowProcessInstanceEntity = new FlowProcessInstanceEntity();
            this.flowStepInstanceEntity = new FlowStepInstanceEntity();
		}
		#endregion

        #region 重载。
        /// <summary>
        /// 重载。
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            IFlowProcessInstanceEditView editView = this.View as IFlowProcessInstanceEditView;
            if (editView != null)
            {
                editView.BindFlowInstanceStatus(this.EnumDataSource(typeof(EnumInstanceProcessStatus)));
            }

        }
        #endregion

        #region 数据操作函数。
        /// <summary>
        /// 获取列表数据源。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IFlowProcessInstanceListView listView = this.View as IFlowProcessInstanceListView;
                if (listView != null)
                {
                    DataTable dtSource = this.flowProcessInstanceEntity.ListDataSource(listView.ProcessInstanceName);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("InstanceProcessStatusName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["InstanceProcessStatusName"] = this.GetEnumMemberName(typeof(EnumInstanceProcessStatus), Convert.ToInt32(row["InstanceProcessStatus"]));
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
        
        /// <summary>
        /// 获取编辑数据源。
        /// </summary>
        public DataTable StepInstanceDataSource
        {
            get
            {
                IFlowProcessInstanceEditView editView = this.View as IFlowProcessInstanceEditView;
                if (editView != null)
                {
                    DataTable dtSource = new FlowStepInstanceEntity().ListDataSource(editView.ProcessInstanceID);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("InstanceStepStatusName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["InstanceStepStatusName"] = this.GetEnumMemberName(typeof(EnumInstanceStepStatus), Convert.ToInt32(row["InstanceStepStatus"]));
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取步骤实例任务数据源。
        /// </summary>
        public DataTable StepInstanceTaskDataSource
        {
            get
            {
                IFlowProcessInstanceEditView editView = this.View as IFlowProcessInstanceEditView;
                if (editView != null && editView.ProcessInstanceID.IsValid)
                {
                    DataTable dtSource = new FlowInstanceTaskEntity().ListDataSource(editView.ProcessInstanceID);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("StepInstanceName");
                        dtSource.Columns.Add("TaskCategoryName");
                        dtSource.Columns.Add("BeginModeName");
                        dtSource.Columns.Add("EndModeName");

                        string processInstanceName = null, stepInstanceName = null;
                        foreach (DataRow row in dtSource.Rows)
                        {
                            if (this.flowStepInstanceEntity.GetInstanceStepName(new GUIDEx(row["StepInstanceID"]), out processInstanceName, out stepInstanceName))
                                row["StepInstanceName"] = stepInstanceName;
                            row["TaskCategoryName"] = this.GetEnumMemberName(typeof(EnumTaskCategory), Convert.ToInt32(row["TaskCategory"]));
                            row["BeginModeName"] = this.GetEnumMemberName(typeof(EnumTaskBeginMode), Convert.ToInt32(row["BeginMode"]));
                            row["EndModeName"] = this.GetEnumMemberName(typeof(EnumTaskEndMode), Convert.ToInt32(row["EndMode"]));
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取异常数据源。
        /// </summary>
        public DataTable StepInstanceRunErrorDataSource
        {
            get
            {
                IFlowProcessInstanceEditView editView = this.View as IFlowProcessInstanceEditView;
                if (editView != null)
                {
                    return this.flowProcessInstanceEntity.StepInstanceRunErrorListDataSource(editView.ProcessInstanceID);
                }
                return null;
            }
        }
       
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<FlowProcessInstance>> handler)
		{
            IFlowProcessInstanceEditView editView = this.View as IFlowProcessInstanceEditView;
            if (editView != null && editView.ProcessInstanceID.IsValid)
            {
                FlowProcessEntity flowProcessEntity = new FlowProcessEntity();
                FlowProcessInstance data = new FlowProcessInstance();
                data.ProcessInstanceID = editView.ProcessInstanceID;
                if (this.flowProcessInstanceEntity.LoadRecord(ref data))
                {
                    FlowProcess flowProcess = new FlowProcess();
                    flowProcess.ProcessID = data.ProcessID;
                    if (flowProcessEntity.LoadRecord(ref flowProcess))
                        data.ProcessName = flowProcess.ProcessName;

                    handler(this, new EntityEventArgs<FlowProcessInstance>(data));
                    editView.BindProcessResumes(new FlowStepInstanceDataEntity().GetProcessResumes(data.ProcessInstanceID));
                }
            }
		}
        
        /// <summary>
        /// 更新流程实例状态。
        /// </summary>
        /// <param name="processInstanceID">流程实例ID。</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool ChangeFlowInstanceStatus(GUIDEx processInstanceID, EnumInstanceProcessStatus status)
        {
            return this.flowProcessInstanceEntity.ChangeFlowInstanceStatus(processInstanceID, status);
        }
        
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="priColletion"></param>
        /// <returns></returns>
        public bool BatchDeleteFlowProcessInstance(StringCollection priColletion)
        {
            bool result = false;
            if (priColletion != null && priColletion.Count > 0)
            {
                string err = null;
                foreach (string p in priColletion)
                {
                    result = this.flowProcessInstanceEntity.DeleteRecord(p, out err);
                    if (!result)
                    {
                        this.View.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 批量删除任务数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteStepInstanceTask(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                result = new FlowInstanceTaskEntity().DeleteRecord(priCollection);
            }
            return result;
        }
		#endregion

	}

}
