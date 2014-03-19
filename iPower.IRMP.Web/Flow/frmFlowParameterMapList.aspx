<%--
//================================================================================
// FileName: frmFlowParameterMapList.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmFlowParameterMapList.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowParameterMapList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--标题-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmFlowParameterMapEdit.aspx" PickerType="Modal" PickerWidth="420px" PickerHeight="280px" onclick="btnAdd_Click"/>
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
			<JWC:TextBoxEx ID="txtParameterName" runat="server"  Width="128px" />
	    </div>
	    
	    <div style="float:left;">
			<span style="float:left;">所属流程：</span>
			<JWC:DropDownListEx ID="ddlProcess" runat="server" ShowUnDefine="true" Width="168px" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged" />
		</div>
	    
		<div style="float:left;">
			<span style="float:left;">所属变迁规则：</span>
			<JWC:DropDownListEx ID="ddlTransitionID" runat="server" ShowUnDefine="true" Width="168px" />
		</div>
		
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	<!--数据显示区域-->
	<JWC:DataGridView ID="dgfrmFlowParameterMapList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		AllowSorting="true" AllowPaging="true" MouseoverCssClass="DataGridHighLight"
		PageSize="15" onbuilddatasource="dgfrmFlowParameterMapList_BuildDataSource">
		<PagerSettings Mode="NextPreviousFirstLast" />
		<AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		<Columns>
			<JWC:CheckBoxFieldEx DataField="ParameterMapID">
				<HeaderStyle Width="8px" />
			</JWC:CheckBoxFieldEx>
			
			<JWC:BoundFieldEx DataField="ProcessName" HeaderText="所属流程" SortExpression="ProcessName">
				<HeaderStyle Width="15%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="TransitionName" HeaderText="所属变迁规则" SortExpression="TransitionName">
				<HeaderStyle Width="25%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="280px"
				DataNavigateUrlFormatString="frmFlowParameterMapEdit.aspx?ParameterMapID={0}" DataNavigateUrlField="ParameterMapID"
				HeaderText="参数映射" DataField="MapParameterName" SortExpression="MapParameterName">
				<HeaderStyle Width="45%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
						
			<JWC:BoundFieldEx DataField="MapModeName" HeaderText="映射模式" SortExpression="MapModeName">
				<HeaderStyle Width="15%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
