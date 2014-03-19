<%--
//================================================================================
// FileName: frmFlowProcessInstanceEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowProcessInstanceEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowProcessInstanceEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowProcessInstanceEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--数据录入区域-->
	<JWC:TabMultiView ID="tabMultiView" runat="server" CssClass="TableSearch" Width="98%" Height="410px" DefaultActiveTabIndex="0">
	    
	    <JWC:TabView ID="tabProcessInstanceInfo" runat="server" Text="基本信息" TabIndex="0">
	         
            <div style="float:left;width:100%;">
                <span style="float:left;">流程名称：</span>
                <JWC:TextBoxEx ID="txtProcessName" runat="server" TextMode="MultiLine" Rows="3" Width="418px" ReadOnly="true"/>
            </div>

            <div style="float:left;width:100%;">
                <span style="float:left;">实例名称：</span>
                <JWC:TextBoxEx ID="txtProcessInstanceName" runat="server" TextMode="MultiLine" Rows="3" Width="418px" ReadOnly="true" />
            </div>
    		 
            <div style="float:left;width:100%;">
                <span style="float:left;">启动时间：</span>
                <JWC:TextBoxEx ID="txtCreateDate" runat="server" Width="168px" ReadOnly="true" />
            </div>
            
            <div style="float:left;width:100%;">
                <span style="float:left;"> 结束时间：</span>
                <JWC:TextBoxEx ID="txtEndDate" runat="server" Width="168px" ReadOnly="true" />
            </div>
    		 
	        <div style="float:left;">
		        <span style="float:left;">实例状态：</span>
		        <JWC:DropDownListEx ID="ddlFlowInstanceStatus" runat="server" Width="168px" IsRequired="true" ErrorMessage="流程实例状态不能为空！" />
	        </div>
		    
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabStepInstanceInfo" runat="server" TabIndex="1" Text="步骤实例">
	       <div style="float:left; width:100%; height:400px; overflow:auto;">
	            <JWC:DataGridView ID="dgStepInstance" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="true"
		            AllowSorting="true" AllowPaging="false" MouseoverCssClass="DataGridHighLight"
		            PageSize="15" onbuilddatasource="dgStepInstance_BuildDataSource">
		            <PagerSettings Mode="NextPreviousFirstLast" />
		            <AlternatingRowStyle CssClass="DataGridAlter" />
		            <HeaderStyle CssClass="DataGridHeader" />
		            <FooterStyle CssClass="DataGridFooter" />
		            <RowStyle CssClass="DataGridItem" />
		            <Columns>
    			        
    			        <JWC:MultiQueryStringFieldEx PopupWin="true" WinType="Modal" WinWidth="420px" WinHeight="240px"
				            DataNavigateUrlFormatString="frmFlowStepInstanceEdit.aspx?StepInstanceID={0}" DataNavigateUrlField="StepInstanceID"
				            HeaderText="步骤名称" DataField="StepName" SortExpression="StepName">
				            <HeaderStyle Width="40%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:MultiQueryStringFieldEx>
    			     
			            <JWC:BoundFieldEx DataField="FromEmployeeName" HeaderText="推进用户" SortExpression="FromEmployeeName">
				            <HeaderStyle Width="20%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:BoundFieldEx>
            			 
			            <JWC:BoundFieldEx DataField="CreateDate" HeaderText="启动时间" SortExpression="CreateDate" DataFormatString="{0:yyyy-MM-dd}">
				            <HeaderStyle Width="15%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="EndDate" HeaderText="结束时间" SortExpression="EndDate" DataFormatString="{0:yyyy-MM-dd}">
				            <HeaderStyle Width="15%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="InstanceStepStatusName" HeaderText="步骤状态" SortExpression="InstanceStepStatusName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
		            </Columns>
	            </JWC:DataGridView>
	        </div>
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabStepInstanceTask" runat="server" TabIndex="2" Text="流程步骤任务">
	        <div style="float:left; width:100%; height:400px; overflow:auto;">
	            <div class="TitleBar">
	                <div style="float:right;">
	                    <JWC:ButtonEx ID="btnDelete" runat="server" ButtonType="Del" ShowConfirmMsg="true" ConfirmMsg="您确定要删除当前所选择的数据吗？" onclick="btnDelete_Click" />
	                </div>
	            </div>
	            
	            <JWC:DataGridView ID="dgStepInstanceTask" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="false"
		            AllowSorting="true" AllowPaging="false" MouseoverCssClass="DataGridHighLight" onbuilddatasource="dgStepInstanceTask_BuildDataSource">
		            <PagerSettings Mode="NextPreviousFirstLast" />
		            <AlternatingRowStyle CssClass="DataGridAlter" />
		            <HeaderStyle CssClass="DataGridHeader" />
		            <FooterStyle CssClass="DataGridFooter" />
		            <RowStyle CssClass="DataGridItem" />
		            <Columns>
    			        <JWC:CheckBoxFieldEx DataField="TaskID">
				            <HeaderStyle Width="8px" />
			            </JWC:CheckBoxFieldEx>
    			     
			            <JWC:BoundFieldEx DataField="StepInstanceName" HeaderText="步骤名称" SortExpression="StepInstanceName" ShowToolTip="true" ToolTipField="URL">
				            <HeaderStyle Width="28%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:BoundFieldEx>
    			        
			            <JWC:BoundFieldEx DataField="EmployeeName" HeaderText="推进用户" SortExpression="EmployeeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			            <JWC:BoundFieldEx DataField="AuthorizeEmployeeName" HeaderText="授权用户" SortExpression="AuthorizeEmployeeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="DoEmployeeName" HeaderText="执行用户" SortExpression="DoEmployeeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="TaskCategoryName" HeaderText="任务类型" SortExpression="TaskCategoryName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="BeginModeName" HeaderText="进入状态" SortExpression="BeginModeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="EndModeName" HeaderText="离开状态" SortExpression="EndModeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="BeginDate" HeaderText="推进时间" SortExpression="BeginDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
				            <HeaderStyle Width="12%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
		            </Columns>
	            </JWC:DataGridView>
	        </div>
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabStepInstanceRunError" runat="server" TabIndex="3" Text="步骤异常记录">
	        <div style="float:left; width:100%; height:400px; overflow:auto;">
	             <JWC:DataGridView ID="dgStepInstanceRunError" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="false"
		            AllowSorting="true" AllowPaging="false" MouseoverCssClass="DataGridHighLight" onbuilddatasource="dgStepInstanceRunError_BuildDataSource">
		            <PagerSettings Mode="NextPreviousFirstLast" />
		            <AlternatingRowStyle CssClass="DataGridAlter" />
		            <HeaderStyle CssClass="DataGridHeader" />
		            <FooterStyle CssClass="DataGridFooter" />
		            <RowStyle CssClass="DataGridItem" />
		            <Columns>
    			     
			            <JWC:BoundFieldEx DataField="StepName" HeaderText="步骤名称" SortExpression="StepName">
				            <HeaderStyle Width="28%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:BoundFieldEx>
    			        
			            <JWC:BoundFieldEx DataField="ErrorMessage" HeaderText="错误异常信息" SortExpression="ErrorMessage" ShowToolTip="true" ToolTipField="ErrorMessage" >
				            <HeaderStyle Width="56%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="CreateDate" HeaderText="时间" SortExpression="CreateDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
				            <HeaderStyle Width="14%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
		            </Columns>
	            </JWC:DataGridView>
	        </div>
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabProcessInstanceChart" runat="server" TabIndex="5" Text="流程图">
	        <div class="TableControl" style="height:400px; overflow:auto;">
	            <img alt="流程图" style="width:100%; height:100%;" src="FlowChartHandler.ashx?ProcessInstanceID=<%=this.ProcessInstanceID %>"/>
	        </div>
	    </JWC:TabView>
	      
	    <JWC:TabView ID="tabProcessResumes" runat="server" TabIndex="5" Text="流程履历">
	        <div style="float:left; width:100%; height:400px; overflow:auto;">
	            <asp:Repeater id="rpProcessResumes" runat="server">
	                 <ItemTemplate>
	                    <div class="TableSearch">
	                        <div style="float:left;width:100%;">
                                <span>步骤名称：</span>
                                <div style="float:left; width:418px; border-bottom:solid 1px #000; margin-top:10px;">
                                    <%# Eval("StepInstanceName")%>
                                </div>
                            </div>
	                        <div style="float:left; width:100%;">
	                             <div style="float:left;">
	                                    <span>审核用户：</span>
	                                    <div style="float:left; width:168px; border-bottom:solid 1px #000; margin-top:10px;">
	                                        <%#Eval("DoEmployeeName")%>
	                                    </div>
	                            </div>
	                            <div style="float:left; margin-left:10px;">
	                                   <span>处理时间：</span>
	                                   <div style="float:left; width:168px; border-bottom:solid 1px #000; margin-top:10px;">
	                                        <%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
	                                   </div>
	                             </div>
	                         </div>
    	                     
	                         <fieldset style="float:left; width:98%;margin-top:10px;">
	                            <legend>审批意见</legend>
	                            <div style="float:left; width:100%; height:45px; overflow:auto; border:solid 1px #ccc;">
	                                <%# Eval("ApprovalViews")%>
	                            </div>
	                         </fieldset>
	                    </div>
	                 </ItemTemplate>
	            </asp:Repeater>
	        </div>
	    </JWC:TabView>
	</JWC:TabMultiView>
	
	<!--数据控制区域-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
		    <JWC:ServerAlert ID="errMsg" runat="server" />
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
