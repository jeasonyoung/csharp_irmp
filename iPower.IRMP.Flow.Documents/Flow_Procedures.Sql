--*******************************************************************************************************************************************************--
--	流程引擎 数据库脚本 存储过程
--*******************************************************************************************************************************************************--
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--参数定义查询
if exists(select * from sysobjects where xtype = 'p' and name = 'spFlowStepListView')
begin
	print 'drop procedure spFlowStepListView'
	drop procedure spFlowStepListView
end
go
	print 'create procedure spFlowStepListView'
go
create procedure spFlowStepListView
(
	@ParameterName	nvarchar(128),	--参数名称。
	@ProcessID		nvarchar(32),	--所属流程ID。
	@StepID			nvarchar(32)	--所属步骤ID。
)
as
begin
	declare @temp table(
							ProcessID		nvarchar(32),
							ProcessName		nvarchar(256),
							StepID			nvarchar(32),
							StepName		nvarchar(256),
							ParameterID		nvarchar(32),
							ParameterName	nvarchar(256),
							ParameterType	int,
							DefaultValue    nvarchar(256)
						)

	if ((isnull(@ProcessID,'') <> '') and (isnull(@StepID,'') <> ''))
	begin
		insert into @temp(ProcessID,ProcessName,StepID,StepName,ParameterID,ParameterName,ParameterType,DefaultValue)
		select b.ProcessID,c.ProcessName,a.StepID,b.StepName,a.ParameterID,a.ParameterName,a.ParameterType, a.DefaultValue
		from tblFlowParameter a
		inner join tblFlowStep b
		on b.StepID = a.StepID
		inner join tblFlowProcess c
		on c.ProcessID = b.ProcessID
		where b.ProcessID = @ProcessID and a.StepID = @StepID and (a.ParameterName like '%'+@ParameterName+'%')
		order by c.ProcessName, b.OrderNo
	end 
	else if ((isnull(@ProcessID,'') <> '') and (isnull(@StepID,'') = ''))
	begin
		insert into @temp(ProcessID,ProcessName,StepID,StepName,ParameterID,ParameterName,ParameterType,DefaultValue)
		select b.ProcessID,c.ProcessName,a.StepID,b.StepName,a.ParameterID,a.ParameterName,a.ParameterType, a.DefaultValue
		from tblFlowParameter a
		inner join tblFlowStep b
		on b.StepID = a.StepID
		inner join tblFlowProcess c
		on c.ProcessID = b.ProcessID
		where b.ProcessID = @ProcessID and (a.ParameterName like '%'+@ParameterName+'%')
		order by c.ProcessName, b.OrderNo
	end
	else
	begin
		insert into @temp(ProcessID,ProcessName,StepID,StepName,ParameterID,ParameterName,ParameterType,DefaultValue)
		select b.ProcessID,c.ProcessName,a.StepID,b.StepName,a.ParameterID,a.ParameterName,a.ParameterType, a.DefaultValue
		from tblFlowParameter a
		inner join tblFlowStep b
		on b.StepID = a.StepID
		inner join tblFlowProcess c
		on c.ProcessID = b.ProcessID
		order by c.ProcessName, b.OrderNo
	end
	
	select ProcessID,ProcessName,StepID,StepName,ParameterID,ParameterName,ParameterType,DefaultValue
	from @temp
	order by ProcessName--,StepName,ParameterName
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--变迁规则
if exists(select * from sysobjects where xtype = 'fn' and name = 'fnFlowConditionValues')
begin
	print 'drop function fnFlowConditionValues'
	drop function fnFlowConditionValues
end
go
	print 'create function fnFlowConditionValues'
