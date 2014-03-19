<%--
//================================================================================
// FileName: frmOrgDepartmentList.aspx
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
//--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmOrgDepartmentList.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgDepartmentList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--����-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmOrgDepartmentEdit.aspx" PickerType="Modal" PickerWidth="460px" PickerHeight="440px" onclick="btnAdd_Click"/>
			</span>
			<span style="float:left;">|</span>
			<span style="float:left;">
				<JWC:ButtonEx ID="btnDelete" runat="server" ButtonType="Del" ShowConfirmMsg="true" ConfirmMsg="��ȷ��Ҫɾ����ǰ��ѡ���������" onclick="btnDelete_Click" />
			</span>
		</div>
	</div>
	<!--��ѯ����-->
	<div class="TableSearch">
	
	    <div style="float:left;">
	        <JWC:LabelEx ID="lbDepartmentName" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentName">�������ƣ�</JWC:LabelEx>
	        <JWC:TextBoxEx ID="txtDepartmentName" runat="server" Width="128px" />
	    </div>
	    
		<div style="float:left;">
		    <JWC:LabelEx ID="lbDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentDepartment">�ϼ����ţ�</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="128px" />
		</div>
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	<!--������ʾ����-->
	<JWC:DataGridView ID="dgfrmOrgDepartmentList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		AllowSorting="true" AllowPaging="true" AllowExport="false" MouseoverCssClass="DataGridHighLight"
		PageSize="15" onbuilddatasource="dgfrmOrgDepartmentList_BuildDataSource">
		<PagerSettings Mode="NextPreviousFirstLast" />
		<AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		<Columns>
			<JWC:CheckBoxFieldEx DataField="DepartmentID">
				<HeaderStyle Width="8px" />
			</JWC:CheckBoxFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="460px" WinHeight="440px"
				DataNavigateUrlFormatString="frmOrgDepartmentEdit.aspx?DepartmentID={0}" DataNavigateUrlField="DepartmentID"
				HeaderText="��������" DataField="DepartmentName" SortExpression="DepartmentName" meta:resourcekey="Org_DepartmentName">
				<HeaderStyle Width="25%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:BoundFieldEx DataField="FullDepartmentName" HeaderText="ȫ����" SortExpression="FullDepartmentName" meta:resourcekey="Org_FullDepartmentName">
				<HeaderStyle Width="45%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>		
			
			<JWC:BoundFieldEx DataField="DepartmentLeader" HeaderText="������" SortExpression="DepartmentLeader" meta:resourcekey="Org_DepartmentLeader">
				<HeaderStyle Width="10%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="DepartmentCapability" HeaderText="��������" SortExpression="DepartmentCapability" meta:resourcekey="Org_DepartmentCapability">
				<HeaderStyle Width="10%" />
				<ItemStyle HorizontalAlign="Right" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="DepartmentStatusName" HeaderText="״̬" SortExpression="DepartmentStatusName" meta:resourcekey="Org_DepartmentStatus">
				<HeaderStyle Width="10%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
