<%--
//================================================================================
// FileName: frmFlowStepAuthorizeEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowStepAuthorizeEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepAuthorizeEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowStepAuthorizeEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
	    <div style="float:left;">
			<span style="float:left;">所&nbsp;&nbsp;&nbsp;属&nbsp;&nbsp;&nbsp;流&nbsp;&nbsp;程：</span>
			<JWC:DropDownListEx ID="ddlProcessID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="所属流程不能为空！"
			 AutoPostBack="true" OnSelectedIndexChanged="ddlProcessID_OnSelectedIndexChanged" />
		</div>
	
		<div style="float:left;">
			<span style="float:left;">授&nbsp;&nbsp;&nbsp;权&nbsp;&nbsp;&nbsp;步&nbsp;&nbsp;骤：</span>
			<JWC:DropDownListEx ID="ddlStepID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="授权步骤不能为空！" />
		</div>
		
		<div style="float:left;">
			<span style="float:left;">授&nbsp;&nbsp;&nbsp;权&nbsp;&nbsp;&nbsp;用&nbsp;&nbsp;户：</span>
			<JWC:PickerBase ID="txtEmployeePickerBase" runat="server" Width="198px"  MultiSelect="false" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" 
			 IsRequired="true" ErrorMessage="授权用户不能为空！"/>
		</div>
		
		<div style="float:left;">
			<span style="float:left;">被&nbsp;授&nbsp;权&nbsp;用&nbsp;户：</span>
			<JWC:PickerBase ID="txtTargetEmployeePickerBase" runat="server" Width="198px"  MultiSelect="false" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" 
			 IsRequired="true" ErrorMessage="被授权用户不能为空！"/>
		</div>
		
		<div style="float:left;">
			<span style="float:left;">授权开始日期：</span>
			<JWC:TextBoxEx ID="txtBeginDate" runat="server" Width="168px" ValidationExpression="^[0-9]{4}-[0-9]{2}-[0-9]{2}$" RegularErrorMessage="授权日期格式不正确(yyyy-MM-dd)！"
			 IsRequired="true" RequiredErrorMessage="授权开始日期不能为空！"/>
		</div>
		
		<div style="float:left;">
			<span style="float:left;">授权结束日期：</span>
			<JWC:TextBoxEx ID="txtEndDate" runat="server" Width="168px" ValidationExpression="^[0-9]{4}-[0-9]{2}-[0-9]{2}$" RegularErrorMessage="授权日期格式不正确(yyyy-MM-dd)！"
			 IsRequired="true" RequiredErrorMessage="授权结束日期不能为空！"/>
		</div>
	</div>
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
		    <JWC:ServerAlert ID="errMessage" runat="server" />
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
