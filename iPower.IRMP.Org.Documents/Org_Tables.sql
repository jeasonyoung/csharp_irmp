/*
//================================================================================
//  FileName: Org_Tables.sql
//  Desc:
//
//  Called by
//
//  Auth:���£�jeason1914@gmail.com��
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
--ɾ����
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
--��֯�ṹ����������ϵͳ����֯�ܹ�
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
	DepartmentID			GUIDEx	not null,---��֯ID��
	ParentDepartmentID		GUIDEx	null,	 ---�ϼ���֯iD��
	DepartmentSign			nvarchar(256),	 ---��֯��ʶ��
	DepartmentName			nvarchar(256) not null,--��֯���ơ�
	DepartmentDescription	nvarchar(1024),--��֯������
	
	DepartmentOrder			int default(0),--����˳��
	DepartmentLevel			int	default(0),--��֯��Ρ�
	DepartmentStatus		int default(0),--��֯״̬��
	
	DepartmentAddress		nvarchar(255) default null,--��ַ��
	DepartmentFax			nvarchar(32)  default null,--���档
	DepartmentTel			nvarchar(32)  default null,--�绰��
	DepartmentLeader		nvarchar(32)  default null,--���˻����ˡ�
	DepartmentCapability	int	default(0),--��֯������Ϊ0��ʾ�����ơ�
	
	DepartmentEx1			nvarchar(255) null,--��չ�ֶ�һ��
	DepartmentEx2			nvarchar(255) null,--��չ�ֶζ���
	DepartmentEx3			nvarchar(255) null,--��չ�ֶ�����
	DepartmentEx4			nvarchar(255) null,--��չ�ֶ��ġ�
	
	constraint PK_tblOrgDepartment primary key(DepartmentID),--����Լ����
	constraint UK_tblOrgDepartment_DepartmentSign unique(DepartmentSign)
)
go
-------------------------------------------------------------------------------------------------------------------
--��֯�����и�λ���������ʾ��������֯�е����и�λ�������ϵ���ͽṹ��
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
	RankID				GUIDEx,--��λ����ID��
	ParentRankID		GUIDEx	default null,--�ϼ���λ����ID��
	RankName			nvarchar(255),--��λ�������ơ�
	RankDescription		nvarchar(1024),--��λ����������
	
	constraint PK_tblOrgRank primary key(RankID)--����Լ����
)
go
-------------------------------------------------------------------------------------------------------------------
--��֯�еľ����λ�ṹ����ʾ��������֯�еĸ��ָ�λ���ͽṹ��
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
	PostID			GUIDEx,--��λID��
	ParentPostID	GUIDEx default null,--�ϼ���λID��
	
	DepartmentID	GUIDEx,--��λ���ڵ���֯ID��
	RankID			GUIDEx,--��λ����ID��
	
	PostSign		nvarchar(255),--��λ��ʶ��
	PostName		nvarchar(255),--��λ���ơ�
	PostDescription	nvarchar(1024),--��λ������
	
	constraint PK_tblOrgPost primary key(PostID),--����Լ����
	constraint UK_tblOrgPost_PostSign unique(PostSign), --ΨһԼ����
	
	constraint FK_tblOrgPost_tblOrgDepartment_DepartmentID foreign key(DepartmentID) references tblOrgDepartment(DepartmentID),--���Լ����
	constraint FK_tblOrgPost_tblOrgRank_RankID foreign key(RankID) references tblOrgRank(RankID) 
)
-------------------------------------------------------------------------------------------------------------------
--�û�����ʾ��ϵͳ���е��û�
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
	EmployeeID			GUIDEx,--�û�ID��
	DepartmentID		GUIDEx not null,--�û����ڵ���֯��
	PostID				GUIDEx null,--�û����ڵĸ�λ��
	EmployeeSign		nvarchar(64) not null,--�û���ʶ��
	EmployeeName		nvarchar(64) not null,--�û����ơ�
	NickName			nvarchar(64) default null,--�ǳơ�
	EmployeePassword	nvarchar(64) default null,--�û���½���롣
	EmployeePassword2	nvarchar(64) default null,--�û���½��ʱ���롣
	PasswordDate		datetime default(getdate()),--�����������ڡ�
	PasswordDate2		datetime default(getdate()),--��ʱ�����������ڡ�
	PasswordHistory		nvarchar(512) default null,--������ʷ��¼�������䣵�顣
	Gender				int default(0),--�û��Ա�0��δ֪��1���У�2��Ů��
	Birthday			nvarchar(10) default null,--���ա�
	Nation				nvarchar(32) default null,--���塣
	IdentityCard		nvarchar(32) default null,--���֤���롣
	MSNNO				nvarchar(64) default null,--�ͣӣκ��롣
	QQNO				nvarchar(32) default null,--�ѣѺ��롣

	EmployeeDescription	nvarchar(512),--�û�������
	EmployeeStatus		int default(0) not null,--�û�״̬��

	CardID				nvarchar(32) null,--�û����š�
	Email				nvarchar(512) null,--�����ʼ���
	MobileNo			nvarchar(32) null,--�ƶ��绰��
	WorkTelNo			nvarchar(32) null,--�����绰��
	Address				nvarchar(512) null,--��ϵ��ַ��

	OrderNo				int default(0),--��š�

	EmployeeEx1			nvarchar(255) null,--��չ�ֶ�1��
	EmployeeEx2			nvarchar(255) null,--��չ�ֶ�2��
	EmployeeEx3			nvarchar(255) null,--��չ�ֶ�3��
	EmployeeEx4			nvarchar(255) null,--��չ�ֶ�4��

	constraint PK_tblOrgEmployee primary key(EmployeeID),--����Լ����
	constraint UK_tblOrgEmployee_EmployeeSign unique(EmployeeSign),--ΨһԼ����
	constraint FK_tblOrgEmployee_tblOrgDepartment_DepartmentID foreign key(DepartmentID) references tblOrgDepartment(DepartmentID),--���Լ����
	constraint FK_tblOrgEmployee_tblOrgPost_PostID foreign key(PostID) references tblOrgPost(PostID) --���Լ����
)
go
-------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--ӳ���쵼����ֹܵĲ���֮��Ĺ�ϵ��
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
	EmployeeID		GUIDEx,--�û�ID��
	DepartmentID	GUIDEx,--����ID��
	
	constraint PK_tblOrgLeaderSubCharge primary key(EmployeeID,DepartmentID),--����Լ����
	constraint FK_tblOrgLeaderSubCharge_tblOrgEmployee_EmployeeID foreign key(EmployeeID) references tblOrgEmployee(EmployeeID),--���Լ����
	constraint FK_tblOrgLeaderSubCharge_tblOrgDepartment_DepartmentID foreign key(DepartmentID) references tblOrgDepartment(DepartmentID) --���Լ����
)
go
-------------------------------------------------------------------------------------------------------------------
--�û���ϵͳӳ��������û���Ӧ��ϵͳ�ķ��ʡ�
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
	EmployeeID		GUIDEx,--�û�ID��
	SystemID		GUIDEx,--Ӧ��ϵͳID��
	
	constraint PK_tblOrgEmployeeSystem primary key(EmployeeID,SystemID),--����Լ����
	constraint FK_tblOrgEmployeeSystem_tblOrgEmployee_EmployeeID foreign key(EmployeeID) references tblOrgEmployee(EmployeeID) --���Լ����
)	
go
-------------------------------------------------------------------------------------------------------------------
