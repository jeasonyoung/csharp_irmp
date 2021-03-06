﻿<%--
//================================================================================
//  FileName: frmSecurityRegsiterImport.aspx
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/30
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
<%@ Page Title="" Language="C#" MasterPageFile="Share/ModuleNormalMasterPage.Master" AutoEventWireup="true" CodeBehind="frmSecurityRegsiterImport.aspx.cs" Inherits="iPower.IRMP.Security.Web.frmSecurityRegsiterImport" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>

<asp:Content ID="ContentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
<JWC:ValidationSummaryEx id="vsfrmSecurityRegsiterImport" runat="server"  ShowMessageBox="true" ShowSummary="false"/>

<!--查询区域-->
<div class="TableSearch">
    <div style="float:left;">
		<JWC:LabelEx ID="lbFileUpload" runat="server" Style="float:left;" meta:resourcekey="Sec_FileUpload">文件地址：</JWC:LabelEx>
		<asp:FileUpload ID="txtFileUpload" runat="server" Width="450px" />
    </div>

    <div style="float:right;">
	    <JWC:ButtonEx ID="btnUpload" runat="server" CausesValidation="false" onclick="btnUpload_Click" Text="上传"/>
    </div>
</div>

<!--数据显示区域-->
<div style="text-align:center; width:100%; height:350px; overflow:auto;">
<JWC:DataGridView ID="dgfrmSecurityRegsiterImport" runat="server" CssClass="DataGrid" Width="97%" ShowFooter="true"
	AllowSorting="true" AllowPaging="false" AllowExport="false" MouseoverCssClass="DataGridHighLight"
	PageSize="10" onbuilddatasource="dgfrmSecurityRegsiterImport_BuildDataSource">
	<PagerSettings Mode="NextPreviousFirstLast" />
	<AlternatingRowStyle CssClass="DataGridAlter" />
	<HeaderStyle CssClass="DataGridHeader" />
	<FooterStyle CssClass="DataGridFooter" />
	<RowStyle CssClass="DataGridItem" />
	<Columns>
		<JWC:CheckBoxFieldEx DataField="SystemID">
			<HeaderStyle Width="8px" />
		</JWC:CheckBoxFieldEx>
		 
		 <JWC:BoundFieldEx DataField="SystemID" HeaderText="系统编号" SortExpression="SystemID">
			<HeaderStyle Width="25%" />
			<ItemStyle HorizontalAlign="Center" />
		</JWC:BoundFieldEx>
		 
		 <JWC:BoundFieldEx DataField="SystemSign" HeaderText="系统标识" SortExpression="SystemSign">
			<HeaderStyle Width="20%" />
			<ItemStyle HorizontalAlign="Center" />
		</JWC:BoundFieldEx>
		 		　
		<JWC:BoundFieldEx DataField="SystemName" HeaderText="系统名称" SortExpression="SystemName">
			<HeaderStyle Width="20%" />
			<ItemStyle HorizontalAlign="Left" />
		</JWC:BoundFieldEx>
		
		<JWC:BoundFieldEx DataField="SystemDescription" HeaderText="系统描述" SortExpression="SystemDescription">
			<HeaderStyle Width="35%" />
			<ItemStyle HorizontalAlign="Center" />
		</JWC:BoundFieldEx>
		　
	</Columns>
</JWC:DataGridView>
</div>
<!--数据录入区域-->
<div class="TableSearch">
    <div style="float:left;">
        <JWC:LabelEx ID="lbSystemType" runat="server" Style="float:left;" meta:resourcekey="Sec_SystemType">类型：</JWC:LabelEx>
        <JWC:DropDownListEx ID="ddlSystemType" runat="server" Width="168px" ShowUnDefine="true" IsRequired="true" ErrorMessage="类型不能为空！" />
    </div>

    <div style="float:left;">
        <JWC:LabelEx ID="lbSystemStatus" runat="server" Style="float:left;" meta:resourcekey="Sec_SystemStatus">状态：</JWC:LabelEx>
        <JWC:DropDownListEx ID="ddlSystemStatus" runat="server" Width="168px" ShowUnDefine="true" IsRequired="true" ErrorMessage="状态不能为空！" />
    </div>
</div>
<!--数据控制区域-->
<div class="TableControl">
	<div style="margin:0 auto; text-align:center; width:100%;">
        <JWC:ServerAlert ID="errMessage" runat="server" />
		<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" Enabled="false" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
		<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
	</div>
</div>
</asp:Content>
