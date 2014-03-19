<%--
//================================================================================
//  FileName: frmFlowStepInstanceEdit.aspx
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/17
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
//--%>
<%@ Page  Language="C#" MasterPageFile="Share/ModuleNormalMasterPage.Master" AutoEventWireup="true" CodeBehind="frmFlowStepInstanceEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowStepInstanceEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>

<asp:Content ID="ContentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowStepInstanceEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>

    <!--数据录入区域-->
	<div class="TableSearch">
	    <div style="float:left;width:100%;">
	        <span style="float:left;">步骤名称：</span>
	        <JWC:TextBoxEx ID="txtStepName" runat="server" Width="168px" ReadOnly="true" />
	    </div>
	    
	     <div style="float:left;width:100%;">
	        <span style="float:left;">推进用户：</span>
	        <JWC:PickerBase ID="pbFromEmployee" runat="server" Width="168px" Readonly="true" />
	    </div>
	    
	    <div style="float:left;width:100%;">
	        <span style="float:left;">启动时间：</span>
	        <JWC:TextBoxEx ID="txtCreateDate" runat="server" Width="168px" ReadOnly="true" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
	    </div>
	    
	    <div style="float:left;width:100%;">
	        <span style="float:left;">结束时间：</span>
	        <JWC:TextBoxEx ID="txtEndDate" runat="server" Width="168px" ReadOnly="true" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"/>
	    </div>
	    
	     <div style="float:left;width:100%;">
	        <span style="float:left;">步骤状态：</span>
	        <JWC:DropDownListEx ID="ddlInstanceStepStatus" runat="server" Width="80px" IsRequired="true" ErrorMessage="不走状态不能为空！" />
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
