--*******************************************************************************************************************************************************--
--	流程引擎 数据库脚本
--*******************************************************************************************************************************************************--
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--外键约束
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowCondition')
begin
	print 'drop table tblFlowCondition'
	drop table tblFlowCondition
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowParameterMap')
begin
	print 'drop table tblFlowParameterMap'
	drop table tblFlowParameterMap
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowTransition')
begin
	print 'drop table tblFlowTransition'
	drop table tblFlowTransition
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowParameter')
begin
	print 'drop table tblFlowParameter'
	drop table tblFlowParameter
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepRole')
begin
	print 'drop table tblFlowStepRole'
	drop table tblFlowStepRole
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepPost')
begin
	print 'drop table tblFlowStepPost'
	drop table tblFlowStepPost
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepRank')
begin
	print 'drop table tblFlowStepRank'
	drop table tblFlowStepRank
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepEmployee')
begin
	print 'drop table tblFlowStepEmployee'
	drop table tblFlowStepEmployee
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepAuthorize')
begin
	print 'drop table tblFlowStepAuthorize'
	drop table tblFlowStepAuthorize
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStep')
begin
	print 'drop table tblFlowStep'
	drop table tblFlowStep
end
go
if exists(select 0 from sysobjects where xtype  = 'u' and name = 'tblFlowProcessSerialization')
begin
	print 'drop table tblFlowProcessSerialization'
	drop table tblFlowProcessSerialization
end
go
--流程实例。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowParameterInstance')
begin
	print 'drop table tblFlowParameterInstance'
	drop table tblFlowParameterInstance
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowInstanceTask')
begin
	print 'drop table tblFlowInstanceTask'
	drop table tblFlowInstanceTask
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepInstanceData')
begin
	print 'drop table tblFlowStepInstanceData'
	drop table tblFlowStepInstanceData
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowInstanceRunError')
begin
	print 'drop table tblFlowInstanceRunError'
	drop table tblFlowInstanceRunError
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepInstance')
begin 
	print 'drop table tblFlowStepInstance'
	drop table tblFlowStepInstance
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
---流程定义表
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowProcess')
begin 
	print 'drop table tblFlowProcess'
	drop table tblFlowProcess
end
go
	print 'create table tblFlowProcess'
