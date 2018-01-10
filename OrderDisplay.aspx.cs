using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class OrderDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dvShowData.InnerHtml = ShowData();
        }
    }


    public string ShowData()
    {

        StringBuilder sbData = new StringBuilder();

        sbData.Append("<form id=\"frmPaypal\" method=\"post\" action=\"https://www.paypal.com/cgi-bin/webscr\">");
        sbData.Append("<div class=\"payment\">");
        sbData.Append("<h4>");
        sbData.Append("Thank you for placing your order with us.");
        sbData.Append("</h4>");
        sbData.Append("<p class=\"orderTxt\">");
        sbData.Append("The order is not yet complete. Please click on the 'Make Payment' button below to");
        sbData.Append("make the payment for the order. The next page will take you to the secured web page");
        sbData.Append("of our Payment Gateway - PAYPAL / Your Paypal account will show a charge as PAYPAL");
        sbData.Append("*GIFTSTOINDI. Flowerstoindia24x7.com is a part of GiftsToIndia24x7.com network of");
        sbData.Append("website. Your credit card / paypal account / Bank account will show a charge in");
        sbData.Append("the name of GiftsToIndia24x7.com.</p>");
        sbData.Append(" <br class=\"clear\">");
        sbData.Append(" <!-- Section to print the hidden fields of the particular payment gateway Starts -->");
        sbData.Append("<input id=\"invoice\" name=\"invoice\" value=\"GTI53675/2009\" type=\"hidden\"><input id=\"business\"");
        sbData.Append("name=\"business\" value=\"sales@giftstoindia24x7.com\" type=\"hidden\"><input id=\"currency_code\"");
        sbData.Append("name=\"currency_code\" value=\"USD\" type=\"hidden\"><input id=\"return\" name=\"return\" value=\"http://www.giftstoindia24x7.com/24x7_Success.aspx?sbillno=GTI53675/2009&amp;siteId=72\"");
        sbData.Append("type=\"hidden\">");
        sbData.Append("<input id=\"cancel_return\" name=\"cancel_return\" value=\"http://www.giftstoindia24x7.com/c_form_rakhi.aspx?sbillno=GTI53675/2009&amp;siteId=72\"");
        sbData.Append("type=\"hidden\">");
        sbData.Append("<input id=\"no_shipping\" name=\"no_shipping\" value=\"1\" type=\"hidden\"><input id=\"first_name\"");
        sbData.Append("name=\"first_name\" value=\"bill_test\" type=\"hidden\"><input id=\"last_name\" name=\"last_name\"");
        sbData.Append("value=\"bill_test\" type=\"hidden\"><input id=\"address1\" name=\"address1\" value=\"bill_test\"");
        sbData.Append("type=\"hidden\"><input id=\"address2\" name=\"address2\" value=\"bill_test\" type=\"hidden\"><input");
        sbData.Append("id=\"email\" name=\"email\" value=\"bill_test@yahoo.com\" type=\"hidden\"><input id=\"city\"");
        sbData.Append("name=\"city\" value=\"kol\" type=\"hidden\"><input id=\"state\" name=\"state\" value=\"West Bengal\"");
        sbData.Append("type=\"hidden\"><input id=\"country\" name=\"country\" value=\"IN\" type=\"hidden\"><input");
        sbData.Append("id=\"zip\" name=\"zip\" value=\"1111111\" type=\"hidden\"><input id=\"night_phone_b\" name=\"night_phone_b\"");
        sbData.Append("value=\"111111111\" type=\"hidden\"><input id=\"day_phone_b\" name=\"day_phone_b\" value=\"111111111\"");
        sbData.Append("type=\"hidden\">");
        sbData.Append("<input id=\"merchant_return_link\" name=\"merchant_return_link\" value=\"GTI53675/2009\"");
        sbData.Append(" type=\"hidden\"><input id=\"upload\" name=\"upload\" value=\"1\" type=\"hidden\"><input id=\"cmd\"");
        sbData.Append(" name=\"cmd\" value=\"_cart\" type=\"hidden\">");
        sbData.Append(" <!-- Section to print the hidden fields of the particular payment gateway Ends -->");
        sbData.Append(" <input class=\"paymentBtn\" title=\"Make Payment\" value=\"\" type=\"submit\"><br class=\"clear\">");
        sbData.Append(" <div class=\"billDetail\">");
        sbData.Append("  <dl class=\"leftTxt\">");
        sbData.Append("<dt>Billing Details :</dt><dd><p>");
        sbData.Append("   bill_test bill_test</p>");
        sbData.Append("<p>");
        sbData.Append(" bill_test</p>");
        sbData.Append("<p>");
        sbData.Append("West Bengal</p>");
        sbData.Append("<p>");
        sbData.Append(" India</p>");
        sbData.Append(" <p>");
        sbData.Append("     111111</p>");
        sbData.Append("<p>");
        sbData.Append("bill_test@yahoo.com</p>");
        sbData.Append("</dd>");
        sbData.Append("</dl>");
        sbData.Append(" <ul>");
        sbData.Append("<li><span>Order Id :</span>GTI53675/2009</li><li><span>Order Date :</span>23 Aug 2009</li><li>");
        sbData.Append(" <span>Total Value :</span>Rs.999.00</li><li><span>Payment Method :</span>PayPal</li><li>");
        sbData.Append("<span>Delivery Date :</span>24 Aug 2009</li></ul>");
        sbData.Append(" <br class=\"clear\">");
        sbData.Append(" <br class=\"clear\">");
        sbData.Append(" </div>");
        sbData.Append("<br class=\"clear\">");
        sbData.Append(" <ul class=\"topName\">");
        sbData.Append(" <li class=\"width1\">&nbsp;</li><li class=\"width2\">Sl No </li>");
        sbData.Append(" <li class=\"width3\">Item</li><li class=\"width2\">Qty</li><li class=\"width4\">Price</li><li");
        sbData.Append("  class=\"width4\">Total Price</li></ul>");
        sbData.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
        sbData.Append("<tbody>");
        sbData.Append("  <tr>");
        sbData.Append(" <td align=\"left\" valign=\"top\" width=\"189\">");
        sbData.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
        sbData.Append("<tbody>");
        sbData.Append(" <tr>");
        sbData.Append("<td class=\"shipDetail\" align=\"left\" valign=\"top\">");
        sbData.Append("<p>");
        sbData.Append("<span>ship_test ship_test</span><br>");
        sbData.Append("  ship_test<br>");
        sbData.Append("1111111111<br>");
        sbData.Append("111111 India<br>");
        sbData.Append("1111111111 Kolkata</p>");
        sbData.Append("</td>");
        sbData.Append("  </tr>");
        sbData.Append("  <tr>");
        sbData.Append(" <td class=\"msgDetail\" align=\"left\" valign=\"top\">");
        sbData.Append("<p class=\"msgDetailTxt\">");
        sbData.Append("To view the message and special instructions for the order, mouseover the below");
        sbData.Append(" button.</p>");
        sbData.Append(" <span id=\"snmsg\" class=\"msg\" title=\"header=[&lt;img src='images/info.gif' style='vertical-align:middle'&gt;&nbsp;&nbsp;Message] body=[None]\">");
        sbData.Append("Message</span><br>");
        sbData.Append("<span id=\"snins\" class=\"ins\" title=\"header=[&lt;img src='images/info.gif' style='vertical-align:middle'&gt;&nbsp;&nbsp;Special Instructions] body=[None]\">");
        sbData.Append("Special Instructions</span></td>");
        sbData.Append(" </tr>");
        sbData.Append(" <tr>");
        sbData.Append("<td align=\"left\" valign=\"top\">");
        sbData.Append("&nbsp;</td>");
        sbData.Append(" </tr>");
        sbData.Append("</tbody>");
        sbData.Append("</table>");
        sbData.Append("</td>");
        sbData.Append("<td align=\"left\" valign=\"top\" width=\"774\">");
        sbData.Append(" <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
        sbData.Append(" <tbody>");
        sbData.Append("  <tr>");
        sbData.Append("    <td align=\"left\" valign=\"top\">");
        sbData.Append(" <ul class=\"prodList\">");




        sbData.Append(" <li class=\"width2\">1</li><li class=\"width3\">");
        sbData.Append(" <img src=\"images/GTI0076.jpg\" alt=\"Outstanding Basket\" title=\"Outstanding Basket\"");
        sbData.Append("          class=\"itemImg\">Outstanding Basket<input name=\"item_name_1\" value=\"Outstanding Basket\"");
        sbData.Append(" type=\"hidden\"></li><li class=\"width2\">1<input name=\"quantity_1\" value=\"1\" type=\"hidden\"></li><li");
        sbData.Append("  class=\"width4\">Rs.999.00/$.22.20 </li>");
        sbData.Append("  <li class=\"width9\">Rs.999.00/$.22.20<input name=\"amount_1\" value=\"22.20\" type=\"hidden\"></li></ul>");
        sbData.Append("<ul class=\"totalList\">");
        sbData.Append("<li class=\"width3\"><strong>Total Qty</strong></li><li class=\"width2\">1</li><li class=\"width7\">");
        sbData.Append(" Rs. 999.00/$.22.20</li></ul>");
        sbData.Append("<ul class=\"totalList\">");
        sbData.Append("<li class=\"width8\"><strong>Total</strong></li><li class=\"width9\">Rs. 999.00/$.22.20</li></ul>");
        sbData.Append(" <ul class=\"totalList\">");
        sbData.Append(" <li class=\"width8\"><strong>Discount</strong></li><li class=\"width9\">Rs. 0/$.0.00</li></ul>");
        sbData.Append("<ul class=\"totalList\">");
        sbData.Append("<li class=\"width8\"><strong>Payable</strong></li><li class=\"width9\">Rs.999.00/$.22.20</li></ul>");




        sbData.Append("</td>");
        sbData.Append("</tr>");
        sbData.Append("</tbody>");
        sbData.Append("</table>");
        sbData.Append("</td>");
        sbData.Append("</tr>");
        sbData.Append("</tbody>");
        sbData.Append(" </table>");
        sbData.Append("  <br class=\"clear\">");
        sbData.Append("</div>");
        sbData.Append(" <br class=\"clear\">");
        sbData.Append(" </form>");


        return sbData.ToString();

    }
}
