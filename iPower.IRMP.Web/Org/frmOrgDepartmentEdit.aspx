<%--
//================================================================================
// FileName: frmOrgDepartmentEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgDepartmentEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgDepartmentEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgDepartmentEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
		
		<div style="float:left; width:100%;">
			<JWC:LabelEx ID="lbParentDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentDepartment">上级组织：</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlParentDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="326px"  />
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbDepartmentName" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentName">部门名称：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentName" runat="server" Width="326px"  IsRequired="true" RequiredErrorMessage="部门名称不能为空！" />
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbDepartmentSign" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentSign">部门标识：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentSign" runat="server" Width="326px" IsRequired="true" RequiredErrorMessage="部门标识不能为空！" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">部门序号：</span>
			<JWC:TextBoxEx ID="txtDepartmentOrder" runat="server" Width="326px" OnlyNumber="true" Text="0"  />
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbDepartmentDescription" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentDescription">部门描述：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentDescription" runat="server" TextMode="MultiLine" Rows="3" Width="326px"  />
		</div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentTel" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentTel">部门电话：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentTel" runat="server" Width="128px"  />
		    </div>
    		
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentFax" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentFax">部门传真：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentFax" runat="server" Width="128px"  />
		    </div>
	    </div>
		
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentAddress" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentAddress">部门地址：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentAddress" runat="server" Width="326px"  />
		</div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentLeader" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentLeader">负&nbsp;&nbsp;责&nbsp;&nbsp;人：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentLeader" runat="server" Width="128px"  />
		    </div>
    		
		    <div style="float:left;" title="为0表示不限制">
			    <JWC:LabelEx ID="lbDepartmentCapability" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentCapability">编制容量：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentCapability" runat="server" Width="128px" OnlyNumber="true" Text="0" ToolTip="为0表示不限制" />
		    </div>
	    </div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentLevel" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentLevel">部门级别：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentLevel" runat="server" Width="128px" OnlyNumber="true" Text="0" />
		    </div>
    		
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentStatus" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentStatus">部门状态：</JWC:LabelEx>
			    <JWC:DropDownListEx ID="ddlDepartmentStatus" runat="server" Width="128px" IsRequired="true" ErrorMessage="部门状态不能为空！" />
		    </div>
		</div>
		
	
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx1" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx1">扩展字段1：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx1" runat="server" Width="326px"  />
		</div>
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx2" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx2">扩展字段2：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx2" runat="server" Width="326px"  />
		</div>
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx3" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx3">扩展字段3：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx3" runat="server" Width="326px"  />
		</div>
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx4" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx4">扩展字段4：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx4" runat="server" Width="326px"  />
		</div>
	</div>
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
			<JWC:ServerAlert ID="errMsg" runat="server" />
		</div>
	</div>
</asp:Content>
