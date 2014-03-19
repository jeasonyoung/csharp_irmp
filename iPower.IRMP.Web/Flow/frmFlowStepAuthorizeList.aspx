<%--
//================================================================================
// FileName: frmFlowStepAuthorizeList.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmFlowStepAuthorizeList.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepAuthorizeList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
    <JWC:ValidationSummaryEx id="vsfrmFlowStepAuthorizeList" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	<!--����-->
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
				<JWC:ButtonEx ID="btnDelete" runat="server" ButtonType="Del" ShowConfirmMsg="true" ConfirmMsg="��ȷ��Ҫɾ����ǰ��ѡ���������" onclick="btnDelete_Click" />
			</span>
		</div>
	</div>
	<!--��ѯ����-->
	<div class="TableSearch">
	    <div style="float:left; width:100%;">
	        <div style="float:left;">
			    <span style="float:left;">�������̣�</span>
			    <JWC:DropDownListEx ID="ddlProcessID" runat="server" Width="170px" ShowUnDefine="true" />
		    </div>
    	
		    <div style="float:left;">
			    <span style="float:left;">�������ƣ�</span>
			    <JWC:TextBoxEx ID="txtStepName" runat="server" Width="168px" />
		    </div>
		</div>
		
		<div style="float:left; width:100%;">
		    <div style="float:left;">
			    <span style="float:left;">��Ȩ���ڣ�</span>
			    <JWC:TextBoxEx ID="txtValidDate" runat="server" Width="168px" ValidationExpression="^[0-9]{4}-[0-9]{2}-[0-9]{2}$" RegularErrorMessage="��Ȩ���ڸ�ʽ����ȷ(yyyy-MM-dd)��" />
		    </div>
    		
		    <div style="float:left;">
			    <span style="float:left;">�û����ƣ�</span>
			    <JWC:PickerBase ID="txtEmployeePickerBase" runat="server" Width="168px"  MultiSelect="false" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" />
		    </div>
    		
		    <div style="float:right;">
			    <JWC:ButtonEx ID="btnSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnSearch_Click"/>
			    <JWC:ServerAlert ID="errMessage" runat="server" />
		    </div>
		</div>
	</div>
	<!--������ʾ����-->
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
			
			<JWC:BoundFieldEx DataField="ProcessName" HeaderText="��������" SortExpression="ProcessName">
				<HeaderStyle Width="18%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="240px"
				DataNavigateUrlFormatString="frmFlowStepAuthorizeEdit.aspx?AuthorizeID={0}" DataNavigateUrlField="AuthorizeID"
				HeaderText="��Ȩ����" DataField="StepName" SortExpression="StepName">
				<HeaderStyle Width="18%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:MultiQueryStringFieldEx>
			
			<JWC:BoundFieldEx DataField="EmployeeID" HeaderText="��Ȩ�û�" SortExpression="EmployeeID">
				<HeaderStyle Width="12%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="TargetEmployeeID" HeaderText="����Ȩ�û�" SortExpression="TargetEmployeeID">
				<HeaderStyle Width="12%" />
				<ItemStyle HorizontalAlign="Left" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="BeginDate" HeaderText="��Ȩ��Ч����" SortExpression="BeginDate" DataFormatString="{0:yyyy-MM-dd}">
				<HeaderStyle Width="16%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="EndDate" HeaderText="��ȨʧЧ����" SortExpression="EndDate" DataFormatString="{0:yyyy-MM-dd}">
				<HeaderStyle Width="16%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
			
			<JWC:BoundFieldEx DataField="Valid" HeaderText="�Ƿ���Ч" SortExpression="Valid">
				<HeaderStyle Width="8%" />
				<ItemStyle HorizontalAlign="Center" />
			</JWC:BoundFieldEx>
		</Columns>
	</JWC:DataGridView>
</asp:Content>
