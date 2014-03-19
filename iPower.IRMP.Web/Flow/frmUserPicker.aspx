<%@ Page Title="" Language="C#" MasterPageFile="Share/ModuleNormalMasterPage.Master" AutoEventWireup="true" CodeBehind="frmUserPicker.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmUserPicker" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>

<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmUserPicker" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	<!--数据录入区域-->
	<div style="text-align:center;">
	<!--选择用户-->
    <asp:Panel ID="EmployeePanel" runat="server" Visible="false" ScrollBars="Auto" CssClass="TableSearch" Height="390px">
        <fieldset style=" float:left; width:92%;">
            <legend>查询条件</legend>
            <div style="float:left; width:100%;">
                  <div style="float:left; width:100%">
                      <span style="float:left;">部门名称：</span>
                      <JWC:TextBoxEx ID="txtDepartmentName" runat="server" Width="168px" />
                  </div>
                  <div style="float:left; width:100%">
                      <span style="float:left;">关&nbsp;&nbsp;键&nbsp;&nbsp;字：</span>
                      <JWC:TextBoxEx ID="txtKey" runat="server" Width="168px" />
                  </div>
                  <div style="float:left; width:60%">
                      <span style="float:left;">性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别：</span>
                      <JWC:DropDownListEx ID="ddlSex" runat="server" Width="80px">
                        <asp:ListItem Value="" Text="==所有==" />
                        <asp:ListItem Value="男" Text="男" />
                        <asp:ListItem Value="女" Text="女" />
                      </JWC:DropDownListEx>
                  </div>
                  <div style="float:right; width:40%">
                       <JWC:ButtonEx ID="btnEmployeSearch" runat="server" ButtonType="Search" CausesValidation="true" onclick="btnEmployeSearch_Click"/>
                  </div>
            </div>
        </fieldset>
        <asp:Panel ID="EmployeePanelMultiSelect" runat="server" Visible="false" ScrollBars="Auto" CssClass="TableSearch" Height="280px">
            <table cellpadding="1" cellspacing="1" border="0" style="width:100%; height:280px;">
                <tr>
                    <td valign="top" width="40%">
                        <asp:ListBox ID="lbEmployeeMulti" runat="server" Width="100%" Height="270px" SelectionMode="Multiple" />
                    </td>
                    <td width="20%">
                        <p align="center">
                            <JWC:ButtonEx ID="btnEmployeeSelectAll" runat="server" CausesValidation="false" Text="&gt;&gt;" OnClick="btnEmployeeSelectAll_OnClick"/>
                        </p>
                        <p align="center">
                            <JWC:ButtonEx ID="btnEmployeeSelect" runat="server" CausesValidation="false" Text="&gt;"  OnClick="btnEmployeeSelect_OnClick"/>
                        </p>
                        <p align="center">
                            <JWC:ButtonEx ID="btnEmployeeRemove" runat="server" CausesValidation="false" Text="&lt;" OnClick="btnEmployeeRemove_OnClick" />
                        </p>
                        <p align="center">
                            <JWC:ButtonEx ID="btnEmployeeRemoveAll" runat="server" CausesValidation="false" Text="&lt;&lt;" OnClick="btnEmployeeRemoveAll_OnClick" />
                        </p>
                    </td>
                    <td valign="top" width="40%">
                        <asp:ListBox ID="lbEmployeeSelect" runat="server" Width="100%" Height="270px" SelectionMode="Multiple" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="EmployeePanelSelect" runat="server" Visible="false" ScrollBars="Auto" CssClass="TableSearch" Height="280px">
            <asp:ListBox ID="lbEmployeeSingleSelect" runat="server" Width="60%" Height="270px" SelectionMode="Single" />
        </asp:Panel>
    </asp:Panel>
    <!--选择角色-->
    <asp:Panel ID="RolePanel" runat="server" Visible="false"  ScrollBars="Auto" CssClass="TableSearch" Height="380px">
        <asp:CheckBoxList ID="chkRole" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" Width="98%"/>
    </asp:Panel>
    <!--选择岗位级别-->
    <asp:Panel ID="RankPanel" runat="server" Visible="false"  ScrollBars="Auto" CssClass="TableSearch" Height="380px">
        <asp:CheckBoxList ID="chkRank" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" Width="98%"/>
    </asp:Panel>
    <!--选择岗位-->
    <asp:Panel ID="PostPanel" runat="server" Visible="false"  ScrollBars="Auto" CssClass="TableSearch" Height="380px">
        <asp:CheckBoxList ID="chkPost" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" Width="98%" />
    </asp:Panel>
    </div>
	
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
