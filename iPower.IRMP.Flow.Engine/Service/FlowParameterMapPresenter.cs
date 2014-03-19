//================================================================================
// FileName: FlowParameterMapPresenter.cs
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
	/// IFlowParameterMapView�ӿڡ�
	///</summary>
	public interface IFlowParameterMapView: IModuleView
	{
        /// <summary>
        /// ��ȡ��������ID��
        /// </summary>
        GUIDEx ProcessID { get; }
        /// <summary>
        /// ��ȡ������Ǩ����ID��
        /// </summary>
        GUIDEx TransitionID { get; }
        /// <summary>
        /// ���������̡�
        /// </summary>
        /// <param name="data"></param>
        void BindProcess(IListControlsData data);
        /// <summary>
        /// ��������Ǩ����
        /// </summary>
        /// <param name="data"></param>
        void BindTransition(IListControlsData data);
	}
    /// <summary>
    /// 
    /// </summary>
    public interface IFlowParameterMapListView : IFlowParameterMapView
    {
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string ParameterName { get; }
        
    }

    public interface IFlowParameterMapEditView : IFlowParameterMapView
    {
        /// <summary>
        /// ��ȡ����ӳ��ID��
        /// </summary>
        GUIDEx[] ParameterMapID { get; }
        /// <summary>
        /// �󶨲���ӳ��ģʽ��
        /// </summary>
        /// <param name="data"></param>
        void BindEnumMapMode(IListControlsData data);
        /// <summary>
        /// �󶨲������ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindParameterData(IListControlsData data);
        /// <summary>
        /// ��ӳ��������ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindMapParameterData(IListControlsData data);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
    }
		
	///<summary>
	/// FlowParameterMapPresenter��Ϊ�ࡣ
	///</summary>
	public class FlowParameterMapPresenter: ModulePresenter<IFlowParameterMapView>
	{
		#region ��Ա���������캯����
        FlowParameterMapEntity parameterMapEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public FlowParameterMapPresenter(IFlowParameterMapView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.ParameterMap_ModuleID;
            this.parameterMapEntity = new FlowParameterMapEntity();
		}
		#endregion

        #region ���ԡ�
        /// <summary>
        /// �б����ݡ�
        /// </summary>
        public DataTable ListViewDataSource
        {
            get
            {
                IFlowParameterMapListView listView = this.View as IFlowParameterMapListView;
                if (listView != null)
                {
                    DataTable dtSource = this.parameterMapEntity.ListDataSource(listView.ProcessID, listView.TransitionID, listView.ParameterName);
                    dtSource.Columns.Add("MapModeName", typeof(String));
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["MapModeName"] = this.GetEnumMemberName(typeof(EnumMapMode), Convert.ToInt32(row["MapMode"]));
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
            IFlowParameterMapView mapView = this.View as IFlowParameterMapView;
            if (mapView != null)
            {
                mapView.BindProcess(new FlowProcessEntity().BindProcess());
                mapView.BindTransition(new FlowTransitionEntity().BindTransition(mapView.ProcessID));
            }

            IFlowParameterMapEditView editView = this.View as IFlowParameterMapEditView;
            if (editView != null)
            {
                editView.BindEnumMapMode(new ConstListControlsDataSource<ModuleConfiguration>(typeof(EnumMapMode), this.ModuleConfig));
                this.ChangeTransition();
            }
        }
        #endregion

        #region ���ݲ���������
        /// <summary>
        /// �ı��������̡�
        /// </summary>
        public void ChangeProcess()
        {
            IFlowParameterMapView mapView = this.View as IFlowParameterMapView;
            if (mapView != null)
            {
                mapView.BindTransition(new FlowTransitionEntity().BindTransition(mapView.ProcessID));
            }
        }
        /// <summary>
        /// �ı��Ǩ����
        /// </summary>
        public void ChangeTransition()
        {
            IFlowParameterMapEditView editView = this.View as IFlowParameterMapEditView;
            if (editView != null)
            {
                FlowTransitionEntity entity = new FlowTransitionEntity();
                editView.BindParameterData(entity.BindFromStepParameterData(editView.TransitionID));
                editView.BindMapParameterData(entity.BindToStepParameterData(editView.TransitionID));
            }
        }
        ///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<FlowParameterMap>> handler)
		{
            IFlowParameterMapEditView editView = this.View as IFlowParameterMapEditView;
            if (editView != null && editView.ParameterMapID != null && editView.ParameterMapID.Length == 3)
            {
                FlowParameterMap data = new FlowParameterMap();
                data.TransitionID = editView.ParameterMapID[0];
                data.ParameterID = editView.ParameterMapID[1];
                data.MapParameterID = editView.ParameterMapID[2];

                FlowTransition flowTransition = new FlowTransition();
                flowTransition.TransitionID = data.TransitionID;
                FlowTransitionEntity flowTransitionEntity = new FlowTransitionEntity();
                flowTransitionEntity.LoadRecord(ref flowTransition);

                if (this.parameterMapEntity.LoadRecord(ref data))
                {
                    handler(flowTransition.ProcessID, new EntityEventArgs<FlowParameterMap>(data));
                }

            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateData(FlowParameterMap data)
        {
            return this.parameterMapEntity.UpdateRecord(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pri"></param>
        /// <returns></returns>
        public bool BatchDeleteData(StringCollection pri)
        {
            return this.parameterMapEntity.DeleteRecord(pri);
        }
        #endregion

	}

}
