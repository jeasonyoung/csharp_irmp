/*
//================================================================================
//  FileName: Security_Procedure.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/29
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
*/
---------------------------------------------------------------------------------------------------------
--返回指定数据表（tblSecurityRegsiter，tblSecurityModule，tblSecurityRole）中，具有上下级关系的所有子孙（除去给定值的子孙）。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityNotSelfGetOffSprings')
begin
	print 'drop procedure spSecurityNotSelfGetOffSprings'
	drop procedure spSecurityNotSelfGetOffSprings
end
go
	print 'create procedure spSecurityNotSelfGetOffSprings'
go
create procedure spSecurityNotSelfGetOffSprings
(
	@TableName		nvarchar(384),--数据表名称。
	@FieldValue		nvarchar(256)='',--项目值。
	@SystemID		nvarchar(256)='' --所属的系统ID。
)
as
begin
	declare @result table(
							FieldID			nvarchar(32),
							FieldName		nvarchar(256),
							ParentFieldID	nvarchar(32)
						 )
	--tblSecurityRegsiter
	if(@TableName = 'tblSecurityRegsiter')
	begin
		insert into @result(FieldID,FieldName,ParentFieldID)
		select SystemID,SystemName,ParentSystemID
		from tblSecurityRegsiter
		where SystemID not in (select FieldID from fnSecurityGetOffSprings(@TableName,@FieldValue,1,'-'))
	end
	--tblSecurityModule
	if(@TableName = 'tblSecurityModule')
	begin
		if(isnull(@SystemID,'') <> '')
		begin
			insert into @result(FieldID,FieldName,ParentFieldID)
			select ModuleID,ModuleName,ParentModuleID
			from tblSecurityModule
			where (SystemID = @SystemID) and
				  (ModuleID not in (select FieldID from fnSecurityGetOffSprings(@TableName,@FieldValue,1,'-')))
		end else begin
			insert into @result(FieldID,FieldName,ParentFieldID)
			select ModuleID,ModuleName,ParentModuleID
			from tblSecurityModule
			where ModuleID not in (select FieldID from fnSecurityGetOffSprings(@TableName,@FieldValue,1,'-'))
		end
	end
	--tblSecurityRole
	if(@TableName = 'tblSecurityRole')
	begin
		if(isnull(@SystemID,'') <> '')
		begin
			insert into @result(FieldID,FieldName,ParentFieldID)
			select RoleID,RoleName,ParentRoleID
			from tblSecurityRole
			where (RoleID in (select RoleID from tblSecurityRoleSystem where SystemID = @SystemID)) and
				  (RoleID not in (select FieldID from fnSecurityGetOffSprings(@TableName,@FieldValue,1,'-')))
		end else begin
			insert into @result(FieldID,FieldName,ParentFieldID)
			select RoleID,RoleName,ParentRoleID
			from tblSecurityRole
			where RoleID not in (select FieldID from fnSecurityGetOffSprings(@TableName,@FieldValue,1,'-'))
		end
	end
	---
	select FieldID,FieldName,ParentFieldID from @result
end
go
---------------------------------------------------------------------------------------------------------
--应用系统注册列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRegsiterListView')
begin
	print 'drop procedure spSecurityRegsiterListView'
	drop procedure spSecurityRegsiterListView
end
go
	print 'create procedure spSecurityRegsiterListView'
go
create procedure spSecurityRegsiterListView
(
	@SystemName		nvarchar(256),--系统名称。
	@ParentSystemID	nvarchar(32)=''--上级系统。
)
as
begin
	if(isnull(@ParentSystemID,'') = '')
	begin
		select a.SystemID,a.SystemSign,a.SystemName,b.FullName as FullSystemName,
		a.SystemType,a.SystemStatus
		from tblSecurityRegsiter a
		left outer join fnSecurityGetOffSprings('tblSecurityRegsiter',null,1,'>') b
		on b.FieldID = a.SystemID
		where (a.SystemName like '%'+@SystemName+'%' or a.SystemSign like '%'+@SystemName+'%' or a.SystemID like '%'+@SystemName+'%')
		order by a.SystemType,a.SystemID
	end else begin
		select a.SystemID,a.SystemSign,a.SystemName,b.FullName as FullSystemName,
		a.SystemType,a.SystemStatus
		from tblSecurityRegsiter a
		left outer join fnSecurityGetOffSprings('tblSecurityRegsiter',null,1,'>') b
		on b.FieldID = a.SystemID
		where (a.SystemID in (select FieldID from fnSecurityGetOffSprings('tblSecurityRegsiter',@ParentSystemID,1,'>'))) and
		(a.SystemName like '%'+@SystemName+'%' or a.SystemSign like '%'+@SystemName+'%' or a.SystemID like '%'+@SystemName+'%')
		order by a.SystemType,a.SystemID
	end
