//================================================================================
// FileName: FlowProcessPresenter.cs
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
using System.IO;
using System.Xml;

using iPower;
using iPower.Data;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;

using iPower.IRMP.Flow;
namespace iPower.IRMP.Flow.Engine.Service
{
	///<summary>
	/// IFlowProcessView接口。
	///</summary>
	public interface IFlowProcessView: IModuleView
	{

	}
    /// <summary>
    /// 列表页面接口。
    /// </summary>
    public interface IFlowProcessListView : IFlowProcessView
    {
        /// <summary>
        /// 获取流程名称。
        /// </summary>
        string ProcessName
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
    }
    /// <summary>
    /// 编辑页面接口。
    /// </summary>
    public interface IFlowProcessEditView : IFlowProcessView
    {
        /// <summary>
        /// 获取流程ID。
        /// </summary>
        GUIDEx ProcessID
        {
            get;
        }
        /// <summary>
        /// 绑定步骤类型。
        /// </summary>
        /// <param name="data"></param>
        void BindEnumProcessStatus(IListControlsData data);
        /// <summary>
        /// 显示信息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// 数据导入。
    /// </summary>
    public interface IFlowProcessImportView : IFlowProcessView
    {
        /// <summary>
        /// 
        /// </summary>
        string ProcessXml { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messge"></param>
        void ShowMesssage(string messge);
    }
    /// <summary>
    /// 数据导出。
    /// </summary>
    public interface IFlowProcessExportView : IFlowProcessView
    {
        /// <summary>
        /// 获取流程ID。
        /// </summary>
        GUIDEx ProcessID
        {
            get;
        }
    }
	///<summary>
	/// FlowProcessPresenter行为类。
	///</summary>
	public class FlowProcessPresenter: ModulePresenter<IFlowProcessView>
	{
		#region 成员变量，构造函数。
        FlowProcessEntity processEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public FlowProcessPresenter(IFlowProcessView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Process_ModuleID;
            this.processEntity = new FlowProcessEntity();
		}
		#endregion

        #region 属性。
        /// <summary>
        /// 列表数据源。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IFlowProcessListView listView = this.View as IFlowProcessListView;
                if (listView != null)
                {
                    DataTable dtSource = this.processEntity.ListDataSource(listView.ProcessName);
                    dtSource.Columns.Add("ProcessStatusName", typeof(string));
                    dtSource.Columns.Add("FlowProcessChart", typeof(string));
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["FlowProcessChart"] = "流程图";
                        row["ProcessStatusName"] = this.GetEnumMemberName(typeof(EnumProcessStatus), Convert.ToInt32(row["ProcessStatus"]));
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
            IFlowProcessEditView editView = this.View as IFlowProcessEditView;
            if (editView != null)
            {
                editView.BindEnumProcessStatus(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumProcessStatus), this.ModuleConfig));
            }
        }
        #endregion

