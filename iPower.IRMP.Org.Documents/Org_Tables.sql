/*
//================================================================================
//  FileName: Org_Tables.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/25
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
-------------------------------------------------------------------------------------------------------------------
--删除表
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgEmployeePost')
begin
	print 'drop table tblOrgEmployeePost'
	drop table tblOrgEmployeePost
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgPost')
begin
	print 'drop table tblOrgPost'
	drop table tblOrgPost
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgLeaderSubCharge')
begin
	print 'drop table tblOrgLeaderSubCharge'
	drop table tblOrgLeaderSubCharge
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgEmployeeSystem')
begin
	print 'drop table tblOrgEmployeeSystem'
	drop table tblOrgEmployeeSystem
end
go
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgEmployee')
begin
	print 'drop table tblOrgEmployee'
	drop table tblOrgEmployee
end
go
-------------------------------------------------------------------------------------------------------------------
--组织结构表，描述整个系统的组织架构
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgDepartment')
begin
	print 'drop table tblOrgDepartment'
	drop table tblOrgDepartment
end
go
	print 'create table tblOrgDepartment'
go
create table tblOrgDepartment
(
	DepartmentID			GUIDEx	not null,---组织ID。
	ParentDepartmentID		GUIDEx	null,	 ---上级组织iD。
	DepartmentSign			nvarchar(256),	 ---组织标识。
	DepartmentName			nvarchar(256) not null,--组织名称。
	DepartmentDescription	nvarchar(1024),--组织描述。
	
	DepartmentOrder			int default(0),--排列顺序。
	DepartmentLevel			int	default(0),--组织层次。
	DepartmentStatus		int default(0),--组织状态。
	
	DepartmentAddress		nvarchar(255) default null,--地址。
	DepartmentFax			nvarchar(32)  default null,--传真。
	DepartmentTel			nvarchar(32)  default null,--电话。
	DepartmentLeader		nvarchar(32)  default null,--法人或负责人。
	DepartmentCapability	int	default(0),--组织容量，为0表示不限制。
	
	DepartmentEx1			nvarchar(255) null,--扩展字段一。
	DepartmentEx2			nvarchar(255) null,--扩展字段二。
	DepartmentEx3			nvarchar(255) null,--扩展字段三。
	DepartmentEx4			nvarchar(255) null,--扩展字段四。
	
	constraint PK_tblOrgDepartment primary key(DepartmentID),--主键约束。
	constraint UK_tblOrgDepartment_DepartmentSign unique(DepartmentSign)
)
go
-------------------------------------------------------------------------------------------------------------------
--组织中所有岗位级别定义表，表示存在于组织中的所有岗位级别及其关系树型结构。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgRank')
begin
	print 'drop table tblOrgRank'
	drop table tblOrgRank
end
go
	print 'create table tblOrgRank'
go
create table tblOrgRank
(
	RankID				GUIDEx,--岗位级别ID。
	ParentRankID		GUIDEx	default null,--上级岗位级别ID。
	RankName			nvarchar(255),--岗位级别名称。
	RankDescription		nvarchar(1024),--岗位级别描述。
	
	constraint PK_tblOrgRank primary key(RankID)--主键约束。
)
go
-------------------------------------------------------------------------------------------------------------------
--组织中的具体岗位结构，表示存在于组织中的各种岗位树型结构。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgPost')
begin
	print 'drop table tblOrgPost'
	drop table tblOrgPost
end
go
	print 'create table tblOrgPost'
go
create table tblOrgPost
(
	PostID			GUIDEx,--岗位ID。
	ParentPostID	GUIDEx default null,--上级岗位ID。
	
	DepartmentID	GUIDEx,--岗位所在的组织ID。
	RankID			GUIDEx,--岗位级别ID。
	
	PostSign		nvarchar(255),--岗位标识。
	PostName		nvarchar(255),--岗位名称。
	PostDescription	nvarchar(1024),--岗位描述。
	
	constraint PK_tblOrgPost primary key(PostID),--主键约束。
	constraint UK_tblOrgPost_PostSign unique(PostSign), --唯一约束。
	
	constraint FK_tblOrgPost_tblOrgDepartment_DepartmentID foreign key(DepartmentID) references tblOrgDepartment(DepartmentID),--外键约束。
	constraint FK_tblOrgPost_tblOrgRank_RankID foreign key(RankID) references tblOrgRank(RankID) 
)
-------------------------------------------------------------------------------------------------------------------
--用户表，表示该系统所有的用户
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgEmployee')
begin
	print 'drop table tblOrgEmployee'
	drop table tblOrgEmployee
end
go
	print 'create table tblOrgEmployee'
go
create table tblOrgEmployee
(
	EmployeeID			GUIDEx,--用户ID。
	DepartmentID		GUIDEx not null,--用户所在的组织。
	PostID				GUIDEx null,--用户所在的岗位。
	EmployeeSign		nvarchar(64) not null,--用户标识。
	EmployeeName		nvarchar(64) not null,--用户名称。
	NickName			nvarchar(64) default null,--昵称。
	EmployeePassword	nvarchar(64) default null,--用户登陆密码。
	EmployeePassword2	nvarchar(64) default null,--用户登陆临时密码。
	PasswordDate		datetime default(getdate()),--密码设置日期。
	PasswordDate2		datetime default(getdate()),--临时密码设置日期。
	PasswordHistory		nvarchar(512) default null,--密码历史记录，最多记忆５组。
	Gender				int default(0),--用户性别；0：未知　1：男，2：女。
	Birthday			nvarchar(10) default null,--生日。
	Nation				nvarchar(32) default null,--民族。
	IdentityCard		nvarchar(32) default null,--身份证号码。
	MSNNO				nvarchar(64) default null,--ＭＳＮ号码。
	QQNO				nvarchar(32) default null,--ＱＱ号码。

	EmployeeDescription	nvarchar(512),--用户描述。
	EmployeeStatus		int default(0) not null,--用户状态。

	CardID				nvarchar(32) null,--用户卡号。
	Email				nvarchar(512) null,--电子邮件。
	MobileNo			nvarchar(32) null,--移动电话。
	WorkTelNo			nvarchar(32) null,--工作电话。
	Address				nvarchar(512) null,--联系地址。

	OrderNo				int default(0),--序号。

	EmployeeEx1			nvarchar(255) null,--扩展字段1。
	EmployeeEx2			nvarchar(255) null,--扩展字段2。
	EmployeeEx3			nvarchar(255) null,--扩展字段3。
	EmployeeEx4			nvarchar(255) null,--扩展字段4。

	constraint PK_tblOrgEmployee primary key(EmployeeID),--主键约束。
	constraint UK_tblOrgEmployee_EmployeeSign unique(EmployeeSign),--唯一约束。
	constraint FK_tblOrgEmployee_tblOrgDepartment_DepartmentID foreign key(DepartmentID) references tblOrgDepartment(DepartmentID),--外键约束。
	constraint FK_tblOrgEmployee_tblOrgPost_PostID foreign key(PostID) references tblOrgPost(PostID) --外键约束。
)
go
-------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--映射领导及其分管的部门之间的关系表。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgLeaderSubCharge')
begin
	print 'drop table tblOrgLeaderSubCharge'
	drop table tblOrgLeaderSubCharge
end
go
	print 'create table tblOrgLeaderSubCharge'
go
create table tblOrgLeaderSubCharge
(
	EmployeeID		GUIDEx,--用户ID。
	DepartmentID	GUIDEx,--部门ID。
	
	constraint PK_tblOrgLeaderSubCharge primary key(EmployeeID,DepartmentID),--主键约束。
	constraint FK_tblOrgLeaderSubCharge_tblOrgEmployee_EmployeeID foreign key(EmployeeID) references tblOrgEmployee(EmployeeID),--外键约束。
	constraint FK_tblOrgLeaderSubCharge_tblOrgDepartment_DepartmentID foreign key(DepartmentID) references tblOrgDepartment(DepartmentID) --外键约束。
)
go
-------------------------------------------------------------------------------------------------------------------
--用户和系统映射表，激活用户到应用系统的访问。
if exists(select 0 from sysobjects where xtype = 'u' and name = 'tblOrgEmployeeSystem')
begin
	print 'drop table tblOrgEmployeeSystem'
	drop table tblOrgEmployeeSystem
end
go
	print 'create table tblOrgEmployeeSystem'
go
create table tblOrgEmployeeSystem
(
	EmployeeID		GUIDEx,--用户ID。
	SystemID		GUIDEx,--应用系统ID。
	
	constraint PK_tblOrgEmployeeSystem primary key(EmployeeID,SystemID),--主键约束。
	constraint FK_tblOrgEmployeeSystem_tblOrgEmployee_EmployeeID foreign key(EmployeeID) references tblOrgEmployee(EmployeeID) --外键约束。
)	
go
-------------------------------------------------------------------------------------------------------------------