go
create table tblFlowProcess
(
	ProcessID		GUIDEx, --流程ID。
	ProcessSign		GUIDEx,	--流程的唯一标识。
	ProcessName		nvarchar(256),--流程名称。
	ProcessStatus	int	default(0),--流程状态，0-表示禁用，1-表示启用。
	
	BeginDate		datetime default(getdate()),--有效期开始时间。
	EndDate			datetime default('9999-12-31 23:59:59'),--有效期结束时间。
	
	ProcessDescription	nvarchar(1024),--流程描述信息。
	
	constraint PK_tblFlowProcess_ProcessID primary key(ProcessID),
	constraint UK_tblFlowProcess_ProcessSign unique(ProcessSign)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤定义表
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStep')
begin
	print 'drop table tblFlowStep'
	drop table tblFlowStep
end
go
	print 'create table tblFlowStep'
go
create table tblFlowStep
(
	StepID			GUIDEx, --步骤ID。
	StepSign		GUIDEx, --步骤标识。
	StepName		nvarchar(256),--步骤名称。
	ProcessID		GUIDEx, --流程ID。
	
	StepType		int,--步骤类型， 位表示法，1-开始步骤，2-中间步骤，4-终结步骤。
	EntryAction		nvarchar(1024),--该步骤的运行入口方法或者URL。
	EntryQuery		nvarchar(1024),--该步骤的查看入口方法或者URL。
	
	StepMode		int,--步骤模式， 位表示法，1-与分支，2-或分支，4-与汇聚，8-或汇聚。
	
	StepDuration	int,--步骤的变迁周期，以秒为单位。
	StepWarning		int	default(0),--步骤通知消息类型，位操作：0-未定义，1-短信，2-邮件，4-站内消息。
	StepDescription	nvarchar(1024),--步骤描述信息。
	OrderNo			int default(0),--排序字段。
	
	constraint PK_tblFlowStep_StepID primary key(StepID),
	constraint UK_tblFlowStep_StepSign unique(StepSign),
	constraint FK_tblFlowStep_tblFlowProcess_ProcessID foreign key(ProcessID) references tblFlowProcess(ProcessID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤参数定义表
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowParameter')
begin
	print 'drop table tblFlowParameter'
	drop table tblFlowParameter
end
go
	print 'create table tblFlowParameter'
go
create table tblFlowParameter
(
	ParameterID				GUIDEx,--参数定义ID。
	StepID					GUIDEx, --步骤ID。
	ParameterName			nvarchar(64),--参数名称。
	ParameterType			int	default(0),--参数类型，0-字符串，1-整型。
	DefaultValue			nvarchar(256),--参数默认值。
	ParameterDescription	nvarchar(256),--参数描述信息。
	
	constraint PK_tblFlowParameter_ParameterID primary key(ParameterID),
	constraint FK_tblFlowParameter_tblFlowStep_StepID foreign key(StepID) references tblFlowStep(StepID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--步骤变迁规则定义表(图的邻接表表示法)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowTransition')
begin
	print 'drop table tblFlowTransition'
	drop table tblFlowTransition
end
go
	print 'create table tblFlowTransition'
go
create table tblFlowTransition
(
	TransitionID	GUIDEx,--变迁规则ID。
	ProcessID		GUIDEx, --流程ID。
	FromStepID		GUIDEx,--前驱步骤ID。
	ToStepID		GUIDEx,--后续步骤ID。
	TransitionRule	int	default(1),--变迁规则，条件组织规则，1-与，2-或。
	
	constraint PK_tblFlowTransition_TransitionID primary key(TransitionID),
	constraint PK_tblFlowTransition_tblFlowProcess_ProcessID foreign key(ProcessID) references tblFlowProcess(ProcessID),
	constraint FK_tblFlowTransition_tblFlowStep_FromStepID foreign key(FromStepID) references tblFlowStep(StepID),
	constraint FK_tblFlowTransition_tblFlowStep_ToStepID foreign key(ToStepID) references tblFlowStep(StepID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--步骤变迁条件定义表(用参数的值判定)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowCondition')
begin
	print 'drop table tblFlowCondition'
	drop table tblFlowCondition
end
go
	print 'create table tblFlowCondition'
go
create table tblFlowCondition
(
	ConditionID		GUIDEx,--条件ID。
	TransitionID	GUIDEx,--变迁规则ID。
	ParameterID		GUIDEx,--参数定义ID。
	CompareValue	nvarchar(1024),--比较的值。
	ConditionValue	int,--比较结果，位表示法，1-大于，2-等于，4-小于
	
	constraint PK_tblFlowCondition_ConditionID primary key(ConditionID),
	constraint FK_tblFlowCondition_tblFlowTransition_TransitionID foreign key(TransitionID) references tblFlowTransition(TransitionID),
	constraint FK_tblFlowCondition_tblFlowParameter_ParameterID foreign key(ParameterID) references tblFlowParameter(ParameterID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--步骤参数映射关系，为流程流转提供参数依据。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowParameterMap')
begin
	print 'drop table tblFlowParameterMap'
	drop table tblFlowParameterMap
end
go
	print 'create table tblFlowParameterMap'
go
create table tblFlowParameterMap
(
	TransitionID	GUIDEx,--变迁规则ID。
	ParameterID		GUIDEx,--参数ID。
	MapParameterID	GUIDEx,--映射参数ID。
	MapMode			int	default(0),--映射模式，0-直接传值，1-映射函数。
	AssemblyName	nvarchar(512),--映射函数的程序集，模式为0时忽略。
	ClassName		nvarchar(512),--映射函数的类名称，模式为0时忽略。
	EntryName		nvarchar(512),--映射函数入口名称，模式为0时忽略。
	
	constraint PK_tblFlowParameterMap_TransitionID_ParameterID_MapParameterID primary key(TransitionID,ParameterID,MapParameterID),
	constraint FK_tblFlowParameterMap_tblFlowTransition_TransitionID foreign key(TransitionID) references tblFlowTransition(TransitionID),
	constraint FK_tblFlowParameterMap_tblFlowParameter_ParameterID foreign key(ParameterID) references tblFlowParameter(ParameterID),
	constraint FK_tblFlowParameterMap_tblFlowParameter_MapParameterID foreign key(MapParameterID) references tblFlowParameter(ParameterID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤上的用户(映射角色)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepRole')
begin
	print 'drop table tblFlowStepRole'
	drop table tblFlowStepRole
end
go
	print 'create table tblFlowStepRole'
go
create table tblFlowStepRole
(
	StepID		GUIDEx,--步骤ID。
	RoleID		GUIDEx,--角色定义ID。 
	RoleName	nvarchar(64),--角色定义名称。
	
	constraint PK_tblFlowStepRole_StepID_RoleID primary key(StepID,RoleID),
	constraint UK_tblFlowStepRole_tblFlowStep_StepID foreign key(StepID) references tblFlowStep(StepID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤上的用户(映射岗位)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepPost')
begin
	print 'drop table tblFlowStepPost'
	drop table tblFlowStepPost
end
go
	print 'create table tblFlowStepPost'
go
create table tblFlowStepPost
(
	StepID		GUIDEx,--步骤ID。
	PostID		GUIDEx,--岗位ID。
	PostName	nvarchar(64),--岗位名称。
	
	constraint PK_tblFlowStepPost_StepID_PostID primary key(StepID,PostID),
	constraint FK_tblFlowStepPost_tblFlowStep_StepID foreign key(StepID) references tblFlowStep(StepID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤上的用户(映射岗位级别)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepRank')
begin
	print 'drop table tblFlowStepRank'
	drop table tblFlowStepRank
end
go
	print 'create table tblFlowStepRank'
go
create table tblFlowStepRank
(
	StepID		GUIDEx,--步骤ID。
	RankID		GUIDEx,--岗位级别定义。
	RankName	nvarchar(64),--岗位级别名称。
	
	constraint PK_tblFlowStepRank_StepID_RankID primary key(StepID,RankID),
	constraint FK_tblFlowStepRank_tblFlowStep_StepID foreign key(StepID) references tblFlowStep(StepID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤上的用户(映射用户)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepEmployee')
begin
	print 'drop table tblFlowStepEmployee'
	drop table tblFlowStepEmployee
end
go
	print 'create table tblFlowStepEmployee'
go
create table tblFlowStepEmployee
(
	StepID			GUIDEx,--步骤ID。
	EmployeeID		GUIDEx,--用户ID。
	EmployeeName	nvarchar(64),--用户名称。
	
	constraint PK_tblFlowStepEmployee_StepID_EmployeeID primary key(StepID,EmployeeID),
	constraint FK_tblFlowStepEmployee_tblFlowStep_StepID foreign key(StepID) references tblFlowStep(StepID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤的授权(针对自己拥有权限的步骤授权)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepAuthorize')
begin
	print 'drop table tblFlowStepAuthorize'
	drop table tblFlowStepAuthorize
end
go
	print 'create table tblFlowStepAuthorize'
go
create table tblFlowStepAuthorize
(
	AuthorizeID			GUIDEx,--授权ID。
	StepID				GUIDEx,--步骤ID。
	EmployeeID			GUIDEx,--用户ID。
	EmployeeName		nvarchar(64),--用户名称。
	TargetEmployeeID	GUIDEx,--被授权用户ID。
	TargetEmployeeName	nvarchar(64),--被授权用户名称。
	
	BeginDate			datetime default(getdate()),--授权生效开始时间。
	EndDate				datetime default('9999-12-31 23:59:59'),--授权生效结束时间。
	
	constraint PK_tblFlowStepAuthorize_AuthorizeID primary key(AuthorizeID),
	constraint FK_tblFlowStepAuthorize_tblFlowStep_StepID foreign key(StepID) references tblFlowStep(StepID) 
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程序列化(流程定义完成后生成序列化的数据存储)
if exists(select 0 from sysobjects where xtype  = 'u' and name = 'tblFlowProcessSerialization')
begin
	print 'drop table tblFlowProcessSerialization'
	drop table tblFlowProcessSerialization
end
go
	print 'create table tblFlowProcessSerialization'
go
create table tblFlowProcessSerialization
(
	ProcessID				GUIDEx, --流程ID。
	Serialization			ntext, --序列化数据。
	Verify					nvarchar(128),--校验码
	
	constraint PK_tblFlowProcessSerialization_ProcessID primary key(ProcessID),
	constraint FK_tblFlowProcessSerialization_tblFlowProcess_ProcessID foreign key(ProcessID) references tblFlowProcess(ProcessID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程实例化(第一个步骤被实例化时实例化流程)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowProcessInstance')
begin
	print 'drop table tblFlowProcessInstance'
	drop table tblFlowProcessInstance
end
go
	print 'create table tblFlowProcessInstance'
go
create table tblFlowProcessInstance
(
	ProcessInstanceID		GUIDEx,--流程实例化ID。
	ProcessInstanceName		nvarchar(256),--流程实例化名称。
	
	ProcessID				GUIDEx null,--流程ID。
	ProcessName				nvarchar(256),--流程名称。
	
	ProcessSerialization	ntext null,--流程序列化数据。
	Verify					nvarchar(128),--校验码
	CreateDate				datetime default(getdate()),--流程实例化日期。
	EndDate					datetime null,--流程完成时间。

	FromEmployeeID			GUIDEx,--推进该步骤的用户ID。
	FromEmployeeName		nvarchar(64),--推进该步骤的用户姓名。
	
	InstanceProcessStatus	int	default(0),--流程状态。
	
	constraint PK_tblFlowProcessInstance_ProcessInstanceID primary key(ProcessInstanceID)--主键约束。
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤实例化(映射具体的任务，鉴于可以回退和循环，一个步骤定义可以被多次实例化)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepInstance')
begin 
	print 'drop table tblFlowStepInstance'
	drop table tblFlowStepInstance
end
go
	print 'create table tblFlowStepInstance'
go
create table tblFlowStepInstance
(
	StepInstanceID		GUIDEx,--步骤实例化ID。
	ProcessInstanceID	GUIDEx,--流程实例化ID。
	
	StepID				GUIDEx,--步骤ID。
	StepName			nvarchar(256),--步骤名称。
		
	FromEmployeeID		GUIDEx,--推进该步骤的用户ID。
	FromEmployeeName	nvarchar(64),--推进该步骤的用户姓名。
	
	CreateDate			datetime default(getdate()),--步骤被实例化的时间。
	EndDate				datetime null,--步骤完成(或超时)时间。
	
	InstanceStepStatus	int	default(0),--流程状态。
	
	constraint PK_tblFlowStepInstance_StepInstanceID primary key(StepInstanceID),
	constraint FK_tblFlowStepInstance_tblFlowProcessInstance_ProcessInstanceID foreign key(ProcessInstanceID) references tblFlowProcessInstance(ProcessInstanceID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程的参数实例化(映射参数的具体值)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowParameterInstance')
begin
	print 'drop table tblFlowParameterInstance'
	drop table tblFlowParameterInstance
end
go
	print 'create table tblFlowParameterInstance'
go
create table tblFlowParameterInstance
(
	StepInstanceID		GUIDEx,--步骤实例化ID。
	
	ParameterID			GUIDEx,--参数ID。
	ParameterName		nvarchar(64),--参数名称。
	ParameterValue		nvarchar(256),--参数值。
	
	constraint PK_tblFlowParameterInstance_StepInstanceID_ParameterID primary key(StepInstanceID,ParameterID),
	constraint FK_tblFlowParameterInstance_tblFlowStepInstance_StepInstanceID foreign key(StepInstanceID) references tblFlowStepInstance(StepInstanceID)
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程任务相关数据(审批结果等)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowStepInstanceData')
begin
	print 'drop table tblFlowStepInstanceData'
	drop table tblFlowStepInstanceData
end
go
	print 'create table tblFlowStepInstanceData'
go
create table tblFlowStepInstanceData
(
	TaskDataID			GUIDEx,--关联数据ID
	StepInstanceID		GUIDEx,--步骤实例化ID。
	DataCategory		int default(1),--数据类别，0：附加数据，1：流程履历。
	DataText			ntext null,--数据。
	CreateDate			datetime default(getdate()),--记录时间。
		
	constraint PK_tblFlowStepInstanceData_TaskDataID primary key(TaskDataID),--主键约束。
	constraint FK_tblFlowStepInstanceData_tblFlowStepInstance_StepInstanceID foreign key(StepInstanceID) references tblFlowStepInstance(StepInstanceID)--外键约束。
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程实例流转异常记录。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowInstanceRunError')
begin
	print 'drop table tblFlowInstanceRunError'
	drop table tblFlowInstanceRunError
end
go
	print 'create table tblFlowInstanceRunError'
go
create table tblFlowInstanceRunError
(
	ErrorID				GUIDEx,--异常ID。
	ProcessInstanceID	GUIDEx,--流程实例化ID。
	StepInstanceID		GUIDEx	null,--步骤实例化ID。
	ErrorMessage		ntext null,--异常信息。
	CreateDate			datetime default(getdate()),--记录时间。
	
	constraint PK_tblFlowInstanceRunError primary key(ErrorID),--主键约束。
	constraint FK_tblFlowInstanceRunError_tblFlowProcessInstance_ProcessInstanceID foreign key(ProcessInstanceID) references tblFlowProcessInstance(ProcessInstanceID)--外键约束。
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程步骤的实例化和人员的映射关系，即为任务(体现为待办待阅)
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblFlowInstanceTask')
begin
	print 'drop table tblFlowInstanceTask'
	drop table tblFlowInstanceTask
end
go
	print 'create table tblFlowInstanceTask'
go
create table tblFlowInstanceTask
(
	TaskID					GUIDEx,--执行任务ID。
	StepInstanceID			GUIDEx,--步骤实例化ID。
	
	EmployeeID				GUIDEx,--任务推送用户ID。
	EmployeeName			nvarchar(64),--任务推送用户名称。
	AuthorizeEmployeeID		GUIDEx null,--授权用户ID(流程授权，事先授权)。
	AuthorizeEmployeeName	nvarchar(64),--授权用户名称(流程授权，事先授权)。
	DoEmployeeID			GUIDEx null,--完成任务的用户ID(可能因待办授权引起)
	DoEmployeeName			nvarchar(64),--完成任务的用户名称(可能因待办授权引起)
	TaskCategory			int,--任务类别，1-待办，2-待阅。
	
	BeginDate				datetime default(getdate()),--任务推进时间。
	EndDate					datetime null,--任务完成(或超时)时间。
	
	BeginMode				int default(0),--进入状态：0-未进入，1- 正常进入，2-授权进入
	EndMode					int default(0),--离开状态：0-未处理，1-正常离开，2-超时处理，3-任务强占
	
	URL						nvarchar(1024),--URL地址。
	
	TaskDescription			nvarchar(256),--任务描述。
	
	constraint PK_tblFlowInstanceTask_TaskID primary key(TaskID), --主键约束。
	constraint FK_tblFlowInstanceTask_tblFlowStepInstance_StepInstanceID foreign key(StepInstanceID) references tblFlowStepInstance(StepInstanceID)--外键约束。
)
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--*******************************************************************************************************************************************************--