end
go
---------------------------------------------------------------------------------------------------------
--删除应用系统注册。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityDeleteRegsiter')
begin
	print 'drop procedure spSecurityDeleteRegsiter'
	drop procedure spSecurityDeleteRegsiter
end
go
	print 'create procedure spSecurityDeleteRegsiter'
go
create procedure spSecurityDeleteRegsiter
(
	@SystemID	GUIDEx --系统ID。
)
as
begin
	declare @result nvarchar(256)
	declare @SystemName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	
	select top 1 @SystemName = SystemName
	from tblSecurityRegsiter
	where SystemID = @SystemID
	
	---tblSecurityRegsiter
	if exists(select 0 from fnSecurityGetOffSprings('tblSecurityRegsiter',@SystemID,0,'-'))
	begin
		set @resultCode = -1
		set @result = '该系统['+@SystemName+']下包含有子系统，请先将其删除！' 
	end
	---tblSecurityModule
	if(@resultCode = 0 and exists(select 0 from tblSecurityModule where SystemID = @SystemID))
	begin
		set @resultCode = -1
		set @result = '系统模块注册中包含有['+@SystemName+']系统，请先将其删除！'
	end
	---tblSecurityRoleSystem
	if(@resultCode = 0 and exists(select 0 from tblSecurityRoleSystem where SystemID = @SystemID))
	begin
		set @resultCode = -1
		set @result = '角色定义中包含有['+@SystemName+']系统，请先将其删除！'
	end
	--
	if(@resultCode = 0)
	begin
		delete from tblSecurityRegsiter where SystemID = @SystemID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------
--系统模块注册列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityModuleListView')
begin
	print 'drop procedure spSecurityModuleListView'
	drop procedure spSecurityModuleListView
end
go
	print 'create procedure spSecurityModuleListView'
go
create procedure spSecurityModuleListView
(
	@ModuleName	nvarchar(256),--模块名称。
	@SystemID	nvarchar(32) --所属系统ID。
)
as
begin
	if(isnull(@SystemID,'') = '')
	begin
		select a.ModuleID,a.ModuleName, b.SystemName,
		c.FullName as FullModuleName,
		a.ModuleStatus
		from tblSecurityModule a
		inner join tblSecurityRegsiter b
		on b.SystemID = a.SystemID
		left outer join fnSecurityGetOffSprings('tblSecurityModule',null,1,'>') c
		on c.FieldID = a.ModuleID
		where (a.ModuleName like '%'+@ModuleName+'%' or a.ModuleID like '%'+@ModuleName+'%')
		order by b.SystemName,c.FullName,a.OrderNo
	end else begin
		select a.ModuleID,a.ModuleName, b.SystemName,
		c.FullName as FullModuleName,
		a.ModuleStatus
		from tblSecurityModule a
		inner join tblSecurityRegsiter b
		on b.SystemID = a.SystemID
		left outer join fnSecurityGetOffSprings('tblSecurityModule',null,1,'>') c
		on c.FieldID = a.ModuleID
		where (a.SystemID in (select FieldID from fnSecurityGetOffSprings('tblSecurityRegsiter',@SystemID,1,'>')))
		and (a.ModuleName like '%'+@ModuleName+'%' or a.ModuleID like '%'+@ModuleName+'%')
		order by b.SystemName,c.FullName,a.OrderNo
	end
end
go
---------------------------------------------------------------------------------------------------------
--删除模块注册。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityDeleteModule')
begin
	print 'drop procedure spSecurityDeleteModule'
	drop procedure spSecurityDeleteModule
end
go
	print 'create procedure spSecurityDeleteModule'
