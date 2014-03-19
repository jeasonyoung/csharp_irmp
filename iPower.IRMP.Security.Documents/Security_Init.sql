/*
//================================================================================
//  FileName: Security_Init.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/25
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
----------------------------------------------------------------------------------------
--变量定义。
declare @EnumName nvarchar(256)
----------------------------------------------------------------------------------------
--系统类型枚举。
set @EnumName='iPower.IRMP.Security.Engine.Persistence.EnumSystemType'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'PlatformModule','平台模块',0x01,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'WebApplication','应用系统',0x02,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'InfoShowFront','信息展示前台',0x04,2)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'ClientServerApp','CS应用系统',0x08,3)
----------------------------------------------------------------------------------------
--系统状态。
set @EnumName='iPower.IRMP.Security.Engine.Persistence.EnumSystemStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Stop','停用',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Start','启用',0x01,1)
----------------------------------------------------------------------------------------
--元操作类型。
set @EnumName='iPower.IRMP.Security.Engine.Persistence.EnumActionType'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Basic','基本',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Extend','扩展',0x01,1)
----------------------------------------------------------------------------------------
--权限元操作。
delete from tblSecurityAction where ActionID in (select data.ActionID 
											     from tblSecurityAction data
											     where data.ActionID like 'AAA000000000000000000000000000A_'
												 and (not exists(select 0 from tblSecurityRight where ActionID = data.ActionID)))
declare @ActionID nvarchar(32)
set @ActionID = 'AAA000000000000000000000000000A1'
if(not exists(select 0 from tblSecurityRight where ActionID = @ActionID))
begin
	insert into tblSecurityAction(ActionID,ActionSign,ActionType,ActionName,ActionDescription) values (@ActionID,'Security.Query',0,'查询','')
end

set @ActionID = 'AAA000000000000000000000000000A2'
if(not exists(select 0 from tblSecurityRight where ActionID = @ActionID))
begin
	insert into tblSecurityAction(ActionID,ActionSign,ActionType,ActionName,ActionDescription) values (@ActionID,'Security.Add',0,'新增','')
end

set @ActionID = 'AAA000000000000000000000000000A3'
if(not exists(select 0 from tblSecurityRight where ActionID = @ActionID))
begin
	insert into tblSecurityAction(ActionID,ActionSign,ActionType,ActionName,ActionDescription) values (@ActionID,'Security.Save',0,'保存','')
end

set @ActionID = 'AAA000000000000000000000000000A4'
if(not exists(select 0 from tblSecurityRight where ActionID = @ActionID))
begin
	insert into tblSecurityAction(ActionID,ActionSign,ActionType,ActionName,ActionDescription) values (@ActionID,'Security.Delete',0,'删除','')
end

set @ActionID = 'AAA000000000000000000000000000A5'
if(not exists(select 0 from tblSecurityRight where ActionID = @ActionID))
begin
	insert into tblSecurityAction(ActionID,ActionSign,ActionType,ActionName,ActionDescription) values (@ActionID,'Security.Export',0,'导出','')
end

set @ActionID = 'AAA000000000000000000000000000A6'
if(not exists(select 0 from tblSecurityRight where ActionID = @ActionID))
begin
	insert into tblSecurityAction(ActionID,ActionSign,ActionType,ActionName,ActionDescription) values (@ActionID,'Security.Import',0,'导入','')
end
----------------------------------------------------------------------------------------
