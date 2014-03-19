<%--
//================================================================================
// FileName: frmOrgEmployeeEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgEmployeeEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgEmployeeEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgEmployeeEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<div class="TableSearch">
	     <div style="float:left; width:100%;">
    	        <div style="float:left;">
			        <JWC:LabelEx ID="lbEmployeeSign" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeSign">�û��˺ţ�</JWC:LabelEx>
			        <JWC:TextBoxEx ID="txtEmployeeSign" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="�û��˺Ų���Ϊ�գ�" />
		        </div>
		        
		        <div style="float:left;">
			        <JWC:LabelEx ID="lbEmployeeName" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeName">�û�������</JWC:LabelEx>
			        <JWC:TextBoxEx ID="txtEmployeeName" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="�û���������Ϊ�գ�" />
		        </div>
	     </div>
	     
	    <div style="float:left; width:100%;">
	            <div style="float:left;">
			        <JWC:LabelEx ID="lbNickName" runat="server" Style="float:left;" meta:resourcekey="Org_NickName">�û��ǳƣ�</JWC:LabelEx>
			        <JWC:TextBoxEx ID="txtNickName" runat="server" Width="168px"  />
			        <span>&nbsp;&nbsp;</span>
		        </div>
		        
		        <div style="float:left;">
			        <JWC:LabelEx ID="lbDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentID">�������ţ�</JWC:LabelEx>
			        <JWC:DropDownListEx ID="ddlDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="168px" 
			         AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentID_OnSelectedIndexChanged"
			        IsRequired="true" ErrorMessage="�������Ų���Ϊ�գ�" />
		        </div>
	    </div>
	    	
		<div style="float:left; width:100%;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeePassword" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeePassword">�û����룺</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeePassword" runat="server" TextMode="Password" Width="168px"  />
			    <span>&nbsp;&nbsp;</span>
		    </div>
		
		    <div style="float:left;">
		        <JWC:LabelEx ID="lbPostID" runat="server" Style="float:left;" meta:resourcekey="Org_PostID">������λ��</JWC:LabelEx>
		        <JWC:DropDownListEx ID="ddlPostID" runat="server" Width="168px" ShowUnDefine="true" ShowTreeView="true" IsRequired="true" ErrorMessage="������λ����Ϊ�գ�" />
		    </div>		
		</div>
		
       <%-- <div style="float:left; width:100%;">
            <span style="float:left;">��ʱ���룺</span>
            <JWC:TextBoxEx ID="txtEmployeePassword2" runat="server" TextMode="Password" Width="168px"  />
        </div>--%>
		
		<div style="float:left; width:100%; margin-top:10px;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbGender" runat="server" Style="float:left;" meta:resourcekey="Org_Gender">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��</JWC:LabelEx>
			    <JWC:DropDownListEx ID="ddlGender" runat="server" Width="168px" />
			    <span>&nbsp;&nbsp;&nbsp;</span>
		    </div>
		    
            <div style="float:left;">
                <JWC:LabelEx ID="lbBirthday" runat="server" Style="float:left;" meta:resourcekey="Org_Birthday">�������ڣ�</JWC:LabelEx>
                <JWC:TextBoxEx ID="txtBirthday" runat="server" Width="168px" ValidationExpression="\d{4}-\d{2}-\d{2}" RegularErrorMessage="�����������ݸ�ʽ����ȷ(Ӧ�磺2010-01-01)��" ToolTip="���ڸ�ʽӦ�磺2010-01-01" />
            </div>
		</div>
		
		<div style="float:left; width:100%;">
	    	<div style="float:left;">
	            <JWC:LabelEx ID="lbIdentityCard" runat="server" Style="float:left;" meta:resourcekey="Org_IdentityCard">���֤�ţ�</JWC:LabelEx>
	            <JWC:TextBoxEx ID="txtIdentityCard" runat="server" Width="168px"  />
	            <span>&nbsp;&nbsp;</span>
            </div>
            
            <div style="float:left;">
		        <JWC:LabelEx ID="lbCardID" runat="server" Style="float:left;" meta:resourcekey="Org_CardID">��&nbsp;&nbsp;��&nbsp;&nbsp;�ţ�</JWC:LabelEx>
		        <JWC:TextBoxEx ID="txtCardID" runat="server" Width="168px"  />
	        </div>
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbNation" runat="server" Style="float:left;" meta:resourcekey="Org_Nation">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�壺</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtNation" runat="server" Width="168px"  />
		</div>
		
		<div style="float:left; width:100%;margin-top:10px;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbWorkTelNo" runat="server" Style="float:left;" meta:resourcekey="Org_WorkTelNo">�����绰��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtWorkTelNo" runat="server" Width="168px"  />
			    <span>&nbsp;&nbsp;</span>
		    </div>
		    
		     <div style="float:left;">
			    <JWC:LabelEx ID="lbMobileNo" runat="server" Style="float:left;" meta:resourcekey="Org_MobileNo">�ƶ��绰��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtMobileNo" runat="server" Width="168px"  />
		    </div>
		</div>
		
		<div style="float:left; width:100%;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbMSNNO" runat="server" Style="float:left;" meta:resourcekey="Org_MSNNO">M&nbsp;&nbsp;&nbsp;S&nbsp;&nbsp;N&nbsp;&nbsp;��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtMSNNO" runat="server" Width="168px"  />
			    <span>&nbsp;&nbsp;</span>
		    </div>
		    
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbQQNO" runat="server" Style="float:left;" meta:resourcekey="Org_QQNO">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Q&nbsp;&nbsp;Q&nbsp;&nbsp;&nbsp;��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtQQNO" runat="server" Width="168px"  />
		    </div>
		</div>
		
		<div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbEmail" runat="server" Style="float:left;" meta:resourcekey="Org_Email">�����ʼ���</JWC:LabelEx>
		    <JWC:TextBoxEx ID="txtEmail" runat="server" Width="416px"  />
 		</div>
 		
 		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbAddress" runat="server" Style="float:left;" meta:resourcekey="Org_Address">��ϵ��ַ��</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtAddress" runat="server" Width="416px"  />
		</div>
	
	    <div style="float:left; width:100%;margin-top:10px;">
	        <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeStatus" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeStatus">״&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;̬��</JWC:LabelEx>
			    <JWC:DropDownListEx ID="ddlEmployeeStatus" runat="server" Width="168px" IsRequired="true" ErrorMessage="״̬����Ϊ�գ�" />
		    </div>
		    
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbOrderNo" runat="server" Style="float:left;" meta:resourcekey="Org_OrderNo">��&nbsp;&nbsp;��&nbsp;&nbsp;�ţ�</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtOrderNo" runat="server" Width="168px"  />
		    </div>
	    </div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbEmployeeDescription" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeDescription">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtEmployeeDescription" runat="server" TextMode="MultiLine" Rows="3" Width="416px"  />
		</div>
		
		<div style="float:left;width:100%;margin-top:5px;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx1" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx1">��չ�ֶ�1��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx1" runat="server" Width="168px"  />
		    </div>
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx2" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx2">��չ�ֶ�2��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx2" runat="server" Width="168px"  />
		    </div>
		</div>
		
		<div style="float:left;width:100%;">	
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx3" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx3">��չ�ֶ�3��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx3" runat="server" Width="168px"  />
		    </div>
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx4" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx4">��չ�ֶ�4��</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx4" runat="server" Width="168px"  />
		    </div>
		</div>
	</div>
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