go
create function fnFlowConditionValues
(
	@TransitionRule		nvarchar(32) --规则ID。
)
returns nvarchar(2048)
as
begin	
	--设置游标
	declare Condition_Cursor cursor for
	select '('+ b.ParameterName + '[' + c.MemberName + ']' + a.CompareValue + ')'
	from tblFlowCondition a
	inner join tblFlowParameter b
	on b.ParameterID = a.ParameterID
	inner join tblCommonEnums c
	on c.EnumName = 'iPower.IRMP.Flow.EnumCompareSign' and c.IntValue = a.ConditionValue
	where a.TransitionID = @TransitionRule
	
	declare @strValue  nvarchar(256)
	declare @strResult nvarchar(2048)
	
	--打开游标
	open Condition_Cursor
	fetch next from Condition_Cursor into @strValue
	
	while(@@fetch_status = 0)
	begin
		if(isnull(@strResult,'') = '')
			set @strResult = @strValue
		else
			set @strResult = @strResult + ',' + @strValue
		
		fetch next from Condition_Cursor into @strValue
	end
	--关闭游标
	close Condition_Cursor
	deallocate Condition_Cursor
	--返回数据。
	return(@strResult)
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--变迁规则查询
if exists(select * from sysobjects where xtype = 'p' and name = 'spFlowTransitionListView')
begin
	print 'drop procedure spFlowTransitionListView'
	drop procedure spFlowTransitionListView
end
go
	print 'create procedure spFlowTransitionListView'
go
create procedure spFlowTransitionListView
(
	@StepName	nvarchar(128),--步骤名称。
	@ProcessID	nvarchar(32)  --所属流程。
)
as
begin
	select a.TransitionID,--a.ProcessID,
	b.ProcessName,
	--a.FromStepID,
	c.StepName as FromStepName,
	--a.ToStepID,
	d.StepName as ToStepName,
	a.TransitionRule,
	dbo.fnFlowConditionValues(a.TransitionID) as ConditionName
	from tblFlowTransition a
	inner join tblFlowProcess b
	on b.ProcessID = a.ProcessID
	inner join tblFlowStep c
	on c.StepID = a.FromStepID
	inner join tblFlowStep d
	on d.StepID = a.ToStepID
	
	where a.ProcessID like '%'+@ProcessID+'%'
	and ((c.StepName like '%'+@StepName+'%') or (d.StepName like '%'+@StepName+'%'))
	order by b.ProcessName,c.OrderNo
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--参数映射列表
if exists(select * from sysobjects where xtype = 'p' and name = 'spFlowParameterMapListView')
begin
	print 'drop procedure spFlowParameterMapListView'
	drop procedure spFlowParameterMapListView
end
go
	print 'create procedure spFlowParameterMapListView'
go
create procedure spFlowParameterMapListView
(
	@ProcessID		GUIDEx,--流程ID。
	@TransitionID	GUIDEx,--变迁ID。
	@ParameterName	nvarchar(128) = '' --参数名称。
)
as
begin
	select a.TransitionID + '_' + a.ParameterID + '_' + a.MapParameterID as ParameterMapID,
	b.ProcessID,p.ProcessName,
	c.StepName +'->'+ d.StepName as TransitionName,
	--a.ParameterID,
	'[' + e.ParameterName + '] -> [' + f.ParameterName + ']' as MapParameterName,
	--a.MapParameterID,
	a.MapMode
	from tblFlowParameterMap a
	
	inner join tblFlowTransition b
	on b.TransitionID = a.TransitionID
	
	inner join tblFlowProcess p
	on p.ProcessID = b.ProcessID
	
	inner join tblFlowStep c
	on c.StepID = b.FromStepID
	
	inner join tblFlowStep d
	on d.StepID = b.ToStepID
	
	inner join tblFlowParameter e
	on e.ParameterID = a.ParameterID
	
	inner join tblFlowParameter f
	on f.ParameterID = a.MapParameterID
	
	where (b.ProcessID like '%'+@ProcessID+'%') 
	and (a.TransitionID like '%'+@TransitionID+'%')
	and ((e.ParameterName like '%'+@ParameterName+'%') or (f.ParameterName like '%'+@ParameterName+'%'))
	
	order by p.ProcessName,c.OrderNo, c.StepName +'->'+ d.StepName,'[' + e.ParameterName + '] -> [' + f.ParameterName + ']'
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--权限管理视图列表
if exists(select * from sysobjects where xtype = 'p' and name = 'spFlowStepAuthorizeListView')
begin
	print 'drop procedure spFlowStepAuthorizeListView'
	drop procedure spFlowStepAuthorizeListView
