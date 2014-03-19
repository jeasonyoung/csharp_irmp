/*
//================================================================================
//  FileName: Security_Tables.sql
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
------------------------------------------------------------------------------------------------
--安全管理
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleRight')
begin
	print 'drop table tblSecurityRoleRight'
	drop table tblSecurityRoleRight
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRight')
begin
	print 'drop table tblSecurityRight'
	drop table tblSecurityRight
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityModule')
begin
	print 'drop table tblSecurityModule'
	drop table tblSecurityModule
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleEmployee')
begin
	print 'drop table tblSecurityRoleEmployee'
	drop table tblSecurityRoleEmployee
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleRank')
begin
	print 'drop table tblSecurityRoleRank'
	drop table tblSecurityRoleRank
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRolePost')
begin
	print 'drop table tblSecurityRolePost'
	drop table tblSecurityRolePost
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleDepartment')
begin
	print 'drop table tblSecurityRoleDepartment'
	drop table tblSecurityRoleDepartment
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleSystem')
begin
	print 'drop table tblSecurityRoleSystem'
	drop table tblSecurityRoleSystem
end
go
------------------------------------------------------------------------------------------------
--应用系统注册
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRegsiter')
begin
	print 'drop table tblSecurityRegsiter'
	drop table tblSecurityRegsiter
end
go
	print 'create table tblSecurityRegsiter'
go
create table tblSecurityRegsiter
(
	SystemID			GUIDEx,--系统ID。
	ParentSystemID		GUIDEx	null,--上级系统。
	SystemSign			nvarchar(256),--系统标识。
	SystemName			nvarchar(256),--系统名称。
	SystemURL			nvarchar(512),--系统入口URL。
	SecurityURL			nvarchar(512),--安全管理入口URL。
	PatchURL			nvarchar(512),--更新下载URL。
	ModuleConfigURL		nvarchar(512),--模块配置URL。
	
	SystemType			int	default(0),--系统类型(1:平台模块;2:应用系统;4:信息展现前台 8 CS系统)。
	SystemStatus		int default(1),--系统状态(0:停用;1:启用)。
	
	SystemDescription	nvarchar(512),--系统描述。
	
	constraint PK_tblSecurityRegsiter primary key(SystemID),--主键约束。
	constraint UK_tblSecurityRegsiter_SystemSign unique(SystemSign)-- 唯一约束。
)
go
------------------------------------------------------------------------------------------------
--系统权限基础资料，包括各个系统涉及权限的功能模块树型结构（目前可为平面结构），该部分在系统稳定后
--不允许修改。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityModule')
begin
	print 'drop table tblSecurityModule'
	drop table tblSecurityModule
end
go
	print 'create table tblSecurityModule'
go
create table tblSecurityModule
(
	SystemID			GUIDEx,--系统ID。
	ModuleID			GUIDEx,--模块ID。
	ParentModuleID		GUIDEx null,--上级模块ID。
	ModuleName			nvarchar(255),--模块名称。
	OrderNo				int,--排序号，同级内按照排序进行。
	
	ModuleStatus		int default(1),--模块状态。0-禁用，1-启用。
	ModuleDescription	nvarchar(1024),--模块描述信息。
	
	constraint PK_tblSecurityModule primary key(ModuleID),--主键约束。
	constraint UK_tblSecurityModule unique(SystemID,ModuleID),--唯一约束。
	constraint FK_tblSecurityModule_tblSecurityRegsiter_SystemID foreign key(SystemID) references tblSecurityRegsiter(SystemID)--外键约束。
)
go
------------------------------------------------------------------------------------------------
--权限基础资料，系统元操作（一般为创建、修改、删除、查看详细信息、列表、权限控制、特殊权限等）
--该部分为系统封装。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityAction')
begin
	print 'drop table tblSecurityAction'
	drop table tblSecurityAction
end
go
	print 'create table tblSecurityAction'
go
create table tblSecurityAction
(
	ActionID			GUIDEx,--元操作ID。
	ActionSign			nvarchar(255),--元操作标识。
	ActionName			nvarchar(255),--元操作名称。
	ActionType			int default(0),--类型(0-基本，1-扩展)。
	ActionDescription	nvarchar(255),--元操作描述。
	
	constraint PK_tblSecurityAction primary key(ActionID),--主键约束。
	constraint UK_tblSecurityAction_ActionSign unique(ActionSign) --唯一约束。
)
go
------------------------------------------------------------------------------------------------
--系统模块和元操作的映射，即系统有效权限集（由系统模块和基于模块的操作控制具体的权限）
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRight')
begin
	print 'drop table tblSecurityRight'
	drop table tblSecurityRight
end
go
	print 'create table tblSecurityRight'
go
create table tblSecurityRight
(
	RightID			GUIDEx,--有效权限ID。
	ModuleID		GUIDEx,--系统模块ID。
	ActionID		GUIDEx,--元操作ID。
	RightName		nvarchar(255),--有效权限名称（元操作和模块映射后的名称）。
	
	constraint PK_tblSecurityRight primary key(RightID),--主键约束。
	constraint UK_tblSecurityRole unique(ModuleID,ActionID),---唯一约束。
	constraint FK_tblSecurityRight_tblSecurityModule_ModuleID foreign key(ModuleID) references tblSecurityModule(ModuleID),--外键约束。
	constraint FK_tblSecurityRight_tblSecurityAction_ActionID foreign key(ActionID) references tblSecurityAction(ActionID) --外键约束。
)
go
------------------------------------------------------------------------------------------------
--角色定义，角色是系统里唯一拥有权限的定义，一个角色可以包含另一个角色（注意无穷递归，在网络结构中避免出现封闭路径）。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRole')
begin
	print 'drop table tblSecurityRole'
	drop table tblSecurityRole
end
go
	print 'create table tblSecurityRole'
go
create table tblSecurityRole
(
	RoleID			GUIDEx,--角色ID。
	RoleName		GUIDEx,--角色名称。
	ParentRoleID	GUIDEx null,--上级角色。
	
	RoleDescription	nvarchar(256),--角色描述。
	RoleStatus		int default(1),--系统状态(0:停用;1:启用)。
	
	constraint PK_tblSecurityRole primary key(RoleID)--主键约束。
)
go
------------------------------------------------------------------------------------------------
--角色系统映射。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleSystem')
begin
	print 'drop table tblSecurityRoleSystem'
	drop table tblSecurityRoleSystem
end
go
	print 'create table tblSecurityRoleSystem'
go
create table tblSecurityRoleSystem
(
	RoleID			GUIDEx,--角色ID。
	SystemID		GUIDEx,--系统ID。
	
	constraint PK_tblSecurityRoleSystem primary key(RoleID,SystemID),--主键约束。
	constraint FK_tblSecurityRoleSystem_tblSecurityRole_RoleID foreign key(RoleID) references tblSecurityRole(RoleID),--外键约束。
	constraint FK_tblSecurityRoleSystem_tblSecurityRegsiter_SystemID foreign key(SystemID) references tblSecurityRegsiter(SystemID)--外键约束。
)
go
------------------------------------------------------------------------------------------------
--角色权限映射
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleRight')
begin
	print 'drop table tblSecurityRoleRight'
	drop table tblSecurityRoleRight
end
go
	print 'create table tblSecurityRoleRight'
go
create table tblSecurityRoleRight
(
	RoleID		GUIDEx,--角色ID。
	RightID		GUIDEx,--权限ID。
	
	constraint PK_tblSecurityRoleRight primary key(RoleID,RightID),--主键约束
	constraint FK_tblSecurityRoleRight_tblSecurityRole_RoleID foreign key(RoleID) references tblSecurityRole(RoleID),--外键约束。
	constraint FK_tblSecurityRoleRight_tblSecurityRight_RightID foreign key(RightID) references tblSecurityRight(RightID) --外键约束。
)
go
------------------------------------------------------------------------------------------------
--角色和用户的映射。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleEmployee')
begin
	print 'drop table tblSecurityRoleEmployee'
	drop table tblSecurityRoleEmployee
end
go
	print 'create table tblSecurityRoleEmployee'
go
create	table tblSecurityRoleEmployee
(
	RoleID			GUIDEx,--角色ID。
	EmployeeID		GUIDEx,--用户ID。
	EmployeeName	nvarchar(50),--用户名称。
	
	constraint PK_tblSecurityRoleEmployee primary key(RoleID,EmployeeID),--主键约束。
	constraint FK_tblSecurityRoleEmployee_tblSecurityRole_RoleID foreign key(RoleID) references tblSecurityRole(RoleID) --外键约束。
)
go
------------------------------------------------------------------------------------------------
--角色和岗位级别的映射。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleRank')
begin
	print 'drop table tblSecurityRoleRank'
	drop table tblSecurityRoleRank
end
go
	print 'create table tblSecurityRoleRank'
go
create table tblSecurityRoleRank
(
	RoleID		GUIDEx,--角色ID。
	RankID		GUIDEx,--岗位级别ID。
	RankName	nvarchar(50),--岗位级别名称。
	
	constraint PK_tblSecurityRoleRank primary key(RoleID,RankID),--主键约束。
	constraint FK_tblSecurityRoleRank_tblSecurityRole_RoleID foreign key(RoleID) references tblSecurityRole(RoleID) --外键约束。
)
go
------------------------------------------------------------------------------------------------
--角色和岗位的映射。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRolePost')
begin
	print 'drop table tblSecurityRolePost'
	drop table tblSecurityRolePost
end
go
	print 'create table tblSecurityRolePost'
go
create table tblSecurityRolePost
(
	RoleID		GUIDEx,--角色ID。
	PostID		GUIDEx,--岗位ID。
	PostName	nvarchar(50),--岗位名称。
	
	constraint FK_tblSecurityRolePost primary key(RoleID,PostID),--主键约束。
	constraint FK_tblSecurityRolePost_tblSecurityRole_RoleID foreign key(RoleID) references tblSecurityRole(RoleID) --外键约束。
)
go
------------------------------------------------------------------------------------------------
--角色和部门的映射。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblSecurityRoleDepartment')
begin
	print 'drop table tblSecurityRoleDepartment'
	drop table tblSecurityRoleDepartment
end
go
	print 'create table tblSecurityRoleDepartment'
go
create table tblSecurityRoleDepartment
(
	RoleID			GUIDEx,--角色ID。
	DepartmentID	GUIDEx,--部门ID。
	DepartmentName	nvarchar(50),--部门名称。
	
	constraint PK_tblSecurityRoleDepartment primary key(RoleID,DepartmentID),--主键约束。
	constraint FK_tblSecurityRoleDepartment_tblSecurityRole_RoleID foreign key(RoleID) references tblSecurityRole(RoleID) --外键约束。
)
go
------------------------------------------------------------------------------------------------
