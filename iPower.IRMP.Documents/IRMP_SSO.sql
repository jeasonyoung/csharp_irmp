/*
//================================================================================
//  FileName: IRMP_SSO.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/9
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
---票据数据表。
if exists(select * from sysobjects where xtype = 'u' and name = 'tblSSOTicket')
begin
	print 'drop table tblSSOTicket'
	drop table tblSSOTicket
end
go
	print 'create table tblSSOTicket'
go
create table tblSSOTicket
(
	Token			GUIDEx,--令牌，票据的唯一标识。
	UserData		nvarchar(100),--用户数据，用于存储用户数据。
	IssueDate		datetime default(getdate()),--令牌发布时间。
	Expiration		datetime default(getdate()),--令牌过期时间。
	
	IssueClientIP	nvarchar(50),--申请票据的客户机IP。
	RenewalCount	int default(0),--票据续约次数。
	LastRenewalIP	nvarchar(50),--最后续约的客户机IP。
	HasValid	as  (case when Expiration > getdate() then 1 else 0 end), --是否有效。
	
	constraint PK_tblSSOTicket primary key(Token)
)
go
-----------------------------------------------------------------------------------------------------
