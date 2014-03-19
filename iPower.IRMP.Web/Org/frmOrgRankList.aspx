<%--
//================================================================================
// FileName: frmOrgRankList.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmOrgRankList.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgRankList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--����-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmOrgRankEdit.aspx" PickerType="Modal" PickerWidth="340px" PickerHeight="260px" onclick="btnAdd_Click"/>
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
			<JWC:LabelEx ID="lbRankName" runat="server" Style="float:left;" meta:resourcekey="Org_RankName">��λ���ƣ�</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtRankName" runat="server" Width="128px" />
		</div>
		<div style="float:left;">
			<JWC:LabelEx ID="lbParentRankID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentRankID">�ϼ���λ��</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlParentRankID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="128px" />
		</div>
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	<!--������ʾ����-->
	<JWC:DataGridView ID="dgfrmOrgRankList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		AllowSorting="true" AllowPaging="true" AllowExport="false" MouseoverCssClass="DataGridHighLight"
		PageSize="15" onbuilddatasource="dgfrmOrgRankList_BuildDataSource">
		<PagerSettings Mode="NextPreviousFirstLast" />
		<AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		<Columns>
			<JWC:CheckBoxFieldEx DataField="RankID">
				<HeaderStyle Width="8px" />
			</JWC:CheckBoxFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="340px" WinHeight="260px"
				DataNavigateUrlFormatString="frmOrgRankEdit.aspx?RankID={0}" DataNavigateUrlField="RankID"
				HeaderText="��λ����" DataField="RankName" SortExpression="RankName" meta:resourcekey="Org_RankName">
				<HeaderStyle Width="20%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:BoundFieldEx DataField="FullRankName" HeaderText="��λȫ��" SortExpression="FullRankName" meta:resourcekey="Org_FullRankName">
				<HeaderStyle Width="55%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="RankDescription" HeaderText="��λ����" SortExpression="RankDescription" meta:resourcekey="Org_RankDescription">
				<HeaderStyle Width="25%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
