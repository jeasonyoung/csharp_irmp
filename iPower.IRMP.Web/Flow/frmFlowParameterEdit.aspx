<%--
//================================================================================
// FileName: frmFlowParameterEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowParameterEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowParameterEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>

<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowParameterEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
		<div style="float:left; width:100%;">
			<span style="float:left;">所属流程：</span>
			<JWC:DropDownListEx ID="ddlProcess" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="所属流程不能为空！"
			  AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged"/>
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">所属步骤：</span>
			<JWC:DropDownListEx ID="ddlStep" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="所属步骤不能为空！"  />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">参数名称：</span>
			<JWC:TextBoxEx ID="txtParameterName" runat="server" Width="268px" IsRequired="true" RequiredErrorMessage="参数名称不能为空！" />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">参数类型：</span>
			<JWC:RadioButtonListEx ID="rdParameterType" runat="server" Width="168px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="所属流程不能为空！" />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">默&nbsp;&nbsp;认&nbsp;&nbsp;值：</span>
			<JWC:TextBoxEx ID="txtDefaultValue" runat="server" Width="268px"  />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">参数描述：</span>
			<JWC:TextBoxEx ID="txtParameterDescription" runat="server" Width="268px" TextMode="MultiLine" Rows="4" />
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
