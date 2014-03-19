//================================================================================
// FileName: FlowStep.cs
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
	///tblFlowStep映射类。
	///</summary>
	[DbTable("tblFlowStep")]
	public class FlowStep
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowStep()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置StepID。
		///</summary>
		[DbField("StepID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	StepID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepSign。
		///</summary>
		[DbField("StepSign")]
		public	GUIDEx	StepSign
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepName。
		///</summary>
		[DbField("StepName")]
		public	string	StepName
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
			
		///<summary>
		///获取或设置StepType。
		///</summary>
		[DbField("StepType")]
		public	int	StepType
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EntryAction。
		///</summary>
		[DbField("EntryAction")]
		public	string	EntryAction
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EntryQuery。
		///</summary>
		[DbField("EntryQuery")]
		public	string	EntryQuery
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepMode。
		///</summary>
		[DbField("StepMode")]
		public	int	StepMode
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepDuration。
		///</summary>
		[DbField("StepDuration")]
		public	int	StepDuration
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepWarning。
		///</summary>
		[DbField("StepWarning")]
		public	int	StepWarning
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepDescription。
		///</summary>
		[DbField("StepDescription")]
		public	string	StepDescription
		{
			get;set;

		}
        /// <summary>
        /// 获取或设置排序字段。
        /// </summary>
        [DbField("OrderNo")]
        public int OrderNo
        {
            get;
            set;
        }
		#endregion

	}

}
