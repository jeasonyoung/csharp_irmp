/*
//================================================================================
//  FileName: SysMgr_Tables.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/27
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
----------------------------------------------------------------------------------------------
--系统管理。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrEmployeeAuthorization')
begin
	print 'drop table tblSysMgrEmployeeAuthorization'
	drop table tblSysMgrEmployeeAuthorization
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrSettingPersonal')
begin
	print 'drop table tblSysMgrSettingPersonal'
	drop table tblSysMgrSettingPersonal
end
go

if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrSetting')
begin
	print 'drop table tblSysMgrSetting'
	drop table tblSysMgrSetting
end
go

if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrWebPartPersonal')
begin
	print 'drop table tblSysMgrWebPartPersonal'
	drop table tblSysMgrWebPartPersonal
end
go

if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrWebPartProperty')
begin
	print 'drop table tblSysMgrWebPartProperty'
	drop table tblSysMgrWebPartProperty
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrWebPart')
begin
	print 'drop table tblSysMgrWebPart'
	drop table tblSysMgrWebPart
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrRegWebPartTemplateProperty')
begin
	print 'drop table tblSysMgrRegWebPartTemplateProperty'
	drop table tblSysMgrRegWebPartTemplateProperty
end
go
----------------------------------------------------------------------------------------------
--系统访问授权。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrAppAuthorization')
begin
	print 'drop table tblSysMgrAppAuthorization'
	drop table tblSysMgrAppAuthorization
end
go
	print 'create table tblSysMgrAppAuthorization'
go
create table tblSysMgrAppAuthorization
(
	AppAuthID		GUIDEx,--访问授权ID。
	SystemID		GUIDEx,--系统ID。
	SystemName		nvarchar(256),--系统名称。
	AuthPwd			nvarchar(50),--授权密码。
	AuthStatus		int default(1),--授权状态(0:停用;1:启用)。
	
	constraint PK_tblSysMgrAppAuthorization primary key(AppAuthID),--主键约束。
	constraint UK_tblSysMgrAppAuthorization_SystemID unique(SystemID)--唯一约束。
)
go
----------------------------------------------------------------------------------------------
--用户访问授权。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrEmployeeAuthorization')
begin
	print 'drop table tblSysMgrEmployeeAuthorization'
	drop table tblSysMgrEmployeeAuthorization
end
go
	print 'create table tblSysMgrEmployeeAuthorization'
go
create table tblSysMgrEmployeeAuthorization
(
	AppAuthID			GUIDEx,--访问授权ID。
	EmployeeID			GUIDEx,--用户ID。
	EmployeeName		nvarchar(50),--用户名称。
	
	constraint PK_tblSysMgrEmployeeAuthorization primary key(AppAuthID,EmployeeID),--主键约束。
	constraint FK_tblSysMgrEmployeeAuthorization_tblSysMgrAppAuthorization_AppAuthID foreign key(AppAuthID) references tblSysMgrAppAuthorization(AppAuthID)--外键约束。
)
go
----------------------------------------------------------------------------------------------
--用户限制 拒绝IP。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrLimitRefusedIPAddr')
begin
	print 'drop table tblSysMgrLimitRefusedIPAddr'
	drop table tblSysMgrLimitRefusedIPAddr
end
go
	print 'create table tblSysMgrLimitRefusedIPAddr'
go
create table tblSysMgrLimitRefusedIPAddr
(
	RefusedID		GUIDEx,--拒绝ID。
	EmployeeID		GUIDEx null,--用户ID(为空时表示全局)。
	EmployeeName	nvarchar(50) null,--用户名称(为空时表示全局)。
	
	RefusedIPAddr	nvarchar(50),--IP地址。
	
	constraint PK_tblSysMgrLimitRefusedIPAddr primary key(RefusedID),--主键约束。
	constraint UK_tblSysMgrLimitRefusedIPAddr_EmployeeID_RefusedIPAddr unique(EmployeeID,RefusedIPAddr)--唯一约束。
)
----------------------------------------------------------------------------------------------
--用户限制 绑定IP。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrLimitBindIPAddr')
begin
	print 'drop table tblSysMgrLimitBindIPAddr'
	drop table tblSysMgrLimitBindIPAddr
end
go
	print 'create table tblSysMgrLimitBindIPAddr'
go
create table tblSysMgrLimitBindIPAddr
(
	BindID			GUIDEx,--绑定ID。
	
	EmployeeID		GUIDEx null,--用户ID(为空时表示全局)。
	EmployeeName	nvarchar(50) null,--用户名称(为空时表示全局)。
	
	BindIPAddr		nvarchar(50),--IP地址。
	
	constraint PK_tblSysMgrLimitBindIPAddr primary key(BindID),--主键约束。
	constraint UK_tblSysMgrLimitBindIPAddr_EmployeeID_BindIPAddr unique(EmployeeID,BindIPAddr) --唯一约束。
)
go
----------------------------------------------------------------------------------------------
--用户限制 指定时间区间。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrLimitSpecifyTimeZone')
begin
	print 'drop table tblSysMgrLimitSpecifyTimeZone'
	drop table tblSysMgrLimitSpecifyTimeZone
end
go
	print 'create table tblSysMgrLimitSpecifyTimeZone'
go
create table tblSysMgrLimitSpecifyTimeZone
(
	ZoneID			GUIDEx,--指定ID。
	
	EmployeeID		GUIDEx null,--用户ID(为空时表示全局)。
	EmployeeName	nvarchar(50) null,--用户名称(为空时表示全局)。
	
	StartTime		datetime not null,--开始时间。
	EndTime			datetime not null,--结束时间。
	
	AuthStatus		int default(1),--授权状态(0:停用;1:启用)。
	
	constraint PK_tblSysMgrLimitSpecifyTimeZone primary key(ZoneID),--主键约束。
	constraint UK_tblSysMgrLimitSpecifyTimeZone_EmployeeID_StartTime_EndTime unique(EmployeeID,StartTime,EndTime)--唯一约束。
)
go
----------------------------------------------------------------------------------------------
--用户限制 限制登录。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrLimitLogin')
begin
	print 'drop table tblSysMgrLimitLogin'
	drop table tblSysMgrLimitLogin
end
go
	print 'create table tblSysMgrLimitLogin'
go
create table tblSysMgrLimitLogin
(
	LimitID		GUIDEx,--限制ID。
	
	EmployeeID		GUIDEx null,--用户ID(为空时表示全局)。
	EmployeeName	nvarchar(50) null,--用户名称(为空时表示全局)。
	
	constraint PK_tblSysMgrLimitLogin primary key(LimitID),--主键约束。
	constraint UK_tblSysMgrLimitLogin_EmployeeID unique(EmployeeID)--唯一约束。
)
go
----------------------------------------------------------------------------------------------
--系统配置 系统参数设置。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrSetting')
begin
	print 'drop table tblSysMgrSetting'
	drop table tblSysMgrSetting
end
go
	print 'create table tblSysMgrSetting'
go
create table tblSysMgrSetting
(
	SettingID		GUIDEx,--配置ID。
	AppAuthID		GUIDEx,--访问授权ID。
	
	SettingType		int default(0),--配置类型(0-系统，1-个性化)。
	SettingSign		nvarchar(256), --配置符号。
	DefaultValue	nvarchar(512), --默认配置值。
	
	Description		nvarchar(512),--配置描述信息。
	
	constraint PK_tblSysMgrSetting primary key(SettingID),--主键约束。
	constraint UK_tblSysMgrSetting_AppAuthID_SettingSign unique(AppAuthID,SettingSign), --唯一约束。
	constraint FK_tblSysMgrSetting_tblSysMgrAppAuthorization_AppAuthID foreign key(AppAuthID) references tblSysMgrAppAuthorization(AppAuthID) --外键约束。
)
go
----------------------------------------------------------------------------------------------
--实例化系统参数设置。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrSettingPersonal')
begin
	print 'drop table tblSysMgrSettingPersonal'
	drop table tblSysMgrSettingPersonal
end
go
	print 'create table tblSysMgrSettingPersonal'
go
create table tblSysMgrSettingPersonal
(
	PersonalSettingID	GUIDEx,--实例化ID。
	SettingID			GUIDEx,--配置ID。
	
	EmployeeID			GUIDEx null,--用户ID(为空时表示全局)。
	EmployeeName		nvarchar(50) null,--用户名称(为空时表示全局)。
	
	SettingValue	nvarchar(512),--配置值。
	
	constraint PK_tblSysMgrSettingPersonal primary key(PersonalSettingID),--主键约束。
	constraint UK_tblSysMgrSettingPersonal_PersonalSettingID_SettingID unique(PersonalSettingID,SettingID),--唯一约束。
	constraint FK_tblSysMgrSettingPersonal_tblSysMgrSetting_SettingID foreign key(SettingID) references tblSysMgrSetting(SettingID) --外键约束。
)
go
----------------------------------------------------------------------------------------------
--注册系统部件模板。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrRegWebPartTemplate')
begin
	print 'drop table tblSysMgrRegWebPartTemplate'
	drop table tblSysMgrRegWebPartTemplate
end
go
	print 'create table tblSysMgrRegWebPartTemplate'
go
create table tblSysMgrRegWebPartTemplate
(
	WebPartTemplateID		GUIDEx,--部件模板ID。
	WebPartTemplateName		nvarchar(50),--部件名称。
	WebPartTemplatePath		nvarchar(1024),--部件地址。
	
	--WebPartStatus			int default(0),--状态（0-停用，1-启用）。
	
	Description				nvarchar(512),--描述。
	
	constraint PK_tblSysMgrRegWebPartTemplate primary key(WebPartTemplateID),--主键约束。
	constraint UK_tblSysMgrRegWebPartTemplate_WebPartTemplateName unique(WebPartTemplateName)--唯一约束。
)
go
----------------------------------------------------------------------------------------------
--注册部件属性。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrRegWebPartTemplateProperty')
begin
	print 'drop table tblSysMgrRegWebPartTemplateProperty'
	drop table tblSysMgrRegWebPartTemplateProperty
end
go
	print 'create table tblSysMgrRegWebPartTemplateProperty'
go
create table tblSysMgrRegWebPartTemplateProperty
(
	TemplatePropertyID		GUIDEx,--属性ID。
	WebPartTemplateID		GUIDEx,--部件模板ID。
	
	TemplatePropertyName	nvarchar(128),--属性名称。
	TemplateDefaultValue	nvarchar(1024),--默认值。
		
	Description		nvarchar(512),--描述。
	
	constraint PK_tblSysMgrRegWebPartTemplateProperty primary key(TemplatePropertyID),--主键约束。
	constraint UK_tblSysMgrRegWebPartTemplateProperty_WebPartTemplateID_TemplatePropertyName unique(WebPartTemplateID,TemplatePropertyName),--唯一约束。
	constraint FK_tblSysMgrRegWebPartTemplateProperty_tblSysMgrRegWebPartTemplate_WebPartTemplateID foreign key(WebPartTemplateID) references tblSysMgrRegWebPartTemplate(WebPartTemplateID) --外键约束。
)
go
----------------------------------------------------------------------------------------------
--部件设置。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrWebPart')
begin
	print 'drop table tblSysMgrWebPart'
	drop table tblSysMgrWebPart
end
go
	print 'create table tblSysMgrWebPart'
go
create table tblSysMgrWebPart
(
	WebPartID				GUIDEx,--部件ID。
	WebPartName				nvarchar(50),--部件名称。
	WebPartTemplateID		GUIDEx,--部件模板ID。

	DataAssemblyName		nvarchar(512),--数据程序集。
	DataClassName			nvarchar(512),--数据提供接口类名称，实现IWebPartData接口。
	
	WebPartStatus			int default(0),--状态（0-停用，1-启用）。
	
	Description				nvarchar(512),--描述。
	
	constraint PK_tblSysMgrWebPart primary key(WebPartID),--主键约束。
	constraint UK_tblSysMgrWebPart_WebPartName unique(WebPartName),--唯一约束。
	constraint FK_tblSysMgrWebPart_tblSysMgrRegWebPartTemplate_WebPartTemplateID foreign key(WebPartTemplateID) references tblSysMgrRegWebPartTemplate(WebPartTemplateID)--外键约束。
)
go
----------------------------------------------------------------------------------------------
--部件属性设置。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrWebPartProperty')
begin
	print 'drop table tblSysMgrWebPartProperty'
	drop table tblSysMgrWebPartProperty
end
go
	print 'create table tblSysMgrWebPartProperty'
go
create table tblSysMgrWebPartProperty
(
	WebPartID				GUIDEx,--部件ID。
	TemplatePropertyID		GUIDEx,--属性ID。
	
	PropertyValue			nvarchar(1024),--属性值。
	
	constraint PK_tblSysMgrWebPartProperty primary key(WebPartID,TemplatePropertyID),--主键约束。
	constraint FK_tblSysMgrWebPartProperty_tblSysMgrWebPart_WebPartID foreign key(WebPartID) references tblSysMgrWebPart(WebPartID),--外键约束。
	constraint FK_tblSysMgrWebPartProperty_tblSysMgrRegWebPartTemplateProperty_TemplatePropertyID foreign key(TemplatePropertyID) references tblSysMgrRegWebPartTemplateProperty(TemplatePropertyID) --外键约束。
)
go
----------------------------------------------------------------------------------------------
--WebPart的显示位置定义。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrWebPartZone')
begin
	print 'drop table tblSysMgrWebPartZone'
	drop table tblSysMgrWebPartZone
end
go
	print 'create table tblSysMgrWebPartZone'
go
create table tblSysMgrWebPartZone
(
	ZoneID		GUIDEx,--显示位置ID。
	ZoneName	nvarchar(50),--显示位置名称。
	
	AppAuthID	GUIDEx null,--访问授权ID(为空则为所有默认首页)。
	ZoneMode	int,--显示模式（0-左，1-中，2-右）
	ZoneLength	int default(0),--最大显示数目（为0则不限制）。
	
	Description	nvarchar(256),--描述。
	
	constraint PK_tblSysMgrWebPartZone primary key(ZoneID),--主键约束。
	constraint UK_tblSysMgrWebPartZone_ZoneName unique(ZoneName) --唯一约束。
)
go
----------------------------------------------------------------------------------------------
--实例化WebPart，控制页面显示和页面数据。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrWebPartPersonal')
begin
	print 'drop table tblSysMgrWebPartPersonal'
	drop table tblSysMgrWebPartPersonal
end
go
	print 'create table tblSysMgrWebPartPersonal'
go
create table tblSysMgrWebPartPersonal
(
	PersonalWebPartID	GUIDEx,--实例化WebPartID。
	WebPartID			GUIDEx,--WebPart ID。
	
	EmployeeID		GUIDEx null,--用户ID(为空时表示全局)。
	EmployeeName	nvarchar(50) null,--用户名称(为空时表示全局)。
	
	ZoneID				GUIDEx,--显示具体呈现位置ID。
	
	OrderNo				int default(1),--部件呈现顺序。
	
	constraint PK_tblSysMgrWebPartPersonal primary key(PersonalWebPartID),--主键约束。
	constraint FK_tblSysMgrWebPartPersonal_tblSysMgrWebPart_WebPartID foreign key(WebPartID) references tblSysMgrWebPart(WebPartID),--外键约束。
	constraint FK_tblSysMgrWebPartPersonal_tblSysMgrWebPartZone_ZoneID foreign key(ZoneID) references tblSysMgrWebPartZone(ZoneID)--外键约束。
)
go
----------------------------------------------------------------------------------------------
--常用链接。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSysMgrLinks')
begin
	print 'drop table tblSysMgrLinks'
	drop table tblSysMgrLinks
end
go
	print 'create table tblSysMgrLinks'
go
create table tblSysMgrLinks
(
	LinkID		GUIDEx,--链接ID。
	LinkName	nvarchar(128) not null,--链接名称。
	LinkUrl		nvarchar(512),--链接地址。
	LinkTarget	int default(0),--链接方式（0-blank,1-self）。
	LinkStatus	int default(0),--链接状态（0-停用，1-启用，2-申请）。
	
	ImageUrlID	GUIDEx null,--链接显示图片ID。
	
	EmployeeID		GUIDEx null,--用户ID(为空时表示全局)。
	EmployeeName	nvarchar(50) null,--用户名称(为空时表示全局)。
	
	Description		nvarchar(256),--描述信息。
	
	OrderNo			int not null default(1),--序号。
	
	constraint PK_tblSysMgrLinks primary key(LinkID) --主键约束。
)
go
----------------------------------------------------------------------------------------------
