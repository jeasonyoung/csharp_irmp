//================================================================================
// FileName: FlowProcessInstance.cs
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
	///tblFlowProcessInstance映射类。
	///</summary>
	[DbTable("tblFlowProcessInstance")]
	public class FlowProcessInstance
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowProcessInstance()
		{

		}
		#endregion
		#region 属性。
       
		///<summary>
		///获取或设置ProcessInstanceID。
		///</summary>
		[DbField("ProcessInstanceID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ProcessInstanceID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ProcessInstanceName。
		///</summary>
		[DbField("ProcessInstanceName")]
		public	string	ProcessInstanceName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ProcessID。
		///</summary>
		[DbField("ProcessID")]
		public	GUIDEx	ProcessID
		{
			get;set;

		}
        /// <summary>
        /// 获取或设置流程名称。
        /// </summary>
        [DbField("ProcessName")]
        public string ProcessName
        {
            get;
            set;
        }
		///<summary>
		///获取或设置ProcessSerialization。
		///</summary>
		[DbField("ProcessSerialization")]
		public	string	ProcessSerialization
		{
			get;set;

		}
			
		///<summary>
		///获取或设置Verify。
		///</summary>
		[DbField("Verify")]
		public	string	Verify
		{
			get;set;

		}
			
		///<summary>
		///获取或设置CreateDate。
		///</summary>
		[DbField("CreateDate")]
		public	DateTime CreateDate
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EndDate。
		///</summary>
		[DbField("EndDate", DbFieldUsage.EmptyOrNullNotUpdate)]
		public	DateTime ? EndDate
		{
			get;set;

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
		///获取或设置InstanceProcessStatus。
		///</summary>
		[DbField("InstanceProcessStatus")]
		public	int	InstanceProcessStatus
		{
			get;set;

		}
			
		#endregion

	}

}
