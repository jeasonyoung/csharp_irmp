//================================================================================
// FileName: FlowStepPresenter.cs
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
	/// ���趨��ӿڡ�
	///</summary>
	public interface IFlowStepView: IModuleView
	{
        /// <summary>
        /// �����̡�
        /// </summary>
        /// <param name="data"></param>
        void BindProcess(IListControlsData data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
	}
    /// <summary>
    /// �����б�ҳ��
    /// </summary>
    public interface IFlowStepListView : IFlowStepView
    {
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string StepName { get; }
        /// <summary>
        /// ��ȡ��������ID��
        /// </summary>
        string ProcessID { get; }
    }
    /// <summary>
    /// ����༭ҳ��ӿڡ�
    /// </summary>
    public interface IFlowStepEditView : IFlowStepView
    {
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx StepID { get; }

        string[] GetEmployeeID{ get; }
        string[] GetEmployeeName { get; }
        string[] GetRoleID { get; }
        string[] GetRoleName { get; }
        string[] GetRankID { get; }
        string[] GetRankName { get; }
        string[] GetPostID { get; }
        string[] GetPostName { get; }

        void SetEmployee(string[] employeeID, string[] employeeName);
        void SetRole(string[] roleID, string[] roleName);
        void SetRank(string[] rankID, string[] rankName);
        void SetPost(string[] postID, string[] postName);
        /// <summary>
        /// �󶨲������͡�
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindEnumStepType(IListControlsData data);
        /// <summary>
        /// �󶨲���ģʽ��
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindEnumStepMode(IListControlsData data);
        /// <summary>
        /// ��֪ͨģʽ��
        /// </summary>
        /// <param name="data"></param>
        void BindEnumStepWarning(IListControlsData data);
    }
		
	///<summary>
    /// ���趨����Ϊ�ࡣ
	///</summary>
	public class FlowStepPresenter: ModulePresenter<IFlowStepView>
	{
		#region ��Ա���������캯����
        FlowStepEntity flowStepEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public FlowStepPresenter(IFlowStepView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Step_ModuleID;
            this.flowStepEntity = new FlowStepEntity();
		}
		#endregion

        #region ���ԡ�
        public DataTable ListDataSource
        {
            get
            {
                IFlowStepListView listView = this.View as IFlowStepListView;
                if (listView != null)
                {
                    DataTable dtSource = this.flowStepEntity.ListDataSource(listView.ProcessID, listView.StepName);
                    dtSource.Columns.Add("StepTypeName");
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["StepTypeName"] = this.GetEnumMemberName(typeof(EnumStepType), Convert.ToInt32(row["StepType"]));
                    }
                    return dtSource;
                }
                return null;
            }
        }
        #endregion

        #region ���ء�
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            if (this.View != null)
            {
                this.View.BindProcess(new FlowProcessEntity().BindProcess());
            }
            IFlowStepEditView editView = this.View as IFlowStepEditView;
            if (editView != null)
            {
                editView.BindEnumStepType(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumStepType), this.ModuleConfig));
                editView.BindEnumStepMode(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumStepMode), this.ModuleConfig));
                editView.BindEnumStepWarning(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumStepWarning), new int[] { 0 }, this.ModuleConfig));
            }
        }
        #endregion

        #region ���ݲ���������
        ///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<FlowStep>> handler)
		{
            IFlowStepEditView editView = this.View as IFlowStepEditView;
            if (editView != null && !string.IsNullOrEmpty(editView.StepID))
            {
                FlowStep data = new FlowStep();
                data.StepID = editView.StepID;
                if (this.flowStepEntity.LoadRecord(ref data))
                {
                    handler(this, new EntityEventArgs<FlowStep>(data));

                    string[] outID = null, outName = null;

                    FlowStepEmployeeEntity employeeEntity = new FlowStepEmployeeEntity();
                    if (employeeEntity.LoadFlowStepEmployee(editView.StepID, out outID, out outName))
                        editView.SetEmployee(outID, outName);

                    FlowStepRoleEntity roleEntity = new FlowStepRoleEntity();
                    if (roleEntity.LoadFlowStepRole(editView.StepID, out outID, out outName))
                        editView.SetRole(outID, outName);

                    FlowStepRankEntity rankEntity = new FlowStepRankEntity();
                    if (rankEntity.LoadFlowStepRank(editView.StepID, out outID, out outName))
                        editView.SetRank(outID, outName);

                    FlowStepPostEntity postEntity = new FlowStepPostEntity();
                    if (postEntity.LoadFlowStepPost(editView.StepID, out outID, out outName))
                        editView.SetPost(outID, outName);
                }
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateData(FlowStep data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    result = this.flowStepEntity.UpdateRecord(data);
                    IFlowStepEditView editView = this.View as IFlowStepEditView;
                    if (result && editView != null)
                    {
                        //Employee
                        new FlowStepEmployeeEntity().DeleteAllAndInsert(data.StepID, editView.GetEmployeeID, editView.GetEmployeeName);
                        //Role
                        new FlowStepRoleEntity().DeleteAllAndInsert(data.StepID, editView.GetRoleID, editView.GetRoleName);
                        //Rank
                        new FlowStepRankEntity().DeleteAllAndInsert(data.StepID, editView.GetRankID, editView.GetRankName);
                        //Post
                        new FlowStepPostEntity().DeleteAllAndInsert(data.StepID, editView.GetPostID, editView.GetPostName);
                    }
                }
            }
            catch (Exception e)
            {
                IFlowStepEditView editView = this.View as IFlowStepEditView;
                if (editView != null)
                    editView.ShowMessage(e.Message);
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
                    result = this.flowStepEntity.DeleteRecord(pri);
            }
            catch (Exception e)
            {
                IFlowStepListView listView = this.View as IFlowStepListView;
                if (listView != null)
                    listView.ShowMessage(e.Message);
            }
            return result;
        }
		#endregion

	}

}
