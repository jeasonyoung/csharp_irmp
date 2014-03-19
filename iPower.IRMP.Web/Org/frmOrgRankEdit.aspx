<%--
//================================================================================
// FileName: frmOrgRankEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgRankEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgRankEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgRankEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
		
		<div style="float:left; width:100%;">
			<JWC:LabelEx ID="lbParentRankID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentRankID">�ϼ���λ����</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlParentRankID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="198px"  />
		</div>
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbRankName" runat="server" Style="float:left;" meta:resourcekey="Org_RankName">��λ�������ƣ�</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtRankName" runat="server" Width="198px"  IsRequired="true" RequiredErrorMessage="��λ�������Ʋ���Ϊ�գ�" />
		</div>
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbRankDescription" runat="server" Style="float:left;" meta:resourcekey="Org_RankDescription">��λ����������</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtRankDescription" runat="server" TextMode="MultiLine" Rows="4" Width="198px"  />
		</div>
	</div>
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
