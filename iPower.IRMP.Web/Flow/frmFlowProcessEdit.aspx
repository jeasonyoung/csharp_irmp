<%--
//================================================================================
// FileName: frmFlowProcessEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowProcessEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowProcessEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowProcessEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
	    <div style="float:left; width:100%;">
			<span style="float:left;">流程名称：</span>
			<JWC:TextBoxEx ID="txtProcessName" runat="server" Width="298px" IsRequired="true" RequiredErrorMessage="流程名称不能为空！" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">流程标识：</span>
			<JWC:TextBoxEx ID="txtProcessSign" runat="server" Width="298px" IsRequired="true" RequiredErrorMessage="流程标识不能为空！"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">开始时间：</span>
			<JWC:TextBoxEx ID="txtBeginDate" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="开始时间不能为空！" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">结束时间：</span>
			<JWC:TextBoxEx ID="txtEndDate" runat="server" Width="168px"  IsRequired="true" RequiredErrorMessage="结束时间不能为空！"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">流程状态：</span>
			<JWC:DropDownListEx ID="ddlProcessStatus" runat="server" Width="80px" IsRequired="true" ErrorMessage="流程状态不能为空！" Enabled="false" />
		</div>
		
		
		<div style="float:left;width:100%;">
			<span style="float:left;">流程描述：</span>
			<JWC:TextBoxEx ID="txtProcessDescription" runat="server" Width="298px" TextMode="MultiLine" Rows="4"  />
		</div>
	</div>
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
		    <JWC:ServerAlert ID="errMessage" runat="server" />
		    <JWC:ButtonEx ID="btnProcessStatus" runat="server" ButtonType="Enable" OnClick="btnProcessStatus_OnClick" ConfirmMsg="您确定要改变流程状态？" ShowConfirmMsg="true" Visible="false" />
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
