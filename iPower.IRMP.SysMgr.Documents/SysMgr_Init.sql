/*
//================================================================================
//  FileName: SysMgr_Init.sql
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
----------------------------------------------------------------------------------------
--变量定义。
declare @EnumName nvarchar(256)
----------------------------------------------------------------------------------------
--授权状态枚举。
set @EnumName='iPower.IRMP.SysMgr.Engine.Persistence.EnumAuthStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Stop','停用',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Start','启用',0x01,1)
----------------------------------------------------------------------------------------
--配置类型枚举。
set @EnumName='iPower.IRMP.SysMgr.Engine.Persistence.EnumSettingType'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'System','系统',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Personal','个性化',0x01,1)
----------------------------------------------------------------------------------------
--部件模板状态枚举。
set @EnumName='iPower.IRMP.SysMgr.Engine.Persistence.EnumWebPartStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Stop','停用',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Start','启用',0x01,1)
----------------------------------------------------------------------------------------
--显示位置定义枚举
set @EnumName='iPower.IRMP.SysMgr.Engine.Persistence.EnumZoneMode'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Left','左',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Middle','中',0x01,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Right','右',0x02,2)
----------------------------------------------------------------------------------------
--链接方式枚举
set @EnumName='iPower.IRMP.SysMgr.Engine.Persistence.EnumLinkTarget'
delete from tblCommonEnums where EnumName=@EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Blank','新页面',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Self','本页面',0x01,1)
----------------------------------------------------------------------------------------
--链接状态枚举
set @EnumName='iPower.IRMP.SysMgr.Engine.Persistence.EnumLinkStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Disable','停用',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Enabled','启用',0x01,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Application','申请',0x02,2)
----------------------------------------------------------------------------------------