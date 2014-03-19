<%--
//================================================================================
// FileName: frmFlowStepAuthorizeList.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmFlowStepAuthorizeList.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepAuthorizeList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
    <JWC:ValidationSummaryEx id="vsfrmFlowStepAuthorizeList" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	<!--标题-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmFlowStepAuthorizeEdit.aspx" PickerType="Modal" PickerWidth="420px" PickerHeight="240px" onclick="btnAdd_Click"/>
			</span>
			<span style="float:left;">|</span>
			<span style="float:left;">
				<JWC:ButtonEx ID="btnDelete" runat="server" ButtonType="Del" ShowConfirmMsg="true" ConfirmMsg="您确定要删除当前所选择的数据吗？" onclick="btnDelete_Click" />
			</span>
		</div>
	</div>
	<!--查询区域-->
	<div class="TableSearch">
	    <div style="float:left; width:100%;">
	        <div style="float:left;">
			    <span style="float:left;">所属流程：</span>
			    <JWC:DropDownListEx ID="ddlProcessID" runat="server" Width="170px" ShowUnDefine="true" />
		    </div>
    	
		    <div style="float:left;">
			    <span style="float:left;">步骤名称：</span>
			    <JWC:TextBoxEx ID="txtStepName" runat="server" Width="168px" />
		    </div>
		</div>
		
		<div style="float:left; width:100%;">
		    <div style="float:left;">
			    <span style="float:left;">授权日期：</span>
			    <JWC:TextBoxEx ID="txtValidDate" runat="server" Width="168px" ValidationExpression="^[0-9]{4}-[0-9]{2}-[0-9]{2}$" RegularErrorMessage="授权日期格式不正确(yyyy-MM-dd)！" />
		    </div>
    		
		    <div style="float:left;">
			    <span style="float:left;">用户名称：</span>
			    <JWC:PickerBase ID="txtEmployeePickerBase" runat="server" Width="168px"  MultiSelect="false" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" />
		    </div>
    		
		    <div style="float:right;">
			    <JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			    <JWC:ServerAlert ID="errMessage" runat="server" />
		    </div>
		</div>
	</div>
	<!--数据显示区域-->
	<JWC:DataGridView ID="dgfrmFlowStepAuthorizeList" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		AllowSorting="true" AllowPaging="true" MouseoverCssClass="DataGridHighLight"
		PageSize="15" onbuilddatasource="dgfrmFlowStepAuthorizeList_BuildDataSource">
		<PagerSettings Mode="NextPreviousFirstLast" />
		<AlternatingRowStyle CssClass="DataGridAlter" />
		<HeaderStyle CssClass="DataGridHeader" />
		<FooterStyle CssClass="DataGridFooter" />
		<RowStyle CssClass="DataGridItem" />
		<Columns>
			<JWC:CheckBoxFieldEx DataField="AuthorizeID">
				<HeaderStyle Width="8px" />
			</JWC:CheckBoxFieldEx>
			
			<JWC:BoundFieldEx DataField="ProcessName" HeaderText="所属流程" SortExpression="ProcessName">
				<HeaderStyle Width="18%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="240px"
				DataNavigateUrlFormatString="frmFlowStepAuthorizeEdit.aspx?AuthorizeID={0}" DataNavigateUrlField="AuthorizeID"
				HeaderText="授权步骤" DataField="StepName" SortExpression="StepName">
				<HeaderStyle Width="18%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:BoundFieldEx DataField="EmployeeID" HeaderText="授权用户" SortExpression="EmployeeID">
				<HeaderStyle Width="12%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="TargetEmployeeID" HeaderText="被授权用户" SortExpression="TargetEmployeeID">
				<HeaderStyle Width="12%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="BeginDate" HeaderText="授权生效日期" SortExpression="BeginDate" DataFormatString="{0:yyyy-MM-dd}">
				<HeaderStyle Width="16%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="EndDate" HeaderText="授权失效日期" SortExpression="EndDate" DataFormatString="{0:yyyy-MM-dd}">
				<HeaderStyle Width="16%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="Valid" HeaderText="是否有效" SortExpression="Valid">
				<HeaderStyle Width="8%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
