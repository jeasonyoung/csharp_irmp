//================================================================================
// FileName: FlowProcessPresenter.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	/// IFlowProcessView�ӿڡ�
	///</summary>
	public interface IFlowProcessView: IModuleView
	{

	}
    /// <summary>
    /// �б�ҳ��ӿڡ�
    /// </summary>
    public interface IFlowProcessListView : IFlowProcessView
    {
        /// <summary>
        /// ��ȡ�������ơ�
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
    /// �༭ҳ��ӿڡ�
    /// </summary>
    public interface IFlowProcessEditView : IFlowProcessView
    {
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx ProcessID
        {
            get;
        }
        /// <summary>
        /// �󶨲������͡�
        /// </summary>
        /// <param name="data"></param>
        void BindEnumProcessStatus(IListControlsData data);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// ���ݵ��롣
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
    /// ���ݵ�����
    /// </summary>
    public interface IFlowProcessExportView : IFlowProcessView
    {
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx ProcessID
        {
            get;
        }
    }
	///<summary>
	/// FlowProcessPresenter��Ϊ�ࡣ
	///</summary>
	public class FlowProcessPresenter: ModulePresenter<IFlowProcessView>
	{
		#region ��Ա���������캯����
        FlowProcessEntity processEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public FlowProcessPresenter(IFlowProcessView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Process_ModuleID;
            this.processEntity = new FlowProcessEntity();
		}
		#endregion

        #region ���ԡ�
        /// <summary>
        /// �б�����Դ��
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
                        row["FlowProcessChart"] = "����ͼ";
                        row["ProcessStatusName"] = this.GetEnumMemberName(typeof(EnumProcessStatus), Convert.ToInt32(row["ProcessStatus"]));
                    }
                    return dtSource;
                }
                return null;
            }
        }
        #endregion

        #region ���ء�
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

        #region ���ݲ���������
        ///<summary>
		///�༭ҳ��������ݡ�
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
        /// �������ݡ�
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
        /// ����ɾ�����ݡ�
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
        /// �ϴ����ݴ���
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
                    throw new Exception("�����������ݲ����ڣ�");

                Process p = Process.DeSerializer(stream);
                if (p == null)
                    throw new Exception("�ϴ����ݲ������������ݶ����ʽ��");

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
        /// �����������ݡ�
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
                    throw new Exception("�����������ݲ����ڣ�");

                Process p = null;
                using(MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);
                    ms.Position = 0;

                    p = Process.DeSerializer(ms);
                    ms.Close();
                }
                if (p == null)
                    throw new Exception("�ϴ����ݲ������������ݶ����ʽ��");

                #region ������Ϣ��
                FlowProcess flowPorcess = new FlowProcess();
                flowPorcess.ProcessID = p.ProcessID;
                flowPorcess.ProcessName = p.ProcessName;
                flowPorcess.ProcessSign = p.ProcessSign;
                flowPorcess.ProcessDescription = p.ProcessDescription;
                flowPorcess.BeginDate = p.BeginDate;
                flowPorcess.EndDate = p.EndDate;
                flowPorcess.ProcessStatus = (int)EnumProcessStatus.Stop;
                #endregion

                #region ������Ϣ��
                List<FlowStep> listFlowStep = new List<FlowStep>();
                List<FlowParameter> listFlowParameter = new List<FlowParameter>();
                List<FlowStepAuthorize> listFlowStepAuthorize = new List<FlowStepAuthorize>();
                List<FlowStepEmployee> listFlowStepEmployee = new List<FlowStepEmployee>();
                List<FlowStepPost> listFlowStepPost = new List<FlowStepPost>();
                List<FlowStepRank> listFlowStepRank = new List<FlowStepRank>();
                List<FlowStepRole> listFlowStepRole = new List<FlowStepRole>();
                foreach (Step s in p.Steps)
                {
                    #region ������Ϣ��
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

                    #region ���������
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

                    #region ������Ȩ��
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

                    #region �����û�
                    foreach (StepEmployee se in s.StepEmployees)
                    {
                        FlowStepEmployee flowStepEmployee = new FlowStepEmployee();
                        flowStepEmployee.StepID = s.StepID;
                        flowStepEmployee.EmployeeID = se.EmployeeID;
                        flowStepEmployee.EmployeeName = se.EmployeeName;
                        listFlowStepEmployee.Add(flowStepEmployee);
                    }
                    #endregion

                    #region �����û�(��λ)
                    foreach (StepPost sp in s.StepPosts)
                    {
                        FlowStepPost flowStepPost = new FlowStepPost();
                        flowStepPost.StepID = s.StepID;
                        flowStepPost.PostID = sp.PostID;
                        flowStepPost.PostName = sp.PostName;
                        listFlowStepPost.Add(flowStepPost);
                    }
                    #endregion

                    #region �����û�(��λ����)
                    foreach (StepRank sr in s.StepRanks)
                    {
                        FlowStepRank flowStepRank = new FlowStepRank();
                        flowStepRank.StepID = s.StepID;
                        flowStepRank.RankID = sr.RankID;
                        flowStepRank.RankName = sr.RankName;
                        listFlowStepRank.Add(flowStepRank);
                    }
                    #endregion

                    #region �����û�(��ɫ)
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

                #region ��Ǩ����
                List<FlowTransition> listFlowTransition = new List<FlowTransition>();
                List<FlowCondition> listFlowCondition = new List<FlowCondition>();
                List<FlowParameterMap> listFlowParameterMap = new List<FlowParameterMap>();
                foreach (Transition t in p.Transitions)
                {
                    #region ��Ǩ����
                    FlowTransition flowTransition = new FlowTransition();
                    flowTransition.ProcessID = p.ProcessID;
                    flowTransition.TransitionID = t.TransitionID;
                    flowTransition.FromStepID = t.FromStepID;
                    flowTransition.ToStepID = t.ToStepID;
                    flowTransition.TransitionRule = (int)t.TransitionRule;

                    listFlowTransition.Add(flowTransition);
                    #endregion

                    #region ��Ǩ����
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

                    #region ����ӳ��
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

                #region ���ݴ���
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
                    if (access.BeginTransaction())//��ʼ����
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
                            //���衣
                            foreach (FlowStep step in listFlowStep)
                            {
                                flowStepEntity.UpdateRecord(step);
                            }
                            //������
                            foreach (FlowParameter param in listFlowParameter)
                            {
                                flowParameterEntity.UpdateRecord(param);
                            }
                            //������Ȩ
                            foreach (FlowStepAuthorize authorize in listFlowStepAuthorize)
                            {
                                flowStepAuthorizeEntity.UpdateRecord(authorize);
                            }
                            //�����û�
                            foreach (FlowStepEmployee employee in listFlowStepEmployee)
                            {
                                flowStepEmployeeEntity.UpdateRecord(employee);
                            }
                            //�����û�(��λ)
                            foreach (FlowStepPost post in listFlowStepPost)
                            {
                                flowStepPostEntity.UpdateRecord(post);
                            }
                            //�����û�(��λ����)
                            foreach (FlowStepRank rank in listFlowStepRank)
                            {
                                flowStepRankEntity.UpdateRecord(rank);
                            }
                            //�����û�(��ɫ)
                            foreach (FlowStepRole role in listFlowStepRole)
                            {
                                flowStepRoleEntity.UpdateRecord(role);
                            }
                            //��Ǩ����
                            foreach (FlowTransition t in listFlowTransition)
                            {
                                flowTransitionEntity.UpdateRecord(t);
                            }
                            //��Ǩ����
                            foreach (FlowCondition c in listFlowCondition)
                            {
                                flowConditionEntity.UpdateRecord(c);
                            }
                            //����ӳ��
                            foreach (FlowParameterMap map in listFlowParameterMap)
                            {
                                flowParameterMapEntity.UpdateRecord(map);
                            }
                        }
                        result = access.CommitTransaction();//�ύ����
                    }
                }
                catch (Exception e)
                {
                    if (access != null)
                        access.RollbackTransaction();//�ع�����
                    throw e;
                }
                #endregion
            }
            return result;
        }
        /// <summary>
        /// �����������ݡ�
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
        /// ��ȡ��ǰ�������ơ�
        /// </summary>
        public string GetProcessName(GUIDEx processID)
        {
            if (processID.IsValid)
                return this.processEntity.FindProcessName(processID);
            return string.Empty;
        }
        /// <summary>
        /// �ı�����״̬��
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
                            //���á�
                            data.ProcessStatus = (int)EnumProcessStatus.Stop;
                            result = this.processEntity.UpdateRecord(data);
                        }
                        else
                        {
                            //���á�
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
