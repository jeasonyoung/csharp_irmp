//================================================================================
// FileName: FlowParameterMapPresenter.cs
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
	/// IFlowParameterMapView接口。
	///</summary>
	public interface IFlowParameterMapView: IModuleView
	{
        /// <summary>
        /// 获取所属流程ID。
        /// </summary>
        GUIDEx ProcessID { get; }
        /// <summary>
        /// 获取所属变迁规则ID。
        /// </summary>
        GUIDEx TransitionID { get; }
        /// <summary>
        /// 绑定所属流程。
        /// </summary>
        /// <param name="data"></param>
        void BindProcess(IListControlsData data);
        /// <summary>
        /// 绑定所属变迁规则。
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
        /// 获取参数名称。
        /// </summary>
        string ParameterName { get; }
        
    }

    public interface IFlowParameterMapEditView : IFlowParameterMapView
    {
        /// <summary>
        /// 获取参数映射ID。
        /// </summary>
        GUIDEx[] ParameterMapID { get; }
        /// <summary>
        /// 绑定参数映射模式。
        /// </summary>
        /// <param name="data"></param>
        void BindEnumMapMode(IListControlsData data);
        /// <summary>
        /// 绑定参数数据。
        /// </summary>
        /// <param name="data"></param>
        void BindParameterData(IListControlsData data);
        /// <summary>
        /// 绑定映射参数数据。
        /// </summary>
        /// <param name="data"></param>
        void BindMapParameterData(IListControlsData data);
        /// <summary>
        /// 显示信息。
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
    }
		
	///<summary>
	/// FlowParameterMapPresenter行为类。
	///</summary>
	public class FlowParameterMapPresenter: ModulePresenter<IFlowParameterMapView>
	{
		#region 成员变量，构造函数。
        FlowParameterMapEntity parameterMapEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public FlowParameterMapPresenter(IFlowParameterMapView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.ParameterMap_ModuleID;
            this.parameterMapEntity = new FlowParameterMapEntity();
		}
		#endregion

        #region 属性。
        /// <summary>
        /// 列表数据。
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

        #region 重载。
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

        #region 数据操作函数。
        /// <summary>
        /// 改变所属流程。
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
        /// 改变变迁规则。
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
		///编辑页面加载数据。
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
        /// 更新数据。
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
