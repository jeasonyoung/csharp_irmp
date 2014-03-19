<%--
//================================================================================
// FileName: frmFlowParameterMapEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowParameterMapEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowParameterMapEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowParameterMapEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
	    <div style="float:left;">
			<span style="float:left;">所&nbsp;&nbsp;&nbsp;属&nbsp;&nbsp;&nbsp;流&nbsp;&nbsp;程：</span>
			<JWC:DropDownListEx ID="ddlProcess" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="所属流程不能为空！"
			 AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged" />
		</div>
		<div style="float:left;">
			<span style="float:left;">所属变迁规则：</span>
			<JWC:DropDownListEx ID="ddlTransitionID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="所属变迁规则不能为空！"
			 AutoPostBack="true" OnSelectedIndexChanged="ddlTransitionID_OnSelectedIndexChanged" />
		</div>
		<div style="float:left;">
			<span style="float:left;">参&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数：</span>
			<JWC:DropDownListEx ID="ddlParameterID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="参数不能为空！" />
		</div>
		<div style="float:left;">
			<span style="float:left;">映&nbsp;&nbsp;&nbsp;射&nbsp;&nbsp;&nbsp;参&nbsp;&nbsp;数：</span>
			<JWC:DropDownListEx ID="ddlMapParameterID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="映射参数不能为空！" />
		</div>
		<div style="float:left;">
			<span style="float:left;">映&nbsp;&nbsp;&nbsp;射&nbsp;&nbsp;&nbsp;模&nbsp;&nbsp;式：</span>
			<JWC:RadioButtonListEx ID="rdMapMode" runat="server" Width="198px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="映射模式不能为空！" />
		</div>
		<div style="float:left;">
			<span style="float:left;">程&nbsp;序&nbsp;集&nbsp;名&nbsp;称：</span>
			<JWC:TextBoxEx ID="txtAssemblyName" runat="server" Width="168px"  /><span style="color:Red;">(映射模式为函数时使用)</span>
		</div>
		<div style="float:left;">
			<span style="float:left;">类&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;称：</span>
			<JWC:TextBoxEx ID="txtClassName" runat="server" Width="168px"  /><span style="color:Red;">(映射模式为函数时使用)</span>
		</div>
		<div style="float:left;">
			<span style="float:left;">入&nbsp;&nbsp;&nbsp;口&nbsp;&nbsp;&nbsp;函&nbsp;&nbsp;数：</span>
			<JWC:TextBoxEx ID="txtEntryName" runat="server" Width="168px"  /><span style="color:Red;">(映射模式为函数时使用)</span>
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