end
go
	print 'create procedure spFlowStepAuthorizeListView'
go
create procedure spFlowStepAuthorizeListView
(
	@ProcessID	nvarchar(64),--流程ID。
	@StepName	nvarchar(512),--步骤名称。
	@EmployeeID	nvarchar(64),--用户ID。
	@ValidDate	nvarchar(20) --授权日期
)
as 
begin
	if(isnull(@ValidDate,'') <> '')
	begin
	
		select a.AuthorizeID, c.ProcessName, a.StepID,b.StepName,
		a.EmployeeID,a.TargetEmployeeID,
		a.BeginDate, a.EndDate,
		case when (getdate() between a.BeginDate and a.EndDate) then '有效' else '无效' end as Valid
		from tblFlowStepAuthorize a
		inner join tblFlowStep b
		on b.StepID = a.StepID
		inner join tblFlowProcess c
		on c.ProcessID = b.ProcessID
		where (b.ProcessID like '%'+@ProcessID+'%')
		and (b.StepName like '%'+@StepName+'%') and ((a.EmployeeID like '%'+@EmployeeID+'%') or (a.TargetEmployeeID like '%'+@EmployeeID+'%'))
		and (@ValidDate between a.BeginDate and a.EndDate)
		
	end
	else begin
	
		select a.AuthorizeID, c.ProcessName, a.StepID,b.StepName,
		a.EmployeeID,a.TargetEmployeeID,
		a.BeginDate, a.EndDate,
		case when (getdate() between a.BeginDate and a.EndDate) then '有效' else '无效' end as Valid
		from tblFlowStepAuthorize a
		inner join tblFlowStep b
		on b.StepID = a.StepID
		inner join tblFlowProcess c
		on c.ProcessID = b.ProcessID
		where (b.ProcessID like '%'+@ProcessID+'%')
		and (b.StepName like '%'+@StepName+'%') 
		and ((a.EmployeeID like '%'+@EmployeeID+'%') or (a.TargetEmployeeID like '%'+@EmployeeID+'%'))
		
	end
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--启用流程检查
if exists(select * from sysobjects where xtype = 'p' and name = 'spFlowEnableProcess')
begin
	print 'drop procedure spFlowEnableProcess'
	drop procedure spFlowEnableProcess
end
go
	print 'create procedure spFlowEnableProcess'
go
create procedure spFlowEnableProcess
(
	@ProcessID	GUIDEx --流程ID。
)
as
begin	
		declare @resultCode int --结果值。
		declare @result nvarchar(255)
		set @resultCode = 0
		set @result = '流程步骤及其变迁检查完毕。'
		
		--检查步骤
		if not exists(select 0 from tblFlowStep where ProcessID=@ProcessID)
		begin
			set @resultCode = -1
			set @result = '该流程没有任何步骤。'
		end
		--检查开始步骤
		if((@result = 0) and (not exists(select 0 from tblFlowStep where ProcessID=@ProcessID and StepType=1)))
		begin
			set @resultCode = -1
			set @result = '该流程至少应该有且仅有一个开始步骤。'
		end
		--检查结束步骤
		if((@result = 0) and (not exists(select 0 from tblFlowStep where ProcessID=@ProcessID and StepType=4)))
		begin
			set @resultCode = -1
			set @result = '该流程至少应该有且仅有一个终结步骤。'
		end
		--检查前驱
		if((@result = 0) and (exists(select 0 from tblFlowStep s
							   where s.ProcessID=@ProcessID and s.StepType <> 1
							   and not exists(select 0 from tblFlowTransition f where f.ToStepID = s.StepID))))
		begin
			 set @resultCode = -1
			 set @result = '该流程并不是所有的非开始步骤都有前驱步骤。'
		end
		--检查后续
		if((@result = 0) and (exists(select 0 from tblFlowStep s
							   where s.ProcessID=@ProcessID and s.StepType <> 4
							   and not exists(select 0 from tblFlowTransition f where f.FromStepID = s.StepID))))
		begin
			 set @resultCode = -1
			 set @result = '该流程并不是所有的非终结步骤都有后续步骤。'
		end
		------------------------------------------------------------------------------------------------------------
		if(@result = 0)
		begin
			update tblFlowProcess 
			set ProcessStatus = 1 
			where ProcessID = @ProcessID
		end
		------------------------------------------------------------------------------------------------------------
		select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程实例管理列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spFlowProcessInstanceListView')
