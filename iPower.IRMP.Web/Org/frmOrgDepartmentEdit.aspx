<%--
//================================================================================
// FileName: frmOrgDepartmentEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgDepartmentEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgDepartmentEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgDepartmentEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
		
		<div style="float:left; width:100%;">
			<JWC:LabelEx ID="lbParentDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_ParentDepartment">�ϼ���֯��</JWC:LabelEx>
			<JWC:DropDownListEx ID="ddlParentDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="326px"  />
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbDepartmentName" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentName">�������ƣ�</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentName" runat="server" Width="326px"  IsRequired="true" RequiredErrorMessage="�������Ʋ���Ϊ�գ�" />
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbDepartmentSign" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentSign">���ű�ʶ��</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentSign" runat="server" Width="326px" IsRequired="true" RequiredErrorMessage="���ű�ʶ����Ϊ�գ�" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">������ţ�</span>
			<JWC:TextBoxEx ID="txtDepartmentOrder" runat="server" Width="326px" OnlyNumber="true" Text="0"  />
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbDepartmentDescription" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentDescription">����������</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentDescription" runat="server" TextMode="MultiLine" Rows="3" Width="326px"  />
		</div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentTel" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentTel">���ŵ绰��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentTel" runat="server" Width="128px"  />
		    </div>
    		
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentFax" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentFax">���Ŵ��棺</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentFax" runat="server" Width="128px"  />
		    </div>
	    </div>
		
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentAddress" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentAddress">���ŵ�ַ��</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentAddress" runat="server" Width="326px"  />
		</div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentLeader" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentLeader">��&nbsp;&nbsp;��&nbsp;&nbsp;�ˣ�</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentLeader" runat="server" Width="128px"  />
		    </div>
    		
		    <div style="float:left;" title="Ϊ0��ʾ������">
			    <JWC:LabelEx ID="lbDepartmentCapability" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentCapability">����������</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentCapability" runat="server" Width="128px" OnlyNumber="true" Text="0" ToolTip="Ϊ0��ʾ������" />
		    </div>
	    </div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentLevel" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentLevel">���ż���</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtDepartmentLevel" runat="server" Width="128px" OnlyNumber="true" Text="0" />
		    </div>
    		
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbDepartmentStatus" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentStatus">����״̬��</JWC:LabelEx>
			    <JWC:DropDownListEx ID="ddlDepartmentStatus" runat="server" Width="128px" IsRequired="true" ErrorMessage="����״̬����Ϊ�գ�" />
		    </div>
		</div>
		
	
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx1" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx1">��չ�ֶ�1��</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx1" runat="server" Width="326px"  />
		</div>
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx2" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx2">��չ�ֶ�2��</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx2" runat="server" Width="326px"  />
		</div>
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx3" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx3">��չ�ֶ�3��</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx3" runat="server" Width="326px"  />
		</div>
		<div style="float:left;width:100%">
			<JWC:LabelEx ID="lbDepartmentEx4" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentEx4">��չ�ֶ�4��</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtDepartmentEx4" runat="server" Width="326px"  />
		</div>
	</div>
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
			<JWC:ServerAlert ID="errMsg" runat="server" />
		</div>
	</div>
</asp:Content>
