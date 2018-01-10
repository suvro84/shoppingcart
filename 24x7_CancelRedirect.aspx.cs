using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Data;
using System.Configuration;

public partial class _24x7_CancelRedirect : System.Web.UI.Page
{
    Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
    string strSchema = ConfigurationManager.AppSettings["Schema"].ToString();
    string strSql = "";
    string strError = "";
    cartFunctions objCartFunc = new cartFunctions();
    public string strPgId = "";
    public string strNewPgId = "";
    public string strPoId = "";
    public string strNewPoId = "";
    public string strSiteId = "";
    public string strFlag = "";
    public string strPgName = "";
    public string strPgNameToShow = "";
    public string strPgChargeName = "";
    public string strPoName = "";
    public string strCommon = "";
    public string strpageheaderimage = "";
    public string strRedirectBaseDomain = "";
    public string strGenSitename = "";
    public string strpageCSS = "";
    public decimal grndtotal = 0, grnddis = 0, grndpay = 0, grndordercount = 1;
    System.Collections.Hashtable htUserDetails = new Hashtable();
    public int countSbill = 0;
    public ArrayList arr = new ArrayList();
    public string strshippingDetails = "";
    string flag = "0";
    public string strbody = "";
    public int intcountid = 1;

    public decimal decConvertedCurr = 0;
    public string strConvertedCurrSym = "";
    clsRakhi objRakhiHd = new clsRakhi();
    public string strPoPgRank = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        htUserDetails = (Hashtable)Session["userDetails"];
        string[] arrConvreCurr = null;

        objRakhiHd.functionforImageHead(Convert.ToInt32(htUserDetails["siteId"]), ref strpageCSS, ref strpageheaderimage, ref strRedirectBaseDomain, ref strGenSitename);
        strGenSitename = objRakhiHd.SentenceCase(strGenSitename.Trim());
        if ((Convert.ToString(htUserDetails["siteId"]) == "132") || (Convert.ToString(htUserDetails["siteId"]) == "154") || (Convert.ToString(htUserDetails["siteId"]) == "1"))      // If its giftbhejo, ndtv.giftstoindia24x7 or giftstoindia24x7 dont show the payment message
        {
            topMsg.Visible = false;
        }
        else
        {
            topMsg.Visible = true;
        }
        //============checking combo or sbill===================
        if (htUserDetails.Contains("comboid"))
        {
            strCommon = Convert.ToString(htUserDetails["comboid"].ToString());
            //get converted curr
            DataTable dtCombo = fnGrtOrderCombo(strCommon);
            arrConvreCurr = fnConvertedCurr(dtCombo.Rows[0]["SBillNo"].ToString()).Split(new char[] { '^' });
        }
        if (htUserDetails.Contains("ordNo"))
        {
            strCommon = Convert.ToString(htUserDetails["ordNo"].ToString());
            //get converted curr
            arrConvreCurr = fnConvertedCurr(strCommon).Split(new char[] { '^' });
        }
        if (arrConvreCurr.Length > 0)
        {
            decConvertedCurr = Convert.ToDecimal(arrConvreCurr[0]);
            strConvertedCurrSym = arrConvreCurr[1].ToString().Trim();
        }
        //====================================
        if (Request.QueryString["siteId"] != null)
        {
            strSiteId = Convert.ToString(Request.QueryString["siteId"].ToString());
            if (objCommonFunction.IsNumeric(strSiteId) == false)
            {
                strSiteId = "1";
            }
        }
        if (Request.QueryString["pgId"] != null)
        {
            strPgId = Convert.ToString(Request.QueryString["pgId"].ToString());
            if (objCommonFunction.IsNumeric(strPgId) == false)
            {
                strPgId = "";
            }
        }
        if (Request.QueryString["poId"] != null)
        {
            strPoId = Convert.ToString(Request.QueryString["poId"].ToString());
            if (objCommonFunction.IsNumeric(strPoId) == false)
            {
                strPoId = "";
            }
        }
        if (Request.QueryString["flag"] != null)
        {
            strFlag = Convert.ToString(Request.QueryString["flag"].ToString());
            if (objCommonFunction.IsNumeric(strFlag) == false)
            {
                strFlag = "";
            }
        }

