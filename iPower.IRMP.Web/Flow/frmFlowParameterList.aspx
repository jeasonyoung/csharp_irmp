<%--
//================================================================================
// FileName: frmFlowParameterList.aspx
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
// Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================
//--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmFlowParameterList.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowParameterList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--标题-->
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
				<JWC:ButtonEx ID="btnDelete" runat="server" ButtonType="Del" ShowConfirmMsg="true" ConfirmMsg="您确定要删除当前所选择的数据吗？" onclick="btnDelete_Click" />
			</span>
		</div>
	</div>
	<!--查询区域-->
	<div class="TableSearch">
	    <div style="float:left;">
			<span style="float:left;">参数名称：</span>
			<JWC:TextBoxEx ID="txtParameterName" runat="server" Width="128px" />
		</div>
		<div style="float:left;">
			<span style="float:left;">所属流程：</span>
			<JWC:DropDownListEx ID="ddlProcess" runat="server" ShowUnDefine="true" Width="128px" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged" />
		</div>
		<div style="float:left;">
			<span style="float:left;">所属步骤：</span>
			<JWC:DropDownListEx ID="ddlStep" runat="server" ShowUnDefine="true" Width="128px" />
		</div>
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	<!--数据显示区域-->
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
				HeaderText="所属流程" DataField="ProcessName" SortExpression="ProcessName">
				<HeaderStyle Width="25%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="620px" WinHeight="430px"
				DataNavigateUrlFormatString="frmFlowStepEdit.aspx?StepID={0}" DataNavigateUrlField="StepID"
				HeaderText="所属步骤" DataField="StepName" SortExpression="StepName">
				<HeaderStyle Width="25%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="310px"
				DataNavigateUrlFormatString="frmFlowParameterEdit.aspx?ParameterID={0}" DataNavigateUrlField="ParameterID"
				HeaderText="参数名称" DataField="ParameterName" SortExpression="ParameterName">
				<HeaderStyle Width="20%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
		
			<JWC:BoundFieldEx DataField="ParameterTypeName" HeaderText="参数类型" SortExpression="ParameterTypeName">
				<HeaderStyle Width="10%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
		
			<JWC:BoundFieldEx DataField="DefaultValue" HeaderText="默认值" SortExpression="DefaultValue">
				<HeaderStyle Width="20%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
