<%--
//================================================================================
// FileName: frmOrgRankEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgRankEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgRankEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgRankEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
		
		<div style="float:left; width:100%;">
			<JWC:LabelEx ID="lbParentRankID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentRankID">上级岗位级别：</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlParentRankID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="198px"  />
		</div>
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbRankName" runat="server" Style="float:left;" meta:resourcekey="Org_RankName">岗位级别名称：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtRankName" runat="server" Width="198px"  IsRequired="true" RequiredErrorMessage="岗位级别名称不能为空！" />
		</div>
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbRankDescription" runat="server" Style="float:left;" meta:resourcekey="Org_RankDescription">岗位级别描述：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtRankDescription" runat="server" TextMode="MultiLine" Rows="4" Width="198px"  />
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
