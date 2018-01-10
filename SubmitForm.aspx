<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubmitForm.aspx.cs" Inherits="SubmitForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <script language="javaScript" type="text/javascript" src="js/calendarDateInput.js"></script>
    <script language="javaScript" type="text/javascript" src="js/jsFormValidation.js"></script>
    <script language="javaScript" type="text/javascript" src="js/jsCommonFunction.js"></script>
    <script language="javaScript" type="text/javascript" src="js/jsAjaxSubmitForm.js"></script>

</head>
<body>
    <form id="formcheck" runat="server">
    <div>
    <table width="1004" align="center" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
                <td width="100%" colspan="5">
                    
                    <table width="1004" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <div align="right">
                                    <img src="Pictures/GiftstoIndia24x7-Logo-Oform.gif" width="463" height="136" alt="Giftstoindia24x7.com Submit Form" /></div>
                            </td>
                            <td width="540" height="67" valign="bottom" background="Pictures/GiftstoIndia24x7-Logo-Payment-up.gif">
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" background="Pictures/GiftstoIndia24x7-Logo-Payment-Customer-info.gif"
                                style="height: 69px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5" valign="top" align="center" style="height: 793px">
                    <div id="dvMain" style="width: 100%">
                        <!-- The main table section Starts -->
                        <table width="100%" border="0" align="center" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
                            <tr>
                                <td width="1%" class="style1">
                                </td>
                                <td width="46%" class="style4" align="center" valign="top">
                                    <div align="justify">
                                        <span class="style8">Billing Details:</span><br />
                                        This address should match your Credit Card billing address.
                                    </div>
                                </td>
                                <td width="6%" class="style1" align="center">
                                    <!-- Ajax rotator -->
                                    <div id="dvAjaxPic" style="display: none; top: 50%; left: 5%; position: relative;
                                        width: 100%;">
                                        <table id="tabAjax" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td valign="middle" align="center" width="30%">
                                                    <img src="Pictures/loading.gif" height="18px" width="18px" />
                                                </td>
                                                <td width="2%" valign="bottom">
                                                </td>
                                                <td valign="bottom" align="center" width="68%" class="style1">
                                                    &nbsp;Wait</td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!-- Ajax rotator -->
                                </td>
                                <td width="1%" class="style1">
                                </td>
                                <td width="46%" class="style2" align="left" valign="top">
                                    <span class="style8">Shipping Details:</span><br />
                                    Details of person to whom the gifts are being sent.
                                </td>
                            </tr>
                            <tr class="clear10">
                                <td colspan="5" style="height: 10px" class="small_red">
                                    <span id="lblError" class="small_red" style="display:inline-block;width:100%;"></span>
                                </td>
                            </tr>
                            <tr>
                                <td width="1%" class="style1">
                                </td>
                                <td width="46%" class="style1" align="center" valign="top">
                                    <div id="dvShipping" style="width: 100%;">
                                        <!-- Billing Section Starts -->
                                        <table id="tabShipping" width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    First Name:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billFName" runat="server" type="text" class="big_red" id="billFName" size="36" maxlength="25"
                                                        tabindex="1" />*
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    Last Name:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billLName" runat="server" type="text" class="big_red" id="billLName" size="36" maxlength="25"
                                                        tabindex="2" />*
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    Address1:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billAddress1" runat="server" type="text" class="big_red" id="billAddress1" size="36"
                                                        tabindex="3" maxlength="100" />*
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    Address2:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billAddress2" runat="server" type="text" class="big_red" id="billAddress2" size="36"
                                                        tabindex="4" maxlength="100" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    Postal Code:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billZip" runat="server" type="text" class="big_red" id="billZip" size="36" maxlength="20"
                                                        tabindex="5" />*
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    Country:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style1" align="left" valign="middle">
                                                    <div id="dvBillingCountry" runat="server" style="width: 100%; height: 18px;">
                                                    
<%--                                                    <select name="billCountry" tabindex="6" class="big_red" id="billCountry" style="width: 235px;z-index:1001;" onChange="javascript:loadBillingState(this);"><option value="0" >Select Country</option><option value=5>Australia</option><option value=3>Canada</option><option value=2>India</option><option value=7>Newzealand</option><option value=9999>Other Country</option><option value=6>Singapore</option><option value=8>UAE</option><option value=4>UK</option><option value=1>USA</option><option value="9999">Other Country</option></select>*</div>
--%>                                                    

                                                        <asp:DropDownList ID="billCountry"  onChange="javascript:loadBillingState(this);"  runat="server">
                                                        </asp:DropDownList>

