<%--
//================================================================================
// FileName: frmFlowParameterList.aspx
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
// Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================
//--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmFlowParameterList.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowParameterList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--����-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmFlowParameterEdit.aspx" PickerType="Modal" PickerWidth="420px" PickerHeight="310px" onclick="btnAdd_Click"/>
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
			<span style="float:left;">�������ƣ�</span>
			<JWC:TextBoxEx ID="txtParameterName" runat="server" Width="128px" />
		</div>
		<div style="float:left;">
			<span style="float:left;">�������̣�</span>
			<JWC:DropDownListEx ID="ddlProcess" runat="server" ShowUnDefine="true" Width="128px" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged" />
		</div>
		<div style="float:left;">
			<span style="float:left;">�������裺</span>
			<JWC:DropDownListEx ID="ddlStep" runat="server" ShowUnDefine="true" Width="128px" />
		</div>
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	<!--������ʾ����-->
	<JWC:DataGridView ID="dgfrmFlowParameterList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		AllowSorting="true" AllowPaging="true" MouseoverCssClass="DataGridHighLight"
		PageSize="15" onbuilddatasource="dgfrmFlowParameterList_BuildDataSource">
		<PagerSettings Mode="NextPreviousFirstLast" />
		<AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		<Columns>
			<JWC:CheckBoxFieldEx DataField="ParameterID">
				<HeaderStyle Width="8px" />
			</JWC:CheckBoxFieldEx>
					
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="310px"
				DataNavigateUrlFormatString="frmFlowProcessEdit.aspx?ProcessID={0}" DataNavigateUrlField="ProcessID"
				HeaderText="��������" DataField="ProcessName" SortExpression="ProcessName">
				<HeaderStyle Width="25%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="620px" WinHeight="430px"
				DataNavigateUrlFormatString="frmFlowStepEdit.aspx?StepID={0}" DataNavigateUrlField="StepID"
				HeaderText="��������" DataField="StepName" SortExpression="StepName">
				<HeaderStyle Width="25%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="310px"
				DataNavigateUrlFormatString="frmFlowParameterEdit.aspx?ParameterID={0}" DataNavigateUrlField="ParameterID"
				HeaderText="��������" DataField="ParameterName" SortExpression="ParameterName">
				<HeaderStyle Width="20%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
		
			<JWC:BoundFieldEx DataField="ParameterTypeName" HeaderText="��������" SortExpression="ParameterTypeName">
				<HeaderStyle Width="10%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
		
			<JWC:BoundFieldEx DataField="DefaultValue" HeaderText="Ĭ��ֵ" SortExpression="DefaultValue">
				<HeaderStyle Width="20%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
