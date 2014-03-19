<%--
//================================================================================
// FileName: frmFlowStepAuthorizeEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowStepAuthorizeEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepAuthorizeEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowStepAuthorizeEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
	    <div style="float:left;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;�̣�</span>
			<JWC:DropDownListEx ID="ddlProcessID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="�������̲���Ϊ�գ�"
			 AutoPostBack="true" OnSelectedIndexChanged="ddlProcessID_OnSelectedIndexChanged" />
		</div>
	
		<div style="float:left;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;Ȩ&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;�裺</span>
			<JWC:DropDownListEx ID="ddlStepID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="��Ȩ���費��Ϊ�գ�" />
		</div>
		
		<div style="float:left;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;Ȩ&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;����</span>
			<JWC:PickerBase ID="txtEmployeePickerBase" runat="server" Width="198px"  MultiSelect="false" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" 
			 IsRequired="true" ErrorMessage="��Ȩ�û�����Ϊ�գ�"/>
		</div>
		
		<div style="float:left;">
			<span style="float:left;">��&nbsp;��&nbsp;Ȩ&nbsp;��&nbsp;����</span>
			<JWC:PickerBase ID="txtTargetEmployeePickerBase" runat="server" Width="198px"  MultiSelect="false" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" 
			 IsRequired="true" ErrorMessage="����Ȩ�û�����Ϊ�գ�"/>
		</div>
		
		<div style="float:left;">
			<span style="float:left;">��Ȩ��ʼ���ڣ�</span>
			<JWC:TextBoxEx ID="txtBeginDate" runat="server" Width="168px" ValidationExpression="^[0-9]{4}-[0-9]{2}-[0-9]{2}$" RegularErrorMessage="��Ȩ���ڸ�ʽ����ȷ(yyyy-MM-dd)��"
			 IsRequired="true" RequiredErrorMessage="��Ȩ��ʼ���ڲ���Ϊ�գ�"/>
		</div>
		
		<div style="float:left;">
			<span style="float:left;">��Ȩ�������ڣ�</span>
			<JWC:TextBoxEx ID="txtEndDate" runat="server" Width="168px" ValidationExpression="^[0-9]{4}-[0-9]{2}-[0-9]{2}$" RegularErrorMessage="��Ȩ���ڸ�ʽ����ȷ(yyyy-MM-dd)��"
			 IsRequired="true" RequiredErrorMessage="��Ȩ�������ڲ���Ϊ�գ�"/>
		</div>
	</div>
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
		    <JWC:ServerAlert ID="errMessage" runat="server" />
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
