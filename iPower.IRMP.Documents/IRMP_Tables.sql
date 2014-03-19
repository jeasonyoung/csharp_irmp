/*
//================================================================================
//  FileName: IRMP_Tables.sql
//  Desc: IRMP_数据库表结构
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/24
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
----------------------------------------------------------------------------------------------------
--系统后台维护日志表
if exists(select * from sysobjects where xtype = 'u' and name = 'tblIRMPCommonLog')
begin
	print 'drop table tblIRMPCommonLog'
	drop table tblIRMPCommonLog
end
go
	print 'create table tblIRMPCommonLog'
go
create table tblIRMPCommonLog
(
	LogID				GUIDEx,--操作日志ID。
	SystemID			GUIDEx,	--应用系统ID。
	SystemName			nvarchar(512),--应用系统名称。
	RelationTable		nvarchar(384),--关联数据表。
	LogContext			nvarchar(4096),--日志内容。
	
	CreateDate			datetime default(getdate()),--日志创建日期
	CreateEmployeeID	GUIDEx,--创建日志用户ID。
	CreateEmployeeName	nvarchar(50),--创建日志的用户。
	
	constraint PK_tblIRMPCommonLog_LogID primary key(LogID)
)
go
----------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------
