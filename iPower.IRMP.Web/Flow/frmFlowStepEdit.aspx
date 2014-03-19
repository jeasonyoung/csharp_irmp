<%--
//================================================================================
// FileName: frmFlowStepEdit.aspx
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
// Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================
//--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowStepEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowStepEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
	    <div style="float:left; width:100%">
		    <div style="float:left;">
			    <span style="float:left;">所属流程：</span>
			    <JWC:DropDownListEx ID="ddlProcessID" runat="server" ShowUnDefine="true" Width="168px" IsRequired="true" ErrorMessage="所属流程不能为空！"  />
		    </div>
    		
		    <div style="float:left;">
			    <span style="float:left;">运行周期：</span>
			    <JWC:TextBoxEx ID="txtStepDuration" runat="server" Width="168px"  Text="86400"  IsRequired="true" RequiredErrorMessage="运行周期" 
			    OnlyNumber="true" ValidationExpression="[0-9]+" RegularErrorMessage="只能输入数字！"/>
		    </div>
		</div>
		
		<div style="float:left; width:100%">
		    <div style="float:left;">
			    <span style="float:left;">步骤名称：</span>
			    <JWC:TextBoxEx ID="txtStepName" runat="server" Width="168px"  IsRequired="true" RequiredErrorMessage="步骤名称不能为空！" />
		    </div>
    		
		    <div style="float:left;">
			    <span style="float:left;">步骤标识：</span>
			    <JWC:TextBoxEx ID="txtStepSign" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="步骤标识不能为空！" />
		    </div>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">步骤类型：</span>
			<JWC:RadioButtonListEx ID="rdStepType" runat="server" Width="268px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="步骤类型不能为空！"  />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">步骤模式：</span>
			<JWC:RadioButtonListEx ID="rdStepMode" runat="server" Width="268px" RepeatDirection="Horizontal" RepeatLayout="Table" IsRequired="true" ErrorMessage="步骤模式不能为空！"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">操作入口：</span>
			<JWC:TextBoxEx ID="txtEntryAction" runat="server" Width="468px"  />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">查看入口：</span>
			<JWC:TextBoxEx ID="txtEntryQuery" runat="server" Width="468px"  />
		</div>
				
		<div style="float:left;width:100%;">
			<span style="float:left;">通知模式：</span>
		    <asp:CheckBoxList ID="chkStepWarning" runat="server" Width="168px" RepeatDirection="Horizontal" RepeatLayout="Table" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">人&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员：</span>
			<JWC:PickerBase ID="txtEmployeePickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Employee" PickerWidth="320px" PickerHeight="470px" />
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">角&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;色：</span>
			<JWC:PickerBase ID="txtRolePickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Role" PickerWidth="320px" PickerHeight="470px"/>
		</div>
				
		<div style="float:left;width:100%;">
			<span style="float:left;">岗位级别：</span>
			<JWC:PickerBase ID="txtRankPickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Rank" PickerWidth="320px" PickerHeight="470px"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">岗&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;位：</span>
			<JWC:PickerBase ID="txtPostPickerBase" runat="server" Width="468px"  MultiSelect="true" PickerPage="frmUserPicker.aspx?t=Post" PickerWidth="320px" PickerHeight="470px"/>
		</div>
		
		<div style="float:left;width:100%;">
			<span style="float:left;">步骤描述：</span>
			<JWC:TextBoxEx ID="txtStepDescription" runat="server" Width="468px" TextMode="MultiLine" Rows="3"  />
		</div>
		<div style="float:left;width:100%;">
			<span style="float:left;">步骤排序：</span>
			<JWC:TextBoxEx ID="txtStepOrderNo" runat="server" Width="80px" Text="1" OnlyNumber="true" IsRequired="true" RequiredErrorMessage="步骤排序不能为空！" />
	    </div>
	</div>
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
			<JWC:ServerAlert ID="errMessage" runat="server" />
		</div>
	</div>
</asp:Content>
