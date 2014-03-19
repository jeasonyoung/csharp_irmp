<%--
//================================================================================
// FileName: frmOrgPostEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgPostEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgPostEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgPostEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
	     <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentID">所属部门：</JWC:LabelEx>
		    <JWC:DropDownListEx ID="ddlDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="268px" 
		         AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentID_OnSelectedIndexChanged"
		    IsRequired="true" ErrorMessage="所属部门不能为空！"/>
	    </div>
	
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbParentPostID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentPostID">上级岗位：</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlParentPostID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="268px"  />
		</div>
	
	 
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbPostName" runat="server" Style="float:left;" meta:resourcekey="Org_PostName">岗位名称：</JWC:LabelEx>
		    <JWC:TextBoxEx ID="txtPostName" runat="server" Width="268px" IsRequired="true" RequiredErrorMessage="岗位名称不能为空！" />
	    </div>
	
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbPostSign" runat="server" Style="float:left;" meta:resourcekey="Org_PostSign">岗位标识：</JWC:LabelEx>
		    <JWC:TextBoxEx ID="txtPostSign" runat="server" Width="268px" IsRequired="true" RequiredErrorMessage="岗位标识不能为空！"/>
	    </div>
		
	    <div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbRankID" runat="server" Style="float:left;" meta:resourcekey="Org_RankID">所属级别：</JWC:LabelEx>
		    <JWC:DropDownListEx ID="ddlRankID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="268px" IsRequired="true" ErrorMessage="所属级别不能为空！" />
	    </div>
 		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbPostDescription" runat="server" Style="float:left;" meta:resourcekey="Org_PostDescription">岗位描述：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtPostDescription" runat="server" TextMode="MultiLine" Rows="4" Width="268px"  />
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
