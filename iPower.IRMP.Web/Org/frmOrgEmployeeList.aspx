<%--
//================================================================================
// FileName: frmOrgEmployeeList.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="frmOrgEmployeeList.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgEmployeeList" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.TreeView" tagprefix="JWC" %>

<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<!--����-->
	<div class="TitleBar">
		<span class="LabelTitle" style="float:left;">
			<asp:Label id="lbTitle" runat="server"/>
		</span>
		<div style="float:right;">
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmOrgEmployeeEdit.aspx" PickerType="Modal" PickerWidth="550px" PickerHeight="540px" onclick="btnAdd_Click"/>
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
			<JWC:LabelEx ID="lbEmployeeName" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeName">�û����ƣ�</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtEmployeeName" runat="server" Width="128px" />
		</div>
		
		<div style="float:left;">
		    <JWC:LabelEx ID="lbDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentID">�������ţ�</JWC:LabelEx>
		    <JWC:DropDownListEx ID="ddlDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="168px" />
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
	        <!--������ʾ����-->
	        <JWC:DataGridView ID="dgfrmOrgEmployeeList" runat="server" CssClass="DataGrid" Width="100%" ShowFooter="true"
		        AllowSorting="true" AllowPaging="true" AllowExport="false" MouseoverCssClass="DataGridHighLight"
		        PageSize="15" onbuilddatasource="dgfrmOrgEmployeeList_BuildDataSource">
		        <PagerSettings Mode="NextPreviousFirstLast" />
		        <AlternatingRowStyle CssClass="DataGridAlter" />
		        <HeaderStyle CssClass="DataGridHeader" />
		        <FooterStyle CssClass="DataGridFooter" />
		        <RowStyle CssClass="DataGridItem" />
		        <Columns>
			        <JWC:CheckBoxFieldEx DataField="EmployeeID">
				        <HeaderStyle Width="8px" />
			        </JWC:CheckBoxFieldEx>
        			
			        <JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="550px" WinHeight="540px"
				        DataNavigateUrlFormatString="frmOrgEmployeeEdit.aspx?EmployeeID={0}" DataNavigateUrlField="EmployeeID"
				        HeaderText="�û��˺�" DataField="EmployeeSign" SortExpression="EmployeeSign" meta:resourcekey="Org_EmployeeSign">
				        <HeaderStyle Width="15%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:MultiQueryStringFieldEx>
        						
			        <JWC:BoundFieldEx DataField="EmployeeName" HeaderText="�û�����" SortExpression="EmployeeName" meta:resourcekey="Org_EmployeeName">
				        <HeaderStyle Width="20%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
        						
			        <JWC:BoundFieldEx DataField="GenderName" HeaderText="�Ա�" SortExpression="GenderName" meta:resourcekey="Org_GenderName">
				        <HeaderStyle Width="10%" />
				        <ItemStyle HorizontalAlign="Center" />
			        </JWC:BoundFieldEx>
        					
			        <JWC:BoundFieldEx DataField="DepartmentName" HeaderText="��������" SortExpression="DepartmentName" meta:resourcekey="Org_DepartmentName">
				        <HeaderStyle Width="20%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
        			
			        <JWC:BoundFieldEx DataField="PostName" HeaderText="������λ" SortExpression="PostName" meta:resourcekey="Org_PostName">
				        <HeaderStyle Width="25%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
        			
			        <JWC:BoundFieldEx DataField="EmployeeStatusName" HeaderText="״̬" SortExpression="EmployeeStatusName" meta:resourcekey="Org_EmployeeStatus">
				        <HeaderStyle Width="10%" />
				        <ItemStyle HorizontalAlign="Center" />
			        </JWC:BoundFieldEx>
        			
		        </Columns>
	        </JWC:DataGridView>
	    </div>
	</div>
</asp:Content>