go
create procedure spSecurityDeleteModule
(
	@ModuleID	nvarchar(32) --模块ID。
)
as
begin
	declare @result nvarchar(256)
	declare @ModuleName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	
	select top 1 @ModuleName = ModuleName
	from tblSecurityModule
	where ModuleID = @ModuleID
	
	--tblSecurityModule
	if exists(select 0 from fnSecurityGetOffSprings('tblSecurityModule',@ModuleID,0,'-'))
	begin
		set @resultCode = -1
		set @result = '该系统模块['+@ModuleName+']下包含有子系统模块，请先将其删除！' 
	end
	
	--tblSecurityRight
	if((@resultCode = 0) and exists(select 0 from tblSecurityRight where ModuleID = @ModuleID))
	begin
		set @resultCode = -1
		set @result = '模块权限中包含有该模块['+@ModuleName+']的权限设置，请先将其删除！' 
	end
	
	if(@resultCode = 0)
	begin
		delete from tblSecurityModule where ModuleID = @ModuleID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------
--模块权限列表
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRightListView')
begin
	print 'drop procedure spSecurityRightListView'
	drop procedure spSecurityRightListView
end
go
	print 'create procedure spSecurityRightListView'
go
create procedure spSecurityRightListView
(
	@SystemID	nvarchar(32),--所属系统。
	@ModuleName	nvarchar(256)--模块名称。
)
as
begin
	declare @ModuleID nvarchar(32)
	set @ModuleID = ''
	select top 1 @ModuleID = ModuleID  from tblSecurityModule where ModuleName = @ModuleName

	if(isnull(@ModuleID,'') = '')
	begin
	
		select a.RightID, b.ModuleName, c.ActionName, a.RightName
		from tblSecurityRight a
		inner join tblSecurityModule  b
		on b.ModuleID = a.ModuleID and b.SystemID = @SystemID
		inner join tblSecurityAction c
		on c.ActionID = a.ActionID
		
		where b.ModuleName like '%'+@ModuleName+'%'
		order by b.ModuleName,c.ActionID
		
	end else begin
	
		select a.RightID, b.ModuleName, c.ActionName, a.RightName
		from tblSecurityRight a
		inner join tblSecurityModule  b
		on b.ModuleID = a.ModuleID and b.SystemID = @SystemID
		inner join tblSecurityAction c
		on c.ActionID = a.ActionID
		
		where b.ModuleID in (select FieldID from fnSecurityGetOffSprings('tblSecurityModule',@ModuleID,1,'>'))
		order by b.ModuleName,c.ActionID
	end
end
go
---------------------------------------------------------------------------------------------------------
--角色定义列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRoleListView')
begin
	print 'drop procedure spSecurityRoleListView'
	drop procedure spSecurityRoleListView
end
go
	print 'create procedure spSecurityRoleListView'
go
create procedure spSecurityRoleListView
(
	@RoleName	nvarchar(32) --角色名称。
)
as
begin

	select a.RoleID,a.RoleName, b.FullName as FullRoleName,
	a.RoleDescription,a.RoleStatus,
	dbo.fnSecurityStackRoleSystemName(a.RoleID) as SystemNames
	from tblSecurityRole a
	left outer join  fnSecurityGetOffSprings('tblSecurityRole',null,1,'>') b
	on b.FieldID = a.RoleID
	
	where a.RoleName like '%'+@RoleName+'%'
	
end
go
---------------------------------------------------------------------------------------------------------
--删除角色
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityDeleteRole')
begin
	print 'drop procedure spSecurityDeleteRole'
	drop procedure spSecurityDeleteRole
end
go
	print 'create procedure spSecurityDeleteRole'
go
create procedure spSecurityDeleteRole
(
	@RoleID	nvarchar(32) --角色ID。
)
as
begin
	declare @result nvarchar(256)
	declare @RoleName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	
	select top 1 @RoleName = RoleName
	from tblSecurityRole
	where RoleID = @RoleID
	
	--tblSecurityRole
	if exists(select 0 from fnSecurityGetOffSprings('tblSecurityRole',@RoleID,0,'-'))
	begin
		set @resultCode = -1
		set @result = '该角色['+@RoleName+']下包含了子角色，请先将其删除！' 
	end
	--tblSecurityRoleSystem
	--if((@resultCode = 0) and exists(select 0 from tblSecurityRoleSystem where RoleID = @RoleID))
	--begin
	--	set @resultCode = -1
	--	set @result = '该角色['+@RoleName+']下已经分配了所属系统，请先将其删除！'
	--end
	--tblSecurityRoleRight
	if((@resultCode = 0) and exists(select 0 from tblSecurityRoleRight where  RoleID = @RoleID))
	begin
		set @resultCode = -1
		set @result = '该角色['+@RoleName+']下已经分配了权限，请先将其删除！'
	end
	--tblSecurityRoleEmployee
	if((@resultCode = 0) and exists(select 0 from tblSecurityRoleEmployee where  RoleID = @RoleID))
	begin
		set @resultCode = -1
		set @result = '该角色['+@RoleName+']下已经分配了用户，请先将其删除！'
	end
	--tblSecurityRoleRank
	if((@resultCode = 0) and exists(select 0 from tblSecurityRoleRank where  RoleID = @RoleID))
	begin
		set @resultCode = -1
		set @result = '该角色['+@RoleName+']下已经分配了岗位级别，请先将其删除！'
	end
	--tblSecurityRolePost
	if((@resultCode = 0) and exists(select 0 from tblSecurityRolePost where  RoleID = @RoleID))
	begin
		set @resultCode = -1
		set @result = '该角色['+@RoleName+']下已经分配了岗位，请先将其删除！'
	end
	--tblSecurityRoleDepartment
	if((@resultCode = 0) and exists(select 0 from tblSecurityRoleDepartment where  RoleID = @RoleID))
	begin
		set @resultCode = -1
		set @result = '该角色['+@RoleName+']下已经分配了部门，请先将其删除！'
	end
	
	if(@resultCode = 0)
	begin
		delete from tblSecurityRoleSystem where RoleID = @RoleID
		delete from tblSecurityRole where RoleID = @RoleID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------
