<%@ Page Title="" Language="C#" MasterPageFile="Share/ModuleNormalMasterPage.Master" AutoEventWireup="true" CodeBehind="frmFlowChartDesign.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowChartDesign" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>

<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowChartDesign" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
    <!--标题-->
	<div class="TitleBar">
		<div style="float:right;">
		    <span style="float:left;">
				<JWC:ButtonEx ID="btnExport" runat="server" ButtonType="Export" PickerPage="frmFlowProcessExport.aspx" PickerType="Normal2" PickerHeight="0" PickerWidth="0"  ShowConfirmMsg="true" ConfirmMsg="您确定导出流程数据！" OnClick="btnExport_Click"/>
			</span>	
		    <span style="float:left;">|</span>
			<span style="float:left;">
				<JWC:ButtonEx ID="btnAdd" runat="server" ButtonType="Add" PickerPage="frmFlowStepEdit.aspx" PickerType="Modal" PickerWidth="620px" PickerHeight="430px" onclick="btnAdd_Click"/>
			</span>
			<span style="float:left;">|</span>
			<span style="float:left;">
			    <JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			</span>
			<span style="float:left;">|</span>
			<span style="float:left;">
			    <JWC:ServerAlert ID="errMessage" runat="server" />
			    <JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
			</span>
		</div>
	</div>
	<!--流程图-->
	<div class="TableControl" style="height:550px; overflow:auto;">
	    <img alt="流程图" style="width:100%; height:100%;" src="FlowChartHandler.ashx?ProcessID=<%=this.ProcessID %>"/>
	</div>
</asp:Content>
