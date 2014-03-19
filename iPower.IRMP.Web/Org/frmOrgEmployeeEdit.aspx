<%--
//================================================================================
// FileName: frmOrgEmployeeEdit.aspx
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
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
//--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmOrgEmployeeEdit.aspx.cs" Inherits="iPower.IRMP.Org.Web.frmOrgEmployeeEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmOrgEmployeeEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<div class="TableSearch">
	     <div style="float:left; width:100%;">
    	        <div style="float:left;">
			        <JWC:LabelEx ID="lbEmployeeSign" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeSign">用户账号：</JWC:LabelEx>
			        <JWC:TextBoxEx ID="txtEmployeeSign" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="用户账号不能为空！" />
		        </div>
		        
		        <div style="float:left;">
			        <JWC:LabelEx ID="lbEmployeeName" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeName">用户姓名：</JWC:LabelEx>
			        <JWC:TextBoxEx ID="txtEmployeeName" runat="server" Width="168px" IsRequired="true" RequiredErrorMessage="用户姓名不能为空！" />
		        </div>
	     </div>
	     
	    <div style="float:left; width:100%;">
	            <div style="float:left;">
			        <JWC:LabelEx ID="lbNickName" runat="server" Style="float:left;" meta:resourcekey="Org_NickName">用户昵称：</JWC:LabelEx>
			        <JWC:TextBoxEx ID="txtNickName" runat="server" Width="168px"  />
			        <span>&nbsp;&nbsp;</span>
		        </div>
		        
		        <div style="float:left;">
			        <JWC:LabelEx ID="lbDepartmentID" runat="server" Style="float:left;" meta:resourcekey="Org_DepartmentID">所属部门：</JWC:LabelEx>
			        <JWC:DropDownListEx ID="ddlDepartmentID" runat="server" ShowUnDefine="true" ShowTreeView="true" Width="168px" 
			         AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentID_OnSelectedIndexChanged"
			        IsRequired="true" ErrorMessage="所属部门不能为空！" />
		        </div>
	    </div>
	    	
		<div style="float:left; width:100%;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeePassword" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeePassword">用户密码：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeePassword" runat="server" TextMode="Password" Width="168px"  />
			    <span>&nbsp;&nbsp;</span>
		    </div>
		
		    <div style="float:left;">
		        <JWC:LabelEx ID="lbPostID" runat="server" Style="float:left;" meta:resourcekey="Org_PostID">所属岗位：</JWC:LabelEx>
		        <JWC:DropDownListEx ID="ddlPostID" runat="server" Width="168px" ShowUnDefine="true" ShowTreeView="true" IsRequired="true" ErrorMessage="所属岗位不能为空！" />
		    </div>		
		</div>
		
       <%-- <div style="float:left; width:100%;">
            <span style="float:left;">临时密码：</span>
            <JWC:TextBoxEx ID="txtEmployeePassword2" runat="server" TextMode="Password" Width="168px"  />
        </div>--%>
		
		<div style="float:left; width:100%; margin-top:10px;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbGender" runat="server" Style="float:left;" meta:resourcekey="Org_Gender">性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别：</JWC:LabelEx>
			    <JWC:DropDownListEx ID="ddlGender" runat="server" Width="168px" />
			    <span>&nbsp;&nbsp;&nbsp;</span>
		    </div>
		    
            <div style="float:left;">
                <JWC:LabelEx ID="lbBirthday" runat="server" Style="float:left;" meta:resourcekey="Org_Birthday">出生日期：</JWC:LabelEx>
                <JWC:TextBoxEx ID="txtBirthday" runat="server" Width="168px" ValidationExpression="\d{4}-\d{2}-\d{2}" RegularErrorMessage="出生日期数据格式不正确(应如：2010-01-01)！" ToolTip="日期格式应如：2010-01-01" />
            </div>
		</div>
		
		<div style="float:left; width:100%;">
	    	<div style="float:left;">
	            <JWC:LabelEx ID="lbIdentityCard" runat="server" Style="float:left;" meta:resourcekey="Org_IdentityCard">身份证号：</JWC:LabelEx>
	            <JWC:TextBoxEx ID="txtIdentityCard" runat="server" Width="168px"  />
	            <span>&nbsp;&nbsp;</span>
            </div>
            
            <div style="float:left;">
		        <JWC:LabelEx ID="lbCardID" runat="server" Style="float:left;" meta:resourcekey="Org_CardID">工&nbsp;&nbsp;卡&nbsp;&nbsp;号：</JWC:LabelEx>
		        <JWC:TextBoxEx ID="txtCardID" runat="server" Width="168px"  />
	        </div>
		</div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbNation" runat="server" Style="float:left;" meta:resourcekey="Org_Nation">民&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;族：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtNation" runat="server" Width="168px"  />
		</div>
		
		<div style="float:left; width:100%;margin-top:10px;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbWorkTelNo" runat="server" Style="float:left;" meta:resourcekey="Org_WorkTelNo">工作电话：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtWorkTelNo" runat="server" Width="168px"  />
			    <span>&nbsp;&nbsp;</span>
		    </div>
		    
		     <div style="float:left;">
			    <JWC:LabelEx ID="lbMobileNo" runat="server" Style="float:left;" meta:resourcekey="Org_MobileNo">移动电话：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtMobileNo" runat="server" Width="168px"  />
		    </div>
		</div>
		
		<div style="float:left; width:100%;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbMSNNO" runat="server" Style="float:left;" meta:resourcekey="Org_MSNNO">M&nbsp;&nbsp;&nbsp;S&nbsp;&nbsp;N&nbsp;&nbsp;：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtMSNNO" runat="server" Width="168px"  />
			    <span>&nbsp;&nbsp;</span>
		    </div>
		    
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbQQNO" runat="server" Style="float:left;" meta:resourcekey="Org_QQNO">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Q&nbsp;&nbsp;Q&nbsp;&nbsp;&nbsp;：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtQQNO" runat="server" Width="168px"  />
		    </div>
		</div>
		
		<div style="float:left;width:100%;">
		    <JWC:LabelEx ID="lbEmail" runat="server" Style="float:left;" meta:resourcekey="Org_Email">电子邮件：</JWC:LabelEx>
		    <JWC:TextBoxEx ID="txtEmail" runat="server" Width="416px"  />
 		</div>
 		
 		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbAddress" runat="server" Style="float:left;" meta:resourcekey="Org_Address">联系地址：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtAddress" runat="server" Width="416px"  />
		</div>
	
	    <div style="float:left; width:100%;margin-top:10px;">
	        <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeStatus" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeStatus">状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：</JWC:LabelEx>
			    <JWC:DropDownListEx ID="ddlEmployeeStatus" runat="server" Width="168px" IsRequired="true" ErrorMessage="状态不能为空！" />
		    </div>
		    
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbOrderNo" runat="server" Style="float:left;" meta:resourcekey="Org_OrderNo">排&nbsp;&nbsp;序&nbsp;&nbsp;号：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtOrderNo" runat="server" Width="168px"  />
		    </div>
	    </div>
		
		<div style="float:left;width:100%;">
			<JWC:LabelEx ID="lbEmployeeDescription" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeDescription">描&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;述：</JWC:LabelEx>
			<JWC:TextBoxEx ID="txtEmployeeDescription" runat="server" TextMode="MultiLine" Rows="3" Width="416px"  />
		</div>
		
		<div style="float:left;width:100%;margin-top:5px;">
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx1" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx1">扩展字段1：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx1" runat="server" Width="168px"  />
		    </div>
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx2" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx2">扩展字段2：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx2" runat="server" Width="168px"  />
		    </div>
		</div>
		
		<div style="float:left;width:100%;">	
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx3" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx3">扩展字段3：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx3" runat="server" Width="168px"  />
		    </div>
		    <div style="float:left;">
			    <JWC:LabelEx ID="lbEmployeeEx4" runat="server" Style="float:left;" meta:resourcekey="Org_EmployeeEx4">扩展字段4：</JWC:LabelEx>
			    <JWC:TextBoxEx ID="txtEmployeeEx4" runat="server" Width="168px"  />
		    </div>
		</div>
	</div>
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