---角色权限列表
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRoleRightListView')
begin
	print 'drop procedure spSecurityRoleRightListView'
	drop procedure spSecurityRoleRightListView
end
go
	print 'create procedure spSecurityRoleRightListView'
go
create procedure spSecurityRoleRightListView
(
	@RoleName	nvarchar(256),--角色名称。
	@RightName	nvarchar(256) --模块权限。
)
as
begin
	--结果数据。
	declare @Result table(
							RoleID		nvarchar(32),
							RoleName	nvarchar(256),
							RightNames	nvarchar(2048)
						  )
	--分隔符。
	declare @Seperator char(1)
	set @Seperator = ','
	declare @v_RoleID		nvarchar(32)
	declare @v_Old_RoleID	nvarchar(32)
	declare @v_RoleName		nvarchar(256)
	declare @v_Old_RoleName nvarchar(256)
	declare @v_RightName	nvarchar(256)
	declare @v_RgihtNames	nvarchar(2048)
	set @v_Old_RoleID = ''
	set @v_Old_RoleName = ''
	set @v_RgihtNames = ''
	--定义游标。
	declare right_cursor cursor for
	select a.RoleID,b.RoleName,c.RightName
	from tblSecurityRoleRight a
	inner join tblSecurityRole b
	on b.RoleID = a.RoleID
	inner join tblSecurityRight c
	on (c.RightID = a.RightID) and (c.ModuleID in (select ModuleID from tblSecurityModule where ModuleStatus = 1 and (SystemID in (select SystemID from tblSecurityRegsiter where SystemStatus = 1))))
	where (b.RoleName like '%'+@RoleName+'%')
	order by a.RoleID,c.RightName
	---打开游标。
	open right_cursor
	fetch next from right_cursor into @v_RoleID,@v_RoleName,@v_RightName
	--循环。
	while(@@fetch_status = 0)
	begin
		if((isnull(@v_Old_RoleID,'') <> '') and (@v_Old_RoleID <> @v_RoleID))
		begin
			insert into @Result(RoleID,RoleName,RightNames)
			select @v_Old_RoleID,@v_Old_RoleName,@v_RgihtNames
			set @v_RgihtNames = ''
		end
		
		set @v_Old_RoleID = @v_RoleID
		set @v_Old_RoleName = @v_RoleName
		if(isnull(@v_RgihtNames,'') = '')
			set @v_RgihtNames = @v_RightName
		else
			set @v_RgihtNames = @v_RgihtNames + @Seperator + @v_RightName
		
		fetch next from right_cursor into @v_RoleID,@v_RoleName,@v_RightName
	end
	--关闭游标，结束游标。
	close right_cursor
	deallocate right_cursor
	--最后一行数据。
	if(isnull(@v_Old_RoleID,'') <> '')
	begin
		insert into @Result(RoleID,RoleName,RightNames)
		select @v_Old_RoleID,@v_Old_RoleName,@v_RgihtNames
	end
	---结果数据。
	select RoleID,RoleName,RightNames
	from @Result
	where RightNames like '%'+@RightName+'%'
	order by RoleName