begin
	print 'drop procedure spFlowProcessInstanceListView'
	drop procedure spFlowProcessInstanceListView
end
go
	print 'create procedure spFlowProcessInstanceListView'
go
create procedure spFlowProcessInstanceListView
(
	@ProcessInstanceName	nvarchar(128)--流程实例化名称。
)
as
begin
	select data.ProcessInstanceID, data.ProcessInstanceName, p.ProcessName,--data.ProcessID,
	data.CreateDate, data.EndDate, data.InstanceProcessStatus
	from tblFlowProcessInstance data
	inner join tblFlowProcess p
	on p.ProcessID = data.ProcessID
	where data.ProcessInstanceName like '%'+@ProcessInstanceName+'%'
	order by data.CreateDate desc,data.EndDate desc,data.InstanceProcessStatus
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--删除流程实例。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spFlowDeteleProcessInstance')
begin
	print 'drop procedure spFlowDeteleProcessInstance'
	drop procedure spFlowDeteleProcessInstance
end
go
	print 'create procedure spFlowDeteleProcessInstance'
go
create procedure spFlowDeteleProcessInstance
(
	@ProcessInstanceID		GUIDEx--流程实例化ID。
)
as
begin
		declare @result nvarchar(256)
		declare @ProcessInstanceName nvarchar(256)
		declare @resultCode int
		set @resultCode = 0
		set @result = '删除流程实例全部数据。'
		---------------------------------------------------------------------------------
		select top 1 @ProcessInstanceName = ProcessInstanceName
		from tblFlowProcessInstance
		where ProcessInstanceID = @ProcessInstanceID
		---------------------------------------------------------------------------------
		--tblFlowProcessInstance
		if exists(select 0 from tblFlowProcessInstance where ProcessInstanceID = @ProcessInstanceID and InstanceProcessStatus = 0x01)
		begin
			set @resultCode = -1
			set @result = '流程实例['+@ProcessInstanceName+']状态应先停止。'
		end
		--删除全部数据
		if(@resultCode = 0)
		begin
			--tblFlowInstanceTask
			delete from tblFlowInstanceTask 
			where (StepInstanceID in (select StepInstanceID from tblFlowStepInstance where ProcessInstanceID = @ProcessInstanceID))
			--tblFlowInstanceRunError
			delete from tblFlowInstanceRunError where ProcessInstanceID = @ProcessInstanceID
			--tblFlowStepInstanceData
			delete from tblFlowStepInstanceData
			where (StepInstanceID in (select StepInstanceID from tblFlowStepInstance where ProcessInstanceID = @ProcessInstanceID))
			--tblFlowParameterInstance
			delete from tblFlowParameterInstance 
			where (StepInstanceID in (select StepInstanceID from tblFlowStepInstance where ProcessInstanceID = @ProcessInstanceID))
			--tblFlowStepInstance
			delete from tblFlowStepInstance where ProcessInstanceID = @ProcessInstanceID
			--tblFlowProcessInstance
			delete from tblFlowProcessInstance where ProcessInstanceID = @ProcessInstanceID
		end
		---------------------------------------------------------------------------------
		select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程实例流转异常列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spFlowInstanceRunErrorListView')
