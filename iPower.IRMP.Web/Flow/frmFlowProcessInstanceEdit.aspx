<%--
//================================================================================
// FileName: frmFlowProcessInstanceEdit.aspx
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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleNormalMasterPage.Master" CodeBehind="frmFlowProcessInstanceEdit.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowProcessInstanceEdit" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>
<asp:Content ID="contentWorkPlace" ContentPlaceHolderID="workPlace" runat="server">
	<JWC:ValidationSummaryEx id="vsfrmFlowProcessInstanceEdit" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
	
	<!--����¼������-->
	<JWC:TabMultiView ID="tabMultiView" runat="server" CssClass="TableSearch" Width="98%" Height="410px" DefaultActiveTabIndex="0">
	    
	    <JWC:TabView ID="tabProcessInstanceInfo" runat="server" Text="������Ϣ" TabIndex="0">
	         
            <div style="float:left;width:100%;">
                <span style="float:left;">�������ƣ�</span>
                <JWC:TextBoxEx ID="txtProcessName" runat="server" TextMode="MultiLine" Rows="3" Width="418px" ReadOnly="true"/>
            </div>

            <div style="float:left;width:100%;">
                <span style="float:left;">ʵ�����ƣ�</span>
                <JWC:TextBoxEx ID="txtProcessInstanceName" runat="server" TextMode="MultiLine" Rows="3" Width="418px" ReadOnly="true" />
            </div>
    		 
            <div style="float:left;width:100%;">
                <span style="float:left;">����ʱ�䣺</span>
                <JWC:TextBoxEx ID="txtCreateDate" runat="server" Width="168px" ReadOnly="true" />
            </div>
            
            <div style="float:left;width:100%;">
                <span style="float:left;"> ����ʱ�䣺</span>
                <JWC:TextBoxEx ID="txtEndDate" runat="server" Width="168px" ReadOnly="true" />
            </div>
    		 
	        <div style="float:left;">
		        <span style="float:left;">ʵ��״̬��</span>
		        <JWC:DropDownListEx ID="ddlFlowInstanceStatus" runat="server" Width="168px" IsRequired="true" ErrorMessage="����ʵ��״̬����Ϊ�գ�" />
	        </div>
		    
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabStepInstanceInfo" runat="server" TabIndex="1" Text="����ʵ��">
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
				            HeaderText="��������" DataField="StepName" SortExpression="StepName">
				            <HeaderStyle Width="40%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:MultiQueryStringFieldEx>
    			     
			            <JWC:BoundFieldEx DataField="FromEmployeeName" HeaderText="�ƽ��û�" SortExpression="FromEmployeeName">
				            <HeaderStyle Width="20%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:BoundFieldEx>
            			 
			            <JWC:BoundFieldEx DataField="CreateDate" HeaderText="����ʱ��" SortExpression="CreateDate" DataFormatString="{0:yyyy-MM-dd}">
				            <HeaderStyle Width="15%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="EndDate" HeaderText="����ʱ��" SortExpression="EndDate" DataFormatString="{0:yyyy-MM-dd}">
				            <HeaderStyle Width="15%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="InstanceStepStatusName" HeaderText="����״̬" SortExpression="InstanceStepStatusName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
		            </Columns>
	            </JWC:DataGridView>
	        </div>
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabStepInstanceTask" runat="server" TabIndex="2" Text="���̲�������">
	        <div style="float:left; width:100%; height:400px; overflow:auto;">
	            <div class="TitleBar">
	                <div style="float:right;">
	                    <JWC:ButtonEx ID="btnDelete" runat="server" ButtonType="Del" ShowConfirmMsg="true" ConfirmMsg="��ȷ��Ҫɾ����ǰ��ѡ���������" onclick="btnDelete_Click" />
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
    			     
			            <JWC:BoundFieldEx DataField="StepInstanceName" HeaderText="��������" SortExpression="StepInstanceName" ShowToolTip="true" ToolTipField="URL">
				            <HeaderStyle Width="28%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:BoundFieldEx>
    			        
			            <JWC:BoundFieldEx DataField="EmployeeName" HeaderText="�ƽ��û�" SortExpression="EmployeeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			            <JWC:BoundFieldEx DataField="AuthorizeEmployeeName" HeaderText="��Ȩ�û�" SortExpression="AuthorizeEmployeeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="DoEmployeeName" HeaderText="ִ���û�" SortExpression="DoEmployeeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="TaskCategoryName" HeaderText="��������" SortExpression="TaskCategoryName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="BeginModeName" HeaderText="����״̬" SortExpression="BeginModeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
			            
			             <JWC:BoundFieldEx DataField="EndModeName" HeaderText="�뿪״̬" SortExpression="EndModeName">
				            <HeaderStyle Width="10%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="BeginDate" HeaderText="�ƽ�ʱ��" SortExpression="BeginDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
				            <HeaderStyle Width="12%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
		            </Columns>
	            </JWC:DataGridView>
	        </div>
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabStepInstanceRunError" runat="server" TabIndex="3" Text="�����쳣��¼">
	        <div style="float:left; width:100%; height:400px; overflow:auto;">
	             <JWC:DataGridView ID="dgStepInstanceRunError" runat="server" CssClass="DataGrid" Width="98%" ShowFooter="false"
		            AllowSorting="true" AllowPaging="false" MouseoverCssClass="DataGridHighLight" onbuilddatasource="dgStepInstanceRunError_BuildDataSource">
		            <PagerSettings Mode="NextPreviousFirstLast" />
		            <AlternatingRowStyle CssClass="DataGridAlter" />
		            <HeaderStyle CssClass="DataGridHeader" />
		            <FooterStyle CssClass="DataGridFooter" />
		            <RowStyle CssClass="DataGridItem" />
		            <Columns>
    			     
			            <JWC:BoundFieldEx DataField="StepName" HeaderText="��������" SortExpression="StepName">
				            <HeaderStyle Width="28%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Left" />
			            </JWC:BoundFieldEx>
    			        
			            <JWC:BoundFieldEx DataField="ErrorMessage" HeaderText="�����쳣��Ϣ" SortExpression="ErrorMessage" ShowToolTip="true" ToolTipField="ErrorMessage" >
				            <HeaderStyle Width="56%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
            			
			            <JWC:BoundFieldEx DataField="CreateDate" HeaderText="ʱ��" SortExpression="CreateDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
				            <HeaderStyle Width="14%" HorizontalAlign="Center" />
				            <ItemStyle HorizontalAlign="Center" />
			            </JWC:BoundFieldEx>
		            </Columns>
	            </JWC:DataGridView>
	        </div>
	    </JWC:TabView>
	    
	    <JWC:TabView ID="tabProcessInstanceChart" runat="server" TabIndex="5" Text="����ͼ">
	        <div class="TableControl" style="height:400px; overflow:auto;">
	            <img alt="����ͼ" style="width:100%; height:100%;" src="FlowChartHandler.ashx?ProcessInstanceID=<%=this.ProcessInstanceID %>"/>
	        </div>
	    </JWC:TabView>
	      
	    <JWC:TabView ID="tabProcessResumes" runat="server" TabIndex="5" Text="��������">
	        <div style="float:left; width:100%; height:400px; overflow:auto;">
	            <asp:Repeater id="rpProcessResumes" runat="server">
	                 <ItemTemplate>
	                    <div class="TableSearch">
	                        <div style="float:left;width:100%;">
                                <span>�������ƣ�</span>
                                <div style="float:left; width:418px; border-bottom:solid 1px #000; margin-top:10px;">
                                    <%# Eval("StepInstanceName")%>
                                </div>
                            </div>
	                        <div style="float:left; width:100%;">
	                             <div style="float:left;">
	                                    <span>����û���</span>
	                                    <div style="float:left; width:168px; border-bottom:solid 1px #000; margin-top:10px;">
	                                        <%#Eval("DoEmployeeName")%>
	                                    </div>
	                            </div>
	                            <div style="float:left; margin-left:10px;">
	                                   <span>����ʱ�䣺</span>
	                                   <div style="float:left; width:168px; border-bottom:solid 1px #000; margin-top:10px;">
	                                        <%# Eval("ApprovalDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
	                                   </div>
	                             </div>
	                         </div>
    	                     
	                         <fieldset style="float:left; width:98%;margin-top:10px;">
	                            <legend>�������</legend>
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
	
	<!--���ݿ�������-->
	<div class="TableControl">
		<div style="margin:0 auto; text-align:center; width:100%;">
		    <JWC:ServerAlert ID="errMsg" runat="server" />
			<JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="��ȷ���������ݣ�" ShowConfirmMsg="true"/>
			<JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
		</div>
	</div>
</asp:Content>
