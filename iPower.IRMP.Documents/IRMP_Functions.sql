/*
//================================================================================
//  FileName: IRMP_Functions.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/8
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
---------------------------------------------------------------------------------------------
--IsNullOrEmpty
if exists(select 0 from sysobjects where xtype = 'fn' and name = 'fnIRMPFieldIsNullOrEmpty')
begin
	print 'drop function fnIRMPFieldIsNullOrEmpty'
	drop function fnIRMPFieldIsNullOrEmpty
end
go
	print 'create function fnIRMPFieldIsNullOrEmpty'
go
create function fnIRMPFieldIsNullOrEmpty
(
	@FieldValue		nvarchar(128),--字段值。
	@DefaultData	nvarchar(128)--默认数据。
)
returns nvarchar(128)
as
begin
	declare @strResult nvarchar(128)
	-----------------------------------------
	set @strResult = @FieldValue
	if(isnull(@strResult,'') = '')
		set @strResult = @DefaultData
	return(@strResult)
end
go
---------------------------------------------------------------------------------------------