end
go
---------------------------------------------------------------------------------------------------------
--获取指定角色系统下的所有权限（返回系统-模块-权限）。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRoleSystemAllRight')
begin
	print 'drop procedure spSecurityRoleSystemAllRight'
	drop procedure spSecurityRoleSystemAllRight
end
go
	print 'create procedure spSecurityRoleSystemAllRight'
go
create procedure spSecurityRoleSystemAllRight
(
	@RoleID		nvarchar(32) --角色ID。
)
as
begin
	--分隔符。
	declare @Seperator char(1)
	set @Seperator = '-'
	--结果数据定义。
	declare @v_result table (
								RSRID			nvarchar(256)	primary key,--id
								ParentRSRID		nvarchar(256)	null,--pid
								RSRName			nvarchar(256)
							)
	--结果临时数据定义。
	declare @v_temp_result table (
								RSRID			nvarchar(256)	primary key,--id
								ParentRSRID		nvarchar(256)	null,--pid
								RSRName			nvarchar(256)
							)
	--模块临时数据定义。
	declare @v_temp table (
								RSRID			nvarchar(256)	primary key,--id
								ModuleID		nvarchar(32)	null,
								ParentRSRID		nvarchar(256)	null,--pid
								RSRName			nvarchar(256)
						  )
	--插入系统数据。
	insert into @v_temp(RSRID,ModuleID,ParentRSRID,RSRName)
	select a.SystemID, null,null,b.SystemName
	from tblSecurityRoleSystem a
	inner join tblSecurityRegsiter b
	on b.SystemID = a.SystemID
	where b.SystemStatus = 1 and  a.RoleID = @RoleID
	--插入模块。
	while(@@rowcount > 0)
	begin
		insert into @v_temp(RSRID,ModuleID,ParentRSRID,RSRName)
		select tmp.RSRID + @Seperator + data.ModuleID,data.ModuleID,
		case when isnull(data.ParentModuleID,'') = '' then tmp.RSRID else tmp.RSRID + @Seperator + data.ParentModuleID end,
		data.ModuleName
		from tblSecurityModule data
		inner join @v_temp tmp
		on tmp.RSRID = data.SystemID
		where data.ModuleStatus = 1 
		and (not exists(select 0
						from @v_temp tmp2
						where tmp2.RSRID = tmp.RSRID + @Seperator + data.ModuleID))
	end
	
	--插入模块。
	insert into @v_temp_result(RSRID,ParentRSRID,RSRName)
	select RSRID,ParentRSRID,RSRName
	from @v_temp
	--插入权限。
	while(@@rowcount > 0)
	begin
		insert into @v_temp_result(RSRID,ParentRSRID,RSRName)
		select tmp.RSRID + @Seperator + 'R'+ @Seperator + data.RightID, 
		tmp.RSRID,data.RightName
		from tblSecurityRight data
		inner join @v_temp tmp
		on tmp.ModuleID = data.ModuleID
		where not exists(select 0
						 from @v_temp_result tmp2
						 where tmp2.RSRID = tmp.RSRID + @Seperator + 'R'+ @Seperator + data.RightID)
	end
	--插入结果。
	insert into @v_result(RSRID,ParentRSRID,RSRName)
	select data.RSRID,data.ParentRSRID,data.RSRName
	from @v_temp_result data
	where (right(data.RSRID,len(data.RSRID) - charindex(@Seperator + 'R'+ @Seperator,data.RSRID) - 2) in (select RightID from tblSecurityRight))
	    and (not exists(select 0 
					 from @v_temp_result tmp
					 where tmp.ParentRSRID = data.RSRID))
	--循环插入父节点。
	while(@@rowcount > 0)
	begin
		insert into @v_result(RSRID,ParentRSRID,RSRName)
		select distinct data.RSRID,data.ParentRSRID,data.RSRName
		from @v_temp_result data
		inner join @v_result tmp
		on tmp.ParentRSRID = data.RSRID
		where not exists(select 0
						 from @v_result tmp2
						 where tmp2.RSRID = data.RSRID)
	end
	--返回结果数据。
	select RSRID,ParentRSRID,RSRName
	from @v_result
end
go
---------------------------------------------------------------------------------------------------------
--获取系统用户模块权限。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityModulePermissions')
begin
	print 'drop procedure spSecurityModulePermissions'
	drop procedure spSecurityModulePermissions
end
go
	print 'create procedure spSecurityModulePermissions'
