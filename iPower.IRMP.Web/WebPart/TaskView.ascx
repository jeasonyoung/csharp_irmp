<%--
//================================================================================
//  FileName: TaskView.ascx
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/1
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
//--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskView.ascx.cs" Inherits="iPower.IRMP.Web.WebPart.TaskView" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<div style="width:100%; height:200px">
    <div class="TitleBar" style="float:left; width:100%;text-align:left;"><%=this.WebPartData.DynamicTextData(this.CurrentUserID) %></div>
    <JWC:DataGridView ID="dgTaskViewList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true" 
        AllowSorting="false" AllowPaging="true" AllowExport="false"
        MouseoverCssClass="DataGridHighLight" PageSize="10" OnBuildDataSource="dgTaskViewList_OnBuildDataSource">
        <PagerSettings Mode="NextPreviousFirstLast" />
        <AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		
		<Columns>
		    <JWC:TemplateFieldEx>
		        <HeaderStyle Width="45%" />
				<ItemStyle HorizontalAlign="Left" />
				<HeaderTemplate>
				    <%=this.QueryPropertyValue("DisplayText")%>
				</HeaderTemplate>
				<ItemTemplate>
				    <%#Eval("Display") %>
				</ItemTemplate>
		    </JWC:TemplateFieldEx>
		    
		    <JWC:TemplateFieldEx>
		        <HeaderStyle Width="55%" />
				<ItemStyle HorizontalAlign="Left" />
				<HeaderTemplate>
				    <%=this.QueryPropertyValue("ValueText")%>
				</HeaderTemplate>
				<ItemTemplate>
				    <a href="<%#Eval("Url")%>" target="_blank"><%#Eval("Value") %></a>
				</ItemTemplate>
		    </JWC:TemplateFieldEx>
		</Columns>
       
    </JWC:DataGridView>
</div>