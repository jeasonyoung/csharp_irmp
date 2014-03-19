/*
//================================================================================
//  FileName: SysMgr_WebPart_Procedure.sql
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/30
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
---------------------------------------------------------------------------------------------------------------
--WebPart管理。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrWebPartFactory')
begin
	print 'drop procedure spSysMgrWebPartFactory'
	drop procedure spSysMgrWebPartFactory
end
go
	print 'create procedure spSysMgrWebPartFactory'
go
create procedure spSysMgrWebPartFactory
(
	@SystemID	nvarchar(50),--系统ID。
	@EmployeeID	nvarchar(50),--用户ID。
	@ZoneMode	int--WebPart布局。
)
as
begin
	declare @AppAuthID nvarchar(50)
	set @AppAuthID = ''
	declare @tempWebPartPersonal table(PersonalWebPartID	nvarchar(50),
									   WebPartID			nvarchar(50),
									   OrderNo				int)
	------------------------------------------------------------------------------------------------------------
	--获取系统授权ID。
	if exists(select 0 from tblSysMgrAppAuthorization where SystemID = @SystemID)
	begin
		select top 1 @AppAuthID = AppAuthID
		from tblSysMgrAppAuthorization
		where SystemID = @SystemID
	end
	------------------------------------------------------------------------------------------------------------
	--获取WebPartPersonal。
	insert into @tempWebPartPersonal(PersonalWebPartID,WebPartID,OrderNo)
	select a.PersonalWebPartID,a.WebPartID,a.OrderNo
	from tblSysMgrWebPartPersonal a
	where dbo.fnIRMPFieldIsNullOrEmpty(a.EmployeeID,@EmployeeID) = @EmployeeID
	and (a.ZoneID in (select ZoneID
					  from tblSysMgrWebPartZone
					  where ZoneMode = @ZoneMode
					  and dbo.fnIRMPFieldIsNullOrEmpty(AppAuthID,@AppAuthID) = @AppAuthID))
	------------------------------------------------------------------------------------------------------------
	--获取所有需要信息。
	select a.PersonalWebPartID as PersonalWebPartID,
	b.DataAssemblyName as AssemblyName,
	b.DataClassName as ClassName,
	c.WebPartTemplatePath as WebPartPath
	from @tempWebPartPersonal a
	inner join tblSysMgrWebPart b
	on b.WebPartID = a.WebPartID and b.WebPartStatus = 1
	inner join tblSysMgrRegWebPartTemplate c
	on c.WebPartTemplateID = b.WebPartTemplateID
	order by a.OrderNo
	------------------------------------------------------------------------------------------------------------
end
go
---------------------------------------------------------------------------------------------------------------
--获取WebPart属性。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrWebPartProperties')
begin
	print 'drop procedure spSysMgrWebPartProperties'
	drop procedure spSysMgrWebPartProperties
end
go
	print 'create procedure spSysMgrWebPartProperties'
go
create procedure spSysMgrWebPartProperties
(
	@PersonalWebPartID	nvarchar(50)--WebPart唯一标识。
)
as
begin
	select b.TemplatePropertyName as PropertyName,
	dbo.fnIRMPFieldIsNullOrEmpty(a.PropertyValue,b.TemplateDefaultValue) as PropertyValue
	from tblSysMgrWebPartProperty a
	inner join tblSysMgrRegWebPartTemplateProperty b
	on b.TemplatePropertyID = a.TemplatePropertyID
	where a.WebPartID in (select WebPartID
						  from tblSysMgrWebPartPersonal
						  where PersonalWebPartID = @PersonalWebPartID)
end
go
---------------------------------------------------------------------------------------------------------------
