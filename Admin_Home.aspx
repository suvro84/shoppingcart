<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Home.aspx.cs" Inherits="Admin_Home" %>

<%--<%@ Register Src="ucFooter.ascx" TagName="ucFooter" TagPrefix="uc2" %>
--%><%--<%@ Register Src="ucHeader.ascx" TagName="ucHeader" TagPrefix="uc1" %>
--%>
<%--<%@ Register Src="UcLeftPannel.ascx" TagName="ucLeftPannel" TagPrefix="uc3" %>
--%><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>:: Admin Home ::</title>
    <link href="http://www.reliablelinkexchange.com/css/style.css" rel="stylesheet" type="text/css" />
    <%--    <script type="text/javascript" src="js/JSStrengthPW.js"></script>
--%>

    <script type="text/javascript" src="js/jquery.js"></script>

    <script type="text/javascript" src="js/ddaccordion.js"></script>

    <script type="text/javascript" src="js/ddaccordion1.js"></script>

    <script type="text/javascript" src="js/ajaxScript.js"></script>

    <script type="text/javascript" src="js/boxover.js"></script>

    <script type="text/javascript" language="javascript" src="js/JScript.js"></script>

    <link href="css/style.css" rel="stylesheet" type="text/css" />
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
        <%--<uc1:ucHeader ID="UcHeader1" runat="server" />--%>
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
                                                    <%-- <uc3:ucLeftPannel ID="UcLeftPannel" runat="server" />--%>
                                                    <!--Left Panel End -->
                                                </td>
                                                <td align="left" valign="top" class="leftBorder">
                                                    <asp:Repeater ID="rptLink" runat="server">
                                                        <HeaderTemplate>
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tableBorder">
                                                                <tr>
                                                                    <td width="10%" align="center" valign="top" class="tableBorder1 tableBorder2">
                                                                        S. No
                                                                    </td>
                                                                    <td align="center" valign="top" class="tableBorder1">
                                                                        Website Name
                                                                    </td>
                                                                    <td width="20%" align="center" valign="top" class="tableBorder1">
                                                                        Approved
                                                                    </td>
                                                                    <td width="20%" align="center" valign="top" class="tableBorder1">
                                                                        Wating for Approval
                                                                    </td>
                                                                    <td width="25%" align="center" valign="top" class="tableBorder1 tableBorder4">
                                                                        Rejected
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="1" colspan="5" align="center" valign="top" class="topBorder">
                                                                    </td>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center" valign="top" class="rightBorder">
                                                                    <%#Eval("SlNo")%>
                                                                </td>
                                                                <td align="center" valign="top" class="rightBorder siteName1">
                                                                    <%--  <a   href="WebSite-Details.aspx?SiteId=<%# Eval("id")%>" class="siteName" >
                                                                            <%#Eval("Name")%>
                                                                        </a>--%>
                                                                    <%#get_data(Convert.ToString(Eval("id")),Convert.ToString(Eval("Name")),Convert.ToString(Eval("Wait")),Convert.ToString(Eval("Approved")),Convert.ToString(Eval("Reject")))%>
                                                                </td>
                                                                <td align="center" valign="top" class="rightBorder approvedLink">
                                                                    <span id="spApproved" runat="server">
                                                                        <%#Eval("Wait")%>
                                                                    </span><span id="spApprovedView" runat="server">| <a href="LinkDetails-View.aspx?Status=2&SiteId=<%# Eval("id")%>"
                                                                        title="view">view</a> </span>
                                                                </td>
                                                                <td align="center" valign="top" class="rightBorder approvedLink">
                                                                    <span id="spWait" runat="server">
                                                                        <%#Eval("Approved")%>
                                                                    </span><span id="spWaitView" runat="server">| <a href="LinkDetails-View.aspx?Status=1&SiteId=<%# Eval("id")%>"
                                                                        title="view">view</a> </span>
                                                                </td>
                                                                <td align="center" valign="top" class="approvedLink">
                                                                    <span id="spReject" runat="server">
                                                                        <%#Eval("Reject")%>
                                                                    </span><span id="spRejectView" runat="server">| <a href="LinkDetails-View.aspx?Status=3&SiteId=<%# Eval("id")%>"
                                                                        title="view">view</a> </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="5" colspan="5" align="center" valign="top" class="bottomBorder">
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="4" align="left" valign="top" class="rightBg">
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
        <%-- <uc2:ucFooter ID="UcFooter1" runat="server" />--%>
    </table>
    </form>
</body>
</html>
