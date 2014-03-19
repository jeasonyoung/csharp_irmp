<%--
//================================================================================
// FileName: frmFlowTransitionEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowTransitionEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowTransitionEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowTransitionEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<fieldset class="TableSearch">
	    <legend>��Ǩ����</legend>
	    
	    <div style="float:left; width:50%;">
			<span style="float:left;">�������̣�</span>
			<JWC:DropDownListEx ID="ddlProcessID" runat="server" ShowUnDefine="true" Width="168px" IsRequired="true" ErrorMessage="�������̲���Ϊ�գ�"  
			 AutoPostBack="true" OnSelectedIndexChanged="ddlProcessID_OnSelectedIndexChanged"/>
		</div>
		
		<div style="float:left;width:50%;">
			<span style="float:left;">��Ǩ����</span>
			<JWC:RadioButtonListEx ID="rdTransitionRule" runat="server" Width="168px" IsRequired="true" ErrorMessage="��Ǩ������Ϊ�գ�" RepeatDirection="Horizontal" RepeatLayout="Table" />
		</div>
		
		<div style="float:left; width:50%; margin-bottom:5px;">
			<span style="float:left;">ǰ�����裺</span>
			<JWC:DropDownListEx ID="ddlFromStepID" runat="server" ShowUnDefine="true" Width="168px"  IsRequired="true" ErrorMessage="ǰ�����費��Ϊ�գ�"
			 AutoPostBack="true" OnSelectedIndexChanged="ddlFromStepID_OnSelectedIndexChanged"/>
		</div>
		
		<div style="float:left; width:50%;margin-bottom:5px;">
			<span style="float:left;">�������裺</span>
			<JWC:DropDownListEx ID="ddlToStepID" runat="server" ShowUnDefine="true" Width="168px" IsRequired="true" ErrorMessage="�������費��Ϊ�գ�" />
		</div>
	</fieldset>
	<!--��Ǩ����-->
	<fieldset class="TableSearch">
	    <legend>��Ǩ����</legend>
	    <div class="TableSearch" style="margin-bottom:10px;">
	        <div style="float:left; width:100%; margin-bottom:5px;">
	           <span style="float:left;">������</span>
	           <JWC:DropDownListEx ID="ddlTransitionParameter" runat="server" Width="148px" ShowUnDefine="true" />
	           <JWC:DropDownListEx ID="ddlCondition" runat="server" Width="100px" ToolTip="�ȽϽ��" ShowUnDefine="true" />
	           <span style="float:left;">�Ƚϵ�ֵ��</span>
	           <JWC:TextBoxEx ID="txtCompareValue" runat="server" Width="64px" />
	           <div style="float:left; margin-left:10px;">
	                <JWC:ButtonEx ID="btnSaveCondition" runat="server" ButtonType="Add" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="false" onclick="btnSaveCondition_Click"/>
	                <JWC:ButtonEx ID="btnDeleteCondition" runat="server" ButtonType="Del" ConfirmMsg="��ȷ��Ҫɾ����ǰ��ѡ���������" ShowConfirmMsg="true"   onclick="btnDeleteCondition_Click" />
	           </div>
	       </div
	    </div>
	    
	    <!--������ʾ����-->
        <div class="TableSearch" style=" height:200px; margin-bottom:10px; overflow:auto;">
            <JWC:DataGridView ID="dgTransitionCondition" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
	        AllowSorting="true" AllowPaging="false" MouseoverCssClass="DataGridHighLight"
	        PageSize="10" onbuilddatasource="dgTransitionCondition_BuildDataSource">
		        <PagerSettings Mode="NextPreviousFirstLast" />
		        <AlternatingRowStyle CssClass="DataGridAlter" />
		        <HeaderStyle CssClass="DataGridHeader" />
		        <FooterStyle CssClass="DataGridFooter" />
		        <RowStyle CssClass="DataGridItem" />
		        <Columns>
			        <JWC:CheckBoxFieldEx DataField="ConditionID">
				        <HeaderStyle Width="8px" />
			        </JWC:CheckBoxFieldEx>
        			
			        <JWC:BoundFieldEx DataField="ParameterName" HeaderText="��������" SortExpression="ParameterName">
				        <HeaderStyle Width="45%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
        		
			        <JWC:BoundFieldEx DataField="ConditionValueName" HeaderText="�ȽϽ��" SortExpression="ConditionValueName">
				        <HeaderStyle Width="15%" HorizontalAlign="Center" />
				        <ItemStyle HorizontalAlign="Center" />
			        </JWC:BoundFieldEx>
        			
			        <JWC:BoundFieldEx DataField="CompareValue" HeaderText="�Ƚϵ�ֵ" SortExpression="CompareValue">
				        <HeaderStyle Width="40%" />
				        <ItemStyle HorizontalAlign="Left" />
			        </JWC:BoundFieldEx>
		        </Columns>
	        </JWC:DataGridView>
	    </div>
	</fieldset>
	
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
		    <JWC:ServerAlert ID="errMessage" runat="server" />
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
