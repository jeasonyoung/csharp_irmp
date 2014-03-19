<%@ Page Title="" Language="C#" MasterPageFile="Share/ModuleNormalMasterPage.Master" AutoEventWireup="true" CodeBehind="frmFlowProcessImport.aspx.cs" Inherits="iPower.IRMP.Flow.Web.frmFlowProcessImport" %>
<%@ Register assembly="iPower.Web" namespace="iPower.Web.UI" tagprefix="JWC" %>

<%--<asp:Content ID="contentHeader" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
     v\:* { behavior: url(#default#VML); }
 </style>
 
 <script language="javascript" type="text/javascript">
     //Select Current Element.
     var lastItem = null, lineSel = null;
     //Check the point is on the direction of the line
     function CheckPosValid(x1, y1, x2, y2, x3, y3) {
         //alert("x1=" + x1 + ", y1=" + y1 + ",x2=" + x2 + ", y2=" + y2 + ", x3=" + x3 + ",y3=" + y3);
         if (x2 == x3)
             return ((y3 > y2 && y1 >= y3) || (y3 < y2 && y1 <= y3));
         return ((x3 > x2 && x1 >= x3) || (x3 < x2 && x1 <= x3));
     }
     //GetLinePoints
     function GetLinePoints(fromID, toID) {
         var pts = "";
         var diff = 150;
         var fs = document.getElementById(fromID);
         var ft = document.getElementById(toID);

         if (fs && ft) {
             var x1 = fs.style.posLeft;
             var y1 = fs.style.posTop;
             var x2 = ft.style.posLeft;
             var y2 = ft.style.posTop;
             var dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;

             y1 += fs.style.posHeight;
             x1 += fs.style.posWidth / 2;
             dy1 = diff;
             dx1 = 0;

             x2 += ft.style.posWidth / 2;
             dy2 = -diff;
             dx2 = 0;


             pts = x1 + "," + y1 + "," + (x1 + dx1) + "," + (y1 + dy1);
             var x11 = x1 + dx1;
             var y11 = y2 + dy2;
             var x22 = 0, y22 = 0;
             var b = CheckPosValid(x11, y11, x1, y1, x1 + dx1, y1 + dy1) && CheckPosValid(x11, y11, x2, y2, x2 + dx2, y2 + dy2);
             if (!b) {
                 x11 = x2 + dx2;
                 y11 = y1 + dy1;
                 b = CheckPosValid(x11, y11, x1, y1, x1 + dx1, y1 + dy1) && CheckPosValid(x11, y11, x2, y2, x2 + dx2, y2 + dy2);
                 if (!b) {
                     x22 = x2 + dx2;
                     x11 = x1 + dx1;
                     if (Math.abs(fs.style.posTop - ft.style.posTop) > (500 + diff))
                         y11 = (fs.style.posTop + ft.style.posTop) / 2 + 250;
                     else
                         y11 = Math.max(fs.style.posTop, ft.style.posTop) + 500 + diff;
                     y22 = y11;
                     b = CheckPosValid(x11, y11, x1, y1, x1 + dx1, y1 + dy1) && CheckPosValid(x22, y22, x2, y2, x2 + dx2, y2 + dy2);
                     if (!b) {
                         y11 = y1 + dy1;
                         y22 = y2 + dy2;
                         if (Math.abs(fs.style.posLeft - ft.style.posLeft) > (1000 + diff))
                             x11 = (fs.style.posLeft + ft.style.posLeft) / 2 + 500;
                         else
                             x11 = Math.min(fs.style.posLeft, ft.style.posLeft) - diff;
                         x22 = x11;

                         b = CheckPosValid(x11, y11, x1, y1, x1 + dx1, y1 + dy1) && CheckPosValid(x22, y22, x2, y2, x2 + dx2, y2 + dy2);
                         if (!b) {
                             x11 = null;
                             y22 = null;
                         }
                     }
                 }
             }

             if (x11 && y11)
                 pts += "," + x11 + "," + y11;
             if (x22 && y22)
                 pts += "," + x22 + "," + y22;

             pts += "," + (x2 + dx2) + "," + (y2 + dy2);
             pts += "," + x2 + "," + y2;
         }
         //alert(pts);
         return pts;
     }
     //Draw Relation
     function DrawRelation(id, fromID, toID, title) {
         var group = document.getElementById("WorkFlowGroup");
         if (!group)
             return;
         var points = GetLinePoints(fromID, toID);
         var l = document.getElementById(id);
         if (l)
             l.points.value = points;
         else {
             var stroke = "<v:stroke color='black' StartArrow='Oval' EndArrow='classic'></v:stroke>";
             var s = document.createElement(stroke);
             s.color = "blue";
             var line = "<v:polyline filled='false'onclick='OnLineClick(this)' onmouseover='OnLineOver(this)' style='position:relative;z-index:9001;' id='" + id + "' points='" + points + "' fromID='" + fromID + "' toID='" + toID + "' title='" + title + "'></v:polyline>";
             l = document.createElement(line);
             l.appendChild(s);
             group.appendChild(l);
         }
     }
     //Cap on line mouse over
     function OnLineOver(obj) {
         if (obj)
             obj.style.cursor = "hand";
     }
     //Cap on line Click
     function OnLineClick(obj) {
         if (obj) {
             if (lineSel) {
                 lineSel.style.zIndex = 9001;
                 lineSel.strokecolor = "blue";
             }
             if (lastItem) {
                 var s = lastItem.type;
                 lastItem.type = s.substring(0, s.length - 1) + "1";
                 lastItem = null;
             }

             lineSel = obj;
             lineSel.strokecolor = "red";
             lineSel.style.zIndex = 9002;
         }
     }
 </script>
</asp:Content>--%>

<asp:Content ID="Content" ContentPlaceHolderID="workPlace" runat="server">
<JWC:ValidationSummaryEx id="vsfrmFlowProcessImport" runat="server"  ShowMessageBox="true" ShowSummary="false"/>

<asp:Panel ID="importPanel" runat="server" Width="100%" Visible="true">
   <!--数据录入区域-->
    <div class="TableSearch">
        <div style="float:left;">
            <span style="float:left;">文件地址：</span>
            <asp:FileUpload ID="uploadProcess" runat="server" Width="298px" />
            <asp:RequiredFieldValidator ID="requiredFieldUploadProcess"	 runat="server" ControlToValidate="uploadProcess" ErrorMessage="文件地址不能为空！" EnableClientScript="true" />	
        </div>
    </div>
   
   <div class="TableControl">
        <div style="margin:0 auto; text-align:center; width:100%;">
	        <JWC:ButtonEx ID="btnImportSave" runat="server" ButtonType="Save" onclick="btnImportSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
	        <JWC:ButtonEx ID="btnImportCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
        </div>
   </div>
</asp:Panel>

<asp:Panel ID="drawChartPanel" runat="server" Width="100%" Visible="false">
    <script language="javascript" type="text/javascript">
        //Resize the modal dialog
        function ResizeDialog(width, height) {
            var vOpener = window.opener;
            if (vOpener == "undefined" || vOpener == null)//modal dialog
            {
                window.dialogWidth = width + "px";
                window.dialogHeight = height + "px";
                window.dialogTop = (screen.height - height) / 2 + "px";
                window.dialogLeft = (screen.width - width) / 2 + "px";
            }
            else //normal window
            {
                window.moveTo((screen.width - width) / 2, (screen.height - height - 76) / 2)
                window.resizeTo(width, height + 76)
            }
        }
        ResizeDialog(800, 600);
    </script>
    <!--流程图设计-->
	<table id="tblDesinger" cellspacing="0" cellpadding="0" width="98%" border="0">
	    <tr>
	        <td valign="top" nowrap="nowrap" align="left">
	            <div id="FlowDesinger" style="border:solid 1px blue; font-size:12px; z-index:101; width:99%; height:550px; overflow:auto;">
	               <%-- <asp:xml id="WorkflowDisplay" runat="server" TransformSource="FlowChartDesign.xslt" />--%>
	            </div>
	        </td>
	    </tr>
	</table>
	
	 <div class="TableControl">
        <div style="margin:0 auto; text-align:center; width:100%;">
	        <JWC:ButtonEx ID="btnSave" runat="server" ButtonType="Save" onclick="btnSave_Click" CausesValidation="true" ConfirmMsg="您确定保存数据？" ShowConfirmMsg="true"/>
	        <JWC:ButtonEx ID="btnCancel" runat="server" ButtonType="Cancel" LeftSpace="2" beforeclickscript='window.returnValue="";window.close();return false;'/>
        </div>
   </div>
</asp:Panel>

<JWC:ServerAlert ID="errMessage" runat="server" />

</asp:Content>
