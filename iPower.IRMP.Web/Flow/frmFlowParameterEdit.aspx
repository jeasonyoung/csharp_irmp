<%--
//================================================================================
// FileName: frmFlowParameterEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowParameterEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowParameterEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>

<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowParameterEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
		<div style="float:left; width:100%;">
			<span style="float:left;">�������̣�</span>
			<JWC:DropDownListEx ID="ddlProcess" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="�������̲���Ϊ�գ�"
			  AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged"/>
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">�������裺</span>
			<JWC:DropDownListEx ID="ddlStep" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="�������費��Ϊ�գ�"  />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">�������ƣ�</span>
			<JWC:TextBoxEx ID="txtParameterName" runat="server" Width="268px" IsRequired="true" RequiredErrorMessage="�������Ʋ���Ϊ�գ�" />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">�������ͣ�</span>
			<JWC:RadioButtonListEx ID="rdParameterType" runat="server" Width="168px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="�������̲���Ϊ�գ�" />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">Ĭ&nbsp;&nbsp;��&nbsp;&nbsp;ֵ��</span>
			<JWC:TextBoxEx ID="txtDefaultValue" runat="server" Width="268px"  />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">����������</span>
			<JWC:TextBoxEx ID="txtParameterDescription" runat="server" Width="268px" TextMode="MultiLine" Rows="4" />
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