go
create procedure spSecurityModulePermissions
(
	@SystemID	nvarchar(32),--系统ID。
	@ModuleID	nvarchar(32),--模块ID。
	@EmployeeID	nvarchar(32), --用户ID。
	@DepartmentID	nvarchar(32),--用户的部门ID。
	@RankID			nvarchar(32),--用户的岗位级别ID。
	@PostID			nvarchar(32) --用户的岗位ID。
)
as
begin
	--定义角色存储。
	declare @v_Role Table(RoleID	nvarchar(32))
	--填充角色数据。
	insert into @v_Role(RoleID)
	select distinct RoleID
	from dbo.fnSecurityGetEmployeeRoles(@SystemID,@EmployeeID,@DepartmentID,@RankID,@PostID)
	--结果数据。
	select distinct a.ActionID as PermissionID,a.ActionName as PermissionName
	from tblSecurityAction a
	inner join tblSecurityRight b
	on b.ActionID = a.ActionID and b.ModuleID = @ModuleID
	where b.RightID in (select RightID 
						from tblSecurityRoleRight
						where RoleID in (select RoleID from @v_Role))
end
go
---------------------------------------------------------------------------------------------------------
--角色岗位级别列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRoleRankListView')
begin
	print 'drop procedure spSecurityRoleRankListView'
	drop procedure spSecurityRoleRankListView
end
go
	print 'create procedure spSecurityRoleRankListView'
go
create procedure spSecurityRoleRankListView
(
	@RoleName	nvarchar(50),--角色名称。
	@RankName	nvarchar(50) --岗位级别名称。
)
as
begin
	select distinct a.RoleID,b.RoleName,a.RankName
	from tblSecurityRoleRank a
	inner join tblSecurityRole b
	on b.RoleID = a.RoleID and b.RoleStatus = 1
	where (b.RoleName like '%'+@RoleName+'%') and (a.RankName like '%'+@RankName+'%')
	order by a.RoleID,a.RankName
end
go
---------------------------------------------------------------------------------------------------------
--角色岗位列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRolePostListView')
begin
	print 'drop procedure spSecurityRolePostListView'
	drop procedure spSecurityRolePostListView
end
go
	print 'create procedure spSecurityRolePostListView'
go
create procedure spSecurityRolePostListView
(
	@RoleName	nvarchar(50),--角色名称。
	@PostName	nvarchar(50) --岗位名称。
)
as
begin
	select distinct a.RoleID,b.RoleName,a.PostName
	from tblSecurityRolePost a
	inner join tblSecurityRole b
	on b.RoleID = a.RoleID and b.RoleStatus = 1
	where (b.RoleName like '%'+@RoleName+'%') and (a.PostName like '%'+@PostName+'%')
	order by a.RoleID,a.PostName
end
go
---------------------------------------------------------------------------------------------------------
--角色部门列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRoleDepartmentListView')
begin
	print 'drop procedure spSecurityRoleDepartmentListView'
	drop procedure spSecurityRoleDepartmentListView
end
go
	print 'create procedure spSecurityRoleDepartmentListView'
go
create procedure spSecurityRoleDepartmentListView
(
	@RoleName		nvarchar(50),--角色名称。
	@DepartmentName	nvarchar(50) --部门名称。
)
as
begin
	select distinct a.RoleID,b.RoleName,a.DepartmentName
	from tblSecurityRoleDepartment a
	inner join tblSecurityRole b
	on b.RoleID = a.RoleID and b.RoleStatus = 1
	where (b.RoleName like '%'+@RoleName+'%') and (a.DepartmentName like '%'+@DepartmentName+'%')
	order by a.RoleID,a.DepartmentName
end
go
---------------------------------------------------------------------------------------------------------
--角色用户列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityRoleEmployeeListView')
begin
	print 'drop procedure spSecurityRoleEmployeeListView'
	drop procedure spSecurityRoleEmployeeListView
end
go
	print 'create procedure spSecurityRoleEmployeeListView'
go
create procedure spSecurityRoleEmployeeListView
(
	@RoleName		nvarchar(50),--角色名称。
	@EmployeeName	nvarchar(50) --部门名称。
)
as
begin
	select distinct a.RoleID,b.RoleName,a.EmployeeName
	from tblSecurityRoleEmployee a
	inner join tblSecurityRole b
	on b.RoleID = a.RoleID and b.RoleStatus = 1
	where (b.RoleName like '%'+@RoleName+'%') and (a.EmployeeName like '%'+@EmployeeName+'%')
	order by a.RoleID,a.EmployeeName
end
go
---------------------------------------------------------------------------------------------------------