begin
	print 'drop procedure spFlowInstanceRunErrorListView'
	drop procedure spFlowInstanceRunErrorListView
end
go
	print 'create procedure spFlowInstanceRunErrorListView'
go
create procedure spFlowInstanceRunErrorListView
(
	@ProcessInstanceID		GUIDEx--流程实例化ID。
)
as
begin
	select a.ErrorID,isnull(b.StepName,'') as StepName,a.ErrorMessage,a.CreateDate
	from tblFlowInstanceRunError a
	left outer join tblFlowStepInstance b
	on b.StepInstanceID = a.StepInstanceID
	where a.ProcessInstanceID = @ProcessInstanceID
	order by a.CreateDate desc
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程履历。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spFlowProcessResumes')
begin
	print 'drop procedure spFlowProcessResumes'
	drop procedure spFlowProcessResumes
end
go
	print 'create procedure spFlowProcessResumes'
go
create procedure spFlowProcessResumes
(
	@ProcessInstanceID		GUIDEx--流程实例化ID。
)
as
begin
	declare @tempResult table ( TaskDataID			nvarchar(64),
								StepInstanceID		nvarchar(64),
								StepInstanceName	nvarchar(1024),
								DoEmployeeID		nvarchar(64),
								DoEmployeeName		nvarchar(128),
								ApprovalDate		datetime,
								ApprovalViews		ntext)

	--插入数据。
	insert into @tempResult(TaskDataID,StepInstanceID,StepInstanceName,DoEmployeeID,DoEmployeeName,ApprovalDate,ApprovalViews)
	select a.TaskDataID, a.StepInstanceID, isnull(c.ProcessInstanceName,'') + '-' + isnull(b.StepName,''),
	b.FromEmployeeID,b.FromEmployeeName,
	a.CreateDate,a.DataText
	from tblFlowStepInstanceData a
	inner join tblFlowStepInstance b
	on b.StepInstanceID = a.StepInstanceID and b.ProcessInstanceID = @ProcessInstanceID
	inner join tblFlowProcessInstance c
	on c.ProcessInstanceID = b.ProcessInstanceID
	where a.DataCategory = 1
	
	--更新数据。
	update @tempResult 
	set DoEmployeeID = data.DoEmployeeID,DoEmployeeName = data.DoEmployeeName
	from @tempResult t
	inner join tblFlowInstanceTask data
	on data.TaskID = t.TaskDataID
	
	--显示数据。
	select StepInstanceID,StepInstanceName,DoEmployeeID,DoEmployeeName,ApprovalDate,ApprovalViews
	from @tempResult
	order by ApprovalDate
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程任务列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spFlowInstanceTaskListView')
begin
	print 'drop procedure spFlowInstanceTaskListView'
	drop procedure spFlowInstanceTaskListView
end
go
	print 'create procedure spFlowInstanceTaskListView'
go
create procedure spFlowInstanceTaskListView
(
	@ProcessInstanceID		GUIDEx--流程实例化ID。
)
as
begin
	select a.TaskID,a.StepInstanceID,--b.StepName as StepInstanceName,
	a.EmployeeID,a.EmployeeName,a.AuthorizeEmployeeID,a.AuthorizeEmployeeName,a.DoEmployeeID,a.DoEmployeeName,
	a.TaskCategory,a.BeginDate,a.BeginMode,a.EndMode,a.URL
	from tblFlowInstanceTask a
	inner join tblFlowStepInstance b
	on b.StepInstanceID = a.StepInstanceID
	where b.ProcessInstanceID = @ProcessInstanceID
	order by  a.BeginDate desc
end
go
-----------------------------------------------------------------------------------------------------------------------------------------------------------
