<%--
//================================================================================
// FileName: frmFlowProcessEdit.aspx
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
// Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================
//--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowProcessEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowProcessEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowProcessEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
	    <div style="float:left; width:100%;">
			<span style="float:left;">�������ƣ�</span>
			<JWC:TextBoxEx ID="txtProcessName" runat="server" Width="298px" IsRequired="true" RequiredErrorMessage="�������Ʋ���Ϊ�գ�" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">���̱�ʶ��</span>
			<JWC:TextBoxEx ID="txtProcessSign" runat="server" Width="298px" IsRequired="true" RequiredErrorMessage="���̱�ʶ����Ϊ�գ�"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">��ʼʱ�䣺</span>
			<JWC:TextBoxEx ID="txtBeginDate" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="��ʼʱ�䲻��Ϊ�գ�" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">����ʱ�䣺</span>
			<JWC:TextBoxEx ID="txtEndDate" runat="server" Width="168px"  IsRequired="true" RequiredErrorMessage="����ʱ�䲻��Ϊ�գ�"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">����״̬��</span>
			<JWC:DropDownListEx ID="ddlProcessStatus" runat="server" Width="80px" IsRequired="true" ErrorMessage="����״̬����Ϊ�գ�" Enabled="false" />
		</div>
		
		
		<div style="float:left;width:100%;">
			<span style="float:left;">����������</span>
			<JWC:TextBoxEx ID="txtProcessDescription" runat="server" Width="298px" TextMode="MultiLine" Rows="4"  />
		</div>
	</div>
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
		    <JWC:ServerAlert ID="errMessage" runat="server" />
		    <JWC:ButtonEx ID="btnProcessStatus" runat="server" ButtonType="Enable" OnClick="btnProcessStatus_OnClick" ConfirmMsg="��ȷ��Ҫ�ı�����״̬��" ShowConfirmMsg="true" Visible="false" />
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
