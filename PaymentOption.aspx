<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentOption.aspx.cs" Inherits="PaymentOption" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script language="javaScript" type="text/javascript" src="js/jsPO.js"></script>

</head>
<body>
    <form id="frmPo" runat="server">
        <div>
            <div id="dvPaymentOptList" style="width: 100%;">
                <!-- Payment Options genaration starts -->
                <table id="tabPOMain" class="tableborder" border="0" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td colspan="3" class="style8" align="center" valign="top" width="100%">
                                You can make the payment by selecting any of the following methods. We support all
                                major credit cards &amp; transfers through Paypal.</td>
                        </tr>
                        <tr>
                            <td colspan="3" height="15">
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <asp:Repeater ID="rptPo" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" valign="top" width="49%">
                                        <table id="tabPO0" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="style1" align="center" valign="middle" width="5%">
                                                        <%#Eval("Poid") %>
                                                    </td>
                                                    <td class="style1" align="right" valign="middle" width="5%">
                                                      <%--  <input name="radPayOpt" id="<%#Eval("Poid")%>" onclick="javascript:setPoId_PoName(<%#Eval("Poid")%>,'<%#Eval("POptionName")%>');"
                                                            value="<%#Eval("Poid")%>" class="input" type="radio" runat="server" />--%>
                                                            
                                                            <input name="radPayOpt" id="<%#Eval("Poid")%>" value="<%#Eval("Poid")%>" class="input" onclick="javascript:setPoPgIdNew('1', '2');"
                                                                                            type="radio" checked="CHECKED">
                                                            
                                                            
                                                            
                                                    </td>
                                                    <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                        <%-- <%#Eval("POptionName") %>--%>
                                                        <asp:Label ID="lblPOptionName" Text='<%#Eval("POptionName") %>' runat="server"></asp:Label>
                                                        <%--                                                      <asp:RadioButton ID="radPayOpt"     Text='<%#Eval("POptionName") %>'   runat="server" /> 
--%>
                                                    </td>
                                                    <td class="style1" align="center" valign="left" width="30%">
                                                        <img src="<%#Eval("Image")%>" alt="Visa" align="left">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--  <tr>
                            <td align="center" valign="top" width="49%">
                         
                                <table id="tabPO0" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="style1" align="center" valign="middle" width="5%">
                                                1.</td>
                                            <td class="style1" align="right" valign="middle" width="5%">
                                                <input name="radPayOpt" id="1" value="1" class="input" 
                                                    type="radio"></td>
                                            <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                Visa</td>
                                            <td class="style1" align="center" valign="left" width="30%">
                                                <img src="images/visa.jpg" alt="Visa" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="background-image: url(Pictures/hr-h2.jpg); background-repeat: repeat-y;"
                                align="center" valign="top" width="1%">
                            </td>
                            <td align="center" valign="top" width="49%">
                                <table id="tabPO1" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="style1" align="center" valign="middle" width="5%">
                                                2.</td>
                                            <td class="style1" align="right" valign="middle" width="5%">
                                                <input name="radPayOpt" id="2" value="2"  class="input" 
                                                    type="radio"></td>
                                            <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                Master Card</td>
                                            <td class="style1" align="center" valign="left" width="30%">
                                                <img src="images/master.jpg" alt="Master Card" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" width="49%">
                                <table id="tabPO2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="style1" align="center" valign="middle" width="5%">
                                                3.</td>
                                            <td class="style1" align="right" valign="middle" width="5%">
                                                <input name="radPayOpt" id="4" value="4"  class="input" 
                                                    type="radio"></td>
                                            <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                American Express or Discover</td>
                                            <td class="style1" align="center" valign="left" width="30%">
                                                <img src="images/American-Discover-Card.jpg" alt="American Express or Discover" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="background-image: url(Pictures/hr-h2.jpg); background-repeat: repeat-y;"
                                align="center" valign="top" width="1%">
                            </td>
                            <td align="center" valign="top" width="49%">
                                <table id="tabPO3" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="style1" align="center" valign="middle" width="5%">
                                                4.</td>
                                            <td class="style1" align="right" valign="middle" width="5%">
                                                <input name="radPayOpt" id="3" value="3"  class="input" 
                                                    type="radio"></td>
                                            <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                PayPal</td>
                                            <td class="style1" align="center" valign="left" width="30%">
                                                <img src="images/paypal_logo_new.gif" alt="PayPal" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" width="49%">
                                <table id="tabPO4" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="style1" align="center" valign="middle" width="5%">
                                                5.</td>
                                            <td class="style1" align="right" valign="middle" width="5%">
                                                <input name="radPayOpt" id="5" value="5"  class="input"
                                                    type="radio"></td>
                                            <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                Diners Club</td>
                                            <td class="style1" align="center" valign="left" width="30%">
                                                <img src="images/Diners.jpg" alt="Diners Club" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="background-image: url(Pictures/hr-h2.jpg); background-repeat: repeat-y;"
                                align="center" valign="top" width="1%">
                            </td>
                            <td align="center" valign="top" width="49%">
                                <table id="tabPO5" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="style1" align="center" valign="middle" width="5%">
                                                6.</td>
                                            <td class="style1" align="right" valign="middle" width="5%">
                                                <input name="radPayOpt" id="6" value="6"  class="input" 
                                                    type="radio"></td>
                                            <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                Citibank E-cards</td>
                                            <td class="style1" align="center" valign="left" width="30%">
                                                <img src="images/Citibank.jpg" alt="Citibank E-cards" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" width="49%">
                                <table id="tabPO6" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="style1" align="center" valign="middle" width="5%">
                                                7.</td>
                                            <td class="style1" align="right" valign="middle" width="5%">
                                                <input name="radPayOpt" id="7" value="7" class="input" 
                                                    type="radio"></td>
                                            <td class="style8" style="padding: 0pt 5px;" align="left" valign="middle" width="59%">
                                                Indian Bank Fund Transfer</td>
                                            <td class="style1" align="center" valign="left" width="30%">
                                                <img src="images/bank2.gif" alt="Indian Bank Fund Transfer" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>--%>
                        <!-- Payment Options genaration Ends -->
                        <tr>
                            <td colspan="3" height="15">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div id="dvBtn" style="width: 100%;" align="center">
            <%-- <input name="btnSubmit" id="btnSubmit" src="images/btn_proceed_checkout.gif" alt="Proceed to Checkout"
                onclick='javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("btnSubmit", "", true, "validpayment", "", false, false))'
                style="border-width: 0px;" type="image">--%>
            <input name="btnSubmit" id="btnSubmit" src="images/btn_proceed_checkout.gif" validationgroup="valid"
                runat="server" alt="Proceed to Checkout" style="border-width: 0px;" type="image"
                onserverclick="btnSubmit_ServerClick" />
            <asp:CustomValidator ID="cvPo" runat="server" EnableClientScript="true" ValidationGroup="valid"
                ClientValidationFunction="validate" ErrorMessage=""></asp:CustomValidator>
        </div>
        <input id="hdSbillno" type="hidden" runat="server"/>
        <input type="hidden" id="hdnPoId" runat="server" />
        <input name="hdnPgId" id="hdnPgId" runat="server" type="hidden" />
        <input type="hidden" id="hdnPoName" runat="server" />
    </form>
</body>
</html>
