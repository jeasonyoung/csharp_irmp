<%--
//================================================================================
// FileName: frmOrgLeaderSubChargeEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgLeaderSubChargeEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgLeaderSubChargeEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.TreeView" tagprefix="JWC" %>

<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgLeaderSubChargeEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
		<div style="float:left; width:100%;">
			<JWC:LabelEx ID="lbEmployeeName" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeName">用户姓名：</JWC:LabelEx>
			<JWC:PickerBase ID="txtEmployeeName" runat="server" Width="268px" MultiSelect="false" IsRequired="true" ErrorMessage="用户姓名不能为空！" 
			PickerHeight="470px" PickerWidth="320px" PickerPage="frmOrgEmployeePicker.aspx" />
 		</div>
		<div style="float:left; width:100%;">
		    <fieldset style="height:350px; margin-top:5px;">
		        <legend>
		            <JWC:LabelEx ID="lbSubDepartment" runat="server" Style="float:left;" meta:resourcekey="Org_SubDepartment">分管部门</JWC:LabelEx>
		        </legend>
		        <div style="float:left;">
		        <JWC:TreeView ID="txtSubDepartment" runat="server" CheckType="CheckBox" ShowCheckBox="true" ExpandFirstLevel="true"/>
		        </div>
		    </fieldset>
		</div>
	</div>
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
