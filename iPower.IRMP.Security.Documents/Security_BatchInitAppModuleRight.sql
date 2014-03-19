/*
//================================================================================
//  FileName: Security_Procedure_BatchInit.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/4
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
------------------------------------------------------------------------------------------------------
--批量初始化应用系统模块权限。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSecurityBatchInitAppModuleRight')
begin
	print 'drop procedure spSecurityBatchInitAppModuleRight'
	drop procedure spSecurityBatchInitAppModuleRight
end
go
	print 'create procedure spSecurityBatchInitAppModuleRight'
go
create procedure spSecurityBatchInitAppModuleRight
(
	@SystemID	GUIDEx --系统ID。
)
as
begin
	---------------------------------------------------------------------------------------------------
	--定义临时表结构。
	declare @tempModule table(ModuleID			nvarchar(50) primary key,
							  ParentModuleID	nvarchar(50),
							  ModuleName		nvarchar(255),
							  OrderNo			int)
	---------------------------------------------------------------------------------------------------
	--将系统模块写入临时表。
	insert into @tempModule(ModuleID,ParentModuleID,ModuleName,OrderNo)
	select ModuleID,ParentModuleID,ModuleName,OrderNo
	from tblSecurityModule
	where SystemID = @SystemID
	order by OrderNo
	---------------------------------------------------------------------------------------------------
	if(@@rowcount > 0)
	begin
		--定义游标。
		declare @ModuleID		nvarchar(50)
		declare @ModuleName		nvarchar(50)
		declare module_cursor cursor for
		select ModuleID,ModuleName
		from @tempModule
		order by OrderNo
		--打开游标。
		open module_cursor
		fetch next from module_cursor 
		into @ModuleID,@ModuleName
		--循环。
		while(@@fetch_status = 0)
		begin
			--查找模块的叶子节点。
			if not exists(select 0 from @tempModule where ParentModuleID = @ModuleID)
			begin
				--删除未使用的模块权限。
				delete from tblSecurityRight where ModuleID = @ModuleID 
				and (RightID not in (select RightID from tblSecurityRoleRight))
				--创建模块权限。
				insert into tblSecurityRight(RightID,ModuleID,ActionID,RightName)
				select replace(newid(),'-',''),@ModuleID,a.ActionID,@ModuleName + '-' + a.ActionName
				from tblSecurityAction a
				where ActionType = 0 and (not exists(select 0 
													 from tblSecurityRight
													 where ModuleID = @ModuleID and ActionID = a.ActionID))
			end
			--下一条数据。
			fetch next from module_cursor 
			into @ModuleID,@ModuleName
		end
		--关闭游标。
		close module_cursor
		deallocate module_cursor
	end
	---------------------------------------------------------------------------------------------------
	select a.RightID,a.ModuleID,b.ModuleName,a.ActionID,c.ActionName,a.RightName
	from tblSecurityRight a
	inner join tblSecurityModule b
	on b.ModuleID = a.ModuleID and b.SystemID = @SystemID
	left outer join tblSecurityAction c
	on c.ActionID = a.ActionID
end
go
------------------------------------------------------------------------------------------------------
