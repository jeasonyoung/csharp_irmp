<%--
//================================================================================
// FileName: frmFlowParameterMapEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowParameterMapEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowParameterMapEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowParameterMapEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
	    <div style="float:left;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;�̣�</span>
			<JWC:DropDownListEx ID="ddlProcess" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="�������̲���Ϊ�գ�"
			 AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged" />
		</div>
		<div style="float:left;">
			<span style="float:left;">������Ǩ����</span>
			<JWC:DropDownListEx ID="ddlTransitionID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="������Ǩ������Ϊ�գ�"
			 AutoPostBack="true" OnSelectedIndexChanged="ddlTransitionID_OnSelectedIndexChanged" />
		</div>
		<div style="float:left;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����</span>
			<JWC:DropDownListEx ID="ddlParameterID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="��������Ϊ�գ�" />
		</div>
		<div style="float:left;">
			<span style="float:left;">ӳ&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;����</span>
			<JWC:DropDownListEx ID="ddlMapParameterID" runat="server" ShowUnDefine="true" Width="268px" IsRequired="true" ErrorMessage="ӳ���������Ϊ�գ�" />
		</div>
		<div style="float:left;">
			<span style="float:left;">ӳ&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;ģ&nbsp;&nbsp;ʽ��</span>
			<JWC:RadioButtonListEx ID="rdMapMode" runat="server" Width="198px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="ӳ��ģʽ����Ϊ�գ�" />
		</div>
		<div style="float:left;">
			<span style="float:left;">��&nbsp;��&nbsp;��&nbsp;��&nbsp;�ƣ�</span>
			<JWC:TextBoxEx ID="txtAssemblyName" runat="server" Width="168px"  /><span style="color:Red;">(ӳ��ģʽΪ����ʱʹ��)</span>
		</div>
		<div style="float:left;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ƣ�</span>
			<JWC:TextBoxEx ID="txtClassName" runat="server" Width="168px"  /><span style="color:Red;">(ӳ��ģʽΪ����ʱʹ��)</span>
		</div>
		<div style="float:left;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;����</span>
			<JWC:TextBoxEx ID="txtEntryName" runat="server" Width="168px"  /><span style="color:Red;">(ӳ��ģʽΪ����ʱʹ��)</span>
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
