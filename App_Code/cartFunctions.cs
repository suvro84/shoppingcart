using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for cartFunctions
/// </summary>
public class cartFunctions
{
    Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["Schema"].ToString());
    string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
    string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());
    string strsiteDomain = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
    string strSql = "";
    string strError = "";


    #region [member]
    private string _senddata = "";
    private DataTable _dt = null;
    private int? _userid = null;
    private int? _currencyid = null;
    private string _comboid = null;
    #endregion
    # region [prop]
    public string ComboID
    {
        get
        {
            return _comboid;
        }
        set
        {
            _comboid = value;
        }
    }
    public int? CurrencyID
    {
        get
        {
            return _currencyid;
        }
        set
        {
            _currencyid = value;
        }
    }
    public int? UserId
    {
        get
        {
            return _userid;
        }
        set
        {
            _userid = value;
        }
    }
    public string SendData
    {
        get
        {
            fnSendData(_dt);
            return _senddata;
        }

    }
    public DataTable dt
    {
        get
        {
            return _dt;
        }
        set
        {
            _dt = value;
        }
    }
    #endregion

    private string fnSendData(DataTable _dt)
    {
        if (_dt.Rows.Count > 0)
        {
            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    _senddata = _senddata + _dt.Rows[i]["prodId"].ToString() + "," + _dt.Rows[i]["qnty"].ToString() + "," + _dt.Rows[i]["catId"].ToString() + "|";
                }
            }

        }
        return _senddata;
    }
    //public string showDetail(DataTable dtCart, int? _userid, string comboid, int? _currencyid, string _senddata)
    //{
    //    string strOutPut = "";
    //    StringBuilder strCartOutput = new StringBuilder();
    //    if (dtCart.Rows.Count > 0)
    //    {
    //        double dblCurrencyValue = 1;
    //        string strDiscountCode = "";
    //        string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
    //        int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
    //        if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
    //        {
    //            string strTxtBoxId = "";
    //            int intCount = 0;
    //            //double dblTotRowPrice = 0.00;                
    //            double dblTotGrandPrice = 0.00;
    //            bool blDiscount = false;
    //            int intDiscountType = 0;
    //            double dblDiscountValue = 0.00;


    //            strCartOutput.Append("<ul class=\"topName\">");
    //            strCartOutput.Append("<li>Sl</li>");
    //            strCartOutput.Append("<li class=\"width1\">Products Name</li>");
    //            strCartOutput.Append("<li class=\"width2\">Price</li>");
    //            strCartOutput.Append("<li>Qty</li>");
    //            strCartOutput.Append("<li class=\"width2\">Total Price</li>");
    //            strCartOutput.Append("</ul><br class=\"clear\" />");
    //            //===============cart row=================================
    //            foreach (DataRow dRow in dtCart.Rows)
    //            {
    //                intCount++;
    //                if (intCount == 1)
    //                {
    //                    blDiscount = Convert.ToBoolean(dRow["boolDiscount"]);
    //                    strDiscountCode = Convert.ToString(dRow["discCode"].ToString());
    //                    dblDiscountValue = Convert.ToDouble(dRow["discValue"]);
    //                    intDiscountType = Convert.ToInt32(dRow["discType"]);
    //                }
    //                int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
    //                double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
    //                double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
    //                dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);

    //                string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
    //                string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");

    //                strCartOutput.Append("<ul class=\"prdctDetail\">");
    //                strCartOutput.Append("<li>" + intCount + ".</li>");
    //                strCartOutput.Append("<li class=\"width3\"><img src=\"" + dRow["prodImage"].ToString() + "\" alt=\"" + dRow["prodName"].ToString() + "\" title=\"\" />" + dRow["prodName"].ToString() + "</li>");
    //                strCartOutput.Append("<li class=\"width2\">" + strRowPrice + "</li>");
    //                strCartOutput.Append("<li class=\"width4\"><input onKeyPress=\"javascript:return IsNumeric(event);\" onBlur=\"javascript:chkNumeric(this);\" maxlength=\"4\" value=\"" + Convert.ToInt32(dRow["qnty"]) + "\" type=\"text\" name=\"txt" + dRow["recId"].ToString() + "\" id=\"txt" + dRow["recId"].ToString() + "\" /></li>");
    //                strTxtBoxId = strTxtBoxId + "|" + "txt" + dRow["recId"].ToString();
    //                strCartOutput.Append("<li class=\"width2\"><span id=\"ProductPrice\" title=\"" + dblTotRowPrice + "\">" + strTotRowPrice + "</span></li>");
    //                strCartOutput.Append("<li><a href=\"#\"><img src=\"images/delete.gif\" alt=\"Delete\" title=\"Delete\" border=\"0\" class=\"removeBtn\" onclick=\"deleteFromCart(" + dRow["recId"].ToString() + ", '" + dRow["prodId"].ToString() + "')\"; /></a></li>");
    //                strCartOutput.Append("</ul><br class=\"clear\" />");

    //            }
    //            //======================================================
    //            //=============subtotal ====================
    //            string strSubTotalPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");

    //            strCartOutput.Append("<ul class=\"subTotal\">");
    //            strCartOutput.Append("<li><strong>Sub Total :</strong></li>");
    //            strCartOutput.Append("<li class=\"width5\"><span id=\"ProductPrice\" title=\"" + dblTotGrandPrice + "\">" + strSubTotalPrice + "</span></li>");
    //            strCartOutput.Append("<li><strong>Shipping :</strong></li>");
    //            strCartOutput.Append("<li class=\"width5\">Free</li>");

    //            //===========================================================

    //            if (blDiscount)
    //            {
    //                //========================discount true==========================
    //                strCartOutput.Append("<li><strong>Discount Code :</strong></li>");
    //                double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
    //                strCartOutput.Append("<li class=\"width5\"><span id=\"dvDisTxt\">" + strDiscountCode + "</span></li>");
    //                //================================================================

    //            }
    //            else
    //            {
    //                //================if no discount=============================
    //                strCartOutput.Append("<li><strong>Discount Code :</strong></li>");
    //                strCartOutput.Append("<li class=\"width6\"><input type=\"text\" id=\"txtDiscount\" name=\"txtDiscount\" /><a href=\"#\"><img onclick=\"updateCartForDiscount(3, '" + HttpContext.Current.Session["SiteId"].ToString() + "', 'txtDiscount');\" src=\"images/updateBtn.gif\" alt=\"Update\" border=\"0\" title=\"Update\" /></a><br class=\"clear\" /></li>");
    //                //===================end=================================== 
    //            }

    //            strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //            //==================saving and grand total====================================
    //            string strTotalGrandPrice = "";
    //            if ((blDiscount == true) && (intDiscountType == 1))
    //            {
    //                double dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblDiscountValue);
    //                strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");


    //                strCartOutput.Append("<li><strong>Savings : </strong></li>");
    //                strCartOutput.Append("<li class=\"width5\"><span id=\"ProductPrice\" title=\"" + dblDiscountValue + "\">" + strTotalDiscountPrice + "</span></li>");
    //                strCartOutput.Append("<li><strong>Grand Total :</strong></li>");
    //                strCartOutput.Append("<li class=\"width5\"><span id=\"ProductPrice\" title=\"" + dblTotalAfterDiscount + "\">" + strTotalGrandPrice + "</span></li><br class=\"clear\" />");
    //                strCartOutput.Append("</ul><br class=\"clear\" />");

    //            }
    //            else if ((blDiscount == true) && (intDiscountType == 2))            //  %
    //            {
    //                double dblTotalAfterDiscount = 0.00;
    //                double dblTotDiscPrice = 0.00;
    //                if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
    //                {
    //                    double dblCartTotal = 0.00;
    //                    strError = "";
    //                    if (GetCartTotal(dtCart, ref dblCartTotal, ref strError) == false)
    //                    {
    //                        dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                    }
    //                    else
    //                    {
    //                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                        double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
    //                        if (dblTotDiscPrice > dblLimit)
    //                        {
    //                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblLimit);
    //                            dblTotDiscPrice = dblLimit;
    //                        }
    //                        else
    //                        {
    //                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                    dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                }

    //                strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblTotDiscPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotDiscPrice / dblCurrencyValue).ToString("0.00");

    //                strCartOutput.Append("<li><strong>Savings : </strong></li>");
    //                strCartOutput.Append("<li class=\"width5\"><span id=\"ProductPrice\" title=\"" + dblTotDiscPrice + "\">" + strTotalDiscountPrice + "</span></li>");
    //                strCartOutput.Append("<li><strong>Grand Total :</strong></li>");
    //                //strCartOutput.Append("<li class=\"width5\"><span id=\"ProductPrice\" title=\"" + dblTotalAfterDiscount + "\">" + dblTotalAfterDiscount + "</span></li><br class=\"clear\" />");
    //                strCartOutput.Append("<li class=\"width5\"><span id=\"ProductPrice\" title=\"" + dblTotalAfterDiscount + "\">" + strTotalGrandPrice + "</span></li><br class=\"clear\" />");
    //                strCartOutput.Append("</ul><br class=\"clear\" />");

    //                dblTotGrandPrice = dblTotalAfterDiscount;
    //            }
    //            else
    //            {
    //                strCartOutput.Append("<li><strong>Savings : </strong></li>");
    //                strCartOutput.Append("<li class=\"width5\"<span id=\"ProductPrice\" title=\"" + "0.00" + "\"><span id=\"ProductPrice\" title=\"" + "0.00" + "\">Rs.0.00 /" + strCurrencySymbol.ToString() + "0.00</span></span></li>");
    //                strCartOutput.Append("<li><strong>Grand Total :</strong></li>");
    //                strCartOutput.Append("<li class=\"width5\"><span id=\"ProductPrice\" title=\"" + dblTotGrandPrice + "\">" + strSubTotalPrice + "</span></li>");
    //                strCartOutput.Append("</ul><br class=\"clear\" />");

    //            }

    //            //=============process buttons==========================
    //            strCartOutput.Append("<ul class=\"process\">");
    //            strCartOutput.Append("<li><a href=\"#\"  title=\"update\" onclick=\"updateCart(1, '" + strTxtBoxId + "');\"><img src=\"images/updateBtnRefresh.gif\" alt=\"Update\" title=\"Update\" width=\"123\" height=\"35\" border=\"0\"></a></a></li>");
    //            strCartOutput.Append("<li><a href=\"index.aspx" + "\"  title=\"Back to Shopping\"><img src=\"images/contShoppingBtn.gif\" alt=\"Continue Shopping\" title=\"Continue Shopping\" width=\"193\" height=\"35\" border=\"0\"></a></li>");
    //            strCartOutput.Append("<li><a href=\"frmOption.aspx\"  title=\"Go to Checkout\"><img src=\"images/checkBtn.gif\" alt=\"Checkout\" title=\"Checkout\" width=\"133\" height=\"35\" border=\"0\"></a></li>");
    //            strCartOutput.Append("</ul><br class=\"clear\" />");
    //            //==========for combo only===============
    //            if ((comboid != "") && (comboid != null))
    //            {
    //                string frmCartSbill = "";
    //                int coun = 0;
    //                fngetComboDetailsinCart(comboid, ref frmCartSbill, ref coun);
    //                strCartOutput.Append("</p>Combind order details:<br/>");
    //                strCartOutput.Append("There are " + coun + " orders pending for payment. To view <a href=\"" + strsiteDomain + "/payment_option3.aspx?frmCart=1&frmCartSbill=" + frmCartSbill + "&frmCartCombo=" + comboid + "\">click here</a>.");
    //                strCartOutput.Append("</p><br class=\"clear\" />");
    //            }
    //            //====end =======================

    //            //===============end=================================
    //            strOutPut = strCartOutput.ToString();
    //            HttpContext.Current.Session["CartPrice"] = dblTotGrandPrice.ToString("####.00");
    //            HttpContext.Current.Session["CartQuantity"] = intCount;
    //        }
    //        else
    //        {
    //            strOutPut = "Currency retrievation error! Please try again.";
    //        }
    //    }
    //    else
    //    {
    //        strOutPut = showNoitem();
    //    }

    //    return strOutPut;
    //}
    //public string showDetail(DataTable dtCart, int? _userid, string comboid, int? _currencyid, string _senddata)
    //{
    //    string strOutPut = "";
    //    StringBuilder strCartOutput = new StringBuilder();
    //    if (dtCart.Rows.Count > 0)
    //    {
    //        double dblCurrencyValue = 1;
    //        string strDiscountCode = "";
    //        string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
    //        int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
    //        if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
    //        {
    //            string strTxtBoxId = "";
    //            int intCount = 0;
    //            //double dblTotRowPrice = 0.00;                
    //            double dblTotGrandPrice = 0.00;
    //            bool blDiscount = false;
    //            int intDiscountType = 0;
    //            double dblDiscountValue = 0.00;

    //            strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableBorder\"> ");
    //            //strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            strCartOutput.Append("<td valign=\"top\" align=\"left\">");
    //            strCartOutput.Append("<!--- The main Cart Table Starts--->");
    //            strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
    //            strCartOutput.Append("<tr bgcolor=\"#B20000\" class=\"footer-copyright\">");
    //            strCartOutput.Append("<td width=\"8%\" valign=\"middle\" align=\"left\" class=\"redTxt\" style=\"height: 20px\"><div align=\"center\"><strong>Sl No</strong></div></td> ");
    //            strCartOutput.Append("<td width=\"35%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Item</strong></td> ");
    //            strCartOutput.Append("<td width=\"7%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Qty</strong></td>");
    //            strCartOutput.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Price</strong></td> ");
    //            strCartOutput.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Net Price</strong></td> ");
    //            strCartOutput.Append("<td width=\"10%\" valign=\"middle\" align=\"left\" class=\"redTxt\" style=\"height: 20px\">&nbsp;</td>");
    //            strCartOutput.Append("</tr>");

    //            foreach (DataRow dRow in dtCart.Rows)
    //            {
    //                intCount++;
    //                if (intCount == 1)
    //                {
    //                    blDiscount = Convert.ToBoolean(dRow["boolDiscount"]);
    //                    strDiscountCode = Convert.ToString(dRow["discCode"].ToString());
    //                    dblDiscountValue = Convert.ToDouble(dRow["discValue"]);
    //                    intDiscountType = Convert.ToInt32(dRow["discType"]);
    //                }
    //                int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
    //                double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
    //                double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
    //                dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);

    //                string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
    //                string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr> ");
    //                strCartOutput.Append("<td colspan=\"6\">");
    //                strCartOutput.Append("<!--- Cart's Rows Starts--->");
    //                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"8%\" align=\"center\" valign=\"middle\">");
    //                strCartOutput.Append("<div align=\"center\">" + intCount + "</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"35%\">");
    //                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"30%\" valign=\"middle\" align=\"center\" class=\"padd3\">");

    //                strCartOutput.Append("<!--- Product Hyperlink Starts--->");
    //                ////strCartOutput.Append("<div align=\"center\"><a href=\"Gifts.aspx?proid=" + dRow["prodId"].ToString() + "&CatId=" + dRow["catId"].ToString() + "\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"></a></div>");
    //                //strCartOutput.Append("<div align=\"center\"><a href=\"#lightbox" + dRow["prodId"].ToString() + "\" rel=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"lbOn\">");
    //                //strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"/></a>");
    //                //strCartOutput.Append("</div>");
    //                strCartOutput.Append("<a href=\"" + dRow["prodImage"].ToString() + "\" rel=\"lightbox\">");
    //                strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"/>");
    //                strCartOutput.Append("</a>");

    //                //strCartOutput.Append("<!--start popup window-->");
    //                //strCartOutput.Append("<div id=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"leightbox\">");
    //                //strCartOutput.Append("<div class=\"holder\" style=\"width:450px;\">");
    //                //strCartOutput.Append("<table id=\"tabPopUp" + dRow["prodId"].ToString() + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" bgcolor=\"#ffffff\"> ");
    //                ////strCartOutput.Append("<tr> ");
    //                ////strCartOutput.Append("<td align=\"right\" class=\"small_red\" style=\"width: 20%\"></td> ");
    //                ////strCartOutput.Append("<td width=\"1%\" class=\"small_red\"></td>");
    //                ////strCartOutput.Append("<td width=\"79%\" class=\"style1\" align=\"right\"> <a href=\"#\" class=\"lbAction\" rel=\"deactivate\" style=\"text-decoration:none; \"><b>X</b></a>&nbsp;&nbsp;&nbsp; </td> ");
    //                ////strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("<tr>");
    //                //strCartOutput.Append("<td width=\"100%\" colspan=\"3\" align=\"center\" class=\"redTxt\"><b>" + dRow["prodName"].ToString() + "</b></td> ");
    //                //strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("<tr>");
    //                ////strCartOutput.Append("<td align=\"right\" class=\"small_red\" width=\"20%\">");
    //                //strCartOutput.Append("<td align=\"center\" valign=\"middle\" class=\"small_red\" colspan=\"3\" width=\"100%\">");
    //                //strCartOutput.Append("<div id=\"dvPopUpImg\" style=\"width:100%;\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"350\" height=\"350\" border=\"0\"/></div>");
    //                ////strCartOutput.Append("</td>");
    //                ////strCartOutput.Append("<td width=\"1%\" class=\"small_red\"></td> ");
    //                ////strCartOutput.Append("<td width=\"79%\" class=\"style2\">");
    //                ////strCartOutput.Append("<div id=\"dvProdDesc\" style=\"width:100%;\">");
    //                ////strCartOutput.Append("</div>");
    //                //strCartOutput.Append("</td>");
    //                //strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("<tr>");
    //                //strCartOutput.Append("<td align=\"center\" class=\"small_red\" width=\"20%\" colspan=\"3\" valign=\"middle\"><a href=\"#\" class=\"lbAction\" rel=\"deactivate\"><img src=\"Pictures/close.jpg\" alt=\"Close\" border=\"0\"/></a> </td> ");
    //                //strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("</table>");
    //                //strCartOutput.Append("</div>");
    //                //strCartOutput.Append("</div>");
    //                //strCartOutput.Append("<!--end popup window-->");
    //                strCartOutput.Append("<!--- Product Hyperlink Ends--->");

    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"padd3\">" + dRow["prodName"].ToString() + "</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"7%\" align=\"center\" valign=\"middle\">");
    //                strCartOutput.Append("<div align=\"center\">");
    //                strTxtBoxId = strTxtBoxId + "|" + "txt" + dRow["recId"].ToString();
    //                strCartOutput.Append("<input name=\"txt" + dRow["recId"].ToString() + "\" maxlength=\"4\" id=\"txt" + dRow["recId"].ToString() + "\" onKeyPress=\"javascript:return IsNumeric(event);\" onBlur=\"javascript:chkNumeric(this);\" type=\"text\" value=\"" + intRowQnty + "\" class=\"qty\" size=\"4\"> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"20%\" align=\"center\" valign=\"middle\"> ");
    //                strCartOutput.Append("<div title=\"" + dblRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strRowPrice + "</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"20%\" align=\"left\" valign=\"middle\" > ");
    //                strCartOutput.Append("<div title=\"" + dblTotRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strTotRowPrice + "</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"10%\" valign=\"middle\" align=\"center\" class=\"padd3\"> ");
    //                strCartOutput.Append("<div align=\"center\" id=\"" + dRow["recId"].ToString() + "\">");
    //                strCartOutput.Append("<a href=\"#" + dRow["prodId"].ToString() + "\" onclick=\"deleteFromCart(" + dRow["recId"].ToString() + ", '" + dRow["prodId"].ToString() + "')\";><img src=\"" + System.Configuration.ConfigurationManager.AppSettings["Domain"].ToString() + "images/remove_btn.gif\" width=\"56\" height=\"15\" border=\"0\" alt=\"Remove\" ></a>");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("<!--- Cart's Rows Ends--->");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //            }
    //            strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            strCartOutput.Append("<td colspan=\"4\" align=\"left\" valign=\"middle\">");
    //            //strCartOutput.Append("&nbsp; &nbsp; &nbsp; If you have changed quantities&nbsp;<a href=\"#\" onclick=\"updateCart(1);\"><img src=\"Pictures/but_update.gif\" onclick=\"updateCart(1);\"/ border=\"0\"></a> your total.");
    //            strCartOutput.Append("&nbsp; &nbsp; &nbsp; If you have changed quantities&nbsp;<a href=\"#\" onclick=\"updateCart(1, '" + strTxtBoxId + "');\"><img src=\"images/update_btn.gif\" alt=\"Update\" border=\"0\"></a> your total.");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("<td colspan=\"2\">");
    //            strCartOutput.Append("</tr>");
    //            //strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<!--- Cart's SubTotal Section Starts--->");
    //            string strSubTotalPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
    //            strCartOutput.Append("<tr height=\"18px\">");
    //            strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //            strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //            strCartOutput.Append("<strong>SubTotal&nbsp;&nbsp;:</strong>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //            strCartOutput.Append("<div align=\"left\" id=\"ProductSubPrice\">");
    //            strCartOutput.Append("<div align=\"left\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\"> " + strSubTotalPrice + "</div> ");
    //            strCartOutput.Append("</div>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<!--- Cart's SubTotal Section Ends--->");
    //            strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");
    //            strCartOutput.Append("<tr height=\"18px\">");
    //            strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //            strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //            strCartOutput.Append("<strong>Shipping :&nbsp;&nbsp; </strong>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //            strCartOutput.Append(" - FREE - ");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");

    //            if (blDiscount)
    //            {

    //                strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //                strCartOutput.Append("<strong>Savings Code::&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"30%\" colspan=\"3\" valign=\"middle\">");
    //                if (Convert.ToString(HttpContext.Current.Session["discLimit"]) != "0")
    //                {
    //                    double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
    //                    //strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + " (Limit:" + dblLimit + ")</div>");                        
    //                    strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
    //                }
    //                else
    //                {
    //                    strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
    //                }

    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
    //            }
    //            else
    //            {
    //                strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //                strCartOutput.Append("<strong>Discount Code:&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"30%\" valign=\"middle\">");
    //                strCartOutput.Append("<div id=\"dvDisTxt\"><input type=\"text\" id=\"txtDiscount\" name=\"txtDiscount\" style=\"width:80px;\" class=\"qty\" maxlength=\"10\"/></div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"1%\"></td>");
    //                strCartOutput.Append("<td width=\"69%\">");
    //                if (HttpContext.Current.Session["SiteId"] != null)
    //                {
    //                    strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, '" + HttpContext.Current.Session["SiteId"].ToString() + "', 'txtDiscount');\"><img src=\"images/update_btn.gif\" alt=\"Update\" border=\"0\"></a>");
    //                }
    //                else
    //                {
    //                    strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, '1', 'txtDiscount');\"><img src=\"images/update_btn.gif\" alt=\"Update\" border=\"0\"></a>");
    //                }
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
    //            }

    //            strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //            string strTotalGrandPrice = "";
    //            if ((blDiscount == true) && (intDiscountType == 1))
    //            {
    //                double dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblDiscountValue);
    //                strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //                dblTotGrandPrice = dblTotalAfterDiscount;
    //            }
    //            else if ((blDiscount == true) && (intDiscountType == 2))            //  %
    //            {
    //                double dblTotalAfterDiscount = 0.00;
    //                double dblTotDiscPrice = 0.00;
    //                if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
    //                {
    //                    double dblCartTotal = 0.00;
    //                    strError = "";
    //                    if (GetCartTotal(dtCart, ref dblCartTotal, ref strError) == false)
    //                    {
    //                        dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                    }
    //                    else
    //                    {
    //                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                        double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
    //                        if (dblTotDiscPrice > dblLimit)
    //                        {
    //                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblLimit);
    //                            dblTotDiscPrice = dblLimit;
    //                        }
    //                        else
    //                        {
    //                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                    dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                }

    //                strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblTotDiscPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotDiscPrice / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"mailDisc\">");
    //                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"mailDisc\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotDiscPrice + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //                dblTotGrandPrice = dblTotalAfterDiscount;
    //            }
    //            else
    //            {
    //                strTotalGrandPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
    //                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //            }


    //            strCartOutput.Append("</table>");
    //            strCartOutput.Append("<!--- The main Cart Table Ends--->");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"><input type=\"hidden\" id=\"hdnTxtBoxId\" value=\"" + strTxtBoxId + "\" /></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            strCartOutput.Append("<td align=\"center\" valign=\"top\" class=\"padd5\">");
    //            strCartOutput.Append("<div align=\"center\"><a href=\"" + strDomain + "\"><img src=\"images/continue_shop_btn.gif\" alt=\"Continue Shopping\" title=\"Continue Shopping\" border=\"0\" /></a> </div>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            // Blocked due the site down
    //            strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"padd3\">");
    //            strCartOutput.Append("<a href=\"SubmitForm.aspx\"><img src=\"images/proceed_btn.gif\" alt=\"Proceed to Checkout\" width=\"182\" height=\"24\" border=\"0\" title=\"Proceed to Checkout\"/></a>");
    //            // Blocked due the site down
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("</table>");
    //            strOutPut = strCartOutput.ToString();
    //            HttpContext.Current.Session["CartPrice"] = dblTotGrandPrice;
    //            HttpContext.Current.Session["CartQuantity"] = intCount;
    //        }
    //        else
    //        {
    //            strOutPut = "Currency retrievation error! Please try again.";
    //        }
    //    }
    //    else
    //    {
    //        strOutPut = showNoitem();
    //    }

    //    return strOutPut;
    //}
    protected void fngetComboDetailsinCart(string combo, ref string frmCartSbill, ref int coun)
    {

        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[ComboSbill_Relation].[sbillno] " +
                    " FROM " +
                        "[" + strSchema + "].[ComboSbill_Relation] " +
                    " WHERE " +
                        "([" + strSchema + "].[ComboSbill_Relation].[comboid]='" + combo + "')";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        coun++;
                        frmCartSbill = dr["sbillno"].ToString();
                    }
                }
            }
            else
            {
                coun = 0;
            }
        }
        catch (SqlException ex)
        {
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }



    public cartFunctions()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public cartFunctions(DataTable dtCart)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Prints the html depending on the datatable holded in the session.
    /// </summary>
    /// <param name="dtCart">The datatable object.</param>
    /// <returns>HTML</returns>
    //public string showDetail(DataTable dtCart)
    //{
    //    string strOutPut = "";
    //    StringBuilder strCartOutput=new StringBuilder();
    //    if (dtCart.Rows.Count > 0)
    //    {
    //        double dblCurrencyValue = 1;
    //        string strDiscountCode="";
    //        string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
    //        int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
    //        if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
    //        {
    //            string strTxtBoxId = "";
    //            int intCount = 0;
    //            //double dblTotRowPrice = 0.00;                
    //            double dblTotGrandPrice = 0.00;
    //            bool blDiscount = false;
    //            int intDiscountType = 0;
    //            double dblDiscountValue = 0.00;

    //            strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"> ");
    //            strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            strCartOutput.Append("<td valign=\"top\" align=\"left\">");
    //            strCartOutput.Append("<!--- The main Cart Table Starts--->");
    //            strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
    //            strCartOutput.Append("<tr bgcolor=\"#FF6600\" class=\"footer-copyright\">");
    //            strCartOutput.Append("<td width=\"5%\" valign=\"middle\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><div align=\"center\"><strong>Sl No</strong></div></td> ");
    //            strCartOutput.Append("<td width=\"38%\" valign=\"middle\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Item</strong></td> ");
    //            strCartOutput.Append("<td width=\"7%\" valign=\"middle\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Qty</strong></td>");
    //            strCartOutput.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Price</strong></td> ");
    //            strCartOutput.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Net Price</strong></td> ");
    //            strCartOutput.Append("<td width=\"10%\" valign=\"middle\" align=\"left\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\">&nbsp;&nbsp;&nbsp;</td>");
    //            strCartOutput.Append("</tr>");

    //            foreach (DataRow dRow in dtCart.Rows)
    //            {
    //                intCount++;
    //                if (intCount == 1)
    //                {
    //                    blDiscount=Convert.ToBoolean(dRow["boolDiscount"]);
    //                    strDiscountCode=Convert.ToString(dRow["discCode"].ToString());
    //                    dblDiscountValue = Convert.ToDouble(dRow["discValue"]);
    //                    intDiscountType = Convert.ToInt32(dRow["discType"]);
    //                }
    //                int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
    //                double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
    //                double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
    //                dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);

    //                string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
    //                string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr class=\"body-text-dark12\"> ");
    //                strCartOutput.Append("<td colspan=\"6\">");
    //                strCartOutput.Append("<!--- Cart's Rows Starts--->");
    //                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
    //                strCartOutput.Append("<tr class=\"style1\">");
    //                strCartOutput.Append("<td width=\"5%\" align=\"center\" valign=\"middle\">");
    //                strCartOutput.Append("<div align=\"center\">" + intCount + "</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"35%\" class=\"style1\">");
    //                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"30%\" valign=\"middle\" align=\"center\" class=\"style1\">");

    //                strCartOutput.Append("<!--- Product Hyperlink Starts--->");
    //                //strCartOutput.Append("<div align=\"center\"><a href=\"Gifts.aspx?proid=" + dRow["prodId"].ToString() + "&CatId=" + dRow["catId"].ToString() + "\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"></a></div>");
    //                strCartOutput.Append("<div align=\"center\"><a href=\"#lightbox" + dRow["prodId"].ToString() + "\" rel=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"lbOn\">");
    //                strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"/></a>");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("<!--start popup window-->");
    //                strCartOutput.Append("<div id=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"leightbox\">");
    //                strCartOutput.Append("<div class=\"holder\" style=\"width:450px;\">");
    //                strCartOutput.Append("<table id=\"tabPopUp" + dRow["prodId"].ToString() + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" bgcolor=\"#ffffff\"> ");
    //                //strCartOutput.Append("<tr> ");
    //                //strCartOutput.Append("<td align=\"right\" class=\"small_red\" style=\"width: 20%\"></td> ");
    //                //strCartOutput.Append("<td width=\"1%\" class=\"small_red\"></td>");
    //                //strCartOutput.Append("<td width=\"79%\" class=\"style1\" align=\"right\"> <a href=\"#\" class=\"lbAction\" rel=\"deactivate\" style=\"text-decoration:none; \"><b>X</b></a>&nbsp;&nbsp;&nbsp; </td> ");
    //                //strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"100%\" colspan=\"3\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\"><b>" + dRow["prodName"].ToString() + "</b></td> ");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<tr>");
    //                //strCartOutput.Append("<td align=\"right\" class=\"small_red\" width=\"20%\">");
    //                strCartOutput.Append("<td align=\"center\" valign=\"middle\" class=\"small_red\" colspan=\"3\" width=\"100%\">");
    //                strCartOutput.Append("<div id=\"dvPopUpImg\" style=\"width:100%;\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"350\" height=\"350\" border=\"0\"/></div>");
    //                //strCartOutput.Append("</td>");
    //                //strCartOutput.Append("<td width=\"1%\" class=\"small_red\"></td> ");
    //                //strCartOutput.Append("<td width=\"79%\" class=\"style2\">");
    //                //strCartOutput.Append("<div id=\"dvProdDesc\" style=\"width:100%;\">");
    //                //strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td align=\"center\" class=\"small_red\" width=\"20%\" colspan=\"3\" valign=\"middle\"><a href=\"#\" class=\"lbAction\" rel=\"deactivate\"><img src=\"Pictures/close.jpg\" alt=\"Close\" border=\"0\"/></a> </td> ");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("<!--end popup window-->");
    //                strCartOutput.Append("<!--- Product Hyperlink Ends--->");

    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"style1\">" + dRow["prodName"].ToString() + "</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"10%\" align=\"center\" valign=\"middle\">");
    //                strCartOutput.Append("<div align=\"center\">");
    //                strTxtBoxId = strTxtBoxId + "|" + "txt" + dRow["recId"].ToString();
    //                strCartOutput.Append("<input name=\"txt" + dRow["recId"].ToString() + "\" maxlength=\"4\" id=\"txt" + dRow["recId"].ToString() + "\" onKeyPress=\"javascript:return IsNumeric(event);\" onBlur=\"javascript:chkNumeric(this);\" type=\"text\" value=\"" + intRowQnty + "\" class=\"body-text-dark\" size=\"4\"> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"20%\" align=\"center\" valign=\"middle\"> ");
    //                strCartOutput.Append("<div title=\"" + dblRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strRowPrice + "</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"20%\" align=\"left\" valign=\"middle\" > ");
    //                strCartOutput.Append("<div title=\"" + dblTotRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strTotRowPrice + "</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"10%\" valign=\"middle\" align=\"center\" > ");
    //                strCartOutput.Append("<div align=\"center\" id=\"" + dRow["recId"].ToString() + "\">");
    //                strCartOutput.Append("<a href=\"#" + dRow["prodId"].ToString() + "\" onclick=\"deleteFromCart(" + dRow["recId"].ToString() + ", '" + dRow["prodId"].ToString() + "')\";><img src=\"Pictures/but_remove.gif\" width=\"85\" height=\"15\" border=\"0\" alt=\"Remove\" ></a>");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("<!--- Cart's Rows Ends--->");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");                    
    //            }                
    //            strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            strCartOutput.Append("<td colspan=\"4\" class=\"style4\" align=\"left\" valign=\"middle\">");
    //            //strCartOutput.Append("&nbsp; &nbsp; &nbsp; If you have changed quantities&nbsp;<a href=\"#\" onclick=\"updateCart(1);\"><img src=\"Pictures/but_update.gif\" onclick=\"updateCart(1);\"/ border=\"0\"></a> your total.");
    //            strCartOutput.Append("&nbsp; &nbsp; &nbsp; If you have changed quantities&nbsp;<a href=\"#\" onclick=\"updateCart(1, '" + strTxtBoxId + "');\"><img src=\"Pictures/but_update.gif\" alt=\"Update\" border=\"0\"></a> your total.");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("<td colspan=\"2\" class=\"style4\">");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<!--- Cart's SubTotal Section Starts--->");
    //            string strSubTotalPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
    //            strCartOutput.Append("<tr height=\"18px\">");
    //            strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //            strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //            strCartOutput.Append("<strong>SubTotal&nbsp;&nbsp;:</strong>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //            strCartOutput.Append("<div align=\"left\" id=\"ProductSubPrice\">");
    //            strCartOutput.Append("<div align=\"left\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\"> " + strSubTotalPrice + "</div> ");
    //            strCartOutput.Append("</div>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<!--- Cart's SubTotal Section Ends--->");
    //            strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");
    //            strCartOutput.Append("<tr height=\"18px\">");
    //            strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //            strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //            strCartOutput.Append("<strong>Shipping :&nbsp;&nbsp; </strong>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //            strCartOutput.Append(" - FREE - ");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");

    //            if (blDiscount)
    //            {

    //                strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Savings Code::&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"30%\" colspan=\"3\" valign=\"middle\" class=\"style1\">");
    //                if (Convert.ToString(HttpContext.Current.Session["discLimit"]) != "0")
    //                {
    //                    double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
    //                    //strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + " (Limit:" + dblLimit + ")</div>");                        
    //                    strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
    //                }
    //                else
    //                {
    //                    strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");                        
    //                }

    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
    //            }
    //            else
    //            {
    //                strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Discount Code:&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
    //                strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
    //                strCartOutput.Append("<tr>");
    //                strCartOutput.Append("<td width=\"30%\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<div id=\"dvDisTxt\"><input type=\"text\" id=\"txtDiscount\" name=\"txtDiscount\" style=\"width:80px;\" class=\"small_red\" maxlength=\"10\"/></div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td width=\"1%\" class=\"style1\"></td>");
    //                strCartOutput.Append("<td width=\"69%\" class=\"style1\">");
    //                if (HttpContext.Current.Session["SiteId"] != null)
    //                {
    //                    strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, '" + HttpContext.Current.Session["SiteId"].ToString() + "', 'txtDiscount');\"><img src=\"Pictures/but_update.gif\" alt=\"Update\" border=\"0\"></a>");
    //                }
    //                else
    //                {
    //                    strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, '1', 'txtDiscount');\"><img src=\"Pictures/but_update.gif\" alt=\"Update\" border=\"0\"></a>");
    //                }
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
    //                strCartOutput.Append("</table>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
    //            }

    //            strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //            string strTotalGrandPrice = "";
    //            if ((blDiscount == true) && (intDiscountType == 1))
    //            {
    //                double dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice- dblDiscountValue);
    //                strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");                    
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //                dblTotGrandPrice = dblTotalAfterDiscount;
    //            }
    //            else if ((blDiscount == true) && (intDiscountType == 2))            //  %
    //            {
    //                double dblTotalAfterDiscount = 0.00;
    //                double dblTotDiscPrice = 0.00;
    //                if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
    //                {
    //                    double dblCartTotal = 0.00;
    //                    strError = "";
    //                    if (GetCartTotal(dtCart, ref dblCartTotal, ref strError) == false)
    //                    {
    //                        dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                    }
    //                    else
    //                    {
    //                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                        double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
    //                        if (dblTotDiscPrice > dblLimit)
    //                        {
    //                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblLimit);
    //                            dblTotDiscPrice = dblLimit;
    //                        }
    //                        else
    //                        {                                
    //                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));                                
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
    //                    dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
    //                }                    

    //                strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblTotDiscPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotDiscPrice / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotDiscPrice + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
    //                dblTotGrandPrice = dblTotalAfterDiscount;
    //            }
    //            else
    //            {
    //                strTotalGrandPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
    //                string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

    //                strCartOutput.Append("<tr height=\"18px\">");
    //                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\" class=\"style1\">&nbsp;</td>");
    //                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"style1\">");
    //                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
    //                strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
    //                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
    //                strCartOutput.Append("</div>");
    //                strCartOutput.Append("</td>");
    //                strCartOutput.Append("</tr>");
    //                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");     
    //            }                


    //            strCartOutput.Append("</table>");
    //            strCartOutput.Append("<!--- The main Cart Table Ends--->");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"><input type=\"hidden\" id=\"hdnTxtBoxId\" value=\"" + strTxtBoxId + "\" /></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            strCartOutput.Append("<td align=\"center\" valign=\"top\" class=\"style1\">");
    //            strCartOutput.Append("<div align=\"center\"><a href=\"http://www.giftstokolkata24x7.com\"><img src=\"Pictures/but_continue_shopping.gif\" alt=\"Continue Shopping\" border=\"0\" /></a> </div>");
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
    //            strCartOutput.Append("<tr>");
    //            // Blocked due the site down
    //            strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
    //            strCartOutput.Append("<a href=\"SubmitForm.aspx\"><img src=\"Pictures/btn_proceed_checkout.gif\" alt=\"Proceed to Checkout\" border=\"0\" /></a>");                
    //            // Blocked due the site down
    //            strCartOutput.Append("</td>");
    //            strCartOutput.Append("</tr>");
    //            strCartOutput.Append("</table>");
    //            strOutPut = strCartOutput.ToString();
    //            HttpContext.Current.Session["CartPrice"] = dblTotGrandPrice;
    //            HttpContext.Current.Session["CartQuantity"] = intCount;
    //        }
    //        else
    //        {
    //            strOutPut = "Currency retrievation error! Please try again.";
    //        }
    //    }
    //    else
    //    {
    //        strOutPut = showNoitem();
    //    }

    //    return strOutPut;
    //}
    public string showDetail(DataTable dtCart, int? _userid, string comboid, int? _currencyid, string _senddata)
    {
        string strOutPut = "";
        StringBuilder strCartOutput = new StringBuilder();
        if (dtCart.Rows.Count > 0)
        {
            double dblCurrencyValue = 1;
            string strDiscountCode = "";
            string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
            int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
            if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
            {
                string strTxtBoxId = "";
                int intCount = 0;
                //double dblTotRowPrice = 0.00;                
                double dblTotGrandPrice = 0.00;
                bool blDiscount = false;
                int intDiscountType = 0;
                double dblDiscountValue = 0.00;

                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableBorder\"> ");
                //strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td valign=\"top\" align=\"left\">");
                strCartOutput.Append("<!--- The main Cart Table Starts--->");
                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                strCartOutput.Append("<tr bgcolor=\"#B20000\" class=\"footer-copyright\">");
                strCartOutput.Append("<td width=\"8%\" valign=\"middle\" align=\"left\" class=\"redTxt\" style=\"height: 20px\"><div align=\"center\"><strong>Sl No</strong></div></td> ");
                strCartOutput.Append("<td width=\"35%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Item</strong></td> ");
                strCartOutput.Append("<td width=\"7%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Qty</strong></td>");
                strCartOutput.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Price</strong></td> ");
                strCartOutput.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Net Price</strong></td> ");
                strCartOutput.Append("<td width=\"10%\" valign=\"middle\" align=\"left\" class=\"redTxt\" style=\"height: 20px\">&nbsp;</td>");
                strCartOutput.Append("</tr>");

                foreach (DataRow dRow in dtCart.Rows)
                {
                    intCount++;
                    if (intCount == 1)
                    {
                        blDiscount = Convert.ToBoolean(dRow["boolDiscount"]);
                        strDiscountCode = Convert.ToString(dRow["discCode"].ToString());
                        dblDiscountValue = Convert.ToDouble(dRow["discValue"]);
                        intDiscountType = Convert.ToInt32(dRow["discType"]);
                    }
                    int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
                    double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
                    double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
                    dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);

                    string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
                    string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr> ");
                    strCartOutput.Append("<td colspan=\"6\">");
                    strCartOutput.Append("<!--- Cart's Rows Starts--->");
                    strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td width=\"8%\" align=\"center\" valign=\"middle\">");
                    strCartOutput.Append("<div align=\"center\">" + intCount + "</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"35%\">");
                    strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td width=\"30%\" valign=\"middle\" align=\"center\" class=\"padd3\">");

                    strCartOutput.Append("<!--- Product Hyperlink Starts--->");
                    ////strCartOutput.Append("<div align=\"center\"><a href=\"Gifts.aspx?proid=" + dRow["prodId"].ToString() + "&CatId=" + dRow["catId"].ToString() + "\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"></a></div>");
                    //strCartOutput.Append("<div align=\"center\"><a href=\"#lightbox" + dRow["prodId"].ToString() + "\" rel=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"lbOn\">");
                    //strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"/></a>");
                    //strCartOutput.Append("</div>");
                    strCartOutput.Append("<a href=\"" + dRow["prodImage"].ToString() + "\" rel=\"lightbox\">");
                    strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"/>");
                    strCartOutput.Append("</a>");

                    //strCartOutput.Append("<!--start popup window-->");
                    //strCartOutput.Append("<div id=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"leightbox\">");
                    //strCartOutput.Append("<div class=\"holder\" style=\"width:450px;\">");
                    //strCartOutput.Append("<table id=\"tabPopUp" + dRow["prodId"].ToString() + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" bgcolor=\"#ffffff\"> ");
                    ////strCartOutput.Append("<tr> ");
                    ////strCartOutput.Append("<td align=\"right\" class=\"small_red\" style=\"width: 20%\"></td> ");
                    ////strCartOutput.Append("<td width=\"1%\" class=\"small_red\"></td>");
                    ////strCartOutput.Append("<td width=\"79%\" class=\"style1\" align=\"right\"> <a href=\"#\" class=\"lbAction\" rel=\"deactivate\" style=\"text-decoration:none; \"><b>X</b></a>&nbsp;&nbsp;&nbsp; </td> ");
                    ////strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr>");
                    //strCartOutput.Append("<td width=\"100%\" colspan=\"3\" align=\"center\" class=\"redTxt\"><b>" + dRow["prodName"].ToString() + "</b></td> ");
                    //strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr>");
                    ////strCartOutput.Append("<td align=\"right\" class=\"small_red\" width=\"20%\">");
                    //strCartOutput.Append("<td align=\"center\" valign=\"middle\" class=\"small_red\" colspan=\"3\" width=\"100%\">");
                    //strCartOutput.Append("<div id=\"dvPopUpImg\" style=\"width:100%;\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"350\" height=\"350\" border=\"0\"/></div>");
                    ////strCartOutput.Append("</td>");
                    ////strCartOutput.Append("<td width=\"1%\" class=\"small_red\"></td> ");
                    ////strCartOutput.Append("<td width=\"79%\" class=\"style2\">");
                    ////strCartOutput.Append("<div id=\"dvProdDesc\" style=\"width:100%;\">");
                    ////strCartOutput.Append("</div>");
                    //strCartOutput.Append("</td>");
                    //strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr>");
                    //strCartOutput.Append("<td align=\"center\" class=\"small_red\" width=\"20%\" colspan=\"3\" valign=\"middle\"><a href=\"#\" class=\"lbAction\" rel=\"deactivate\"><img src=\"Pictures/close.jpg\" alt=\"Close\" border=\"0\"/></a> </td> ");
                    //strCartOutput.Append("</tr>");
                    //strCartOutput.Append("</table>");
                    //strCartOutput.Append("</div>");
                    //strCartOutput.Append("</div>");
                    //strCartOutput.Append("<!--end popup window-->");
                    strCartOutput.Append("<!--- Product Hyperlink Ends--->");

                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"padd3\">" + dRow["prodName"].ToString() + "</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("</table>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"7%\" align=\"center\" valign=\"middle\">");
                    strCartOutput.Append("<div align=\"center\">");
                    strTxtBoxId = strTxtBoxId + "|" + "txt" + dRow["recId"].ToString();
                    strCartOutput.Append("<input name=\"txt" + dRow["recId"].ToString() + "\" maxlength=\"4\" id=\"txt" + dRow["recId"].ToString() + "\" onKeyPress=\"javascript:return IsNumeric(event);\" onBlur=\"javascript:chkNumeric(this);\" type=\"text\" value=\"" + intRowQnty + "\" class=\"qty\" size=\"4\"> ");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"20%\" align=\"center\" valign=\"middle\"> ");
                    strCartOutput.Append("<div title=\"" + dblRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strRowPrice + "</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"20%\" align=\"left\" valign=\"middle\" > ");
                    strCartOutput.Append("<div title=\"" + dblTotRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strTotRowPrice + "</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"10%\" valign=\"middle\" align=\"center\" class=\"padd3\"> ");
                    strCartOutput.Append("<div align=\"center\" id=\"" + dRow["recId"].ToString() + "\">");
                    strCartOutput.Append("<a href=\"#" + dRow["prodId"].ToString() + "\" onclick=\"deleteFromCart(" + dRow["recId"].ToString() + ", '" + dRow["prodId"].ToString() + "')\";><img src=\"" + System.Configuration.ConfigurationManager.AppSettings["Domain"].ToString() + "images/remove_btn.gif\" width=\"85\" height=\"15\" border=\"0\" alt=\"Remove\" ></a>");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("</table>");
                    strCartOutput.Append("<!--- Cart's Rows Ends--->");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                }
                strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td colspan=\"4\" align=\"left\" valign=\"middle\">");
                //strCartOutput.Append("&nbsp; &nbsp; &nbsp; If you have changed quantities&nbsp;<a href=\"#\" onclick=\"updateCart(1);\"><img src=\"Pictures/but_update.gif\" onclick=\"updateCart(1);\"/ border=\"0\"></a> your total.");
                strCartOutput.Append("&nbsp; &nbsp; &nbsp; If you have changed quantities&nbsp;<a href=\"#\" onclick=\"updateCart(1, '" + strTxtBoxId + "');\"><img src=\"images/update_btn.gif\" alt=\"Update\" border=\"0\"></a> your total.");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"2\">");
                strCartOutput.Append("</tr>");
                //strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
                strCartOutput.Append("<!--- Cart's SubTotal Section Starts--->");
                string strSubTotalPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>SubTotal&nbsp;&nbsp;:</strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                strCartOutput.Append("<div align=\"left\" id=\"ProductSubPrice\">");
                strCartOutput.Append("<div align=\"left\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\"> " + strSubTotalPrice + "</div> ");
                strCartOutput.Append("</div>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's SubTotal Section Ends--->");
                strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>Shipping :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                strCartOutput.Append(" - FREE - ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");

                if (blDiscount)
                {

                    strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                    strCartOutput.Append("<strong>Savings Code:&nbsp;&nbsp; </strong>");

                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td width=\"30%\" colspan=\"3\" valign=\"middle\">");
                    if (Convert.ToString(HttpContext.Current.Session["discLimit"]) != "0")
                    {
                        double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                        //strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + " (Limit:" + dblLimit + ")</div>");                        
                        strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                    }
                    else
                    {
                        strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                    }

                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
                    strCartOutput.Append("</table>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
                }
                else
                {
                    strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                    strCartOutput.Append("<strong>Discount Code:&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td width=\"30%\" valign=\"middle\">");
                    strCartOutput.Append("<div id=\"dvDisTxt\"><input type=\"text\" id=\"txtDiscount\" name=\"txtDiscount\" style=\"width:80px;\" class=\"qty\" maxlength=\"10\"/></div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"1%\"></td>");
                    strCartOutput.Append("<td width=\"69%\">");
                    if (HttpContext.Current.Session["SiteId"] != null)
                    {
                        strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, '" + HttpContext.Current.Session["SiteId"].ToString() + "', 'txtDiscount');\"><img src=\"images/update_btn.gif\" alt=\"Update\" border=\"0\"></a>");
                    }
                    else
                    {
                        strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, '1', 'txtDiscount');\"><img src=\"images/update_btn.gif\" alt=\"Update\" border=\"0\"></a>");
                    }
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
                    strCartOutput.Append("</table>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
                }

                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                string strTotalGrandPrice = "";
                if ((blDiscount == true) && (intDiscountType == 1))
                {
                    double dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblDiscountValue);
                    strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"mailDisc\">");
                    //strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("<strong>Savings :</strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
                    strCartOutput.Append("<div align=\"left\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                    strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
                    strCartOutput.Append("<div align=\"left\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                    dblTotGrandPrice = dblTotalAfterDiscount;
                }
                else if ((blDiscount == true) && (intDiscountType == 2))            //  %
                {
                    double dblTotalAfterDiscount = 0.00;
                    double dblTotDiscPrice = 0.00;
                    if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
                    {
                        double dblCartTotal = 0.00;
                        strError = "";
                        if (GetCartTotal(dtCart, ref dblCartTotal, ref strError) == false)
                        {
                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                            dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                        }
                        else
                        {
                            dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                            double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                            if (dblTotDiscPrice > dblLimit)
                            {
                                dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblLimit);
                                dblTotDiscPrice = dblLimit;
                            }
                            else
                            {
                                dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                            }
                        }
                    }
                    else
                    {
                        dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                    }

                    strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    string strTotalDiscountPrice = "Rs." + dblTotDiscPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotDiscPrice / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"mailDisc\">");
                    //strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("<strong>Savings :</strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"mailDisc\">");
                    strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
                    strCartOutput.Append("<div align=\"left\" title=\"" + dblTotDiscPrice + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                    strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
                    strCartOutput.Append("<div align=\"left\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                    dblTotGrandPrice = dblTotalAfterDiscount;
                }
                else
                {
                    strTotalGrandPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                    strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
                    strCartOutput.Append("<div align=\"left\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >" + strTotalDiscountPrice + "</div> ");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                    strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
                    strCartOutput.Append("<div align=\"left\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div> ");
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                }


                strCartOutput.Append("</table>");
                strCartOutput.Append("<!--- The main Cart Table Ends--->");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"><input type=\"hidden\" id=\"hdnTxtBoxId\" value=\"" + strTxtBoxId + "\" /></td></tr>");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td align=\"center\" valign=\"top\" class=\"padd5\">");
                strCartOutput.Append("<div align=\"center\"><a href=\"" + strDomain + "\"><img src=\"images/continue_shop_btn.gif\" alt=\"Continue Shopping\" title=\"Continue Shopping\" border=\"0\" /></a> </div>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                strCartOutput.Append("<tr>");
                // Blocked due the site down
                strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"padd3\">");
                strCartOutput.Append("<a href=\"frmOption.aspx\"><img src=\"images/proceed_btn.gif\" alt=\"Proceed to Checkout\" width=\"182\" height=\"24\" border=\"0\" title=\"Proceed to Checkout\"/></a>");
                // Blocked due the site down
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("</table>");
                //==========for combo only===============
                if ((comboid != "") && (comboid != null))
                {
                    string frmCartSbill = "";
                    int coun = 0;
                    fngetComboDetailsinCart(comboid, ref frmCartSbill, ref coun);
                    strCartOutput.Append("</p>Combind order details:<br/>");
                    strCartOutput.Append("There are " + coun + " orders pending for payment. To view <a href=\"" + strsiteDomain + "/payment_option3.aspx?frmCart=1&frmCartSbill=" + frmCartSbill + "&frmCartCombo=" + comboid + "\">click here</a>.");
                    strCartOutput.Append("</p><br class=\"clear\" />");
                }
                //====end =======================
                strOutPut = strCartOutput.ToString();
                HttpContext.Current.Session["CartPrice"] = dblTotGrandPrice;
                HttpContext.Current.Session["CartQuantity"] = intCount;
            }
            else
            {
                strOutPut = "Currency retrievation error! Please try again.";
            }
        }
        else
        {
            strOutPut = showNoitem();
        }

        return strOutPut;
    }

    /// <summary>
    /// Prints the no item html, on getting no rows in the cart datatable.
    /// </summary>
    /// <returns>HTML</returns>
    public string showNoitem()
    {
        string strOutPut = "";
        StringBuilder strCartOutput = new StringBuilder();
        strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"> ");
        strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strCartOutput.Append("<tr>");
        strCartOutput.Append("<td valign=\"top\" align=\"center\" height=\"24\">");
        strCartOutput.Append("There are no items in your cart. Please continue shopping by clicking on the 'continue shopping' button below.");
        strCartOutput.Append("</td>");
        strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strCartOutput.Append("<tr align=\"center\" valign=\"middle\">");
        strCartOutput.Append("<td valign=\"middle\" align=\"center\">");
        strCartOutput.Append("<div align=\"center\"><a href=\"" + strDomain + "\"><img src=\"images/continue_shop_btn.gif\" border=\"0\" /></a> </div>");
        strCartOutput.Append("</td>");
        strCartOutput.Append("</tr>");
        strCartOutput.Append("</table>");
        strOutPut = strCartOutput.ToString();
        HttpContext.Current.Session["CartPrice"] = 0;
        HttpContext.Current.Session["CartQuantity"] = 0;
        return strOutPut;
    }
    /// <summary>
    /// Create the cart object.
    /// </summary>
    /// <param name="strCartError"></param>
    /// <returns>The total Cart Datatable</returns>
    public DataTable createCart(ref string strCartError)
    {
        strCartError = null;
        System.Data.DataTable dtCart = new System.Data.DataTable();
        try
        {
            // Declaration for the Cart Datatable...            
            //System.Data.DataRow dRowCart=dtCart.NewRow();
            //dtCart.Columns.Add("slNo", typeof(int));
            //dtCart.Columns["slNo"].AutoIncrement = true;
            //dtCart.Columns["slNo"].AutoIncrementSeed = 1;

            dtCart.Columns.Add("recId", typeof(int));
            dtCart.Columns["recId"].AutoIncrement = true;
            dtCart.Columns["recId"].AutoIncrementSeed = 1;

            dtCart.Columns.Add("prodId", typeof(string));
            dtCart.Columns.Add("catId", typeof(int));
            dtCart.Columns.Add("prodName", typeof(string));
            dtCart.Columns.Add("prodImage", typeof(string));
            dtCart.Columns.Add("qnty", typeof(int));
            dtCart.Columns.Add("boolDiscount", typeof(bool));
            dtCart.Columns.Add("discCode", typeof(string));
            dtCart.Columns.Add("discType", typeof(int));
            dtCart.Columns.Add("discValue", typeof(int));
            dtCart.Columns.Add("rowPrice", typeof(double));
            dtCart.Columns.Add("total", typeof(double));
            dtCart.PrimaryKey = new System.Data.DataColumn[] { dtCart.Columns["recId"] };
        }
        catch (Exception ex)
        {
            strCartError = ex.Message;
        }
        return dtCart;
    }

    /// <summary>
    /// Adds a row in the datatable holded in the session.
    /// </summary>
    /// <param name="intSiteId">The site id.</param>
    /// <param name="dtCart">The datatable object</param>
    /// <param name="strProdId">Product Id to add.</param>
    /// <param name="intCatId">The category id of the product.</param>
    /// <param name="intQnty">How many of the product.</param>
    /// <param name="strCartError">Returning error.</param>
    /// <returns>True/False</returns>
    public bool AddRowToCart(int intSiteId, ref DataTable dtCart, string strProdId, int intCatId, int intQnty, ref string strCartError)
    {
        bool blFlag = false;
        string strCartDupError = "";
        if (dtCart != null)
        {
            System.Data.DataRow drNew = dtCart.NewRow();
            if (CheckCartDuplicate(ref dtCart, strProdId, intCatId, intQnty, ref strCartDupError) == false)
            {
                strError = "";
                string strProductName = "";
                string strProductImage = "";
                double dblPrice = 0.00;
                double dblTotal = 0.00;

                if (getProductData(intSiteId, strProdId, intCatId, ref strProductName, ref strProductImage, ref dblPrice, ref strError) == true)
                {
                    drNew["prodId"] = strProdId;
                    drNew["catId"] = intCatId;
                    drNew["prodName"] = strProductName;
                    drNew["prodImage"] = strProductImage;
                    drNew["qnty"] = intQnty;
                    drNew["rowPrice"] = dblPrice;

                    // Check the discount starts                    
                    if (Convert.ToString(HttpContext.Current.Session["flagDiscount"].ToString()) != "0")
                    {
                        if (dtCart.Rows.Count > 0)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                drNew["boolDiscount"] = Convert.ToBoolean(dtCart.Rows[i]["boolDiscount"]);
                                drNew["discCode"] = Convert.ToString(dtCart.Rows[i]["discCode"]);
                                drNew["discType"] = Convert.ToInt32(dtCart.Rows[i]["discType"]);
                                drNew["discValue"] = Convert.ToDouble(dtCart.Rows[i]["discValue"]);
                                //if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
                                //{
                                //    double dblCartTotal=0.00;
                                //    strError = "";
                                //    if (GetCartTotal(dtCart, ref dblCartTotal, ref strError) == false)
                                //    {
                                //        drNew["discValue"] = 0;
                                //    }
                                //    else
                                //    {
                                //        double dblDiscValue = Convert.ToDouble(dtCart.Rows[i]["discValue"]);
                                //        double dblDiscPrice = Convert.ToDouble((dblCartTotal * dblDiscValue) / 100);
                                //        if (Convert.ToDouble(HttpContext.Current.Session["discLimit"]) > dblDiscPrice)
                                //        {
                                //            drNew["discValue"] = Convert.ToDouble(dblDiscPrice);
                                //        }
                                //        else
                                //        {
                                //            drNew["discValue"] = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    drNew["discValue"] = Convert.ToDouble(dtCart.Rows[i]["discValue"]);
                                //}
                                dblTotal = Convert.ToDouble(intQnty * dblPrice);
                            }
                        }
                        else
                        {
                            drNew["boolDiscount"] = false;
                            drNew["discCode"] = "-";
                            drNew["discType"] = 0;
                            drNew["discValue"] = 0.00;
                            dblTotal = Convert.ToDouble(intQnty * dblPrice);
                        }
                    }
                    else
                    {
                        drNew["boolDiscount"] = false;
                        drNew["discCode"] = "-";
                        drNew["discType"] = 0;
                        drNew["discValue"] = 0.00;
                        dblTotal = Convert.ToDouble(intQnty * dblPrice);
                    }
                    // Check the discount ends                    
                    drNew["total"] = dblTotal;
                    dtCart.Rows.Add(drNew);
                    blFlag = true;
                }
                else
                {
                    blFlag = false;
                    strCartError = strError;
                }
            }
            else
            {
                blFlag = true;
            }
        }
        else
        {
            blFlag = false;
            strCartError = "Cart does not exist!";
        }
        return blFlag;
    }
    /// <summary>
    /// Checks duplicate in the datatable depending on the prod id and the category id.
    /// </summary>
    /// <param name="dtCart">The datatable object.</param>
    /// <param name="strProdId">Product id to find.</param>
    /// <param name="intCatId">Category Id of the product.</param>
    /// <param name="intQnty">Quantity of the product.</param>
    /// <param name="strCartError">Returning error.</param>
    /// <returns>True/False</returns>
    public bool CheckCartDuplicate(ref DataTable dtCart, string strProdId, int intCatId, int intQnty, ref string strCartError)
    {
        bool blFlag = false;
        if (dtCart != null)
        {
            if (dtCart.Rows.Count > 0)
            {
                for (int i = 0; i < dtCart.Rows.Count; i++)
                {
                    DataRow dRow = dtCart.Rows[i];
                    string strCartProdId = Convert.ToString(dRow["prodId"].ToString());
                    int intCartCatId = Convert.ToInt32(dRow["catId"].ToString());
                    int intCartQnty = Convert.ToInt32(dRow["qnty"].ToString());

                    if ((strCartProdId == strProdId) && (intCartCatId == intCatId))
                    {
                        dRow["qnty"] = intCartQnty + intQnty;
                        double dblPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
                        double dblTotal = intQnty * dblPrice;
                        dRow["total"] = Convert.ToDouble((intCartQnty + intQnty) * dblTotal);
                        blFlag = true;
                    }
                }
            }
            else
            {
                blFlag = false;
                strError = "There is no items in the cart.";
            }
        }
        return blFlag;
    }
    /// <summary>
    /// Updates the cart on getting the quantity changed.
    /// </summary>
    /// <param name="dtCart">The datatable object.</param>
    /// <param name="intRecId">The record id to update.</param>
    /// <param name="intNewQuantity">The new incoming quantity.</param>
    /// <param name="strCartError">Returning error.</param>
    /// <returns>True/False</returns>

    public bool UpdateCart(ref DataTable dtCart, int intRecId, int intNewQuantity, ref string strCartError)
    {
        bool blFlag = false;
        if (dtCart != null)
        {
            try
            {
                if (dtCart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCart.Rows.Count; i++)
                    {
                        DataRow dRow = dtCart.Rows.Find(intRecId);
                        if (dRow != null)
                        {
                            double dblPrice = 0.00;
                            dRow["qnty"] = intNewQuantity;
                            dblPrice = Convert.ToDouble(dRow["rowPrice"]);
                            dRow["total"] = Convert.ToDouble(intNewQuantity * dblPrice);
                            blFlag = true;
                            break;
                        }
                        else
                        {
                            blFlag = false;
                            strCartError = "The row now found!";
                        }
                    }
                }
                else
                {
                    blFlag = false;
                    strError = "There is no items in the cart.";
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                blFlag = false;
            }
        }
        return blFlag;
    }
    /// <summary>
    /// Delete a particular row from the datatable depending on the record key.
    /// </summary>
    /// <param name="dtCart">The datatable object.</param>
    /// <param name="intRecId">The record id to delete.</param>
    /// <param name="strProdId">The product id to delete.</param>
    /// <param name="strCartError">Returning error.</param>
    /// <returns>True/False</returns>

    public bool DeleteFromCart(ref DataTable dtCart, int intRecId, string strProdId, ref string strCartError)
    {
        bool blFlag = false;
        if (dtCart != null)
        {
            try
            {
                if (dtCart.Rows.Count > 0)
                {
                    DataRow[] RwProd = dtCart.Select("prodId='" + strProdId + "'");
                    if (RwProd.Length > 0)
                    {
                        for (int i = 0; i < dtCart.Rows.Count; i++)
                        {
                            DataRow dRow = dtCart.Rows[i];
                            string strCartProdId = Convert.ToString(dRow["prodId"].ToString());
                            int intCartRecId = Convert.ToInt32(dRow["recId"].ToString());
                            if ((intCartRecId == intRecId) && (strCartProdId == strProdId) && (dtCart.Rows.Count > 1))
                            {
                                dtCart.Rows.Remove(dRow);
                                blFlag = true;
                            }
                            else if ((intCartRecId == intRecId) && (strCartProdId == strProdId) && (dtCart.Rows.Count == 1))
                            {
                                dtCart.Rows.Remove(dRow);
                                HttpContext.Current.Session["flagDiscount"] = "0";
                                blFlag = true;
                            }
                        }
                    }
                    else
                    {
                        blFlag = false;
                        strCartError = "-";
                    }
                }
                else
                {
                    blFlag = false;
                    strCartError = "Your session is expired. Please reshop.";
                }
            }
            catch (Exception ex)
            {
                blFlag = false;
                strCartError = ex.Message;
            }
        }
        return blFlag;
    }

    /// <summary>
    /// Returns the total price of the datatable
    /// </summary>
    /// <param name="dtCart">The datatable object.</param>
    /// <param name="dblCartTotal">The returning total price.</param>
    /// <param name="strCartError">The returning error.</param>
    /// <returns>True/False</returns>

    public bool GetCartTotal(DataTable dtCart, ref double dblCartTotal, ref string strCartError)
    {
        bool blFlag = false;
        dblCartTotal = 0;
        double dblTotal = 0;
        if (dtCart != null)
        {
            for (int i = 0; i < dtCart.Rows.Count; i++)
            {
                DataRow dRow = dtCart.Rows[i];
                int intCartQnty = Convert.ToInt32(dRow["qnty"].ToString());
                double dblPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
                dblTotal = (intCartQnty * dblPrice);
                dblCartTotal = dblCartTotal + dblTotal;
            }
            blFlag = true;
        }
        else
        {
            strCartError = "No items in the cart...";
            blFlag = false;
        }
        return blFlag;
    }

    /// <summary>
    /// Returns the total rows in the datatable.
    /// </summary>
    /// <param name="dtCart">The datatable object.</param>
    /// <param name="intCartTotalQnty">The returning quantity.</param>
    /// <param name="strCartError">The returning error.</param>
    /// <returns>True/False</returns>
    public bool GetCartTotalQnty(DataTable dtCart, ref int intCartTotalQnty, ref string strCartError)
    {
        bool blFlag = false;
        intCartTotalQnty = 0;
        if (dtCart != null)
        {
            //for (int i = 0; i < dtCart.Rows.Count; i++)
            //{
            //    DataRow dRow = dtCart.Rows[i];
            //    int intCartQnty = Convert.ToInt32(dRow["qnty"].ToString());
            //    intCartTotalQnty = intCartTotalQnty + intCartQnty;
            //}
            intCartTotalQnty = dtCart.Rows.Count;
            blFlag = true;
        }
        else
        {
            strCartError = "No items in the cart...";
            blFlag = false;
        }
        return blFlag;
    }

    protected bool RoundOff(double dblValue, ref int intReturnValue, ref string strRoundOffError)
    {
        bool blFlag = false;
        try
        {
            intReturnValue = Convert.ToInt32(Math.Ceiling(dblValue - 0.49));
            blFlag = true;
        }
        catch (Exception ex)
        {
            strRoundOffError = ex.Message;
            blFlag = false;
        }
        return blFlag;
    }
    private bool getProductData(int intSiteId, string strProdId, int intCatId, ref string strProdName, ref string strProdImg, ref double dblPrice, ref string strProdError)
    {
        bool blFlag = false;
        strSql = " SELECT " +
                    " [" + strSchema + "].[ItemMaster_Server].[Item_ImagePath], " +
                    " [" + strSchema + "].[ItemMaster_Server].[Item_Price], " +
                    " [" + strSchema + "].[ItemMaster_Server].[Item_Name]," +
                    " [" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id] " +
                " FROM " +
                    " [" + strSchema + "].[ItemMaster_Server] " +
                " INNER JOIN " +
                    "[" + strSchema + "].[ItemCategoryRelation_Web_Server] " +
                " ON " +
                    "[" + strSchema + "].[ItemMaster_Server].[Product_Id]=[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Product_Id] " +
                " WHERE " +
                    " ([" + strSchema + "].[ItemMaster_Server].[Product_Id] = '" + strProdId + "') " +
                "AND " +
                    "([" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id]=" + intCatId + ") " +
                "AND " +
                    "([" + strSchema + "].[ItemMaster_Server].[Record_Status]=1)";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdProdData = new SqlCommand(strSql, conn);
            SqlDataReader drProdData = cmdProdData.ExecuteReader();
            if (drProdData.HasRows)
            {
                blFlag = true;
                if (drProdData.Read())
                {
                    strProdName = drProdData["Item_Name"].ToString();
                    //strProdImg=drProdData["Item_ImagePath"].ToString();
                    strProdImg = strSitePath + "/ASP_Img/" + strProdId + ".jpg";
                    dblPrice = Convert.ToDouble(drProdData["Item_Price"]);
                }
            }
            else
            {
                blFlag = false;
                strProdError = "The selected product is currently out of stock.  You are kindly requested to select another product.";
            }
        }
        catch (SqlException ex)
        {
            strProdError = "Product data retrieval error!<br/>" + ex.Message;
            blFlag = false;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return blFlag;
    }

    /// <summary>
    /// With discount code. (Execute and get the percentage from the database)
    /// </summary>
    /// <param name="dblTotalPrice">The total price to change.</param>
    /// <param name="dtDiscDate">The date upto which the disc code is to consider.</param>
    /// <param name="strDiscountCode">The discount code.</param>
    /// <param name="strError">Returning Error.</param>
    /// <returns>True/False</returns>

    public bool checkAndReturnDiscount(int intSiteId, ref DataTable dtCart, string strDiscountCode, ref string strFinalError)
    {
        bool blDiscount = false;
        strFinalError = null;
        if (dtCart.Rows.Count > 0)
        {
            int intDiscountType = 0;
            double dblTempPrice = 0.00;
            double dblRowPrice = 0.00;
            double dblCartTotal = 0.00;
            double dblDiscountValue = 0.00;
            if (GetCartTotal(dtCart, ref dblCartTotal, ref strError) == true)
            {
                if (getPercentageSlab(intSiteId, strDiscountCode, dblCartTotal, ref intDiscountType, ref dblDiscountValue, ref strError) == true)
                {
                    foreach (DataRow dRow in dtCart.Rows)
                    {
                        dblRowPrice = 0.00;
                        dblTempPrice = 0.00;
                        if (intDiscountType == 1)       // Fixed
                        {
                            dblRowPrice = Convert.ToDouble(dRow["rowPrice"]);
                            dblTempPrice = Convert.ToDouble(dblTempPrice + dblRowPrice);
                            int intQnty = Convert.ToInt32(dRow["qnty"]);
                            dRow["total"] = Convert.ToDouble(intQnty * (dblRowPrice));
                            dRow["boolDiscount"] = true;
                            dRow["discCode"] = strDiscountCode;
                            dRow["discType"] = 1;
                            dRow["discValue"] = dblDiscountValue;
                        }
                        else if (intDiscountType == 2)         // Percentage
                        {
                            dblRowPrice = Convert.ToDouble(dRow["rowPrice"]);
                            dblTempPrice = Convert.ToDouble(dblTempPrice + dblRowPrice);
                            int intQnty = Convert.ToInt32(dRow["qnty"]);
                            dRow["total"] = Convert.ToDouble(intQnty * (dblRowPrice));
                            dRow["boolDiscount"] = true;
                            dRow["discCode"] = strDiscountCode;
                            dRow["discType"] = 2;
                            dRow["discValue"] = dblDiscountValue;
                        }
                        blDiscount = true;
                    }
                    HttpContext.Current.Session["flagDiscount"] = 1;
                }
                else
                {
                    strFinalError = getDiscountError(strError);
                    blDiscount = false;
                }
            }
            else
            {
                strFinalError = "Cart total retrievation error. Please reshop.";
                blDiscount = false;
            }
        }
        else
        {
            strFinalError = "Your session is expired. Please reshop..";
            blDiscount = false;
        }
        return blDiscount;
    }
    public bool checkAndReturnDiscount(string strOrderNo, ref DataTable dtCart, string strDiscountCode, ref string strFinalError)
    {
        bool blDiscount = false;
        strFinalError = null;
        if (dtCart.Rows.Count > 0)
        {
            int intDiscountType = 0;
            double dblTempPrice = 0.00;
            double dblRowPrice = 0.00;
            double dblCartTotal = 0.00;
            double dblDiscountValue = 0.00;
            if (GetCartTotal(dtCart, ref dblCartTotal, ref strError) == true)
            {
                if (getPercentageSlab(strOrderNo, strDiscountCode, dblCartTotal, ref intDiscountType, ref dblDiscountValue, ref strError) == true)
                {
                    foreach (DataRow dRow in dtCart.Rows)
                    {
                        dblRowPrice = 0.00;
                        dblTempPrice = 0.00;
                        if (intDiscountType == 1)       // Fixed
                        {
                            dblRowPrice = Convert.ToDouble(dRow["rowPrice"]);
                            dblTempPrice = Convert.ToDouble(dblTempPrice + dblRowPrice);
                            int intQnty = Convert.ToInt32(dRow["qnty"]);
                            dRow["total"] = Convert.ToDouble(intQnty * (dblRowPrice));
                            dRow["boolDiscount"] = true;
                            dRow["discCode"] = strDiscountCode;
                            dRow["discType"] = 1;
                            dRow["discValue"] = dblDiscountValue;
                        }
                        else if (intDiscountType == 2)         // Percentage
                        {
                            dblRowPrice = Convert.ToDouble(dRow["rowPrice"]);
                            dblTempPrice = Convert.ToDouble(dblTempPrice + dblRowPrice);
                            int intQnty = Convert.ToInt32(dRow["qnty"]);
                            dRow["total"] = Convert.ToDouble(intQnty * (dblRowPrice));
                            dRow["boolDiscount"] = true;
                            dRow["discCode"] = strDiscountCode;
                            dRow["discType"] = 2;
                            dRow["discValue"] = dblDiscountValue;
                        }
                        blDiscount = true;
                    }
                    HttpContext.Current.Session["flagDiscount"] = 1;
                }
                else
                {
                    strFinalError = getDiscountError(strError);
                    blDiscount = false;
                }
            }
            else
            {
                strFinalError = "Cart total retrievation error. Please reshop.";
                blDiscount = false;
            }
        }
        else
        {
            strFinalError = "Your session is expired. Please reshop..";
            blDiscount = false;
        }
        return blDiscount;
    }

    /// <summary>
    /// Manual discount execution with the parameter val=1
    /// </summary>
    /// <param name="dblPrice">The price to change.</param>
    /// <param name="blDiscount">If allowed discount.</param>
    /// <param name="dblDiscountPrice">If discount==true then how much the price will be.</param>
    /// <param name="intDiscountPercentage">The discount percentage.</param>
    /// <param name="strError">Returning Error.</param>
    public void checkAndReturnDiscount(double dblPrice, ref bool blDiscount, ref double dblDiscountPrice, ref int intDiscountPercentage, ref string strError)
    {
        strError = null;
        double dblTempPrice = 0;
        int intDiscountPercent = 0;
        if (Convert.ToString(HttpContext.Current.Session["flagDiscount"].ToString()) != "0")
        {
            getPercentageSlab(ref intDiscountPercent, dblPrice);
            dblTempPrice = Convert.ToDouble(dblPrice - Convert.ToDouble((dblPrice * intDiscountPercent) / 100));
            blDiscount = true;
            dblDiscountPrice = dblTempPrice;
        }
        else
        {
            //strError = "The user is not allowd discount...";
            blDiscount = false;
            dblDiscountPrice = 0.00;
            intDiscountPercent = 0;
        }
    }
    protected void chkDiscount(ref double dblPrice, ref string strError)
    {
        strError = null;
        double dblTempPrice = 0;
        int intDiscountPercent = 0;
        getPercentageSlab(ref intDiscountPercent, dblPrice);
        if (Convert.ToString(HttpContext.Current.Session["flagDiscount"].ToString()) != "0")
        {

            dblTempPrice = Convert.ToDouble(dblPrice - Convert.ToDouble((dblPrice * intDiscountPercent) / 100));
            dblPrice = dblTempPrice;
        }
        else
        {
            strError = "The user is not allowd discount...";
        }
    }
    protected void modifyDiscount(int intDiscountPercent, ref double dblPrice, ref string strError)
    {
        strError = null;
        double dblTempPrice = dblPrice;
        try
        {
            dblTempPrice = Convert.ToDouble(dblPrice - Convert.ToDouble((dblPrice * intDiscountPercent) / 100));
            dblPrice = dblTempPrice;
        }
        catch (SqlException ex)
        {
            strError = ex.Message;
        }
    }
    protected void adjustTotalWithDiscount(ref int intDiscountPercent, ref double dblTotPrice, ref string strError)
    {
        strError = null;
        // Declare the slab of discount;
        intDiscountPercent = 0;
        double dblTempPrice = 0;
        getPercentageSlab(ref intDiscountPercent, dblTotPrice);

        if (Convert.ToString(HttpContext.Current.Session["flagDiscount"].ToString()) != "0")
        {

            dblTempPrice = Convert.ToDouble(dblTotPrice - Convert.ToDouble((dblTotPrice * intDiscountPercent) / 100));
            dblTotPrice = dblTempPrice;
        }
        else
        {
            intDiscountPercent = 0;
            strError = "The user is not allowed discount...";
        }
    }

    /// <summary>
    /// Returns the discount slab's discount percentage.
    /// </summary>
    /// <param name="intPercentage">The returning percentage.</param>
    /// <param name="dblTotPrice">The price to consider for discount.</param>
    protected void getPercentageSlab(ref int intPercentage, double dblTotPrice)
    {
        if ((Convert.ToDecimal(dblTotPrice) > 0) && (Convert.ToDecimal(dblTotPrice) < 2500))
        {
            intPercentage = 5;
        }
        else if ((Convert.ToDecimal(dblTotPrice) >= 2500) && (Convert.ToDecimal(dblTotPrice) < 4999))
        {
            intPercentage = 10;
        }
        else if ((Convert.ToDecimal(dblTotPrice) >= 5000) && (Convert.ToDecimal(dblTotPrice) < 7499))
        {
            intPercentage = 15;
        }
        else if ((Convert.ToDecimal(dblTotPrice) >= 7500) && (Convert.ToDecimal(dblTotPrice) < 9999))
        {
            intPercentage = 20;
        }
        else if ((Convert.ToDecimal(dblTotPrice) >= 10000) && (Convert.ToDecimal(dblTotPrice) < 12499))
        {
            intPercentage = 25;
        }
        else if (Convert.ToDecimal(dblTotPrice) >= 12500)
        {
            intPercentage = 30;
        }
        else
        {
            intPercentage = 0;
        }
    }
    /// <summary>
    /// Return the discount type, value from the database.
    /// </summary>
    /// <param name="strDiscountCode">Incoming discount code.</param>
    /// <param name="dblCartTotal">Incoming cart total.</param>
    /// <param name="intDiscountType">Type of the discount.</param>
    /// <param name="dblDiscountValue">The discount value.</param>
    /// <param name="strDiscErr">Returning Error.</param>
    /// <returns></returns>

    protected bool getPercentageSlab(int intSiteId, string strDiscountCode, double dblCartTotal, ref int intDiscountType, ref double dblDiscountValue, ref string strDiscErr)
    {
        bool blFlag = false;
        try
        {
            // Execute the database query
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[Discount_Code_Master].[Type], " +
                        "[" + strSchema + "].[Discount_Code_Master].[TypeValue], " +
                        "[" + strSchema + "].[Discount_Code_Master].[Limit], " +
                        "[" + strSchema + "].[Discount_Code_Master].[MaxUse], " +
                        "[" + strSchema + "].[Discount_Code_Master].[ValidityFrom], " +
                        "[" + strSchema + "].[Discount_Code_Master].[ValidityTo] " +
                    "FROM " +
                        "[" + strSchema + "].[Discount_Code_Master] " +
                    "WHERE " +
                        "([" + strSchema + "].[Discount_Code_Master].[Code]='" + strDiscountCode + "') " +
                    "AND " +
                        "([" + strSchema + "].[Discount_Code_Master].[SiteId]=" + intSiteId + ") " +
                    "AND " +
                        "([" + strSchema + "].[Discount_Code_Master].[DiscountStatus]=1);";
            SqlCommand cmdDiscount = new SqlCommand(strSql, conn);
            SqlDataReader drDiscount = cmdDiscount.ExecuteReader(CommandBehavior.CloseConnection);
            if (drDiscount.HasRows)
            {
                if (drDiscount.Read())
                {
                    DateTime dtDateValidFrom = Convert.ToDateTime(drDiscount["ValidityFrom"].ToString());
                    DateTime dtDateValidTo = Convert.ToDateTime(drDiscount["ValidityTo"].ToString());
                    TimeSpan tsDatePrev = System.DateTime.Now.Subtract(dtDateValidFrom);
                    TimeSpan tsDateNext = dtDateValidTo.Subtract(System.DateTime.Now);
                    if ((Convert.ToInt32(Math.Ceiling(tsDatePrev.TotalDays)) >= 0) && (Convert.ToInt32(Math.Ceiling(tsDateNext.TotalDays)) >= 0))
                    {
                        if (Convert.ToInt32(drDiscount["MaxUse"]) > 0)
                        {
                            switch (Convert.ToInt32(drDiscount["Type"]))
                            {
                                case 1:             // Fixed                                    
                                    intDiscountType = 1;
                                    dblDiscountValue = Convert.ToDouble(drDiscount["TypeValue"]);
                                    HttpContext.Current.Session["discLimit"] = 0;
                                    blFlag = true;
                                    break;
                                case 2:             // Percentage
                                    if ((drDiscount["Limit"] != null) && (drDiscount["Limit"].ToString() != ""))
                                    {
                                        double dblDiscValue = Convert.ToDouble(drDiscount["TypeValue"]);
                                        double dblDiscPrice = Convert.ToDouble((dblCartTotal * dblDiscValue) / 100);
                                        if (dblDiscPrice < Convert.ToDouble(drDiscount["Limit"]))
                                        {
                                            intDiscountType = 2;
                                            dblDiscountValue = dblDiscValue;
                                            blFlag = true;
                                        }
                                        else
                                        {
                                            blFlag = false;
                                            strDiscErr = "5";
                                        }
                                        HttpContext.Current.Session["discLimit"] = Convert.ToDouble(drDiscount["Limit"]);
                                    }
                                    else
                                    {
                                        intDiscountType = 2;
                                        dblDiscountValue = dblCartTotal;
                                        blFlag = true;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            blFlag = false;
                            strDiscErr = "1";
                        }
                    }
                    else
                    {
                        blFlag = false;
                        strDiscErr = "3";
                    }
                }
            }
            else
            {
                blFlag = false;
                strDiscErr = "2";
            }
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strDiscErr = ex.Message;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return blFlag;
    }

    protected bool getPercentageSlab(string strOrderNumber, string strDiscountCode, double dblCartTotal, ref int intDiscountType, ref double dblDiscountValue, ref string strDiscErr)
    {
        bool blFlag = false;
        try
        {
            // Execute the database query
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[Discount_Code_Master].[Type], " +
                        "[" + strSchema + "].[Discount_Code_Master].[TypeValue], " +
                        "[" + strSchema + "].[Discount_Code_Master].[Limit], " +
                        "[" + strSchema + "].[Discount_Code_Master].[MaxUse], " +
                        "[" + strSchema + "].[Discount_Code_Master].[ValidityFrom], " +
                        "[" + strSchema + "].[Discount_Code_Master].[ValidityTo] " +
                    "FROM " +
                        "[" + strSchema + "].[Discount_Code_Master] " +
                    "WHERE " +
                        "(UNICODE([" + strSchema + "].[Discount_Code_Master].[Code])=UNICODE('" + strDiscountCode + "')) " +
                    "AND " +
                        "([" + strSchema + "].[Discount_Code_Master].[OrderId]='" + strOrderNumber + "');";
            SqlCommand cmdDiscount = new SqlCommand(strSql, conn);
            SqlDataReader drDiscount = cmdDiscount.ExecuteReader(CommandBehavior.CloseConnection);
            if (drDiscount.HasRows)
            {
                if (drDiscount.Read())
                {
                    DateTime dtDateValidFrom = Convert.ToDateTime(drDiscount["ValidityFrom"].ToString());
                    DateTime dtDateValidTo = Convert.ToDateTime(drDiscount["ValidityTo"].ToString());
                    TimeSpan tsDatePrev = System.DateTime.Now.Subtract(dtDateValidFrom);
                    TimeSpan tsDateNext = dtDateValidTo.Subtract(System.DateTime.Now);
                    if ((Convert.ToInt32(Math.Ceiling(tsDatePrev.TotalDays)) >= 0) && (Convert.ToInt32(Math.Ceiling(tsDateNext.TotalDays)) >= 0))
                    {
                        if (Convert.ToInt32(drDiscount["MaxUse"]) > 0)
                        {
                            switch (Convert.ToInt32(drDiscount["Type"]))
                            {
                                case 1:             // Fixed                                    
                                    intDiscountType = 1;
                                    dblDiscountValue = Convert.ToDouble(drDiscount["TypeValue"]);
                                    HttpContext.Current.Session["discLimit"] = 0;
                                    blFlag = true;
                                    break;
                                case 2:             // Percentage
                                    if ((drDiscount["Limit"] != null) && (drDiscount["Limit"].ToString() != ""))
                                    {
                                        double dblDiscValue = Convert.ToDouble(drDiscount["TypeValue"]);
                                        double dblDiscPrice = Convert.ToDouble((dblCartTotal * dblDiscValue) / 100);
                                        if (dblDiscPrice < Convert.ToDouble(drDiscount["Limit"]))
                                        {
                                            intDiscountType = 2;
                                            dblDiscountValue = dblDiscValue;
                                            blFlag = true;
                                        }
                                        else
                                        {
                                            blFlag = false;
                                            strDiscErr = "5";
                                        }
                                        HttpContext.Current.Session["discLimit"] = Convert.ToDouble(drDiscount["Limit"]);
                                    }
                                    else
                                    {
                                        intDiscountType = 2;
                                        dblDiscountValue = dblCartTotal;
                                        blFlag = true;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            blFlag = false;
                            strDiscErr = "1";
                        }
                    }
                    else
                    {
                        blFlag = false;
                        strDiscErr = "3";
                    }
                }
            }
            else
            {
                blFlag = false;
                strDiscErr = "2";
            }
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strDiscErr = ex.Message;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return blFlag;
    }
    /// <summary>
    /// Returns the discount details.
    /// </summary>
    /// <param name="strDiscountCode">The discount code.</param>
    /// <param name="intPercentage">The returning percentage.</param>
    /// <param name="strDiscErr"></param>
    /// <returns>True/False</returns>
    protected bool getPercentageSlab(string strDiscountCode, ref string strDiscountedProductId, ref int intPercentage, ref string strDiscErr)
    {
        bool blFlag = false;
        try
        {
            // Execute the database query
            DateTime dtDate = Convert.ToDateTime("04/19/2008");
            string strDBProductId = "GTI0388";
            string strdbDiscCode = "AB2008";
            TimeSpan tsDate = dtDate.Subtract(System.DateTime.Now);
            if ((tsDate != null) && (strDBProductId != ""))
            {
                if ((strdbDiscCode == strDiscountCode) && (tsDate.TotalDays < 1))
                {
                    strDiscountedProductId = strDBProductId;
                    intPercentage = 5;
                    blFlag = true;
                }
                else if ((strdbDiscCode == strDiscountCode) && ((tsDate.TotalDays > 1) && (tsDate.TotalDays < 10)))
                {
                    strDiscountedProductId = strDBProductId;
                    intPercentage = 10;
                    blFlag = true;
                }
                else if ((strdbDiscCode == strDiscountCode) && (tsDate.TotalDays < 0))
                {
                    intPercentage = 0;
                    blFlag = false;
                    strDiscErr = "1";
                }
                else if ((strdbDiscCode == strDiscountCode) && (tsDate.TotalDays < 0))
                {
                    intPercentage = 0;
                    blFlag = false;
                    strDiscErr = "2";
                }
                else if (strdbDiscCode != strDiscountCode)
                {
                    intPercentage = 0;
                    blFlag = false;
                    strDiscErr = "3";
                }
            }
        }
        catch (Exception ex)
        {
            blFlag = false;
            strDiscErr = ex.Message;
        }
        return blFlag;
    }
    protected string getDiscountError(string strErrorCode)
    {
        string strOutPut = "";
        switch (strErrorCode)
        {
            case "1":
                strOutPut = "Max limit crossed. This discount code has reached its maximum number of uses. This code cannot be used.";
                break;
            case "2":
                strOutPut = "The discount code entered by you does not match.  Please try again.";
                break;
            case "3":
                strOutPut = "The time for utilising this discount code has expired.  This code cannot be used.";
                break;
            case "4":
                strOutPut = "The discount code is not valid. Does not valid for any of the product.";
                break;
            case "5":
                strOutPut = "The discount code is not valid. It is out of cart total range.";
                break;
            default:
                strOutPut = "The discount code is not valid. Discount code does not match.";
                break;
        }
        return strOutPut;
    }
    public string printCartHtml(DataTable dtShoppingCart)
    {
        string strOutPut = "";
        StringBuilder strCartOutput = new StringBuilder();
        if (dtShoppingCart.Rows.Count > 0)
        {
            double dblCurrencyValue = 1;
            string strDiscountCode = "";
            int intCurrencyId = 0;
            string strCurrencySymbol = "";
            if (HttpContext.Current.Session["CurrencySymbol"] != null)
            {
                strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
            }
            else
            {
                strCurrencySymbol = "";
            }
            if (HttpContext.Current.Session["CurrencyId"] != null)
            {
                intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
            }
            else
            {
                intCurrencyId = 1;
            }
            if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
            {
                int intCount = 0;
                //double dblTotRowPrice = 0.00;                
                double dblTotGrandPrice = 0.00;
                bool blDiscount = false;
                int intDiscountType = 0;
                double dblDiscountValue = 0.00;

                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableborder\"> ");
                //strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr><tr>");
                strCartOutput.Append("<tr bgcolor=\"#B20000\">");
                strCartOutput.Append("<td align=\"center\" valign=\"middle\" class=\"style3\" style=\"height: 20px\">");
                strCartOutput.Append("<b>:: Product Details ::</b>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td valign=\"top\" align=\"left\">");
                strCartOutput.Append("<!--- The main Cart Table Starts--->");
                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                strCartOutput.Append("<tr bgcolor=\"#B20000\">");
                strCartOutput.Append("<td width=\"5%\" align=\"center\" class=\"style3\" style=\"height: 20px\"><div align=\"center\"><strong>Sl No</strong></div></td> ");
                strCartOutput.Append("<td width=\"35%\" align=\"center\" class=\"style3\" style=\"height: 20px\"><strong>Item</strong></td> ");
                strCartOutput.Append("<td width=\"10%\" align=\"center\" class=\"style3\" style=\"height: 20px\"><strong>Qty</strong></td>");
                strCartOutput.Append("<td width=\"25%\" align=\"center\" class=\"style3\" style=\"height: 20px\"><strong>Price</strong></td> ");
                strCartOutput.Append("<td width=\"25%\" align=\"left\" class=\"style3\" style=\"height: 20px\"><strong>Net Price</strong></td> ");
                strCartOutput.Append("</tr>");

                foreach (DataRow dRow in dtShoppingCart.Rows)
                {
                    intCount++;
                    if (intCount == 1)
                    {
                        blDiscount = Convert.ToBoolean(dRow["boolDiscount"]);
                        strDiscountCode = Convert.ToString(dRow["discCode"].ToString());
                        dblDiscountValue = Convert.ToDouble(dRow["discValue"]);
                        intDiscountType = Convert.ToInt32(dRow["discType"]);
                    }
                    int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
                    double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
                    double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
                    dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);
                    // Modification to send discounted amount to paypal
                    if ((blDiscount) && (intDiscountType == 1))                 // Fixed discount
                    {
                        dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice - dblDiscountValue);
                        int intCartTotalQnty = 0;
                        if (GetCartTotalQnty(dtShoppingCart, ref intCartTotalQnty, ref strError))
                        {
                            //dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice - (dblDiscountValue/intCartTotalQnty));
                            dblRowPrice = Convert.ToDouble(dblRowPrice - Convert.ToDouble(dblDiscountValue / intCartTotalQnty));
                        }
                    }
                    else if ((blDiscount) && (intDiscountType == 2))            //  % discount
                    {
                        //dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                        dblRowPrice = Convert.ToDouble(dblRowPrice - Convert.ToDouble((dblRowPrice * dblDiscountValue) / 100));
                    }

                    string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
                    string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr> ");
                    strCartOutput.Append("<td colspan=\"6\">");
                    strCartOutput.Append("<!--- Cart's Rows Starts--->");
                    strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
                    strCartOutput.Append("<tr class=\"style1\">");
                    strCartOutput.Append("<td width=\"5%\" align=\"center\" valign=\"middle\">");
                    strCartOutput.Append("<div align=\"center\">" + intCount + "</div>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"35%\" class=\"style1\">");
                    strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td width=\"30%\" valign=\"middle\" align=\"center\" class=\"style1\">");

                    strCartOutput.Append("<!--- Product Hyperlink Starts--->");
                    //strCartOutput.Append("<div align=\"center\"><a href=\"Gifts.aspx?proid=" + dRow["prodId"].ToString() + "&CatId=" + dRow["catId"].ToString() + "\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"></a></div>");
                    //strCartOutput.Append("<div align=\"center\"><a href=\"#lightbox" + dRow["prodId"].ToString() + "\" rel=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"lbOn\">");
                    strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"/></a>");
                    strCartOutput.Append("</div>");
                    //strCartOutput.Append("<!--start popup window-->");
                    //strCartOutput.Append("<div id=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"leightbox\">");
                    //strCartOutput.Append("<div class=\"holder\" style=\"width:450px;\">");
                    //strCartOutput.Append("<table id=\"tabPopUp" + dRow["prodId"].ToString() + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" bgcolor=\"#ffffff\"> ");
                    //strCartOutput.Append("<tr>");
                    //strCartOutput.Append("<td width=\"100%\" colspan=\"3\" align=\"center\" class=\"redTxt\"><b>" + dRow["prodName"].ToString() + "</b></td> ");
                    //strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr>");
                    //strCartOutput.Append("<td align=\"center\" valign=\"middle\" class=\"small_red\" colspan=\"3\" width=\"100%\">");
                    //strCartOutput.Append("<div id=\"dvPopUpImg\" style=\"width:100%;\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"350\" height=\"350\" border=\"0\"/></div>");
                    //strCartOutput.Append("</td>");
                    //strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr>");
                    //strCartOutput.Append("<td align=\"center\" class=\"small_red\" width=\"20%\" colspan=\"3\" valign=\"middle\"><a href=\"#\" class=\"lbAction\" rel=\"deactivate\"><img src=\"Pictures/close.jpg\" alt=\"Close\" border=\"0\"/></a> </td> ");
                    //strCartOutput.Append("</tr>");
                    //strCartOutput.Append("</table>");
                    //strCartOutput.Append("</div>");
                    //strCartOutput.Append("</div>");
                    //strCartOutput.Append("<!--end popup window-->");
                    strCartOutput.Append("<!--- Product Hyperlink Ends--->");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"style1\">");
                    strCartOutput.Append(dRow["prodName"].ToString());
                    strCartOutput.Append("<input type=\"hidden\" name=\"item_name_" + intCount + "\" value=\"" + dRow["prodName"].ToString() + "\">");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("</table>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"10%\" class=\"style1\" align=\"center\" valign=\"middle\">");
                    strCartOutput.Append("<div align=\"center\">");
                    strCartOutput.Append(intRowQnty);
                    strCartOutput.Append("</div>");
                    strCartOutput.Append("<input type=\"hidden\" name=\"quantity_" + intCount + "\" value=\"" + intRowQnty + "\">");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"25%\" align=\"center\" valign=\"middle\"> ");
                    strCartOutput.Append("<div title=\"" + dblRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strRowPrice + "</div>");
                    //strCartOutput.Append("<input type=\"hidden\" name=\"amount_" + intCount + "\" value=\"" + dblRowPrice + "\">");
                    // Convert the INR into respective currency 
                    double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, intCurrencyId));
                    //double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, 1));
                    strCartOutput.Append("<input type=\"hidden\" name=\"amount_" + intCount + "\" value=\"" + dblDollarAmt.ToString("######.00") + "\">");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td width=\"25%\" class=\"style1\" align=\"left\" valign=\"middle\" > ");
                    strCartOutput.Append(strTotRowPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("</table>");
                    strCartOutput.Append("<!--- Cart's Rows Ends--->");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                }
                strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
                strCartOutput.Append("<!--- Cart's SubTotal Section Starts--->");
                string strSubTotalPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                strCartOutput.Append("<strong>SubTotal :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"1\" valign=\"top\" align=\"left\" class=\"style1\">");
                strCartOutput.Append(strSubTotalPrice);
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's SubTotal Section Ends--->");
                strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                strCartOutput.Append("<strong>Shipping :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                strCartOutput.Append(" - FREE - ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");

                if (blDiscount)
                {

                    strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Savings Code:&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td width=\"30%\" colspan=\"3\" valign=\"middle\" class=\"style1\">");
                    if (Convert.ToString(HttpContext.Current.Session["discLimit"]) != "0")
                    {
                        double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                        //strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + " (Limit:" + dblLimit + ")</div>");
                        strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                    }
                    else
                    {
                        strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                    }

                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
                    strCartOutput.Append("</table>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
                }
                //else
                //{
                //    strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
                //    strCartOutput.Append("<tr>");
                //    strCartOutput.Append("<td colspan=\"4\" align=\"right\" valign=\"middle\" class=\"style1\">");
                //    strCartOutput.Append("<strong>discount code:&nbsp;&nbsp; </strong>");
                //    strCartOutput.Append("</td>");
                //    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                //    strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                //    strCartOutput.Append("<tr>");
                //    strCartOutput.Append("<td width=\"30%\" valign=\"middle\" class=\"style1\">");
                //    strCartOutput.Append("<div id=\"dvDisTxt\"><input type=\"text\" id=\"txtDiscount\" name=\"txtDiscount\" style=\"width:120px;\" maxlength=\"10\"/></div>");
                //    strCartOutput.Append("</td>");
                //    strCartOutput.Append("<td width=\"1%\" class=\"style1\"></td>");
                //    strCartOutput.Append("<td width=\"69%\" class=\"style1\">");
                //    strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, 'txtDiscount');\"><img src=\"Pictures/but_update.gif\" alt=\"Update\" border=\"0\"></a>");
                //    strCartOutput.Append("</td>");
                //    strCartOutput.Append("</tr>");
                //    //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
                //    strCartOutput.Append("</table>");
                //    strCartOutput.Append("</td>");
                //    strCartOutput.Append("</tr>");
                //    strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
                //}

                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                string strTotalGrandPrice = "";
                if ((blDiscount == true) && (intDiscountType == 1))
                {
                    double dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblDiscountValue);
                    strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalDiscountPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalGrandPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                    dblTotGrandPrice = dblTotalAfterDiscount;
                }
                else if ((blDiscount == true) && (intDiscountType == 2))            //  %
                {
                    double dblTotalAfterDiscount = 0.00;
                    double dblTotDiscPrice = 0.00;
                    if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
                    {
                        double dblCartTotal = 0.00;
                        strError = "";
                        if (GetCartTotal(dtShoppingCart, ref dblCartTotal, ref strError) == false)
                        {
                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                            dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                        }
                        else
                        {
                            dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                            double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                            if (dblTotDiscPrice > dblLimit)
                            {
                                dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblLimit);
                                dblTotDiscPrice = dblLimit;
                            }
                            else
                            {
                                dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                            }
                        }
                    }
                    else
                    {
                        dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                    }

                    strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    string strTotalDiscountPrice = "Rs." + dblTotDiscPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotDiscPrice / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalDiscountPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalGrandPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                    dblTotGrandPrice = dblTotalAfterDiscount;
                }
                else
                {
                    strTotalGrandPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalDiscountPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalGrandPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                }


                strCartOutput.Append("</table>");
                strCartOutput.Append("<!--- The main Cart Table Ends--->");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                strCartOutput.Append("</table>");
                strOutPut = strCartOutput.ToString();
            }
            else
            {
                strOutPut = "Currency retrievation error! Please try again.";
            }
        }
        else
        {
            strOutPut = showNoitem();
        }

        return strOutPut;
    }
    public string printCartHtml(int intSiteId, string strOrderNumber)
    {
        string strOutPut = "";
        StringBuilder strCartOutput = new StringBuilder();
        if (strOrderNumber != "")
        {
            double dblCurrencyValue = 1;
            string strDiscountCode = "";
            int intCurrencyId = 0;
            string strCurrencySymbol = "";
            if (HttpContext.Current.Session["CurrencySymbol"] != null)
            {
                strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
            }
            else
            {
                strCurrencySymbol = "";
            }
            if (HttpContext.Current.Session["CurrencyId"] != null)
            {
                intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
            }
            else
            {
                intCurrencyId = 1;
            }
            if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
            {
                int intCount = 0;
                //double dblTotRowPrice = 0.00;                
                double dblTotGrandPrice = 0.00;
                bool blDiscount = false;
                int intDiscountType = 0;
                double dblDiscountValue = 0.00;

                DataTable dtShoppingCart = (DataTable)HttpContext.Current.Session["dtCart"];
                if (dtShoppingCart.Rows.Count > 0)
                {
                    dtShoppingCart.Rows.Clear();
                }
                if (makeTheCart(intSiteId, strOrderNumber, ref dtShoppingCart, ref strError))
                {
                    strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableborder\"> ");
                    strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr><tr>");
                    strCartOutput.Append("<td align=\"center\" valign=\"middle\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\">");
                    strCartOutput.Append("<b>:: Product Details ::</b>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                    strCartOutput.Append("<tr>");
                    strCartOutput.Append("<td valign=\"top\" align=\"left\">");
                    strCartOutput.Append("<!--- The main Cart Table Starts--->");
                    strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                    strCartOutput.Append("<tr bgcolor=\"#B20000\" class=\"footer-copyright\">");
                    strCartOutput.Append("<td width=\"5%\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><div align=\"center\"><strong>Sl No</strong></div></td> ");
                    strCartOutput.Append("<td width=\"35%\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Item</strong></td> ");
                    strCartOutput.Append("<td width=\"10%\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Qty</strong></td>");
                    strCartOutput.Append("<td width=\"25%\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Price</strong></td> ");
                    strCartOutput.Append("<td width=\"25%\" align=\"left\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\"><strong>Net Price</strong></td> ");
                    strCartOutput.Append("</tr>");

                    foreach (DataRow dRow in dtShoppingCart.Rows)
                    {
                        intCount++;
                        if (intCount == 1)
                        {
                            blDiscount = Convert.ToBoolean(dRow["boolDiscount"]);
                            strDiscountCode = Convert.ToString(dRow["discCode"].ToString());
                            dblDiscountValue = Convert.ToDouble(dRow["discValue"]);
                            intDiscountType = Convert.ToInt32(dRow["discType"]);
                        }
                        int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
                        double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
                        double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
                        dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);

                        string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
                        string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");
                        strCartOutput.Append("<tr> ");
                        strCartOutput.Append("<td colspan=\"6\">");
                        strCartOutput.Append("<!--- Cart's Rows Starts--->");
                        strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
                        strCartOutput.Append("<tr class=\"style1\">");
                        strCartOutput.Append("<td width=\"5%\" align=\"center\" valign=\"middle\">");
                        strCartOutput.Append("<div align=\"center\">" + intCount + "</div>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td width=\"35%\" class=\"style1\">");
                        strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                        strCartOutput.Append("<tr>");
                        strCartOutput.Append("<td width=\"30%\" valign=\"middle\" align=\"center\" class=\"style1\">");

                        strCartOutput.Append("<!--- Product Hyperlink Starts--->");
                        //strCartOutput.Append("<div align=\"center\"><a href=\"Gifts.aspx?proid=" + dRow["prodId"].ToString() + "&CatId=" + dRow["catId"].ToString() + "\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"></a></div>");
                        //strCartOutput.Append("<div align=\"center\"><a href=\"#lightbox" + dRow["prodId"].ToString() + "\" rel=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"lbOn\">");
                        strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"60\" height=\"60\" border=\"0\"/></a>");
                        strCartOutput.Append("</div>");
                        //strCartOutput.Append("<!--start popup window-->");
                        //strCartOutput.Append("<div id=\"lightbox" + dRow["prodId"].ToString() + "\" class=\"leightbox\">");
                        //strCartOutput.Append("<div class=\"holder\" style=\"width:450px;\">");
                        //strCartOutput.Append("<table id=\"tabPopUp" + dRow["prodId"].ToString() + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" bgcolor=\"#ffffff\"> ");
                        //strCartOutput.Append("<tr>");
                        //strCartOutput.Append("<td width=\"100%\" colspan=\"3\" align=\"center\" bgcolor=\"#B20000\" class=\"style3\"><b>" + dRow["prodName"].ToString() + "</b></td> ");
                        //strCartOutput.Append("</tr>");
                        //strCartOutput.Append("<tr>");
                        //strCartOutput.Append("<td align=\"center\" valign=\"middle\" class=\"small_red\" colspan=\"3\" width=\"100%\">");
                        //strCartOutput.Append("<div id=\"dvPopUpImg\" style=\"width:100%;\"><img src=\"" + dRow["prodImage"].ToString() + "\" width=\"350\" height=\"350\" border=\"0\"/></div>");
                        //strCartOutput.Append("</td>");
                        //strCartOutput.Append("</tr>");
                        //strCartOutput.Append("<tr>");
                        //strCartOutput.Append("<td align=\"center\" class=\"small_red\" width=\"20%\" colspan=\"3\" valign=\"middle\"><a href=\"#\" class=\"lbAction\" rel=\"deactivate\"><img src=\"Pictures/close.jpg\" alt=\"Close\" border=\"0\"/></a> </td> ");
                        //strCartOutput.Append("</tr>");
                        //strCartOutput.Append("</table>");
                        //strCartOutput.Append("</div>");
                        //strCartOutput.Append("</div>");
                        //strCartOutput.Append("<!--end popup window-->");
                        strCartOutput.Append("<!--- Product Hyperlink Ends--->");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"style1\">");
                        strCartOutput.Append(dRow["prodName"].ToString());
                        strCartOutput.Append("<input type=\"hidden\" name=\"item_name_" + intCount + "\" value=\"" + dRow["prodName"].ToString() + "\">");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("</table>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td width=\"10%\" class=\"style1\" align=\"center\" valign=\"middle\">");
                        strCartOutput.Append("<div align=\"center\">");
                        strCartOutput.Append(intRowQnty);
                        strCartOutput.Append("</div>");
                        strCartOutput.Append("<input type=\"hidden\" name=\"quantity_" + intCount + "\" value=\"" + intRowQnty + "\">");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td width=\"25%\" align=\"center\" valign=\"middle\"> ");
                        strCartOutput.Append("<div title=\"" + dblRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strRowPrice + "</div>");
                        //strCartOutput.Append("<input type=\"hidden\" name=\"amount_" + intCount + "\" value=\"" + dblRowPrice + "\">");
                        double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, intCurrencyId));
                        strCartOutput.Append("<input type=\"hidden\" name=\"amount_" + intCount + "\" value=\"" + dblDollarAmt.ToString("######.00") + "\">");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td width=\"25%\" class=\"style1\" align=\"left\" valign=\"middle\" > ");
                        strCartOutput.Append(strTotRowPrice);
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("</table>");
                        strCartOutput.Append("<!--- Cart's Rows Ends--->");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                    }
                    strCartOutput.Append("<tr><td colspan=\"6\" class=\"clear10\"></td></tr>");
                    strCartOutput.Append("<!--- Cart's SubTotal Section Starts--->");
                    string strSubTotalPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>SubTotal :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"1\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strSubTotalPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's SubTotal Section Ends--->");
                    strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Shipping :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(" - FREE - ");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");

                    if (blDiscount)
                    {

                        strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
                        strCartOutput.Append("<tr height=\"18px\">");
                        strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                        strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                        strCartOutput.Append("<strong>Savings Code:&nbsp;&nbsp; </strong>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                        strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                        strCartOutput.Append("<tr>");
                        strCartOutput.Append("<td width=\"30%\" colspan=\"3\" valign=\"middle\" class=\"style1\">");
                        if (Convert.ToString(HttpContext.Current.Session["discLimit"]) != "0")
                        {
                            double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                            //strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + " (Limit:" + dblLimit + ")</div>");
                            strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                        }
                        else
                        {
                            strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                        }

                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
                        strCartOutput.Append("</table>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
                    }
                    //else
                    //{
                    //    strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
                    //    strCartOutput.Append("<tr>");
                    //    strCartOutput.Append("<td colspan=\"4\" align=\"right\" valign=\"middle\" class=\"style1\">");
                    //    strCartOutput.Append("<strong>discount code:&nbsp;&nbsp; </strong>");
                    //    strCartOutput.Append("</td>");
                    //    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                    //    strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                    //    strCartOutput.Append("<tr>");
                    //    strCartOutput.Append("<td width=\"30%\" valign=\"middle\" class=\"style1\">");
                    //    strCartOutput.Append("<div id=\"dvDisTxt\"><input type=\"text\" id=\"txtDiscount\" name=\"txtDiscount\" style=\"width:120px;\" maxlength=\"10\"/></div>");
                    //    strCartOutput.Append("</td>");
                    //    strCartOutput.Append("<td width=\"1%\" class=\"style1\"></td>");
                    //    strCartOutput.Append("<td width=\"69%\" class=\"style1\">");
                    //    strCartOutput.Append("&nbsp;&nbsp;<a href=\"#txtDiscount\" onclick=\"updateCartForDiscount(3, 'txtDiscount');\"><img src=\"Pictures/but_update.gif\" alt=\"Update\" border=\"0\"></a>");
                    //    strCartOutput.Append("</td>");
                    //    strCartOutput.Append("</tr>");
                    //    //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
                    //    strCartOutput.Append("</table>");
                    //    strCartOutput.Append("</td>");
                    //    strCartOutput.Append("</tr>");
                    //    strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
                    //}

                    strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                    string strTotalGrandPrice = "";
                    if ((blDiscount == true) && (intDiscountType == 1))
                    {
                        double dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblDiscountValue);
                        strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
                        strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                        string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
                        strCartOutput.Append("<tr height=\"18px\">");
                        strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                        strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                        strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                        strCartOutput.Append(strTotalDiscountPrice);
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                        strCartOutput.Append("<tr height=\"18px\">");
                        strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                        strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                        strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                        strCartOutput.Append(strTotalGrandPrice);
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                        dblTotGrandPrice = dblTotalAfterDiscount;
                    }
                    else if ((blDiscount == true) && (intDiscountType == 2))            //  %
                    {
                        double dblTotalAfterDiscount = 0.00;
                        double dblTotDiscPrice = 0.00;
                        if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
                        {
                            double dblCartTotal = 0.00;
                            strError = "";
                            if (GetCartTotal(dtShoppingCart, ref dblCartTotal, ref strError) == false)
                            {
                                dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                                dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                            }
                            else
                            {
                                dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                                double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                                if (dblTotDiscPrice > dblLimit)
                                {
                                    dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblLimit);
                                    dblTotDiscPrice = dblLimit;
                                }
                                else
                                {
                                    dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                                }
                            }
                        }
                        else
                        {
                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                            dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                        }

                        strTotalGrandPrice = "Rs." + dblTotalAfterDiscount + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotalAfterDiscount / dblCurrencyValue).ToString("0.00");
                        strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                        string strTotalDiscountPrice = "Rs." + dblTotDiscPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotDiscPrice / dblCurrencyValue).ToString("0.00");
                        strCartOutput.Append("<tr height=\"18px\">");
                        strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                        strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                        strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                        strCartOutput.Append(strTotalDiscountPrice);
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                        strCartOutput.Append("<tr height=\"18px\">");
                        strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                        strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                        strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                        strCartOutput.Append(strTotalGrandPrice);
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                        dblTotGrandPrice = dblTotalAfterDiscount;
                    }
                    else
                    {
                        strTotalGrandPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
                        strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                        string strTotalDiscountPrice = "Rs." + dblDiscountValue + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblDiscountValue / dblCurrencyValue).ToString("0.00");
                        strCartOutput.Append("<tr height=\"18px\">");
                        strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                        strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                        strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                        strCartOutput.Append(strTotalDiscountPrice);
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                        strCartOutput.Append("<tr height=\"18px\">");
                        strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                        strCartOutput.Append("<td align=\"left\" valign=\"top\" class=\"style1\">");
                        strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                        strCartOutput.Append(strTotalGrandPrice);
                        strCartOutput.Append("</td>");
                        strCartOutput.Append("</tr>");
                        strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                    }


                    strCartOutput.Append("</table>");
                    strCartOutput.Append("<!--- The main Cart Table Ends--->");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                    strCartOutput.Append("</table>");
                    strOutPut = strCartOutput.ToString();
                }
                else
                {
                    strOutPut = strError;
                }
            }
            else
            {
                strOutPut = "Currency retrievation error! Please try again.";
            }
        }
        else
        {
            strOutPut = showNoitem();
        }

        return strOutPut;

    }
    public bool makeTheCart(int intSiteId, string strOrderNumber, ref DataTable dtCart, ref string strCartError)
    {
        bool blFlag = false;
        strCartError = "";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[SalesDetails_BothWay].[Product_Id], " +
                        "[" + strSchema + "].[SalesDetails_BothWay].[QOS], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[Tax_Percentage] AS [flagDiscount] " +
                    "FROM " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[SalesDetails_BothWay] " +
                    "ON " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[SalesDetails_BothWay].[SBillNo] " +
                    "WHERE " +
                        "([" + strSchema + "].[SalesDetails_BothWay].[SBillNo]='" + strOrderNumber + "')";
            SqlDataAdapter daCart = new SqlDataAdapter(strSql, conn);
            DataTable dtSalesDetails = new DataTable();
            daCart.Fill(dtSalesDetails);

            if (dtSalesDetails.Rows.Count > 0)
            {
                blFlag = true;
                int intCatId = 0;
                foreach (DataRow drCart in dtSalesDetails.Rows)
                {
                    strSql = "SELECT TOP 1 " +
                                "[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id] " +
                            "FROM " +
                                "[" + strSchema + "].[ItemCategoryRelation_Web_Server] " +
                            "WHERE " +
                                "([" + strSchema + "].[ItemCategoryRelation_Web_Server].[Product_Id]= '" + drCart["Product_Id"].ToString() + "');";
                    DataTable dtProdCat = new DataTable();
                    SqlDataAdapter daProdCat = new SqlDataAdapter(strSql, conn);
                    daProdCat.Fill(dtProdCat);

                    if (dtProdCat.Rows.Count > 0)
                    {
                        for (int i = 0; i < 1; i++) // (DataRow drProdCat in dtProdCat.Rows)
                        {
                            intCatId = Convert.ToInt32(dtProdCat.Rows[i]["Category_Id"]);
                        }
                        strError = "";
                        if (!AddRowToCart(intSiteId, ref dtCart, Convert.ToString(drCart["Product_Id"].ToString()), intCatId, Convert.ToInt32(drCart["QOS"]), ref strError))
                        {
                            strCartError = strCartError + strError;
                        }
                    }
                    else
                    {
                        intCatId = 0;
                        strError = "";
                        if (!AddRowToCart(intSiteId, ref dtCart, Convert.ToString(drCart["Product_Id"].ToString()), intCatId, Convert.ToInt32(drCart["QOS"]), ref strError))
                        {
                            strCartError = strCartError + strError;
                        }
                    }
                }
                // Check if the order no has discount, if so modify it.
                orderDetail objOrdDetail = new orderDetail();
                if (objOrdDetail.gotDiscount(strOrderNumber))
                {
                    string strDiscCode = "";
                    int intType = 0;
                    double dblDiscVal = 0.00;
                    double dblDiscGot = 0.00;
                    double dblLimit = 0.00;
                    strError = "";
                    if (objOrdDetail.getDiscountDetail(strOrderNumber, ref strDiscCode, ref dblDiscVal, ref dblDiscGot, ref intType, ref dblLimit, ref strError))
                    {
                        if (!checkAndReturnDiscount(strOrderNumber, ref dtCart, strDiscCode, ref strError))
                        {
                            strCartError = strCartError + "<br/>" + strError;
                            blFlag = false;
                        }
                        else
                        {
                            strCartError = "";
                            blFlag = true;
                        }
                    }
                }

            }
            else
            {
                blFlag = false;
                strCartError = "No product found against this order number.";
            }
        }
        catch (SqlException ex)
        {
            strCartError = ex.Message;
            blFlag = false;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            if (strCartError != "")
            {
                blFlag = false;
            }
            else
            {
                blFlag = true;
            }
        }
        return blFlag;
    }
    public string alsoBought(DataTable _dtCart)
    {
        StringBuilder _strOutput = new StringBuilder();

        try
        {
            string _extensionToShow = Convert.ToString(ConfigurationManager.AppSettings["extToShow"]);
            string strSql = "";
            if (_dtCart.Rows.Count > 0)
            {
                string strProdIds = "";
                string strProdNames = "";
                if (_dtCart.Rows.Count > 1)
                {
                    for (int i = 0; i < _dtCart.Rows.Count; i++)
                    {
                        strProdIds = "'" + Convert.ToString(_dtCart.Rows[i]["prodId"]) + "', " + strProdIds;
                        strProdNames = Convert.ToString(_dtCart.Rows[i]["prodName"]) + ", " + strProdNames;
                    }
                    strProdIds = strProdIds.Substring(0, strProdIds.LastIndexOf(","));
                    strProdNames = strProdNames.Substring(0, strProdNames.LastIndexOf(","));
                }
                else
                {
                    for (int i = 0; i < _dtCart.Rows.Count; i++)
                    {
                        strProdIds = "'" + Convert.ToString(_dtCart.Rows[i]["prodId"]) + "'";
                        strProdNames = Convert.ToString(_dtCart.Rows[i]["prodName"]);
                    }
                }
                if (strProdIds != "")
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    int intSiteId = (int)HttpContext.Current.Application["SiteId"];
                    //strSql = "SELECT " +
                    //            "[" + strSchema + "].[AB_Product_Web_Server].[AB_Count], " +
                    //            "[" + strSchema + "].[AB_Product_Web_Server].[Product_Id_2] AS [Product_Id], " +
                    //            "[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id], " +
                    //            "[" + strSchema + "].[ItemMaster_Server].[Item_Name], " +
                    //            "'" + strSitePath + "/ASP_Img/' + [" + strSchema + "].[ItemMaster_Server].[Product_Id] + '.jpg' AS [Product_Image], " +
                    //            "[" + strSchema + "].[ItemMaster_Server].[Item_Price] " +
                    //        "FROM " +
                    //            "[" + strSchema + "].[ItemMaster_Server] " +
                    //        "INNER JOIN " +
                    //             "[" + strSchema + "].[AB_Product_Web_Server] " +
                    //        "ON " +
                    //            "([" + strSchema + "].[AB_Product_Web_Server].[Product_Id_2]=[" + strSchema + "].[ItemMaster_Server].[Product_Id]) " +
                    //        "INNER JOIN " +
                    //            "[" + strSchema + "].[ItemCategoryRelation_Web_Server] " +
                    //        "ON " +
                    //            "([" + strSchema + "].[ItemMaster_Server].[Product_Id]=[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Product_Id]) " +
                    //        //"INNER JOIN " +
                    //        //    "[" + strSchema + "].[SiteCatgory_Web_Server] " +
                    //        //"ON " +
                    //        //    "([" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id]=[" + strSchema + "].[SiteCatgory_Web_Server].[Category_Id]) " +
                    //        "WHERE " +
                    //            "([" + strSchema + "].[AB_Product_Web_Server].[Product_Id_1] IN (" + strProdIds + ")) " +
                    //        //"AND " +
                    //        //    "([" + strSchema + "].[SiteCatgory_Web_Server].[Record_Status]=1) " +
                    //        //"AND " +
                    //        //    "([" + strSchema + "].[SiteCatgory_Web_Server].[Site_Id]='" + intSiteId + "') " +
                    //        "AND " +
                    //            "([" + strSchema + "].[AB_Product_Web_Server].[AB_Count]>1) " +
                    //        "ORDER BY" +
                    //            "[" + strSchema + "].[AB_Product_Web_Server].[AB_Count] DESC;";
                    strSql = "SELECT " +
                                "[" + strSchema + "].[AB_Product_Web_Server].[AB_Count], " +
                                "[" + strSchema + "].[AB_Product_Web_Server].[Product_Id_2] AS [Product_Id], " +
                                "[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id], " +
                                "[" + strSchema + "].[ItemMaster_Server].[Item_Name], [" + strSchema + "].[ItemMaster_Server].[UrlName],  " +
                                "'" + strSitePath + "/ASP_Img/' + [" + strSchema + "].[ItemMaster_Server].[Product_Id] + '.jpg' AS [Product_Image], " +
                                "[" + strSchema + "].[ItemMaster_Server].[Item_Price],[" + strSchema + "].ItemCategory_Web_Server.RewriteUrlPath  " +
                            "FROM " +
                                "[" + strSchema + "].[ItemMaster_Server] " +
                            "INNER JOIN " +
                                 "[" + strSchema + "].[AB_Product_Web_Server] " +
                            "ON " +
                                "([" + strSchema + "].[AB_Product_Web_Server].[Product_Id_2]=[" + strSchema + "].[ItemMaster_Server].[Product_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ItemCategoryRelation_Web_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ItemMaster_Server].[Product_Id]=[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Product_Id]) " +
                            "INNER JOIN [" + strSchema + "].ItemCategory_Web_Server " +
                            "ON ([" + strSchema + "].ItemCategoryRelation_Web_Server.Category_Id = [" + strSchema + "].ItemCategory_Web_Server.Category_Id) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[SiteCatgory_Web_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id]=[" + strSchema + "].[SiteCatgory_Web_Server].[Category_Id]) " +
                            "WHERE " +
                                "([" + strSchema + "].[AB_Product_Web_Server].[Product_Id_1] IN (" + strProdIds + ")) " +
                            "AND " +
                                "([" + strSchema + "].[ItemCategoryRelation_Web_Server].[Record_Status]=1) " +
                            "AND " +
                                "([" + strSchema + "].[SiteCatgory_Web_Server].[Record_Status]=1) " +
                            "AND " +
                                "([" + strSchema + "].[SiteCatgory_Web_Server].[Site_Id]='" + intSiteId + "') " +
                            "AND " +
                                "([" + strSchema + "].[AB_Product_Web_Server].[AB_Count]>1) " +
                            "ORDER BY" +
                                "[" + strSchema + "].[AB_Product_Web_Server].[AB_Count] DESC;";
                    DataTable _dtAlsoBought = new DataTable();
                    _dtAlsoBought.Columns.Add("slNo", typeof(int));
                    _dtAlsoBought.Columns["slNo"].AutoIncrement = true;
                    _dtAlsoBought.Columns["slNo"].AutoIncrementSeed = 1;
                    _dtAlsoBought.Columns.Add("Product_Id", typeof(string));
                    _dtAlsoBought.Columns.Add("Category_Id", typeof(string));
                    _dtAlsoBought.Columns.Add("Item_Name", typeof(string));
                    _dtAlsoBought.Columns.Add("Product_Image", typeof(string));
                    _dtAlsoBought.Columns.Add("Item_Price", typeof(string));
                    _dtAlsoBought.Columns.Add("UrlName", typeof(string));
                    _dtAlsoBought.Columns.Add("RewriteUrlPath", typeof(string));
                    _dtAlsoBought.PrimaryKey = new System.Data.DataColumn[] { _dtAlsoBought.Columns["slNo"] };
                    // Individual product find starts
                    SqlCommand _cmdAB = new SqlCommand(strSql, conn);
                    SqlDataReader _drAB = _cmdAB.ExecuteReader();
                    if (_drAB.HasRows)
                    {
                        while ((_drAB.Read()) && (_dtAlsoBought.Rows.Count < 4))
                        {
                            if (strProdIds.IndexOf(Convert.ToString(_drAB["Product_Id"])) == -1)
                            {
                                bool blExist = false;
                                foreach (DataRow _drExist in _dtAlsoBought.Rows)
                                {
                                    if (Convert.ToString(_drAB["Product_Id"]) == Convert.ToString(_drExist["Product_Id"]))
                                    {
                                        blExist = true;
                                    }
                                }
                                if (!blExist)
                                {
                                    DataRow _drNew = _dtAlsoBought.NewRow();
                                    _drNew["Product_Id"] = _drAB["Product_Id"].ToString();
                                    _drNew["Category_Id"] = _drAB["Category_Id"].ToString();
                                    _drNew["Item_Name"] = _drAB["Item_Name"].ToString();
                                    _drNew["Product_Image"] = _drAB["Product_Image"].ToString();
                                    _drNew["Item_Price"] = _drAB["Item_Price"].ToString();
                                    _drNew["UrlName"] = _drAB["UrlName"].ToString();
                                    _drNew["RewriteUrlPath"] = _drAB["RewriteUrlPath"].ToString();
                                    _dtAlsoBought.Rows.Add(_drNew);
                                }
                            }
                        }
                    }
                    _drAB.Dispose();
                    _cmdAB.Dispose();
                    int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
                    string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"]);
                    double dblCurrencyValue = 1;
                    if ((_dtAlsoBought.Rows.Count > 0) && (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol)))
                    {
                        _strOutput.Append("<div class=\"cartPick\">");
                        _strOutput.Append("<h2 class=\"redTxt\">:: Customers who have bought (" + strProdNames + ") have also bought ::</h2>");
                        _strOutput.Append("<ul>");
                        string strLink = "";
                        foreach (DataRow _dRow in _dtAlsoBought.Rows)
                        {
                            strLink = strDomain + Convert.ToString(_dRow["RewriteUrlPath"]) + Convert.ToString(_dRow["UrlName"]) + _extensionToShow;
                            // Inner product table starts
                            string strProdPrice = "Rs." + _dRow["Item_Price"].ToString() + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(double.Parse(_dRow["Item_Price"].ToString()) / dblCurrencyValue).ToString("0.00");
                            _strOutput.Append("<li>");
                            // _strOutput.Append("<a href=\"" + strDomain + "/Gifts.aspx?proid=" + _dRow["Product_Id"].ToString() + "&CatId=" + _dRow["Category_Id"].ToString() + "\" title=\"" + _dRow["Item_Name"].ToString() + "\">");
                            _strOutput.Append("<a href=\"" + strLink + "\" title=\"" + _dRow["Item_Name"].ToString() + "\">");
                            _strOutput.Append("<img src=\"" + _dRow["Product_Image"].ToString() + "\" alt=\"" + _dRow["Item_Name"].ToString() + "\" border=\"0\" class=\"thumbImg\" title=\"" + _dRow["Item_Name"].ToString() + "\" />");
                            _strOutput.Append("</a>");
                            _strOutput.Append("<br class=\"clear\">");
                            if (Convert.ToString(_dRow["Item_Name"]).Length > 15)
                            {
                                _strOutput.Append("<span class=\"pickPrice\">" + objCommonFunction.ReturnRemainString(Convert.ToString(_dRow["Item_Name"]), 15) + "...</span>");
                            }
                            else
                            {
                                _strOutput.Append("<span class=\"pickPrice\">" + Convert.ToString(_dRow["Item_Name"]) + "</span>");
                            }
                            _strOutput.Append("<div id=\"ProductPrice\" title=\"" + _dRow["Item_Price"].ToString() + "\">" + strProdPrice + "</div>");
                            //_strOutput.Append("<a href=\"javascript:addToCart(1, '" + _dRow["Product_Id"].ToString() + "', '" + _dRow["Category_Id"].ToString() + "', 1);\" title=\"Add " + _dRow["Item_Name"].ToString() + " to cart.\"><img src=\"Pictures/Add2Cart2.gif\" width=\"110\" height=\"25\" align=\"Add to Cart\" border=\"0\" /></a>");
                            _strOutput.Append("<a href=\"javascript:addToCartNR(this, 2, '" + _dRow["Product_Id"].ToString() + "', '" + _dRow["Category_Id"].ToString() + "', 1);\" title=\"Add " + _dRow["Item_Name"].ToString() + " to cart.\"><img src=\"images/add_cart_sm.gif\" alt=\"Add to Cart\" title=\"Add to Cart\" border=\"0\" class=\"cartBtn\" /></a>");
                            // Inner product table ends
                            _strOutput.Append("</li>");
                        }
                        _strOutput.Append("</ul>");
                        _strOutput.Append("</div>");
                    }
                    else
                    {
                        _strOutput.Remove(0, _strOutput.Length);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            _strOutput.Remove(0, _strOutput.Length);
            _strOutput.Append(ex.Message.ToString());
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return _strOutput.ToString();
    }
    public string alsoBought(int intSiteId, DataTable _dtCart)
    {
        StringBuilder _strOutput = new StringBuilder();
        try
        {
            string strSql = "";
            if (_dtCart.Rows.Count > 0)
            {
                string strProdIds = "";
                string strProdNames = "";
                if (_dtCart.Rows.Count > 1)
                {
                    for (int i = 0; i < _dtCart.Rows.Count; i++)
                    {
                        strProdIds = "'" + Convert.ToString(_dtCart.Rows[i]["prodId"]) + "', " + strProdIds;
                        strProdNames = Convert.ToString(_dtCart.Rows[i]["prodName"]) + ", " + strProdNames;
                    }
                    strProdIds = strProdIds.Substring(0, strProdIds.LastIndexOf(","));
                    strProdNames = strProdNames.Substring(0, strProdNames.LastIndexOf(","));
                }
                else
                {
                    for (int i = 0; i < _dtCart.Rows.Count; i++)
                    {
                        strProdIds = "'" + Convert.ToString(_dtCart.Rows[i]["prodId"]) + "'";
                        strProdNames = Convert.ToString(_dtCart.Rows[i]["prodName"]);
                    }
                }
                if (strProdIds != "")
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    strSql = "SELECT " +
                                "[" + strSchema + "].[AB_Product_Web_Server].[AB_Count], " +
                                "[" + strSchema + "].[AB_Product_Web_Server].[Product_Id_2] AS [Product_Id], " +
                                "[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id], " +
                                "[" + strSchema + "].[ItemMaster_Server].[Item_Name], " +
                                "'" + strSitePath + "/ASP_Img/' + [" + strSchema + "].[ItemMaster_Server].[Product_Id] + '.jpg' AS [Product_Image], " +
                                "[" + strSchema + "].[ItemMaster_Server].[Item_Price] " +
                            "FROM " +
                                "[" + strSchema + "].[ItemMaster_Server] " +
                            "INNER JOIN " +
                                 "[" + strSchema + "].[AB_Product_Web_Server] " +
                            "ON " +
                                "([" + strSchema + "].[AB_Product_Web_Server].[Product_Id_2]=[" + strSchema + "].[ItemMaster_Server].[Product_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ItemCategoryRelation_Web_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ItemMaster_Server].[Product_Id]=[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Product_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[SiteCatgory_Web_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ItemCategoryRelation_Web_Server].[Category_Id]=[" + strSchema + "].[SiteCatgory_Web_Server].[Category_Id]) " +
                            "WHERE " +
                                "([" + strSchema + "].[AB_Product_Web_Server].[Product_Id_1] IN (" + strProdIds + ")) " +
                            "AND " +
                                "([" + strSchema + "].[ItemCategoryRelation_Web_Server].[Record_Status]=1) " +
                            "AND " +
                                "([" + strSchema + "].[SiteCatgory_Web_Server].[Record_Status]=1) " +
                            "AND " +
                                "([" + strSchema + "].[SiteCatgory_Web_Server].[Site_Id]='" + intSiteId + "') " +
                            "AND " +
                                "([" + strSchema + "].[AB_Product_Web_Server].[AB_Count]>1) " +
                            "ORDER BY" +
                                "[" + strSchema + "].[AB_Product_Web_Server].[AB_Count] DESC;";
                    DataTable _dtAlsoBought = new DataTable();
                    _dtAlsoBought.Columns.Add("slNo", typeof(int));
                    _dtAlsoBought.Columns["slNo"].AutoIncrement = true;
                    _dtAlsoBought.Columns["slNo"].AutoIncrementSeed = 1;
                    _dtAlsoBought.Columns.Add("Product_Id", typeof(string));
                    _dtAlsoBought.Columns.Add("Category_Id", typeof(string));
                    _dtAlsoBought.Columns.Add("Item_Name", typeof(string));
                    _dtAlsoBought.Columns.Add("Product_Image", typeof(string));
                    _dtAlsoBought.Columns.Add("Item_Price", typeof(string));
                    _dtAlsoBought.PrimaryKey = new System.Data.DataColumn[] { _dtAlsoBought.Columns["slNo"] };
                    // Individual product find starts
                    SqlCommand _cmdAB = new SqlCommand(strSql, conn);
                    SqlDataReader _drAB = _cmdAB.ExecuteReader();
                    if (_drAB.HasRows)
                    {
                        while ((_drAB.Read()) && (_dtAlsoBought.Rows.Count < 4))
                        {
                            if (strProdIds.IndexOf(Convert.ToString(_drAB["Product_Id"])) == -1)
                            {
                                bool blExist = false;
                                foreach (DataRow _drExist in _dtAlsoBought.Rows)
                                {
                                    if (Convert.ToString(_drAB["Product_Id"]) == Convert.ToString(_drExist["Product_Id"]))
                                    {
                                        blExist = true;
                                    }
                                }
                                if (!blExist)
                                {
                                    DataRow _drNew = _dtAlsoBought.NewRow();
                                    _drNew["Product_Id"] = _drAB["Product_Id"].ToString();
                                    _drNew["Category_Id"] = _drAB["Category_Id"].ToString();
                                    _drNew["Item_Name"] = _drAB["Item_Name"].ToString();
                                    _drNew["Product_Image"] = _drAB["Product_Image"].ToString();
                                    _drNew["Item_Price"] = _drAB["Item_Price"].ToString();
                                    _dtAlsoBought.Rows.Add(_drNew);
                                }
                            }
                        }
                    }
                    _drAB.Dispose();
                    _cmdAB.Dispose();
                    int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
                    string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"]);
                    double dblCurrencyValue = 1;
                    if ((_dtAlsoBought.Rows.Count > 0) && (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol)))
                    {
                        _strOutput.Append("<div class=\"cartPick\">");
                        _strOutput.Append("<h2 class=\"redTxt\">:: Customers who have bought (" + strProdNames + ") have also bought ::</h2>");
                        _strOutput.Append("<ul>");
                        foreach (DataRow _dRow in _dtAlsoBought.Rows)
                        {
                            // Inner product table starts
                            string strProdPrice = "Rs." + _dRow["Item_Price"].ToString() + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(double.Parse(_dRow["Item_Price"].ToString()) / dblCurrencyValue).ToString("0.00");
                            _strOutput.Append("<li>");
                            _strOutput.Append("<a href=\"" + strDomain + "/Gifts.aspx?proid=" + _dRow["Product_Id"].ToString() + "&CatId=" + _dRow["Category_Id"].ToString() + "\" title=\"" + _dRow["Item_Name"].ToString() + "\">");
                            _strOutput.Append("<img src=\"" + _dRow["Product_Image"].ToString() + "\" alt=\"" + _dRow["Item_Name"].ToString() + "\" border=\"0\" class=\"thumbImg\" title=\"" + _dRow["Item_Name"].ToString() + "\" />");
                            _strOutput.Append("</a>");
                            _strOutput.Append("<br class=\"clear\">");
                            if (Convert.ToString(_dRow["Item_Name"]).Length > 15)
                            {
                                _strOutput.Append("<span class=\"pickPrice\">" + objCommonFunction.ReturnRemainString(Convert.ToString(_dRow["Item_Name"]), 15) + "...</span>");
                            }
                            else
                            {
                                _strOutput.Append("<span class=\"pickPrice\">" + Convert.ToString(_dRow["Item_Name"]) + "</span>");
                            }
                            _strOutput.Append("<div id=\"ProductPrice\" title=\"" + _dRow["Item_Price"].ToString() + "\">" + strProdPrice + "</div>");
                            //_strOutput.Append("<a href=\"javascript:addToCart(1, '" + _dRow["Product_Id"].ToString() + "', '" + _dRow["Category_Id"].ToString() + "', 1);\" title=\"Add " + _dRow["Item_Name"].ToString() + " to cart.\"><img src=\"Pictures/Add2Cart2.gif\" width=\"110\" height=\"25\" align=\"Add to Cart\" border=\"0\" /></a>");
                            _strOutput.Append("<a href=\"javascript:addToCartNR(this, 2, '" + _dRow["Product_Id"].ToString() + "', '" + _dRow["Category_Id"].ToString() + "', 1);\" title=\"Add " + _dRow["Item_Name"].ToString() + " to cart.\"><img src=\"images/add_cart_sm.gif\" alt=\"Add to Cart\" title=\"Add to Cart\" border=\"0\" class=\"cartBtn\" /></a>");
                            // Inner product table ends
                            _strOutput.Append("</li>");
                        }
                        _strOutput.Append("</ul>");
                        _strOutput.Append("</div>");
                    }
                    else
                    {
                        _strOutput.Remove(0, _strOutput.Length);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            _strOutput.Remove(0, _strOutput.Length);
            _strOutput.Append(ex.Message.ToString());
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return _strOutput.ToString();
    }

    public string showDetail_24x7(DataTable dtCart)
    {
        string strOutPut = "";
        System.Text.StringBuilder strCartOutput = new System.Text.StringBuilder();
        if (dtCart.Rows.Count > 0)
        {
            int intCount = 0;
            //double dblTotRowPrice = 0.00;                
            double dblTotGrandPrice = 0.00;
            bool blDiscount = false;
            int intDiscountType = 0;
            double dblDiscountValue = 0.00;
            string strDiscountCode = "";
            strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableBorder\"> ");
            //strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
            strCartOutput.Append("<tr>");
            strCartOutput.Append("<td valign=\"top\" align=\"left\">");
            strCartOutput.Append("<!--- The main Cart Table Starts--->");
            strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
            strCartOutput.Append("<tr bgcolor=\"#B20000\" class=\"footer-copyright\">");
            strCartOutput.Append("<td width=\"8%\" valign=\"middle\" align=\"left\" class=\"redTxt\" style=\"height: 20px\"><strong>Sl No</strong></td> ");
            strCartOutput.Append("<td width=\"40%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Item</strong></td> ");
            strCartOutput.Append("<td width=\"7%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Qty</strong></td>");
            strCartOutput.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Price</strong></td> ");
            strCartOutput.Append("<td width=\"25%\" valign=\"middle\" align=\"center\" class=\"redTxt\" style=\"height: 20px\"><strong>Net Price</strong></td> ");
            strCartOutput.Append("</tr>");

            foreach (DataRow dRow in dtCart.Rows)
            {
                intCount++;
                if (intCount == 1)
                {
                    blDiscount = Convert.ToBoolean(dRow["boolDiscount"]);
                    strDiscountCode = Convert.ToString(dRow["discCode"].ToString());
                    dblDiscountValue = Convert.ToDouble(dRow["discValue"]);
                    intDiscountType = Convert.ToInt32(dRow["discType"]);
                }
                int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
                double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
                double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
                dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);

                strCartOutput.Append("<tr> ");
                strCartOutput.Append("<td colspan=\"5\">");
                strCartOutput.Append("<!--- Cart's Rows Starts--->");
                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td width=\"8%\" align=\"center\" valign=\"middle\">");
                strCartOutput.Append("<div align=\"center\">" + intCount + "</div>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td width=\"40%\">");
                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td width=\"30%\" valign=\"middle\" align=\"center\" class=\"padd3\">");

                strCartOutput.Append("<!--- Product Hyperlink Starts--->");
                //strCartOutput.Append("<a href=\"" + dRow["prodImage"].ToString() + "\" rel=\"lightbox\">");
                strCartOutput.Append("<img src=\"" + dRow["prodImage"].ToString() + "\" width=\"40\" height=\"40\" border=\"0\"/>");
                //strCartOutput.Append("</a>");
                strCartOutput.Append("<!--- Product Hyperlink Ends--->");

                strCartOutput.Append("</td>");
                strCartOutput.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"padd3\">" + dRow["prodName"].ToString() + "</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("</table>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td width=\"7%\" align=\"center\" valign=\"middle\">");
                strCartOutput.Append("<div align=\"center\">" + (intRowQnty) + "</div>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td width=\"20%\" align=\"right\" valign=\"middle\"> ");
                strCartOutput.Append("<div title=\"" + dblRowPrice + "\" align=\"right\" id=\"ProductPrice\">Rs." + dblRowPrice.ToString("0.00") + "</div>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td width=\"25%\" align=\"right\" valign=\"middle\" > ");
                strCartOutput.Append("<div title=\"" + dblTotRowPrice + "\" align=\"right\" id=\"ProductPrice\">Rs." + dblTotRowPrice.ToString("0.00") + "</div>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("</table>");
                strCartOutput.Append("<!--- Cart's Rows Ends--->");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
            }
            strCartOutput.Append("<tr><td colspan=\"5\" class=\"clear10\"></td></tr>");

            strCartOutput.Append("<!--- Cart's SubTotal Section Starts--->");
            strCartOutput.Append("<tr height=\"18px\">");
            strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
            strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
            strCartOutput.Append("<strong>SubTotal&nbsp;&nbsp;:</strong>");
            strCartOutput.Append("</td>");
            strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"right\">");
            strCartOutput.Append("<div align=\"right\" id=\"ProductSubPrice\">");
            strCartOutput.Append("<div align=\"right\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\">Rs." + dblTotGrandPrice.ToString("0.00") + "</div> ");
            strCartOutput.Append("</div>");
            strCartOutput.Append("</td>");
            strCartOutput.Append("</tr>");
            strCartOutput.Append("<!--- Cart's SubTotal Section Ends--->");
            strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");
            strCartOutput.Append("<tr height=\"18px\">");
            strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
            strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
            strCartOutput.Append("<strong>Shipping :&nbsp;&nbsp; </strong>");
            strCartOutput.Append("</td>");
            strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"right\">");
            strCartOutput.Append(" - FREE - ");
            strCartOutput.Append("</td>");
            strCartOutput.Append("</tr>");
            strCartOutput.Append("<!--- Cart's Shipping Section Starts--->");

            if (blDiscount)
            {

                strCartOutput.Append("<!--- Cart's Discount Code Section Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>Savings Code::&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\">");
                strCartOutput.Append("<table id=\"tabDiscount\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td width=\"30%\" colspan=\"3\" valign=\"middle\">");
                if (Convert.ToString(HttpContext.Current.Session["discLimit"]) != "0")
                {
                    double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                    //strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + " (Limit:" + dblLimit + ")</div>");                        
                    strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                }
                else
                {
                    strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + "</div>");
                }

                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                //strCartOutput.Append("<tr><td colspan=\"3\" class=\"style8\"><div id=\"dvDiscErr\" style=\"width:100%;height:18px;\"></div></td></tr>");
                strCartOutput.Append("</table>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Discount Code Section Ends--->");
            }

            strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
            string strTotalGrandPrice = "";
            if ((blDiscount) && (intDiscountType == 1))
            {
                double dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblDiscountValue);
                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"mailDisc\">");
                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"middle\" align=\"right\" class=\"mailDisc\">");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >Rs." + dblDiscountValue.ToString("0.00") + " (" + dblDiscountValue + ")</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"right\">");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\">Rs." + dblTotalAfterDiscount.ToString("0.00") + "</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                dblTotGrandPrice = dblTotalAfterDiscount;
            }
            else if ((blDiscount) && (intDiscountType == 2))            //  %
            {
                double dblTotalAfterDiscount = 0.00;
                double dblTotDiscPrice = 0.00;
                if (Convert.ToString(HttpContext.Current.Session["discLimit"].ToString()) != "0")
                {
                    double dblCartTotal = 0.00;
                    strError = "";
                    if (!GetCartTotal(dtCart, ref dblCartTotal, ref strError))
                    {
                        dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                    }
                    else
                    {
                        dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                        double dblLimit = Convert.ToDouble(HttpContext.Current.Session["discLimit"]);
                        if (dblTotDiscPrice > dblLimit)
                        {
                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - dblLimit);
                            dblTotDiscPrice = dblLimit;
                        }
                        else
                        {
                            dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                        }
                    }
                }
                else
                {
                    dblTotalAfterDiscount = Convert.ToDouble(dblTotGrandPrice - Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100));
                    dblTotDiscPrice = Convert.ToDouble((dblTotGrandPrice * dblDiscountValue) / 100);
                }

                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"mailDisc\">");
                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"middle\" align=\"right\" class=\"mailDisc\">");
                //strCartOutput.Append("<div align=\"left\" id=\"ProductGrandPrice\">");
                //strCartOutput.Append("</div>");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblTotDiscPrice + "\" id=\"ProductPrice\" >Rs." + dblTotDiscPrice.ToString("0.00") + " (" + dblDiscountValue + "%)</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"right\">");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >Rs." + dblTotalAfterDiscount.ToString("0.00") + "</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                dblTotGrandPrice = dblTotalAfterDiscount;
            }
            else if ((blDiscount) && (intDiscountType == 3))
            {
                strError = "";
                int discPercentage = 0;
                bool disc = false;
                double dblTotalAfterDiscount = dblTotGrandPrice;
                checkAndReturnDiscount(dblTotGrandPrice, ref disc, ref dblTotalAfterDiscount, ref discPercentage, ref strError);
                dblDiscountValue = (dblTotGrandPrice * discPercentage) / 100;
                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\" class=\"mailDisc\">");
                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"middle\" align=\"right\" class=\"mailDisc\">");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >Rs." + dblDiscountValue.ToString("0.00") + " (" + discPercentage + "%)</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"right\">");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblTotalAfterDiscount + "\" id=\"ProductPrice\" >Rs." + dblTotalAfterDiscount.ToString("0.00") + "</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
                dblTotGrandPrice = dblTotalAfterDiscount;
            }
            else
            {
                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"right\">");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblDiscountValue + "\" id=\"ProductPrice\" >Rs." + dblDiscountValue.ToString("0.00") + "</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                strCartOutput.Append("<tr height=\"18px\">");
                strCartOutput.Append("<td colspan=\"3\" align=\"right\" valign=\"middle\">&nbsp;</td>");
                strCartOutput.Append("<td align=\"left\" valign=\"middle\">");
                strCartOutput.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"right\">");
                strCartOutput.Append("<div align=\"right\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\" >Rs." + dblTotGrandPrice.ToString("0.00") + "</div> ");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<!--- Cart's Grand Total Section Starts--->");
            }


            strCartOutput.Append("</table>");
            strCartOutput.Append("<!--- The main Cart Table Ends--->");
            strCartOutput.Append("</td>");
            strCartOutput.Append("</tr>");
            strCartOutput.Append("</table>");
            strOutPut = strCartOutput.ToString();
        }
        else
        {
            strOutPut = showNoitem(false);
        }

        return strOutPut;
    }


    public string showNoitem(bool ContinueShopping)
    {
        string strOutPut = "";
        System.Text.StringBuilder strCartOutput = new System.Text.StringBuilder();
        strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"> ");
        strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strCartOutput.Append("<tr>");
        strCartOutput.Append("<td valign=\"top\" align=\"center\">");
        if (ContinueShopping)
        {
            strCartOutput.Append("There are no items in your cart. Please continue shopping by clicking on the 'continue shopping' button below.");
        }
        else
        {
            strCartOutput.Append("There are no items in your cart. Please go back and reshop.");
        }
        strCartOutput.Append("</td>");
        strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        if (ContinueShopping)
        {
            strCartOutput.Append("<tr align=\"center\" valign=\"middle\">");
            strCartOutput.Append("<td valign=\"middle\" align=\"center\">");
            strCartOutput.Append("<div align=\"center\"><a href=\"" + strDomain + "\"><img src=\"images/continue_shop_btn.gif\" border=\"0\" /></a></div>");
            strCartOutput.Append("</td>");
            strCartOutput.Append("</tr>");
        }
        strCartOutput.Append("</table>");
        strOutPut = strCartOutput.ToString();
        HttpContext.Current.Session["CartPrice"] = 0;
        HttpContext.Current.Session["CartQuantity"] = 0;
        return Convert.ToString(strOutPut);
    }

}