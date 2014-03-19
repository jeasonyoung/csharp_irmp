/*
//================================================================================
//  FileName: Org_Procedure.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/14
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
---------------------------------------------------------------------------------------------------------------------------------------
--返回指定数据表（tblOrgDepartment,tblOrgPost,tblOrgRank）中，具有上下级的关系的所有子孙（除去给定值的子孙）。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgNotSelfGetOffSprings')
begin
	print 'drop procedure spOrgNotSelfGetOffSprings'
	drop procedure spOrgNotSelfGetOffSprings
end
go
	print 'create procedure spOrgNotSelfGetOffSprings'
go
create procedure spOrgNotSelfGetOffSprings
(
	@TableName		nvarchar(384),--数据表名称。
	@FieldValue		nvarchar(256)=''--项目值。
)
as
begin
	declare @result table(
							FieldID			nvarchar(32),
							FieldName		nvarchar(256),
							ParentFieldID	nvarchar(32)
					      )
	
	--tblOrgDepartment
	if(@TableName = 'tblOrgDepartment')
	begin
		insert into @result(FieldID,FieldName,ParentFieldID)
		select DepartmentID,DepartmentName,ParentDepartmentID
		from tblOrgDepartment 
		where DepartmentID not in (select FieldID from fnOrgGetOffSprings(@TableName,@FieldValue,1,'-'))
	end
	---tblOrgRank
	if(@TableName = 'tblOrgRank')
	begin
		insert into @result(FieldID,FieldName,ParentFieldID)
		select RankID,RankName,ParentRankID
		from tblOrgRank 
		where RankID not in (select FieldID from fnOrgGetOffSprings(@TableName,@FieldValue,1,'-'))
	end
	---tblOrgPost
	if(@TableName = 'tblOrgPost')
	begin
		declare @DepartmentID nvarchar(32)
		declare @index int
		set @index = charindex('-',@FieldValue)
		set @DepartmentID = ''
		if(@index > 0)
		begin
			set @DepartmentID = substring(@FieldValue, 1,@index -1)
			set @FieldValue = substring(@FieldValue,@index+1,len(@FieldValue))
		end
		
		insert into @result(FieldID,FieldName,ParentFieldID)
		select PostID,PostName,ParentPostID
		from tblOrgPost
		where DepartmentID = @DepartmentID
		and PostID not in (select FieldID from fnOrgGetOffSprings(@TableName,@FieldValue,1,'-'))
	end
	---
	select FieldID,FieldName,ParentFieldID from @result
end
go
---------------------------------------------------------------------------------------------------------------------------------------
--组织部门信息列表
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgDepartmentListView')
begin
	print 'drop procedure spOrgDepartmentListView'
	drop procedure spOrgDepartmentListView
end
go
	print 'create procedure spOrgDepartmentListView'
go
create procedure spOrgDepartmentListView
(
	@DepartmentName nvarchar(256), --部门名称
	@ParentDepartmentID	nvarchar(32)=''--上级部门
)
as
begin
	if(isnull(@ParentDepartmentID,'') = '')
	begin		 
		select a.DepartmentID, a.DepartmentName,b.FullName as FullDepartmentName, 
		a.DepartmentStatus,a.DepartmentLeader,a.DepartmentCapability
		from tblOrgDepartment a
		left outer join fnOrgGetOffSprings('tblOrgDepartment', null,1,'>') b
		on b.FieldID = a.DepartmentID
		where a.DepartmentName like '%'+@DepartmentName+'%'
		order by a.DepartmentOrder,len(b.FullName)
	end else begin
		select a.DepartmentID, a.DepartmentName,b.FullName as FullDepartmentName, 
		a.DepartmentStatus,a.DepartmentLeader,a.DepartmentCapability
		from tblOrgDepartment a
		left outer join fnOrgGetOffSprings('tblOrgDepartment', null,1,'>') b
		on b.FieldID = a.DepartmentID
		where (a.DepartmentID in (select FieldID from fnOrgGetOffSprings('tblOrgDepartment', @ParentDepartmentID,1,'>'))) and
		a.DepartmentName like '%'+@DepartmentName+'%'
		order by a.DepartmentOrder,len(b.FullName)	
	end
end
go
---------------------------------------------------------------------------------------------------------------------------------------
--删除部门
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgDeleteDepartment')
begin
	print 'drop procedure spOrgDeleteDepartment'
	drop procedure spOrgDeleteDepartment
end
go
	print 'create procedure spOrgDeleteDepartment'
go
create procedure spOrgDeleteDepartment
(
	@DepartmentID	nvarchar(32) --部门ID。
)
as
begin
	declare @result nvarchar(256)
	declare @DepartmentName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	
	select top 1 @DepartmentName = DepartmentName 
	from tblOrgDepartment 
	where DepartmentID = @DepartmentID
	
	--tblOrgDepartment
	if exists(select 0 from fnOrgGetOffSprings('tblOrgDepartment', @DepartmentID,0,'>'))
	begin
		set @resultCode = -1
		set @result = '该部门['+@DepartmentName+']下包含有子部门，请先将其删除！'
	end
	
	--tblOrgPost
	if(@resultCode = 0 and exists(select 0 from tblOrgPost where DepartmentID = @DepartmentID))
	begin
		set @resultCode = -1
		set @result = '岗位体系中包含有该部门['+@DepartmentName+']，请先将其删除！'
	end
	--tblOrgEmployee
	if(@resultCode = 0 and exists(select 0 from tblOrgEmployee where DepartmentID = @DepartmentID))
	begin
		set @resultCode = -1
		set @result = '用户信息中包含有该部门['+@DepartmentName+']下的用户，请先将其删除！'
	end
	--tblOrgLeaderSubCharge
	if(@resultCode = 0 and exists(select 0 from tblOrgLeaderSubCharge where DepartmentID = @DepartmentID))
	begin
		set @resultCode = -1
		set @result = '领导分管中包含有该部门['+@DepartmentName+']信息，请先将其删除！'
	end
	
	if(@resultCode = 0)
	begin
		delete from tblOrgDepartment where DepartmentID = @DepartmentID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------------------------------
--岗位级别
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgRankListView')
begin 
	print 'drop procedure spOrgRankListView'
	drop procedure spOrgRankListView
end
go
	print 'create procedure spOrgRankListView'
go
create procedure spOrgRankListView
(
	@RankName	nvarchar(256),--岗位级别名称。
	@ParentRankID	nvarchar(32)='' -- 上级岗位级别。
)
as
begin
	if(isnull(@ParentRankID,'') = '')
	begin
		select a.RankID,a.RankName,b.FullName as FullRankName,a.RankDescription
		from tblOrgRank a
		left outer join fnOrgGetOffSprings('tblOrgRank', null,1,'>') b
		on b.FieldID = a.RankID
		where a.RankName like '%'+@RankName+'%'
		order by len(b.FullName),a.RankName
	end else begin
	
		select a.RankID,a.RankName,b.FullName as FullRankName,a.RankDescription
		from tblOrgRank a
		left outer join fnOrgGetOffSprings('tblOrgRank', null,1,'>') b
		on b.FieldID = a.RankID
		where (a.RankID in (select FieldID from fnOrgGetOffSprings('tblOrgRank', @ParentRankID,1,'>')))
		and a.RankName like '%'+@RankName+'%'
		order by len(b.FullName),a.RankName
	end
end
go
---------------------------------------------------------------------------------------------------------------------------------------
--删除岗位级别
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgDeleteRank')
begin
	print 'drop procedure spOrgDeleteRank'
	drop procedure spOrgDeleteRank
end
go
	print 'create procedure spOrgDeleteRank'
go
create procedure spOrgDeleteRank
(
	@RankID	 nvarchar(32)--岗位级别ID
)
as 
begin
	declare @result nvarchar(256)
	declare @RankName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	
	select top 1 @RankName = RankName
	from tblOrgRank
	where RankID = @RankID
	
	--tblOrgRank
	if exists(select 0 from fnOrgGetOffSprings('tblOrgRank', @RankID,0,'>'))
	begin
		set @resultCode = -1
		set @result = '该岗位级别['+@RankName+']下包含有子岗位级别，请先将其删除！'
	end
	
	--tblOrgPost
	if(@resultCode = 0 and exists(select 0 from tblOrgPost where RankID = @RankID))
	begin
		set @resultCode = -1
		set @result = '该岗位级别['+@RankName+']在岗位体系上被使用，请先将其删除！'
	end
	
	if(@resultCode = 0)
	begin
		delete from tblOrgRank where RankID = @RankID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------------------------------
--职位体系列表
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgPostListView')
begin
	print 'drop procedure spOrgPostListView'
	drop procedure spOrgPostListView
end
go
	print 'create procedure spOrgPostListView'
go
create procedure spOrgPostListView
(
	@DepartmentName	nvarchar(256),--部门名称。
	@RankID			nvarchar(32)  --岗位级别ID。
)
as
begin
	declare @DepartmentID nvarchar(32)
	set @DepartmentID = ''
	
	declare @tmp table(PostID nvarchar(32),
					   PostName nvarchar(255),
					   FullPostName nvarchar(1024),
					   DepartmentName nvarchar(255),
					   RankName nvarchar(255),
					   RankID	nvarchar(32)
					  )
	
	if(isnull(@DepartmentName,'') <> '')
	begin
		select top 1 @DepartmentID = DepartmentID from tblOrgDepartment where DepartmentName = @DepartmentName
	end
	
	if(isnull(@DepartmentID,'') <> '')
	begin
		insert into @tmp(PostID,PostName,FullPostName,DepartmentName,RankName,RankID)
		select a.PostID,a.PostName,b.FullName as FullPostName, 
		c.FieldName as DepartmentName,
		d.FieldName as RankName,
		a.RankID
		from tblOrgPost a
		left outer join fnOrgGetOffSprings('tblOrgPost', null,1,'>') b
		on b.FieldID = a.PostID
		inner join fnOrgGetOffSprings('tblOrgDepartment', @DepartmentID,1,'>') c
		on c.FieldID = a.DepartmentID
		inner join fnOrgGetOffSprings('tblOrgRank', @RankID,1,'>') d
		on d.FieldID = a.RankID
	    order by len(d.FullName),len(b.FullName)
	end else begin
		insert into @tmp(PostID,PostName,FullPostName,DepartmentName,RankName,RankID)
		select a.PostID,a.PostName,b.FullName as FullPostName, 
		c.DepartmentName,
		d.FieldName as RankName,
		a.RankID
		from tblOrgPost a
		left outer join fnOrgGetOffSprings('tblOrgPost', null,1,'>') b
		on b.FieldID = a.PostID
		inner join tblOrgDepartment c
		on c.DepartmentID = a.DepartmentID
		inner join fnOrgGetOffSprings('tblOrgRank', @RankID,1,'>') d
		on d.FieldID = a.RankID
		where c.DepartmentName like '%'+@DepartmentName+'%'
		order by len(d.FullName),len(b.FullName)
	end
	
	if(isnull(@RankID,'')<> '')
	begin
		select PostID,PostName,FullPostName,DepartmentName,RankName
		from @tmp
		where RankID = @RankID
	end else begin
		select PostID,PostName,FullPostName,DepartmentName,RankName
		from @tmp
	end
end
go
---------------------------------------------------------------------------------------------------------------------------------------
---删除职位体系
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgDeletePost')
begin
	print 'drop procedure spOrgDeletePost'
	drop procedure spOrgDeletePost
end
go
	print 'create procedure spOrgDeletePost'
go
create procedure spOrgDeletePost
(
	@PostID	nvarchar(32) --职位ID。
)
as
begin
	declare @result nvarchar(256)
	declare @PostName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	
	select top 1 @PostName = PostName
	from tblOrgPost
	where PostID = @PostID
	
	--tblOrgPost
	if exists(select 0 from fnOrgGetOffSprings('tblOrgPost', @PostID,0,'>'))
	begin
		set @resultCode = -1
		set @result = '该岗位['+@PostName+']下包含有子岗位，请先将其删除！'
	end
	
	if(@resultCode = 0 and exists(select 0 from tblOrgEmployee where PostID = @PostID))
	begin
		set @resultCode = -1
		set @result = '该岗位['+@PostName+']下包含有用户，请先将其删除！'
	end
	
	if(@resultCode = 0)
	begin
		delete from tblOrgPost where PostID = @PostID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------------------------------
---用户信息列表
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgEmployeeListView')
begin
	print 'drop procedure spOrgEmployeeListView'
	drop procedure spOrgEmployeeListView
end
go
	print 'create procedure spOrgEmployeeListView'
go
create procedure spOrgEmployeeListView
(
	@EmployeeName	nvarchar(256),--用户名称。
	@DepartmentID	nvarchar(256) --部门ID。
)
as
begin
	if(isnull(@DepartmentID,'') <> '')
	begin
		declare @index int
		set @index = charindex('-',@DepartmentID)
		if(@index > 0)
		begin
			declare @PostID nvarchar(32)
			set @PostID = substring(@DepartmentID, @index + 1,len(@DepartmentID))
			set @DepartmentID = substring(@DepartmentID, 1, @index - 1)
			
			select a.EmployeeID,a.EmployeeSign,
			case when isnull(a.NickName,'') <> '' then a.EmployeeName + '(' + a.NickName + ')' else a.EmployeeName end as EmployeeName,
			a.Gender,b.FieldName as DepartmentName, 
			c.PostName,
			a.EmployeeStatus
			from tblOrgEmployee a
			inner join fnOrgGetOffSprings('tblOrgDepartment', @DepartmentID,1,'>') b
			on b.FieldID = a.DepartmentID
			left outer join tblOrgPost c
			on c.PostID = a.PostID 
			where  a.PostID = @PostID
			 and ((a.EmployeeName like '%'+@EmployeeName+'%') or (a.EmployeeSign like '%'+@EmployeeName+'%') or (a.NickName like '%'+@EmployeeName+'%'))
			order by a.OrderNo,a.EmployeeSign
		end else begin
	
			select a.EmployeeID,a.EmployeeSign,
			case when isnull(a.NickName,'') <> '' then a.EmployeeName + '(' + a.NickName + ')' else a.EmployeeName end as EmployeeName,
			a.Gender,b.FieldName as DepartmentName, 
			c.PostName,
			a.EmployeeStatus
			from tblOrgEmployee a
			inner join fnOrgGetOffSprings('tblOrgDepartment', @DepartmentID,1,'>') b
			on b.FieldID = a.DepartmentID
			left outer join tblOrgPost c
			on c.PostID = a.PostID
			where (a.EmployeeName like '%'+@EmployeeName+'%') or (a.EmployeeSign like '%'+@EmployeeName+'%') or (a.NickName like '%'+@EmployeeName+'%')
			order by a.OrderNo,a.EmployeeSign
		end
	end else begin
			
		select a.EmployeeID,a.EmployeeSign,
		case when isnull(a.NickName,'') <> '' then a.EmployeeName + '(' + a.NickName + ')' else a.EmployeeName end as EmployeeName,
		a.Gender,b.DepartmentName, 
		c.PostName,
		a.EmployeeStatus
		from tblOrgEmployee a
		left outer join tblOrgDepartment b
		on b.DepartmentID = a.DepartmentID
		left outer join tblOrgPost c
		on c.PostID = a.PostID
		where (a.EmployeeName like '%'+@EmployeeName+'%') or (a.EmployeeSign like '%'+@EmployeeName+'%') or (a.NickName like '%'+@EmployeeName+'%')
		order by a.OrderNo,a.EmployeeSign
	end
end
go
---------------------------------------------------------------------------------------------------------------------------------------
--删除用户数据
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgDeleteEmployee')
begin
	print 'drop procedure spOrgDeleteEmployee'
	drop procedure spOrgDeleteEmployee
end
go
	print 'create procdure spOrgDeleteEmployee'
go
create procedure spOrgDeleteEmployee
(
	@EmployeeID	nvarchar(32) --用户ID。
)
as
begin
	declare @result nvarchar(256)
	declare @EmployeeName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	
	select top 1 @EmployeeName = EmployeeName
	from tblOrgEmployee
	where EmployeeID = @EmployeeID
	
	---tblOrgLeaderSubCharge
	if(@resultCode = 0 and exists(select 0 from tblOrgLeaderSubCharge where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '该用户['+@EmployeeName+']关联了分管部门，请先将其删除！'
	end
	
	---tblOrgEmployeeSystem
	if(@resultCode = 0 and exists(select 0 from tblOrgEmployeeSystem where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '该用户['+@EmployeeName+']关联了系统，请先将其删除！'
	end
	
	if(@resultCode = 0)
	begin
		delete from tblOrgEmployee where EmployeeID = @EmployeeID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------------------------------
--获取部门岗位数据
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgDepartmentPost')
begin
	print 'drop procedure spOrgDepartmentPost'
	drop procedure spOrgDepartmentPost
end
go
	print 'create procedure spOrgDepartmentPost'
go
create procedure spOrgDepartmentPost
(
	@DepartmentID	nvarchar(32)
)
as
begin
	--定义临时存储。
	declare @v_temp table(
							FieldID			nvarchar(256) primary key,
							FieldName		nvarchar(256),
							ParentFieldID	nvarchar(256)
						 )
	--插入部门。
	if(isnull(@DepartmentID,'') <> '')
	begin
	
		insert into @v_temp(FieldID,FieldName,ParentFieldID)
		select DepartmentID,DepartmentName,ParentDepartmentID
		from tblOrgDepartment 
		where DepartmentID = @DepartmentID
		
	end else begin
	
		insert into @v_temp(FieldID,FieldName,ParentFieldID)
		select DepartmentID,DepartmentName,ParentDepartmentID
		from tblOrgDepartment 
		
	end
	--插入部门下的职位。
	while(@@rowcount > 0)
	begin
		insert into @v_temp(FieldID,FieldName,ParentFieldID)
		select  tmp.FieldID + '-'+ data.PostID,
		data.PostName,
		case when isnull(data.ParentPostID,'') = '' then tmp.FieldID else tmp.FieldID + '-'+ data.ParentPostID end
		from tblOrgPost data
		inner join @v_temp tmp
		on tmp.FieldID = data.DepartmentID
		where not exists(select 0 
						 from @v_temp tmp2
						 where tmp2.FieldID = tmp.FieldID + '-'+ data.PostID)
	end
	--显示数据
	select FieldID,FieldName,ParentFieldID
	from @v_temp
end
go
---------------------------------------------------------------------------------------------------------------------
---选择用户信息
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgEmployeePicker')
begin
	print 'drop procedure spOrgEmployeePicker'
	drop procedure spOrgEmployeePicker
end
go
	print 'create procedure spOrgEmployeePicker'
go
create procedure spOrgEmployeePicker
(
	@DepartmentName	nvarchar(256),--部门名称。
	@EmployeeKey	nvarchar(256),--用户关键字。
	@Gender			nvarchar(256) --性别。
)
as
begin
	if(isnull(@Gender,'') = '')
	begin 
	
		select a.EmployeeID,a.EmployeeName
		from tblOrgEmployee a
		left outer join tblOrgDepartment b
		on b.DepartmentID = a.DepartmentID
		where ((b.DepartmentName like '%'+@DepartmentName+'%') or (b.DepartmentSign like '%'+@DepartmentName+'%'))
		and ((a.EmployeeName like '%'+@EmployeeKey+'%') or (a.EmployeeSign like '%'+@EmployeeKey+'%') or (a.NickName like '%'+@EmployeeKey+'%'))
		
	end else begin
		
		select a.EmployeeID,a.EmployeeName
		from tblOrgEmployee a
		left outer join tblOrgDepartment b
		on b.DepartmentID = a.DepartmentID
		where a.Gender = @Gender
		and ((b.DepartmentName like '%'+@DepartmentName+'%') or (b.DepartmentSign like '%'+@DepartmentName+'%'))
		and ((a.EmployeeName like '%'+@EmployeeKey+'%') or (a.EmployeeSign like '%'+@EmployeeKey+'%') or (a.NickName like '%'+@EmployeeKey+'%'))
		
	end
end
go
---------------------------------------------------------------------------------------------------------------------
---领导分管部门列表
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgLeaderSubChargeListView')
begin
	print 'drop procedure spOrgLeaderSubChargeListView'
	drop procedure spOrgLeaderSubChargeListView
end
go
	print 'drop procedure spOrgLeaderSubChargeListView'
go
create procedure spOrgLeaderSubChargeListView
(
	@DepartmentName		nvarchar(256),--部门名称。
	@EmployeeName		nvarchar(256) --用户姓名。
)
as
begin
	
	select a.EmployeeID,c.EmployeeName, b.DepartmentName
	from tblOrgLeaderSubCharge a
	inner join tblOrgDepartment b
	on b.DepartmentID = a.DepartmentID
	inner join tblOrgEmployee c
	on c.EmployeeID = a.EmployeeID
	where ((b.DepartmentName like '%'+@DepartmentName+'%') or (b.DepartmentSign like '%'+@DepartmentName+'%'))
	and ((c.EmployeeName like '%'+@EmployeeName+'%') or (c.EmployeeSign like '%'+@EmployeeName+'%') or (c.NickName like '%'+@EmployeeName+'%'))
	order by c.OrderNo, a.EmployeeID,c.EmployeeName, b.DepartmentName
end
go
---------------------------------------------------------------------------------------------------------------------
--用户下的部门数据。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spOrgEmployeeSubChargeDepartment')
begin
	print 'drop procedure spOrgEmployeeSubChargeDepartment'
	drop procedure spOrgEmployeeSubChargeDepartment
end
go
	print 'create procedure spOrgEmployeeSubChargeDepartment'
go
create procedure spOrgEmployeeSubChargeDepartment
(
	@EmployeeID	nvarchar(32)--用户ID。
)
as
begin
	declare @v_result table(
								DepartmentID		nvarchar(32),
								ParentDepartmentID	nvarchar(32),
								DepartmentName		nvarchar(256),
								DepartmentOrder		int
						   )
	--检查用户状态是否有效。
	if(exists(select 0 from tblOrgEmployee where EmployeeID = @EmployeeID and EmployeeStatus = 1))
	begin
	
		--插入用户所在的部门。
		insert into @v_result(DepartmentID,ParentDepartmentID,DepartmentName,DepartmentOrder)
		select b.DepartmentID,b.ParentDepartmentID,b.DepartmentName,b.DepartmentOrder
		from tblOrgEmployee a
		inner join tblOrgDepartment b
		on b.DepartmentID = a.DepartmentID and b.DepartmentStatus = 1
		where a.EmployeeID = @EmployeeID
		--插入用户分管的部门。
		insert into @v_result(DepartmentID,ParentDepartmentID,DepartmentName,DepartmentOrder)
		select distinct b.DepartmentID,b.ParentDepartmentID,b.DepartmentName,b.DepartmentOrder
		from tblOrgLeaderSubCharge a
		inner join tblOrgDepartment b
		on	b.DepartmentID = a.DepartmentID and b.DepartmentStatus = 1
		where a.EmployeeID = @EmployeeID
	
	end
	--结果数据。
	select distinct DepartmentID,ParentDepartmentID,DepartmentName,DepartmentOrder
	from @v_result
end
go
---------------------------------------------------------------------------------------------------------------------
