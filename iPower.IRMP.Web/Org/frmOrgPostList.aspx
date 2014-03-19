<%--
//================================================================================
// FileName: frmOrgPostList.aspx
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmOrgPostList.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgPostList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.TreeView" tagprefix="JWC" %>

<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--标题-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmOrgPostEdit.aspx" PickerType="Modal" PickerWidth="420px" PickerHeight="300px" onclick="btnAdd_Click"/>
			</span>
			<span style="float:left;">|</span>
			<span style="float:left;">
				<JWC:ButtonEx ID="btnDelete" runat="server" ButtonType="Del" ShowConfirmMsg="true" ConfirmMsg="您确定要删除当前所选择的数据吗？" onclick="btnDelete_Click" />
			</span>
		</div>
	</div>
	<!--查询区域-->
	<div class="TableSearch">
	    <div style="float:left;">
			<JWC:LabelEx ID="lbDepartmentName" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentName">部门名称：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentName" runat="server" Width="128px" />
		</div>
		<div style="float:left;">
			<JWC:LabelEx ID="lbRankID" runat="server" Style="float:left;" meta:resourcekey="Org_RankID">岗位级别：</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlRankID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="128px" />
		</div>
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	
	<div class="TableArea">
	    <div style="float:left; width:29%;" class="TableControl">
	        <div style="float:left;">
	        <JWC:TreeView ID="tvDepartment" runat="server" EnabledNodeClickEvent="true" ExpandFirstLevel="true" height="100%" width="200px" showscrollbar="true"
	         OnNodeClick="tvDepartment_OnNodeClick" />
	        </div>
	    </div>
	     <div style="float:right; width:70%;">
	        <!--数据显示区域-->
	        <JWC:DataGridView ID="dgfrmOrgPostList" runat="server" CssClass="DataGrid" Width="100%" ShowFooter="true"
		        AllowSorting="true" AllowPaging="true" AllowExport="false" MouseoverCssClass="DataGridHighLight"
		        PageSize="15" onbuilddatasource="dgfrmOrgPostList_BuildDataSource">
		        <PagerSettings Mode="NextPreviousFirstLast" />
		        <AlternatingRowStyle CssClass="DataGridAlter" />
		        <HeaderStyle CssClass="DataGridHeader" />
		        <FooterStyle CssClass="DataGridFooter" />
		        <RowStyle CssClass="DataGridItem" />
		        <Columns>
			        <JWC:CheckBoxFieldEx DataField="PostID">
				        <HeaderStyle Width="8px" />
			        </JWC:CheckBoxFieldEx>
			        
			        <JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="300px"
				        DataNavigateUrlFormatString="frmOrgPostEdit.aspx?PostID={0}" DataNavigateUrlField="PostID"
				        HeaderText="岗位名称" DataField="PostName" SortExpression="PostName" meta:resourcekey="Org_PostName">
				        <HeaderStyle Width="20%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:MultiQueryStringFieldEx>
			        
			        <JWC:BoundFieldEx DataField="FullPostName" HeaderText="岗位全称" SortExpression="FullPostName" meta:resourcekey="Org_FullPostName">
				        <HeaderStyle Width="40%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
			        
			        <JWC:BoundFieldEx DataField="DepartmentName" HeaderText="部门名称" SortExpression="DepartmentName" meta:resourcekey="Org_DepartmentName">
				        <HeaderStyle Width="20%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
			        
			        <JWC:BoundFieldEx DataField="RankName" HeaderText="岗位级别" SortExpression="RankName" meta:resourcekey="Org_RankName">
				        <HeaderStyle Width="20%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
			      
		        </Columns>
	        </JWC:DataGridView>
	    </div>
	</div>
</asp:Content>