<input type="hidden" id="hdnBillCountryName" name="hdnBillCountryName" value="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    State/Province:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <div id="dvBillingState" style="width: 100%; height: 18px;">
                                                        
                                                    </div>
                                                    <input type="hidden" id="hdnBillStateName" runat="server" name="hdnBillStateName" value="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle" style="height: 29px">
                                                    City:</td>
                                                <td width="1%" class="style4" align="left" valign="middle" style="height: 29px">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle" style="height: 29px">
                                                    <input name="billCity" runat="server" type="text" class="big_red" id="billCity" size="36" tabindex="8" maxlength="30" />*
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle" style="height: 24px">
                                                    Telephone:</td>
                                                <td width="1%" class="style4" align="left" valign="middle" style="height: 24px">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle" style="height: 24px">
                                                    <input name="billPhNo" runat="server" type="text" class="big_red" id="billPhNo" size="36" tabindex="9"
                                                        maxlength="20" onkeypress="javascript:return IsNumeric(event);" />*
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    Mobile:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billMobNo" runat="server" type="text" class="big_red" id="billMobNo" size="36" tabindex="10"
                                                        maxlength="20" onkeypress="javascript:return IsNumeric(event);" />
                                                </td>
                                            </tr>
                                            <tr class="clear10">
                                                <td colspan="3" class="clear10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="style1">
                                                    <div align="justify">
                                                        <span class="style8">E-mail Address:</span><br />
                                                        <span class="style4">We will send order receipt, delivery confirmation and other order
                                                            related notifications to the below email address; please make sure you enter it
                                                            correctly. This should be the email address of the person placing the order, not
                                                            the recipient. </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    E-mail ID:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billEmail" runat="server" type="text" id="billEmail" class="big_red" size="36" tabindex="11" maxlength="100" />*
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                    Re-type Email:</td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <input name="billEmail2" runat="server" type="text" class="big_red" id="billEmail2" size="36" tabindex="12" />*
                                                    
                                                </td>
                                            </tr>
                                            <tr class="clear10">
                                                <td colspan="3" class="clear10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="style1" align="left">
                                                    <span class="style8">Notes / Instructions</span><br />
                                                    <span class="style4">Please mention below any special instructions with relation to
                                                        this order that you want us to follow. </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="1%" class="style4" align="left" valign="middle">
                                                </td>
                                                <td width="69%" class="style4" align="left" valign="middle">
                                                    <textarea name="billInstructions" runat="server" cols="36" rows="6" class="big_red" id="billInstructions"
                                                        tabindex="13"></textarea>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- Billing Section Ends -->
                                    </div>
                                </td>
                                <td width="6%" align="center" rowspan="2" valign="middle">
                                    <table width="6" height="425" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td background="Pictures/v-hr.jpg">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="1%" class="style1">
                                </td>
                                <td width="46%" class="style1" align="center" valign="top">
                                    <!-- Shipping Section Starts -->
                                    <table id="tabBilling" width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Date of Delivery:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="big_red" align="left" valign="middle">
                                                
                                                <div id="dvShipCal" style="z-index: 1001;">
                                                    

                                                    <script type="text/javascript">DateInput('shipDate', true, 'MM/DD/YYYY')</script>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                First Name:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipFName" runat="server" type="text" class="big_red" id="shipFName" size="36" tabindex="15"
                                                    maxlength="25" />*
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Last Name:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipLName" runat="server" type="text" class="big_red" id="shipLName" size="36" tabindex="16"
                                                    maxlength="25" />*
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Address1:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipAddress1" runat="server" type="text" class="big_red" id="shipAddress1" size="36"
                                                    tabindex="17" maxlength="100" />*
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Address2:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipAddress2" runat="server" type="text" class="big_red" id="shipAddress2" size="36"
                                                    tabindex="18" maxlength="100" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Postal Code:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipZip" runat="server" type="text" class="big_red" id="shipZip" size="36" tabindex="19"
                                                    maxlength="20" />*
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Country:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipCountry" runat="server" type="text" class="big_red" id="shipCountry" value="India"
                                                    size="36" tabindex="20" readonly="readonly" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                State:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <div id="dvShippingState" runat="server" style="width: 100%; height: 19px">
                                              <%--  <select name="shipState" runat="server" tabindex="21" class="big_red" id="shipState" style="width: 235px;z-index:1001;" onChange="javascript:loadShippingCity(this);">
                                                <option value="0" >Select State</option><option value="9999">Not Known</option><option value=71>Andhra Pradesh</option><option value=73>Assam</option><option value=74>Bihar</option><option value=76>Chandigarh</option><option value=75>Chhattisgarh</option><option value=77>Daman and Diu</option><option value=79>Delhi</option><option value=80>Goa</option><option value=81>Gujarat</option><option value=82>Haryana</option><option value=83>Himachal Pradesh</option><option value=85>Jammu and Kashmir</option><option value=84>Jharkhand</option><option value=86>Karnataka</option><option value=87>Kerala</option><option value=92>Madhya Pradesh</option><option value=90>Maharashtra</option><option value=91>Manipur</option><option value=89>Meghalaya</option><option value=113>New Delhi</option><option value=95>Orissa</option><option value=97>Pondicherry</option><option value=96>Punjab</option><option value=98>Rajasthan</option><option value=99>Sikkim</option><option value=100>Tamil Nadu</option><option value=102>Uttar Pradesh</option><option value=103>Uttaranchal</option><option value=104>West Bengal</option>
                                                </select>--%>
                                                    <asp:DropDownList ID="shipState" onChange="javascript:loadShippingCity(this);" runat="server">
                                                    </asp:DropDownList>
                                                
                                                
                                                *</div>
                                                <input type="hidden" id="hdnShipStateName" name="hdnShipStateName" value="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                City:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <div id="divShippingCity" style="width: 100%; height: 18px;">
                                                </div>
                                                <div id="dvOtherCity" style="width: 100%; height: 18px; display: none;">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                           <%-- <td style="width: 25%" align="left" valign="middle" class="style4">
                                                                City Name</td>--%>
                                                            <td style="width: 1%" align="left" valign="middle" class="style4">
                                                                &nbsp;</td>
                                                            <td style="width: 73%" align="left" valign="middle" class="style4">
                                                                <input name="shipOtherCity" runat="server" type="text" class="big_red" id="shipOtherCity" tabindex="23"
                                                                    maxlength="30" style="width: 158px" />*
                                                            </td>
                                                            <td style="width: 1%" align="left" valign="middle" class="style4">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <input type="hidden" id="hdnShipCityName" runat="server" name="hdnShipCityName" value="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Telephone:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipPhNo" runat="server" type="text" class="big_red" id="shipPhNo" size="36" tabindex="23"
                                                    maxlength="20" onkeypress="javascript:return IsNumeric(event);" />*
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Mobile:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <input name="shipMobNo" runat="server" type="text" class="big_red" id="shipMobNo" size="36" tabindex="24"
                                                    maxlength="20" onkeypress="javascript:return IsNumeric(event);" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle" style="height: 36px">
                                                E-mail ID:</td>
                                            <td width="1%" class="style4" align="left" valign="middle" style="height: 36px">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle" style="height: 36px">
                                                <input name="shipEmail" runat="server" type="text" class="big_red" id="shipEmail" size="36" tabindex="25"
                                                    maxlength="100" /></td>
                                        </tr>
                                        <tr>
                                            <td width="30%" class="style4" align="left" valign="middle">
                                                Message with the Gifts:</td>
                                            <td width="1%" class="style4" align="left" valign="middle">
                                            </td>
                                            <td width="69%" class="style4" align="left" valign="middle">
                                                <textarea name="shipMsg" runat="server" cols="36" rows="6" class="big_red" id="shipMsg" tabindex="26"></textarea>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- Shipping Section Ends -->
                                </td>
                            </tr>
                            <tr>
                                <td width="1%" class="style1">
                                </td>
                                <td width="46%" class="style1" align="center" valign="top">
                                </td>
                                <td width="1%" class="style1">
                                </td>
                                <td width="46%" class="style1" align="center" valign="top">
                                </td>
                            </tr>
                        </table>
                        <!-- The main table section Ends -->
                    </div>
                </td>
            </tr>
            <tr height="25px">
                <td width="16%" valign="top">
                </td>
                <td width="1%">
                    &nbsp;</td>
                <td valign="top" class="small_red" align="center">
                    <input id="chkTerms" type="checkbox" name="chkTerms" checked="checked" tabindex="27" />I have read and agree
                    to the <a href="http://www.giftstoindia24x7.com/terms.aspx" title="Terms & Conditions" target="_blank" >
                        Terms of Service</a>.
                </td>
                <td width="1%">
                    &nbsp;</td>
                <td width="16%" valign="top">
                </td>
            </tr>
            <tr>
                <td valign="top" width="16%">
                </td>
                <td width="1%">
                </td>
                <td valign="top" align="center">
                    <div id="dvBtn" style="width: 100%; height: 100%">
                        
<%--                        <input type="image" id="btnSubmit" tabindex="28" src="images/continue.gif" runat="server" style="border-width:0px;" />
--%>                        <asp:ImageButton ID="btnSubmit" ValidationGroup="valid"   ImageUrl="images/continue.gif" runat="server" OnClick="btnSubmit_Click"/>
                       <%-- <span id="cvFormValidator" style="color:Red;visibility:hidden;"></span>--%>
                        <asp:CustomValidator ID="cvFormValidator" ValidationGroup="valid" EnableClientScript="true"  runat="server" ClientValidationFunction="formValidate" ErrorMessage=""></asp:CustomValidator>
                    </div>
                </td>
                <td width="1%">
                </td>
                <td valign="top" width="16%">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
