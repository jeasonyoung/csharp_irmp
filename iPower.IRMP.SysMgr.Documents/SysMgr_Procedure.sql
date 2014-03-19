/*
//================================================================================
//  FileName: SysMgr_Procedure.sql
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
---------------------------------------------------------------------------------------------------------------
--系统验证。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrAppAuthorizedToVerify')
begin
	print 'drop procedure spSysMgrAppAuthorizedToVerify'
	drop procedure spSysMgrAppAuthorizedToVerify
end
go
	print 'create procedure spSysMgrAppAuthorizedToVerify'
go
create procedure spSysMgrAppAuthorizedToVerify
(
	@SystemID		GUIDEx,--系统ID。
	@AuthPassword	nvarchar(50)--授权密码。
)
as
begin
	declare @result nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = '系统已授权'
	-------------------------------------------------------------------------
	--是否被授权。
	if not exists(select 0 from tblSysMgrAppAuthorization where SystemID = @SystemID)
	begin
		set @resultCode = -1
		set @result = '系统[系统ID：'+@SystemID+']未被授权！'
	end
	--验证授权密码。
	if(@resultCode = 0 and not exists(select 0 from tblSysMgrAppAuthorization where SystemID = @SystemID and AuthPwd = @AuthPassword))
	begin
		set @resultCode = -1
		set @result = '系统授权密码错误！'
	end
	--验证状态。
	if(@resultCode = 0 and exists(select 0 from tblSysMgrAppAuthorization where SystemID = @SystemID and AuthPwd = @AuthPassword and AuthStatus = 0))
	begin
		set @resultCode = -1
		set @result = '系统授权被停止！'
	end
	-------------------------------------------------------------------------
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--用户验证。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrEmployeeAuthentication')
begin
	print 'drop procedure spSysMgrEmployeeAuthentication'
	drop procedure spSysMgrEmployeeAuthentication
end
go
	print 'create procedure spSysMgrEmployeeAuthentication'
go
create procedure spSysMgrEmployeeAuthentication
(
	@EmployeeID	GUIDEx,--用户ID。
	@SystemID	GUIDEx,--系统ID。
	@ClientIP	nvarchar(20)--IP地址。
)
as
begin
	declare @result nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = '用户已被系统授权。'
	------------------------------------------------------------------------------
	--tblSysMgrAppAuthorization 获取系统访问授权ID
	declare @AppAuthID nvarchar(32)--访问授权ID。
	declare @SystemName nvarchar(256)--系统名称。
	------------------------------------------------------------------------------
	select top 1 @AppAuthID = AppAuthID,@SystemName = SystemName 
	from tblSysMgrAppAuthorization 
	where SystemID = @SystemID
	------------------------------------------------------------------------------
	if not exists(select 0 from tblSysMgrAppAuthorization where SystemID = @SystemID and AuthStatus = 1)
	begin
		set @resultCode = -1
		set @result = '该系统[系统ID：'+@SystemID+']未被访问授权！'
	end
	--tblSysMgrEmployeeAuthorization
	if(@resultCode = 0 and not exists(select 0 from tblSysMgrEmployeeAuthorization where AppAuthID = @AppAuthID and EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户未被授权访问['+@SystemName+']系统！'
	end
	--tblSysMgrLimitRefusedIPAddr
 	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitRefusedIPAddr where RefusedIPAddr = @ClientIP and dbo.fnIRMPFieldIsNullOrEmpty(EmployeeID,@EmployeeID) = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户IP地址['+@EmployeeID+']访问被拒绝！'
	end
	--tblSysMgrLimitBindIPAddr
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitBindIPAddr where dbo.fnIRMPFieldIsNullOrEmpty(EmployeeID,@EmployeeID) = @EmployeeID))
	begin
		--绑定IP地址。
		if not exists(select 0 from tblSysMgrLimitBindIPAddr where  dbo.fnIRMPFieldIsNullOrEmpty(EmployeeID,@EmployeeID) = @EmployeeID and BindIPAddr = @ClientIP)
		begin
			set @resultCode = -1
			set @result = '用户IP地址未在绑定的IP地址列表中！'
		end
	end
	--tblSysMgrLimitSpecifyTimeZone
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitSpecifyTimeZone where dbo.fnIRMPFieldIsNullOrEmpty(EmployeeID,@EmployeeID) = @EmployeeID))
	begin
		--时间区间。
		if exists(select 0 from tblSysMgrLimitSpecifyTimeZone where dbo.fnIRMPFieldIsNullOrEmpty(EmployeeID,@EmployeeID) = @EmployeeID and (getdate() between StartTime and EndTime) and AuthStatus = 0)
		begin
			set @resultCode = -1
			set @result = '用户在被限制的时间区间内！'
		end else if(exists(select 0 from tblSysMgrLimitSpecifyTimeZone where dbo.fnIRMPFieldIsNullOrEmpty(EmployeeID,@EmployeeID) = @EmployeeID and (getdate() > EndTime) and AuthStatus = 1))
		begin
			set @resultCode = -1
			set @result = '用户不在被授权使用的时间区间内！'
		end
	end
	--tblSysMgrLimitLogin
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitLogin where dbo.fnIRMPFieldIsNullOrEmpty(EmployeeID,@EmployeeID) = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户被限制登录！'
	end
	------------------------------------------------------------------------------
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--删除系统访问授权。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrDeleteAppAuthorization')
begin
	print 'drop procedure spSysMgrDeleteAppAuthorization'
	drop procedure spSysMgrDeleteAppAuthorization
end
go
	print 'create procedure spSysMgrDeleteAppAuthorization'
go
create procedure spSysMgrDeleteAppAuthorization
(
	@AppAuthID	GUIDEx--访问授权ID。
)
as
begin
	declare @result nvarchar(256)
	declare @SystemName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	------------------------------------------------------------------
	select top 1 @SystemName = SystemName
	from tblSysMgrAppAuthorization
	where AppAuthID = @AppAuthID
	------------------------------------------------------------------
	--tblSysMgrEmployeeAuthorization
	if(@resultCode = 0 and exists(select 0 from tblSysMgrEmployeeAuthorization where AppAuthID = @AppAuthID))
	begin
		set @resultCode = -1
		set @result = '用户访问授权中包含有对['+@SystemName+']系统用户授权，请先将其删除！' 
	end
	------------------------------------------------------------------
	--tblSysMgrSetting
	if(@resultCode = 0 and exists(select 0 from tblSysMgrSetting where AppAuthID = @AppAuthID))
	begin
		set @resultCode = -1
		set @result = '系统参数设置中包含有对['+@SystemName+']系统的设置，请先将其删除！'
	end
	------------------------------------------------------------------
	--tblSysMgrWebPartZone
	if(@resultCode = 0 and exists(select 0 from tblSysMgrWebPartZone where AppAuthID = @AppAuthID))
	begin
		set @resultCode = -1
		set @result = '部件显示位置设置中包含有对['+@SystemName+']系统的设置，请先将其删除！'
	end
	------------------------------------------------------------------
	if(@resultCode = 0)
	begin
		delete from tblSysMgrAppAuthorization where AppAuthID = @AppAuthID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--用户授权访问。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrEmployeeAuthorizationListView')
begin
	print 'drop procedure spSysMgrEmployeeAuthorizationListView'
	drop procedure spSysMgrEmployeeAuthorizationListView
end
go
	print 'create procedure spSysMgrEmployeeAuthorizationListView'
go
create procedure spSysMgrEmployeeAuthorizationListView
(
	@EmployeeName nvarchar(50)--用户名称。
)
as
begin
	select EmployeeID,EmployeeName, dbo.fnSysMgrEmployeeAuthorization(EmployeeID) as AppSystemName
	from tblSysMgrEmployeeAuthorization
	where EmployeeName like '%'+@EmployeeName+'%'
	group by EmployeeID,EmployeeName
end
go
---------------------------------------------------------------------------------------------------------------
--删除用户授权访问
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrDeleteEmployeeAuthorization')
begin
	print 'drop procedure spSysMgrDeleteEmployeeAuthorization'
	drop procedure spSysMgrDeleteEmployeeAuthorization
end
go
	print 'create procedure spSysMgrDeleteEmployeeAuthorization'
go
create procedure spSysMgrDeleteEmployeeAuthorization
(
	@EmployeeID	GUIDEx--用户ID。
)
as
begin
	declare @result nvarchar(256)
	declare @EmployeeName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	------------------------------------------------------------------
	select top 1 @EmployeeName = EmployeeName
	from tblSysMgrEmployeeAuthorization
	where EmployeeID = @EmployeeID
	------------------------------------------------------------------
	--tblSysMgrLimitRefusedIPAddr
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitRefusedIPAddr where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户['+@EmployeeName+']在用户限制 拒绝IP中被使用，请先将其删除！'
	end
	--tblSysMgrLimitBindIPAddr
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitBindIPAddr where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户['+@EmployeeName+']在用户限制 绑定IP中被使用，请先将其删除！'
	end
	--tblSysMgrLimitSpecifyTimeZone
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitSpecifyTimeZone where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户['+@EmployeeName+']在用户限制 指定时间区间中被使用，请先将其删除！'
	end
	--tblSysMgrLimitLogin
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLimitLogin where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户['+@EmployeeName+']在用户限制 限制登录中被使用，请先将其删除！'
	end
	--tblSysMgrSettingPersonal
	if(@resultCode = 0 and exists(select 0 from tblSysMgrSettingPersonal where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户['+@EmployeeName+']在参数设置中被使用，请先将其删除！'
	end
	--tblSysMgrWebPartPersonal
	if(@resultCode = 0 and exists(select 0 from tblSysMgrWebPartPersonal where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户['+@EmployeeName+']在首页设置中被使用，请先将其删除！'
	end
	--tblSysMgrLinks
	if(@resultCode = 0 and exists(select 0 from tblSysMgrLinks where EmployeeID = @EmployeeID))
	begin
		set @resultCode = -1
		set @result = '用户['+@EmployeeName+']在链接管理中被使用，请先将其删除！'
	end
	------------------------------------------------------------------
	if(@resultCode = 0)
	begin
		delete from tblSysMgrEmployeeAuthorization where EmployeeID = @EmployeeID
	end
	
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--授权用户数据列表
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrAuthEmployee')
begin
	print 'drop procedure spSysMgrAuthEmployee'
	drop procedure spSysMgrAuthEmployee
end
go
	print 'create procedure spSysMgrAuthEmployee'
go
create procedure spSysMgrAuthEmployee
(
	@EmployeeName	nvarchar(50)='' --用户姓名。
)
as
begin
	select distinct EmployeeID,EmployeeName
	from tblSysMgrEmployeeAuthorization
	where EmployeeName like '%'+@EmployeeName+'%'
end
go
---------------------------------------------------------------------------------------------------------------
--绑定用户访问授权数据。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrEmployeeAuthorizationApp')
begin
	print 'drop procedure spSysMgrEmployeeAuthorizationApp'
	drop procedure spSysMgrEmployeeAuthorizationApp
end
go
	print 'create procedure spSysMgrEmployeeAuthorizationApp'
go
create procedure spSysMgrEmployeeAuthorizationApp
(
	@EmployeeID	GUIDEx, --用户ID。
	@IsAuth		int = 0 --是否授权
)
as
begin
	if(@IsAuth = 0) --未授权。
	begin
		select AppAuthID,SystemName
		from tblSysMgrAppAuthorization
		where AppAuthID not in (select  AppAuthID 
								from tblSysMgrEmployeeAuthorization
								where EmployeeID = @EmployeeID)
	end else begin --已授权。
		select AppAuthID,SystemName
		from tblSysMgrAppAuthorization
		where AppAuthID in (select  AppAuthID 
							from tblSysMgrEmployeeAuthorization
							where EmployeeID = @EmployeeID)
	end
end
go
---------------------------------------------------------------------------------------------------------------
--系统参数设置数据列表
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrSettingListView')
begin
	print 'drop procedure spSysMgrSettingListView'
	drop procedure spSysMgrSettingListView
end
	print 'create procedure spSysMgrSettingList'
go
create procedure spSysMgrSettingListView
(
	@SystemName nvarchar(256)  --系统名称。
)
as
begin
	select a.AppAuthID,b.SystemName,a.SettingID,a.SettingType,a.SettingSign,a.DefaultValue,a.Description 
	from tblSysMgrSetting a 
	inner join tblSysMgrAppAuthorization b 
	on a.AppAuthID=b.AppAuthID
	where b.SystemName like '%'+@SystemName+'%'
end
go
---------------------------------------------------------------------------------------------------------------
--删除系统参数设置
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrDeleteSetting')
begin
	print 'drop procedure spSysMgrDeleteSetting'
	drop procedure spSysMgrDeleteSetting
end
	print 'create procedure spSysMgrDeleteSetting'
go
create procedure spSysMgrDeleteSetting
(
	@SettingID GUIDEx--配置ID
)
as
begin
	declare @result nvarchar(256)
	declare @SettingSign nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	---------------------------------------------------------
	select top 1 @SettingSign=SettingSign 
	from tblSysMgrSetting
	where SettingID=@SettingID
	---------------------------------------------------------
	--实例化系统参数设置。tblSysMgrSettingPersonal
	if(@resultCode = 0 and exists(select 0 from tblSysMgrSettingPersonal where SettingID=@SettingID ))
	begin
		set @resultCode=1
		set @result = '配置符号['+@SettingSign+']设置管理-参数设置中被使用，请先将其删除！'
	end
	---------------------------------------------------------
							--预留--
	---------------------------------------------------------
	if(@resultCode = 0)
	begin
		delete from tblSysMgrSetting where SettingID = @SettingID
	end
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--删除系统注册部件模板。
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrDeleteRegWebPartTemplate')
begin
	print 'drop procedure spSysMgrDeleteRegWebPartTemplate'
	drop procedure spSysMgrDeleteRegWebPartTemplate
end
	print 'create procedure spSysMgrDeleteRegWebPartTemplate'
go
create procedure spSysMgrDeleteRegWebPartTemplate
(
	@WebPartTemplateID		GUIDEx --部件模板ID。
)
as
begin
	declare @result nvarchar(256)
	declare @WebPartTemplateName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	-------------------------------------------------------
	select top 1 @WebPartTemplateName = WebPartTemplateName
	from tblSysMgrRegWebPartTemplate
	where WebPartTemplateID = @WebPartTemplateID
	-------------------------------------------------------
	----部件设置     tblSysMgrWebPart
	if(@resultCode = 0 and exists(select 0 from tblSysMgrWebPart where WebPartTemplateID=@WebPartTemplateID))
	begin
		set @resultCode = -1
		set @result='['+@WebPartTemplateName+']在部件设置中被使用，请先将其删除！'
	end
	if(@resultCode = 0)
	begin
		delete from tblSysMgrRegWebPartTemplateProperty where WebPartTemplateID = @WebPartTemplateID
		delete from tblSysMgrRegWebPartTemplate where WebPartTemplateID = @WebPartTemplateID		
	end
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--系统部件设置列表
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrWebPartListView')
begin
	print 'drop procedure spSysMgrWebPartListView'
	drop procedure spSysMgrWebPartListView
end
	print 'create procedure spSysMgrWebPartListView'
go
create procedure spSysMgrWebPartListView
(
	@WebPartName  nvarchar(50)--部件名称。
)
as
begin
	select a.WebPartID,a.WebPartName,b.WebPartTemplateName,
		   a.DataAssemblyName,a.DataClassName,a.WebPartStatus
	from tblSysMgrWebPart a 
	inner join tblSysMgrRegWebPartTemplate b 
	on a.WebPartTemplateID = b.WebPartTemplateID
	where a.WebPartName like '%'+@WebPartName+'%'
end
go
---------------------------------------------------------------------------------------------------------------
--部件属性列表。
if exists(select 0 from sysobjects where xtype = 'p' and name = 'spSysMgrWebPartPropertyListView')
begin
	print 'drop procedure spSysMgrWebPartPropertyListView'
	drop procedure spSysMgrWebPartPropertyListView
end
go
	print 'create procedure spSysMgrWebPartPropertyListView'
go
create procedure spSysMgrWebPartPropertyListView
(
	@WebPartID	GUIDEx --部件ID。
)
as
begin
	select a.TemplatePropertyID as PropertyID,b.TemplatePropertyName as PropertyName,a.PropertyValue,b.Description as PropertyDescription
	from tblSysMgrWebPartProperty a
	inner join tblSysMgrRegWebPartTemplateProperty b
	on b.TemplatePropertyID = a.TemplatePropertyID
	where a.WebPartID = @WebPartID
	order by b.TemplatePropertyName
end
go
---------------------------------------------------------------------------------------------------------------
--删除系统部件
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrDeleteWebPart')
begin
	print 'drop procedure spSysMgrDeleteWebPart'
	drop procedure spSysMgrDeleteWebPart
end
	print 'create procedure spSysMgrDeleteWebPart'
go
create procedure spSysMgrDeleteWebPart
(
	@WebPartID	GUIDEx--部件ID。	
)
as
begin
	declare @result nvarchar(256)
	declare @WebPartName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
	---------------------------------------------------------
	select top 1 @WebPartName=WebPartName 
	from tblSysMgrWebPart
	where WebPartID=@WebPartID
	---------------------------------------------------------
	--实例化WebPart，控制页面显示和页面数据。
	if(@resultCode = 0 and exists (select 0 from tblSysMgrWebPartPersonal where WebPartID=@WebPartID))
	begin
		set @resultCode=1
		set @result='['+@WebPartName+']在首页设置中被使用，请先将其删除！'
	end
	if(@resultCode = 0)
	begin
		delete from tblSysMgrWebPartProperty where WebPartID = @WebPartID
		delete from tblSysMgrWebPart where WebPartID = @WebPartID;		
	end
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--部件位置定义列表
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrWebPartZoneListView')
begin
	print ' dorp procedure spSysMgrWebPartZoneListView';
	drop procedure spSysMgrWebPartZoneListView
end
	print 'create procedure spSysMgrWebPartZoneListView'
go
create procedure spSysMgrWebPartZoneListView
(
	@ZoneName nvarchar(50) --显示位置名称。
)
as
begin
	select a.ZoneID,a.ZoneName,b.SystemName,a.ZoneMode,a.ZoneLength,a.Description 
	from tblSysMgrWebPartZone a 
	left outer join tblSysMgrAppAuthorization b 
	on a.AppAuthID=b.AppAuthID
	where a.ZoneName like '%'+@ZoneName+'%'
end
go
---------------------------------------------------------------------------------------------------------------
--删除部件位置定义
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrDeleteWebPartZone')
begin
	print 'dorp procedure spSysMgrDeleteWebPartZone'
	drop procedure spSysMgrDeleteWebPartZone
end
	print 'create procedure spSysMgrDeleteWebPartZone'
go
create procedure spSysMgrDeleteWebPartZone
(
	@ZoneID		GUIDEx --显示位置ID。
)
as
begin
	declare @result nvarchar(256)
	declare @ZoneName nvarchar(256)
	declare @resultCode int
	set @resultCode = 0
	set @result = ''
-------------------------------------------------------------
    select top 1 @ZoneName=ZoneName 
    from tblSysMgrWebPartZone
    where ZoneID=@ZoneID
-------------------------------------------------------------
--实例化WebPart，控制页面显示和页面数据。
	if(@resultCode = 0 and exists(select 0 from tblSysMgrWebPartPersonal where ZoneID=@ZoneID))
	begin
		 set @resultCode = 1
		 set @result='['+@ZoneName+']在页面管理--首页设置中被使用，请先将其删除'
	end
-------------------------------------------------------------	
	if(@resultCode = 0)
	begin
		delete from tblSysMgrWebPartZone where ZoneID=@ZoneID
	end
	select cast(@resultCode as nvarchar(2)) + '|' + @result
end
go
---------------------------------------------------------------------------------------------------------------
--首页设置列表
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrWebPartPersonalListView')
begin
	print 'drop procedure spSysMgrWebPartPersonalListView'
	drop procedure spSysMgrWebPartPersonalListView
end
	print 'create procedure spSysMgrWebPartPersonalListView'
go
create procedure spSysMgrWebPartPersonalListView
(
	@WebPartName nvarchar(50)--部件名称。
)
as
begin
	select a.PersonalWebPartID,b.WebPartName,b.WebPartStatus,a.EmployeeName,c.ZoneName,a.OrderNo
	from tblSysMgrWebPartPersonal a
	inner join tblSysMgrWebPart b 
	on a.WebPartID=b.WebPartID
	inner join tblSysMgrWebPartZone c 
	on a.ZoneID=c.ZoneID
	where b.WebPartName like '%'+@WebPartName+'%'
	order by a.OrderNo
end
go
------------------------------------------------------------------------------------------------------------
--实例化参数设置列表
if exists(select 0 from sysobjects where xtype='p' and name='spSysMgrSettingPersonalListView')
begin
	print 'drop procedure spSysMgrSettingPersonalListView'
	drop procedure spSysMgrSettingPersonalListView
end
	print 'create procedure spSysMgrSettingPersonalListView'
go
create procedure spSysMgrSettingPersonalListView
(
	@EmployeeName nvarchar(50)--用户名称
)
as
begin
	select a.PersonalSettingID,b.SettingSign,a.EmployeeName,a.SettingValue
	from tblSysMgrSettingPersonal a
	inner join tblSysMgrSetting b 
	on a.SettingID=b.SettingID
	where a.EmployeeName like '%'+@EmployeeName+'%'
end
go
------------------------------------------------------------------------------------------------------------