        if (Request.QueryString["PoPgRank"] != null)
        {
            strPoPgRank = Convert.ToString(Request.QueryString["PoPgRank"].ToString());
            if (objCommonFunction.IsNumeric(strPoPgRank) == false)
            {
                strPoPgRank = "";
            }
        }
        if ((strPgId != "") && (strPoId != "") && (strFlag != "") && (!Page.IsPostBack))
        {
            if ((Session["dtCart"] != null) && (Session["userDetails"] != null))
            {
                System.Data.DataTable dtCart = (DataTable)Session["dtCart"];
                System.Collections.Hashtable htUserDetail = (Hashtable)Session["userDetails"];
                orderDetail objOrdDetail = new orderDetail();
                if (dtCart.Rows.Count > 0)
                {
                    if (htUserDetails.Count > 0)
                    {
                        int intCounter = 0;
                        if (objOrdDetail.checkCancelReturn(htUserDetail["ordNo"].ToString(), strPgId, strPoId, ref intCounter, ref strError))
                        {
                            if (intCounter > 0)
                            {
                                if (htUserDetail.Contains("ordNoPg"))
                                {
                                    htUserDetail.Remove("ordNoPg");
                                    htUserDetail.Add("ordNoPg", htUserDetail["ordNo"].ToString() + "-" + intCounter);
                                }
                                else
                                {
                                    htUserDetail.Add("ordNoPg", htUserDetail["ordNo"].ToString() + "-" + intCounter);
                                }
                            }
                        }
                        switch (strFlag)
                        {
                            case "1":               // Redirect to Same gateway
                                strSql = "SELECT " +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[ID] AS [GatewayId], " +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[Name] AS [GatewayName], " +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[GatewayNameToShow] AS [GatewayNametoShow], " +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[ChargeNameToShow] AS [ChargeName], " +
                                            "[" + strSchema + "].[Payment_Option_Master].[ID] AS [PoId], " +
                                            "[" + strSchema + "].[Payment_Option_Master].[Name] AS [PoName], " +
                                            "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank], " +
                                            "[" + strSchema + "].[Payment_Option_Master].[Description] " +
                                        "FROM " +
                                            "[" + strSchema + "].[Payment_Option_Master] " +
                                        "INNER JOIN " +
                                            "[" + strSchema + "].[Payment_Option_Gateway_Relation] " +
                                        "ON " +
                                            "([" + strSchema + "].[Payment_Option_Gateway_Relation].[PoId]=[" + strSchema + "].[Payment_Option_Master].[ID]) " +
                                        "INNER JOIN " +
                                            "[" + strSchema + "].[Payment_Gateway_Master] " +
                                        "ON " +
                                            "([" + strSchema + "].[Payment_Option_Gateway_Relation].[PGId]=[" + strSchema + "].[Payment_Gateway_Master].[ID]) " +
                                        "WHERE " +
                                            "([" + strSchema + "].[Payment_Gateway_Master].[ID]=" + strPgId + ") " +
                                        "AND " +
                                            "([" + strSchema + "].[Payment_Option_Master].[ID]=" + strPoId + ")" +
                                        " AND " +
                                            "([" + strSchema + "].[Payment_Option_Master].[Record_Status]=1) " +
                                        "AND " +
                                            "([" + strSchema + "].[Payment_Gateway_Master].[Record_Status]=1) " +
                                        " ORDER BY " +
                                            "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank]";

                                strError = "";
                                //if (getGatewayName(strSql, ref strPgName, ref strPgChargeName, ref strError) == true)
                                //{
                                //    rightPanel.InnerHtml = printForm(strPgName, dtCart, htUserDetails);                                    
                                //}
                                if (getGatewayDetails(strSql, ref strNewPgId, ref strPgName, ref strPgNameToShow, ref strPgChargeName, ref strNewPoId, ref strPoName, ref strError))
                                {
                                    strError = "";
                                    string strOrderNo;
                                    if (strCommon.Substring(0, 1).Equals("C"))
                                    {
                                        DataTable dtCombo = fnGrtOrderCombo(strCommon);
                                        strOrderNo = dtCombo.Rows[0]["SBillNo"].ToString();
                                    }
                                    else
                                    {
                                        strOrderNo = strCommon;
                                    }
                                    if (!checkSamePoPg(strOrderNo, strNewPgId, strNewPoId, ref strError))
                                    {
                                        rightPanel.InnerHtml = strError;
                                    }
                                    else
                                    {
                                        if (!htUserDetails.Contains("poId"))
                                        {
                                            htUserDetails.Add("poId", strNewPoId);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("poId");
                                            htUserDetails.Add("poId", strNewPoId);
                                        }

                                        if (!htUserDetails.Contains("poName"))
                                        {
                                            htUserDetails.Add("poName", strPoName);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("poName");
                                            htUserDetails.Add("poName", strPoName);
                                        }

                                        if (!htUserDetails.Contains("pgId"))
                                        {
                                            htUserDetails.Add("pgId", strNewPgId);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgId");
                                            htUserDetails.Add("pgId", strNewPgId);
                                        }

                                        if (!htUserDetails.Contains("pgName"))
                                        {
                                            htUserDetails.Add("pgName", strPgName);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgName");
                                            htUserDetails.Add("pgName", strPgName);
                                        }

                                        if (!htUserDetails.Contains("pgNametoShow"))
                                        {
                                            htUserDetails.Add("pgNametoShow", strPgNameToShow);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgNametoShow");
                                            htUserDetails.Add("pgNametoShow", strPgNameToShow);
                                        }

                                        if (!htUserDetails.Contains("pgChargeName"))
                                        {
                                            htUserDetails.Add("pgChargeName", strPgChargeName);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgChargeName");
                                            htUserDetails.Add("pgChargeName", strPgChargeName);
                                        }
                                        if (!htUserDetails.Contains("flagWaitMail"))
                                        {
                                            strError = "";
                                            if (!htUserDetails.Contains("comboid"))
                                            {
                                                if (sendWaitMail(htUserDetails, dtCart, ref strError))
                                                {
                                                    if (!htUserDetails.Contains("flagWaitMail"))
                                                    {
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                    else
                                                    {
                                                        htUserDetails.Remove("flagWaitMail");
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mailWaitRakhi(strCommon, 0, Convert.ToString(htUserDetails["siteName"]));
                                                if (!htUserDetails.Contains("flagWaitMail"))
                                                {
                                                    htUserDetails.Add("flagWaitMail", 1);
                                                }
                                                else
                                                {
                                                    htUserDetails.Remove("flagWaitMail");
                                                    htUserDetails.Add("flagWaitMail", 1);
                                                }
                                            }
                                            rightPanel.InnerHtml = printForm(strPgName, dtCart, htUserDetails);
                                        }
                                    }
                                }
                                else
                                {
                                    //rightPanel.InnerHtml = strError;
                                    string strSiteName = "";
                                    if (objCommonFunction.returnSiteName(Convert.ToInt32(strSiteId), ref strSiteName) == false)
                                    {
                                        rightPanel.InnerHtml = strError + "<br/>Error on retrieving site name. Please press refresh button.";
                                    }
                                    else
                                    {
                                        rightPanel.InnerHtml = strError + "<br/><a href=\"http://www." + strSiteName + "\">Click Here</a> to go back.";
                                    }
                                }
                                break;
                            case "2":                   // Redirect to next available gateway
                                strSql = "SELECT TOP 1" +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[ID] AS [GatewayId], " +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[Name] AS [GatewayName], " +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[GatewayNameToShow] AS [GatewayNametoShow], " +
                                            "[" + strSchema + "].[Payment_Gateway_Master].[ChargeNameToShow] AS [ChargeName], " +
                                            "[" + strSchema + "].[Payment_Option_Master].[ID] AS [PoId], " +
                                            "[" + strSchema + "].[Payment_Option_Master].[Name] AS [PoName] , " +
                                            "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank], " +
                                            "[" + strSchema + "].[Payment_Option_Master].[Description] " +
                                        "FROM " +
                                            "[" + strSchema + "].[Payment_Option_Master] " +
                                        "INNER JOIN " +
                                            "[" + strSchema + "].[Payment_Option_Gateway_Relation] " +
                                        "ON " +
                                            "([" + strSchema + "].[Payment_Option_Gateway_Relation].[PoId]=[" + strSchema + "].[Payment_Option_Master].[ID]) " +
                                        "INNER JOIN " +
                                            "[" + strSchema + "].[Payment_Gateway_Master] " +
                                        "ON " +
                                            "([" + strSchema + "].[Payment_Option_Gateway_Relation].[PGId]=[" + strSchema + "].[Payment_Gateway_Master].[ID]) " +
                                        "WHERE " +
                                            "([" + strSchema + "].[Payment_Gateway_Master].[ID]!=" + strPgId + ") " +
                                        "AND " +
                                            "([" + strSchema + "].[Payment_Option_Master].[ID]=" + strPoId + ")" +
                                        " AND " +
                                            "([" + strSchema + "].[Payment_Option_Master].[Record_Status]=1) " +
                                        "AND " +
                                            "([" + strSchema + "].[Payment_Gateway_Master].[Record_Status]=1) " +
                                         " and [rgcards_gti24x7].[Payment_Option_Gateway_Relation].[Rank] >" + strPoPgRank + " " +
                                        " ORDER BY " +
                                            "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank]";

                                strError = "";
                                if (getGatewayDetails(strSql, ref strNewPgId, ref strPgName, ref strPgNameToShow, ref strPgChargeName, ref strNewPoId, ref strPoName, ref strError))
                                {
                                    strError = "";
                                    string strOrderNo;
                                    //if (strPgName == "PayPal")
                                    //{
                                    //    strPoName = "PayPal";
                                    //    strNewPoId = "3";
                                    //    strNewPgId = "1";
                                    //}
                                    //else if (strPgName == "ccavenue")
                                    //{
                                    //    strPoName = "Visa";
                                    //    strNewPoId = "1";
                                    //    strNewPgId = "4";
                                    //}
                                    if (strCommon.Substring(0, 1).Equals("C"))
                                    {
                                        DataTable dtCombo = fnGrtOrderCombo(strCommon);
                                        strOrderNo = dtCombo.Rows[0]["SBillNo"].ToString();
                                    }
                                    else
                                    {
                                        strOrderNo = strCommon;
                                    }
                                    if (!checkSamePoPg(strOrderNo, strNewPgId, strNewPoId, ref strError))
                                    {
                                        rightPanel.InnerHtml = strError;
                                    }
                                    else
                                    {
                                        if (!htUserDetails.Contains("poId"))
                                        {
                                            htUserDetails.Add("poId", strNewPoId);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("poId");
                                            htUserDetails.Add("poId", strNewPoId);
                                        }

                                        if (!htUserDetails.Contains("poName"))
                                        {
                                            htUserDetails.Add("poName", strPoName);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("poName");
                                            htUserDetails.Add("poName", strPoName);
                                        }

                                        if (!htUserDetails.Contains("pgId"))
                                        {
                                            htUserDetails.Add("pgId", strNewPgId);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgId");
                                            htUserDetails.Add("pgId", strNewPgId);
                                        }

                                        if (!htUserDetails.Contains("pgName"))
                                        {
                                            htUserDetails.Add("pgName", strPgName);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgName");
                                            htUserDetails.Add("pgName", strPgName);
                                        }

                                        if (!htUserDetails.Contains("pgNametoShow"))
                                        {
                                            htUserDetails.Add("pgNametoShow", strPgNameToShow);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgNametoShow");
                                            htUserDetails.Add("pgNametoShow", strPgNameToShow);
                                        }

                                        if (!htUserDetails.Contains("pgChargeName"))
                                        {
                                            htUserDetails.Add("pgChargeName", strPgChargeName);
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("pgChargeName");
                                            htUserDetails.Add("pgChargeName", strPgChargeName);
                                        }

                                        //rightPanel.InnerHtml = printForm(strPgName, dtCart, htUserDetails);
                                        if (!htUserDetails.Contains("flagWaitMail"))
                                        {
                                            strError = "";
                                            if (!htUserDetails.Contains("comboid"))
                                            {
                                                //if (sendWaitMail(htUserDetails, dtCart, ref strError) == true)
                                                if (sendWaitMail(false, strCommon, ref strError))
                                                {
                                                    if (!htUserDetails.Contains("flagWaitMail"))
                                                    {
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                    else
                                                    {
                                                        htUserDetails.Remove("flagWaitMail");
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //mailWaitRakhi(strCommon, 0, Convert.ToString(htUserDetails["siteName"]));
                                                if (sendWaitMail(true, strCommon, ref strError))
                                                {
                                                    if (!htUserDetails.Contains("flagWaitMail"))
                                                    {
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                    else
                                                    {
                                                        htUserDetails.Remove("flagWaitMail");
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                }
                                            }
                                            rightPanel.InnerHtml = printForm(strPgName, dtCart, htUserDetails);
                                        }
                                        else
                                        {
                                            rightPanel.InnerHtml = printForm(strPgName, dtCart, htUserDetails);
                                        }
                                    }

                                }
                                else
                                {
                                    //rightPanel.InnerHtml = strError;
                                    string strSiteName = "";
                                    if (objCommonFunction.returnSiteName(Convert.ToInt32(strSiteId), ref strSiteName) == false)
                                    {
                                        rightPanel.InnerHtml = strError + "<br/>Error on retrieving site name. Please press refresh button.";
                                    }
                                    else
                                    {
                                        rightPanel.InnerHtml = strError + "<br/><a href=\"http://www." + strSiteName + "\">Click Here</a> to go back.";
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Response.Redirect("cart.aspx");
                    }
                }
                else
                {
                    Response.Redirect("cart.aspx");
                }
            }
            else
            {
                Response.Redirect("cart.aspx");
            }
        }
        else
        {
            rightPanel.InnerHtml = "There is some error. Cannot continue.";
        }
    }

    protected bool getGatewayDetails(string strSqlToExecute, ref string strPaymentGatewayId, ref string strPaymentGatewayName, ref string strPaymentGatewayNametoShow, ref string strPaymentGatewayChargeName, ref string strPaymentOptionId, ref string strPaymentOptionName, ref string strPgError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdPg = new SqlCommand(strSql, conn);
            SqlDataReader drPg = cmdPg.ExecuteReader(CommandBehavior.CloseConnection);
            if (drPg.HasRows)
            {
                blFlag = true;
                if (drPg.Read())
                {
                    strPaymentGatewayId = Convert.ToString(drPg["GatewayId"].ToString());
                    strPaymentGatewayName = Convert.ToString(drPg["GatewayName"].ToString());
                    strPaymentGatewayNametoShow = Convert.ToString(drPg["GatewayNametoShow"].ToString());
                    strPaymentGatewayChargeName = Convert.ToString(drPg["ChargeName"].ToString());
                    strPaymentOptionId = Convert.ToString(drPg["PoId"].ToString());
                    strPaymentOptionName = Convert.ToString(drPg["PoName"].ToString());
                }
            }
            else
            {
                blFlag = false;
                strPgError = "No available paymentgateway found. Please go back to home and reshop.";
            }

        }
        catch (SqlException ex)
        {
            blFlag = false;
            strPgError = ex.Message;
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
    private string printForm(string strOrderId, DataTable dtCart, Hashtable htUserDetails)
    {
        string strOutput = "";
        double dblTotal = 0.00;
        StringBuilder sbFormOutput = new StringBuilder();
        try
        {
            string strPgName = htUserDetails["pgName"].ToString();
            switch (strPgName)
            {
                case "PayPal":           // PAYPAL
                    //sbFormOutput.Append("<form id=\"frmPaypal\" method=\"post\" action=\"https://www.paypal.com/cgi-bin/webscr\">");
                    sbFormOutput.Append("<form id=\"frmPaypal\" method=\"post\" action=\"https://www.sandbox.paypal.com/cgi-bin/webscr\">");
                    sbFormOutput.Append("<div class=\"payment\"> ");
                    sbFormOutput.Append("<h4>Thank you for placing your order with us. </h4>");
                    sbFormOutput.Append("<p class=\"orderTxt\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our Payment Gateway - " + htUserDetails["pgNametoShow"].ToString() + " / " + htUserDetails["pgChargeName"].ToString() + "");
                    //sbFormOutput.Append("Rakhi24x7.com is a part of GiftsToIndia24x7.com network.</p>");
                    sbFormOutput.Append("" + objRakhiHd.SentenceCase(htUserDetails["siteName"].ToString().Trim()) + " is a part of GiftsToIndia24x7.com network of website.  Your credit card / paypal account / Bank account will show a charge in the name of GiftsToIndia24x7.com.</p>");
                    sbFormOutput.Append("<br class=\"clear\" />");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Starts -->");
                    //Passthrough variable you can use to identify your invoice number for this purchase. Default: no variable is passed back to you. Optional
                    sbFormOutput.Append("<input type=\"hidden\" id=\"invoice\" name=\"invoice\" value=\"" + strCommon + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"business\" name=\"business\" value=\"sales@giftstoindia24x7.com\">");

                    // The currency of the payment. Defines the currency in which the monetary variables 
                    //  (amount, shipping, shipping2, handling, tax) are denoted.
                    //  Default: all monetary fields are interpreted as U.S. Dollars. Optional 3
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"currency_code\" name=\"currency_code\" value=\"USD\">");
                    //changed by me
                    // sbFormOutput.Append("<input type=\"hidden\" id=\"currency_code\" name=\"currency_code\" value=\"" + objCommonFunction.ReturnRemainString(objCommonFunction.FetchCurrencyName(Convert.ToInt32(Session["CurrencyId"])), 3) + "\">");


                    //The URL to which the payer’s browser is redirected after completing the payment; 
                    //  for example, a URL on your site that displays a “Thank you for your payment” page.
                    //  Default: payer is redirected to a PayPal web page. Optional                
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"return\" name=\"return\" value=\"http://www.giftstoindia24x7.com/o_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&billing_country=" + htUserDetails["billCountryName"].ToString() + "&bankId=" + htUserDetails["pgId"].ToString() + "&bankname=" + htUserDetails["pgName"].ToString() + "&image_path=http://www." + htUserDetails["siteName"].ToString() + "&sitename=" + htUserDetails["siteName"].ToString() + "\"> ");
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"return\" name=\"return\" value=\"http://test.giftstoindia24x7.com/o_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");
                    sbFormOutput.Append("<input type=\"hidden\" id=\"return\" name=\"return\" value=\"http://www.giftstoindia24x7.com/24x7_Success.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");
                    // sbFormOutput.Append("<input type=\"hidden\" id=\"return\" name=\"return\" value=\"http://giftstoindia24x7.reliable.local/24x7_Success.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");



                    //cancel_return
                    //  A URL to which the payer’s browser is redirected if payment is cancelled; 
                    //  for example, a URL on your website that displays a “Payment Canceled” page.
                    //  Default: Browser is redirected to a PayPal web page.

                    //sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://www.giftstoindia24x7.com/c_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&billing_country=" + htUserDetails["billCountryName"].ToString() + "&bankId=" + htUserDetails["pgId"].ToString() + "&bankname=" + htUserDetails["pgName"].ToString() + "&image_path=http://www." + htUserDetails["siteName"].ToString() + "&sitename=" + htUserDetails["siteName"].ToString() + "\"> ");
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://test.giftstoindia24x7.com/c_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");
                    sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://www.giftstoindia24x7.com/24x7_Fail.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");
                    // sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://giftstoindia24x7.reliable.local/24x7_Fail.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");


                    // Prompt buyer for shipping address. Allowed values are:
                    //  0: (default) buyer is prompted to include a shipping address.
                    //  1: buyer is not asked for a shipping address
                    //  2: buyer must provide a shipping address
                    //Optional
                    sbFormOutput.Append("<input type=\"hidden\" id=\"no_shipping\" name=\"no_shipping\" value=\"1\" >");
                    // The URL of the 150x50-pixel image displayed as your logo in the upper left corner of PayPal’s pages.
                    //  Default: your business name (if you have a business account) or your email address (if you have premier account). Optional
                    //  Temporararily Closed
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"image_url\" name=\"image_url\" value=\"http://www." + htUserDetails["siteName"].ToString() + "/Pictures/GiftstoIndia24x7-Logo-L1.jpg\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"first_name\" name=\"first_name\" value=\"" + htUserDetails["Billing_FName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"last_name\" name=\"last_name\" value=\"" + htUserDetails["Billing_LName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"address1\" name=\"address1\" value=\"" + htUserDetails["Billing_Address1"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"address2\" name=\"address2\" value=\"" + htUserDetails["Billing_Address2"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"email\" name=\"email\" value=\"" + htUserDetails["Billing_Email"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"city\" name=\"city\" value=\"" + htUserDetails["Billing_City"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"state\" name=\"state\" value=\"" + htUserDetails["Billing_StateName"].ToString() + "\" >");
                    //changed by me
                    // sbFormOutput.Append("<input type=\"hidden\" id=\"country\" name=\"country\" value=\"" + objCommonFunction.returnCountryPrefix(htUserDetails["Billing_CountryName"].ToString()) + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"zip\" name=\"zip\" value=\"" + htUserDetails["Billing_PinCode"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"night_phone_b\" name=\"night_phone_b\" value=\"" + htUserDetails["Billing_Mobile"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"day_phone_b\" name=\"day_phone_b\" value=\"" + htUserDetails["Billing_PhNo"].ToString() + "\" > ");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"merchant_return_link\" name=\"merchant_return_link\" value=\"" + strCommon + "\">");

                    // For cart & product & amount
                    //  Add an item to the PayPal Shopping Cart. This variable must be set as follows: 
                    //  add=1
                    //  The alternative is the display=1 variable, which displays the contents of the PayPal Shopping Cart to the buyer.
                    //  If both add and display are specified, display takes precedence.    1
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"add\" name=\"add\" value=\"1\" >");
                    sbFormOutput.Append("<input type=\"hidden\" id=\"upload\" name=\"upload\" value=\"1\" >");

                    //sbFormOutput.Append("<input type=\"hidden\" id=\"cmd\" name=\"cmd\" value=\"_ext-enter\" > ");
                    sbFormOutput.Append("<input type=\"hidden\" id=\"cmd\" name=\"cmd\" value=\"_cart\" > ");
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"redirect_cmd\" name=\"redirect_cmd\" value=\"_xclick\" >");
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"item_name\" name=\"item_name\" value=\"Gifts Purchased at " + htUserDetails["siteName"].ToString() + ".  Order Details will be emailed to you separately.\">");
                    //double dblDollarAmt = 0;
                    //dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(Convert.ToDouble(htUserDetails["ordTotal"].ToString()), Convert.ToInt32(Session["CurrencyId"])));

                    //sbFormOutput.Append("<input type=\"hidden\" id=\"amount\" name=\"amount\" value=\"" + dblDollarAmt.ToString("######.00") + "\" >");

                    ////TABLE 10.1 Allowed Values for cmd Variable
                    ////Value of cmd Description
                    ////_xclick For Buy Now buttons
                    ////_cart For shopping cart buttons; use these additional variables to specify the specific
                    ////shopping cart action of the button:
                    ////  add="1" for Add to Cart buttons used with the PayPal Shopping Cart
                    ////  display="1" for View Cart buttons used with the PayPal Shopping Cart
                    ////  upload="1" for third party shopping carts that upload to PayPal
                    ////  _donations For Donation buttons
                    //sbFormOutput.Append("<!-- newly added --><input type=\"hidden\" name=\"cmd\" value=\"_cart\">");               
                    //// Indicates the use of third party shopping cart
                    //sbFormOutput.Append("<input type=\"hidden\" name=\"upload\" value=\"1\">");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Ends -->");
                    sbFormOutput.Append("</td>");
                    sbFormOutput.Append("</tr>");
                    sbFormOutput.Append("<tr class=\"style1\">");
                    sbFormOutput.Append("<td class=\"style1\" align=\"center\" valign=\"middle\">");
                    // the submit (Make Payment button)
                    //sbFormOutput.Append("<input type=\"submit\" value=\"Make Payment\" style=\"width:120px;\" onClick=\"return sendWaitMail('" + htUserDetails["pgName"].ToString() + "', 'dvAjaxPic');\"/>");
                    //sbFormOutput.Append("<input type=\"submit\" value=\"Make Payment\" style=\"width:120px;\" />");
                    ////sbFormOutput.Append("<input type=\"image\" src=\"Pictures/make_payment.gif\" style=\"width:140px; \" value=\"Make Payment\" alt=\"Make Payment\" tabindex=\"1\"/>");
                    sbFormOutput.Append("<input type=\"submit\" class=\"paymentBtn\" title=\"Make Payment\" value=\"\" /><br class=\"clear\" />");

                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr><tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                    //sbFormOutput.Append("<tr>");
                    // sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    //sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    //=================================
                    if (htUserDetails.Contains("comboid"))
                    {
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetComboDetails(strCommon, ref coun, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Combined order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of Individual order :</span>" + coun + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        //===========end=====================
                        sbFormOutput.Append(functionforOredDetailForCOMBO(strCommon));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        //flag = "1";
                        //sbFormOutput.Append(functionforOrderDetails(strCommon));
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetSingleDetails(strCommon, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of order :</span>1</li>");
                        //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        //===========end=====================
                        sbFormOutput.Append(functionforOrderDetails(strCommon));
                        sbFormOutput.Append("</tr></table>");
                        //// sbFormOutput.Append(printShipBillHtml(htUserDetails));
                    }
                    //==============end
                    // sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");

                    //sbFormOutput.Append("<tr>");
                    //sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    sbFormOutput.Append("<!--- The main Cart Table Starts--->");

                    //=================================
                    if (htUserDetails.Contains("comboid"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                        ////sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                    }
                    //==============end

                    sbFormOutput.Append("<!--- The main Cart Table Ends--->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");

                    //sbFormOutput.Append("</table>");
                    sbFormOutput.Append("</form>");
                    strOutput = sbFormOutput.ToString();
                    break;

                case "EBS":           // VISA

                    sbFormOutput.Append("<form id=\"frmEBS\" method=\"post\" action=\"https://www.ebs.in/pg/www/ma/transaction/register/\">");
                    sbFormOutput.Append("<div class=\"payment\"> ");
                    sbFormOutput.Append("<h4>Thank you for placing your order with us. </h4>");
                    sbFormOutput.Append("<p class=\"orderTxt\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our Payment Gateway - " + htUserDetails["pgNametoShow"].ToString() + " / " + htUserDetails["pgChargeName"].ToString() + "");
                    sbFormOutput.Append("" + objRakhiHd.SentenceCase(htUserDetails["siteName"].ToString().Trim()) + " is a part of GiftsToIndia24x7.com network of website.  Your credit card / paypal account / Bank account will show a charge in the name of GiftsToIndia24x7.com.</p>");

                    sbFormOutput.Append("<br class=\"clear\" />");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Starts -->");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_name]\" name=\"description[billing_name]\" value=\"" + htUserDetails["Billing_FName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_name_2]\" name=\"description[billing_name_2]\" value=\"" + htUserDetails["Billing_LName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_address]\" name=\"description[billing_address]\" value=\"" + htUserDetails["Billing_Address1"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_address_2]\" name=\"description[billing_address_2]\" value=\"" + htUserDetails["Billing_Address2"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"action\" name=\"action\" value=\"insert\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"transaction[usr_id]\" name=\"transaction[usr_id]\" value=\"78\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[currency]\" name=\"description[currency]\" value=\"INR\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[currency_value]\" name=\"description[currency_value]\" value=\"1.00\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[return_url]\" name=\"description[return_url]\" value=\"http://www.giftstoindia24x7.com/24x7_response.asp?DR=${DR}\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[test]\" name=\"description[test]\" value=\"N\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_city]\" name=\"description[billing_city]\" value=\"" + htUserDetails["Billing_City"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_state]\" name=\"description[billing_state]\" value=\"" + htUserDetails["Billing_StateName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_country]\" name=\"description[billing_country]\" value=\"" + htUserDetails["Billing_CountryName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_telephone]\" name=\"description[billing_telephone]\" value=\"" + htUserDetails["Billing_PhNo"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[billing_postcode]\" name=\"description[billing_postcode]\" value=\"" + htUserDetails["Billing_PinCode"].ToString() + "\">");


                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_name]\" name=\"description[delivery_name]\" value=\"" + htUserDetails["Shipping_FName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_name_2]\" name=\"description[delivery_name_2]\" value=\"" + htUserDetails["Shipping_LName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_address]\" name=\"description[delivery_address]\" value=\"" + htUserDetails["Shipping_Address1"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_address_2]\" name=\"description[delivery_address_2]\" value=\"" + htUserDetails["Shipping_Address2"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_city]\" name=\"description[delivery_city]\" value=\"" + htUserDetails["Shipping_CityName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_state]\" name=\"description[delivery_state]\" value=\"" + htUserDetails["Shipping_StateName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_country]\" name=\"description[delivery_country]\" value=\"" + htUserDetails["Shipping_CountryName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_postcode]\" name=\"description[delivery_postcode]\" value=\"" + htUserDetails["Shipping_PinCode"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[delivery_telephone]\" name=\"description[delivery_telephone]\" value=\"" + htUserDetails["Shipping_PhNo"].ToString() + "\" >");


                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[email]\" name=\"description[email]\" value=\"" + htUserDetails["Billing_Email"].ToString() + "\" >");

                    //============total amount=====================
                    if (htUserDetails.Contains("comboid"))
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"transaction[amount]\" name=\"transaction[amount]\" value=\"" + Convert.ToString(fnFinalAmount(strCommon)) + "\" >");
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"transaction[amount]\" name=\"transaction[amount]\" value=\"" + htUserDetails["ordTotal"].ToString() + "\" >");
                    }
                    //==============end
                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[description]\" name=\"description[description]\" value=\"" + strCommon.ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"description[merchant_txn_number]\" name=\"description[merchant_txn_number]\" value=\"" + strCommon.ToString() + "\" >");


                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Ends -->");
                    // the submit (Make Payment button)
                    //////sbFormOutput.Append("<input type=\"image\" src=\"Pictures/make_payment.gif\" style=\"width:140px; \" value=\"Make Payment\" alt=\"Make Payment\" tabindex=\"1\"/>");
                    sbFormOutput.Append("<input type=\"image\" class=\"paymentBtn\" title=\"Make Payment\" /><br class=\"clear\" />");



                    //
                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    //sbFormOutput.Append(printShipBillHtml(htUserDetails));
                    //=================================
                    if (htUserDetails.Contains("comboid"))
                    {
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetComboDetails(strCommon, ref coun, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Combined order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of Individual order :</span>" + coun + "</li>");
                        //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        //===========end=====================
                        sbFormOutput.Append(functionforOredDetailForCOMBO(strCommon));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        //flag = "1";
                        //sbFormOutput.Append(functionforOrderDetails(strCommon));
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetSingleDetails(strCommon, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of order :</span>1</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        //===========end=====================
                        sbFormOutput.Append(functionforOrderDetails(strCommon));
                        sbFormOutput.Append("</tr></table>");
                        //// sbFormOutput.Append(printShipBillHtml(htUserDetails));
                    }
                    //==============end
                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    ////sbFormOutput.Append("</td>");
                    ////sbFormOutput.Append("</tr>");
                    ////sbFormOutput.Append("<tr>");
                    ////sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                    //=================================
                    if (htUserDetails.Contains("comboid"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                        ////sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                    }
                    //==============end
                    //sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                    sbFormOutput.Append("<!--- The main Cart Table Ends--->");
                    ////sbFormOutput.Append("</td>");
                    ////sbFormOutput.Append("</tr>");
                    ////sbFormOutput.Append("</table>");
                    sbFormOutput.Append("</form>");
                    strOutput = sbFormOutput.ToString();
                    break;

                case "Eazy2Pay":           // AMERICAN EXPRESS.
                    //sbFormOutput.Append("<form id=\"frmEBS\" method=\"get\" onSubmit=\"javascript:if(sendWaitMail('" + htUserDetails["pgName"].ToString() + "', 'dvAjaxPic')==false) return false;\" action=\"https://www.eazy2pay.com/processing/merchantrequestidbi.asp?mrfnbr=$mer_id&flagtest=1\">");
                    sbFormOutput.Append("<form id=\"frmEazy2Pay\" method=\"get\" action=\"https://www.eazy2pay.com/processing/merchantrequestidbi.asp?mrfnbr=628315&flagtest=1\">");
                    sbFormOutput.Append("<div class=\"payment\"> ");
                    sbFormOutput.Append("<h4>Thank you for placing your order with us. </h4>");
                    sbFormOutput.Append("<p class=\"orderTxt\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our Payment Gateway - " + htUserDetails["pgNametoShow"].ToString() + " / " + htUserDetails["pgChargeName"].ToString() + "");
                    sbFormOutput.Append("" + objRakhiHd.SentenceCase(htUserDetails["siteName"].ToString().Trim()) + " is a part of GiftsToIndia24x7.com network of website.  Your credit card / paypal account / Bank account will show a charge in the name of GiftsToIndia24x7.com.</p>");

                    sbFormOutput.Append("<br class=\"clear\" />");
                    //sbFormOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#ffffff\"> ");
                    //sbFormOutput.Append("<tr class=\"style8\"><td align=\"center\" valign=\"middle\" class=\"style8\" style=\"height:20px\">Thank you for placing your order with us.</td></tr>");
                    //sbFormOutput.Append("<tr>");
                    //sbFormOutput.Append("<td  align=\"center\" valign=\"top\" class=\"style4\">");
                    //sbFormOutput.Append("<div align=\"justify\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our ");
                    //sbFormOutput.Append("Payment Gateway - <b>" + htUserDetails["pgNametoShow"].ToString() + "</b><br/>");
                    //sbFormOutput.Append("<b>" + htUserDetails["pgChargeName"].ToString() + "</b>");
                    //sbFormOutput.Append("</div>");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");
                    //sbFormOutput.Append("<tr class=\"clear10\">");
                    //sbFormOutput.Append("<td class=\"clear10\">");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Starts -->");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"mrfnbr\" name=\"mrfnbr\" value=\"628315\">");

                    //sbFormOutput.Append("<input type=\"hidden\" name=\"Amount\" value=\"" + htUserDetails["ordTotal"].ToString() + "\">");

                    //============total amount=====================
                    if (htUserDetails.Contains("comboid"))
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"Amount\" name=\"Amount\" value=\"" + Convert.ToString(fnFinalAmount(strCommon)) + "\">");
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"Amount\" name=\"Amount\" value=\"" + htUserDetails["ordTotal"].ToString() + "\">");
                    }
                    //==============end



                    sbFormOutput.Append("<input type=\"hidden\" id=\"succurl\" name=\"succurl\" value=\"http://www.giftstoindia24x7.com/24x7_Success.aspx?sbillno=" + strCommon.ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"failurl\" name=\"failurl\" value=\"http://www.giftstoindia24x7.com/24x7_Fail.aspx?sbillno=" + strCommon.ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"reliablegifts\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"mordnbr\" name=\"mordnbr\" value=\"" + strCommon.ToString() + "\">");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Ends -->");

                    // the submit (Make Payment button)
                    // sbFormOutput.Append("<input type=\"image\" src=\"Pictures/make_payment.gif\" style=\"width:140px; \" value=\"Make Payment\" alt=\"Make Payment\" tabindex=\"1\"/>");
                    sbFormOutput.Append("<input type=\"image\" class=\"paymentBtn\" alt=\"Make Payment\" title=\"Make Payment\" /></a><br class=\"clear\" />");


                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    // sbFormOutput.Append(printShipBillHtml(htUserDetails));
                    //=================================
                    if (htUserDetails.Contains("comboid"))
                    {
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetComboDetails(strCommon, ref coun, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Combined order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of Individual order :</span>" + coun + "</li>");
                        //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        //===========end=====================
                        sbFormOutput.Append(functionforOredDetailForCOMBO(strCommon));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        //flag = "1";
                        //sbFormOutput.Append(functionforOrderDetails(strCommon));
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetSingleDetails(strCommon, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of order :</span>1</li>");
                        //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        //===========end=====================
                        sbFormOutput.Append(functionforOrderDetails(strCommon));
                        sbFormOutput.Append("</tr></table>");
                        ////sbFormOutput.Append(printShipBillHtml(htUserDetails));
                    }
                    //==============end
                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    ////sbFormOutput.Append("</td>");
                    ////sbFormOutput.Append("</tr>");
                    ////sbFormOutput.Append("<tr>");
                    ////sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                    //=================================
                    if (htUserDetails.Contains("comboid"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                        ////sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                    }
                    //==============end
                    //sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                    sbFormOutput.Append("<!--- The main Cart Table Ends--->");
                    ////sbFormOutput.Append("</td>");
                    ////sbFormOutput.Append("</tr>");
                    ////sbFormOutput.Append("</table>");
                    sbFormOutput.Append("</form>");
                    strOutput = sbFormOutput.ToString();
                    break;
                case "ccavenue":           // ccavenue
                    //sbFormOutput.Append("<form id=\"frmEBS\" method=\"post\" onSubmit=\"javascript:if(sendWaitMail('" + htUserDetails["pgName"].ToString() + "', 'dvAjaxPic')==false) return false;\" action=\"https://www.ebs.in/pg/www/ma/transaction/register/\">");
                    sbFormOutput.Append("<form id=\"frmCCAvenue\" method=\"post\" action=\"https://www.ccavenue.com/shopzone/cc_details.jsp\">");

                    sbFormOutput.Append("<div class=\"payment\"> ");
                    sbFormOutput.Append("<h4>Thank you for placing your order with us. </h4>");
                    sbFormOutput.Append("<p class=\"orderTxt\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our Payment Gateway - " + htUserDetails["pgNametoShow"].ToString() + " / " + htUserDetails["pgChargeName"].ToString() + "");
                    sbFormOutput.Append("" + objRakhiHd.SentenceCase(htUserDetails["siteName"].ToString().Trim()) + " is a part of GiftsToIndia24x7.com network of website.  Your credit card / paypal account / Bank account will show a charge in the name of GiftsToIndia24x7.com.</p>");

                    sbFormOutput.Append("<br class=\"clear\" />");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Starts -->");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_name\" name=\"billing_cust_name\" value=\"" + htUserDetails["Billing_Name"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_address\" name=\"billing_cust_address\" value=\"" + htUserDetails["Billing_Address1"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_city\" name=\"billing_cust_city\" value=\"" + htUserDetails["Billing_City"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_state\" name=\"billing_cust_state\" value=\"" + htUserDetails["Billing_StateName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_country\" name=\"billing_cust_country\" value=\"" + htUserDetails["Billing_CountryName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_tel\" name=\"billing_cust_tel\" value=\"" + htUserDetails["Billing_PhNo"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_zip_code\" name=\"billing_zip_code\" value=\"" + htUserDetails["Billing_PinCode"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_email\" name=\"billing_cust_email\" value=\"" + htUserDetails["Billing_Email"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"billing_cust_notes\" name=\"billing_cust_notes\" value=\"" + htUserDetails["Billing_Instructions"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_cust_name\" name=\"delivery_cust_name\" value=\"" + htUserDetails["Shipping_Name"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_cust_address\" name=\"delivery_cust_address\" value=\"" + htUserDetails["Shipping_Address1"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_cust_city\" name=\"delivery_cust_city\" value=\"" + htUserDetails["Shipping_CityName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_cust_state\" name=\"delivery_cust_state\" value=\"" + htUserDetails["Shipping_StateName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_cust_country\" name=\"delivery_cust_country\" value=\"" + htUserDetails["Shipping_CountryName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_zip_code\" name=\"delivery_zip_code\" value=\"" + htUserDetails["Shipping_PinCode"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_cust_tel\" name=\"delivery_cust_tel\" value=\"" + htUserDetails["Shipping_PhNo"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"delivery_cust_notes\" name=\"delivery_cust_notes\" value=\"" + htUserDetails["Shipping_Msg"].ToString() + "\" >");

                    // libfuncs myUtility = new libfuncs();
                    if (htUserDetails.Contains("ordNoPg"))
                    {
                        // sbFormOutput.Append("<input type=\"hidden\" id=\"Checksum\" name=\"Checksum\" value=\"" + myUtility.getchecksum("M_rgcards_1491", htUserDetails["ordNoPg"].ToString(), htUserDetails["ordTotal"].ToString(), "http://www.rgcards.com/24x7_redirecturl.aspx", "c82kwozdzxqbfco03cnobf0p3tsrdrpe") + "\" >");
                    }
                    else
                    {
                        //  sbFormOutput.Append("<input type=\"hidden\" id=\"Checksum\" name=\"Checksum\" value=\"" + myUtility.getchecksum("M_rgcards_1491", htUserDetails["ordNo"].ToString(), htUserDetails["ordTotal"].ToString(), "http://www.rgcards.com/24x7_redirecturl.aspx", "c82kwozdzxqbfco03cnobf0p3tsrdrpe") + "\" >");
                    }
                    sbFormOutput.Append("<input type=\"hidden\" id=\"Redirect_Url\" name=\"Redirect_Url\" value=\"http://www.rgcards.com/24x7_redirecturl.aspx\">");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"Merchant_Id\" name=\"Merchant_Id\" value=\"M_rgcards_1491\" >");

                    sbFormOutput.Append("<input type=\"hidden\" id=\"Amount\" name=\"Amount\" value=\"" + htUserDetails["ordTotal"].ToString() + "\" >");
                    if (htUserDetails.Contains("ordNoPg"))
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"Order_Id\" name=\"Order_Id\" value=\"" + htUserDetails["ordNoPg"].ToString() + "\" >");
                    }
                    else
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"Order_Id\" name=\"Order_Id\" value=\"" + htUserDetails["ordNo"].ToString() + "\" >");
                    }
                    if (htUserDetails.Contains("ordNoPg"))
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"Merchant_Param\" name=\"Merchant_Param\" value=\"" + htUserDetails["ordNoPg"].ToString() + "\" >");
                    }
                    else
                    {
                        sbFormOutput.Append("<input type=\"hidden\" id=\"Merchant_Param\" name=\"Merchant_Param\" value=\"" + htUserDetails["ordNo"].ToString() + "\" >");
                    }

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Ends -->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");
                    //sbFormOutput.Append("<tr class=\"style1\">");
                    //sbFormOutput.Append("<td class=\"style1\" align=\"center\" valign=\"middle\">");
                    // the submit (Make Payment button)
                    //sbFormOutput.Append("<input type=\"image\" src=\"Pictures/make_payment.gif\" style=\"width:140px; \" value=\"Make Payment\" alt=\"Make Payment\" tabindex=\"1\"/>");
                    sbFormOutput.Append("<input type=\"submit\" class=\"paymentBtn\" title=\"Make Payment\" value=\"\"  /><br class=\"clear\" />");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr><tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                    //sbFormOutput.Append("<tr>");
                    //sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");

                    if (htUserDetails.Contains("comboid"))
                    {
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetComboDetails(strCommon, ref coun, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Combined order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of Individual order :</span>" + coun + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        sbFormOutput.Append(functionforOredDetailForCOMBO(strCommon));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetSingleDetails(strCommon, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of order :</span>1</li>");
                        //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        sbFormOutput.Append(functionforOrderDetails(strCommon));
                        sbFormOutput.Append("</tr></table>");
                    }
                    sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                    if (htUserDetails.Contains("comboid"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                    }
                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");
                    //sbFormOutput.Append("<tr>");
                    //sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                    //sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                    //sbFormOutput.Append("<!--- The main Cart Table Ends--->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");
                    //sbFormOutput.Append("</table>");
                    sbFormOutput.Append("</form>");
                    strOutput = sbFormOutput.ToString();
                    break;
                case "EBS v2.5":           // VISA                    
                    sbFormOutput.Append("<form id=\"frmTransaction\" method=\"post\" action=\"https://secure.ebs.in/pg/ma/sale/pay/\">");
                    sbFormOutput.Append("<div class=\"payment\"> ");
                    sbFormOutput.Append("<h4>Thank you for placing your order with us. </h4>");
                    sbFormOutput.Append("<p class=\"orderTxt\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our Payment Gateway - " + htUserDetails["pgNametoShow"].ToString() + " / " + htUserDetails["pgChargeName"].ToString() + "");
                    sbFormOutput.Append("" + objRakhiHd.SentenceCase(htUserDetails["siteName"].ToString().Trim()) + " is a part of GiftsToIndia24x7.com network of website.  Your credit card / paypal account / Bank account will show a charge in the name of GiftsToIndia24x7.com.</p>");

                    sbFormOutput.Append("<br class=\"clear\" />");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Starts -->");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"account_id\" value=\"5203\" >");

                    //sbFormOutput.Append("<input type=\"hidden\" name=\"reference_no\" value=\"78\" >");
                    sbFormOutput.Append("<input type=\"hidden\" name=\"reference_no\" value=\"" + htUserDetails["ordNo"].ToString().Replace("/", "_") + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"currency\" value=\"INR\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"currency_value\" value=\"1.00\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"return_url\" value=\"http://www.giftstoindia24x7.com/24x7_response_ebs.asp?DR={DR}\">");
                    //sbFormOutput.Append("<input type=\"hidden\" name=\"return_url\" value=\"http://test.giftstoindia24x7.com/response_ebs.asp?DR={DR}\">");                

                    sbFormOutput.Append("<input type=\"hidden\" name=\"mode\" value=\"LIVE\">");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"amount\" value=\"" + htUserDetails["ordTotal"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"description\" value=\"" + htUserDetails["ordNo"].ToString().Replace("/", "_") + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"name\" value=\"" + htUserDetails["Billing_FName"].ToString() + " " + htUserDetails["Billing_LName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"address\" value=\"" + htUserDetails["Billing_Address1"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"city\" value=\"" + htUserDetails["Billing_City"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"state\" value=\"" + htUserDetails["Billing_StateName"].ToString() + "\">");
                    //chnagede by me
                    //sbFormOutput.Append("<input type=\"hidden\" name=\"country\" value=\"" + objCommonFunction.returnCountryPrefix(htUserDetails["Billing_CountryName"].ToString(), 3) + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"phone\" value=\"" + htUserDetails["Billing_PhNo"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"postal_code\" value=\"" + htUserDetails["Billing_PinCode"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"email\" value=\"" + htUserDetails["Billing_Email"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"ship_name\" value=\"" + htUserDetails["Shipping_FName"].ToString() + " " + htUserDetails["Shipping_LName"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"ship_address\" value=\"" + htUserDetails["Shipping_Address1"].ToString() + " " + htUserDetails["Shipping_Address2"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"ship_city\" value=\"" + htUserDetails["Shipping_CityName"].ToString() + "\">");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"ship_state\" value=\"" + htUserDetails["Shipping_StateName"].ToString() + "\">");
                    //changed
                    // sbFormOutput.Append("<input type=\"hidden\" name=\"ship_country\" value=\"" + objCommonFunction.returnCountryPrefix(htUserDetails["Shipping_CountryName"].ToString(), 3) + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"ship_postal_code\" value=\"" + htUserDetails["Shipping_PinCode"].ToString() + "\" >");

                    sbFormOutput.Append("<input type=\"hidden\" name=\"ship_phone\" value=\"" + htUserDetails["Shipping_PhNo"].ToString() + "\" >");

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Ends -->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");
                    //sbFormOutput.Append("<tr class=\"style1\">");
                    //sbFormOutput.Append("<td class=\"style1\" align=\"center\" valign=\"middle\">");
                    // the submit (Make Payment button)                    
                    //sbFormOutput.Append("<input type=\"image\" src=\"Pictures/make_payment.gif\" style=\"width:140px; \" value=\"Make Payment\" alt=\"Make Payment\" tabindex=\"1\"/>");
                    sbFormOutput.Append("<input type=\"submit\" class=\"paymentBtn\" title=\"Make Payment\" value=\"\"  /><br class=\"clear\" />");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr><tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                    //sbFormOutput.Append("<tr>");
                    //sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");

                    if (htUserDetails.Contains("comboid"))
                    {
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetComboDetails(strCommon, ref coun, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Combined order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of Individual order :</span>" + coun + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        sbFormOutput.Append(functionforOredDetailForCOMBO(strCommon));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetSingleDetails(strCommon, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of order :</span>1</li>");
                        //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        sbFormOutput.Append(functionforOrderDetails(strCommon));
                        sbFormOutput.Append("</tr></table>");
                    }
                    sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                    if (htUserDetails.Contains("comboid"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        sbFormOutput.Append(GetCartPaypal(dtCart));
                    }
                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");
                    //sbFormOutput.Append("<tr>");
                    //sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                    //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                    //sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                    //sbFormOutput.Append("<!--- The main Cart Table Ends--->");
                    //sbFormOutput.Append("</td>");
                    //sbFormOutput.Append("</tr>");
                    //sbFormOutput.Append("</table>");
                    sbFormOutput.Append("</form>");
                    strOutput = sbFormOutput.ToString();
                    break;
                case "KlinnK":           // KlinnK
                    sbFormOutput.Append("<form id=\"frmPaypal\" method=\"post\" action=\"https://test.timesofmoney.com/direcpay/secure/dpMerchantParams.jsp\">");

                    // sbFormOutput.Append("<form id=\"frmPaypal\" method=\"post\" action=\" https://test.timesofmoney.com/orbitm/secure/klinnkMerchantLogin.jsp\">");


                    sbFormOutput.Append("<div class=\"payment\"> ");
                    sbFormOutput.Append("<h4>Thank you for placing your order with us. </h4>");
                    sbFormOutput.Append("<p class=\"orderTxt\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our Payment Gateway - " + htUserDetails["pgNametoShow"].ToString() + " / " + htUserDetails["pgChargeName"].ToString() + "");
                    sbFormOutput.Append("" + objRakhiHd.SentenceCase(htUserDetails["siteName"].ToString().Trim()) + " is a part of GiftsToIndia24x7.com network of website.  Your credit card / paypal account / Bank account will show a charge in the name of GiftsToIndia24x7.com.</p>");
                    sbFormOutput.Append("<br class=\"clear\" />");
                    dblTotal = 0.00;
                    if (strCommon.ToLower().Contains("com"))
                    {
                        dblTotal = fnFinalAmount(strCommon);
                    }
                    else
                    {
                        dblTotal = Convert.ToDouble(htUserDetails["ordTotal"]);
                    }


                    string requestparameter = "200911191000001|DOM|IND|INR|" + dblTotal + "|" + htUserDetails["ordNo"].ToString().Replace("/", "_") + "|NULL|http://giftstoindia24x7.reliable.local/24x7_Success.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "|http://giftstoindia24x7.reliable.local/24x7_Fail.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "|TOML|1030-Wallet365 Bank|DD";

                    sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Starts -->");
                    sbFormOutput.Append("<input type=\"hidden\" id=\"requestparameter\" name=\"requestparameter\" value=\"" + requestparameter + "\" >");
                    sbFormOutput.Append("<input type=\"submit\" class=\"paymentBtn\" title=\"Make Payment\" value=\"\"  /><br class=\"clear\" />");

                    sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");

                    if (htUserDetails.Contains("comboid"))
                    {
                        //=======billing details============
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetComboDetails(strCommon, ref coun, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        DateTime dtDOD = Convert.ToDateTime(Convert.ToString(htUserDetails["DOD"]));
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Combined order Id :</span>" + strCommon + "</li>");
                        sbFormOutput.Append("<li><span>No. of Individual order :</span>" + coun + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("<li><span>Delivery Date :</span>" + dtDOD.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        sbFormOutput.Append(functionforOredDetailForCOMBO(strCommon));
                    }
                    if (htUserDetails.Contains("ordNo"))
                    {
                        int coun = 0;
                        double inttotval = 0.00;
                        string strOrdt = "";
                        fngetSingleDetails(strCommon, ref strOrdt, ref inttotval);
                        DateTime dtDos = Convert.ToDateTime(strOrdt);
                        DateTime dtDOD = Convert.ToDateTime(Convert.ToString(htUserDetails["DOD"]));
                        sbFormOutput.Append("<div class=\"billDetail\">");
                        sbFormOutput.Append("<dl class=\"leftTxt\">");
                        sbFormOutput.Append("<dt>Billing Details :</dt>");
                        sbFormOutput.Append("<dd><p>" + htUserDetails["Billing_Name"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Address1"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_StateName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_CountryName"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_PinCode"].ToString() + "</p>");
                        sbFormOutput.Append("<p>" + htUserDetails["Billing_Email"].ToString() + "</p></dd>");
                        sbFormOutput.Append("</dl>");
                        sbFormOutput.Append("<ul>");
                        sbFormOutput.Append("<li><span>Order Id :</span>" + strCommon + "</li>");
                        //sbFormOutput.Append("<li><span>No. of order :</span>1</li>");
                        //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                        sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                        sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                        sbFormOutput.Append("<li><span>Delivery Date :</span>" + dtDOD.ToString("dd MMM yyyy") + "</li>");
                        sbFormOutput.Append("</ul>");
                        sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("<br class=\"clear\" />");
                        sbFormOutput.Append(functionforOrderDetails(strCommon));
                        sbFormOutput.Append("</tr></table>");
                    }




                    sbFormOutput.Append("</form>");
                    strOutput = sbFormOutput.ToString();
                    break;
                default:
                    strOutput = "Payment Gateway not found!";
                    break;
            }
        }
        catch (Exception ex)
        {
            strOutput = ex.Message;
        }
        return strOutput;
    }

    private string printShipBillHtml(Hashtable htUserDetails)
    {
        string strOutPut = "";
        StringBuilder strShipBillOutput = new StringBuilder();
        strShipBillOutput.Append("<table id=\"tabShipBill\" width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
        strShipBillOutput.Append("<tr>");
        strShipBillOutput.Append("<td colspan=\"3\" align=\"center\" valign=\"middle\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\">");
        strShipBillOutput.Append("<b>Order No: " + htUserDetails["ordNo"].ToString() + "</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr class=\"clear10\">");
        strShipBillOutput.Append("<td class=\"clear10\"></td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr>");
        strShipBillOutput.Append("<td width=\"49%\" align=\"center\" valign=\"top\">");
        strShipBillOutput.Append("<!--- The shipping details table Starts--->");
        strShipBillOutput.Append("<table id=\"tabShipping\" width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableborder\"> ");
        strShipBillOutput.Append("<tr bgcolor=\"#cfcfcf\"><td colspan=\"3\" width=\"100%\" align=\"center\" valign=\"middle\" class=\"style8\" style=\"height:18px;\">:: Shipping Details :: </td></tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        if (Convert.ToString(htUserDetails["Shipping_Address2"].ToString()) != "")
        {
            strShipBillOutput.Append("" + htUserDetails["Shipping_Name"].ToString() + " <br/>" + htUserDetails["Shipping_Address1"].ToString() + "<br/>" + htUserDetails["Shipping_Address2"].ToString() + "<br/>" + htUserDetails["Shipping_CityName"].ToString() + " - " + htUserDetails["Shipping_PinCode"].ToString() + "<br/>" + htUserDetails["Shipping_StateName"].ToString() + "<br/>" + htUserDetails["Shipping_CountryName"].ToString() + ".");
        }
        else
        {
            strShipBillOutput.Append("" + htUserDetails["Shipping_Name"].ToString() + " <br/>" + htUserDetails["Shipping_Address1"].ToString() + "<br/>" + htUserDetails["Shipping_CityName"].ToString() + " - " + htUserDetails["Shipping_PinCode"].ToString() + "<br/>" + htUserDetails["Shipping_StateName"].ToString() + "<br/>" + htUserDetails["Shipping_CountryName"].ToString() + ".");
        }
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Phone :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Shipping_PhNo"].ToString());
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        if (Convert.ToString(htUserDetails["Shipping_Mobile"].ToString()) != "")
        {
            strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
            strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
            strShipBillOutput.Append("<b>Mobile :</b>");
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
            strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
            strShipBillOutput.Append(htUserDetails["Shipping_Mobile"].ToString());
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("</tr>");
        }
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Email :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Shipping_Email"].ToString());
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>	");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b> Delivery Date:</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        DateTime dtDod = Convert.ToDateTime(htUserDetails["DOD"].ToString());
        strShipBillOutput.Append(dtDod.ToString("dd MMM yyyy"));
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Message:</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Shipping_Msg"].ToString());
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("</table>");
        strShipBillOutput.Append("<!--- The shipping details table Ends--->");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" align=\"center\" valign=\"top\"></td>");
        strShipBillOutput.Append("<td width=\"49%\" align=\"center\" valign=\"top\">");
        strShipBillOutput.Append("<!--- The billing details table Starts--->");
        strShipBillOutput.Append("<table id=\"tabBilling\" width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableborder\"> ");
        strShipBillOutput.Append("<tr bgcolor=\"#cfcfcf\">");
        strShipBillOutput.Append("<td colspan=\"3\" width=\"100%\" align=\"center\" valign=\"middle\" class=\"style8\" style=\"height:18px;\"> :: Billing Details :: </td> ");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\"> </td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td> ");
        strShipBillOutput.Append("<td width=\"74%\" colspan=\"3\" class=\"greyTxt3\" align=\"left\" valign=\"top\"> ");
        if (Convert.ToString(htUserDetails["Billing_Address2"].ToString()) != "")
        {
            strShipBillOutput.Append(htUserDetails["Billing_Name"].ToString() + "<br/>" + htUserDetails["Billing_Address1"].ToString() + "<br/>" + htUserDetails["Billing_Address2"].ToString() + "<br/>" + htUserDetails["Billing_City"].ToString() + " - " + htUserDetails["Billing_PinCode"].ToString() + "<br/>" + htUserDetails["Billing_StateName"].ToString() + "<br/>" + htUserDetails["Billing_CountryName"].ToString() + ".");
        }
        else
        {
            strShipBillOutput.Append(htUserDetails["Billing_Name"].ToString() + "<br/>" + htUserDetails["Billing_Address1"].ToString() + "<br/>" + htUserDetails["Billing_City"].ToString() + " - " + htUserDetails["Billing_PinCode"].ToString() + "<br/>" + htUserDetails["Billing_StateName"].ToString() + "<br/>" + htUserDetails["Billing_CountryName"].ToString() + ".");
        }
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Phone :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Billing_PhNo"].ToString());
        strShipBillOutput.Append("</td>");
        if (Convert.ToString(htUserDetails["Billing_Mobile"].ToString()) != "")
        {
            strShipBillOutput.Append("</tr>");
            strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
            strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
            strShipBillOutput.Append("<b>Mobile :</b>");
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
            strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
            strShipBillOutput.Append(htUserDetails["Billing_Mobile"].ToString());
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("</tr>");
        }
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Email :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Billing_Email"].ToString());
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\"> ");
        strShipBillOutput.Append("<td width=\"25%\" class=\"greyTxt3\" align=\"right\" valign=\"middle\"> <b>Spl. Instruction :</b> </td> ");
        strShipBillOutput.Append("<td width=\"1%\" class=\"greyTxt3\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"greyTxt3\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Billing_Instructions"].ToString());
        strShipBillOutput.Append("</td> ");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("</table>");
        strShipBillOutput.Append("<!--- The shipping details table Starts--->");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("</table>");

        strOutPut = strShipBillOutput.ToString();
        return strOutPut;
    }
    private string printCartHtml(DataTable dtShoppingCart)
    {
        string strOutPut = "";
        StringBuilder strCartOutput = new StringBuilder();
        if (dtShoppingCart.Rows.Count > 0)
        {
            double dblCurrencyValue = 1;
            string strDiscountCode = "";
            string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
            int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
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
                strCartOutput.Append("<td align=\"center\" valign=\"middle\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\">");
                strCartOutput.Append("<b>:: Product Details ::</b>");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td valign=\"top\" align=\"left\">");
                strCartOutput.Append("<!--- The main Cart Table Starts--->");
                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                strCartOutput.Append("<tr bgcolor=\"#FF6600\" class=\"footer-copyright\">");
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
                    strCartOutput.Append("<tr class=\"body-text-dark12\"> ");
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
                    // double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, Convert.ToInt32(Session["CurrencyId"])));
                    //changed by Me
                    double dblDollarAmt = 0.00;


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
                strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
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
                strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
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
                        strCartOutput.Append("<div id=\"dvDisTxt\">" + strDiscountCode + " (Limit:" + dblLimit + ")</div>");
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
                    strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalDiscountPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
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
                        if (objCartFunc.GetCartTotal(dtShoppingCart, ref dblCartTotal, ref strError) == false)
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
                    strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalDiscountPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");
                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
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
                    strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
                    strCartOutput.Append("<strong>Savings :&nbsp;&nbsp; </strong>");
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("<td colspan=\"4\" valign=\"top\" align=\"left\" class=\"style1\">");
                    strCartOutput.Append(strTotalDiscountPrice);
                    strCartOutput.Append("</td>");
                    strCartOutput.Append("</tr>");
                    strCartOutput.Append("<!--- Cart's Discount Price Starts--->");

                    strCartOutput.Append("<tr height=\"18px\">");
                    strCartOutput.Append("<td colspan=\"3\" align=\"left\" valign=\"top\" class=\"style1\">&nbsp;</td>");
                    strCartOutput.Append("<td align=\"right\" valign=\"top\" class=\"style1\">");
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
            strOutPut = objCartFunc.showNoitem();
        }

        return strOutPut;

    }
    protected bool checkSamePoPg(string strOrderNumber, string strPaymentGatewayId, string strPaymentOptionId, ref string strPoPgError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdSamePoPg = conn.CreateCommand();
            cmdSamePoPg.CommandType = CommandType.StoredProcedure;
            cmdSamePoPg.CommandText = "[" + strSchema + "].[checkSamePoPg]";

            SqlParameter paramOrderNo = cmdSamePoPg.Parameters.Add(new SqlParameter("@strSBillNo", SqlDbType.VarChar));
            paramOrderNo.Direction = ParameterDirection.Input;
            paramOrderNo.Value = strOrderNumber;

            SqlParameter paramPgId = cmdSamePoPg.Parameters.Add(new SqlParameter("@intPgId", SqlDbType.Int));
            paramPgId.Direction = ParameterDirection.Input;
            paramPgId.Value = strPaymentGatewayId;

            SqlParameter paramPoId = cmdSamePoPg.Parameters.Add(new SqlParameter("@intPoId", SqlDbType.Int));
            paramPoId.Direction = ParameterDirection.Input;
            paramPoId.Value = strPaymentOptionId;

            cmdSamePoPg.ExecuteNonQuery();
            blFlag = true;

        }
        catch (SqlException ex)
        {
            blFlag = false;
            strPoPgError = ex.Message;
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
    protected bool sendWaitMail(Hashtable htUserDetails, DataTable dtCart, ref string strMailError)
    {
        bool blFlag = false;
        cartFunctions objCartFunc = new cartFunctions();
        if (dtCart.Rows.Count > 0)
        {
            if (htUserDetails.Count > 0)
            {
                //string strSubject = "Order No.# " + strOrderNumber + " - " + Session["OtherSiteName"].ToString() + " - " + strBankName + " ( Wait )";

                string[] strCondition = new string[1];
                strCondition[0] = htUserDetails["ordNo"].ToString();

                //MailOnlyClass objMailOnlyClass = new MailOnlyClass();
                //objMailOnlyClass.ConnectString = Application["ConnectionString"].ToString();

                string strBody = "";
                string strSubject = "";
                string strRecipient = "tagBillingDetails.Billing_Email";
                string strSiteMailId = "sales@tagSales.SiteName";
                //string strMailFormat = "";
                string strSenderName = strSiteMailId;
                string strFrom = "tagSales.SiteName";
                //string strSiteMailId = "sales@" + htUserDetails["siteName"].ToString() + "";

                //changed
                //if (objCommonFunction.getMailFomat(1, strCondition, ref strSiteMailId, ref strRecipient, ref strFrom, ref strSenderName, ref strSubject, ref strBody, ref strError) == true)
                ////if (objMailOnlyClass.GetMailBody(1, strCondition, ref strBody, ref strSubject, ref strFrom, ref strRecipient, ref strSiteMailId, ref strMailFormat) == true)
                //{
                //    strSubject = strSubject + " (Wait : Repayment)";
                //    strError = "";
                //    // -------------------------------------------------------------------
                //    //For each site, the mail will be sent only to sales@giftstoindia24x7.com
                //    strSiteMailId = "sales@giftstoindia24x7.com";
                //    // -------------------------------------------------------------------
                //    //if (objCommonFunction.SendMail(strSiteMailId, strSiteMailId, strSubject, strBody, strFrom, strSiteMailId, "", "matirswarga@gmail.com", ref strError) == true)
                //    //if (objCommonFunction.SendMail(strSiteMailId, strFrom + "<" + strSiteMailId + ">", strRecipient, strBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
                //    //{
                //    //    blFlag = true;
                //    //    strMailError = "The wait mail is been sent.";
                //    //}
                //    //else
                //    //{
                //    //    blFlag = false;
                //    //    strMailError = strError;
                //    //}
                //}
                //else
                //{
                //    blFlag = false;
                //    strMailError = strError;
                //}
            }
            else
            {
                blFlag = false;
                strMailError = objCartFunc.showNoitem();
            }
        }
        else
        {
            blFlag = false;
            strMailError = objCartFunc.showNoitem();
        }
        return blFlag;
    }
    protected bool sendWaitMail(bool ComboProduct, string OrderId, ref string strMailError)
    {
        bool blFlag = false;
        string strBody = "";
        string strSubject = "";
        string strRecipient = "tagBillingDetails.Billing_Email";
        string strSiteMailId = "sales@tagSales.SiteName";
        string strSenderName = strSiteMailId;
        string strFrom = "tagSales.SiteName";
        // Foramat changed for 24x7 series        
        if (objCommonFunction.getMailFomat(39, OrderId, ComboProduct, ref strSiteMailId, ref strRecipient, ref strFrom, ref strSenderName, ref strSubject, ref strBody, ref strError))
        {
            strSubject = strSubject + " (Wait : Repayment)";
            // -------------------------------------------------------------------
            //For each site, the mail will be sent only to sales@giftstoindia24x7.com
            strSiteMailId = "sales@giftstoindia24x7.com";
            // -------------------------------------------------------------------
            strError = "";
            //changed
            //if (objCommonFunction.SendMail(strSiteMailId, strFrom + "<" + strSiteMailId + ">", strRecipient, strBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
            //{
            //    blFlag = true;
            //    strMailError = "The wait mail is been sent.";
            //}
            //else
            //{
            //    blFlag = false;
            //    strMailError = strError;
            //}
            blFlag = true;
        }
        else
        {
            blFlag = false;
            strMailError = strError;
        }
        return blFlag;
    }
    public void functionforInsertArrayList(string strComboid)
    {
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = " SELECT " +
                        " rgcards_gti24x7.ComboSbill_Relation.ComboId, " +
                        " rgcards_gti24x7.ComboSbill_Relation.SBillNo " +
                        " FROM " +
                        " rgcards_gti24x7.ComboSbill_Relation " +
                        " WHERE " +
                        " (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboid + "')";

        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();

        if (dr.HasRows)
        {
            countSbill = functionforCountSbillNo(strComboid);

            while (dr.Read())
            {
                arr.Add(dr["SBillNo"].ToString());
            }
        }
        dr.Close();
        conn.Close();
    }
    //===============main func desin to view orders========================
    //public string functionforOrderDetails(string strSbillNo)
    //{
    //    decimal decTotAmt = 0;
    //    shippingdetails(strSbillNo);
    //    int count = 1, intTotQty = 0;
    //    string Imgpath = "";

    //    if (conn.State.ToString() == "Closed")
    //    {
    //        conn.Open();
    //    }
    //    string strSQL = " SELECT " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.SBillNo, " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.QOS, " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.Price, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Product_Id, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Item_Name, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Item_ImagePath " +
    //                    " FROM " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay, " +
    //                    " rgcards_gti24x7.ItemMaster_Server " +
    //                    " WHERE " +
    //                    " (rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id) AND " +
    //                    " (rgcards_gti24x7.SalesDetails_BothWay.SBillNo = '" + strSbillNo + "')";

    //    SqlCommand cmd = new SqlCommand(strSQL, conn);
    //    DataTable dt = new DataTable();
    //    dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
    //    DataTableReader dr = dt.CreateDataReader();

    //    if (dr.HasRows)
    //    {
    //        fnGenTblHd(ref strbody, strSbillNo);
    //        while (dr.Read())
    //        {
    //            //if (dr["Item_ImagePath"].ToString().Substring(0, 1).Equals("."))
    //            //{
    //            //    Imgpath = dr["Product_Id"].ToString() + dr["Item_ImagePath"].ToString();
    //            //}
    //            //else
    //            //{
    //            //    Imgpath = dr["Item_ImagePath"].ToString();
    //            //}
    //            Imgpath = dr["Product_Id"].ToString() + ".jpg";

    //            decimal totalamount = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["QOS"].ToString());
    //            strbody = strbody + "<tr><td><table id='tblloop' width='100%' cellpadding='0' cellspacing='0'>" + " <tr class='body-text-dark' height='80'> " +
    //              " <td width='4' background='PicturesForRakhi/dotted_vr_right.gif'><div align='center'></div></td> ";
    //            //=ship count
    //            if (count > 1)
    //            {
    //                strbody = strbody + "<td width='200'><div align='center'>" + "-" + "</div></td> ";
    //            }
    //            else
    //            {
    //                strbody = strbody + "<td width='200'><div align='left'>" + strshippingDetails + "</div></td> ";
    //            }
    //            strbody = strbody + "<td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //          " <td width='38'><div align='center'>" + count + "</div></td> " +
    //          " <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //          " <td width='82'><div align='center'><img src='http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "' width='60' height='60'></div></td> " +
    //          " <td width='247'><div align='center'><font face='Verdana' size='2'><font face='Verdana' size='2'><font face='Verdana' size='2'>" + dr["Item_Name"].ToString() + "</font></font></font></div></td> " +
    //          " <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //          " <td width='38'><div align='center'>" + dr["QOS"].ToString() + "</div></td> " +
    //          " <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //          " <td width='93'><div align='center'>Rs." + dr["Price"].ToString() + "/ " + strConvertedCurrSym + "." + Convert.ToDecimal(Convert.ToDecimal(dr["Price"]) / decConvertedCurr).ToString("0.00") + "</div></td> " +
    //          " <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //          " <td width='106'><div align='center'>Rs." + totalamount + "/ " + strConvertedCurrSym + "." + Convert.ToDecimal(totalamount / decConvertedCurr).ToString("0.00") + "</div><div align='center'></div></td> " +
    //          " <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //          " </tr> " +
    //          " <tr class='body-text-dark'> " +
    //          " <td colspan='13' background='PicturesForRakhi/dotted_line.gif'><img src='PicturesForRakhi/dotted_line.gif' width='14' height='4'></td> " +
    //          " </tr></table></td></tr>";
    //            //if (count > 1)
    //            //{
    //            //    blankShippingAddress();
    //            //}
    //            count++;
    //            Imgpath = "";
    //            decTotAmt = decTotAmt + totalamount;
    //            intTotQty = intTotQty + Convert.ToInt32(dr["QOS"]);
    //        }
    //        fnTotNDisc(ref strbody, decTotAmt, strSbillNo, intTotQty);
    //        //strshippingDetails = strshippingDetails +
    //        //  "<tr class='body-text-dark'> " +
    //        //  " <td colspan='12' background='PicturesForRakhi/dotted_line.gif'><img src='PicturesForRakhi/dotted_line.gif' width='14' height='4'></td> " +
    //        //  " </tr>";

    //        //if (flag != "1")
    //        //{
    //        //    if (countSbill != intcountid)
    //        //    {
    //        //        strshippingDetails = strshippingDetails + " <tr class='style6'> " +
    //        //          " <td colspan='12' bgcolor='#FFF1D4'><strong>&nbsp;&nbsp; Order Id - " + arr[intcountid] + " </strong></td> " +
    //        //          " </tr> " +
    //        //          " <tr class='style6'> " +
    //        //          "<td width='264' colspan='12' bgcolor='#FF6600' style='height: 20px'><span class='style5'>&nbsp;&nbsp; Ship Details </span></td>" +
    //        //          "</tr>";
    //        //        strbody = strbody +
    //        //           " <tr class='style6'> " +
    //        //           " <td colspan='12' bgcolor='#FFF1D4'><strong>&nbsp;&nbsp; </strong></td> " +
    //        //           " </tr> " +
    //        //           " <tr class='style6'> " +
    //        //           "<td width='680'  colspan='12' bgcolor='#FF6600' style='height: 20px'><span class='style5'>&nbsp; Cart Details </span></td>" +
    //        //           "</tr>";
    //        //    }
    //        //}

    //    }
    //    dr.Close();
    //    conn.Close();
    //    return strbody;
    //}
    //public string functionforOrderDetails(string strSbillNo)
    //{
    //    //get converted curr
    //    string[] arrConvreCurr = null;
    //    clsRakhi objRakhi = new clsRakhi();
    //    arrConvreCurr = (objRakhi.fnConvertedCurr(strSbillNo)).Split(new char[] { '^' });
    //    if (arrConvreCurr.Length > 0)
    //    {
    //        decConvertedCurr = Convert.ToDecimal(arrConvreCurr[0]);
    //        strConvertedCurrSym = arrConvreCurr[1].ToString().Trim();
    //    }
    //    decimal decTotAmt = 0;
    //    //shippingdetails(strSbillNo);
    //    int count = 1, intTotQty = 0;
    //    string Imgpath = "";

    //    if (conn.State.ToString() == "Closed")
    //    {
    //        conn.Open();
    //    }
    //    string strSQL = " SELECT " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.SBillNo, " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.QOS, " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.Price, " +
    //                    " rgcards_gti24x7.Salesmaster_BothWay.MWG, " +
    //                    " rgcards_gti24x7.Salesmaster_BothWay.Sinstruction, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Product_Id, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Item_Name, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Item_ImagePath " +
    //                    " FROM " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay, " +
    //                    " rgcards_gti24x7.ItemMaster_Server, " +
    //                    " rgcards_gti24x7.Salesmaster_BothWay" +
    //                    " WHERE " +
    //                    " (rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id) AND " +
    //                    "(rgcards_gti24x7.SalesDetails_BothWay.SBillNo = rgcards_gti24x7.Salesmaster_BothWay.SBillNo) AND" +
    //                    " (rgcards_gti24x7.SalesDetails_BothWay.SBillNo = '" + strSbillNo + "')";

    //    SqlCommand cmd = new SqlCommand(strSQL, conn);
    //    DataTable dt = new DataTable();
    //    dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
    //    DataTableReader dr = dt.CreateDataReader();

    //    if (dr.HasRows)
    //    {
    //        string msg = "", instruc = "";
    //        fnGenTblHd(ref strbody, strSbillNo);
    //        strbody = strbody + "<tr>" +
    //                             "<td align=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
    //                             "<tr>" +
    //                             "<td width=\"189\" align=\"left\" valign=\"top\">";

    //        if (count > 1)
    //        {
    //        }
    //        else
    //        {
    //            if ((dt.Rows[0]["MWG"] != null) && (Convert.ToString(dt.Rows[0]["MWG"]) != ""))
    //            {
    //                msg = dt.Rows[0]["MWG"].ToString();
    //            }
    //            else
    //            {
    //                msg = "None";
    //            }
    //            if ((dt.Rows[0]["Sinstruction"] != null) && (Convert.ToString(dt.Rows[0]["Sinstruction"]) != ""))
    //            {
    //                instruc = dt.Rows[0]["Sinstruction"].ToString();
    //            }
    //            else
    //            {
    //                instruc = "None";
    //            }
    //            strbody = strbody + shippingdetails(strSbillNo, msg, instruc);
    //        }
    //        strbody = strbody + "</td><td width=\"774\" align=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                            "<tr>" +
    //                            "<td align=\"left\" valign=\"top\">";

    //        while (dr.Read())
    //        {

    //            Imgpath = dr["Product_Id"].ToString() + ".jpg";
    //            decimal totalamount = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["QOS"].ToString());
    //            double dblDiscount = GetDiscountPercentage(strSbillNo);     //Convert.ToDouble(fnGetDiscount(strSbillNo) / dt.Rows.Count);

    //            double dblDollarAmt = Convert.ToDouble(new Gti24x7_CommonFunction().convertCurrency(Convert.ToDouble(Convert.ToDouble(dr["Price"]) - ((Convert.ToDouble(dr["Price"]) * dblDiscount) / 100)), Convert.ToInt32(HttpContext.Current.Session["CurrencyId"])));

    //            ////strbody = strbody +"<tr><td>"+
    //            ////  "<table id='tblloop' width='100%' cellpadding='0' cellspacing='0'>"+ " <tr class='body-text-dark' height='80'> " +
    //            ////  " <td width='4' background='PicturesForRakhi/dotted_vr_right.gif'><div align='center'></div></td> " ;
    //            ////  //=ship count

    //            ////if (count > 1)
    //            //// {
    //            ////    strbody=strbody + "<td width='200'><div align='center'>" + "-" + "</div></td> ";
    //            //// }
    //            //// else
    //            //// {
    //            ////     strbody = strbody + "<td width='200'><div align='left'>" + strshippingDetails + "</div></td> ";
    //            //// }
    //            strbody = strbody + "<ul class=\"prodList\">" +
    //                            "<li class=\"width2\">" + count + "</li>" +
    //                            "<li class=\"width3\"><img src=\"http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "\"  alt=\"\" title=\"\" class=\"itemImg\" />" + dr["Item_Name"].ToString() + "<input type=\"hidden\" name=\"item_name_" + count + "\" value=\"" + dr["Item_Name"].ToString() + "\"></li>" +
    //                            "<li class=\"width2\">" + dr["QOS"].ToString() + "<input type=\"hidden\" name=\"quantity_" + count + "\" value=\"" + dr["QOS"].ToString() + "\"></li>" +
    //                            "<li class=\"width4\">Rs." + dr["Price"].ToString() + "/" + strConvertedCurrSym + "." + Convert.ToDecimal((Convert.ToDecimal(dr["Price"]) / decConvertedCurr)).ToString("0.00") + "  </li>" +
    //                            "<li class=\"width9\">Rs." + totalamount + "/" + fnFormatedCurr(totalamount) + "<input type=\"hidden\" name=\"amount_" + count + "\" value=\"" + dblDollarAmt.ToString("0.00") + "\"></li>" +
    //                //"<li><a href=\"#\"><img src=\"images/remove_btn.gif\" alt=\"Delete Item\" border=\"0\" class=\"removeBtn\" title=\"Delete Item\" /></a>" +
    //                            "</ul>";




    //            ////  strbody = strbody + "<td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='38'><div align='center'>" + count + "</div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='82'><div align='center'><img src='http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "' width='60' height='60'></div></td> " +
    //            ////" <td width='247'><div align='center'><font face='Verdana' size='2'><font face='Verdana' size='2'><font face='Verdana' size='2'>" + dr["Item_Name"].ToString() + "</font></font></font></div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='38'><div align='center'>" + dr["QOS"].ToString() + "</div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='93'><div align='center'>Rs." + dr["Price"].ToString() + "</div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='106'><div align='center'>Rs." + totalamount + "</div> <div align='center'></div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" </tr> " +
    //            ////" <tr class='body-text-dark'> " +
    //            ////" <td colspan='13' background='PicturesForRakhi/dotted_line.gif'><img src='PicturesForRakhi/dotted_line.gif' width='14' height='4'></td> " +
    //            ////" </tr></table></td></tr>";





    //            //if (count > 1)
    //            //{
    //            //    blankShippingAddress();
    //            //}
    //            count++;
    //            Imgpath = "";

    //            decTotAmt = decTotAmt + totalamount;
    //            intTotQty = intTotQty + Convert.ToInt32(dr["QOS"]);
    //            // grndtotal = grndtotal + decTotAmt;
    //            grndtotal = grndtotal + totalamount;

    //        }
    //        fnTotNDisc(ref strbody, decTotAmt, strSbillNo, intTotQty);
    //        strbody = strbody + "</td></tr></table></td></tr></table></td></tr>";



    //    }
    //    dr.Close();
    //    conn.Close();
    //    return strbody;
    //}


    //public string functionforOrderDetails(string strSbillNo)
    //{
    //    //get converted curr
    //    string[] arrConvreCurr = null;
    //    clsRakhi objRakhi = new clsRakhi();
    //    arrConvreCurr = (objRakhi.fnConvertedCurr(strSbillNo)).Split(new char[] { '^' });
    //    if (arrConvreCurr.Length > 0)
    //    {
    //        decConvertedCurr = Convert.ToDecimal(arrConvreCurr[0]);
    //        strConvertedCurrSym = arrConvreCurr[1].ToString().Trim();
    //    }
    //    decimal decTotAmt = 0;
    //    //shippingdetails(strSbillNo);
    //    int count = 1, intTotQty = 0;
    //    string Imgpath = "";

    //    if (conn.State.ToString() == "Closed")
    //    {
    //        conn.Open();
    //    }
    //    string strSQL = " SELECT " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.SBillNo, " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.QOS, " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay.Price, " +
    //                    " rgcards_gti24x7.Salesmaster_BothWay.MWG, " +
    //                    " rgcards_gti24x7.Salesmaster_BothWay.Sinstruction, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Product_Id, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Item_Name, " +
    //                    " rgcards_gti24x7.ItemMaster_Server.Item_ImagePath " +
    //                    " FROM " +
    //                    " rgcards_gti24x7.SalesDetails_BothWay, " +
    //                    " rgcards_gti24x7.ItemMaster_Server, " +
    //                    " rgcards_gti24x7.Salesmaster_BothWay" +
    //                    " WHERE " +
    //                    " (rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id) AND " +
    //                    "(rgcards_gti24x7.SalesDetails_BothWay.SBillNo = rgcards_gti24x7.Salesmaster_BothWay.SBillNo) AND" +
    //                    " (rgcards_gti24x7.SalesDetails_BothWay.SBillNo = '" + strSbillNo + "')";

    //    SqlCommand cmd = new SqlCommand(strSQL, conn);
    //    DataTable dt = new DataTable();
    //    dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
    //    DataTableReader dr = dt.CreateDataReader();

    //    if (dr.HasRows)
    //    {
    //        string msg = "", instruc = "";
    //        fnGenTblHd(ref strbody, strSbillNo);
    //        strbody = strbody + "<tr>" +
    //                             "<td align=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
    //                             "<tr>" +
    //                             "<td width=\"189\" align=\"left\" valign=\"top\">";

    //        if (count > 1)
    //        {
    //        }
    //        else
    //        {
    //            if ((dt.Rows[0]["MWG"] != null) && (Convert.ToString(dt.Rows[0]["MWG"]) != ""))
    //            {
    //                msg = dt.Rows[0]["MWG"].ToString();
    //            }
    //            else
    //            {
    //                msg = "None";
    //            }
    //            if ((dt.Rows[0]["Sinstruction"] != null) && (Convert.ToString(dt.Rows[0]["Sinstruction"]) != ""))
    //            {
    //                instruc = dt.Rows[0]["Sinstruction"].ToString();
    //            }
    //            else
    //            {
    //                instruc = "None";
    //            }
    //            strbody = strbody + shippingdetails(strSbillNo, msg, instruc);
    //        }
    //        strbody = strbody + "</td><td width=\"774\" align=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                            "<tr>" +
    //                            "<td align=\"left\" valign=\"top\">";

    //        while (dr.Read())
    //        {

    //            Imgpath = dr["Product_Id"].ToString() + ".jpg";
    //            decimal totalamount = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["QOS"].ToString());

    //            ////strbody = strbody +"<tr><td>"+
    //            ////  "<table id='tblloop' width='100%' cellpadding='0' cellspacing='0'>"+ " <tr class='body-text-dark' height='80'> " +
    //            ////  " <td width='4' background='PicturesForRakhi/dotted_vr_right.gif'><div align='center'></div></td> " ;
    //            ////  //=ship count

    //            ////if (count > 1)
    //            //// {
    //            ////    strbody=strbody + "<td width='200'><div align='center'>" + "-" + "</div></td> ";
    //            //// }
    //            //// else
    //            //// {
    //            ////     strbody = strbody + "<td width='200'><div align='left'>" + strshippingDetails + "</div></td> ";
    //            //// }
    //            strbody = strbody + "<ul class=\"prodList\">" +
    //                            "<li class=\"width2\">" + count + "</li>" +
    //                            "<li class=\"width3\"><img src=\"http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "\"  alt=\"\" title=\"\" class=\"itemImg\" />" + dr["Item_Name"].ToString() + "</li>" +
    //                            "<li class=\"width2\">" + dr["QOS"].ToString() + "</li>" +
    //                            "<li class=\"width4\">Rs." + dr["Price"].ToString() + "/" + strConvertedCurrSym + "." + Convert.ToDecimal((Convert.ToDecimal(dr["Price"]) / decConvertedCurr)).ToString("0.00") + "  </li>" +
    //                            "<li class=\"width9\">Rs." + totalamount + "/" + fnFormatedCurr(totalamount) + "</li>" +
    //                //"<li><a href=\"#\"><img src=\"images/remove_btn.gif\" alt=\"Delete Item\" border=\"0\" class=\"removeBtn\" title=\"Delete Item\" /></a>" +
    //                            "</ul>";




    //            ////  strbody = strbody + "<td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='38'><div align='center'>" + count + "</div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='82'><div align='center'><img src='http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "' width='60' height='60'></div></td> " +
    //            ////" <td width='247'><div align='center'><font face='Verdana' size='2'><font face='Verdana' size='2'><font face='Verdana' size='2'>" + dr["Item_Name"].ToString() + "</font></font></font></div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='38'><div align='center'>" + dr["QOS"].ToString() + "</div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='93'><div align='center'>Rs." + dr["Price"].ToString() + "</div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" <td width='106'><div align='center'>Rs." + totalamount + "</div> <div align='center'></div></td> " +
    //            ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
    //            ////" </tr> " +
    //            ////" <tr class='body-text-dark'> " +
    //            ////" <td colspan='13' background='PicturesForRakhi/dotted_line.gif'><img src='PicturesForRakhi/dotted_line.gif' width='14' height='4'></td> " +
    //            ////" </tr></table></td></tr>";





    //            //if (count > 1)
    //            //{
    //            //    blankShippingAddress();
    //            //}
    //            count++;
    //            Imgpath = "";

    //            decTotAmt = decTotAmt + totalamount;
    //            intTotQty = intTotQty + Convert.ToInt32(dr["QOS"]);
    //            // grndtotal = grndtotal + decTotAmt;
    //            grndtotal = grndtotal + totalamount;

    //        }
    //        fnTotNDisc(ref strbody, decTotAmt, strSbillNo, intTotQty);
    //        strbody = strbody + "</td></tr></table></td></tr></table></td></tr>";



    //    }
    //    dr.Close();
    //    conn.Close();
    //    return strbody;
    //}
    public string functionforOrderDetails(string strSbillNo)
    {
        //get converted curr
        string[] arrConvreCurr = null;
        clsRakhi objRakhi = new clsRakhi();
        //changed
        // arrConvreCurr = (objRakhi.fnConvertedCurr(strSbillNo)).Split(new char[] { '^' });
        if (arrConvreCurr.Length > 0)
        {
            decConvertedCurr = Convert.ToDecimal(arrConvreCurr[0]);
            strConvertedCurrSym = arrConvreCurr[1].ToString().Trim();
        }
        decimal decTotAmt = 0;
        //shippingdetails(strSbillNo);
        int count = 1, intTotQty = 0;
        string Imgpath = "";

        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = " SELECT " +
                        " rgcards_gti24x7.SalesDetails_BothWay.SBillNo, " +
                        " rgcards_gti24x7.SalesDetails_BothWay.QOS, " +
                        " rgcards_gti24x7.SalesDetails_BothWay.Price, " +
                        " rgcards_gti24x7.Salesmaster_BothWay.MWG, " +
                        " rgcards_gti24x7.Salesmaster_BothWay.Sinstruction, " +
                        " rgcards_gti24x7.ItemMaster_Server.Product_Id, " +
                        " rgcards_gti24x7.ItemMaster_Server.Item_Name, " +
                        " rgcards_gti24x7.ItemMaster_Server.Item_ImagePath " +
                        " FROM " +
                        " rgcards_gti24x7.SalesDetails_BothWay, " +
                        " rgcards_gti24x7.ItemMaster_Server, " +
                        " rgcards_gti24x7.Salesmaster_BothWay" +
                        " WHERE " +
                        " (rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id) AND " +
                        "(rgcards_gti24x7.SalesDetails_BothWay.SBillNo = rgcards_gti24x7.Salesmaster_BothWay.SBillNo) AND" +
                        " (rgcards_gti24x7.SalesDetails_BothWay.SBillNo = '" + strSbillNo + "')";

        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();

        if (dr.HasRows)
        {
            string msg = "", instruc = "";
            fnGenTblHd(ref strbody, strSbillNo);
            strbody = strbody + "<tr>" +
                                 "<td align=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                 "<tr>" +
                                 "<td width=\"189\" align=\"left\" valign=\"top\">";

            if (count > 1)
            {
            }
            else
            {
                if ((dt.Rows[0]["MWG"] != null) && (Convert.ToString(dt.Rows[0]["MWG"]) != ""))
                {
                    msg = dt.Rows[0]["MWG"].ToString();
                }
                else
                {
                    msg = "None";
                }
                if ((dt.Rows[0]["Sinstruction"] != null) && (Convert.ToString(dt.Rows[0]["Sinstruction"]) != ""))
                {
                    instruc = dt.Rows[0]["Sinstruction"].ToString();
                }
                else
                {
                    instruc = "None";
                }
                strbody = strbody + shippingdetails(strSbillNo, msg, instruc);
            }
            strbody = strbody + "</td><td width=\"774\" align=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                "<tr>" +
                                "<td align=\"left\" valign=\"top\">";

            while (dr.Read())
            {

                Imgpath = dr["Product_Id"].ToString() + ".jpg";
                decimal totalamount = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["QOS"].ToString());
                double dblDiscount = GetDiscountPercentage(strSbillNo);     //Convert.ToDouble(fnGetDiscount(strSbillNo) / dt.Rows.Count);

                //  double dblDollarAmt = Convert.ToDouble(new Gti24x7_CommonFunction().convertCurrency(Convert.ToDouble(Convert.ToDouble(dr["Price"]) - ((Convert.ToDouble(dr["Price"]) * dblDiscount) / 100)), Convert.ToInt32(HttpContext.Current.Session["CurrencyId"])));
                //chnaged
                double dblDollarAmt = 0.00;



                ////strbody = strbody +"<tr><td>"+
                ////  "<table id='tblloop' width='100%' cellpadding='0' cellspacing='0'>"+ " <tr class='body-text-dark' height='80'> " +
                ////  " <td width='4' background='PicturesForRakhi/dotted_vr_right.gif'><div align='center'></div></td> " ;
                ////  //=ship count

                ////if (count > 1)
                //// {
                ////    strbody=strbody + "<td width='200'><div align='center'>" + "-" + "</div></td> ";
                //// }
                //// else
                //// {
                ////     strbody = strbody + "<td width='200'><div align='left'>" + strshippingDetails + "</div></td> ";
                //// }
                strbody = strbody + "<ul class=\"prodList\">" +
                                "<li class=\"width2\">" + count + "</li>" +
                                "<li class=\"width3\"><img src=\"http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "\"  alt=\"\" title=\"\" class=\"itemImg\" />" + dr["Item_Name"].ToString() + "<input type=\"hidden\" name=\"item_name_" + count + "\" value=\"" + dr["Item_Name"].ToString() + "\"></li>" +
                                "<li class=\"width2\">" + dr["QOS"].ToString() + "<input type=\"hidden\" name=\"quantity_" + count + "\" value=\"" + dr["QOS"].ToString() + "\"></li>" +
                                "<li class=\"width4\">Rs." + dr["Price"].ToString() + "/" + strConvertedCurrSym + "." + Convert.ToDecimal((Convert.ToDecimal(dr["Price"]) / decConvertedCurr)).ToString("0.00") + "  </li>" +
                                "<li class=\"width9\">Rs." + totalamount + "/" + fnFormatedCurr(totalamount) + "<input type=\"hidden\" name=\"amount_" + count + "\" value=\"" + dblDollarAmt.ToString("0.00") + "\"></li>" +
                    //"<li><a href=\"#\"><img src=\"images/remove_btn.gif\" alt=\"Delete Item\" border=\"0\" class=\"removeBtn\" title=\"Delete Item\" /></a>" +
                                "</ul>";




                ////  strbody = strbody + "<td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
                ////" <td width='38'><div align='center'>" + count + "</div></td> " +
                ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
                ////" <td width='82'><div align='center'><img src='http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "' width='60' height='60'></div></td> " +
                ////" <td width='247'><div align='center'><font face='Verdana' size='2'><font face='Verdana' size='2'><font face='Verdana' size='2'>" + dr["Item_Name"].ToString() + "</font></font></font></div></td> " +
                ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
                ////" <td width='38'><div align='center'>" + dr["QOS"].ToString() + "</div></td> " +
                ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
                ////" <td width='93'><div align='center'>Rs." + dr["Price"].ToString() + "</div></td> " +
                ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
                ////" <td width='106'><div align='center'>Rs." + totalamount + "</div> <div align='center'></div></td> " +
                ////" <td width='4' background='PicturesForRakhi/dotted_vr_line.gif'>&nbsp;</td> " +
                ////" </tr> " +
                ////" <tr class='body-text-dark'> " +
                ////" <td colspan='13' background='PicturesForRakhi/dotted_line.gif'><img src='PicturesForRakhi/dotted_line.gif' width='14' height='4'></td> " +
                ////" </tr></table></td></tr>";





                //if (count > 1)
                //{
                //    blankShippingAddress();
                //}
                count++;
                Imgpath = "";

                decTotAmt = decTotAmt + totalamount;
                intTotQty = intTotQty + Convert.ToInt32(dr["QOS"]);
                // grndtotal = grndtotal + decTotAmt;
                grndtotal = grndtotal + totalamount;

            }
            fnTotNDisc(ref strbody, decTotAmt, strSbillNo, intTotQty);
            strbody = strbody + "</td></tr></table></td></tr></table></td></tr>";



        }
        dr.Close();
        conn.Close();
        return strbody;
    }

    //===========end=========================================
    public void fnGenTblHd(ref string strbody, string strSbillNo)
    {
        //strbody = strbody + "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
        strbody = strbody + "<tr>" +
                        "<td align=\"left\" valign=\"top\">" +
                        "<ul class=\"topName\">" +
                        "<li class=\"width1\">" + strSbillNo + "</li>" +
                        "<li class=\"width2\">Sl No </li>" +
                        "<li class=\"width3\">Item</li>" +
                        "<li class=\"width2\">Qty</li>" +
                        "<li class=\"width4\">Price</li>" +
                        "<li class=\"width4\">Total Price</li>";
        if (grndordercount == 2)
        {
            strbody = strbody + " <li><a href=\"javascript:fnDelOrder(1,'" + strSbillNo + "');\"><img src=\"images/trash_icon.gif\" alt=\"Delete Item\" border=\"0\" class=\"removeBtn\" title=\"Delete Item\" /></a></li>";
        }
        strbody = strbody + "</ul></td>" +
        "</tr>";

    }
    public string functionforOredDetailForCOMBO(string strComboid)
    {
        functionforInsertArrayList(strComboid);

        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = " SELECT " +
                        " rgcards_gti24x7.ComboSbill_Relation.ComboId, " +
                        " rgcards_gti24x7.ComboSbill_Relation.SBillNo " +
                        " FROM " +
                        " rgcards_gti24x7.ComboSbill_Relation " +
                        " WHERE " +
                        " (rgcards_gti24x7.ComboSbill_Relation.recordid <> 0) AND" +
                        " (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboid + "') order by ComboSbill_Relation.SBillNo desc";

        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();
        if (dt.Rows.Count >= 2)
        {
            grndordercount = 2;
        }
        if (dr.HasRows)
        {
            countSbill = functionforCountSbillNo(strComboid);
            strbody = strbody + "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
            while (dr.Read())
            {
                functionforOrderDetails(dr["SBillNo"].ToString());
                intcountid++;
            }
        }
        dr.Close();
        conn.Close();
        strbody = strbody + "</tr><tr>" +
                        "<td align=\"left\" valign=\"top\">" +
                        "<ul class=\"subTotal\">" +
                        "<li><span>Grand Total :</span></li>" +
                        "<li class=\"width6\">" + grndtotal + "/" + fnFormatedCurr(grndtotal) + "</li>" +
                        "<li><span>Grand Discount  :</span></li>" +
                        "<li class=\"width6\">Rs." + grnddis + "/" + fnFormatedCurr(grnddis) + "</li>" +
                        "<li><span>Grand Payable :</span></li>" +
                        "<li class=\"width6\">Rs." + Convert.ToDouble(grndtotal - grnddis).ToString("0.00") + "/" + fnFormatedCurr((grndtotal - grnddis)) + "</li>" +
                        "</ul>" +
                        "</td>" +
                        "</tr>" +
                        "</table>";
        ////strbody = strbody + "<ul class=\"subTotal\">" +
        ////                "<li><span>Grand Total :</span></li>" +
        ////                "<li class=\"width6\">Rs.1997.00 / $120.34</li>" +
        ////                "<li><span>Grand Discount  :</span></li>" +
        ////                "<li class=\"width6\">Rs.1997.00 / $120.34</li>" +
        ////                "<li><span>Grand Payable :</span></li>" +
        ////                "<li class=\"width6\">Rs.1997.00 / $120.34</li>" +
        ////                "</ul>" +
        ////                "</table>";
        return strbody;
    }
    public string shippingdetails(string strSbillNo, string msg, string instruc)
    {
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = " SELECT " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name, " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1, " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2, " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode, " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile, " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email, " +
                          " rgcards_gti24x7.Country_Server.Country_Name, " +
                          " rgcards_gti24x7.State_Server.State_Name, " +
                          " rgcards_gti24x7.City_Server.City_Name, " +
                          " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId " +
                        " FROM " +
                          " rgcards_gti24x7.ShippingDetails_Bothway, " +
                          " rgcards_gti24x7.Country_Server, " +
                          " rgcards_gti24x7.State_Server, " +
                          " rgcards_gti24x7.City_Server " +
                        " WHERE " +
                          " (rgcards_gti24x7.ShippingDetails_Bothway.SBillNo = '" + strSbillNo + "') AND  " +
                          " (rgcards_gti24x7.Country_Server.Country_Id = rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId) AND  " +
                          " (rgcards_gti24x7.State_Server.State_Id = rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId) AND  " +
                          " (rgcards_gti24x7.City_Server.City_Id = rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId)";

        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();

        if (dr.HasRows)
        {
            strshippingDetails = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            if (dr.Read())
            {
                strshippingDetails = strshippingDetails + "<tr>" +
                "<td align=\"left\" valign=\"top\" class=\"shipDetail\">" +
                "<p><span>" + dr["Shipping_Name"].ToString() + "</span><br />" +
                "" + dr["Shipping_Address1"].ToString() + "<br/>" + dr["Shipping_Mobile"].ToString() + "<br/>" + dr["Shipping_PinCode"].ToString() + " " + dr["Country_Name"].ToString() + "<br/>" + dr["Shipping_PhNo"].ToString() + " " + dr["City_Name"].ToString() +
                "</p></td>" +
                "</tr>" +
                "<tr>" +
                "<td align=\"left\" valign=\"top\" class=\"msgDetail\"><p class=\"msgDetailTxt\">To view the message and special instructions for the order, mouseover the below button.</p>" +
                "<span id=\"snmsg\" class=\"msg\" title=\"header=[<img src='images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Message] body=[" + msg + "]\">Message</span><br />" +
                "<span id=\"snins\" class=\"ins\" title=\"header=[<img src='images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Special Instructions] body=[" + instruc + "]\">Special Instructions</span>" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align=\"left\" valign=\"top\">&nbsp;</td>" +
                "</tr>";

            }
            strshippingDetails = strshippingDetails + " </table>";



            ////strshippingDetails="<table id='tblship' width=\"200\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            ////if (dr.Read())
            ////{
            ////    strshippingDetails = strshippingDetails + " <tr> " +
            ////    " <td width='14'></td> " +
            ////    " <td width='238'>" + dr["Shipping_Name"].ToString() + "<br> " +
            ////    " <font face='Verdana' size='2'>" + dr["Shipping_Address1"].ToString() + "</font><br> " +
            ////    " <font face='Verdana' size='2'>" + dr["Shipping_Mobile"].ToString() + "</font><br> " +
            ////    " <font face='Verdana' size='2'>" + dr["Shipping_PinCode"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;<font face='Verdana' size='2'>" + dr["Country_Name"].ToString() + "</font><br> " +
            ////    " <font face='Verdana' size='2'>" + dr["Shipping_PhNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;<font face='Verdana' size='2'>" + dr["City_Name"].ToString() + "</font><br> " +
            ////        //" <font face='Verdana' size='2'><a href='mailto:" + dr["Shipping_Email"].ToString() + "' target='_blank' onclick='returntop.js.OpenExtLink(window,event,this)'>" + dr["Shipping_Email"].ToString() + "</a></font></font></font></font></td> " +
            ////    " <td width='12'>&nbsp;</td> " +
            ////    " </tr> ";
            ////}
            ////strshippingDetails = strshippingDetails + " </table>";
        }

        dr.Close();
        conn.Close();
        return strshippingDetails;
    }
    public int functionforCountSbillNo(string strSbillNo)
    {
        int intcount = 0;
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = " SELECT " +
                       " COUNT(rgcards_gti24x7.ComboSbill_Relation.SBillNo) AS SR " +
                       " FROM " +
                       " rgcards_gti24x7.ComboSbill_Relation " +
                       " WHERE " +
                       " (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strSbillNo + "')";
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();
        if (dr.HasRows)
        {
            if (dr.Read())
            {
                intcount = Convert.ToInt32(dr["SR"].ToString());
            }
        }
        dr.Close();
        conn.Close();
        return intcount;
    }
    public string blankShippingAddress()
    {
        strshippingDetails = strshippingDetails + " <tr> " +
            " <td width='14'><font face='Verdana' size='2'><font face='Verdana' size='2'></font></font></td> " +
            " <td width='238'><font face='Verdana' size='2'><font face='Verdana' size='2'><font face='Verdana' size='2'>&nbsp;</font><br> " +
            " <font face='Verdana' size='2'>&nbsp;</font><br> " +
            " <font face='Verdana' size='2'>&nbsp;</font><br> " +
            " <font face='Verdana' size='2'>&nbsp;&nbsp;&nbsp;&nbsp;<font face='Verdana' size='2'>&nbsp;</font><br> " +
            //" <font face='Verdana' size='2'>&nbsp;</font>&nbsp;<br> " +
            " <font face='Verdana' size='2'><a href='#' target='_blank' >&nbsp;</a></font></font></font></font></td> " +
            " <td width='12'>&nbsp;</td> " +
            " </tr> " +
            " <tr class='body-text-dark'> " +
            " <td colspan='12'width='14' height='4'></td> " +
            " </tr>";
        return strshippingDetails;
    }
    public double fnFinalAmount(string strCommon)
    {
        double intTotalComboAmount = 0.0;
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = "SELECT " +
                        "" + strSchema + ".SalesMaster_BothWay.Sales_ATOT" +
                        " FROM " +
                        "" + strSchema + ".ComboSbill_Relation" +
                        " INNER JOIN " + strSchema + ".SalesMaster_BothWay " +
                        " ON (" + strSchema + ".ComboSbill_Relation.SBillNo = " + strSchema + ".SalesMaster_BothWay.SBillNo)" +
                        "WHERE" +
                        "(" + strSchema + ".ComboSbill_Relation.ComboId = '" + strCommon + "')";

        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                intTotalComboAmount = intTotalComboAmount + Convert.ToDouble(dr["Sales_ATOT"]);
                //if (htUserDetails.Contains("disCode"))
                //{
                //    if (htUserDetails["disType"].ToString()=="2")
                //    {
                //        intTotalComboAmount = intTotalComboAmount - ((intTotalComboAmount * Convert.ToDouble(htUserDetails["disVal"])) / 100);
                //    }
                //    else
                //    {
                //        intTotalComboAmount = intTotalComboAmount - Convert.ToDouble(htUserDetails["disVal"]);
                //    }
                //}
            }
        }
        dr.Close();
        conn.Close();
        return intTotalComboAmount;

    }
    public string GetCartPaypal(DataTable dtShoppingCart)
    {

        StringBuilder strCartOutput = new StringBuilder();
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
            double dblTotGrandPrice = 0.00;
            DataTable dt = new DataTable();
            dt = fnGetSalesDetailCombo(strCommon);
            foreach (DataRow dRow in dt.Rows)
            {
                intCount++;

                int intRowQnty = Convert.ToInt32(dRow["qnty"].ToString());
                double dblRowPrice = Convert.ToDouble(dRow["rowPrice"].ToString());
                //=============discount======================
                if (htUserDetails["disCode"].ToString() != "0")
                {
                    if (htUserDetails["disType"].ToString() == "2")
                    {
                        dblRowPrice = dblRowPrice - ((dblRowPrice * Convert.ToDouble(htUserDetails["disVal"])) / 100);
                    }
                    else
                    {
                        dblRowPrice = dblRowPrice - Convert.ToDouble(htUserDetails["disVal"]);
                    }
                }
                //===============end discount========================
                double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
                dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);

                string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
                string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");

                strCartOutput.Append("<!--- Cart's Rows Starts--->");
                strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
                strCartOutput.Append("<tr>");
                strCartOutput.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"style1\">");
                strCartOutput.Append("<input type=\"hidden\" name=\"item_name_" + intCount + "\" value=\"" + dRow["prodName"].ToString() + "\">");
                strCartOutput.Append("</td>");

                strCartOutput.Append("<td width=\"10%\" class=\"style1\" align=\"center\" valign=\"middle\">");
                strCartOutput.Append("<input type=\"hidden\" name=\"quantity_" + intCount + "\" value=\"" + intRowQnty + "\">");
                strCartOutput.Append("</td>");
                strCartOutput.Append("<td width=\"25%\" align=\"center\" valign=\"middle\"> ");
                // Convert the INR into respective currency 

                // changed by me
                //double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, intCurrencyId));
                double dblDollarAmt = 0.00;


                //double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, 1));
                strCartOutput.Append("<input type=\"hidden\" name=\"amount_" + intCount + "\" value=\"" + dblDollarAmt.ToString("######.00") + "\">");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("</table>");
                strCartOutput.Append("<!--- Cart's Rows Ends--->");

            }


        }
        return strCartOutput.ToString();
    }
    public DataTable fnGetSalesDetailCombo(string strcombo)
    {
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = "SELECT " +
                          "rgcards_gti24x7.SalesDetails_BothWay.QOS AS qnty," +
                          "rgcards_gti24x7.SalesDetails_BothWay.VendorId," +
                          "rgcards_gti24x7.SalesDetails_BothWay.Price AS rowPrice," +
                          "rgcards_gti24x7.SalesDetails_BothWay.SlNo," +
                          "rgcards_gti24x7.SalesDetails_BothWay.SBillNo," +
                          "rgcards_gti24x7.ItemMaster_Server.Item_Name AS prodName" +
                        " FROM " +
                          "rgcards_gti24x7.ComboSbill_Relation" +
                          " INNER JOIN rgcards_gti24x7.SalesDetails_BothWay ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.SalesDetails_BothWay.SBillNo)" +
                          "INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
                        " WHERE " +
                          "(rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strcombo + "')" +
                        " ORDER BY " +
                          "rgcards_gti24x7.SalesDetails_BothWay.SBillNo";

        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();

        dr.Close();
        conn.Close();
        return dt;
    }
    public DataTable fnGrtOrderCombo(string strOrderNo)//to get order nos wrt combo id
    {
        string strComboid = strOrderNo;
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }

        string strSQL = " SELECT " +
                        " rgcards_gti24x7.ComboSbill_Relation.ComboId, " +
                        " rgcards_gti24x7.ComboSbill_Relation.SBillNo " +
                        " FROM " +
                        " rgcards_gti24x7.ComboSbill_Relation " +
                        " WHERE " +
                        " (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboid + "')";
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        return dt;
    }
    public void mailWaitRakhi(string strComboId, int sbillCombo, string SiteName)
    {
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }

        string strBody = "";
        string strSQL = "";
        int i = 0;
        int j = 0;
        int proCount = 0;
        int proPrice = 0;
        int totalProPrice = 0;
        int intTotQty = 0;

        strBody = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'" +
                "'http://www.w3.org/TR/html4/loose.dtd'> " +
                "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'> " +
                " <title>:: " + SiteName + " ::</title></head><body><font face='verdana' size=2> " +
                " <table width='100%' height='330' border='0'>";
        //"<tr><td colspan='2'><img src='http://www.giftstoindia24x7.com/PicturesForRakhi/new_header.jpg' width='600' height='108'></td></tr>";
        // "<tr><td height='37' width='100%' cellpadding='0' cellspacing='0'>";

        if (sbillCombo == 0)
        {
            strSQL = "SELECT rgcards_gti24x7.BillingDetails_Bothway.Billing_Name, " +
                    "(CONVERT(varchar(100),rgcards_gti24x7.BillingDetails_Bothway.Billing_Address1) +" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_City +" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_PinCode +" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_State +" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_Country) as Address," +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_Email, " +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_PhNo, " +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_Mobile " +
                    " FROM rgcards_gti24x7.ComboSbill_Relation" +
                    " INNER JOIN rgcards_gti24x7.BillingDetails_Bothway " +
                    " ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.BillingDetails_Bothway.SBillNo)" +
                    " WHERE (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";
        }
        else
        {
            strSQL = "SELECT rgcards_gti24x7.BillingDetails_Bothway.Billing_Name," +
                    "(convert(VARCHAR(100), rgcards_gti24x7.BillingDetails_Bothway.Billing_Address1)+" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_City +" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_PinCode +" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_State +" +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_Country ) as Address," +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_Email," +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_PhNo," +
                    " rgcards_gti24x7.BillingDetails_Bothway.Billing_Mobile " +
                    " FROM rgcards_gti24x7.BillingDetails_Bothway " +
                    " WHERE (rgcards_gti24x7.BillingDetails_Bothway.SBillNo = '" + strComboId + "')";
        }

        SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
        DataTable dtBillDetails = new DataTable();

        da.Fill(dtBillDetails);
        if (sbillCombo == 0)
        {
            strSQL = "SELECT " +
                    "rgcards_gti24x7.ComboSbill_Relation.SBillNo," +
                    "(rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name+" +
                    "' '+CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1)+" +
                    "' '+CONVERT(varchar(100),rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2)+" +
                    "'  '+rgcards_gti24x7.City_Server.City_Name+" +
                    "'  '+rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode +" +
                    "'  '+rgcards_gti24x7.State_Server.State_Name +" +
                    "'  '+rgcards_gti24x7.Country_Server.Country_Name) as Address," +
                    " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
                    " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile ," +
                    " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email," +
                    " CONVERT(CHAR(19),rgcards_gti24x7.SalesMaster_BothWay.DOD,106) AS DOD, " +
                    " rgcards_gti24x7.SalesMaster_BothWay.MWG," +
                    " rgcards_gti24x7.SalesMaster_BothWay.SInstruction " +
                    " FROM rgcards_gti24x7.ComboSbill_Relation " +
                    " INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo) " +
                    " INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id) " +
                    " INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id) " +
                    " INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id) " +
                    " INNER JOIN rgcards_gti24x7.SalesMaster_BothWay ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.SalesMaster_BothWay.SBillNo)" +
                    " WHERE  (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";
        }
        else if (sbillCombo == 1)
        {
            strSQL = "SELECT rgcards_gti24x7.SalesMaster_BothWay.SBillNo," +
                    " (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name +" +
                    "' '+CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1)+" +
                    "' '+CONVERT(varchar(100),rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2)+" +
                    "'  '+rgcards_gti24x7.City_Server.City_Name+" +
                    "'  '+rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode + " +
                    "'  '+rgcards_gti24x7.State_Server.State_Name +" +
                    "'  '+rgcards_gti24x7.Country_Server.Country_Name) as Address, " +
                    " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
                    " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile, " +
                    " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email, " +
                    " CONVERT(CHAR(19),rgcards_gti24x7.SalesMaster_BothWay.DOD,106) AS DOD, " +
                    " rgcards_gti24x7.SalesMaster_BothWay.MWG," +
                    " rgcards_gti24x7.SalesMaster_BothWay.SInstruction" +
                    " FROM " +
                    " rgcards_gti24x7.SalesMaster_BothWay " +
                    " INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo)" +
                    " INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id)" +
                    " INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id)" +
                    " INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id)" +
                    " WHERE" +
                    " (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = '" + strComboId + "')";
        }
        if (dtBillDetails.Rows.Count > 0)
        {
            strBody = strBody + "<tr><td height='37' colspan='2' width='100%'><p>Dear " + dtBillDetails.Rows[0]["Billing_Name"] + ",</p>&nbsp;&nbsp;&nbsp;&nbsp;" +
                    "Thank you for placing your order with us. The gifts will be delivered as per your instruction. Kindly review the below information related to your order.</p>" +
                    "</td></tr><tr><td width='30%'>" +
                    "<table width='100%' height='125'  border='1' cellpadding='0' cellspacing='0'>" +
                    "<tr><td><strong>Order Id</strong>:" + strComboId.ToString() + " </td></tr>" +
                    "<tr><td><strong>Email</strong>:<a href='mailTo:sales@giftstoindia24x7.com'>sales@giftstoindia24x7.com</a></td></tr>" +
                    "<tr>" +
                    "<td><strong>Website</strong>:<a href='http://www." + strGenSitename + "'>http://www." + strGenSitename + "</a></td></tr><tr><td><strong>IP Address</strong>:" + Request.UserHostAddress.ToString() + "</td></tr>" +
                    "</table></td><td width='70%'>" +
                    "<table width='100%' height='126'  border='1' cellpadding='0' cellspacing='0'>" +
                    "<tr><td><strong>Billing Details</strong> : </td></tr><tr> <td>" + dtBillDetails.Rows[0]["Billing_Name"] + "</td></tr><tr><td><strong>Add : </strong>" + dtBillDetails.Rows[0]["Address"] + "</td>" +
                    "</tr><tr><td><strong>Email : </strong>" + dtBillDetails.Rows[0]["Billing_Email"] + "</td><tr><td><strong>Tel : </strong>" + dtBillDetails.Rows[0]["Billing_PhNo"] + "&nbsp;<strong>Mob : </strong>" + dtBillDetails.Rows[0]["Billing_Mobile"] + "</td></tr></table></td></tr><tr>" +
                    "<td width='100%' colspan='2' height='29' align='center'><strong>Product Details </strong></td>" +
                    "</tr>";
        }

        SqlDataAdapter daa = new SqlDataAdapter(strSQL, conn);
        DataTable dtDetails = new DataTable();
        daa.Fill(dtDetails);
        daa.Dispose();
        if (dtDetails.Rows.Count > 0)
        {
            for (i = 0; i < dtDetails.Rows.Count; i++)
            {
                strBody = strBody + "<tr><td width='100%' height='29' colspan='2' align='left'><strong>ORDER ID: &nbsp;" + dtDetails.Rows[i]["SBillNo"] + " </strong></td></tr>" +

                        "<tr><td height='50' colspan='2'><table width='100%' height='190'  border='1' cellpadding='0' cellspacing='0'><tr><td width='46%' valign='top'><table width='100%' height='177'  border='0' cellpadding='0' cellspacing='0'>" +
                        "<tr><td><strong>Date of Delivery</strong>:" + dtDetails.Rows[i]["DOD"].ToString() + " </td></tr><tr><td><strong>Message With The Gifts</strong>:" + dtDetails.Rows[i]["MWG"].ToString() + "</td>" +
                        "</tr><tr><td><strong>Shipping Details:</strong> " + dtDetails.Rows[i]["Address"].ToString() + " </td></tr><tr><td>" + dtDetails.Rows[i]["Shipping_Email"].ToString() + "</td></tr><tr><td>" +
                        "<strong>Tele</strong>:" + dtDetails.Rows[i]["Shipping_PhNo"].ToString() + "</td></tr><tr><td><strong>Mob</strong>:" + dtDetails.Rows[i]["Shipping_Mobile"].ToString() + "</td>" +
                        "</tr><tr><td height='36'><strong>Special Instruction with The Gift</strong>:" + dtDetails.Rows[i]["SInstruction"].ToString() + " </td> " +
                        "</tr></table></td>" +
                        "<td width='54%' valign='top'><table width='100%' height='179'  border='0' cellpadding='0' cellspacing='0'>" +
                        "<tr><th width='11%' scope='col'>S.No</th><th width='38%' scope='col'>Product Name </th>" +
                        "<th width='12%' scope='col'>Qty</th><th width='18%' scope='col'>Price</th><th width='21%' scope='col'>Total</th></tr>";
                strSQL = "SELECT rgcards_gti24x7.SalesDetails_BothWay.SlNo," +
                        "rgcards_gti24x7.ItemMaster_Server.Item_Name," +
                        "rgcards_gti24x7.SalesDetails_BothWay.QOS," +
                        "rgcards_gti24x7.SalesDetails_BothWay.Price " +
                        " FROM rgcards_gti24x7.SalesMaster_BothWay " +
                        " INNER JOIN rgcards_gti24x7.SalesDetails_BothWay " +
                        "ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.SalesDetails_BothWay.SBillNo)" +
                        " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON " +
                        "(rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
                        " WHERE (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = '" + dtDetails.Rows[i]["SBillNo"] + "')";

                da = new SqlDataAdapter(strSQL, conn);
                DataTable dtProduct = new DataTable();
                da.Fill(dtProduct);
                da.Dispose();
                totalProPrice = 0;
                for (j = 0; j < dtProduct.Rows.Count; j++)
                {
                    proCount = j + 1;
                    intTotQty = intTotQty + Convert.ToInt32(dtProduct.Rows[j]["QOS"]);
                    proPrice = Convert.ToInt32(dtProduct.Rows[j]["QOS"]) * Convert.ToInt32(dtProduct.Rows[j]["Price"]);
                    totalProPrice = totalProPrice + proPrice;

                    strBody = strBody + "<tr><td align=\"center\">" + proCount + "</td><td align=\"center\">" + dtProduct.Rows[j]["Item_Name"] + "</td><td align=\"center\">" + dtProduct.Rows[j]["QOS"] + "</td><td align=\"center\">" + dtProduct.Rows[j]["Price"] + "</td><td align=\"center\">" + proPrice + "</td></tr>";
                }
                strBody = strBody + "<tr><td colspan='2' align=\"center\">Total:&nbsp;</td><td align=\"center\">" + intTotQty + "</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + totalProPrice + "</td></tr>" +
                                    "<tr><td colspan='2' align=\"center\">Discount:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()) + "</td></tr>" +
                                    "<tr><td colspan='2' align=\"center\">Payable:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + Convert.ToDouble(totalProPrice - Convert.ToDecimal(fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()))).ToString("0.00") + "</td></tr>" +
                                    "</table></td></tr></table></td></tr>";
                intTotQty = 0;
                dtProduct.Reset();
            }

            //string status = "";
            //if (Request.QueryString.Get("PT") != null)
            //{
            //    status = Request.QueryString["PT"].ToString();
            //}
            //else
            //{
            //    status = "2";
            //}
            //if (status == "1")
            //{
            //    // EBS
            //    strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
            //                    "<tr><td colspan='2' ><li>Thank you for placing an order with Rakhi24x7.com. Please preseve this order for future reference.</li></td></tr>" +
            //                    "<tr><td colspan='2' ><li>All Rakhi are sent by courier to the recipient. We use the services of Blue Dart to send the Rakhi. We will send you the tracking number once the rakhis are shipped. For Non Rakhi Orders, all deliveries are during the day time between 10 AM to 8 PM. If you have mentioned a specific time of delivery above. we will try our level best to follow the same.</li></td></tr>" +
            //                    "<tr><td colspan='2' ><li>Rakhi24x7 is a part of GiftsToIndia24x7.com network. Your Credit Card statement will show a charge as www.GiftsToIndia24x7.com or www.GiftstoIndia24 Kolkata.</li></td></tr>" +
            //                    "<tr><td colspan='2' ><li>For any assistance, please email us at sales@rakhi24x7.com stating your order no.</li></td></tr>" +
            //                    "<tr><td colspan='2' ><li>All online purchases will be billed in your local currency</li></ul></td></tr>";
            //}
            //else
            //{
            //    //PAYPAL
            //    strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
            //                    "<tr><td colspan='2'><li>Thank you for placing an order with Rakhi24x7.com. Please preseve this order for future reference.</li></td></tr>" +
            //                    "<tr><td colspan='2'><li>All Rakhi are sent by courier to the recipient. We use the services of Blue Dart to send the Rakhi. We will send you the tracking number once the rakhis are shipped. For Non Rakhi Orders, all deliveries are during the day time between 10 AM to 8 PM. If you have mentioned a specific time of delivery above. we will try our level best to follow the same.</li></td></tr>" +
            //                    "<tr><td colspan='2'><li>Rakhi24x7 is a part of GiftsToIndia24x7.com network. Your Credit Card statement will show a charge as PAYPAL *GIFTSTOINDI.</li></td></tr>" +
            //                    "<tr><td colspan='2'><li>For any assistance, please email us at sales@rakhi24x7.com stating your order no.</li></td></tr>" +
            //                    "<tr><td colspan='2'><li>All online purchases will be billed in your local currency</li></ul</td></tr>>";
            //}
            strBody = strBody + "<strong>Declaration:</strong></td></tr><tr><td width='100%' colspan='2'><ul type='circle'><li>We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.</li></ul>" +
                    "</td></tr><tr><td width='100%' colspan='2'>Sales Team</td></tr>" +
                    "<tr><td width='100%' colspan='2'>" + SiteName + "</td></tr><tr><td width='100%' colspan='2'>+91.933.953.0030</td></tr>" +
                    "</tr></table></font></body></html>";
            sendingMailRakhi(dtBillDetails.Rows[0]["Billing_Email"].ToString(), "" + SiteName + "<sales@giftstoindia24x7.com>", strBody, "Wait Mail Against your order", strComboId);
        }
        conn.Close();
    }
    //public void mailWaitRakhi(string strComboId, int sbillCombo)
    //{
    //    if (conn.State.ToString() == "Closed")
    //    {
    //        conn.Open();
    //    }

    //    string strBody = "";
    //    string strSQL = "";
    //    int i = 0;
    //    int j = 0;
    //    int proCount = 0;
    //    int proPrice = 0;
    //    int totalProPrice = 0;
    //    int intTotQty = 0;

    //    strBody = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'" +
    //            "'http://www.w3.org/TR/html4/loose.dtd'> " +
    //            "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'> " +
    //            " <title>Untitled Document</title></head><body><font face='verdana' size=2> " +
    //            " <table width='100%' height='330' border='0'><tr><td colspan='2'><img src='http://www.giftstoindia24x7.com/PicturesForRakhi/new_header.jpg' width='600' height='108'></td></tr>";
    //    // "<tr><td height='37' width='100%' cellpadding='0' cellspacing='0'>";

    //    if (sbillCombo == 0)
    //    {
    //        strSQL = "SELECT rgcards_gti24x7.BillingDetails_Bothway.Billing_Name, " +
    //                "(CONVERT(varchar(100),rgcards_gti24x7.BillingDetails_Bothway.Billing_Address1) +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_City +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PinCode +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_State +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Country) as Address," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Email, " +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PhNo, " +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Mobile " +
    //                " FROM rgcards_gti24x7.ComboSbill_Relation" +
    //                " INNER JOIN rgcards_gti24x7.BillingDetails_Bothway " +
    //                " ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.BillingDetails_Bothway.SBillNo)" +
    //                " WHERE (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";
    //    }
    //    else
    //    {
    //        strSQL = "SELECT rgcards_gti24x7.BillingDetails_Bothway.Billing_Name," +
    //                "(convert(VARCHAR(100), rgcards_gti24x7.BillingDetails_Bothway.Billing_Address1)+" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_City +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PinCode +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_State +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Country ) as Address," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Email," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PhNo," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Mobile " +
    //                " FROM rgcards_gti24x7.BillingDetails_Bothway " +
    //                " WHERE (rgcards_gti24x7.BillingDetails_Bothway.SBillNo = '" + strComboId + "')";
    //    }

    //    SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
    //    DataTable dtBillDetails = new DataTable();

    //    da.Fill(dtBillDetails);
    //    if (sbillCombo == 0)
    //    {
    //        strSQL = "SELECT " +
    //                "rgcards_gti24x7.ComboSbill_Relation.SBillNo," +
    //                "(rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name+" +
    //                "' '+CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1)+" +
    //                "' '+CONVERT(varchar(100),rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2)+" +
    //                "'  '+rgcards_gti24x7.City_Server.City_Name+" +
    //                "'  '+rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode +" +
    //                "'  '+rgcards_gti24x7.State_Server.State_Name +" +
    //                "'  '+rgcards_gti24x7.Country_Server.Country_Name) as Address," +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile ," +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email," +
    //                " CONVERT(CHAR(19),rgcards_gti24x7.SalesMaster_BothWay.DOD,106) AS DOD, " +
    //                " rgcards_gti24x7.SalesMaster_BothWay.MWG," +
    //                " rgcards_gti24x7.SalesMaster_BothWay.SInstruction " +
    //                " FROM rgcards_gti24x7.ComboSbill_Relation " +
    //                " INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo) " +
    //                " INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id) " +
    //                " INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id) " +
    //                " INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id) " +
    //                " INNER JOIN rgcards_gti24x7.SalesMaster_BothWay ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.SalesMaster_BothWay.SBillNo)" +
    //                " WHERE  (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";
    //    }
    //    else if (sbillCombo == 1)
    //    {
    //        strSQL = "SELECT rgcards_gti24x7.SalesMaster_BothWay.SBillNo," +
    //                " (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name +" +
    //                "' '+CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1)+" +
    //                "' '+CONVERT(varchar(100),rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2)+" +
    //                "'  '+rgcards_gti24x7.City_Server.City_Name+" +
    //                "'  '+rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode + " +
    //                "'  '+rgcards_gti24x7.State_Server.State_Name +" +
    //                "'  '+rgcards_gti24x7.Country_Server.Country_Name) as Address, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email, " +
    //                " CONVERT(CHAR(19),rgcards_gti24x7.SalesMaster_BothWay.DOD,106) AS DOD, " +
    //                " rgcards_gti24x7.SalesMaster_BothWay.MWG," +
    //                " rgcards_gti24x7.SalesMaster_BothWay.SInstruction" +
    //                " FROM " +
    //                " rgcards_gti24x7.SalesMaster_BothWay " +
    //                " INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo)" +
    //                " INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id)" +
    //                " INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id)" +
    //                " INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id)" +
    //                " WHERE" +
    //                " (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = '" + strComboId + "')";
    //    }
    //    if (dtBillDetails.Rows.Count > 0)
    //    {
    //        strBody = strBody + "<tr><td height='37' colspan='2' width='100%'><p>Dear " + dtBillDetails.Rows[0]["Billing_Name"] + ",</p>&nbsp;&nbsp;&nbsp;&nbsp;" +
    //                "Thank you for placing your order with us. The gifts will be delivered as per your instruction. Kindly review the below information related to your order.</p>" +
    //                "</td></tr><tr><td width='30%'>" +
    //                "<table width='100%' height='125'  border='1' cellpadding='0' cellspacing='0'>" +
    //                "<tr><td><strong>Order Id</strong>:" + strComboId.ToString() + " </td></tr>" +
    //                "<tr><td><strong>Email</strong>:<a href='mailTo:sales@rakhi24x7.com'>sales@rakhi24x7.com</a></td></tr>" +
    //                "<tr>" +
    //                "<td><strong>Website</strong>:<a href='http://www.rakhi24x7.com'>http://www.rakhi24x7.com</a></td></tr><tr><td><strong>IP Address</strong>:" + Request.UserHostAddress.ToString() + "</td></tr>" +
    //                "</table></td><td width='70%'>" +
    //                "<table width='100%' height='126'  border='1' cellpadding='0' cellspacing='0'>" +
    //                "<tr><td><strong>Billing Details</strong> : </td></tr><tr> <td>" + dtBillDetails.Rows[0]["Billing_Name"] + "</td></tr><tr><td><strong>Add : </strong>" + dtBillDetails.Rows[0]["Address"] + "</td>" +
    //                "</tr><tr><td><strong>Email : </strong>" + dtBillDetails.Rows[0]["Billing_Email"] + "</td><tr><td><strong>Tel : </strong>" + dtBillDetails.Rows[0]["Billing_PhNo"] + "&nbsp;<strong>Mob : </strong>" + dtBillDetails.Rows[0]["Billing_Mobile"] + "</td></tr></table></td></tr><tr>" +
    //                "<td width='100%' colspan='2' height='29' align='center'><strong>Product Details </strong></td>" +
    //                "</tr>";
    //    }

    //    SqlDataAdapter daa = new SqlDataAdapter(strSQL, conn);
    //    DataTable dtDetails = new DataTable();
    //    daa.Fill(dtDetails);
    //    daa.Dispose();
    //    if (dtDetails.Rows.Count > 0)
    //    {
    //        for (i = 0; i < dtDetails.Rows.Count; i++)
    //        {
    //            strBody = strBody + "<tr><td width='100%' height='29' colspan='2' align='left'><strong>ORDER ID: &nbsp;" + dtDetails.Rows[i]["SBillNo"] + " </strong></td></tr>" +

    //                    "<tr><td height='50' colspan='2'><table width='100%' height='190'  border='1' cellpadding='0' cellspacing='0'><tr><td width='46%' valign='top'><table width='100%' height='177'  border='0' cellpadding='0' cellspacing='0'>" +
    //                    "<tr><td><strong>Date of Delivery</strong>:" + dtDetails.Rows[i]["DOD"].ToString() + " </td></tr><tr><td><strong>Message With The Gifts</strong>:" + dtDetails.Rows[i]["MWG"].ToString() + "</td>" +
    //                    "</tr><tr><td><strong>Shipping Details:</strong> " + dtDetails.Rows[i]["Address"].ToString() + " </td></tr><tr><td>" + dtDetails.Rows[i]["Shipping_Email"].ToString() + "</td></tr><tr><td>" +
    //                    "<strong>Tele</strong>:" + dtDetails.Rows[i]["Shipping_PhNo"].ToString() + "</td></tr><tr><td><strong>Mob</strong>:" + dtDetails.Rows[i]["Shipping_Mobile"].ToString() + "</td>" +
    //                    "</tr><tr><td height='36'><strong>Special Instruction with The Gift</strong>:" + dtDetails.Rows[i]["SInstruction"].ToString() + " </td> " +
    //                    "</tr></table></td>" +
    //                    "<td width='54%' valign='top'><table width='100%' height='179'  border='0' cellpadding='0' cellspacing='0'>" +
    //                    "<tr><th width='11%' scope='col'>S.No</th><th width='38%' scope='col'>Product Name </th>" +
    //                    "<th width='12%' scope='col'>Qty</th><th width='18%' scope='col'>Price</th><th width='21%' scope='col'>Total</th></tr>";
    //            strSQL = "SELECT rgcards_gti24x7.SalesDetails_BothWay.SlNo," +
    //                    "rgcards_gti24x7.ItemMaster_Server.Item_Name," +
    //                    "rgcards_gti24x7.SalesDetails_BothWay.QOS," +
    //                    "rgcards_gti24x7.SalesDetails_BothWay.Price " +
    //                    " FROM rgcards_gti24x7.SalesMaster_BothWay " +
    //                    " INNER JOIN rgcards_gti24x7.SalesDetails_BothWay " +
    //                    "ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.SalesDetails_BothWay.SBillNo)" +
    //                    " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON " +
    //                    "(rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
    //                    " WHERE (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = '" + dtDetails.Rows[i]["SBillNo"] + "')";

    //            da = new SqlDataAdapter(strSQL, conn);
    //            DataTable dtProduct = new DataTable();
    //            da.Fill(dtProduct);
    //            da.Dispose();
    //            totalProPrice = 0;
    //            for (j = 0; j < dtProduct.Rows.Count; j++)
    //            {
    //                proCount = j + 1;
    //                intTotQty = intTotQty + Convert.ToInt32(dtProduct.Rows[j]["QOS"]);
    //                proPrice = Convert.ToInt32(dtProduct.Rows[j]["QOS"]) * Convert.ToInt32(dtProduct.Rows[j]["Price"]);
    //                totalProPrice = totalProPrice + proPrice;

    //                strBody = strBody + "<tr><td align=\"center\">" + proCount + "</td><td align=\"center\">" + dtProduct.Rows[j]["Item_Name"] + "</td><td align=\"center\">" + dtProduct.Rows[j]["QOS"] + "</td><td align=\"center\">" + dtProduct.Rows[j]["Price"] + "</td><td align=\"center\">" + proPrice + "</td></tr>";
    //            }
    //            strBody = strBody + "<tr><td colspan='2' align=\"center\">Total:&nbsp;</td><td align=\"center\">" + intTotQty + "</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + totalProPrice + "</td></tr>" +
    //                                "<tr><td colspan='2' align=\"center\">Discount:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()) + "</td></tr>" +
    //                                "<tr><td colspan='2' align=\"center\">Payable:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + (totalProPrice - Convert.ToDecimal(fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()))) + "</td></tr>" +
    //                                "</table></td></tr></table></td></tr>";
    //            intTotQty = 0;
    //            dtProduct.Reset();
    //        }

    //        //string status = "";
    //        //if (Request.QueryString.Get("PT") != null)
    //        //{
    //        //    status = Request.QueryString["PT"].ToString();
    //        //}
    //        //else
    //        //{
    //        //    status = "2";
    //        //}
    //        //if (status == "1")
    //        //{
    //        //    // EBS
    //        //    strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>Thank you for placing an order with Rakhi24x7.com. Please preseve this order for future reference.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>All Rakhi are sent by courier to the recipient. We use the services of Blue Dart to send the Rakhi. We will send you the tracking number once the rakhis are shipped. For Non Rakhi Orders, all deliveries are during the day time between 10 AM to 8 PM. If you have mentioned a specific time of delivery above. we will try our level best to follow the same.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>Rakhi24x7 is a part of GiftsToIndia24x7.com network. Your Credit Card statement will show a charge as www.GiftsToIndia24x7.com or www.GiftstoIndia24 Kolkata.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>For any assistance, please email us at sales@rakhi24x7.com stating your order no.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>All online purchases will be billed in your local currency</li></ul></td></tr>";
    //        //}
    //        //else
    //        //{
    //        //    //PAYPAL
    //        //    strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>Thank you for placing an order with Rakhi24x7.com. Please preseve this order for future reference.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>All Rakhi are sent by courier to the recipient. We use the services of Blue Dart to send the Rakhi. We will send you the tracking number once the rakhis are shipped. For Non Rakhi Orders, all deliveries are during the day time between 10 AM to 8 PM. If you have mentioned a specific time of delivery above. we will try our level best to follow the same.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>Rakhi24x7 is a part of GiftsToIndia24x7.com network. Your Credit Card statement will show a charge as PAYPAL *GIFTSTOINDI.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>For any assistance, please email us at sales@rakhi24x7.com stating your order no.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>All online purchases will be billed in your local currency</li></ul</td></tr>>";
    //        //}
    //        strBody = strBody + "<strong>Declaration:</strong></td></tr><tr><td width='100%' colspan='2'><ul type='circle'><li>We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.</li></ul>" +
    //                "</td></tr><tr><td width='100%' colspan='2'>Sales Team</td></tr>" +
    //                "<tr><td width='100%' colspan='2'>rakhi24x7.com</td></tr><tr><td width='100%' colspan='2'> 0091-93391-77995</td></tr>" +
    //                "</tr></table></font></body></html>";
    //        sendingMailRakhi(dtBillDetails.Rows[0]["Billing_Email"].ToString(), "Rakhi24x7<sales@rakhi24x7.com>", strBody, "Wait Mail Against your order", strComboId);
    //    }
    //    conn.Close();
    //}
    //public void mailWaitRakhi(string strComboId, int sbillCombo)
    //{
    //    if (conn.State.ToString() == "Closed")
    //    {
    //        conn.Open();
    //    }

    //    string strBody = "";
    //    string strSQL = "";
    //    int i = 0;
    //    int j = 0;
    //    int proCount = 0;
    //    int proPrice = 0;
    //    int totalProPrice = 0;
    //    int intTotQty = 0;

    //    strBody = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'" +
    //            "'http://www.w3.org/TR/html4/loose.dtd'> " +
    //            "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'> " +
    //            " <title>Untitled Document</title></head><body><font face='verdana' size=2> " +
    //            " <table width='100%' height='330' border='0'><tr><td colspan='2'><img src='http://www.giftstoindia24x7.com/PicturesForRakhi/new_header.jpg' width='600' height='108'></td></tr><tr><td height='37' width='100%' cellpadding='0' cellspacing='0'>";

    //    if (sbillCombo == 0)
    //    {
    //        strSQL = "SELECT rgcards_gti24x7.BillingDetails_Bothway.Billing_Name, " +
    //                "(CONVERT(varchar(100),rgcards_gti24x7.BillingDetails_Bothway.Billing_Address1) +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_City +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PinCode +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_State +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Country) as Address," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Email, " +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PhNo, " +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Mobile " +
    //                " FROM rgcards_gti24x7.ComboSbill_Relation" +
    //                " INNER JOIN rgcards_gti24x7.BillingDetails_Bothway " +
    //                " ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.BillingDetails_Bothway.SBillNo)" +
    //                " WHERE (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";
    //    }
    //    else
    //    {
    //        strSQL = "SELECT rgcards_gti24x7.BillingDetails_Bothway.Billing_Name," +
    //                "(convert(VARCHAR(100), rgcards_gti24x7.BillingDetails_Bothway.Billing_Address1)+" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_City +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PinCode +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_State +" +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Country ) as Address," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Email," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_PhNo," +
    //                " rgcards_gti24x7.BillingDetails_Bothway.Billing_Mobile " +
    //                " FROM rgcards_gti24x7.BillingDetails_Bothway " +
    //                " WHERE (rgcards_gti24x7.BillingDetails_Bothway.SBillNo = '" + strComboId + "')";
    //    }

    //    SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
    //    DataTable dtBillDetails = new DataTable();

    //    da.Fill(dtBillDetails);
    //    if (sbillCombo == 0)
    //    {
    //        strSQL = "SELECT " +
    //                "rgcards_gti24x7.ComboSbill_Relation.SBillNo," +
    //                "(rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name+" +
    //                "' '+CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1)+" +
    //                "' '+CONVERT(varchar(100),rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2)+" +
    //                "'  '+rgcards_gti24x7.City_Server.City_Name+" +
    //                "'  '+rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode +" +
    //                "'  '+rgcards_gti24x7.State_Server.State_Name +" +
    //                "'  '+rgcards_gti24x7.Country_Server.Country_Name) as Address," +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile ," +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email," +
    //                " CONVERT(CHAR(19),rgcards_gti24x7.SalesMaster_BothWay.DOD,106) AS DOD, " +
    //                " rgcards_gti24x7.SalesMaster_BothWay.MWG," +
    //                " rgcards_gti24x7.SalesMaster_BothWay.SInstruction " +
    //                " FROM rgcards_gti24x7.ComboSbill_Relation " +
    //                " INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo) " +
    //                " INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id) " +
    //                " INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id) " +
    //                " INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id) " +
    //                " INNER JOIN rgcards_gti24x7.SalesMaster_BothWay ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.SalesMaster_BothWay.SBillNo)" +
    //                " WHERE  (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";
    //    }
    //    else if (sbillCombo == 1)
    //    {
    //        strSQL = "SELECT rgcards_gti24x7.SalesMaster_BothWay.SBillNo," +
    //                " (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name +" +
    //                "' '+CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1)+" +
    //                "' '+CONVERT(varchar(100),rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2)+" +
    //                "'  '+rgcards_gti24x7.City_Server.City_Name+" +
    //                "'  '+rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode + " +
    //                "'  '+rgcards_gti24x7.State_Server.State_Name +" +
    //                "'  '+rgcards_gti24x7.Country_Server.Country_Name) as Address, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile, " +
    //                " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email, " +
    //                " CONVERT(CHAR(19),rgcards_gti24x7.SalesMaster_BothWay.DOD,106) AS DOD, " +
    //                " rgcards_gti24x7.SalesMaster_BothWay.MWG," +
    //                " rgcards_gti24x7.SalesMaster_BothWay.SInstruction" +
    //                " FROM " +
    //                " rgcards_gti24x7.SalesMaster_BothWay " +
    //                " INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo)" +
    //                " INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id)" +
    //                " INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id)" +
    //                " INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id)" +
    //                " WHERE" +
    //                " (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = '" + strComboId + "')";
    //    }
    //    if (dtBillDetails.Rows.Count > 0)
    //    {
    //        strBody = strBody + "<tr><td height='37' colspan='2' width='100%'><p>Dear " + dtBillDetails.Rows[0]["Billing_Name"] + ",</p>&nbsp;&nbsp;&nbsp;&nbsp;" +
    //                "Thank you for placing your order with us. The gifts will be delivered as per your instruction. Kindly review the below information related to your order.</p>" +
    //                "</td></tr><tr><td width='30%'>" +
    //                "<table width='100%' height='125'  border='1' cellpadding='0' cellspacing='0'>" +
    //                "<tr><td><strong>Order Id</strong>:" + strComboId.ToString() + " </td></tr>" +
    //                "<tr><td><strong>Email</strong>:<a href='mailTo:sales@rakhi24x7.com'>sales@rakhi24x7.com</a></td></tr>" +
    //                "<tr>" +
    //                "<td><strong>Website</strong>:<a href='http://www.rakhi24x7.com'>http://www.rakhi24x7.com</a></td></tr><tr><td><strong>IP Address</strong>:" + Request.UserHostAddress.ToString() + "</td></tr>" +
    //                "</table></td><td width='70%'><table width='100%' height='126'  border='1' cellpadding='0' cellspacing='0'>" +
    //                "<tr><td><strong>Billing Details</strong> : </td></tr><tr> <td>" + dtBillDetails.Rows[0]["Billing_Name"] + "</td></tr><tr><td>" + dtBillDetails.Rows[0]["Address"] + "</td>" +
    //                "</tr><tr><td>" + dtBillDetails.Rows[0]["Billing_Email"] + "</td><tr><td>" + dtBillDetails.Rows[0]["Billing_PhNo"] + "&nbsp;" + dtBillDetails.Rows[0]["Billing_Mobile"] + "</td></tr></table></td></tr><tr>" +
    //                "<td width='100%' colspan='2' height='29' align='center'><strong>Product Details </strong></td>" +
    //                "</tr>";
    //    }

    //    SqlDataAdapter daa = new SqlDataAdapter(strSQL, conn);
    //    DataTable dtDetails = new DataTable();
    //    daa.Fill(dtDetails);
    //    daa.Dispose();
    //    if (dtDetails.Rows.Count > 0)
    //    {
    //        for (i = 0; i < dtDetails.Rows.Count; i++)
    //        {
    //            strBody = strBody + "<tr><td width='100%' height='29' colspan='2' align='left'><strong>ORDER ID: &nbsp;" + dtDetails.Rows[i]["SBillNo"] + " </strong></td></tr>" +

    //                    "<tr><td height='50' colspan='2'><table width='100%' height='190'  border='1' cellpadding='0' cellspacing='0'><tr><td width='46%' valign='top'><table width='100%' height='177'  border='1' cellpadding='0' cellspacing='0'>" +
    //                    "<tr><td><strong>Date of Delivery</strong>:" + dtDetails.Rows[i]["DOD"].ToString() + " </td></tr><tr><td><strong>Message With The Gifts</strong>:" + dtDetails.Rows[i]["MWG"].ToString() + "</td>" +
    //                    "</tr><tr><td><strong>Shipping Details:</strong> " + dtDetails.Rows[i]["Address"].ToString() + " </td></tr><tr><td>" + dtDetails.Rows[i]["Shipping_Email"].ToString() + "</td></tr><tr><td>" +
    //                    "<strong>Tele</strong>:" + dtDetails.Rows[i]["Shipping_PhNo"].ToString() + "</td></tr><tr><td><strong>Mob</strong>:" + dtDetails.Rows[i]["Shipping_Mobile"].ToString() + "</td>" +
    //                    "</tr><tr><td height='36'><strong>Special Instruction with The Gift</strong>:" + dtDetails.Rows[i]["SInstruction"].ToString() + " </td> " +
    //                    "</tr></table></td>" +
    //                    "<td width='54%' valign='top'><table width='100%' height='179'  border='1' cellpadding='0' cellspacing='0'>" +
    //                    "<tr><th width='11%' scope='col'>S.No</th><th width='38%' scope='col'>Product Name </th>" +
    //                    "<th width='12%' scope='col'>Qty</th><th width='18%' scope='col'>Price</th><th width='21%' scope='col'>Total</th></tr>";
    //            strSQL = "SELECT rgcards_gti24x7.SalesDetails_BothWay.SlNo," +
    //                    "rgcards_gti24x7.ItemMaster_Server.Item_Name," +
    //                    "rgcards_gti24x7.SalesDetails_BothWay.QOS," +
    //                    "rgcards_gti24x7.SalesDetails_BothWay.Price " +
    //                    " FROM rgcards_gti24x7.SalesMaster_BothWay " +
    //                    " INNER JOIN rgcards_gti24x7.SalesDetails_BothWay " +
    //                    "ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.SalesDetails_BothWay.SBillNo)" +
    //                    " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON " +
    //                    "(rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
    //                    " WHERE (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = '" + dtDetails.Rows[i]["SBillNo"] + "')";

    //            da = new SqlDataAdapter(strSQL, conn);
    //            DataTable dtProduct = new DataTable();
    //            da.Fill(dtProduct);
    //            da.Dispose();
    //            totalProPrice = 0;
    //            for (j = 0; j < dtProduct.Rows.Count; j++)
    //            {
    //                proCount = j + 1;
    //                proPrice = Convert.ToInt32(dtProduct.Rows[j]["QOS"]) * Convert.ToInt32(dtProduct.Rows[j]["Price"]);
    //                totalProPrice = totalProPrice + proPrice;
    //                intTotQty = intTotQty + Convert.ToInt32(dtProduct.Rows[j]["QOS"]);
    //                strBody = strBody + "<tr><td>" + proCount + "</td><td>" + dtProduct.Rows[j]["Item_Name"] + "</td><td>" + dtProduct.Rows[j]["QOS"] + "</td><td>" + dtProduct.Rows[j]["Price"] + "</td><td>" + proPrice + "</td></tr>";
    //            }
    //            strBody = strBody + "<tr><td colspan='2' align=\"center\">Total:&nbsp;</td><td align=\"center\">" + intTotQty + "</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + totalProPrice + "</td></tr>" +
    //                                "<tr><td colspan='2' align=\"center\">Discount:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()) + "</td></tr>" +
    //                                "<tr><td colspan='2' align=\"center\">Payable:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + (totalProPrice - Convert.ToDecimal(fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()))) + "</td></tr>" +
    //                                "</table></td></tr></table></td></tr>";
    //            dtProduct.Reset();
    //        }

    //        //string status = "";
    //        //if (Request.QueryString.Get("PT") != null)
    //        //{
    //        //    status = Request.QueryString["PT"].ToString();
    //        //}
    //        //else
    //        //{
    //        //    status = "2";
    //        //}
    //        //if (status == "1")
    //        //{
    //        //    // EBS
    //        //    strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>Thank you for placing an order with Rakhi24x7.com. Please preseve this order for future reference.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>All Rakhi are sent by courier to the recipient. We use the services of Blue Dart to send the Rakhi. We will send you the tracking number once the rakhis are shipped. For Non Rakhi Orders, all deliveries are during the day time between 10 AM to 8 PM. If you have mentioned a specific time of delivery above. we will try our level best to follow the same.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>Rakhi24x7 is a part of GiftsToIndia24x7.com network. Your Credit Card statement will show a charge as www.GiftsToIndia24x7.com or www.GiftstoIndia24 Kolkata.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>For any assistance, please email us at sales@rakhi24x7.com stating your order no.</li></td></tr>" +
    //        //                    "<tr><td colspan='2' ><li>All online purchases will be billed in your local currency</li></ul></td></tr>";
    //        //}
    //        //else
    //        //{
    //        //    //PAYPAL
    //        //    strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>Thank you for placing an order with Rakhi24x7.com. Please preseve this order for future reference.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>All Rakhi are sent by courier to the recipient. We use the services of Blue Dart to send the Rakhi. We will send you the tracking number once the rakhis are shipped. For Non Rakhi Orders, all deliveries are during the day time between 10 AM to 8 PM. If you have mentioned a specific time of delivery above. we will try our level best to follow the same.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>Rakhi24x7 is a part of GiftsToIndia24x7.com network. Your Credit Card statement will show a charge as PAYPAL *GIFTSTOINDI.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>For any assistance, please email us at sales@rakhi24x7.com stating your order no.</li></td></tr>" +
    //        //                    "<tr><td colspan='2'><li>All online purchases will be billed in your local currency</li></ul</td></tr>>";
    //        //}
    //        strBody = strBody + "<strong>Declaration:</strong></td></tr><tr><td width='100%' colspan='2'><ul type='circle'><li>We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.</li></ul>" +
    //                "</td></tr><tr><td width='100%' colspan='2'>Sales Team</td></tr>" +
    //                "<tr><td width='100%' colspan='2'>rakhi24x7.com</td></tr><tr><td width='100%' colspan='2'> 0091-93391-77995</td></tr>" +
    //                "</tr></table></font></body></html>";
    //        sendingMailRakhi(dtBillDetails.Rows[0]["Billing_Email"].ToString(), "Rakhi24x7<sales@rakhi24x7.com>", strBody, "Wait Mail Against your order", strComboId);
    //    }
    //    conn.Close();
    //}
    public void sendingMailRakhi(string strMailTo, string strMailFrom, string strMailBody, string strMailSubject, string id)
    {

        //MailMessage mail = new MailMessage();
        //mail.From = strMailFrom;
        //mail.To = strMailTo;
        //mail.BodyFormat = MailFormat.Html;
        //mail.Body = strMailBody;
        //mail.Subject = "Order No. : " + id + " From Rakhi24x7.com";
        //try
        //{
        //    SmtpMail.Send(mail);
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
        //string strRecipient = "tagBillingDetails.Billing_Email";
        //string strSiteMailId = "sales@tagSales.SiteName";
        //string strSenderName = strSiteMailId;
        //string strFrom = "tagSales.SiteName";

        //MailMessage mail1 = new MailMessage();
        //mail1.BodyFormat = MailFormat.Html;
        //mail1.Body = strMailBody;
        //mail1.To = "sales@rakhi24x7.com";
        //mail1.From = "Rakhi24x7 <sales@rakhi24x7.com>";
        // mail1.Subject = "Order No. : " + id + " From Rakhi24x7.com" + "STATUS [Wait]";
        DataTable dt = fnGrtOrderCombo(id);
        string strPaymentGateNm = fnGetGateWayName(dt.Rows[0]["SBillNo"].ToString());
        string strSubject = "Order No. : " + id + " - " + strGenSitename + " - " + "" + strPaymentGateNm + " [Wait-Resend]";
        string strMailError = "";
        ////try
        ////{
        ////    SmtpMail.Send(mail1);
        ////}
        ////catch (Exception ex)
        ////{
        ////    Response.Write("Send Failure :" + ex.Message);
        ////}


        //chnaged
        //if (objCommonFunction.SendMail("sales@giftstoindia24x7.com", strMailTo, strMailTo, strMailBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
        //{
        //    strMailError = "The wait mail is been sent.";
        //}
        //else
        //{
        //    strMailError = strError;
        //}




    }
    public string fnGetGateWayName(string strOrderNo)
    {
        string strName = "";
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }

        string strSQL = "SELECT " +
                          "" + strSchema + ".Payment_Gateway_Master.Name" +
                        " FROM " +
                          "" + strSchema + ".Order_Pg_Details" +
                          " INNER JOIN " + strSchema + ".Payment_Gateway_Master ON (" + strSchema + ".Order_Pg_Details.gatewayId = " + strSchema + ".Payment_Gateway_Master.ID)" +
                        " WHERE" +
                          "(" + strSchema + ".Order_Pg_Details.SBillNo = '" + strOrderNo + "')";
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            strName = dr["Name"].ToString();
        }
        conn.Close();
        return strName;
    }

    //===================discount=============================
    public decimal fnGetDiscount(string strSbillNo)
    {
        decimal decDisc = 0;
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }

        string strSQL = "SELECT " +
                          "rgcards_gti24x7.Discount_Code_Master.Value" +
                        " FROM " +
                           "rgcards_gti24x7.Discount_Code_Master" +
                        " WHERE " +
                          "(rgcards_gti24x7.Discount_Code_Master.OrderId = '" + strSbillNo + "')";
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            decDisc = Convert.ToDecimal(dr["Value"]);
        }
        conn.Close();
        return decDisc;
    }
    //design for total and discount
    public void fnTotNDisc(ref string strbody, decimal decTotAmt, string strSbillNo, int intTotQty)
    {
        strbody = strbody + "<ul class=\"totalList\">" +
                                "<li class=\"width3\"><strong>Total Qty</strong></li>" +
                                "<li class=\"width2\">" + intTotQty + "</li>" +
                                "<li class=\"width7\">Rs. " + decTotAmt + "/" + fnFormatedCurr(decTotAmt) + "</li>" +
                                "</ul>" +
                                "<ul class=\"totalList\">" +
                                "<li class=\"width8\"><strong>Total</strong></li>" +
                                "<li class=\"width9\">Rs. " + decTotAmt + "/" + fnFormatedCurr(decTotAmt) + "</li>" +
                                "</ul>" +
                                "<ul class=\"totalList\">" +
                                "<li class=\"width8\"><strong>Discount</strong></li>" +
                                "<li class=\"width9\">Rs. " + fnGetDiscount(strSbillNo) + "/" + fnFormatedCurr(fnGetDiscount(strSbillNo)) + "</li>" +
                                "</ul>" +
                                "<ul class=\"totalList\">" +
                                "<li class=\"width8\"><strong>Payable</strong></li>" +
                                "<li class=\"width9\">Rs." + Convert.ToDouble(decTotAmt - fnGetDiscount(strSbillNo)).ToString("0.00") + "/" + fnFormatedCurr((decTotAmt - fnGetDiscount(strSbillNo))) + "</li>" +
                                "</ul>";

        grnddis = grnddis + fnGetDiscount(strSbillNo);
    }
    //end
    //==========get converted currency==================
    public string fnConvertedCurr(string strOrderNo)
    {
        decimal decCurr = 0;
        string strCurrSym = "";
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }

        string strSQL = "SELECT " +
                        "" + strSchema + ".curency_server.currency_value," + strSchema + ".curency_server.currency_symbol FROM " + strSchema + ".curency_server INNER JOIN " + strSchema + ".salesmaster_bothway" +
                        " ON " + strSchema + ".curency_server.currency_id=" + strSchema + ".salesmaster_bothway.currency_id" +
                        " WHERE " + strSchema + ".curency_server.currency_id=1 AND " + strSchema + ".salesmaster_bothway.sbillno='" + strOrderNo + "'";
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            decCurr = Convert.ToDecimal(dr["currency_value"]);
            strCurrSym = dr["currency_symbol"].ToString();
        }
        conn.Close();
        return decCurr.ToString() + "^" + strCurrSym;
    }
    //=====================end=========================
    protected string fnFormatedCurr(decimal decRs)
    {
        return strConvertedCurrSym + "." + Convert.ToDecimal((Convert.ToDecimal(decRs) / decConvertedCurr)).ToString("0.00");
    }
    protected void fngetComboDetails(string combo, ref int coun, ref string strOrdt, ref double dblTotVal)
    {

        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "" + strSchema + ".SalesMaster_BothWay.Sbill_DOS," +
                        "" + strSchema + ".SalesMaster_BothWay.Sales_ATOT" +
                    " FROM " +
                        "" + strSchema + ".SalesMaster_BothWay" +
                        " INNER JOIN " + strSchema + ".ComboSbill_Relation ON (" + strSchema + ".SalesMaster_BothWay.SBillNo = " + strSchema + ".ComboSbill_Relation.SBillNo)" +
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
                        strOrdt = dr["Sbill_DOS"].ToString();
                        dblTotVal = dblTotVal + Convert.ToDouble(dr["Sales_ATOT"]);

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
    protected void fngetSingleDetails(string strCommon, ref string strOrdt, ref double dblTotVal)
    {
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "" + strSchema + ".SalesMaster_BothWay.Sbill_DOS," +
                        "" + strSchema + ".SalesMaster_BothWay.Sales_ATOT" +
                    " FROM " +
                        "" + strSchema + ".SalesMaster_BothWay" +
                    " WHERE " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SbillNo]='" + strCommon + "')";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {

                        strOrdt = dr["Sbill_DOS"].ToString();
                        dblTotVal = dblTotVal + Convert.ToDouble(dr["Sales_ATOT"]);

                    }
                }
            }
            else
            {

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

    public double GetDiscountPercentage(string strOrderNo)
    {
        double dblVal = 0.00;
        strSql = "SELECT " +
                    "[" + strSchema + "].[Discount_Code_Master].[Code], " +
                    "[" + strSchema + "].[Discount_Code_Master].[Type], " +
                    "[" + strSchema + "].[Discount_Code_Master].[TypeValue], " +
                    "[" + strSchema + "].[Discount_Code_Master].[Limit] " +
                "FROM " +
                    "[" + strSchema + "].[Discount_Code_Master] " +
                "WHERE " +
                    "[" + strSchema + "].[Discount_Code_Master].[OrderId]='" + strOrderNo + "'";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdDiscount = new SqlCommand(strSql, conn);
            SqlDataReader drDiscount = cmdDiscount.ExecuteReader(CommandBehavior.CloseConnection);
            if (drDiscount.HasRows)
            {
                if (drDiscount.Read())
                {
                    if (Convert.ToInt32(drDiscount["Type"]) == 1)
                    {
                        dblVal = Convert.ToDouble(GetTotalOrderValue(strOrderNo) * Convert.ToDouble(drDiscount["TypeValue"]) / 100);
                    }
                    else if (Convert.ToInt32(drDiscount["Type"]) == 2)
                    {
                        dblVal = Convert.ToDouble(drDiscount["TypeValue"]);
                    }
                    else
                    {
                        dblVal = Convert.ToDouble(GetTotalOrderValue(strOrderNo) * Convert.ToDouble(drDiscount["TypeValue"]) / 100);
                    }
                }
            }
            else
            {
                dblVal = 0.00;
            }
        }
        catch (SqlException ex)
        {
            dblVal = 0.00;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return dblVal;
    }
    public double GetTotalOrderValue(string strOrderNo)
    {
        double dblVal = 0.00;
        strSql = "SELECT " +
                    "[" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT] " +
                "FROM " +
                    "[" + strSchema + "].[SalesMaster_BothWay] " +
                "WHERE " +
                    "[" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNo + "'";
        SqlDataReader dr = null;
        strError = "";
        //changed
        //Admin_Module_Works_Select objSelect = new Admin_Module_Works_Select(ref dr, strSql, strSchema, ref strError);
        Admin_Module_Works_Select objSelect = null;

        if (strError == null)
        {
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    dblVal = Convert.ToDouble(dr["Sales_ATOT"]);
                }
            }
        }
        return dblVal;
    }
}

















