<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkDetails_View.aspx.cs"
    Inherits="LinkDetails_View" %>

<%--<%@ Register Src="ucFooter.ascx" TagName="ucFooter" TagPrefix="uc2" %>
--%>
<%--<%@ Register Src="ucHeader.ascx" TagName="ucHeader" TagPrefix="uc1" %>
--%>
<%--<%@ Register Src="UcLeftPannel.ascx" TagName="ucLeftPannel" TagPrefix="uc3" %>
--%><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>:: Link Details View ::</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="js/jquery.js"></script>

    <script type="text/javascript" src="js/ddaccordion.js"></script>

    <script type="text/javascript" src="js/ddaccordion1.js"></script>

    <script type="text/javascript" src="js/ajaxScript.js"></script>

    <script type="text/javascript" src="js/boxover.js"></script>

    <script type="text/javascript" language="javascript" src="js/JScript.js"></script>

    <link href="css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="jscripts/tiny_mce/tiny_mce.js"></script>

    <script type="text/javascript">

        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "advanced",
            plugins: "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount",

            // Theme options
            theme_advanced_buttons1: "bold,italic,underline,|,fontsizeselect,|,forecolor,backcolor",
            theme_advanced_buttons2: "",
            theme_advanced_buttons3: "",
            theme_advanced_buttons4: "",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "none",
            theme_advanced_resizing: true,

            // Example content CSS (should be your site CSS)
            content_css: "css/content.css",

            // Drop lists for link/image/media/template dialogs
            template_external_list_url: "lists/template_list.js",
            external_link_list_url: "lists/link_list.js",
            external_image_list_url: "lists/image_list.js",
            media_external_list_url: "lists/media_list.js",

            // Replace values for the template plugin
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }
        });
 

    </script>

    <script type="text/javascript">

        function CloseMailDiv(SlNo) {
            var dvMail = document.getElementById("dvMail_" + SlNo);
            if (dvMail != null) {
                //scroll(0,0);
                dvMail.style.display = "none";
                document.getElementById("spCloseRow" + SlNo).innerHTML = "";
                document.getElementById("spCloseRow" + SlNo).className = "";

            }
        }


        function closeRowDiv(SlNo) {
            var dvRow = document.getElementById("dvRow" + SlNo);
            document.getElementById("spMSg" + SlNo).innerHTML = "";
            if (dvRow != null) {
                //scroll(0,0);
                dvRow.style.display = "none";
                document.getElementById("spCloseRow" + SlNo).className = "";
                var type = document.getElementById("spddl" + SlNo).innerHTML.toString();
                // alert(type);
                var regEx = /<[^>]*>/g;
                if (type.indexOf("Approve") > -1) {
                    document.getElementById("spMSg" + SlNo).innerHTML = document.getElementById("spHTMLcode" + SlNo).innerHTML.toString().replace(regEx, "") + "has been Approved";
                }
                else {
                    document.getElementById("spMSg" + SlNo).innerHTML = document.getElementById("spHTMLcode" + SlNo).innerHTML.toString().replace(regEx, "") + "has been Rejected";

                }
            }
        }


        function funOpenAllMailDivs() {
            var to = "";
            if (document.getElementById("hdFrom").value != null && document.getElementById("hdFrom").value != null) {
                if (parseInt(document.getElementById("hdTo").value) > parseInt(document.getElementById("hdPagecount").value)) {
                    to = document.getElementById("hdPagecount").value;
                }
                else {
                    to = document.getElementById("hdTo").value;

                }
            }
            for (var i = parseInt(document.getElementById("hdFrom").value); i <= parseInt(to); i++) {
                document.getElementById("dvSendM" + i).style.display = "block";
            }
        }
 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="left" valign="top" class="header">
                <h1 class="logo">
                    <a href="#">
                        <img src="images/logo.gif" alt="Reliable Link Exchange" title="Reliable Link Exchange" /></a></h1>
            </td>
        </tr>
        <%-- <uc1:ucHeader ID="UcHeader1" runat="server" />--%>
        <tr>
            <td align="left" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="41" align="left" valign="top" class="shadowBGtop">
                            &nbsp;
                        </td>
                        <td align="left" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="4" align="left" valign="top" class="leftCornerBg" style="height: 7px">
                                    </td>
                                    <td align="left" valign="top" class="shadowBGtop1" style="height: 7px">
                                    </td>
                                    <td width="4" align="left" valign="top" class="rightCornerBg" style="height: 7px">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="4" align="left" valign="top" class="leftBg">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" class="tablePadd">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="206" align="left" valign="top">
                                                    <!--Left Panel Start -->
                                                    <%--  <uc3:ucLeftPannel ID="UcLeftPannel" runat="server" />--%>
                                                    <!--Left Panel End -->
                                                </td>
                                                <td align="left" valign="top" class="leftBorder">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td align="left" valign="top" class="sitenameHead">
                                                                <asp:Label ID="lblSiteName" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="dvRpt" style="display: none">
                                                                    <asp:Repeater ID="rptLinkDetail" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <div id="dvMailView<%#Eval("SlNo")%>">
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td align="left" valign="top" class="catnameHead">
                                                                                                    <span id="spSubPageName" runat="server">
                                                                                                        <%#Eval("SubPageName")%>
                                                                                                    </span>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="top" class="">
                                                                                                    <span id="spMSg<%#Eval("SlNo")%>"></span>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="top">
                                                                                                    <div id="dvRow<%#Eval("SlNo")%>">
                                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td align="left" valign="top" class="tableBorder">
                                                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                        <tr>
                                                                                                                            <td width="4%" align="center" valign="top" class="tableBorder1 tableBorder2">
                                                                                                                                SN
                                                                                                                            </td>
                                                                                                                            <td width="15%" align="center" valign="top" class="tableBorder1">
                                                                                                                                Reciprocal URL
                                                                                                                            </td>
                                                                                                                            <td width="30%" align="center" valign="top" class="tableBorder1">
                                                                                                                                Ad Given By Them
                                                                                                                            </td>
                                                                                                                            <td width="30%" align="center" valign="top" class="tableBorder1">
                                                                                                                                OUR AD
                                                                                                                            </td>
                                                                                                                            <td width="13%" align="center" valign="top" class="tableBorder1 tableBorder4">
                                                                                                                                MAIL | Action
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td height="1" colspan="5" align="center" valign="top" class="topBorder">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td align="center" valign="top" class="rightBorder sNo">
                                                                                                                                <%--  <asp:Literal ID="lit_SlNo" Visible="false"  runat="server" Text='<%# Eval("SlNo") %>' />
                                                                                                                                    <%-- <asp:Literal ID="lit_Sl"  runat="server" Text='<%# Eval("Sl") %>' />--%>
                                                                                                                                <asp:Literal ID="lit_SlNo" runat="server" Text='<%# Eval("SlNo") %>' />
                                                                                                                            </td>
                                                                                                                            <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                                <%#Eval("Reciprocal")%>
                                                                                                                            </td>
                                                                                                                            <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                                <span id="spHTMLcode<%#Eval("SlNo")%>">
                                                                                                                                    <%--<%#get_HTMLcode(Convert.ToString(Eval("HTMLcode")))%>--%>
                                                                                                                                    <%#Eval("HTMLcode")%>
                                                                                                                                </span>
                                                                                                                            </td>
                                                                                                                            <td align="center" valign="top" class="rightBorder addnameLink1">
                                                                                                                                <%--<a href="<%#Eval("SiteURL")%>" target="_blank">
                                                                                                                                        <%#Eval("Heading")%>
                                                                                                                                    </a>
                                                                                                                                    <%#get_description(Convert.ToString(Eval("Description")))%>--%>
                                                                                                                                <%#Eval("ouradd")%>
                                                                                                                            </td>
                                                                                                                            <td align="center" valign="top" class="btnMargin">
                                                                                                                                <span id="spApproved" runat="server">
                                                                                                                                    <img id="btnApproved" src="images/approved_btn.gif" alt="Approved" title="Approved"
                                                                                                                                        style="padding: 0 0 7px 0" onclick="javascript:funOpenMailDiv(<%#Eval("SlNo")%>,'Approved',<%#Eval("id")%>);" />
                                                                                                                                </span>
                                                                                                                                <br class="clear" />
                                                                                                                                <span id="spReject" runat="server">
                                                                                                                                    <img id="btnReject" src="images/reject_btn.gif" alt="Reject" title="Reject" onclick="javascript:funOpenMailDiv(<%#Eval("SlNo")%>,'Reject',<%#Eval("id")%>);"
                                                                                                                                        style="padding: 0 0 7px 0" />
                                                                                                                                </span>
                                                                                                                                <br class="clear" />
                                                                                                                                <span id="spSendMail" runat="server">
                                                                                                                                    <img id="btnSendMail" src="images/sendMail_btn.gif" alt="SendMail" title="SendMail"
                                                                                                                                        onclick="javascript:funOpenMailDiv(<%#Eval("SlNo")%>,'SendMail',<%#Eval("id")%>);" />
                                                                                                                                </span>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td colspan="5" align="center" valign="top" class="tdPadd">
                                                                                                                                <div id="dvMail_<%#Eval("SlNo")%>" style="display: none">
                                                                                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableBorder3">
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="2" align="right" valign="middle" class="closeBtn">
                                                                                                                                                <a href="javascript:funCloseMailDiv(<%#Eval("SlNo")%>,<%#Eval("id")%>);">Close</a>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td width="30%" align="right" valign="top" class="formTxt">
                                                                                                                                                From:
                                                                                                                                            </td>
                                                                                                                                            <td align="left" valign="middle">
                                                                                                                                                <span id="spFrom<%#Eval("SlNo")%>"></span>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right" valign="top" class="formTxt">
                                                                                                                                                Template:
                                                                                                                                            </td>
                                                                                                                                            <td align="left" valign="middle">
                                                                                                                                                <%-- <input type="text" name="textfield2" class="formTxtField" />--%>
                                                                                                                                                <span id="spddl<%#Eval("SlNo")%>"></span><span id="dvAjaxPic_ddl<%#Eval("SlNo")%>"
                                                                                                                                                    class="loadImage1" style="display: none">
                                                                                                                                                    <img src="http://www.reliablelinkexchange.com/images/loading.gif" />
                                                                                                                                                </span>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right" valign="top" class="formTxt">
                                                                                                                                                To:
                                                                                                                                            </td>
                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                <input id="txtTo<%#Eval("SlNo")%>" type="text" name="textfield3" class="formTxtField" />
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right" valign="top" class="formTxt">
                                                                                                                                                Subject:
                                                                                                                                            </td>
                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                <input id="txtTemplateSubject<%#Eval("SlNo")%>" type="text" name="textfield4" class="formTxtField" />
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right" colspan="2" valign="top" class="formTxt">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right" valign="top" class="formTxt">
                                                                                                                                                Body:
                                                                                                                                            </td>
                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                <textarea id="txtTemplateCode<%#Eval("SlNo")%>" name="textarea" class="textField1"
                                                                                                                                                    style="width: 403px; height: 450px;"></textarea>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right" valign="top">
                                                                                                                                                &nbsp;
                                                                                                                                            </td>
                                                                                                                                            <td align="left" valign="top" class="formPadding1">
                                                                                                                                                <input type="button" id="btnMailSend" onclick="Insert_MailSend(<%#Eval("SlNo")%>,'Insert_MailSend',<%#Eval("id")%>);"
                                                                                                                                                    value="Send" class="submitBtn2 btnmargin1" />
                                                                                                                                                <input type="button" onclick="javascript:funCloseMailDiv(<%#Eval("SlNo")%>,<%#Eval("id")%>);"
                                                                                                                                                    id="btnDiscard<%#Eval("SlNo")%>" value="Discard" class="finalBtn" />
                                                                                                                                                <span id="spUpdateStatus<%#Eval("SlNo")%>">
                                                                                                                                                    <input type="button" id="btnUpdate_withoutMail" onclick="Insert_MailSend(<%#Eval("SlNo")%>,'Update_withoutMail',<%#Eval("id")%>);"
                                                                                                                                                        value="Update Status Without Sending Mail" class="updateBtn" />
                                                                                                                                                </span>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </div>
                                                                                                                                <span id="dvAjaxPic<%#Eval("SlNo")%>" class="loadImage1" style="display: none">
                                                                                                                                    <img src="images/loading.gif" />
                                                                                                                                </span>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td height="5" colspan="5" align="center" valign="top" class="bottomBorder">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td height="5" colspan="5" align="center" valign="top">
                                                                                                                    <span id="spCloseRow<%#Eval("SlNo")%>"></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="top" class="">
                                                                                                    <span class="addnameLink" id="spCount<%#Eval("SlNo")%>">
                                                                                                        <%#get_MailCount(Convert.ToInt32(Eval("Id")), Convert.ToString(Eval("SlNo")))%>
                                                                                                    </span>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <div id="dvMailContent<%#Eval("SlNo")%>" style="display: none">
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td align="left" valign="top" class="catnameHead">
                                                                                                    <span id="spBack<%#Eval("SlNo")%>"></span>&nbsp; <span id="spSubPageNameM<%#Eval("SlNo")%>">
                                                                                                    </span>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="top" class="tableBorder">
                                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                        <tr>
                                                                                                            <td width="7%" align="center" valign="top" class="tableBorder1 tableBorder2">
                                                                                                                S. No
                                                                                                            </td>
                                                                                                            <td width="20%" align="center" valign="top" class="tableBorder1">
                                                                                                                Reciprocal URL
                                                                                                            </td>
                                                                                                            <td align="center" valign="top" class="tableBorder1">
                                                                                                                Ad Given By Them
                                                                                                            </td>
                                                                                                            <td width="20%" align="center" valign="top" class="tableBorder1">
                                                                                                                OUR AD
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td height="1" colspan="5" align="center" valign="top" class="topBorder">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" valign="top" class="rightBorder sNo">
                                                                                                                1.
                                                                                                            </td>
                                                                                                            <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                <span id="spReciprocalM<%#Eval("SlNo")%>"></span>
                                                                                                            </td>
                                                                                                            <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                <span id="spouraddM<%#Eval("SlNo")%>"></span>
                                                                                                            </td>
                                                                                                            <td align="center" valign="top" class="rightBorder addnameLink1">
                                                                                                                <span id="spHTMLcodeM<%#Eval("SlNo")%>"></span>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="5" align="left" valign="top" class="mailPadd closeBtn">
                                                                                                                Following mails have been send to
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <tr>
                                                                                                                <td colspan="5" align="left" valign="top" class="btmtdPadd">
                                                                                                                    <%--<table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                        <tr>
                                                                                                                            <td align="left" valign="top">
                                                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                    <tr>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                            <div class="posRel">
                                                                                                                                                <div class="curveIns">
                                                                                                                                                    <span class="lftTop"></span><span class="rgtTop"></span>
                                                                                                                                                </div>
                                                                                                                                                <div class="mailBordr">
                                                                                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                        <tr>
                                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td width="25%" align="left" valign="top" class="sendShortTxt">
                                                                                                                                                                            Aug 24 (5 days ago)</td>
                                                                                                                                                                        <td align="left" valign="top" class="redBold">
                                                                                                                                                                            <span>Template Name</span></td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                                <div class="posRel">
                                                                                                                                                                    <div class="curveIns">
                                                                                                                                                                        <span class="lftTop"></span><span class="rgtTop"></span>
                                                                                                                                                                    </div>
                                                                                                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td width="25%" align="left" valign="top" class="sendShortTxt">
                                                                                                                                                                                Aug 24 (5 days ago)</td>
                                                                                                                                                                            <td align="left" valign="top" class="redBold">
                                                                                                                                                                                <span>Template Name</span></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </div>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </table>
                                                                                                                                                </div>
                                                                                                                                                <div class="botmBlueBg">
                                                                                                                                                    <span class="botmLft"></span><span class="botmRgt"></span>
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            <td width="15%" align="left" valign="top">
                                                                                                                                <p class="sendDate">
                                                                                                                                    <span><a href="#" title="Expand All">Expand All</a></span></p>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>--%>
                                                                                                                    <div class="mailBordr" id="dvMailSendM<%#Eval("SlNo")%>">
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td width="15%" align="left" valign="top">
                                                                                                                    <p class="sendDate">
                                                                                                                        <span><a id="dvExpand<%#Eval("SlNo")%>" href="javascript:funExpand(<%#Eval("SlNo")%>);"
                                                                                                                            title="Expand All">Expand All</a></span></p>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <td height="5" colspan="5" align="center" valign="top" class="bottomBorder">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </table>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                    <asp:Label ID="lblPage" runat="server" Text="Label"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="dvSubPageWise">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <asp:Repeater ID="rptSubPageWise" runat="server">
                                                                            <HeaderTemplate>
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                        </td>
                                                                                    </tr>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <div id="dvMailView<%#Eval("SlNo")%>">
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td align="left" valign="top" class="catnameHead">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="top" class="">
                                                                                                        <span id="spMSg<%#Eval("SlNo")%>"></span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="top">
                                                                                                        <div id="dvRow<%#Eval("SlNo")%>">
                                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="top" class="tableBorder">
                                                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                            <tr>
                                                                                                                                <td width="4%" align="center" valign="top" class="tableBorder1 tableBorder2">
                                                                                                                                    SN
                                                                                                                                </td>
                                                                                                                                <td width="15%" align="center" valign="top" class="tableBorder1">
                                                                                                                                    Reciprocal URL
                                                                                                                                </td>
                                                                                                                                <td width="30%" align="center" valign="top" class="tableBorder1">
                                                                                                                                    Ad Given By Them
                                                                                                                                </td>
                                                                                                                                <td width="30%" align="center" valign="top" class="tableBorder1">
                                                                                                                                    OUR AD
                                                                                                                                </td>
                                                                                                                                <td width="13%" align="center" valign="top" class="tableBorder1 tableBorder4">
                                                                                                                                    MAIL | Action
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td height="1" colspan="5" align="center" valign="top" class="topBorder">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td align="center" valign="top" class="rightBorder sNo">
                                                                                                                                    <%--  <asp:Literal ID="lit_SlNo" Visible="false"  runat="server" Text='<%# Eval("SlNo") %>' />--%>
                                                                                                                                    <%-- <asp:Literal ID="lit_Sl"  runat="server" Text='<%# Eval("Sl") %>' />--%>
                                                                                                                                    <asp:Literal ID="lit_SlNo" runat="server" Text='<%# Eval("SlNo") %>' />
                                                                                                                                </td>
                                                                                                                                <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                                    <%#Eval("Reciprocal")%>
                                                                                                                                </td>
                                                                                                                                <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                                    <span id="spHTMLcode<%#Eval("SlNo")%>">
                                                                                                                                        <%--<%#get_HTMLcode(Convert.ToString(Eval("HTMLcode")))%>--%>
                                                                                                                                        <%#Eval("HTMLcode")%>
                                                                                                                                    </span>
                                                                                                                                </td>
                                                                                                                                <td align="center" valign="top" class="rightBorder addnameLink1">
                                                                                                                                    <%#Eval("ouradd")%>
                                                                                                                                </td>
                                                                                                                                <td align="center" valign="top" class="btnMargin">
                                                                                                                                    <span id="spApproved" runat="server">
                                                                                                                                        <img id="btnApproved" src="images/approved_btn.gif" alt="Approved" title="Approved"
                                                                                                                                            style="padding: 0 0 7px 0" onclick="javascript:funOpenMailDiv(<%#Eval("SlNo")%>,'Approved',<%#Eval("id")%>);" />
                                                                                                                                    </span>
                                                                                                                                    <br class="clear" />
                                                                                                                                    <span id="spReject" runat="server">
                                                                                                                                        <img id="btnReject" src="images/reject_btn.gif" alt="Reject" title="Reject" onclick="javascript:funOpenMailDiv(<%#Eval("SlNo")%>,'Reject',<%#Eval("id")%>);"
                                                                                                                                            style="padding: 0 0 7px 0" />
                                                                                                                                    </span>
                                                                                                                                    <br class="clear" />
                                                                                                                                    <span id="spSendMail" runat="server">
                                                                                                                                        <img id="btnSendMail" src="images/sendMail_btn.gif" alt="SendMail" title="SendMail"
                                                                                                                                            onclick="javascript:funOpenMailDiv(<%#Eval("SlNo")%>,'SendMail',<%#Eval("id")%>);" />
                                                                                                                                    </span>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td colspan="5" align="center" valign="top" class="tdPadd">
                                                                                                                                    <div id="dvMail_<%#Eval("SlNo")%>" style="display: none">
                                                                                                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableBorder3">
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="2" align="right" valign="middle" class="closeBtn">
                                                                                                                                                    <a href="javascript:CloseMailDiv(<%#Eval("SlNo")%>);">Close</a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td width="30%" align="right" valign="top" class="formTxt">
                                                                                                                                                    From:
                                                                                                                                                </td>
                                                                                                                                                <td align="left" valign="middle">
                                                                                                                                                    <span id="spFrom<%#Eval("SlNo")%>"></span>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="right" valign="top" class="formTxt">
                                                                                                                                                    Template:
                                                                                                                                                </td>
                                                                                                                                                <td align="left" valign="middle">
                                                                                                                                                    <%-- <input type="text" name="textfield2" class="formTxtField" />--%>
                                                                                                                                                    <span id="spddl<%#Eval("SlNo")%>"></span><span id="dvAjaxPic_ddl<%#Eval("SlNo")%>"
                                                                                                                                                        class="loadImage1" style="display: none">
                                                                                                                                                        <img src="http://www.reliablelinkexchange.com/images/loading.gif" />
                                                                                                                                                    </span>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="right" valign="top" class="formTxt">
                                                                                                                                                    To:
                                                                                                                                                </td>
                                                                                                                                                <td align="left" valign="top">
                                                                                                                                                    <input id="txtTo<%#Eval("SlNo")%>" type="text" name="textfield3" class="formTxtField" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="right" valign="top" class="formTxt">
                                                                                                                                                    Subject:
                                                                                                                                                </td>
                                                                                                                                                <td align="left" valign="top">
                                                                                                                                                    <input id="txtTemplateSubject<%#Eval("SlNo")%>" type="text" name="textfield4" class="formTxtField" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="right" colspan="2" valign="top" class="formTxt">
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="right" valign="top" class="formTxt">
                                                                                                                                                    Body:
                                                                                                                                                </td>
                                                                                                                                                <td align="left" valign="top">
                                                                                                                                                    <textarea id="txtTemplateCode<%#Eval("SlNo")%>" name="textarea" class="textField1"
                                                                                                                                                        style="width: 403px; height: 450px;"></textarea>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="right" valign="top">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td align="left" valign="top" class="formPadding1">
                                                                                                                                                    <input type="button" id="btnMailSend" onclick="Insert_MailSend(<%#Eval("SlNo")%>,'Insert_MailSend',<%#Eval("id")%>);"
                                                                                                                                                        value="Send" class="submitBtn2 btnmargin1" />
                                                                                                                                                    <input type="button" onclick="CloseMailDiv(<%#Eval("SlNo")%>);" id="btnDiscard<%#Eval("SlNo")%>"
                                                                                                                                                        value="Discard" class="finalBtn" />
                                                                                                                                                    <span id="spUpdateStatus<%#Eval("SlNo")%>">
                                                                                                                                                        <input type="button" id="btnUpdate_withoutMail" onclick="Insert_MailSend(<%#Eval("SlNo")%>,'Update_withoutMail',<%#Eval("id")%>);"
                                                                                                                                                            value="Update Status Without Sending Mail" class="updateBtn" />
                                                                                                                                                    </span>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </div>
                                                                                                                                    <span id="dvAjaxPic<%#Eval("SlNo")%>" class="loadImage1" style="display: none">
                                                                                                                                        <img src="images/loading.gif" />
                                                                                                                                    </span>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td height="5" colspan="5" align="center" valign="top" class="bottomBorder">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td height="5" colspan="5" align="center" valign="top">
                                                                                                                        <span id="spCloseRow<%#Eval("SlNo")%>"></span>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="top" class="">
                                                                                                        <span class="addnameLink" id="spCount<%#Eval("SlNo")%>">
                                                                                                            <%#get_MailCount(Convert.ToInt32(Eval("Id")), Convert.ToString(Eval("SlNo")))%>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <div id="dvMailContent<%#Eval("SlNo")%>" style="display: none">
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td align="left" valign="top" class="catnameHead">
                                                                                                        <span id="spBack<%#Eval("SlNo")%>"></span>&nbsp;<span id="spSubPageNameM<%#Eval("SlNo")%>">
                                                                                                        </span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="top" class="tableBorder">
                                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td width="7%" align="center" valign="top" class="tableBorder1 tableBorder2">
                                                                                                                    S. No
                                                                                                                </td>
                                                                                                                <td width="20%" align="center" valign="top" class="tableBorder1">
                                                                                                                    Reciprocal URL
                                                                                                                </td>
                                                                                                                <td align="center" valign="top" class="tableBorder1">
                                                                                                                    Ad Given By Them
                                                                                                                </td>
                                                                                                                <td width="20%" align="center" valign="top" class="tableBorder1">
                                                                                                                    OUR AD
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td height="1" colspan="5" align="center" valign="top" class="topBorder">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="center" valign="top" class="rightBorder sNo">
                                                                                                                    1.
                                                                                                                </td>
                                                                                                                <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                    <span id="spReciprocalM<%#Eval("SlNo")%>"></span>
                                                                                                                </td>
                                                                                                                <td align="center" valign="top" class="rightBorder addnameLink">
                                                                                                                    <span id="spouraddM<%#Eval("SlNo")%>"></span>
                                                                                                                </td>
                                                                                                                <td align="center" valign="top" class="rightBorder addnameLink1">
                                                                                                                    <span id="spHTMLcodeM<%#Eval("SlNo")%>"></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="5" align="left" valign="top" class="mailPadd closeBtn">
                                                                                                                    Following mails have been send to
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="5" align="left" valign="top" class="btmtdPadd">
                                                                                                                        <%--<table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                        <tr>
                                                                                                                            <td align="left" valign="top">
                                                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                    <tr>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                            <div class="posRel">
                                                                                                                                                <div class="curveIns">
                                                                                                                                                    <span class="lftTop"></span><span class="rgtTop"></span>
                                                                                                                                                </div>
                                                                                                                                                <div class="mailBordr">
                                                                                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                        <tr>
                                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td width="25%" align="left" valign="top" class="sendShortTxt">
                                                                                                                                                                            Aug 24 (5 days ago)</td>
                                                                                                                                                                        <td align="left" valign="top" class="redBold">
                                                                                                                                                                            <span>Template Name</span></td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                                <div class="posRel">
                                                                                                                                                                    <div class="curveIns">
                                                                                                                                                                        <span class="lftTop"></span><span class="rgtTop"></span>
                                                                                                                                                                    </div>
                                                                                                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td width="25%" align="left" valign="top" class="sendShortTxt">
                                                                                                                                                                                Aug 24 (5 days ago)</td>
                                                                                                                                                                            <td align="left" valign="top" class="redBold">
                                                                                                                                                                                <span>Template Name</span></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </div>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td align="left" valign="top">
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </table>
                                                                                                                                                </div>
                                                                                                                                                <div class="botmBlueBg">
                                                                                                                                                    <span class="botmLft"></span><span class="botmRgt"></span>
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            <td width="15%" align="left" valign="top">
                                                                                                                                <p class="sendDate">
                                                                                                                                    <span><a href="#" title="Expand All">Expand All</a></span></p>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>--%>
                                                                                                                        <div class="mailBordr" id="dvMailSendM<%#Eval("SlNo")%>">
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                    <td width="15%" align="left" valign="top">
                                                                                                                        <p class="sendDate">
                                                                                                                            <span><a id="dvExpand<%#Eval("SlNo")%>" href="javascript:funExpand(<%#Eval("SlNo")%>);"
                                                                                                                                title="Expand All">Expand All</a></span></p>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <td height="5" colspan="5" align="center" valign="top" class="bottomBorder">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Label ID="lblPageSubPageWise" runat="server" Text=""></asp:Label></table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="4" align="left" valign="top" class="rightBg">
                                        <input type="hidden" id="hdButtontype" runat="server" />
                                        <input type="hidden" id="hdSiteId" runat="server" />
                                        <input type="hidden" id="hdSubPageId" runat="server" />
                                        <input type="hidden" id="hdPageNo" value="1" runat="server" />
                                        <input type="hidden" id="hdStatus" runat="server" />
                                        <input type="hidden" id="hdPagecount" runat="server" />
                                        <input type="hidden" id="hdFrom" runat="server" />
                                        <input type="hidden" id="hdTo" runat="server" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="41" align="left" valign="top" class="shadowBGtop">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%-- <uc2:ucfooter id="UcFooter1" runat="server" />--%>
    </table>
    </form>

    <script type="text/javascript">
        function funExpand(SlNo) {
            //    alert(document.getElementById("hdTot").value);

            //    if(document.getElementById("hdTot").value!=1)
            //    {

            if (document.getElementById("dvExpand" + SlNo).innerHTML == "Expand All") {
                document.getElementById("dvExpand" + SlNo).innerHTML = "Colapse All";
                for (var i = 1; i <= parseInt(document.getElementById("hdTot").value - 1); i++) {
                    document.getElementById("dvSendM" + i).style.display = "block";
                }
            }
            else {
                document.getElementById("dvExpand" + SlNo).innerHTML = "Expand All";
                for (var i = 1; i <= parseInt(document.getElementById("hdTot").value - 1); i++) {
                    document.getElementById("dvSendM" + i).style.display = "none";
                }
            }
            //    }
        }
        //function funOpenMailContentDiv(LinkId,SlNo)
        //{
        //    var SiteId=document.getElementById("hdSiteId").value;
        //    var SubPageId=document.getElementById("hdSubPageId").value;
        //    var Status=document.getElementById("hdStatus").value;
        //     var http5;
        //    http5=createRequestObject();
        //   if(SiteId!="")
        //   {
        //      http5.open("GET","New_AjaxPage.aspx?mode=MailContent&LinkId=" + LinkId +"&SiteId="+SiteId+"&Status="+Status+"&r="+Math.random()+"&SlNo="+SlNo); 
        //   } 
        //   else
        //   {
        //      http5.open("GET","New_AjaxPage.aspx?mode=MailContent&LinkId=" + LinkId +"&SubPageId="+SubPageId+"&Status="+Status+"&r="+Math.random()+"&SlNo="+SlNo); 
        //   }   
        //            http5.onreadystatechange=function()
        //            {
        //                if (http5.readyState == 4) 
        //                {       
        //                    var response = http5.responseText; 
        //                    var update =new Array();
        //                    if(response.indexOf('~')>-1)
        //                    {            
        //                         update=response.split('~');
        //                         alert(update[0].toString());
        //                         if(update[0].toString()=="1")
        //                         {
        //                             alert(document.getElementById("hdTot").value);
        //                             alert(document.getElementById("dvExpand"+SlNo));
        //                             if(document.getElementById("hdTot").value=="1")
        //                             {
        //                                 alert(document.getElementById("hdTot").value);
        //                                document.getElementById("dvExpand"+SlNo).style.display="none";
        //                             
        //                             }
        //                           document.getElementById("dvMailContent"+SlNo).style.display="block";
        //                           document.getElementById("dvMailView"+SlNo).style.display="none";
        //                           document.getElementById("lblSiteName").innerHTML="";
        //                            var to="";
        //                           if(document.getElementById("hdFrom").value!=null && document.getElementById("hdFrom").value!=null )
        //                           {
        //                              if(parseInt(document.getElementById("hdTo").value) > parseInt(document.getElementById("hdPagecount").value))
        //                              {
        //                                to=document.getElementById("hdPagecount").value;
        //                              }
        //                             else
        //                             {
        //                                to=document.getElementById("hdTo").value;

        //                             }
        //                          }   
        //                         for(var i=parseInt(document.getElementById("hdFrom").value);i<=parseInt(to);i++)
        //                         {
        //                           document.getElementById("dvMailView"+i).style.display="none";
        //                         }
        //                          if(SiteId!="")
        //                          {
        //                             document.getElementById("spBack"+SlNo).innerHTML="<a href=\"LinkDetails-View.aspx?Status="+Status+"&SiteId="+SiteId+">Back</a>";
        //                             document.getElementById("lblPage").innerHTML="";
        //                          }
        //                          else
        //                          {
        //                             document.getElementById("spBack"+SlNo).innerHTML="<a href=\"LinkDetails-View.aspx?Status="+Status+"&SubPageId="+SubPageId+">Back</a>";
        //                             document.getElementById("lblPageSubPageWise").innerHTML="";
        //                          }
        //                         document.getElementById("spSubPageNameM"+SlNo).innerHTML=update[1].toString();
        //                         document.getElementById("spReciprocalM"+SlNo).innerHTML=update[2].toString();
        //                         document.getElementById("spouraddM"+SlNo).innerHTML=update[3].toString();
        //                         document.getElementById("spHTMLcodeM"+SlNo).innerHTML=update[4].toString();
        //                         document.getElementById("dvMailSendM"+SlNo).innerHTML="";
        //                         document.getElementById("dvMailSendM"+SlNo).innerHTML=update[5].toString();
        //                         document.getElementById("dvSendM"+document.getElementById("hdTot").value).style.display="block";
        //                        
        //                      }
        //                   }
        //                   else
        //                   {
        //                        document.getElementById("dvMailContent"+SlNo).innerHTML="There is some problem";
        //                   }            

        //                      delete http5;
        //                     http5 = null;
        //               }
        //               else
        //               {
        //                 // document.getElementById("dvMailContent"+SlNo).innerHTML="<img  src=\"http://www.reliablelinkexchange.com/images/loading.gif\" alt=\"Wait...\" />"

        //               } 
        //           } 
        //           http5.send(null);  
        //}
    </script>

</body>
</html>
