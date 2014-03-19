/*
//================================================================================
//  FileName: Org_Functions.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/28
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
--返回指定数据表（tblOrgDepartment,tblOrgPost,tblOrgRank）中，具有上下级的关系的所有子孙。
if exists(select * from sysobjects where xtype = 'tf' and name = 'fnOrgGetOffSprings')
begin
	print 'drop function fnOrgGetOffSprings'
	drop function fnOrgGetOffSprings
end
go
	print 'create function fnOrgGetOffSprings'
go
create function fnOrgGetOffSprings
(
	@TableName		nvarchar(384),--数据表名称。
	@FieldValue		nvarchar(100),--项目值。
	@IncludeSelf	bit = 0,--是否包含自己。
	@Seperator		nvarchar(10) = '-'--名称连接字符。
)
returns @tableResult table
			(
				FieldID		nvarchar(32) primary key,
				FieldName	nvarchar(256),
				FullName	nvarchar(2048),
				LevelNum	int
			)
as
begin
	declare @v_table table
				(
					FieldID		nvarchar(32) primary key,
					FieldName	nvarchar(256),
					FullName	nvarchar(1024) default null,
					LevelNum	int,
					No			int identity(1,1)
				)
	declare @v_Level int
	select @v_Level = 0
	
	--组织结构表。
	if(@TableName = 'tblOrgDepartment')
	begin
		--插入自己（或所有同等级的）。
		insert into @v_table(FieldID,FieldName,FullName,LevelNum)
		select DepartmentID,DepartmentName,DepartmentName,@v_Level
		from tblOrgDepartment
		where (DepartmentID = @FieldValue) or
			(isnull(@FieldValue,'') = '' and isnull(ParentDepartmentID,'') = '')
		
		--循环插入自己的子孙
		while(@@rowcount > 0)
		begin
			select @v_Level = @v_Level + 1
			
			insert into @v_table(FieldID,FieldName,FullName,LevelNum)
			select data.DepartmentID,data.DepartmentName,tmp.FullName + @Seperator + data.DepartmentName,@v_Level
			from tblOrgDepartment data
			inner join @v_table tmp
			on tmp.FieldID = data.ParentDepartmentID
			where not exists(select 0 
							 from @v_table tmp2
							 where data.DepartmentID = tmp2.FieldID)
		end
	end
	
	--岗位级别。
	if(@TableName = 'tblOrgRank')
	begin
		--插入自己（或所有同等级的）。
		insert into @v_table(FieldID,FieldName,FullName,LevelNum)
		select RankID,RankName,RankName,@v_Level
		from tblOrgRank
		where (RankID = @FieldValue) or
			(isnull(@FieldValue,'') = '' and isnull(ParentRankID,'') = '')
		
		--循环插入自己的子孙。
		while(@@rowcount > 0)
		begin
			select @v_Level = @v_Level + 1
			
			insert into @v_table(FieldID,FieldName,FullName,LevelNum)
			select data.RankID,data.RankName,tmp.FullName + @Seperator + data.RankName,@v_Level
			from tblOrgRank data
			inner join @v_table tmp
			on tmp.FieldID = data.ParentRankID
			where not exists(select 0 
							 from @v_table tmp2
							 where data.RankID = tmp2.FieldID)
		end
	end
	
	--岗位表。
	if(@TableName = 'tblOrgPost')
	begin
		--插入自己（或所有同等级的）。
		insert into @v_table(FieldID,FieldName,FullName,LevelNum)
		select PostID,PostName,PostName,@v_Level
		from tblOrgPost
		where (PostID = @FieldValue) or
			(isnull(@FieldValue,'') = '' and isnull(ParentPostID,'') = '')
		
		--循环插入自己的子孙
		while(@@rowcount > 0)
		begin
			select @v_Level = @v_Level + 1
			
			insert into @v_table(FieldID,FieldName,FullName,LevelNum)
			select data.PostID,data.PostName,tmp.FullName + @Seperator + data.PostName,@v_Level
			from tblOrgPost data
			inner join @v_table tmp
			on tmp.FieldID = data.ParentPostID
			where not exists(select 0 
							 from @v_table tmp2
							 where data.PostID = tmp2.FieldID)
		end
	end
	--剔除自己。
	if(@IncludeSelf = 0)
		delete from @v_table where (FieldID = @FieldValue) or (isnull(@FieldValue,'') = '' and isnull(FieldID,'') = '')
	
	--返回结果数据。
	insert into @tableResult(FieldID,FieldName,FullName,LevelNum)
	select FieldID,FieldName,FullName,LevelNum
	from @v_table
	order by LevelNum,No
	
	return
end
go
---------------------------------------------------------------------------------------------------------------------------------------
