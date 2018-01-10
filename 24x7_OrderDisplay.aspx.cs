using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;


public partial class _24x7_OrderDisplay : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
    string strSchema = ConfigurationManager.AppSettings["Schema"].ToString();
    string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
    string strSql = "";
    string strError = "";
    public string strbody = "";
    public int intcountid = 1;
    public int countSbill = 0;
    public ArrayList arr = new ArrayList();
    public string strshippingDetails = "";
    public decimal decConvertedCurr = 0;
    public string strConvertedCurrSym = "";
    public string strpageCSS = "";
    public string strpageheaderimage = "";
    public string strGenSitename = "";
    public string strRedirectBaseDomain = "";
    string strCommon = "";
    public string strOrdNo = "";
    System.Collections.Hashtable htUserDetails = new System.Collections.Hashtable();
    public decimal grndordercount = 1;
    double grndtotal = 0.00, grnddis = 0;
    int countMain = 0;
    public int SiteId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htUserDetails = (Hashtable)Session["userDetails"];
            if ((htUserDetails != null) && (htUserDetails.Count > 0))
            {
                if (htUserDetails.Contains("siteId"))
                {
                    SiteId = Convert.ToInt32(htUserDetails["siteId"]);
                    clsRakhi objRakhiHd = new clsRakhi();
                    objRakhiHd.functionforImageHead(Convert.ToInt32(htUserDetails["siteId"].ToString()), ref strpageCSS, ref strpageheaderimage, ref strRedirectBaseDomain, ref strGenSitename);
                    strGenSitename = objRakhiHd.SentenceCase(strGenSitename.Trim());
                    if ((Convert.ToString(htUserDetails["siteId"].ToString()) == "132") || (Convert.ToString(htUserDetails["siteId"].ToString()) == "154") || (Convert.ToString(htUserDetails["siteId"].ToString()) == "1"))      // If its giftbhejo or ndtv.giftstoindia24x7 or giftstoindia24x7, dont show the payment message
                    {
                        topMsg.Visible = false;
                    }
                    else
                    {
                        topMsg.Visible = true;
                    }
                }
                if (htUserDetails.Contains("ordNo"))
                {
                    strOrdNo = Convert.ToString(htUserDetails["ordNo"]);
                }
                if (htUserDetails.Contains("comboid"))
                {
                    strOrdNo = Convert.ToString(htUserDetails["comboid"]);
                }
            }
            else
            {
                container.Visible = false;
                Response.Write("Your session is expired. Please re-shop.");
            }
        }
    }

    public string getPaymentOptionForm()
    {
        Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
        string strOutput = "";
        cartFunctions objCartFunc = new cartFunctions();
        if ((Session["dtCart"] != null) && (Session["userDetails"] != null))
        {
            orderDetail objOrdDetail = new orderDetail();
            System.Data.DataTable dtCart = (DataTable)Session["dtCart"];
            htUserDetails = (Hashtable)Session["userDetails"];
            if (dtCart.Rows.Count > 0)
            {
                //============checking combo or sbill===================
                if (htUserDetails.Contains("comboid"))
                {
                    strCommon = Convert.ToString(htUserDetails["comboid"].ToString());
                    //DataTable dtCombo = new DataManipulationClass().OrdersForTheCombo(strCommon);
                    //double dblTotal = 0.00;
                }
                if (htUserDetails.Contains("ordNo"))
                {
                    strCommon = Convert.ToString(htUserDetails["ordNo"].ToString());
                }
                //====================================
                if (htUserDetails.Count > 0)
                {
                    int intDiscFlag = 0;
                    try
                    {
                        // Get the discount flag
                        if (Session["flagDiscount"] != null)
                        {
                            if (Convert.ToString(Session["flagDiscount"]) == "1")
                            {
                                intDiscFlag = 1;
                            }
                            else
                            {
                                intDiscFlag = 0;
                            }
                        }
                        else
                        {
                            intDiscFlag = 0;
                        }

                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        SqlCommand cmdOrderInsert = conn.CreateCommand();
                        cmdOrderInsert.CommandText = "[" + strSchema + "].[orderInsert]";
                        //cmdOrderInsert.CommandText = "[" + strSchema + "].[OrderInfo_Insert]";
                        cmdOrderInsert.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramBillName = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingName", SqlDbType.VarChar));
                        paramBillName.Direction = ParameterDirection.Input;
                        paramBillName.Value = Convert.ToString(htUserDetails["Billing_Name"].ToString());

                        SqlParameter paramBillAdd1 = cmdOrderInsert.Parameters.Add(new SqlParameter("@txtBillingAddress1", SqlDbType.VarChar));
                        paramBillAdd1.Direction = ParameterDirection.Input;
                        paramBillAdd1.Value = Convert.ToString(htUserDetails["Billing_Address1"].ToString());

                        SqlParameter paramBillAdd2 = cmdOrderInsert.Parameters.Add(new SqlParameter("@txtBillingAddress2", SqlDbType.VarChar));
                        paramBillAdd2.Direction = ParameterDirection.Input;
                        paramBillAdd2.Value = Convert.ToString(htUserDetails["Billing_Address2"].ToString());

                        SqlParameter paramBillPinCode = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingPinCode", SqlDbType.VarChar));
                        paramBillPinCode.Direction = ParameterDirection.Input;
                        paramBillPinCode.Value = Convert.ToString(htUserDetails["Billing_PinCode"].ToString());

                        SqlParameter paramBillPhNo = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingPhNo", SqlDbType.VarChar));
                        paramBillPhNo.Direction = ParameterDirection.Input;
                        paramBillPhNo.Value = Convert.ToString(htUserDetails["Billing_PhNo"].ToString());

                        SqlParameter paramBillMobNo = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingMobile", SqlDbType.VarChar));
                        paramBillMobNo.Direction = ParameterDirection.Input;
                        paramBillMobNo.Value = Convert.ToString(htUserDetails["Billing_Mobile"].ToString());

                        SqlParameter paramBillEmail = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingEmail", SqlDbType.VarChar));
                        paramBillEmail.Direction = ParameterDirection.Input;
                        paramBillEmail.Value = Convert.ToString(htUserDetails["Billing_Email"].ToString());

                        SqlParameter paramBillCityId = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingCity", SqlDbType.VarChar));
                        paramBillCityId.Direction = ParameterDirection.Input;
                        paramBillCityId.Value = Convert.ToString(htUserDetails["Billing_City"].ToString());

                        SqlParameter paramBillStateId = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingStateId", SqlDbType.VarChar));
                        paramBillStateId.Direction = ParameterDirection.Input;
                        paramBillStateId.Value = Convert.ToString(htUserDetails["Billing_StateId"].ToString());

                        SqlParameter paramBillCountryId = cmdOrderInsert.Parameters.Add(new SqlParameter("@strBillingCountryId", SqlDbType.VarChar));
                        paramBillCountryId.Direction = ParameterDirection.Input;
                        paramBillCountryId.Value = Convert.ToString(htUserDetails["Billing_CountryId"].ToString());

                        SqlParameter paramShipName = cmdOrderInsert.Parameters.Add(new SqlParameter("@strShippingName", SqlDbType.VarChar));
                        paramShipName.Direction = ParameterDirection.Input;
                        paramShipName.Value = Convert.ToString(htUserDetails["Shipping_Name"].ToString());

                        SqlParameter paramShipAdd1 = cmdOrderInsert.Parameters.Add(new SqlParameter("@txtShippingAddress1", SqlDbType.VarChar));
                        paramShipAdd1.Direction = ParameterDirection.Input;
                        paramShipAdd1.Value = Convert.ToString(htUserDetails["Shipping_Address1"].ToString());

                        SqlParameter paramShipAdd2 = cmdOrderInsert.Parameters.Add(new SqlParameter("@txtShippingAddress2", SqlDbType.VarChar));
                        paramShipAdd2.Direction = ParameterDirection.Input;
                        paramShipAdd2.Value = Convert.ToString(htUserDetails["Shipping_Address2"].ToString());

                        SqlParameter paramShipPinCode = cmdOrderInsert.Parameters.Add(new SqlParameter("@strShippingPinCode", SqlDbType.VarChar));
                        paramShipPinCode.Direction = ParameterDirection.Input;
                        paramShipPinCode.Value = Convert.ToString(htUserDetails["Shipping_PinCode"].ToString());

                        SqlParameter paramShipPhNo = cmdOrderInsert.Parameters.Add(new SqlParameter("@strShippingPhNo", SqlDbType.VarChar));
                        paramShipPhNo.Direction = ParameterDirection.Input;
                        paramShipPhNo.Value = Convert.ToString(htUserDetails["Shipping_PhNo"].ToString());

                        SqlParameter paramShipMobNo = cmdOrderInsert.Parameters.Add(new SqlParameter("@strShippingMobile", SqlDbType.VarChar));
                        paramShipMobNo.Direction = ParameterDirection.Input;
                        paramShipMobNo.Value = Convert.ToString(htUserDetails["Shipping_Mobile"].ToString());

                        SqlParameter paramShipEmail = cmdOrderInsert.Parameters.Add(new SqlParameter("@strShippingEmail", SqlDbType.VarChar));
                        paramShipEmail.Direction = ParameterDirection.Input;
                        paramShipEmail.Value = Convert.ToString(htUserDetails["Shipping_Email"].ToString());

                        SqlParameter paramShipCityId = cmdOrderInsert.Parameters.Add(new SqlParameter("@intShippingCityId", SqlDbType.Int));
                        paramShipCityId.Direction = ParameterDirection.Input;
                        paramShipCityId.Value = Convert.ToString(htUserDetails["Shipping_CityId"].ToString());

                        // Newly added to implement other city concept 07.08.08
                        if (htUserDetails.Contains("Shipping_OtherCityName"))
                        {
                            SqlParameter paramShipOtherCityName = cmdOrderInsert.Parameters.Add(new SqlParameter("@shipOtherCityName", SqlDbType.VarChar));
                            paramShipOtherCityName.Direction = ParameterDirection.Input;
                            paramShipOtherCityName.Value = Convert.ToString(htUserDetails["Shipping_OtherCityName"].ToString());
                        }
                        // Newly added to implement other city concept 07.08.08

                        SqlParameter paramShipStateId = cmdOrderInsert.Parameters.Add(new SqlParameter("@intShippingStateId", SqlDbType.Int));
                        paramShipStateId.Direction = ParameterDirection.Input;
                        paramShipStateId.Value = Convert.ToString(htUserDetails["Shipping_StateId"].ToString());

                        SqlParameter paramShipCountryName = cmdOrderInsert.Parameters.Add(new SqlParameter("@intShippingCountry", SqlDbType.VarChar));
                        paramShipCountryName.Direction = ParameterDirection.Input;
                        paramShipCountryName.Value = Convert.ToString(htUserDetails["Shipping_CountryName"].ToString());

                        SqlParameter paramShipCountryId = cmdOrderInsert.Parameters.Add(new SqlParameter("@intShippingCountryId", SqlDbType.Int));
                        paramShipCountryId.Direction = ParameterDirection.Input;
                        paramShipCountryId.Value = Convert.ToString(htUserDetails["Shipping_CountryId"].ToString());

                        SqlParameter paramShipDoD = cmdOrderInsert.Parameters.Add(new SqlParameter("@datetimeDOD", SqlDbType.DateTime));
                        paramShipDoD.Direction = ParameterDirection.Input;
                        paramShipDoD.Value = Convert.ToDateTime(htUserDetails["DOD"].ToString());

                        SqlParameter paramShipMwG = cmdOrderInsert.Parameters.Add(new SqlParameter("@txtMWG", SqlDbType.VarChar));
                        paramShipMwG.Direction = ParameterDirection.Input;
                        paramShipMwG.Value = Convert.ToString(htUserDetails["Shipping_Msg"].ToString());

                        SqlParameter paramShipSinstruction = cmdOrderInsert.Parameters.Add(new SqlParameter("@txtSInstruction", SqlDbType.VarChar));
                        paramShipSinstruction.Direction = ParameterDirection.Input;
                        paramShipSinstruction.Value = Convert.ToString(htUserDetails["Billing_Instructions"].ToString());

                        //SqlParameter paramSiteId = cmdOrderInsert.Parameters.Add(new SqlParameter("@strPOSId", SqlDbType.VarChar));
                        //paramSiteId.Direction = ParameterDirection.Input;
                        //paramSiteId.Value = Convert.ToString(htUserDetails["siteId"].ToString());

                        SqlParameter paramBankId = cmdOrderInsert.Parameters.Add(new SqlParameter("@numBankId", SqlDbType.Int));
                        paramBankId.Direction = ParameterDirection.Input;
                        paramBankId.Value = Convert.ToString(htUserDetails["pgId"].ToString());

                        // New added to save transaction of bank details
                        SqlParameter paramPoptionId = cmdOrderInsert.Parameters.Add(new SqlParameter("@pOptionId", SqlDbType.Int));
                        paramPoptionId.Direction = ParameterDirection.Input;
                        paramPoptionId.Value = Convert.ToString(htUserDetails["poId"].ToString());

                        SqlParameter paramCurrencyId = cmdOrderInsert.Parameters.Add(new SqlParameter("@numCurrencyId", SqlDbType.Int));
                        paramCurrencyId.Direction = ParameterDirection.Input;
                        paramCurrencyId.Value = Convert.ToString(Session["CurrencyId"]);

                        SqlParameter paramIpAdd = cmdOrderInsert.Parameters.Add(new SqlParameter("@strIPAddress", SqlDbType.VarChar));
                        paramIpAdd.Direction = ParameterDirection.Input;
                        paramIpAdd.Value = Request.UserHostAddress.ToString();

                        SqlParameter paramTrack = cmdOrderInsert.Parameters.Add(new SqlParameter("@tintTrack", SqlDbType.TinyInt));
                        paramTrack.Direction = ParameterDirection.Input;
                        paramTrack.Value = 0;

                        StringBuilder strCartDetail = new StringBuilder();
                        for (int i = 0; i < dtCart.Rows.Count; i++)
                        {
                            strCartDetail.Append(dtCart.Rows[i]["prodId"].ToString() + "," + dtCart.Rows[i]["qnty"].ToString() + "|");
                        }
                        SqlParameter paramCartDetail = cmdOrderInsert.Parameters.Add(new SqlParameter("@nvarchrOrderDetails", SqlDbType.NVarChar));
                        paramCartDetail.Direction = ParameterDirection.Input;
                        paramCartDetail.Value = strCartDetail.ToString();

                        SqlParameter paramDiscountFlag = cmdOrderInsert.Parameters.Add(new SqlParameter("@intDiscFlag", SqlDbType.Int));
                        paramDiscountFlag.Direction = ParameterDirection.Input;
                        if ((intDiscFlag == 1) && (Convert.ToString(dtCart.Rows[0]["discCode"]) == "-"))
                        {
                            paramDiscountFlag.Value = 2;
                        }
                        else
                        {
                            paramDiscountFlag.Value = intDiscFlag;
                        }


                        SqlParameter paramDiscountCode = cmdOrderInsert.Parameters.Add(new SqlParameter("@strDisCode", SqlDbType.VarChar));
                        paramDiscountCode.Direction = ParameterDirection.Input;
                        paramDiscountCode.Value = Convert.ToString(htUserDetails["disCode"].ToString());

                        double dblCartTotal = 0.00;
                        strError = "";
                        if (objCartFunc.GetCartTotal(dtCart, ref dblCartTotal, ref strError) == false)
                        {
                            dblCartTotal = 0.00;
                        }
                        SqlParameter paramCartTotal = cmdOrderInsert.Parameters.Add(new SqlParameter("@floatSalesATOT", SqlDbType.Float));
                        paramCartTotal.Direction = ParameterDirection.Input;
                        paramCartTotal.Value = dblCartTotal;

                        if (dblCartTotal != 0.00)
                        {
                            string strSiteName = "";
                            if (objCommonFunction.returnSiteName(Convert.ToInt32(htUserDetails["siteId"].ToString()), ref strSiteName) == false)
                            {
                                strOutput = "Error on retrieving site name. Please press refresh button.";
                            }
                            else
                            {
                                if (!htUserDetails.Contains("siteId"))
                                {
                                    htUserDetails.Add("siteId", Convert.ToInt32(htUserDetails["SiteId"]));
                                }
                                if (!htUserDetails.Contains("siteName"))
                                {
                                    htUserDetails.Add("siteName", strSiteName);
                                }
                                string strOrdNo_Total = "";
                                //if (!htUserDetails.Contains("ordNo"))
                                if (strCommon == "")
                                {
                                    strOrdNo_Total = Convert.ToString(cmdOrderInsert.ExecuteScalar());
                                    //strOrdNo_Total = "GTI21030/2008,10.00";

                                }
                                else
                                {
                                    strOrdNo_Total = strCommon.ToString() + "," + htUserDetails["ordTotal"].ToString();
                                }
                                if (strOrdNo_Total != "")
                                {
                                    string[] strOrderNo = strOrdNo_Total.Split(new char[] { ',' });
                                    // used for tracking user starts
                                    //if (htUserDetails.Contains("siteId"))
                                    //{
                                    //using (svtrack obj = new svtrack(strOrdNo_Total, Convert.ToString(htUserDetails["siteId"]))) { }
                                    //}
                                    // used for tracking user ends
                                    if (strOrderNo[0].ToString() != "")
                                    {
                                        if (!htUserDetails.Contains("ordNo"))
                                        {
                                            htUserDetails.Add("ordNo", strOrderNo[0].ToString());
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("ordNo");
                                            htUserDetails.Add("ordNo", strOrderNo[0].ToString());
                                        }
                                        if (!htUserDetails.Contains("comboid"))
                                        {
                                            if (!htUserDetails.Contains("ordNo"))
                                            {
                                                htUserDetails.Add("ordNo", strOrderNo[0].ToString());
                                            }
                                            else
                                            {
                                                htUserDetails.Remove("ordNo");
                                                htUserDetails.Add("ordNo", strOrderNo[0].ToString());
                                            }
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("comboid");
                                            htUserDetails.Add("comboid", strOrderNo[0].ToString());
                                        }
                                        double dblOrdTotal = Convert.ToDouble(strOrderNo[1].ToString());
                                        if (!htUserDetails.Contains("ordTotal"))
                                        {
                                            //htUserDetails.Add("ordTotal", strOrderNo[1].ToString());                                           
                                            htUserDetails.Add("ordTotal", dblOrdTotal.ToString("0.00"));
                                        }
                                        else
                                        {
                                            htUserDetails.Remove("ordTotal");
                                            //htUserDetails.Add("ordTotal", strOrderNo[1].ToString());
                                            htUserDetails.Add("ordTotal", dblOrdTotal.ToString("0.00"));
                                        }


                                        // Check the flag if wait mail is been sent
                                        if (!htUserDetails.Contains("flagWaitMail"))     // Wait mail not sent
                                        {
                                            if (!htUserDetails.Contains("comboid"))     // Not a combo
                                            {
                                                // If it is coming from complete payment page then check in the Retry Master and 
                                                // increment the order id
                                                if (htUserDetails.Contains("flagCompletePayment"))
                                                {
                                                    int intCounter = 0;
                                                    if (objOrdDetail.checkCancelReturn(htUserDetails["ordNo"].ToString(), htUserDetails["pgId"].ToString(), htUserDetails["poId"].ToString(), ref intCounter, ref strError))
                                                    {
                                                        if (intCounter > 0)
                                                        {
                                                            if (htUserDetails.Contains("ordNoPg"))
                                                            {
                                                                htUserDetails.Remove("ordNoPg");
                                                                htUserDetails.Add("ordNoPg", htUserDetails["ordNo"].ToString() + "-" + intCounter);
                                                            }
                                                            else
                                                            {
                                                                htUserDetails.Add("ordNoPg", htUserDetails["ordNo"].ToString() + "-" + intCounter);
                                                            }
                                                        }
                                                    }
                                                }

                                                if (sendWaitMail(false, strOrderNo[0].ToString(), ref strError))
                                                {
                                                    if (!htUserDetails.Contains("flagWaitMail"))
                                                    {
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                    strOutput = printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    // Execute the pending order mail                                            
                                                    objOrdDetail.paymentGatewayDownMail(strOrderNo[0].ToString(), Convert.ToInt32(htUserDetails["pgId"].ToString()));
                                                }
                                                else
                                                {
                                                    strOutput = "Sending wait mail is failed due to " + strError;
                                                    strOutput = strOutput + "<br/>" + printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    // Execute the pending order mail                                            
                                                    objOrdDetail.paymentGatewayDownMail(strOrderNo[0].ToString(), Convert.ToInt32(htUserDetails["pgId"].ToString()));
                                                }

                                            }
                                            else // Its a combo
                                            {
                                                // If it is coming from complete payment page then check in the Retry Master and 
                                                // increment the order id
                                                if (htUserDetails.Contains("flagCompletePayment"))
                                                {
                                                    int intCounter = 0;
                                                    if (objOrdDetail.checkCancelReturn(htUserDetails["comboid"].ToString(), htUserDetails["pgId"].ToString(), htUserDetails["poId"].ToString(), ref intCounter, ref strError))
                                                    {
                                                        if (intCounter > 0)
                                                        {
                                                            if (htUserDetails.Contains("ordNoPg"))
                                                            {
                                                                htUserDetails.Remove("ordNoPg");
                                                                htUserDetails.Add("ordNoPg", htUserDetails["comboid"].ToString() + "-" + intCounter);
                                                            }
                                                            else
                                                            {
                                                                htUserDetails.Add("ordNoPg", htUserDetails["comboid"].ToString() + "-" + intCounter);
                                                            }
                                                        }
                                                    }
                                                }
                                                if (sendWaitMail(true, strCommon, ref strError))
                                                {
                                                    if (!htUserDetails.Contains("flagWaitMail"))
                                                    {
                                                        htUserDetails.Add("flagWaitMail", 1);
                                                    }
                                                    strOutput = printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    // Execute the pending order mail                                            
                                                    objOrdDetail.paymentGatewayDownMail(strOrderNo[0].ToString(), Convert.ToInt32(htUserDetails["pgId"].ToString()));
                                                }
                                                else
                                                {
                                                    strOutput = "Sending wait mail is failed due to " + strError;
                                                    strOutput = strOutput + "<br/>" + printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    // Execute the pending order mail                                            
                                                    objOrdDetail.paymentGatewayDownMail(strOrderNo[0].ToString(), Convert.ToInt32(htUserDetails["pgId"].ToString()));
                                                }
                                            }
                                        }
                                        else                                          // Wait mail sent
                                        {
                                            if (Convert.ToString(htUserDetails["flagWaitMail"].ToString()) == "1")
                                            {
                                                strOutput = printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                            }
                                            else if (Convert.ToString(htUserDetails["flagWaitMail"].ToString()) == "0")
                                            {
                                                //if (sendWaitMail(htUserDetails, dtCart, ref strError) == true)
                                                if (!htUserDetails.Contains("comboid"))     // Not a combo
                                                {
                                                    if (sendWaitMail(false, strOrderNo[0].ToString(), ref strError))
                                                    {
                                                        if (!htUserDetails.Contains("flagWaitMail"))
                                                        {
                                                            htUserDetails.Add("flagWaitMail", 1);
                                                        }
                                                        strOutput = printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    }
                                                    else
                                                    {
                                                        strOutput = "Sending wait mail is failed due to " + strError;
                                                        strOutput = strOutput + "<br/>" + printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    }
                                                }
                                                else // Its a combo
                                                {
                                                    if (sendWaitMail(true, strCommon, ref strError))
                                                    {
                                                        if (!htUserDetails.Contains("flagWaitMail"))
                                                        {
                                                            htUserDetails.Add("flagWaitMail", 1);
                                                        }
                                                        strOutput = printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    }
                                                    else
                                                    {
                                                        strOutput = "Sending wait mail is failed due to " + strError;
                                                        strOutput = strOutput + "<br/>" + printForm(strOrderNo[0].ToString(), dtCart, htUserDetails);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strOutput = "Error on order number generation. Please press refresh button.";
                                    }
                                }
                            }
                        }
                        else
                        {
                            strOutput = objCartFunc.showNoitem();
                        }
                    }
                    catch (Exception ex)
                    {
                        strOutput = "Error!<br/>" + ex.Message;
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
                else
                {
                    strOutput = objCartFunc.showNoitem();
                }
            }
            else
            {
                strOutput = objCartFunc.showNoitem();
            }
        }
        else
        {
            strOutput = objCartFunc.showNoitem();
        }
        return strOutput;
    }
    private string printForm(string strOrderId, DataTable dtCart, Hashtable htUserDetails)
    {
        string strOutput = "";
        double dblTotal = 0.00;
        StringBuilder sbFormOutput = new StringBuilder();
        //try
        //{
        clsRakhi objRakhiHd = new clsRakhi();
        cartFunctions objCartFunc = new cartFunctions();
        Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
        string strPgName = htUserDetails["pgName"].ToString();
        switch (strPgName)
        {
            case "PayPal":           // PAYPAL
                sbFormOutput.Append("<form id=\"frmPaypal\" method=\"post\" action=\"https://www.paypal.com/cgi-bin/webscr\">");
                sbFormOutput.Append("<div class=\"payment\"> ");
                sbFormOutput.Append("<h4>Thank you for placing your order with us. </h4>");
                sbFormOutput.Append("<p class=\"orderTxt\">The order is not yet complete. Please click on the 'Make Payment' button below to make the payment for the order. The next page will take you to the secured web page of our Payment Gateway - " + htUserDetails["pgNametoShow"].ToString() + " / " + htUserDetails["pgChargeName"].ToString() + "");
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
               // changed by me
                //sbFormOutput.Append("<input type=\"hidden\" id=\"currency_code\" name=\"currency_code\" value=\"" + objCommonFunction.ReturnRemainString(objCommonFunction.FetchCurrencyName(Convert.ToInt32(Session["CurrencyId"])), 3) + "\">");

                //The URL to which the payer’s browser is redirected after completing the payment; 
                //  for example, a URL on your site that displays a “Thank you for your payment” page.
                //  Default: payer is redirected to a PayPal web page. Optional                
                //sbFormOutput.Append("<input type=\"hidden\" id=\"return\" name=\"return\" value=\"http://www.giftstoindia24x7.com/o_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&billing_country=" + htUserDetails["billCountryName"].ToString() + "&bankId=" + htUserDetails["pgId"].ToString() + "&bankname=" + htUserDetails["pgName"].ToString() + "&image_path=http://www." + htUserDetails["siteName"].ToString() + "&sitename=" + htUserDetails["siteName"].ToString() + "\"> ");
                //sbFormOutput.Append("<input type=\"hidden\" id=\"return\" name=\"return\" value=\"http://test.giftstoindia24x7.com/o_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");
                sbFormOutput.Append("<input type=\"hidden\" id=\"return\" name=\"return\" value=\"http://www.giftstoindia24x7.com/24x7_Success.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");

                //cancel_return
                //  A URL to which the payer’s browser is redirected if payment is cancelled; 
                //  for example, a URL on your website that displays a “Payment Canceled” page.
                //  Default: Browser is redirected to a PayPal web page.

                //sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://www.giftstoindia24x7.com/c_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&billing_country=" + htUserDetails["billCountryName"].ToString() + "&bankId=" + htUserDetails["pgId"].ToString() + "&bankname=" + htUserDetails["pgName"].ToString() + "&image_path=http://www." + htUserDetails["siteName"].ToString() + "&sitename=" + htUserDetails["siteName"].ToString() + "\"> ");
                //sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://test.giftstoindia24x7.com/c_form.aspx?sbillno=" + htUserDetails["ordNo"].ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");
                //sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://www.giftstoindia24x7.com/c_form_rakhi.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");
                sbFormOutput.Append("<input type=\"hidden\" id=\"cancel_return\" name=\"cancel_return\" value=\"http://www.giftstoindia24x7.com/24x7_Fail.aspx?sbillno=" + strCommon + "&siteId=" + htUserDetails["siteId"].ToString() + "\"> ");


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
                //sbFormOutput.Append("<input type=\"hidden\" id=\"country\" name=\"country\" value=\"" + objCommonFunction.returnCountryPrefix(htUserDetails["Billing_CountryName"].ToString()) + "\" >");

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

                //sbFormOutput.Append("<input type=\"hidden\" id=\"amount\" name=\"amount\" value=\"" + dblDollarAmt.ToString("0.00") + "\" >");

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
                sbFormOutput.Append("<input type=\"submit\" class=\"paymentBtn\" title=\"Make Payment\" value=\"\"  /><br class=\"clear\" />");

                if (strCommon.ToLower().Contains("com"))
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
                else
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
                //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                //if (htUserDetails.Contains("comboid"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //if (htUserDetails.Contains("ordNo"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //sbFormOutput.Append("<!--- The main Cart Table Ends--->");
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
                if (strCommon.ToLower().Contains("com"))
                {
                    sbFormOutput.Append("<input type=\"hidden\" id=\"transaction[amount]\" name=\"transaction[amount]\" value=\"" + Convert.ToString(fnFinalAmount(strCommon)) + "\" >");
                }
                else
                {
                    sbFormOutput.Append("<input type=\"hidden\" id=\"transaction[amount]\" name=\"transaction[amount]\" value=\"" + htUserDetails["ordTotal"].ToString() + "\" >");
                }
                sbFormOutput.Append("<input type=\"hidden\" id=\"description[description]\" name=\"description[description]\" value=\"" + strCommon.ToString() + "\" >");
                sbFormOutput.Append("<input type=\"hidden\" id=\"description[merchant_txn_number]\" name=\"description[merchant_txn_number]\" value=\"" + strCommon.ToString() + "\" >");
                sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Ends -->");
                sbFormOutput.Append("<input type=\"submit\" class=\"paymentBtn\" title=\"Make Payment\" value=\"\"  /><br class=\"clear\" />");
                sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                //=================================
                if (strCommon.ToLower().Contains("com"))
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
                    //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
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
                else
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
                //==============end
                sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                //if (htUserDetails.Contains("comboid"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //if (htUserDetails.Contains("ordNo"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //sbFormOutput.Append("<!--- The main Cart Table Ends--->");
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

                sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Starts -->");

                sbFormOutput.Append("<input type=\"hidden\" id=\"mrfnbr\" name=\"mrfnbr\" value=\"628315\">");
                if (strCommon.ToLower().Contains("com"))
                {
                    sbFormOutput.Append("<input type=\"hidden\" id=\"Amount\" name=\"Amount\" value=\"" + Convert.ToString(fnFinalAmount(strCommon)) + "\">");
                }
                else
                {
                    sbFormOutput.Append("<input type=\"hidden\" id=\"Amount\" name=\"Amount\" value=\"" + htUserDetails["ordTotal"].ToString() + "\">");
                }
                sbFormOutput.Append("<input type=\"hidden\" id=\"succurl\" name=\"succurl\" value=\"http://www.giftstoindia24x7.com/24x7_Success.aspx?sbillno=" + strCommon.ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\">");

                sbFormOutput.Append("<input type=\"hidden\" id=\"failurl\" name=\"failurl\" value=\"http://www.giftstoindia24x7.com/24x7_Fail.aspx?sbillno=" + strCommon.ToString() + "&siteId=" + htUserDetails["siteId"].ToString() + "\">");

                sbFormOutput.Append("<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"reliablegifts\">");

                sbFormOutput.Append("<input type=\"hidden\" id=\"mordnbr\" name=\"mordnbr\" value=\"" + strCommon.ToString() + "\">");

                sbFormOutput.Append("<!-- Section to print the hidden fields of the particular payment gateway Ends -->");

                // the submit (Make Payment button)
                // sbFormOutput.Append("<input type=\"image\" src=\"Pictures/make_payment.gif\" style=\"width:140px; \" value=\"Make Payment\" alt=\"Make Payment\" tabindex=\"1\"/>");
                sbFormOutput.Append("<input type=\"submit\" class=\"paymentBtn\" title=\"Make Payment\" value=\"\"  /></a><br class=\"clear\" />");


                sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                // sbFormOutput.Append(printShipBillHtml(htUserDetails));
                //=================================
                if (strCommon.ToLower().Contains("com"))
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
                    //sbFormOutput.Append("<li><span>Order Date :</span>" + strOrdt + "</li>");
                    sbFormOutput.Append("<li><span>Order Date :</span>" + dtDos.ToString("dd MMM yyyy") + "</li>");
                    sbFormOutput.Append("<li><span>Total Value :</span>Rs." + inttotval.ToString("0.00") + "</li>");
                    sbFormOutput.Append("<li><span>Payment Method :</span>" + htUserDetails["poName"].ToString() + "</li>");
                    sbFormOutput.Append("<li><span>Delivery Date :</span>" + dtDOD.ToString("dd MMM yyyy") + "</li>");
                    sbFormOutput.Append("</ul>");
                    sbFormOutput.Append("<br class=\"clear\" /><br class=\"clear\" />");
                    sbFormOutput.Append("</div>");
                    sbFormOutput.Append("<br class=\"clear\" />");
                    //===========end=====================
                    sbFormOutput.Append(functionforOredDetailForCOMBO(strCommon));
                }
                else
                {
                    //flag = "1";
                    //sbFormOutput.Append(functionforOrderDetails(strCommon));
                    //=======billing details============
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
                //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                ////=================================
                //if (htUserDetails.Contains("comboid"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //if (htUserDetails.Contains("ordNo"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //    ////sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                //}
                ////==============end
                ////sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                //sbFormOutput.Append("<!--- The main Cart Table Ends--->");
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

                dblTotal = 0.00;
                if (strCommon.ToLower().Contains("com"))
                {
                    dblTotal = fnFinalAmount(strCommon);
                }
                else
                {
                    dblTotal = Convert.ToDouble(htUserDetails["ordTotal"]);
                }

               // libfuncs myUtility = new libfuncs();
                if (htUserDetails.Contains("ordNoPg"))
                {
                    //sbFormOutput.Append("<input type=\"hidden\" id=\"Checksum\" name=\"Checksum\" value=\"" + myUtility.getchecksum("M_rgcards_1491", htUserDetails["ordNoPg"].ToString(), dblTotal.ToString("0.00"), "http://www.rgcards.com/24x7_redirecturl.aspx", "c82kwozdzxqbfco03cnobf0p3tsrdrpe") + "\" >");
                }
                else
                {
                   // sbFormOutput.Append("<input type=\"hidden\" id=\"Checksum\" name=\"Checksum\" value=\"" + myUtility.getchecksum("M_rgcards_1491", htUserDetails["ordNo"].ToString(), dblTotal.ToString("0.00"), "http://www.rgcards.com/24x7_redirecturl.aspx", "c82kwozdzxqbfco03cnobf0p3tsrdrpe") + "\" >");
                }
                sbFormOutput.Append("<input type=\"hidden\" id=\"Redirect_Url\" name=\"Redirect_Url\" value=\"http://www.rgcards.com/24x7_redirecturl.aspx\">");

                sbFormOutput.Append("<input type=\"hidden\" id=\"Merchant_Id\" name=\"Merchant_Id\" value=\"M_rgcards_1491\" >");

                sbFormOutput.Append("<input type=\"hidden\" id=\"Amount\" name=\"Amount\" value=\"" + dblTotal.ToString("0.00") + "\" >");
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

                if (strCommon.ToLower().Contains("com"))
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
                else
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
                //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                //if (htUserDetails.Contains("comboid"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //if (htUserDetails.Contains("ordNo"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                //sbFormOutput.Append("</td>");
                //sbFormOutput.Append("</tr>");
                //sbFormOutput.Append("<tr>");
                //sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                sbFormOutput.Append(objCartFunc.printCartHtml(dtCart));
                sbFormOutput.Append("<!--- The main Cart Table Ends--->");
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
                dblTotal = 0.00;
                if (strCommon.ToLower().Contains("com"))
                {
                    dblTotal = fnFinalAmount(strCommon);
                }
                else
                {
                    dblTotal = Convert.ToDouble(htUserDetails["ordTotal"]);
                }
                sbFormOutput.Append("<input type=\"hidden\" name=\"amount\" value=\"" + dblTotal.ToString("0.00") + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"description\" value=\"" + htUserDetails["ordNo"].ToString().Replace("/", "_") + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"name\" value=\"" + htUserDetails["Billing_FName"].ToString() + " " + htUserDetails["Billing_LName"].ToString() + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"address\" value=\"" + htUserDetails["Billing_Address1"].ToString() + "\">");

                sbFormOutput.Append("<input type=\"hidden\" name=\"city\" value=\"" + htUserDetails["Billing_City"].ToString() + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"state\" value=\"" + htUserDetails["Billing_StateName"].ToString() + "\">");
               // changed by me
                //sbFormOutput.Append("<input type=\"hidden\" name=\"country\" value=\"" + objCommonFunction.returnCountryPrefix(htUserDetails["Billing_CountryName"].ToString(), 3) + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"phone\" value=\"" + htUserDetails["Billing_PhNo"].ToString() + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"postal_code\" value=\"" + htUserDetails["Billing_PinCode"].ToString() + "\">");

                sbFormOutput.Append("<input type=\"hidden\" name=\"email\" value=\"" + htUserDetails["Billing_Email"].ToString() + "\">");

                sbFormOutput.Append("<input type=\"hidden\" name=\"ship_name\" value=\"" + htUserDetails["Shipping_FName"].ToString() + " " + htUserDetails["Shipping_LName"].ToString() + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"ship_address\" value=\"" + htUserDetails["Shipping_Address1"].ToString() + " " + htUserDetails["Shipping_Address2"].ToString() + "\" >");

                sbFormOutput.Append("<input type=\"hidden\" name=\"ship_city\" value=\"" + htUserDetails["Shipping_CityName"].ToString() + "\">");

                sbFormOutput.Append("<input type=\"hidden\" name=\"ship_state\" value=\"" + htUserDetails["Shipping_StateName"].ToString() + "\">");
                //changed by me;
                //sbFormOutput.Append("<input type=\"hidden\" name=\"ship_country\" value=\"" + objCommonFunction.returnCountryPrefix(htUserDetails["Shipping_CountryName"].ToString(), 3) + "\" >");

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

                if (strCommon.ToLower().Contains("com"))
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
                else
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
                //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                //if (htUserDetails.Contains("comboid"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //if (htUserDetails.Contains("ordNo"))
                //{
                //    sbFormOutput.Append(GetCartPaypal(dtCart));
                //}
                //sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                ////sbFormOutput.Append("</td>");
                ////sbFormOutput.Append("</tr>");
                ////sbFormOutput.Append("<tr>");
                ////sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
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
        //}
        //catch (Exception ex)
        //{
        //    strOutput = ex.Message;
        //}
        return strOutput;
    }
    private string printShipBillHtml(Hashtable htUserDetails)
    {
        string strOutPut = "";
        StringBuilder strShipBillOutput = new StringBuilder();
        strShipBillOutput.Append("<table id=\"tabShipBill\" width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
        strShipBillOutput.Append("<tr>");
        strShipBillOutput.Append("<td colspan=\"3\" align=\"center\" valign=\"middle\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\">");
        strShipBillOutput.Append("<b>Order No: " + strCommon.ToString() + "</b>");
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
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
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
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Phone :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Shipping_PhNo"].ToString());
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        if (Convert.ToString(htUserDetails["Shipping_Mobile"].ToString()) != "")
        {
            strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
            strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
            strShipBillOutput.Append("<b>Mobile :</b>");
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
            strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
            strShipBillOutput.Append(htUserDetails["Shipping_Mobile"].ToString());
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("</tr>");
        }
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Email :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Shipping_Email"].ToString());
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>	");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b> Delivery Date:</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
        DateTime dtDod = Convert.ToDateTime(htUserDetails["DOD"].ToString());
        strShipBillOutput.Append(dtDod.ToString("dd MMM yyyy"));
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Message:</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
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
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\"> </td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td> ");
        strShipBillOutput.Append("<td width=\"74%\" colspan=\"3\" class=\"style4\" align=\"left\" valign=\"top\"> ");
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
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Phone :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Billing_PhNo"].ToString());
        strShipBillOutput.Append("</td>");
        if (Convert.ToString(htUserDetails["Billing_Mobile"].ToString()) != "")
        {
            strShipBillOutput.Append("</tr>");
            strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
            strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
            strShipBillOutput.Append("<b>Mobile :</b>");
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
            strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
            strShipBillOutput.Append(htUserDetails["Billing_Mobile"].ToString());
            strShipBillOutput.Append("</td>");
            strShipBillOutput.Append("</tr>");
        }
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
        strShipBillOutput.Append("<b>Email :</b>");
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
        strShipBillOutput.Append(htUserDetails["Billing_Email"].ToString());
        strShipBillOutput.Append("</td>");
        strShipBillOutput.Append("</tr>");
        strShipBillOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\"> ");
        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\"> <b>Spl. Instruction :</b> </td> ");
        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
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
    //protected bool sendWaitMail(Hashtable htUserDetails, DataTable dtCart, ref string strMailError)
    //{
    //    bool blFlag = false;
    //    cartFunctions objCartFunc = new cartFunctions();
    //    if (dtCart.Rows.Count > 0)
    //    {
    //        if (htUserDetails.Count > 0)
    //        {
    //            //string strSubject = "Order No.# " + strOrderNumber + " - " + Session["OtherSiteName"].ToString() + " - " + strBankName + " ( Wait )";

    //            string[] strCondition = new string[1];
    //            strCondition[0] = strCommon.ToString();

    //            //MailOnlyClass objMailOnlyClass = new MailOnlyClass();
    //            //objMailOnlyClass.ConnectString = Application["ConnectionString"].ToString();

    //            string strBody = "";
    //            string strSubject = "";
    //            string strRecipient = "tagBillingDetails.Billing_Email";
    //            string strSiteMailId = "sales@tagSales.SiteName";
    //            //string strMailFormat = "";
    //            string strSenderName = strSiteMailId;
    //            string strFrom = "tagSales.SiteName";
    //            //string strSiteMailId = "sales@" + htUserDetails["siteName"].ToString() + "";

    //            if (objCommonFunction.getMailFomat(1, strCondition, ref strSiteMailId, ref strRecipient, ref strFrom, ref strSenderName, ref strSubject, ref strBody, ref strError) == true)
    //            //if (objMailOnlyClass.GetMailBody(1, strCondition, ref strBody, ref strSubject, ref strFrom, ref strRecipient, ref strSiteMailId, ref strMailFormat) == true)
    //            {
    //                strSubject = strSubject + " (Wait)";
    //                // -------------------------------------------------------------------
    //                //For each site, the mail will be sent only to sales@giftstoindia24x7.com
    //                strSiteMailId = "sales@giftstoindia24x7.com";
    //                // -------------------------------------------------------------------
    //                strError = "";
    //                //if (objCommonFunction.SendMail(strSiteMailId, strSiteMailId, strSubject, strBody, strFrom, strSiteMailId, "", "matirswarga@gmail.com", ref strError) == true)
    //                if (objCommonFunction.SendMail(strSiteMailId, strFrom + "<" + strSiteMailId + ">", strRecipient, strBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
    //                {
    //                    blFlag = true;
    //                    strMailError = "The wait mail is been sent.";
    //                }
    //                else
    //                {
    //                    blFlag = false;
    //                    strMailError = strError;
    //                }
    //            }
    //            else
    //            {
    //                blFlag = false;
    //                strMailError = strError;
    //            }
    //        }
    //        else
    //        {
    //            blFlag = false;
    //            strMailError = objCartFunc.showNoitem();
    //        }
    //    }
    //    else
    //    {
    //        blFlag = false;
    //        strMailError = objCartFunc.showNoitem();
    //    }
    //    return blFlag;
    //}
    protected bool sendWaitMail(Hashtable htUserDetails, DataTable dtCart, ref string strMailError)
    {
        bool blFlag = false;
        cartFunctions objCartFunc = new cartFunctions();
        if (dtCart.Rows.Count > 0)
        {
            if (htUserDetails.Count > 0)
            {
                //string strSubject = "Order No.# " + strOrderNumber + " - " + Session["OtherSiteName"].ToString() + " - " + strBankName + " ( Wait )";
                Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
                string[] strCondition = new string[1];
                strCondition[0] = strCommon.ToString();

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

                // Foramat changed for 24x7 series
                //if (objCommonFunction.getMailFomat(39, strCondition, ref strSiteMailId, ref strRecipient, ref strFrom, ref strSenderName, ref strSubject, ref strBody, ref strError))
                
                
                //if (objCommonFunction.getMailFomat(1, strCondition, ref strSiteMailId, ref strRecipient, ref strFrom, ref strSenderName, ref strSubject, ref strBody, ref strError) == true)
                //{
                //    strSubject = strSubject + " (Wait)";
                //    // -------------------------------------------------------------------
                //    //For each site, the mail will be sent only to sales@giftstoindia24x7.com
                //    strSiteMailId = "sales@giftstoindia24x7.com";
                //    // -------------------------------------------------------------------
                //    strError = "";
                //    //if (objCommonFunction.SendMail(strSiteMailId, strSiteMailId, strSubject, strBody, strFrom, strSiteMailId, "", "matirswarga@gmail.com", ref strError) == true)
                //    if (objCommonFunction.SendMail(strSiteMailId, strFrom + "<" + strSiteMailId + ">", strRecipient, strBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
                //    {
                //        blFlag = true;
                //        strMailError = "The wait mail is been sent.";
                //    }
                //    else
                //    {
                //        blFlag = false;
                //        strMailError = strError;
                //    }
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
        Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
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
            if (strBody.Contains("+91.933.953.0030") && SiteId == 154) strBody = strBody.Replace("+91.933.953.0030", "+91.93301.59107");
            strSubject = strSubject + " (Wait)";
            // -------------------------------------------------------------------
            //For each site, the mail will be sent only to sales@giftstoindia24x7.com
            strSiteMailId = "sales@giftstoindia24x7.com";
            // -------------------------------------------------------------------
            strError = "";

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
    //print combo
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
                        " (rgcards_gti24x7.ComboSbill_Relation.RecordId =1) " +
                        "AND " +
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
    public string functionforOrderDetails(string strSbillNo)
    {
        //get converted curr
        string[] arrConvreCurr = null;
        clsRakhi objRakhi = new clsRakhi();
        //changed by me
        //arrConvreCurr = (objRakhi.fnConvertedCurr(strSbillNo)).Split(new char[] { '^' });
        
        arrConvreCurr = null;
        if (arrConvreCurr.Length > 0)
        {
            decConvertedCurr = Convert.ToDecimal(arrConvreCurr[0]);
            strConvertedCurrSym = arrConvreCurr[1].ToString().Trim();
        }
        double decTotAmt = 0.00;
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
                //decimal totalamount = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["QOS"].ToString());
                double dblDiscount = GetDiscountPercentage(strSbillNo);     //Convert.ToDouble(fnGetDiscount(strSbillNo) / dt.Rows.Count);
                double totalamount = Convert.ToDouble(Convert.ToDouble(dr["Price"]) * Convert.ToInt32(dr["QOS"].ToString()));
                //double dblDollarAmt = Convert.ToDouble(new Gti24x7_CommonFunction().convertCurrency(Convert.ToDouble(Convert.ToDouble(dr["Price"]) - ((Convert.ToDouble(dr["Price"]) * dblDiscount) / 100)), Convert.ToInt32(HttpContext.Current.Session["CurrencyId"])));
                
                
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
                if (strConvertedCurrSym == "")
                {
                    strConvertedCurrSym = "$";
                    decConvertedCurr = 1;
                }
                countMain++;
             //chnaged
                double dblDollarAmt = 0.00;
                strbody = strbody + "<ul class=\"prodList\">" +
                                "<li class=\"width2\">" + count + "</li>" +
                                "<li class=\"width3\"><img src=\"http://www.giftstoindia24x7.com/ASP_IMG/" + Imgpath + "\"  alt=\"" + dr["Item_Name"].ToString() + "\" title=\"" + dr["Item_Name"].ToString() + "\" class=\"itemImg\" />" + dr["Item_Name"].ToString() + "<input type=\"hidden\" name=\"item_name_" + countMain + "\" value=\"" + dr["Item_Name"].ToString() + "\"></li>" +
                                "<li class=\"width2\">" + dr["QOS"].ToString() + "<input type=\"hidden\" name=\"quantity_" + countMain + "\" value=\"" + dr["QOS"].ToString() + "\"></li>" +
                                "<li class=\"width4\">Rs." + dr["Price"].ToString() + "/" + strConvertedCurrSym + "." + Convert.ToDecimal((Convert.ToDecimal(dr["Price"]) / decConvertedCurr)).ToString("0.00") + "  </li>" +
                                "<li class=\"width9\">Rs." + totalamount.ToString("0.00") + "/" + fnFormatedCurr(totalamount) + "<input type=\"hidden\" name=\"amount_" + countMain + "\" value=\"" + dblDollarAmt.ToString("0.00") + "\"></li>" +
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
    protected string fnFormatedCurr(double decRs)
    {
        return strConvertedCurrSym + "." + Convert.ToDouble((decRs / Convert.ToInt32(decConvertedCurr))).ToString("0.00");
    }
    public void fnGenTblHd(ref string strbody, string strSbillNo)
    {
        //strbody = strbody + "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
        if (strCommon.ToLower().Contains("com"))
        {
            strbody = strbody + "<tr>" +
                            "<td align=\"left\" valign=\"top\">" +
                            "<ul class=\"topName\">" +
                            "<li class=\"width1\">" + strSbillNo + "</li>" +
                            "<li class=\"width1\">&nbsp;</li>" +
                            "<li class=\"width2\">Sl No </li>" +
                            "<li class=\"width3\">Item</li>" +
                            "<li class=\"width2\">Qty</li>" +
                            "<li class=\"width4\">Price</li>" +
                            "<li class=\"width4\">Total Price</li>";
        }
        else
        {
            strbody = strbody + "<tr>" +
                            "<td align=\"left\" valign=\"top\">" +
                            "<ul class=\"topName\">" +
                //"<li class=\"width1\">" + strSbillNo + "</li>" +
                            "<li class=\"width1\">&nbsp;</li>" +
                            "<li class=\"width2\">Sl No </li>" +
                            "<li class=\"width3\">Item</li>" +
                            "<li class=\"width2\">Qty</li>" +
                            "<li class=\"width4\">Price</li>" +
                            "<li class=\"width4\">Total Price</li>";
        }
        if (grndordercount == 2)
        {
            //strbody = strbody + " <li><a href=\"javascript:fnDelOrder(1,'" + strSbillNo + "');\"><img src=\"images/trash_icon.gif\" alt=\"Delete Item\" border=\"0\" class=\"removeBtn\" title=\"Delete Item\" /></a></li>";
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
        double intTotalComboAmount = 0.00;
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
                        " (" + strSchema + ".ComboSbill_Relation.recordid <> 0) AND " +
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
        Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
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
               // double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, intCurrencyId));
                //changed
                double dblDollarAmt = 0.00;
                
                //double dblDollarAmt = Convert.ToDouble(objCommonFunction.convertCurrency(dblRowPrice, 1));
                strCartOutput.Append("<input type=\"hidden\" name=\"amount_" + intCount + "\" value=\"" + dblDollarAmt.ToString("0.00") + "\">");
                strCartOutput.Append("</td>");
                strCartOutput.Append("</tr>");
                strCartOutput.Append("</table>");
                strCartOutput.Append("<!--- Cart's Rows Ends--->");

            }


        }
        return strCartOutput.ToString();
    }
    //public string GetCartPaypal(DataTable dtShoppingCart)
    //{
    //    StringBuilder strCartOutput = new StringBuilder();

    //    return strCartOutput.ToString();
    //}
    public DataTable fnGetSalesDetailCombo(string strcombo)
    {
        string strSQL = "";
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        if (htUserDetails.Contains("comboid"))
        {
            strSQL = "SELECT " +
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
        }
        else
        {
            strSQL = "SELECT " +
                          "rgcards_gti24x7.SalesDetails_BothWay.QOS AS qnty," +
                          "rgcards_gti24x7.SalesDetails_BothWay.VendorId," +
                          "rgcards_gti24x7.SalesDetails_BothWay.Price AS rowPrice," +
                          "rgcards_gti24x7.SalesDetails_BothWay.SlNo," +
                          "rgcards_gti24x7.SalesDetails_BothWay.SBillNo," +
                          "rgcards_gti24x7.ItemMaster_Server.Item_Name AS prodName" +
                        " FROM " +
                        "rgcards_gti24x7.SalesDetails_BothWay" +
                          " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
                        " WHERE " +
                          "(rgcards_gti24x7.SalesDetails_BothWay.SBillNo = '" + strcombo + "')" +
                        " ORDER BY " +
                          "rgcards_gti24x7.SalesDetails_BothWay.SBillNo";
        }


        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
        DataTableReader dr = dt.CreateDataReader();

        dr.Close();
        conn.Close();
        return dt;
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
    //           // "<tr><td height='37' width='100%' cellpadding='0' cellspacing='0'>";

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
    //                "</table></td><td width='70%'>"+
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
    //            strBody = strBody + "<tr><td colspan='2' align=\"center\">Total:&nbsp;</td><td align=\"center\">" + intTotQty + "</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + totalProPrice + "</td></tr>"+
    //                                "<tr><td colspan='2' align=\"center\">Discount:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()) + "</td></tr>" +
    //                                "<tr><td colspan='2' align=\"center\">Payable:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + (totalProPrice - Convert.ToDecimal(fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()))) + "</td></tr>" +
    //                                "</table></td></tr></table></td></tr>";
    //            intTotQty = 0;
    //            dtProduct.Reset();
    //        }
    //        strBody = strBody + "<strong>Declaration:</strong></td></tr><tr><td width='100%' colspan='2'><ul type='circle'><li>We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.</li></ul>" +
    //                "</td></tr><tr><td width='100%' colspan='2'>Sales Team</td></tr>" +
    //                "<tr><td width='100%' colspan='2'>rakhi24x7.com</td></tr><tr><td width='100%' colspan='2'> 0091-93391-77995</td></tr>" +
    //                "</tr></table></font></body></html>";
    //        sendingMailRakhi(dtBillDetails.Rows[0]["Billing_Email"].ToString(), "Rakhi24x7<sales@rakhi24x7.com>", strBody, "Wait Mail Against your order", strComboId);
    //    }
    //    conn.Close();
    //}
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
                " <title>::" + SiteName + "::</title></head><body><font face='verdana' size=2> " +
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
                    "<tr><td><strong>Email</strong>:<a href='mailTo:sales@" + SiteName + "'>" + SiteName + "</a></td></tr>" +
                    "<tr>" +
                    "<td><strong>Website</strong>:<a href='http://www." + SiteName + "'>http://www." + SiteName + "</a></td></tr><tr><td><strong>IP Address</strong>:" + Request.UserHostAddress.ToString() + "</td></tr>" +
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
            strBody = strBody + "<strong>Declaration:</strong></td></tr><tr><td width='100%' colspan='2'><ul type='circle'><li>We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.</li></ul>" +
                    "</td></tr><tr><td width='100%' colspan='2'>Sales Team</td></tr>" +
                    "<tr><td width='100%' colspan='2'>" + SiteName + "</td></tr><tr><td width='100%' colspan='2'>+91.933.953.0030</td></tr>" +
                    "</tr></table></font></body></html>";
            sendingMailRakhi(dtBillDetails.Rows[0]["Billing_Email"].ToString(), "" + SiteName + "<sales@" + SiteName + ">", strBody, "Wait Mail Against your order", strComboId);
        }
        conn.Close();
    }
    public void sendingMailRakhi(string strMailTo, string strMailFrom, string strMailBody, string strMailSubject, string id)
    {
        Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
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
        string strSubject = "Order No. : " + id + " - " + strGenSitename + " - " + "" + htUserDetails["pgName"].ToString() + " (Wait)";
        string strMailError = "";
        ////try
        ////{
        ////    SmtpMail.Send(mail1);
        ////}
        ////catch (Exception ex)
        ////{
        ////    Response.Write("Send Failure :" + ex.Message);
        ////}

        //if (objCommonFunction.SendMail("sales@rakhi24x7.com", "" + strGenSitename + "" + "<sales@giftstoindia24x7.com>", strMailTo, strMailBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
        
        //changed
        //if (objCommonFunction.SendMail("sales@giftstoindia24x7.com", strMailTo, strMailTo, strMailBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
        //{
        //    strMailError = "The wait mail is been sent.";
        //}
        //else
        //{
        //    strMailError = strError;
        //}

        //Response.Write(strMailError);



    }
    public void fnTotNDisc(ref string strbody, double decTotAmt, string strSbillNo, int intTotQty)
    {
        strbody = strbody + "<ul class=\"totalList\">" +
                                "<li class=\"width3\"><strong>Total Qty</strong></li>" +
                                "<li class=\"width2\">" + intTotQty + "</li>" +
                                "<li class=\"width7\">Rs. " + decTotAmt.ToString("0.00") + "/" + fnFormatedCurr(decTotAmt) + "</li>" +
                                "</ul>" +
                                "<ul class=\"totalList\">" +
                                "<li class=\"width8\"><strong>Total</strong></li>" +
                                "<li class=\"width9\">Rs. " + decTotAmt.ToString("0.00") + "/" + fnFormatedCurr(decTotAmt) + "</li>" +
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
    public double fnGetDiscount(string strSbillNo)
    {
        double decDisc = 0.00;
        if (conn.State != ConnectionState.Open)
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
            if (dr["Value"] != DBNull.Value)
            {
                decDisc = Convert.ToDouble(dr["Value"]);
            }
        }
        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        return decDisc;
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
                        " (" + strSchema + ".ComboSbill_Relation.RecordId =1) " +
                    "AND " +
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
}