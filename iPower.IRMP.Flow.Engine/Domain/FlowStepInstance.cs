//================================================================================
// FileName: FlowStepInstance.cs
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
using System.Text;
	
using iPower;
using iPower.Data;
using iPower.Data.ORM;
namespace iPower.IRMP.Flow.Engine.Domain
{
	///<summary>
	///tblFlowStepInstance映射类。
	///</summary>
	[DbTable("tblFlowStepInstance")]
	public class FlowStepInstance
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowStepInstance()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置StepInstanceID。
		///</summary>
		[DbField("StepInstanceID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	StepInstanceID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ProcessInstanceID。
		///</summary>
		[DbField("ProcessInstanceID")]
		public	GUIDEx	ProcessInstanceID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepID。
		///</summary>
		[DbField("StepID")]
		public	GUIDEx	StepID
		{
			get;set;

		}
        /// <summary>
        /// 获取或设置StepName。
        /// </summary>
        [DbField("StepName")]
        public string StepName
        {
            get;
            set;
        }
			
		///<summary>
		///获取或设置FromEmployeeID。
		///</summary>
		[DbField("FromEmployeeID")]
		public	GUIDEx	FromEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置FromEmployeeName。
		///</summary>
		[DbField("FromEmployeeName")]
		public	string	FromEmployeeName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置CreateDate。
		///</summary>
		[DbField("CreateDate")]
		public	DateTime	CreateDate
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EndDate。
		///</summary>
        [DbField("EndDate", DbFieldUsage.EmptyOrNullNotUpdate)]
        public DateTime? EndDate
        {
            get;
            set;

        }
			
		///<summary>
		///获取或设置InstanceStepStatus。
		///</summary>
		[DbField("InstanceStepStatus")]
		public	int	InstanceStepStatus
		{
			get;set;

		}
			
		#endregion

	}

}
