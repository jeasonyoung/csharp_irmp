<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Share/ModuleFrameSetMasterPage.Master" CodeBehind="Default.aspx.cs" Inherits="iPower.IRMP.Flow.Web._Default" %>

<asp:Content ID="WorkContent" ContentPlaceHolderID="workPlace" runat="server">
<style type="text/css">
 #silverlightControlHost {
	    height: 100%;
	    text-align:center;
    }
</style>
<script language="javascript" type="text/javascript">
    function onSilverlightError(sender, args) {
        var appSource = "";
        if (sender != null && sender != 0) {
            appSource = sender.getHost().Source;
        }

        var errorType = args.ErrorType;
        var iErrorCode = args.ErrorCode;

        if (errorType == "ImageError" || errorType == "MediaError") {
            return;
        }

        var errMsg = "应用程序中未处理的错误" + appSource + "\n";

        errMsg += "代码: " + iErrorCode + "    \n";
        errMsg += "类别: " + errorType + "       \n";
        errMsg += "消息: " + args.ErrorMessage + "     \n";

        if (errorType == "ParserError") {
            errMsg += "文件: " + args.xamlFile + "     \n";
            errMsg += "行: " + args.lineNumber + "     \n";
            errMsg += "位置: " + args.charPosition + "     \n";
        }
        else if (errorType == "RuntimeError") {
            if (args.lineNumber != 0) {
                errMsg += "行: " + args.lineNumber + "     \n";
                errMsg += "位置: " + args.charPosition + "     \n";
            }
            errMsg += "方法名称: " + args.methodName + "     \n";
        }

        throw new Error(errMsg);
    }
</script>

<%--<div id="silverlightControlHost">
 <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
		  <param name="source" value="Client/Yaesoft.IRMP.Flow.Design.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="3.0.40818.0" />
		  <param name="autoUpgrade" value="true" />
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="获取 Microsoft Silverlight" style="border-style:none"/>
		  </a>
 </object>
<iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe>
</div>--%>

</asp:Content>