        #region 数据操作函数。
        ///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<FlowProcess>> handler)
		{
            IFlowProcessEditView editView = this.View as IFlowProcessEditView;
            if (editView != null && editView.ProcessID.IsValid)
            {
                FlowProcess data = new FlowProcess();
                data.ProcessID = editView.ProcessID;
                if (this.processEntity.LoadRecord(ref data))
                {
                    handler(this, new EntityEventArgs<FlowProcess>(data));
                }
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateData(FlowProcess data)
        {
            bool result = false;
            if (data != null)
            {
                result = this.processEntity.UpdateRecord(data);
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
                if (pri != null)
                {
                    result = this.processEntity.DeleteRecord(pri);
                }
            }
            catch (Exception e)
            {
                IFlowProcessListView listView = this.View as IFlowProcessListView;
                if (listView != null)
                    listView.ShowMessage(e.Message);
            }
            return result;
        }
        /// <summary>
        /// 上传数据处理。
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool UploadPorcess(Stream stream)
        {
            bool result = false;
            IFlowProcessImportView importView = this.View as IFlowProcessImportView;
            if (importView != null)
            {
                if (stream == null)
                    throw new Exception("导入流程数据不存在！");

                Process p = Process.DeSerializer(stream);
                if (p == null)
                    throw new Exception("上传数据不符合流程数据定义格式！");

                using (MemoryStream ms = new MemoryStream())
                {
                    p.Serializer(ms);
                    ms.Position = 0;

                    XmlDocument doc = new XmlDocument();
                    doc.Load(ms);
                    ms.Close();

                    importView.ProcessXml = doc.OuterXml;
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 导入流程数据。
        /// </summary>
        public bool ImportProcess()
        {
            bool result = false;
            IFlowProcessImportView importView = this.View as IFlowProcessImportView;
            if (importView != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(importView.ProcessXml);
                if(doc == null)
                    throw new Exception("导入流程数据不存在！");

                Process p = null;
                using(MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);
                    ms.Position = 0;

                    p = Process.DeSerializer(ms);
                    ms.Close();
                }
                if (p == null)
                    throw new Exception("上传数据不符合流程数据定义格式！");

                #region 流程信息。
                FlowProcess flowPorcess = new FlowProcess();
                flowPorcess.ProcessID = p.ProcessID;
                flowPorcess.ProcessName = p.ProcessName;
                flowPorcess.ProcessSign = p.ProcessSign;
                flowPorcess.ProcessDescription = p.ProcessDescription;
                flowPorcess.BeginDate = p.BeginDate;
                flowPorcess.EndDate = p.EndDate;
                flowPorcess.ProcessStatus = (int)EnumProcessStatus.Stop;
                #endregion

                #region 步骤信息。
                List<FlowStep> listFlowStep = new List<FlowStep>();
                List<FlowParameter> listFlowParameter = new List<FlowParameter>();
                List<FlowStepAuthorize> listFlowStepAuthorize = new List<FlowStepAuthorize>();
                List<FlowStepEmployee> listFlowStepEmployee = new List<FlowStepEmployee>();
                List<FlowStepPost> listFlowStepPost = new List<FlowStepPost>();
                List<FlowStepRank> listFlowStepRank = new List<FlowStepRank>();
                List<FlowStepRole> listFlowStepRole = new List<FlowStepRole>();
                foreach (Step s in p.Steps)
                {
                    #region 步骤信息。
                    FlowStep flowStep = new FlowStep();
                    flowStep.ProcessID = p.ProcessID;
                    flowStep.StepID = s.StepID;
                    flowStep.StepName = s.StepName;
                    flowStep.StepSign = s.StepSign;
                    flowStep.StepType = (int)s.StepType;
                    flowStep.StepMode = (int)s.StepMode;
                    flowStep.StepWarning = (int)s.StepWarning;
                    flowStep.StepDuration = s.StepDuration;
                    flowStep.StepDescription = s.StepDescription;
                    flowStep.EntryAction = s.EntryAction;
                    flowStep.EntryQuery = s.EntryQuery;
                    flowStep.OrderNo = s.OrderNo;

                    listFlowStep.Add(flowStep);
                    #endregion

                    #region 步骤参数。
                    foreach (Parameter param in s.Parameters)
                    {
                        FlowParameter flowParameter = new FlowParameter();
                        flowParameter.StepID = s.StepID;
                        flowParameter.ParameterID = param.ParameterID;
                        flowParameter.ParameterName = param.ParameterName;
                        flowParameter.ParameterType = (int)param.ParameterType;
                        flowParameter.DefaultValue = param.DefaultValue;
                        flowParameter.ParameterDescription = param.ParameterDescription;
                        listFlowParameter.Add(flowParameter);
                    }
                    #endregion

                    #region 步骤授权。
                    foreach (StepAuthorize sa in s.StepAuthorizes)
                    {
                        FlowStepAuthorize flowStepAuthorize = new FlowStepAuthorize();
                        flowStepAuthorize.StepID = s.StepID;
                        flowStepAuthorize.AuthorizeID = sa.AuthorizeID;
                        flowStepAuthorize.EmployeeID = sa.EmployeeID;
                        flowStepAuthorize.EmployeeName = sa.EmployeeName;
                        flowStepAuthorize.TargetEmployeeID = sa.EmployeeID;
                        flowStepAuthorize.TargetEmployeeName = sa.TargetEmployeeName;
                        flowStepAuthorize.BeginDate = sa.BeginDate;
                        flowStepAuthorize.EndDate = sa.EndDate;
                        listFlowStepAuthorize.Add(flowStepAuthorize);
                    }
                    #endregion

                    #region 步骤用户
                    foreach (StepEmployee se in s.StepEmployees)
                    {
                        FlowStepEmployee flowStepEmployee = new FlowStepEmployee();
                        flowStepEmployee.StepID = s.StepID;
                        flowStepEmployee.EmployeeID = se.EmployeeID;
                        flowStepEmployee.EmployeeName = se.EmployeeName;
                        listFlowStepEmployee.Add(flowStepEmployee);
                    }
                    #endregion

                    #region 步骤用户(岗位)
                    foreach (StepPost sp in s.StepPosts)
                    {
                        FlowStepPost flowStepPost = new FlowStepPost();
                        flowStepPost.StepID = s.StepID;
                        flowStepPost.PostID = sp.PostID;
                        flowStepPost.PostName = sp.PostName;
                        listFlowStepPost.Add(flowStepPost);
                    }
                    #endregion

                    #region 步骤用户(岗位级别)
                    foreach (StepRank sr in s.StepRanks)
                    {
                        FlowStepRank flowStepRank = new FlowStepRank();
                        flowStepRank.StepID = s.StepID;
                        flowStepRank.RankID = sr.RankID;
                        flowStepRank.RankName = sr.RankName;
                        listFlowStepRank.Add(flowStepRank);
                    }
                    #endregion

                    #region 步骤用户(角色)
                    foreach (StepRole role in s.StepRoles)
                    {
                        FlowStepRole flowStepRole = new FlowStepRole();
                        flowStepRole.StepID = s.StepID;
                        flowStepRole.RoleID = role.RoleID;
                        flowStepRole.RoleName = role.RoleName;
                        listFlowStepRole.Add(flowStepRole);
                    }
                    #endregion
                }
                #endregion

                #region 变迁规则
                List<FlowTransition> listFlowTransition = new List<FlowTransition>();
                List<FlowCondition> listFlowCondition = new List<FlowCondition>();
                List<FlowParameterMap> listFlowParameterMap = new List<FlowParameterMap>();
                foreach (Transition t in p.Transitions)
                {
                    #region 变迁规则
                    FlowTransition flowTransition = new FlowTransition();
                    flowTransition.ProcessID = p.ProcessID;
                    flowTransition.TransitionID = t.TransitionID;
                    flowTransition.FromStepID = t.FromStepID;
                    flowTransition.ToStepID = t.ToStepID;
                    flowTransition.TransitionRule = (int)t.TransitionRule;

                    listFlowTransition.Add(flowTransition);
                    #endregion

                    #region 变迁条件
                    foreach (Condition c in t.Conditions)
                    {
                        FlowCondition flowCondition = new FlowCondition();
                        flowCondition.TransitionID = t.TransitionID;
                        flowCondition.ConditionID = c.ConditionID;
                        flowCondition.ParameterID = c.ParameterID;
                        flowCondition.ConditionValue = (int)c.ConditionValue;
                        flowCondition.CompareValue = c.CompareValue;
                        listFlowCondition.Add(flowCondition);
                    }
                    #endregion

                    #region 参数映射
                    foreach (ParameterMap pm in t.Maps)
                    {
                        FlowParameterMap flowParameterMap = new FlowParameterMap();
                        flowParameterMap.TransitionID = t.TransitionID;
                        flowParameterMap.ParameterID = pm.ParameterID;
                        flowParameterMap.MapParameterID = pm.MapParameterID;
                        flowParameterMap.MapMode = (int)pm.MapMode;
                        flowParameterMap.AssemblyName = pm.AssemblyName;
                        flowParameterMap.ClassName = pm.ClassName;
                        flowParameterMap.EntryName = pm.EntryName;

                        listFlowParameterMap.Add(flowParameterMap);
                    }
                    #endregion
                }
                #endregion

                #region 数据处理。
                IDBAccess access = null;
                try
                {
                    FlowStepEntity flowStepEntity = new FlowStepEntity();
                    FlowParameterEntity flowParameterEntity = new FlowParameterEntity();
                    FlowStepAuthorizeEntity flowStepAuthorizeEntity = new FlowStepAuthorizeEntity();
                    FlowStepEmployeeEntity flowStepEmployeeEntity = new FlowStepEmployeeEntity();
                    FlowStepPostEntity flowStepPostEntity = new FlowStepPostEntity();
                    FlowStepRankEntity flowStepRankEntity = new FlowStepRankEntity();
                    FlowStepRoleEntity flowStepRoleEntity = new FlowStepRoleEntity();
                    FlowTransitionEntity flowTransitionEntity = new FlowTransitionEntity();
                    FlowConditionEntity flowConditionEntity = new FlowConditionEntity();
                    FlowParameterMapEntity flowParameterMapEntity = new FlowParameterMapEntity();

                    access = this.ModuleConfig.ModuleDefaultDatabase;
                    if (access.BeginTransaction())//开始事务
                    {
                        this.processEntity.DatabaseAccess = access;
                        flowStepEntity.DatabaseAccess = access;
                        flowParameterEntity.DatabaseAccess = access;
                        flowStepAuthorizeEntity.DatabaseAccess = access;
                        flowStepEmployeeEntity.DatabaseAccess = access;
                        flowStepPostEntity.DatabaseAccess = access;
                        flowStepRankEntity.DatabaseAccess = access;
                        flowStepRoleEntity.DatabaseAccess = access;
                        flowTransitionEntity.DatabaseAccess = access;
                        flowConditionEntity.DatabaseAccess = access;
                        flowParameterMapEntity.DatabaseAccess = access;

                        if (this.processEntity.UpdateRecord(flowPorcess))
                        {
                            //步骤。
                            foreach (FlowStep step in listFlowStep)
                            {
                                flowStepEntity.UpdateRecord(step);
                            }
                            //参数。
                            foreach (FlowParameter param in listFlowParameter)
                            {
                                flowParameterEntity.UpdateRecord(param);
                            }
                            //步骤授权
                            foreach (FlowStepAuthorize authorize in listFlowStepAuthorize)
                            {
                                flowStepAuthorizeEntity.UpdateRecord(authorize);
                            }
                            //步骤用户
                            foreach (FlowStepEmployee employee in listFlowStepEmployee)
                            {
                                flowStepEmployeeEntity.UpdateRecord(employee);
                            }
                            //步骤用户(岗位)
                            foreach (FlowStepPost post in listFlowStepPost)
                            {
                                flowStepPostEntity.UpdateRecord(post);
                            }
                            //步骤用户(岗位级别)
                            foreach (FlowStepRank rank in listFlowStepRank)
                            {
                                flowStepRankEntity.UpdateRecord(rank);
                            }
                            //步骤用户(角色)
                            foreach (FlowStepRole role in listFlowStepRole)
                            {
                                flowStepRoleEntity.UpdateRecord(role);
                            }
                            //变迁规则
                            foreach (FlowTransition t in listFlowTransition)
                            {
                                flowTransitionEntity.UpdateRecord(t);
                            }
                            //变迁条件
                            foreach (FlowCondition c in listFlowCondition)
                            {
                                flowConditionEntity.UpdateRecord(c);
                            }
                            //参数映射
                            foreach (FlowParameterMap map in listFlowParameterMap)
                            {
                                flowParameterMapEntity.UpdateRecord(map);
                            }
                        }
                        result = access.CommitTransaction();//提交事务
                    }
                }
                catch (Exception e)
                {
                    if (access != null)
                        access.RollbackTransaction();//回滚事务
                    throw e;
                }
                #endregion
            }
            return result;
        }
        /// <summary>
        /// 导出流程数据。
        /// </summary>
        /// <returns></returns>
        public XmlDocument ExportProcess()
        {
            XmlDocument doc = null;
            IFlowProcessExportView exportView = this.View as IFlowProcessExportView;
            if (exportView != null && exportView.ProcessID.IsValid)
            {
                Process p = ModuleUtils.CreateProcess(exportView.ProcessID);
                if (p != null)
                {
                    doc = new XmlDocument();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        p.Serializer(ms);
                        ms.Position = 0;
                        doc.Load(ms);
                        ms.Close();
                    }
                }
            }
            return doc;
        }
        /// <summary>
        /// 获取当前流程名称。
        /// </summary>
        public string GetProcessName(GUIDEx processID)
        {
            if (processID.IsValid)
                return this.processEntity.FindProcessName(processID);
            return string.Empty;
        }
        /// <summary>
        /// 改变流程状态。
        /// </summary>
        /// <returns></returns>
        public bool ChangeProcessStatus()
        {
            bool result = false;
            IFlowProcessEditView editView = this.View as IFlowProcessEditView;
            if (editView != null)
            {
                try
                {
                    FlowProcess data = new FlowProcess();
                    data.ProcessID = editView.ProcessID;
                    if (this.processEntity.LoadRecord(ref data))
                    {
                        EnumProcessStatus status = (EnumProcessStatus)data.ProcessStatus;
                        if (status == EnumProcessStatus.Start)
                        {
                            //禁用。
                            data.ProcessStatus = (int)EnumProcessStatus.Stop;
                            result = this.processEntity.UpdateRecord(data);
                        }
                        else
                        {
                            //启用。
                            Process p = ModuleUtils.CreateProcess(data.ProcessID);
                            if (p != null)
                            {
                                string strVerify = string.Empty;
                                string strSerialization = Utils.SerializerDatabaseFormart<Process>(p, out strVerify);
                                FlowProcessSerializationEntity serializationEntity = new FlowProcessSerializationEntity();
                                FlowProcessSerialization serialization = new FlowProcessSerialization();
                                serialization.ProcessID = data.ProcessID;
                                result = serializationEntity.LoadRecord(ref serialization);
                                if (false == (result = string.Equals(serialization.Verify, strVerify, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    serialization.Serialization = strSerialization;
                                    serialization.Verify = strVerify;
                                    result = serializationEntity.UpdateRecord(serialization);
                                }
                            }
                            if (result)
                            {
                                data.ProcessStatus = (int)EnumProcessStatus.Start;
                                result = this.processEntity.UpdateRecord(data);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    editView.ShowMessage(e.Message);
                }
            }
            return result;
        }
		#endregion

	}

}
