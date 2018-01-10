<%@ Page Language="C#" AutoEventWireup="true" CodeFile="c_form.aspx.cs" Inherits="c_form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form runat="server" id="frmCancel">
        <div>
          
        </div>
        <div>
           
        </div>
        <table align="center" bgcolor="#ffffff" border="0" cellpadding="0" cellspacing="0"
            width="1004">
            <tbody>
                <tr>
                    <td colspan="3">
                        <table border="0" cellpadding="0" cellspacing="0" width="1004">
                            <tbody>
                                <tr>
                                    <td>
                                        <div align="right">
                                            <img src="c_form.aspx_files/GiftstoIndia24x7-Logo-Oform.gif" height="136" width="463"></div>
                                    </td>
                                    <td background="c_form.aspx_files/GiftstoIndia24x7-Logo-Of-02.gif" valign="bottom"
                                        width="540">
                                        <table border="0" cellpadding="0" cellspacing="0" width="434">
                                            <tbody>
                                                <tr>
                                                    <td style="height: 19px;" class="big_red">
                                                        <div align="center">
                                                            <a href="http://www./" class="style2">Home</a> | <a href="http://www.giftstoindia24x7.com/contactus.aspx"
                                                                class="style2">Customer service</a></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="1%">
                        &nbsp;</td>
                    <td valign="top" width="98%">
                        <!-- MAIN TABLE STARTS -->
                        <div id="dvDetails" class="big_red" align="center">
                            <table align="center" bgcolor="#ffffff" border="0" cellpadding="0" cellspacing="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td align="center" valign="top">
                                            <!-- MAIN TABLE STARTS -->
                                            <table align="center" bgcolor="#ffffff" border="0" cellpadding="0" cellspacing="0"
                                                width="100%">
                                                <tbody>
                                                    <tr class="style8">
                                                        <td class="bigText" style="height: 20px;" align="center" valign="middle">
                                                            <div align="center">
                                                                <img src="c_form.aspx_files/order-not-processed.jpg" alt="Transaction failed" align="middle"
                                                                    height="50" width="50">
                                                                Sorry!!! The Bank has declined your Transaction. The possible reasons could be one
                                                                or more of those listed below.
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr class="clear10">
                                                        <td class="clear10">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="big_red" align="right" valign="top" width="30%">
                                                                            <b>Possible Reasons :</b></td>
                                                                        <td align="left" valign="middle" width="1%">
                                                                            &nbsp;</td>
                                                                        <td rowspan="2" class="big_red" align="left" valign="middle" width="69%">
                                                                            a) Invalid Card Details.<br>
                                                                            b) Card has Expired.<br>
                                                                            c) Insufficient Funds.<br>
                                                                            d) VBV/3D Secure Authentication Unsuccessful.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" valign="middle" width="30%">
                                                                            &nbsp;</td>
                                                                        <td align="left" valign="middle" width="1%">
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr class="clear10">
                                                        <td class="clear10">
                                                            <span id="lblError" class="big_red" style="display: inline-block; width: 100%;">
                                                                <br>
                                                                <br>
                                                                <center>
                                                                    <b>There is some error on retrievation of your order number or selected bank.We are
                                                                        sorry for that!</b></center>
                                                                <br>
                                                                Please go to HomePage.<br>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                        
                                                            <asp:Button ID="btnTryagain" runat="server" Text="Try with other gateway" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" align="center" valign="top">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr class="clear10">
                                                        <td class="clear10">
                                                            <input name="hdnSiteId" id="hdnSiteId" type="hidden">
                                                            <input name="hdnPgId" id="hdnPgId" type="hidden">
                                                            <input name="hdnFlag" id="hdnFlag" type="hidden">
                                                            &nbsp;
                                                            <input name="hdnPgName" id="hdnPgName" type="hidden">
                                                        </td>
                                                    </tr>
                                                    <tr class="clear10">
                                                        <td class="clear10">
                                                            <input name="hdnPoId" id="hdnPoId" type="hidden">
                                                            <input name="hdnPoName" id="hdnPoName" type="hidden">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" align="center" valign="top">
                                                            &nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- MAIN TABLE ENDS -->
                    </td>
                    <td width="1%">
                        &nbsp;</td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
