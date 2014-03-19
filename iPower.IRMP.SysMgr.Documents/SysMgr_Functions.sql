/*
//================================================================================
//  FileName: SysMgr_Functions.sql
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
-------------------------------------------------------------------------------------------------
--用户访问授权，用户-系统。
if exists(select 0 from sysobjects where xtype = 'fn' and name = 'fnSysMgrEmployeeAuthorization')
begin
	print 'drop function fnSysMgrEmployeeAuthorization'
	drop function fnSysMgrEmployeeAuthorization
end
go
	print 'create function fnSysMgrEmployeeAuthorization'
go
create function fnSysMgrEmployeeAuthorization
(
	@EmployeeID	nvarchar(32) -- 用户ID。
)
returns nvarchar(2048)
as
begin
	declare @strResult nvarchar(2048)
	declare @strValue nvarchar(256)
	------------------------------------------------------------
	declare EmpSys_Cursor cursor for
	select b.SystemName
	from tblSysMgrEmployeeAuthorization a
	inner join tblSysMgrAppAuthorization b
	on  b.AppAuthID = a.AppAuthID
	where a.EmployeeID = @EmployeeID
	------------------------------------------------------------
	open EmpSys_Cursor
	fetch next from EmpSys_Cursor into @strValue
	------------------------------------------------------------
	while(@@fetch_status = 0)
	begin
		if(isnull(@strResult,'') = '')
			set @strResult = @strValue
		else
			set @strResult = @strResult + ','+ @strValue
		fetch next from EmpSys_Cursor into @strValue
	end
	------------------------------------------------------------
	close EmpSys_Cursor
	deallocate EmpSys_Cursor
	------------------------------------------------------------
	return(@strResult)
end
go
-------------------------------------------------------------------------------------------------
