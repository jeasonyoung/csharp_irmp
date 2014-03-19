//================================================================================
// FileName: FlowTransitionPresenter.cs
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
	/// IFlowTransitionView接口。
	///</summary>
	public interface IFlowTransitionView: IModuleView
	{
        /// <summary>
        /// 绑定所属流程数据。
        /// </summary>
        /// <param name="data"></param>
        void BindProcess(IListControlsData data);
        /// <summary>
        /// 消息处理。
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
	}
    /// <summary>
    /// 
    /// </summary>
    public interface IFlowTransitionListView : IFlowTransitionView
    {
        /// <summary>
        /// 获取步骤名称。
        /// </summary>
        string StepName { get; }
        /// <summary>
        /// 获取所属流程ID。
        /// </summary>
        GUIDEx ProcessID { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IFlowTransitionEditView : IFlowTransitionView
    {
        /// <summary>
        /// 获取变迁规则ID。
        /// </summary>
        GUIDEx TransitionID { get; }
        /// <summary>
        /// 获取或设置所属流程ID。
        /// </summary>
        GUIDEx ProcessID { get; set; }
        /// <summary>
        /// 获取或设置前驱步骤ID。
        /// </summary>
        GUIDEx FromStepID { get; set; }
        /// <summary>
        /// 获取或设置临时条件数据。
        /// </summary>
        DataTable TempTransitionCondition { get; set; }
        /// <summary>
        /// 绑定变迁规则。
        /// </summary>
        /// <param name="data"></param>
        void BindEnumTransitionRule(IListControlsData data);
        /// <summary>
        /// 绑定比较结果。
        /// </summary>
        /// <param name="data"></param>
        void BindEnumCompareSign(IListControlsData data);
        /// <summary>
        /// 绑定前驱步骤。
        /// </summary>
        /// <param name="data"></param>
        void BindFromStep(IListControlsData data);
        /// <summary>
        /// 绑定后续步骤。
        /// </summary>
        /// <param name="data"></param>
        void BindToStep(IListControlsData data);
        /// <summary>
        /// 绑定变迁条件。
        /// </summary>
        /// <param name="data"></param>
        void BindTransitionParameter(IListControlsData data);
        /// <summary>
        /// 加载变迁条件。
        /// </summary>
        void LoadTransitionCondition();
    }
    /// <summary>
    /// 变迁条件接口。
    /// </summary>
    public interface IFlowTransitionConditionView : IModuleView
    {
        GUIDEx TransitionParameterID { get; }
        GUIDEx ConditionID { get; }
        string CompareValue { get; }
    }
	///<summary>
	/// FlowTransitionPresenter行为类。
	///</summary>
	public class FlowTransitionPresenter: ModulePresenter<IFlowTransitionView>
	{
		#region 成员变量，构造函数。
        FlowTransitionEntity flowTransitionEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public FlowTransitionPresenter(IFlowTransitionView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Transition_ModuleID;
            this.flowTransitionEntity = new FlowTransitionEntity();
		}
		#endregion

        #region 属性。
        /// <summary>
        /// 列表数据。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IFlowTransitionListView listView = this.View as IFlowTransitionListView;
                if (listView != null)
                {
                    DataTable dtSource = this.flowTransitionEntity.ListDataSource(listView.StepName, listView.ProcessID);

                    dtSource.Columns.Add("TransitionRuleName", typeof(String));

                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["TransitionRuleName"] = this.GetEnumMemberName(typeof(EnumTransitionRule), Convert.ToInt32(row["TransitionRule"]));
                    }

                    return dtSource;
                }
                return null;
            }
        }
        #endregion

        #region 重载。
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();

            IFlowTransitionView tView = this.View as IFlowTransitionView;
            if (tView != null)
                tView.BindProcess(new FlowProcessEntity().BindProcess());

            IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
            if (editView != null)
            {
                editView.BindEnumTransitionRule(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumTransitionRule), this.ModuleConfig));
                editView.BindEnumCompareSign(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumCompareSign), new int[] { 0x00 }, this.ModuleConfig));
                this.ChangeProcess();
                this.LoadListTransitionConditionDataSouce();
            }
        }
        #endregion

        #region 数据操作函数。
        /// <summary>
        /// 变更所属流程。
        /// </summary>
        public void ChangeProcess()
        {
             IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
             if (editView != null)
             {
                 editView.BindFromStep(new FlowStepEntity().BindStep(editView.ProcessID, GUIDEx.Null));
                 this.ChangeFromStep();
             }
        }
        /// <summary>
        /// 变更前驱。
        /// </summary>
        public void ChangeFromStep()
        {
            IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
            if (editView != null)
            {
                editView.BindToStep(new FlowStepEntity().BindStep(editView.ProcessID, editView.FromStepID));

                editView.BindTransitionParameter(new FlowParameterEntity().BindParameter(editView.FromStepID));
            }
        }
        /// <summary>
        /// 增加变迁条件。
        /// </summary>
        public bool AddTransitionCondition()
        {
            bool bAddOrUpdate = false;
            IFlowTransitionConditionView tView = this.View as IFlowTransitionConditionView;
            if (tView != null &&  tView.TransitionParameterID.IsValid && tView.ConditionID.IsValid 
                && !string.IsNullOrEmpty(tView.CompareValue))
            {
                IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
                if (editView != null)
                {
                    DataTable dtSource = editView.TempTransitionCondition;
                    if (dtSource != null)
                    {
                       // EnumCompareSign enumCondition = (EnumCompareSign)Enum.Parse(typeof(EnumCompareSign), tView.ConditionID.Value);
                        string strConditionValueName = this.GetEnumMemberName(typeof(EnumCompareSign), Convert.ToInt32(tView.ConditionID));
                        DataRow dr = null;
                        DataRow[] rows = dtSource.Select(string.Format("ParameterID='{0}'", tView.TransitionParameterID));
                        if (rows != null && rows.Length > 0)
                        {
                            dr = rows[0];
                            dr["ConditionValue"] = int.Parse(tView.ConditionID);
                            dr["ConditionValueName"] = strConditionValueName;
                            dr["CompareValue"] = tView.CompareValue;

                            bAddOrUpdate = true;
                        }
                        else
                        {
                            FlowParameter parameter = new FlowParameter();
                            parameter.ParameterID = tView.TransitionParameterID;

                            FlowParameterEntity parameterEntity = new FlowParameterEntity();
                            if (parameterEntity.LoadRecord(ref parameter))
                            {
                                dr = dtSource.NewRow();
                                dr["ConditionID"] = GUIDEx.New;
                                dr["ParameterID"] = parameter.ParameterID;
                                dr["ParameterName"] = parameter.ParameterName;
                                dr["ConditionValue"] = int.Parse(tView.ConditionID);
                                dr["ConditionValueName"] = strConditionValueName;
                                dr["CompareValue"] = tView.CompareValue;

                                dtSource.Rows.Add(dr);

                                bAddOrUpdate = true;
                            }
                        }

                        if (bAddOrUpdate)
                        {
                            dtSource.AcceptChanges();
                            editView.TempTransitionCondition = dtSource.Copy();
                        }
                    }
                }
            }

            return bAddOrUpdate;
        }
        /// <summary>
        /// 移除变迁条件。
        /// </summary>
        /// <param name="pri"></param>
        /// <returns></returns>
        public bool RemoveTransitionCondition(StringCollection pri)
        {
            bool result = false;
            IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
            if (editView != null && pri != null && pri.Count > 0)
            {
                DataTable dtSource = editView.TempTransitionCondition;
                if (dtSource != null)
                {
                    dtSource.PrimaryKey = new DataColumn[] { dtSource.Columns["ConditionID"] };
                    DataRow dr = null;
                    foreach (string id in pri)
                    {
                        dr = dtSource.Rows.Find(id);
                        if (dr != null)
                        {
                            dtSource.Rows.Remove(dr);
                            dtSource.AcceptChanges();
                            result = true;
                        }
                    }

                    if (result)
                    {
                        dtSource.AcceptChanges();
                        editView.TempTransitionCondition = dtSource.Copy();
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 从数据库加载变迁条件数据源。
        /// </summary>
        public void LoadListTransitionConditionDataSouce()
        {
            IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
            if (editView != null)
            {
                DataTable dtSource = new FlowConditionEntity().ListDataSource(editView.TransitionID);
                dtSource.Columns.Add("ConditionValueName", typeof(String));
                foreach (DataRow row in dtSource.Rows)
                {
                    row["ConditionValueName"] = this.GetEnumMemberName(typeof(EnumCompareSign), Convert.ToInt32(row["ConditionValue"]));
                }
                editView.TempTransitionCondition = dtSource.Copy();
            }
        }
        ///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<FlowTransition>> handler)
		{
            IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
            if (editView != null && editView.TransitionID.IsValid)
            {
                FlowTransition data = new FlowTransition();
                data.TransitionID = editView.TransitionID;
                if (this.flowTransitionEntity.LoadRecord(ref data))
                {
                    handler(this, new EntityEventArgs<FlowTransition>(data));
                    this.LoadListTransitionConditionDataSouce();
                    editView.LoadTransitionCondition();
                }
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <returns></returns>
        public bool UpdateData(FlowTransition data)
        {
            bool result = false;
            if (data != null)
            {
                result = this.flowTransitionEntity.UpdateRecord(data);
                IFlowTransitionEditView editView = this.View as IFlowTransitionEditView;
                if (editView != null && result)
                {
                    DataTable dtSource = editView.TempTransitionCondition;
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        FlowCondition condition = new FlowCondition();
                        FlowConditionEntity conditionEntity = new FlowConditionEntity();
                        foreach (DataRow row in dtSource.Rows)
                        {
                            condition.ConditionID = new GUIDEx(row["ConditionID"]);
                            condition.ParameterID = new GUIDEx(row["ParameterID"]);
                            condition.TransitionID = data.TransitionID;
                            condition.ConditionValue = Convert.ToInt32(row["ConditionValue"]);
                            condition.CompareValue = Convert.ToString(row["CompareValue"]);

                            result = conditionEntity.UpdateRecord(condition);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="pri"></param>
        /// <returns></returns>
        public bool BatchDeleteData(StringCollection pri)
        {
            bool result = false;
            try
            {
                result = this.flowTransitionEntity.DeleteRecord(pri);
            }
            catch (Exception e)
            {
                IFlowTransitionListView listView = this.View as IFlowTransitionListView;
                if (listView != null)
                    listView.ShowMessage(e.Message);
            }
            return result;
        }
		#endregion
    }

}
