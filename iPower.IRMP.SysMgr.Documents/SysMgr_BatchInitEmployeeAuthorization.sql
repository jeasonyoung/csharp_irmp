/*
//================================================================================
//  FileName: SysMgr_BatchInitEmployeeAuthorization.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/31
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
----------------------------------------------------------------------------------------------------------------------
--批量初始化用户授权访问。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrBatchInitEmployeeAuthorization')
begin
	print 'drop procedure spSysMgrBatchInitEmployeeAuthorization'
	drop procedure spSysMgrBatchInitEmployeeAuthorization
end
go
	print 'create procedure spSysMgrBatchInitEmployeeAuthorization'
go 
create procedure spSysMgrBatchInitEmployeeAuthorization
(
	@SystemID	nvarchar(32) --系统ID。
)
as
begin
	if((isnull(@SystemID,'') = '') and (not exists(select 0 from tblSysMgrAppAuthorization where SystemID = @SystemID)))
	begin
		print '系统ID为空或者系统访问未授权！'
	end else begin
		declare @AppAuthID	nvarchar(32)
		declare @SystemName	nvarchar(256)
		--获取系统授权ID。
		select top 1 @AppAuthID = AppAuthID,@SystemName = SystemName
		from tblSysMgrAppAuthorization
		where SystemID = @SystemID
		
		--检查用户信息表是否在数据库中。
		if not exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgEmployee')
		begin
			print '用户信息表未在本地数据库中！'
		end else begin
			--定义数据临时存储表。
			declare @tmp table(
								AppAuthID		nvarchar(32),--访问授权ID。
								EmployeeID		nvarchar(32),--用户ID。
								EmployeeName	nvarchar(50)--用户名称。
							  )
			--装载临时数据。
			insert into @tmp(AppAuthID,EmployeeID,EmployeeName)
			select distinct @AppAuthID,EmployeeID,EmployeeName
			from tblOrgEmployee
			where EmployeeStatus = 1
			--初始化数据。
			insert into tblSysMgrEmployeeAuthorization(AppAuthID,EmployeeID,EmployeeName)
			select AppAuthID,EmployeeID,EmployeeName
			from @tmp
			where (AppAuthID + '-' + EmployeeID) not in(select (AppAuthID + '-' + EmployeeID) from tblSysMgrEmployeeAuthorization where AppAuthID = @AppAuthID)
			--初始化完成。
			print  '['+@SystemName+']系统用户访问授权 数据初始化完成!'
			
			select @SystemName as '系统名称', EmployeeID as '用户ID',EmployeeName as '用户名称'
			from @tmp
		end
	end
end
go
----------------------------------------------------------------------------------------------------------------------
