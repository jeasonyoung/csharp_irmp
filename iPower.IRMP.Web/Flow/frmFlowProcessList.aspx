<%--
//================================================================================
// FileName: frmFlowProcessList.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmFlowProcessList.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowProcessList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--����-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
		    <span style="float:left;">
				<JWC:ButtonEx ID="btnImport" runat="server" ButtonType="Import" PickerPage="frmFlowProcessImport.aspx" PickerType="Modal" PickerWidth="420px" PickerHeight="180px" onclick="btnImport_Click"/>
			</span>
		    <span style="float:left;">|</span>
		    <span style="float:left;">
				<JWC:ButtonEx ID="btnExport" runat="server" ButtonType="Export" ShowConfirmMsg="true" ConfirmMsg="��ȷ��������������\r\n(ÿ��ֻ�ܵ���һ������)��" OnClick="btnExport_Click"/>
			</span>	
		    <span style="float:left;">|</span>
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmFlowProcessEdit.aspx" PickerType="Modal" PickerWidth="420px" PickerHeight="310px" onclick="btnAdd_Click"/>
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
			<JWC:TextBoxEx ID="txtProcessName" runat="server" Width="128px" />
		</div>
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	<!--������ʾ����-->
	<JWC:DataGridView ID="dgfrmFlowProcessList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		AllowSorting="true" AllowPaging="true" MouseoverCssClass="DataGridHighLight" AllowExport="true"
		PageSize="15" onbuilddatasource="dgfrmFlowProcessList_BuildDataSource">
		<PagerSettings Mode="NextPreviousFirstLast" />
		<AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		<Columns>
			<JWC:CheckBoxFieldEx DataField="ProcessID">
				<HeaderStyle Width="8px" />
			</JWC:CheckBoxFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="310px"
				DataNavigateUrlFormatString="frmFlowProcessEdit.aspx?ProcessID={0}" DataNavigateUrlField="ProcessID"
				HeaderText="��������" DataField="ProcessName" SortExpression="ProcessName">
				<HeaderStyle Width="27%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:BoundFieldEx DataField="ProcessSign" HeaderText="���̱�ʶ" SortExpression="ProcessSign">
				<HeaderStyle Width="27%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="800px" WinHeight="600px"
				DataNavigateUrlFormatString="frmFlowChartDesign.aspx?ProcessID={0}" DataNavigateUrlField="ProcessID"
				HeaderText="����ͼ" DataField="FlowProcessChart" SortExpression="FlowProcessChart">
				<HeaderStyle Width="8%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:BoundFieldEx DataField="ProcessStatusName" HeaderText="����״̬" SortExpression="ProcessStatusName">
				<HeaderStyle Width="12%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="ProcessDescription" HeaderText="��������" SortExpression="ProcessDescription">
				<HeaderStyle Width="26%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
	<asp:Literal id="exportScript" runat="server" />
</asp:Content>
