<%--
//================================================================================
// FileName: frmFlowStepList.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmFlowStepList.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--标题-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmFlowStepEdit.aspx" PickerType="Modal" PickerWidth="620px" PickerHeight="430px" onclick="btnAdd_Click"/>
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
	        <span style="float:left;">步骤名称：</span>
	        <JWC:TextBoxEx ID="txtStepName" runat="server" Width="128px" />
	    </div>
		<div style="float:left;">
			<span style="float:left;">所属流程：</span>
			<JWC:DropDownListEx ID="ddlProcessID" runat="server" ShowUnDefine="true" Width="168px" />
		</div>
		<div style="float:right;">
			<JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
	<!--数据显示区域-->
	<JWC:DataGridView ID="dgfrmFlowStepList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		AllowSorting="true" AllowPaging="true" MouseoverCssClass="DataGridHighLight"
		PageSize="15" onbuilddatasource="dgfrmFlowStepList_BuildDataSource">
		<PagerSettings Mode="NextPreviousFirstLast" />
		<AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		<Columns>
			<JWC:CheckBoxFieldEx DataField="StepID">
				<HeaderStyle Width="8px" />
			</JWC:CheckBoxFieldEx>		
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="620px" WinHeight="430px"
				DataNavigateUrlFormatString="frmFlowStepEdit.aspx?StepID={0}" DataNavigateUrlField="StepID"
				HeaderText="步骤名称" DataField="StepName" SortExpression="StepName">
				<HeaderStyle Width="30%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:BoundFieldEx DataField="ProcessName" HeaderText="所属流程" SortExpression="ProcessName">
				<HeaderStyle Width="30%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
						
			<JWC:BoundFieldEx DataField="StepTypeName" HeaderText="步骤类型" SortExpression="StepTypeName">
				<HeaderStyle Width="10%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="StepDescription" HeaderText="步骤描述" SortExpression="StepDescription">
				<HeaderStyle Width="30%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
