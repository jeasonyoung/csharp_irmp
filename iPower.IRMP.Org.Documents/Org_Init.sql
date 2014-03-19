/*
//================================================================================
//  FileName: Org_Init.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/2
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
--变量定义
declare @EnumName nvarchar(256)
--------------------------------------------------------------------------------------------------------------------------
--部门状态
set @EnumName='iPower.IRMP.Org.Engine.Persistence.EnumStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Start','启用',1,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Stop','禁用',0,1)
--------------------------------------------------------------------------------------------------------------------------
--性别
set @EnumName='iPower.IRMP.Org.Engine.Persistence.EnumGender'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'None','未知',0,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Male','男',1,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Female','女',2,2)
--------------------------------------------------------------------------------------------------------------------------
