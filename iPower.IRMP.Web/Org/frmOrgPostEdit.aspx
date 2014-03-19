<%--
//================================================================================
// FileName: frmOrgPostEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgPostEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgPostEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgPostEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
	     <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentID">�������ţ�</JWC:LabelEx>
		    <JWC:DropDownListEx ID="ddlDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="268px" 
		         AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentID_OnSelectedIndexChanged"
		    IsRequired="true" ErrorMessage="�������Ų���Ϊ�գ�"/>
	    </div>
	
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbParentPostID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentPostID">�ϼ���λ��</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlParentPostID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="268px"  />
		</div>
	
	 
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbPostName" runat="server" Style="float:left;" meta:resourcekey="Org_PostName">��λ���ƣ�</JWC:LabelEx>
		    <JWC:TextBoxEx ID="txtPostName" runat="server" Width="268px" IsRequired="true" RequiredErrorMessage="��λ���Ʋ���Ϊ�գ�" />
	    </div>
	
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbPostSign" runat="server" Style="float:left;" meta:resourcekey="Org_PostSign">��λ��ʶ��</JWC:LabelEx>
		    <JWC:TextBoxEx ID="txtPostSign" runat="server" Width="268px" IsRequired="true" RequiredErrorMessage="��λ��ʶ����Ϊ�գ�"/>
	    </div>
		
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbRankID" runat="server" Style="float:left;" meta:resourcekey="Org_RankID">��������</JWC:LabelEx>
		    <JWC:DropDownListEx ID="ddlRankID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="268px" IsRequired="true" ErrorMessage="����������Ϊ�գ�" />
	    </div>
 		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbPostDescription" runat="server" Style="float:left;" meta:resourcekey="Org_PostDescription">��λ������</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtPostDescription" runat="server" TextMode="MultiLine" Rows="4" Width="268px"  />
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
