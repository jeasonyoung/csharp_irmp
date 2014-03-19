<%--
//================================================================================
// FileName: frmFlowStepEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowStepEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowStepEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
	    <div style="float:left; width:100%">
		    <div style="float:left;">
			    <span style="float:left;">�������̣�</span>
			    <JWC:DropDownListEx ID="ddlProcessID" runat="server" ShowUnDefine="true" Width="168px" IsRequired="true" ErrorMessage="�������̲���Ϊ�գ�"  />
		    </div>
    		
		    <div style="float:left;">
			    <span style="float:left;">�������ڣ�</span>
			    <JWC:TextBoxEx ID="txtStepDuration" runat="server" Width="168px"  Text="86400"  IsRequired="true" RequiredErrorMessage="��������" 
			    OnlyNumber="true" ValidationExpression="[0-9]+" RegularErrorMessage="ֻ���������֣�"/>
		    </div>
		</div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <span style="float:left;">�������ƣ�</span>
			    <JWC:TextBoxEx ID="txtStepName" runat="server" Width="168px"  IsRequired="true" RequiredErrorMessage="�������Ʋ���Ϊ�գ�" />
		    </div>
    		
		    <div style="float:left;">
			    <span style="float:left;">�����ʶ��</span>
			    <JWC:TextBoxEx ID="txtStepSign" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="�����ʶ����Ϊ�գ�" />
		    </div>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">�������ͣ�</span>
			<JWC:RadioButtonListEx ID="rdStepType" runat="server" Width="268px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="�������Ͳ���Ϊ�գ�"  />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">����ģʽ��</span>
			<JWC:RadioButtonListEx ID="rdStepMode" runat="server" Width="268px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="����ģʽ����Ϊ�գ�"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">������ڣ�</span>
			<JWC:TextBoxEx ID="txtEntryAction" runat="server" Width="468px"  />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">�鿴��ڣ�</span>
			<JWC:TextBoxEx ID="txtEntryQuery" runat="server" Width="468px"  />
		</div>
				
		<div style="float:left;width:100%;">
			<span style="float:left;">֪ͨģʽ��</span>
		    <asp:CheckBoxList ID="chkStepWarning" runat="server" Width="168px" RepeatDirection="Horizontal" RepeatLayout="Table" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ա��</span>
			<JWC:PickerBase ID="txtEmployeePickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ɫ��</span>
			<JWC:PickerBase ID="txtRolePickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Role" PickerWidth="320px" PickerHeight="470px"/>
		</div>
				
		<div style="float:left;width:100%;">
			<span style="float:left;">��λ����</span>
			<JWC:PickerBase ID="txtRankPickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Rank" PickerWidth="320px" PickerHeight="470px"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;λ��</span>
			<JWC:PickerBase ID="txtPostPickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Post" PickerWidth="320px" PickerHeight="470px"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">����������</span>
			<JWC:TextBoxEx ID="txtStepDescription" runat="server" Width="468px" TextMode="MultiLine" Rows="3"  />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">��������</span>
			<JWC:TextBoxEx ID="txtStepOrderNo" runat="server" Width="80px" Text="1" OnlyNumber="true" IsRequired="true" RequiredErrorMessage="����������Ϊ�գ�" />
	    </div>
	</div>
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
</asp:Content>
