using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text;


public partial class _24x7_Success : System.Web.UI.Page
{
    string strbody = "";
    string flag = "0";
    public string strOrderNo = "";
    public string strBankId = "";
    public string strSiteId = "";
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["Schema"].ToString());
    Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
    clsRakhi objRakhi = new clsRakhi();

    public decimal decConvertedCurr = 0;
    public string strConvertedCurrSym = "";
    string strSql = "";
    string strError = "";
    public string strBillingNm = "";
    string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());

    string strChangeNametoShow = "";
    public string strpageheaderimage = "";
    public string strpageCSS = "";
    public string strRedirectBaseDomain = "";
    public string strGenSitename = "";
    public int intSbillStatus = 9999;
    DataTable dtCombo = new DataTable();
    public int SiteId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        string[] arrConvreCurr = null;

        if (Session["userDetails"] != null)
        {
            Session["userDetails"] = "";
        }
        if (Session["dtCart"] != null)
        {
            System.Data.DataTable dtCart = (DataTable)Session["dtCart"];
            if (dtCart.Rows.Count > 0)
            {
                dtCart.Rows.Clear();
            }
        }
        // Get the bank transaction id for EBS and Eazy 2 pay
        string strBankTranId = "";
        if (Request.QueryString["authcode"] != null)
        {
            strBankTranId = Convert.ToString(Request.QueryString["authcode"].ToString());
        }
        else
        {
            strBankTranId = "";
        }
        // Get the bank response EBS
        string strBankResponse = "";
        if (Request.QueryString["bR"] != null)
        {
            strBankResponse = Convert.ToString(Request.QueryString["bR"].ToString());
        }
        else
        {
            strBankResponse = "";
        }
        // Get the bank response message EBS
        string strBankResponseMsg = "";
        if (Request.QueryString["bM"] != null)
        {
            strBankResponseMsg = Convert.ToString(Request.QueryString["bM"].ToString());
        }
        else
        {
            strBankResponseMsg = "";
        }
        if (Request.QueryString["sbillno"] != null)
        {
            strOrderNo = Convert.ToString(Request.QueryString["sbillno"].ToString());
            // For cc avenue repeat 
            if (strOrderNo.Contains("-"))
            {
                strOrderNo = strOrderNo.Substring(0, strOrderNo.LastIndexOf("-"));
            }
            // For ebs 2.5
            if (strOrderNo.Contains("_"))
            {
                strOrderNo = strOrderNo.Replace("_", "/");
            }
            if (strOrderNo.ToString().Substring(0, 1).Equals("C"))
            {
                if (strOrderNo.Contains("-"))
                {
                    strOrderNo = strOrderNo.Substring(0, strOrderNo.LastIndexOf("-"));
                }
                //get converted curr
                dtCombo = objRakhi.fnGrtOrderCombo(strOrderNo);
                //arrConvreCurr = objRakhi.fnConvertedCurr(dtCombo.Rows[0]["SBillNo"].ToString()).Split(new char[] { '^' });
                //changed by me
                //arrConvreCurr = objRakhi.fnConvertedCurr(dtCombo.Rows[0]["SBillNo"].ToString()).Split(new char[] { '^' });

                if (arrConvreCurr.Length > 0)
                {
                    decConvertedCurr = Convert.ToDecimal(arrConvreCurr[0]);
                    strConvertedCurrSym = arrConvreCurr[1].ToString().Trim();
                }
                //=============for combo order=======================
                if (strOrderNo != "")
                {
                    dtCombo = objRakhi.fnGrtOrderCombo(strOrderNo);
                    strError = "";
                    string strSiteName = getSiteName(strOrderNo, true);
                    strBillingNm = fnUserName(dtCombo.Rows[0]["SBillNo"].ToString());
                    int intSuccess = 0;
                    for (int i = 0; i < dtCombo.Rows.Count; i++)
                    {
                        if (!getSbillStatus(dtCombo.Rows[i]["SBillNo"].ToString(), ref intSbillStatus, ref strError))
                        {
                            dvDetails.InnerHtml = strError;
                        }
                        else
                        {
                            // The order is just been added so fire mail and make it succss by updating the sbill status
                            if (intSbillStatus == 0)
                            {
                                if (!updateSbillStatus(dtCombo.Rows[i]["SBillNo"].ToString(), strBankTranId, strBankResponse, strBankResponseMsg, ref strError))
                                {
                                    dvDetails.InnerHtml = dvDetails.InnerHtml + strError;
                                }
                                else
                                {

                                }

                            }
                            else if ((intSbillStatus > 0) || (intSbillStatus == 6))
                            {
                                intSuccess++;
                            }
                        }
                    }
                    //sendMailtoBuyer(strOrderNo);
                    if (intSuccess == 0)    // If any product is not in wait then only send the mail
                    {
                        strError = "";
                        if (!sendWaitMail(true, strOrderNo, ref strError))
                        {
                            Response.Write(strError);
                        }
                    }
                    dvDetails.InnerHtml = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td width=\"100%\">" + objRakhi.PrintOrderDetailHeader(strOrderNo) + "</td></tr>" + objRakhi.functionforOredDetailForCOMBO(strOrderNo, ref strbody, decConvertedCurr, strConvertedCurrSym) + "</table>";
                    ////sendMailtoBuyer(strOrderNo);

                }
                else
                {
                    dvDetails.InnerHtml = "<br/><br/><center><b>Though the order placement is been successfully done, there is some error on retrievation of your order number or selected bank.</br>We are sorry for that!</b></center><br/><br/>";
                }
                //===================end=============================
            }
            else
            {
                if (strOrderNo.Contains("-"))
                {
                    strOrderNo = strOrderNo.Substring(0, strOrderNo.LastIndexOf("-"));
                }
                //get converted curr
              //cchanged by me
                //  arrConvreCurr = objRakhi.fnConvertedCurr(strOrderNo).Split(new char[] { '^' });
                if (arrConvreCurr.Length > 0)
                {
                    decConvertedCurr = Convert.ToDecimal(arrConvreCurr[0]);
                    strConvertedCurrSym = arrConvreCurr[1].ToString().Trim();
                }
                //=============for normal order=======================
                if (strOrderNo != "")
                {
                    string strSiteName = getSiteName(strOrderNo, false);
                    strBillingNm = fnUserName(strOrderNo);
                    strError = "";
                    if (!getSbillStatus(strOrderNo, ref intSbillStatus, ref strError))
                    {
                        dvDetails.InnerHtml = strError;
                    }
                    else
                    {
                        // The order is just been added so fire mail and make it succss by updating the sbill status
                        if (intSbillStatus == 0)
                        {
                            strError = "";
                            if (sendWaitMail(false, strOrderNo, ref strError))
                            {
                                strError = "";
                                if (updateSbillStatus(strOrderNo, strBankTranId, strBankResponse, strBankResponseMsg, ref strError))
                                {
                                    dvDetails.InnerHtml = dvDetails.InnerHtml + strError;
                                }
                                else
                                {

                                }
                              //changed by me
                                // dvDetails.InnerHtml = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td width=\"100%\">" + objRakhi.PrintOrderDetailHeader(strOrderNo) + "</td></tr>" + objRakhi.functionforOrderDetails(strOrderNo, ref strbody, decConvertedCurr, strConvertedCurrSym) + "</table>";
                            
                            
                            }
                            else
                            {
                                dvDetails.InnerHtml = strError + "<br/><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td width=\"100%\">" + objRakhi.PrintOrderDetailHeader(strOrderNo) + "</td></tr>" + objRakhi.functionforOrderDetails(strOrderNo, ref strbody, decConvertedCurr, strConvertedCurrSym) + "</table>";
                            }
                        }
                        else
                        {
                            ////dvDetails.InnerHtml = strPrintOutPut(strSiteName, strOrderNo);
                            dvDetails.InnerHtml = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td width=\"100%\">" + objRakhi.PrintOrderDetailHeader(strOrderNo) + "</td></tr>" + objRakhi.functionforOrderDetails(strOrderNo, ref strbody, decConvertedCurr, strConvertedCurrSym) + "</table>";

                        }
                    }
                }
                else
                {
                    dvDetails.InnerHtml = "<br/><br/><center><b>Though the order placement is been successfully done, there is some error on retrievation of your order number or selected bank.</br>We are sorry for that!</b></center><br/><br/>";
                }
                //==================end===============================
            }


            // ---------------------------------------------------
            // Twitter and Facebook Implementation
            // ---------------------------------------------------
            //chkpartnerdetails obj = new chkpartnerdetails(Convert.ToString(ConfigurationManager.AppSettings["dbCon"]), strSchema);
            //if (obj.CheckStatusByOrderNumber(strOrderNo))
            //{
            //    if (obj.Message == "RGCARDS.COM")
            //    {
            //        fcBook.InnerHtml = "<a href=\"http://twitter.com/gifts2india24x7\" target=\"_blank\"><img src=\"images/follow_twitter.gif\" alt=\"Follow us on Twitter\" title=\"Follow us on Twitter\" /></a>";
            //        fcBook.InnerHtml += "<a href=\"http://www.facebook.com/GiftstoIndia?v=app_4949752878\" target=\"_blank\"><img src=\"images/join_facebook.gif\" alt=\"Join us on Facebook\" title=\"Join us on Facebook\" /></a>";
            //    }
            //    else
            //    {
            //        fcBook.InnerHtml = "<img src=\"images/thanx_img.gif\" alt=\"Thanks for placing the order\" title=\"Thanks for placing the order\" />";
            //    }
            //}
            //else
            //{
            //}

            // ---------------------------------------------------
        }


    }

    protected bool getSbillStatus(string strOrderNumber, ref int intSbillStatus, ref string strSbillError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[Sbill_Status] " +
                    "FROM " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "WHERE " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SbillNo]='" + strOrderNumber + "')";
            SqlCommand cmdSbillStatus = new SqlCommand(strSql, conn);
            SqlDataReader drSbillStatus = cmdSbillStatus.ExecuteReader(CommandBehavior.CloseConnection);
            if (drSbillStatus.HasRows)
            {
                blFlag = true;
                if (drSbillStatus.Read())
                {
                    intSbillStatus = Convert.ToInt32(drSbillStatus["Sbill_Status"].ToString());
                }
            }
            else
            {
                intSbillStatus = 9999;
            }
            drSbillStatus.Dispose();
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strSbillError = ex.Message;
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
    protected bool updateSbillStatus(string strOrderNumber, string strBankTransactionId, string strBankTransactionResponse, string strBankTransResponseMsg, ref string strSbillError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlTransaction tranUpdate = conn.BeginTransaction();
            try
            {
                strSql = "UPDATE " +
                            "[" + strSchema + "].[SalesMaster_BothWay]" +
                        "SET " +
                            "[" + strSchema + "].[SalesMaster_BothWay].[Bank_TranId]='" + strBankTransactionId + "', " +
                            "[" + strSchema + "].[SalesMaster_BothWay].[Sbill_Status]='1' " +
                        "WHERE " +
                            "([" + strSchema + "].[SalesMaster_BothWay].[SbillNo]='" + strOrderNumber + "')";
                SqlCommand cmdSbillStatus = new SqlCommand(strSql, conn, tranUpdate);
                cmdSbillStatus.ExecuteNonQuery();

                strSql = "UPDATE " +
                            "[" + strSchema + "].[Order_Pg_Details]" +
                        "SET " +
                            "[" + strSchema + "].[Order_Pg_Details].[BankTranId]='" + strBankTransactionId + "', " +
                            "[" + strSchema + "].[Order_Pg_Details].[BankTranResponse]='" + strBankTransactionResponse + "', " +
                            "[" + strSchema + "].[Order_Pg_Details].[BankTranMsg]='" + strBankTransResponseMsg + "' " +
                        "WHERE " +
                            "([" + strSchema + "].[Order_Pg_Details].[SbillNo]='" + strOrderNumber + "')";
                SqlCommand cmdOrderDetail = new SqlCommand(strSql, conn, tranUpdate);
                cmdOrderDetail.ExecuteNonQuery();
                tranUpdate.Commit();
                blFlag = true;

            }
            catch (Exception tranEx)
            {
                tranUpdate.Rollback();
                blFlag = false;
                strSbillError = tranEx.Message;
            }
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strSbillError = ex.Message;
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
            if (strBody.Contains("+91.933.953.0030") && SiteId == 154) strBody = strBody.Replace("+91.933.953.0030", "+91.93301.59107");
            // -------------------------------------------------------------------
            //For each site, the mail will be sent only to sales@giftstoindia24x7.com
            strSiteMailId = "sales@giftstoindia24x7.com";
            // -------------------------------------------------------------------


            /////////////////////////////////////////////////////////////////////////////
            // Twitter and Facebook Implementation
            /////////////////////////////////////////////////////////////////////////////
            //chkpartnerdetails obj = new chkpartnerdetails(Convert.ToString(ConfigurationManager.AppSettings["dbCon"]), Convert.ToString(ConfigurationManager.AppSettings["Schema"]));
        
            //if (obj.CheckStatusByOrderNumber(strOrderNo))
            //{
            //    if (obj.Message == "RGCARDS.COM")
            //    {
            //        StringBuilder sb = new StringBuilder();

            //        sb.Append("<table width=\"650\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">");
            //        sb.Append("<tr>");
            //        sb.Append("<td width=\"22\" align=\"left\" valign=\"top\" style=\"line-height:24px; background-color:#39badd; font-size:21px; font-weight:bold; font-family:Verdana, Geneva, sans-serif; color:#fff; text-align:center;\"><a href=\"http://twitter.com/gifts2india24x7\" style=\"color:#fff; text-decoration:none;\" title=\"twitter\">t</a></td>");
            //        sb.Append("<td width=\"55\" align=\"left\" valign=\"middle\" style=\"font-size:12px; font-weight:bold; font-family:Arial, Helvetica, sans-serif; color:#3a3a3a; padding-left:4px;\"><a href=\"http://twitter.com/gifts2india24x7\" target=\"_blank\" style=\"color:#3a3a3a; text-decoration:none;\">Twitter</a></td>");
            //        sb.Append("<td width=\"19\" align=\"left\" valign=\"top\" style=\"line-height:24px; background-color:#3a5896; font-size:21px; font-weight:bold; font-family:Verdana, Geneva, sans-serif; color:#fff; text-align:right; padding-right:3px\"><a href=\"http://www.facebook.com/GiftstoIndia?v=app_4949752878\" style=\"color:#fff; text-decoration:none;\" title=\"facebook\">f</a></td>");
            //        sb.Append("<td align=\"left\" valign=\"middle\" style=\"font-size:12px; font-weight:bold; font-family:Arial, Helvetica, sans-serif; color:#3a3a3a; padding-left:4px;\"><a href=\"http://www.facebook.com/GiftstoIndia?v=app_4949752878\" target=\"_blank\" style=\"color:#3a3a3a; text-decoration:none;\">Facebook</a></td>");
            //        sb.Append("</tr>");
            //        sb.Append("</table>");

            //        strBody += "<div align=\"center\">" + Convert.ToString(sb) + "</div>";
            //    }
            //}

            /////////////////////////////////////////////////////////////////////////////

            //changed by me
            //if (objCommonFunction.SendMail(strRecipient, "" + strFrom + "<sales@giftstoindia24x7.com>", "sales@giftstoindia24x7.com", strBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
            //{
            //    strMailError = "The Success mail is been sent.";
            //}
            //else
            //{
            //    strMailError = strError;
            //}
            //==============to admin===============
            strSubject = strSubject + " (Success)";
//changed by me
            
            ////if (objCommonFunction.SendMail(strSiteMailId, "" + strFrom + "" + "<" + strSiteMailId + ">", strRecipient, strBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
            ////{
            ////    strMailError = "The Success mail is been sent.";
            ////}
            ////else
            ////{
            ////    strMailError = strError;
            ////}
            //============== Only for gifts.indiavision ===============
            if (strSiteId == "156")
            {
                //if (objCommonFunction.SendMail("gifts@indiavision.com, rtechkol@gmail.com", "" + strFrom + "" + "<" + strSiteMailId + ">", strRecipient, strBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
                //{
                //    strMailError = "The Success mail is been sent.";
                //}
                //else
                //{
                //    strMailError = strError;
                //}
            }
            //============== Only for gifts.indiavision ===============
            blFlag = true;
        }
        else
        {
            blFlag = false;
            strMailError = strError;
        }
        return blFlag;
    }

    protected string strPrintOutPut(string strSiteName, string strOrderNumber)
    {
        string strFinalOutput = "";
        StringBuilder strOutput = new StringBuilder();
        strOutput.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\"> ");
        strOutput.Append("<tr class=\"clear10\"><td valign=\"top\" class=\"clear10\"> </td></tr> ");
        strOutput.Append("<tr>");
        strOutput.Append("<td valign=\"top\">");
        strOutput.Append("<!-- User Detail Table Starts -->");
        Hashtable htUserDetails = new Hashtable();
        strError = "";
        if (getOrderDetails(strOrderNo, ref htUserDetails, ref strError) == true)
        {
            if (htUserDetails.Count > 0)
            {
                strOutput.Append("<table width=\"100%\" border=\"0\" class=\"center\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#ffffff\">");
                strOutput.Append("<tr>");
                strOutput.Append("<td width=\"100%\" valign=\"top\">");
                strOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"> ");
                strOutput.Append("<tr>");
                strOutput.Append("<td class=\"style2\" colspan=\"2\"> Dear <span class=\"big_red\">" + htUserDetails["Billing_Name"].ToString() + "</span>,</td> ");
                strOutput.Append("</tr>");
                strOutput.Append("<tr>");
                strOutput.Append("<td width=\"1%\">&nbsp;</td>");
                strOutput.Append("<td width=\"99%\" class=\"style2\"> <div align=\"justify\"> Your order has been received and will be processed with the information you provided. If there are any difficulties we will contact you by phone or via e-mail.&nbsp; We would like to thank you for choosing " + strSiteName + ". </div></td>");
                strOutput.Append("</tr>");
                strOutput.Append("</table>");
                strOutput.Append("</td>");
                strOutput.Append("</tr>");
                strOutput.Append("<tr class=\"clear10\"><td valign=\"top\" class=\"clear10\"></td></tr>");
                strOutput.Append("<tr>");
                strOutput.Append("<td width=\"100%\" valign=\"top\">");
                strOutput.Append("<!-- Middle section starts -->");
                strOutput.Append("<table id=\"tabMiddle\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tableborderLite\">");
                strOutput.Append("<tr bgcolor=\"#B31B10\" class=\"style3\">");
                strOutput.Append("<td colspan=\"5\" height=\"20\" align=\"center\"><strong>&nbsp;&nbsp;" + strSiteName + "</strong></td>");
                strOutput.Append("</tr>");
                strOutput.Append("<tr>");
                strOutput.Append("<td width=\"28%\" align=\"left\" valign=\"top\">");
                strOutput.Append("<!-- Ship To table starts -->");
                strOutput.Append("<table id=\"tabShipTo\" width=\"100%\" cellpadding=\"0%\" cellspacing=\"0%\" border=\"0\">");
                strOutput.Append("<tr>");
                strOutput.Append("<td colspan=\"2\" width=\"100%\" align=\"left\" valign=\"middle\" class=\"style8\" height=\"18px\"><b>Ship to :</b></td>");
                strOutput.Append("</tr>");
                strOutput.Append("<tr>");
                strOutput.Append("<td width=\"2%\">&nbsp;</td>");
                strOutput.Append("<td width=\"100%\" align=\"left\" valign=\"middle\" class=\"style4\">" + htUserDetails["Shipping_Name"].ToString() + "<br>");
                strOutput.Append(htUserDetails["Shipping_Address1"].ToString() + "<br/>");
                if (Convert.ToString(htUserDetails["Shipping_Address2"].ToString()) != "")
                {
                    strOutput.Append(htUserDetails["Shipping_Address2"].ToString() + "<br/>");
                }
                strOutput.Append(htUserDetails["City_Name"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + htUserDetails["Shipping_PinCode"].ToString() + "<br/> ");
                strOutput.Append(htUserDetails["State_Name"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + htUserDetails["Country_Name"].ToString() + "<br/> ");
                strOutput.Append(htUserDetails["Shipping_PhNo"].ToString() + "<br/>");
                strOutput.Append(htUserDetails["Shipping_Mobile"].ToString());
                strOutput.Append("</td>");
                strOutput.Append("</tr>");
                strOutput.Append("</table> ");
                strOutput.Append("<!-- Ship To table Ends --> ");
                strOutput.Append("</td>");
                strOutput.Append("<td width=\"2%\" align=\"left\" valign=\"top\">&nbsp;</td> ");
                strOutput.Append("<td width=\"40%\" align=\"left\" valign=\"top\">");
                strOutput.Append("<!-- Order no. table starts -->");
                strOutput.Append("<table id=\"tabOrdNo\" width=\"100%\" cellpadding=\"0%\" cellspacing=\"0%\" border=\"0\" height=\"100%\">");
                strOutput.Append("<tr height=\"20px\">");
                strOutput.Append("<td width=\"35%\" align=\"left\" valign=\"middle\" class=\"style8\"><b>Order No :</b></td>");
                strOutput.Append("<td width=\"1%\" align=\"left\" valign=\"middle\"></td>");
                strOutput.Append("<td width=\"64%\" align=\"left\" valign=\"middle\" class=\"style2\"><b>" + strOrderNo + "</b></td> ");
                strOutput.Append("</tr>");
                strOutput.Append("<tr height=\"20px\">");
                strOutput.Append("<td width=\"35%\" align=\"left\" valign=\"middle\" class=\"style8\"><b>Date Of Delivery :</b></td>");
                strOutput.Append("<td width=\"1%\" align=\"left\" valign=\"middle\"></td>");
                strOutput.Append("<td width=\"64%\" align=\"left\" valign=\"middle\" class=\"style2\"><b>" + htUserDetails["DOD"].ToString() + "</b></td>");
                strOutput.Append("</tr>");
                strOutput.Append("<tr height=\"20px\">");
                strOutput.Append("<td width=\"35%\" align=\"left\" valign=\"middle\" class=\"style8\"><b>Bill Name :</b></td> ");
                strOutput.Append("<td width=\"1%\" align=\"left\" valign=\"middle\"></td>");
                strOutput.Append("<td width=\"64%\" align=\"left\" valign=\"middle\" class=\"style2\"><b>" + htUserDetails["Billing_Name"].ToString() + "</b></td>");
                strOutput.Append("</tr>");
                strOutput.Append("<tr height=\"20px\">");
                strOutput.Append("<td width=\"35%\" align=\"left\" valign=\"middle\" class=\"style8\"><b>Email :</b></td>");
                strOutput.Append("<td width=\"1%\" align=\"left\" valign=\"middle\"></td>");
                strOutput.Append("<td width=\"64%\" align=\"left\" valign=\"middle\" class=\"style2\"><b>" + htUserDetails["Billing_Email"].ToString() + "</b></td>");
                strOutput.Append("</tr>");
                strOutput.Append("</table>");
                strOutput.Append("<!-- Order no. table Ends --> ");
                strOutput.Append("</td>");
                strOutput.Append("<td width=\"2%\" align=\"left\" valign=\"top\">&nbsp;</td> ");
                strOutput.Append("<td width=\"28%\" align=\"left\" valign=\"top\">");
                strOutput.Append("<!-- MWG table starts -->");
                strOutput.Append("<table id=\"tabMWG\" width=\"100%\" cellpadding=\"0%\" cellspacing=\"0%\" border=\"0\">");
                strOutput.Append("<tr>");
                strOutput.Append("<td colspan=\"2\" width=\"100%\" align=\"left\" valign=\"middle\" class=\"style8\" height=\"18px\"><b>Message with the Gift :</b></td> ");
                strOutput.Append("</tr>");
                strOutput.Append("<tr>");
                strOutput.Append("<td width=\"2%\">&nbsp;</td>");
                strOutput.Append("<td width=\"100%\" align=\"left\" valign=\"middle\" class=\"style4\">");
                strOutput.Append(htUserDetails["MWG"].ToString());
                strOutput.Append("</td>");
                strOutput.Append("</tr>");
                strOutput.Append("</table> ");
                strOutput.Append("<!-- MWG table Ends -->");
                strOutput.Append("</td>");
                strOutput.Append("</tr>");
                strOutput.Append("</table>");
                strOutput.Append("<!-- Middle section Edns -->");
                strOutput.Append("</td>");
                strOutput.Append("</tr>");
                strOutput.Append("</table>");

            }
            else
            {
                strOutput.Append("User details not found.");
            }
        }
        else
        {
            strOutput.Append("User details not found due to " + strError);
        }
        strOutput.Append("<!-- User Detail Table Ends -->");
        strOutput.Append("</td>");
        strOutput.Append("</tr>");
        strOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strOutput.Append("<tr>");
        strOutput.Append("<td valign=\"top\">");
        strOutput.Append("<!-- Cart Details Starts -->");
        string strCartOutput = "";
        strError = "";
        if (getCartDetails(strOrderNo, ref strCartOutput, ref strError) == true)
        {
            strOutput.Append(strCartOutput);
        }
        else
        {
            strOutput.Append(strError);
        }
        strOutput.Append("<!-- Cart Details Ends -->");
        strOutput.Append("</td>");
        strOutput.Append("</tr>");
        strOutput.Append("<tr class=\"clear10\"><td valign=\"top\" class=\"clear10\"></td></tr>");
        strOutput.Append("<tr>");
        strOutput.Append("<td valign=\"top\">");
        strOutput.Append("<!-- Text Portion Starts -->");

        strOutput.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"tableborderLite\">");
        strOutput.Append("<tr>");
        strOutput.Append("<td width=\"1%\">&nbsp;</td>");
        strOutput.Append("<td width=\"99%\" class=\"style2\">Kindly review the same and in case of any changes, please <a href=\"http://www." + strSiteName + "/contactus.aspx\">contact us</a> with mentioning the Order number.</td>");
        strOutput.Append("</tr>");
        strOutput.Append("<tr>");
        strOutput.Append("<td width=\"1%\" class=\"clear10\">&nbsp;</td>");
        strOutput.Append("<td width=\"99%\" class=\"clear10\">&nbsp;</td>");
        strOutput.Append("</tr>");
        strOutput.Append("<tr>");
        strOutput.Append("<td width=\"1%\">&nbsp;</td>");
        strOutput.Append("<td  width=\"99%\" class=\"style2\">");
        strOutput.Append("<ul style=\"list-style-position:inside;\">");
        strOutput.Append("<li>Thank you for placing an order with www.<a onclick=\"return top.js.OpenExtLink(window,event,this)\" href=\"http://www." + strSiteName + "/\" target=\"_blank\">" + strSiteName + "</a>. Please preserve this order for future reference. </li> ");
        strOutput.Append("<li>All deliveries are done during the day time between 10 AM to 8 PM. If you have mentioned a specific time of delivery above, we will try our level best to follow the same. </li>");
        strOutput.Append("<li>Midnight orders are delivered between 23:45 Hrs to 00:15 Hrs. </li>");
        strOutput.Append("<li>" + getGatewayChargeNametoShow(strOrderNo) + "</li> ");
        strOutput.Append("<li>For any assistance, please email us at <a href=\"mailto:sales@" + strSiteName + "\">sales@" + strSiteName + ".com</a> stating your Order no. </li>");
        strOutput.Append("<li>All online purchases will be billed in your local currency.</li>");
        strOutput.Append("</ul>");
        strOutput.Append("</td>");
        strOutput.Append("</tr>");
        strOutput.Append("<tr>");
        strOutput.Append("<td width=\"1%\">&nbsp;</td>");
        strOutput.Append("<td width=\"99%\" class=\"style2\">&nbsp;</td>");
        strOutput.Append("</tr>");
        strOutput.Append("</table>");

        strOutput.Append("<!-- Text Portion Ends -->");
        strOutput.Append("</td>");
        strOutput.Append("</tr>");
        strOutput.Append("</table>");
        strFinalOutput = strOutput.ToString();

        return strFinalOutput;
    }
    protected bool getOrderDetails(string strOrderNumber, ref Hashtable htUserDetail, ref string strOrderError)
    {
        bool blFlag = false;
        strSql = "SELECT " +
                    "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Name], " +
                    "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address1], " +
                    "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address2], " +
                    "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PinCode], " +
                    "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PhNo], " +
                    "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Mobile], " +
                    "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Email], " +
                    "[" + strSchema + "].[City_Server].[City_Id], " +
                    "[" + strSchema + "].[City_Server].[City_Name], " +
                    "[" + strSchema + "].[State_Server].[State_Id], " +
                    "[" + strSchema + "].[State_Server].[State_Name], " +
                    "[" + strSchema + "].[Country_Server].[Country_ID], " +
                    "[" + strSchema + "].[Country_Server].[Country_Name], " +
                    "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Name], " +
                    "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Email], " +
                    "CONVERT(VARCHAR(255), [" + strSchema + "].[SalesMaster_BothWay].[DOD], 106) AS [DOD], " +
                    "[" + strSchema + "].[SalesMaster_BothWay].[MWG] " +
                "FROM " +
                    "[" + strSchema + "].[SalesMaster_BothWay] " +
                "INNER JOIN " +
                    "[" + strSchema + "].[ShippingDetails_Bothway] " +
                "ON " +
                    "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
                "INNER JOIN " +
                    "[" + strSchema + "].[BillingDetails_Bothway] " +
                "ON " +
                    "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[BillingDetails_Bothway].[SBillNo]) " +
                "INNER JOIN " +
                    "[" + strSchema + "].[Country_Server] " +
                "ON " +
                    "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId]=[" + strSchema + "].[Country_Server].[Country_Id]) " +
                "INNER JOIN " +
                    "[" + strSchema + "].[State_Server] " +
                "ON " +
                    "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId]=[" + strSchema + "].[State_Server].[State_Id]) " +
                "INNER JOIN " +
                    "[" + strSchema + "].[City_Server] " +
                "ON " +
                    "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId]=[" + strSchema + "].[City_Server].[City_Id]) " +
                "WHERE " +
                    "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNumber + "'); ";
        try
        {
            conn.Open();
            SqlCommand cmdOrderDetail = new SqlCommand(strSql, conn);
            SqlDataReader drOrderDetail = cmdOrderDetail.ExecuteReader(CommandBehavior.CloseConnection);
            if (drOrderDetail.HasRows)
            {
                blFlag = true;
                if (drOrderDetail.Read())
                {
                    if (!htUserDetail.Contains("Shipping_Name"))
                    {
                        htUserDetail.Add("Shipping_Name", drOrderDetail["Shipping_Name"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_Name");
                        htUserDetail.Add("Shipping_Name", drOrderDetail["Shipping_Name"].ToString());
                    }
                    if (!htUserDetail.Contains("Shipping_Address1"))
                    {
                        htUserDetail.Add("Shipping_Address1", drOrderDetail["Shipping_Address1"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_Address1");
                        htUserDetail.Add("Shipping_Address1", drOrderDetail["Shipping_Address1"].ToString());
                    }
                    if (!htUserDetail.Contains("Shipping_Address2"))
                    {
                        htUserDetail.Add("Shipping_Address2", drOrderDetail["Shipping_Address2"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_Address2");
                        htUserDetail.Add("Shipping_Address2", drOrderDetail["Shipping_Address2"].ToString());
                    }
                    if (!htUserDetail.Contains("Shipping_PinCode"))
                    {
                        htUserDetail.Add("Shipping_PinCode", drOrderDetail["Shipping_PinCode"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_PinCode");
                        htUserDetail.Add("Shipping_PinCode", drOrderDetail["Shipping_PinCode"].ToString());
                    }
                    if (!htUserDetail.Contains("Shipping_PhNo"))
                    {
                        htUserDetail.Add("Shipping_PhNo", drOrderDetail["Shipping_PhNo"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_PhNo");
                        htUserDetail.Add("Shipping_PhNo", drOrderDetail["Shipping_PhNo"].ToString());
                    }
                    if (!htUserDetail.Contains("Shipping_Mobile"))
                    {
                        htUserDetail.Add("Shipping_Mobile", drOrderDetail["Shipping_Mobile"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_Mobile");
                        htUserDetail.Add("Shipping_Mobile", drOrderDetail["Shipping_Mobile"].ToString());
                    }
                    if (!htUserDetail.Contains("Shipping_Email"))
                    {
                        htUserDetail.Add("Shipping_Email", drOrderDetail["Shipping_Email"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_Email");
                        htUserDetail.Add("Shipping_Email", drOrderDetail["Shipping_Email"].ToString());
                    }
                    if (!htUserDetail.Contains("City_Id"))
                    {
                        htUserDetail.Add("City_Id", drOrderDetail["City_Id"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("City_Id");
                        htUserDetail.Add("City_Id", drOrderDetail["City_Id"].ToString());
                    }
                    if (!htUserDetail.Contains("City_Name"))
                    {
                        htUserDetail.Add("City_Name", drOrderDetail["City_Name"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("City_Name");
                        htUserDetail.Add("City_Name", drOrderDetail["City_Name"].ToString());
                    }
                    if (!htUserDetail.Contains("State_Id"))
                    {
                        htUserDetail.Add("State_Id", drOrderDetail["State_Id"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("State_Id");
                        htUserDetail.Add("State_Id", drOrderDetail["State_Id"].ToString());
                    }
                    if (!htUserDetail.Contains("State_Name"))
                    {
                        htUserDetail.Add("State_Name", drOrderDetail["State_Name"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("State_Name");
                        htUserDetail.Add("State_Name", drOrderDetail["State_Name"].ToString());
                    }
                    if (!htUserDetail.Contains("Country_ID"))
                    {
                        htUserDetail.Add("Country_ID", drOrderDetail["Country_ID"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Country_ID");
                        htUserDetail.Add("Country_ID", drOrderDetail["Country_ID"].ToString());
                    }
                    if (!htUserDetail.Contains("Country_Name"))
                    {
                        htUserDetail.Add("Country_Name", drOrderDetail["Country_Name"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Country_Name");
                        htUserDetail.Add("Country_Name", drOrderDetail["Country_Name"].ToString());
                    }
                    if (!htUserDetail.Contains("Billing_Name"))
                    {
                        htUserDetail.Add("Billing_Name", drOrderDetail["Billing_Name"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_Name");
                        htUserDetail.Add("Billing_Name", drOrderDetail["Billing_Name"].ToString());
                    }
                    if (!htUserDetail.Contains("Billing_Email"))
                    {
                        htUserDetail.Add("Billing_Email", drOrderDetail["Billing_Email"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_Email");
                        htUserDetail.Add("Billing_Email", drOrderDetail["Billing_Email"].ToString());
                    }


                    if (!htUserDetail.Contains("DOD"))
                    {
                        htUserDetail.Add("DOD", drOrderDetail["DOD"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("DOD");
                        htUserDetail.Add("DOD", drOrderDetail["DOD"].ToString());
                    }
                    if (!htUserDetail.Contains("MWG"))
                    {
                        htUserDetail.Add("MWG", drOrderDetail["MWG"].ToString());
                    }
                    else
                    {
                        htUserDetail.Remove("MWG");
                        htUserDetail.Add("MWG", drOrderDetail["MWG"].ToString());
                    }
                }
                else
                {
                    blFlag = false;
                    strOrderError = "No record found.";
                }
            }
            else
            {
                strOrderError = "No details found for the order number : " + strOrderNumber + ".";
            }
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strOrderError = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return blFlag;
    }
    protected bool getCartDetails(string strOrderNumber, ref string strCartOutput, ref string strOrderDetailsError)
    {
        strCartOutput = "";
        strOrderDetailsError = "";
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "ROW_NUMBER() OVER(ORDER BY [" + strSchema + "].[ItemMaster_Server].[Item_Name]) AS [SlNo], " +
                        "[" + strSchema + "].[SalesDetails_BothWay].[Product_Id], " +
                        "[" + strSchema + "].[ItemMaster_Server].[Item_Name], " +
                        "[" + strSchema + "].[SalesDetails_BothWay].[QOS] AS [Qnty], " +
                        "[" + strSchema + "].[SalesDetails_BothWay].[Price], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[SInstruction] " +
                    "FROM " +
                        "[" + strSchema + "].[SalesDetails_BothWay] " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "ON " +
                        "([" + strSchema + "].[SalesDetails_BothWay].[SBillNo]=[" + strSchema + "].[SalesMaster_BothWay].[SBillNo]) " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[ItemMaster_Server] " +
                    "ON " +
                        "([" + strSchema + "].[SalesDetails_BothWay].[Product_Id]=[" + strSchema + "].[ItemMaster_Server].[Product_Id]) " +
                    "WHERE " +
                        "([" + strSchema + "].[SalesDetails_BothWay].[SBillNo]='" + strOrderNo + "')";

            double dblCurrencyValue = 1;
            string strDiscountCode = "";
            string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"].ToString());
            int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
            if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
            {
                StringBuilder strOutPut = new StringBuilder();
                SqlCommand cmdOrderProd = new SqlCommand(strSql, conn);
                SqlDataReader drOrderProd = cmdOrderProd.ExecuteReader(CommandBehavior.CloseConnection);
                double dblTotGrandPrice = 0.00;
                if (drOrderProd.HasRows)
                {
                    string strSpecialInstruction = "";
                    blFlag = true;
                    strOutPut.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    strOutPut.Append("<tr>");
                    strOutPut.Append("<td width=\"70%\" valign=\"top\">");
                    strOutPut.Append("<table width=\"99%\" border=\"0\" align=\"left\" cellpadding=\"0\" cellspacing=\"0\" class=\"tableborderLite\"> ");
                    strOutPut.Append("<tr bgcolor=\"#FF6600\" class=\"footer-copyright\">");
                    strOutPut.Append("<td width=\"7%\" valign=\"middle\" align=\"center\" bgcolor=\"#B31B10\" class=\"style3\" style=\"height: 20px\"><div align=\"center\"><strong>Sl No</strong></div></td> ");
                    strOutPut.Append("<td width=\"32%\" valign=\"middle\" align=\"center\" bgcolor=\"#B31B10\" class=\"style3\" style=\"height: 20px\"><strong>Item</strong></td>");
                    strOutPut.Append("<td width=\"10%\" valign=\"middle\" align=\"center\" bgcolor=\"#B31B10\" class=\"style3\" style=\"height: 20px\"><strong>Qty</strong></td>");
                    strOutPut.Append("<td width=\"25%\" valign=\"middle\" align=\"center\" bgcolor=\"#B31B10\" class=\"style3\" style=\"height: 20px\"><strong>Price</strong></td> ");
                    strOutPut.Append("<td width=\"25%\" valign=\"middle\" align=\"center\" bgcolor=\"#B31B10\" class=\"style3\" style=\"height: 20px\"><strong>Net Price</strong></td>");
                    strOutPut.Append("</tr>");

                    while (drOrderProd.Read())
                    {
                        strOutPut.Append("<tr class=\"body-text-dark12\"> ");
                        strOutPut.Append("<td colspan=\"5\">");
                        strOutPut.Append("<!--- Cart's Rows Starts--->");
                        if (Convert.ToString(drOrderProd["SlNo"].ToString()) == "1")
                        {
                            strSpecialInstruction = Convert.ToString(drOrderProd["SInstruction"].ToString());
                        }
                        int intRowQnty = Convert.ToInt32(drOrderProd["Qnty"].ToString());
                        double dblRowPrice = Convert.ToDouble(drOrderProd["Price"].ToString());
                        double dblTotRowPrice = Convert.ToDouble(intRowQnty * dblRowPrice);
                        dblTotGrandPrice = Convert.ToDouble(dblTotGrandPrice + dblTotRowPrice);
                        string strRowPrice = "Rs." + dblRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblRowPrice / dblCurrencyValue).ToString("0.00");
                        string strTotRowPrice = "Rs." + dblTotRowPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotRowPrice / dblCurrencyValue).ToString("0.00");

                        strOutPut.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
                        strOutPut.Append("<tr class=\"style1\">");
                        strOutPut.Append("<td width=\"7%\" align=\"center\" valign=\"middle\">");
                        strOutPut.Append("<div align=\"center\">" + drOrderProd["SlNo"].ToString() + "</div>");
                        strOutPut.Append("</td>");
                        strOutPut.Append("<td width=\"32%\" class=\"style1\"> ");
                        strOutPut.Append("<table width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\">");
                        strOutPut.Append("<tr>");
                        strOutPut.Append("<td width=\"20%\" valign=\"middle\" align=\"center\" class=\"style1\">");
                        strOutPut.Append("<div align=\"center\"><img src=\"" + strSitePath + "/ASP_IMG/" + drOrderProd["Product_Id"].ToString() + ".jpg\" width=\"60\" height=\"60\" border=\"0\" alt=\"" + drOrderProd["Item_Name"].ToString() + "\"></div>");
                        strOutPut.Append("</td>");
                        strOutPut.Append("<td width=\"70%\" valign=\"middle\" align=\"center\" class=\"style1\">");
                        strOutPut.Append(drOrderProd["Item_Name"].ToString());
                        strOutPut.Append("</td>");
                        strOutPut.Append("</tr>");
                        strOutPut.Append("</table>");
                        strOutPut.Append("</td>");
                        strOutPut.Append("<td width=\"10%\" align=\"center\" valign=\"middle\">");
                        strOutPut.Append("1");
                        strOutPut.Append("</td>");
                        strOutPut.Append("<td width=\"25%\" align=\"center\" valign=\"middle\">");
                        strOutPut.Append("<div title=\"" + dblRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strRowPrice + "</div>");
                        strOutPut.Append("</td>");
                        strOutPut.Append("<td width=\"25%\" align=\"center\" valign=\"middle\" >");
                        strOutPut.Append("<div title=\"" + dblTotRowPrice + "\" align=\"center\" id=\"ProductPrice\" style=\"width: 127px; height: 24px;\">" + strTotRowPrice + "</div>");
                        strOutPut.Append("</td>");
                        strOutPut.Append("</tr>");
                        strOutPut.Append("</table>");
                        strOutPut.Append("<!--- Cart's Rows Ends--->");
                        strOutPut.Append("</td> ");
                        strOutPut.Append("</tr>");
                    }

                    strOutPut.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                    strOutPut.Append("<tr class=\"tableborder\"><td class=\"tableborder\" colspan=\"5\"></td></tr>");
                    string strTotalGrandPrice = "Rs." + dblTotGrandPrice + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(dblTotGrandPrice / dblCurrencyValue).ToString("0.00");
                    strOutPut.Append("<!--- Cart's Grand Total Section Starts--->");
                    strOutPut.Append("<tr height=\"22px\">");
                    strOutPut.Append("<td colspan=\"4\" align=\"right\" valign=\"middle\" class=\"style1\">");
                    strOutPut.Append("<strong>Grand Total :&nbsp;&nbsp; </strong>");
                    strOutPut.Append("</td>");
                    strOutPut.Append("<td colspan=\"1\" valign=\"middle\" align=\"center\" class=\"small_red\">");
                    strOutPut.Append("<div align=\"center\" id=\"ProductGrantPrice\">");
                    strOutPut.Append("<div align=\"center\" title=\"" + dblTotGrandPrice + "\" id=\"ProductPrice\" >" + strTotalGrandPrice + "</div>");
                    strOutPut.Append("</div>");
                    strOutPut.Append("</td>");
                    strOutPut.Append("</tr>");
                    strOutPut.Append("<!--- Cart's Grand Total Section Ends--->");
                    //strOutPut.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                    strOutPut.Append("</table>");
                    strOutPut.Append("</td>");
                    strOutPut.Append("<td width=\"30%\" valign=\"top\">");
                    strOutPut.Append("<table width=\"100%\" border=\"0\" align=\"left\" cellpadding=\"0\" cellspacing=\"0\" >");
                    strOutPut.Append("<tr>");
                    strOutPut.Append("<td height=\"20\" bgcolor=\"#B31B10\" class=\"style3\"><b>Add Special instructions to this shipment :</b></td> ");
                    strOutPut.Append("<tr>");
                    strOutPut.Append("<td class=\"style2\"><div style=\"text-align:justify; padding:0 5px 0 5px;\">" + strSpecialInstruction + "</div></td> ");
                    strOutPut.Append("</tr>");
                    strOutPut.Append("</table>");
                    strOutPut.Append("</td>");
                    strOutPut.Append("</tr>");
                    strOutPut.Append("</table>");
                    strOutPut.Append("<!-- Cart Details Ends -->");
                    strCartOutput = strOutPut.ToString();
                }
                else
                {
                    strCartOutput = "";
                    strOrderDetailsError = "No product found against the Order number: " + strOrderNo;
                    blFlag = false;
                }
                drOrderProd.Dispose();
            }
            else
            {
                strCartOutput = "";
                strOrderDetailsError = "Currency retrievation error! Please try again.";
                blFlag = false;
            }
        }
        catch (SqlException ex)
        {
            strOrderDetailsError = ex.Message;
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
    protected string getSiteName(string strOrderNumber, bool ComboProduct)
    {
        string strOutput = "";
        try
        {
            if (ComboProduct)
            {
                strSql = "SELECT TOP 1" +
                        "[" + strSchema + "].[POS_BothWay].[POS_Id], " +
                        "[" + strSchema + "].[POS_BothWay].[POS_OName] " +
                    "FROM " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[POS_BothWay] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[POS_Id]=[" + strSchema + "].[POS_BothWay].[POS_Id]) " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[ComboSbill_Relation] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ComboSbill_Relation].[SBillNo])  " +
                    "WHERE " +
                        "([" + strSchema + "].[ComboSbill_Relation].[ComboId]='" + strOrderNumber + "')";
            }
            else
            {
                strSql = "SELECT " +
                        "[" + strSchema + "].[POS_BothWay].[POS_Id], " +
                        "[" + strSchema + "].[POS_BothWay].[POS_OName] " +
                    "FROM " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[POS_BothWay] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[POS_Id]=[" + strSchema + "].[POS_BothWay].[POS_Id]) " +
                    "WHERE " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNumber + "')";
            }
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdSite = new SqlCommand(strSql, conn);
            SqlDataReader drSite = cmdSite.ExecuteReader(CommandBehavior.CloseConnection);
            int intSiteId = 0;
            if (drSite.HasRows)
            {
                if (drSite.Read())
                {
                    strOutput = drSite["POS_OName"].ToString();
                    intSiteId = Convert.ToInt32(drSite["POS_Id"]);
                }
            }
            else
            {
                strOutput = "Site not found";
            }
            drSite.Dispose();
            if (intSiteId != 0)
            {
                SiteId = intSiteId;
                clsRakhi objRakhiHd = new clsRakhi();
                objRakhiHd.functionforImageHead(intSiteId, ref strpageCSS, ref strpageheaderimage, ref strRedirectBaseDomain, ref strGenSitename);
                strGenSitename = objRakhiHd.SentenceCase(strGenSitename.Trim());
                strSiteId = Convert.ToString(intSiteId);
                if ((intSiteId == 132) || (intSiteId == 154) || (intSiteId == 1))      // If its giftbhejo, ndtv.giftstoindia24x7 or giftstoindia24x7 dont show the payment message
                {
                    topMsg.Visible = false;
                    if (intSiteId == 154)
                    {
                        ndtvTracking.InnerHtml = PrintNdtvTracking();
                    }
                }
                else
                {
                    topMsg.Visible = true;
                }
            }
        }
        catch (SqlException ex)
        {
            strOutput = ex.Message;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return strOutput;
    }

    // New methods written by AB
    protected string getGatewayChargeNametoShow(string strOrderNumber)
    {
        string strOutput = "";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[Payment_Gateway_Master].[id], " +
                        "[" + strSchema + "].[Payment_Gateway_Master].[Name], " +
                        "[" + strSchema + "].[Payment_Gateway_Master].[ChargeNameToShow] " +
                    "FROM " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[Payment_Gateway_Master] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[Bank_Id]=[" + strSchema + "].[Payment_Gateway_Master].[ID]) " +
                    "WHERE " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SbillNo]='" + strOrderNumber + "')";
            SqlCommand cmdPg = new SqlCommand(strSql, conn);
            SqlDataReader drPg = cmdPg.ExecuteReader(CommandBehavior.CloseConnection);
            if (drPg.HasRows)
            {
                if (drPg.Read())
                {
                    strOutput = drPg["ChargeNameToShow"].ToString();
                }
            }
            else
            {
                strOutput = "Credit card charge details not found!";
            }
            drPg.Dispose();
        }
        catch (SqlException ex)
        {
            strOutput = ex.Message;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return strOutput;
    }
    public string GetTrackDetails(string strOrderNumber)
    {
        //string strOutputFinal = "";
        string strTotPrice = "";
        string strCityName = "";
        string strStateName = "";
        string strCountryName = "";

        System.Text.StringBuilder Tracking = new System.Text.StringBuilder();
        Tracking.Append("var pageTracker = _gat._getTracker(\"UA-1270228-1\");" + (char)13);
        Tracking.Append("pageTracker._trackPageview();" + (char)13);

        System.Text.StringBuilder ndtvTracking = new System.Text.StringBuilder();
        ndtvTracking.Append("<!-- Individual Starts -->" + (char)13);
        ndtvTracking.Append("<script type=\"text/javascript\">" + (char)13);
        ndtvTracking.Append("var gaJsHost = ((\"https:\" == document.location.protocol) ? \"https://ssl.\" : \"http://www.\");" + (char)13);
        ndtvTracking.Append("document.write(unescape(\"%3Cscript src='\" + gaJsHost + \"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E\"));" + (char)13);
        ndtvTracking.Append("</script>" + (char)13);
        ndtvTracking.Append("<script type=\"text/javascript\">" + (char)13);
        ndtvTracking.Append("try {" + (char)13);
        ndtvTracking.Append("var pageTracker = _gat._getTracker(\"UA-1270228-16\");" + (char)13);
        ndtvTracking.Append("pageTracker._trackPageview();" + (char)13);
        strError = "";
        bool ComboProduct = false;
        if (strOrderNo.ToString().Substring(0, 1).Equals("C"))
        {
            ComboProduct = true;
        }
        if (GetTotPriceAndShipInfo(strOrderNumber, ComboProduct, ref strTotPrice, ref strCityName, ref strStateName, ref strCountryName, ref strError))
        {
            try
            {
                //strOutputFinal = "UTM:T|" + strOrderNumber + "|Main Store|" + strTotPrice + "|0.00|0.00|" + strCityName + "|" + strStateName + "|" + strCountryName + "<br/>";
                Tracking.Append("pageTracker._addTrans(" + (char)13);
                Tracking.Append("\"" + strOrderNumber + "\"," + (char)13);                // Order ID
                Tracking.Append("\"Main Store\"," + (char)13);                            // Affiliation
                Tracking.Append("\"" + strTotPrice + "\"," + (char)13);                   // Total
                Tracking.Append("\"0.00\"," + (char)13);                                  // Tax
                Tracking.Append("\"0.00\"," + (char)13);                                  // Shipping
                Tracking.Append("\"" + strCityName + "\"," + (char)13);                   // City
                Tracking.Append("\"" + strStateName + "\"," + (char)13);                  // State
                Tracking.Append("\"" + strCountryName + "\"" + (char)13);                 // Country
                Tracking.Append(");" + (char)13);

                ndtvTracking.Append("pageTracker._addTrans(" + (char)13);
                ndtvTracking.Append("\"" + strOrderNumber + "\"," + (char)13);                // Order ID
                ndtvTracking.Append("\"Main Store\"," + (char)13);                            // Affiliation
                ndtvTracking.Append("\"" + strTotPrice + "\"," + (char)13);                   // Total
                ndtvTracking.Append("\"0.00\"," + (char)13);                                  // Tax
                ndtvTracking.Append("\"0.00\"," + (char)13);                                  // Shipping
                ndtvTracking.Append("\"" + strCityName + "\"," + (char)13);                   // City
                ndtvTracking.Append("\"" + strStateName + "\"," + (char)13);                  // State
                ndtvTracking.Append("\"" + strCountryName + "\"" + (char)13);                 // Country
                ndtvTracking.Append(");" + (char)13);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                if (ComboProduct)
                {
                    strSql = "SELECT " +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[SBillNo]," +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[Product_Id]," +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[QOS]," +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[Price]," +
                                 "  [" + strSchema + "].[ItemMaster_Server].[Product_Id]," +
                                 "  [" + strSchema + "].[ItemMaster_Server].[Item_CartName]," +
                                 "  [" + strSchema + "].[ItemMaster_Server].[Item_ImagePath]" +
                                 " FROM" +
                                 "  [" + strSchema + "].[SalesDetails_BothWay] " +
                                 "INNER JOIN " +
                                    "[" + strSchema + "].[ItemMaster_Server] " +
                                 "ON " +
                                    "([" + strSchema + "].[SalesDetails_BothWay].[Product_Id]=[" + strSchema + "].[ItemMaster_Server].[Product_Id]) " +
                                 "INNER JOIN " +
                                    "[" + strSchema + "].[ComboSbill_Relation] " +
                                 "ON " +
                                    "([" + strSchema + "].[SalesDetails_BothWay].[SBillNo]=[" + strSchema + "].[ComboSbill_Relation].[SbillNo]) " +
                                 "WHERE " +
                                    "([" + strSchema + "].[ComboSbill_Relation].[ComboId]='" + strOrderNumber + "') " +
                                 "ORDER BY " +
                                    "[" + strSchema + "].[SalesDetails_BothWay].[SBillNo];";
                }
                else
                {
                    strSql = "SELECT " +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[SBillNo]," +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[Product_Id]," +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[QOS]," +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[Price]," +
                                     "  [" + strSchema + "].[ItemMaster_Server].[Product_Id]," +
                                     "  [" + strSchema + "].[ItemMaster_Server].[Item_CartName]," +
                                     "  [" + strSchema + "].[ItemMaster_Server].[Item_ImagePath]" +
                                     " FROM" +
                                     "  [" + strSchema + "].[SalesDetails_BothWay]," +
                                     "  [" + strSchema + "].[ItemMaster_Server]" +
                                     " WHERE" +
                                       "  ([" + strSchema + "].[SalesDetails_BothWay].[Product_Id] = [" + strSchema + "].[ItemMaster_Server].[Product_Id]) AND" +
                                       "  ([" + strSchema + "].[SalesDetails_BothWay].[SBillNo] = '" + strOrderNumber.ToString() + "')";
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        ////string strOutput = "UTM:I|" + strOrderNumber + "|" + dr["Product_Id"].ToString() + "|" + dr["Item_CartName"].ToString() + "|Main|" + dr["Price"].ToString() + "|" + dr["QOS"].ToString() + "<br>";
                        //string strOutput = "UTM:I|" + dr["SBillNo"].ToString() + "|" + dr["Product_Id"].ToString() + "|" + dr["Item_CartName"].ToString() + "|Main|" + dr["Price"].ToString() + "|" + dr["QOS"].ToString() + "<br/>";
                        //strOutputFinal = strOutputFinal + strOutput;
                        Tracking.Append("pageTracker._addItem(" + (char)13);
                        Tracking.Append("\"" + dr["SBillNo"].ToString() + "\"," + (char)13);         // Order ID
                        Tracking.Append("\"" + dr["Product_Id"].ToString() + "\"," + (char)13);      // SKU
                        Tracking.Append("\"" + dr["Item_CartName"].ToString() + "\"," + (char)13);   // Product Name 
                        Tracking.Append("\"Main\"," + (char)13);                                     // Category
                        Tracking.Append("\"" + dr["Price"].ToString() + "\"," + (char)13);           // Price
                        Tracking.Append("\"" + dr["QOS"].ToString() + "\"" + (char)13);              // Quantity
                        Tracking.Append(");" + (char)13);

                        ndtvTracking.Append("pageTracker._addItem(" + (char)13);
                        ndtvTracking.Append("\"" + dr["SBillNo"].ToString() + "\"," + (char)13);         // Order ID
                        ndtvTracking.Append("\"" + dr["Product_Id"].ToString() + "\"," + (char)13);      // SKU
                        ndtvTracking.Append("\"" + dr["Item_CartName"].ToString() + "\"," + (char)13);   // Product Name 
                        ndtvTracking.Append("\"Main\"," + (char)13);                                     // Category
                        ndtvTracking.Append("\"" + dr["Price"].ToString() + "\"," + (char)13);           // Price
                        ndtvTracking.Append("\"" + dr["QOS"].ToString() + "\"" + (char)13);              // Quantity
                        ndtvTracking.Append(");" + (char)13);
                    }
                }
            }
            catch (SqlException ex)
            {
                //strOutputFinal = ex.Message;
                Tracking.Remove(0, Tracking.Length);
                Tracking.Append("Error");
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }
        else
        {
            //strOutputFinal = "No data found.";
            try
            {
                //strOutputFinal = "UTM:T|" + strOrderNumber + "|Main Store|" + strTotPrice + "|0.00|0.00|" + strCityName + "|" + strStateName + "|" + strCountryName + "<br/>";
                Tracking.Append("pageTracker._addTrans(" + (char)13);
                Tracking.Append("\"" + strOrderNumber + "\"," + (char)13);                // Order ID
                Tracking.Append("\"Main Store\"," + (char)13);                            // Affiliation
                Tracking.Append("\"" + strTotPrice + "\"," + (char)13);                   // Total
                Tracking.Append("\"0.00\"," + (char)13);                                  // Tax
                Tracking.Append("\"0.00\"," + (char)13);                                  // Shipping
                Tracking.Append("\"" + strCityName + "\"," + (char)13);                   // City
                Tracking.Append("\"" + strStateName + "\"," + (char)13);                  // State
                Tracking.Append("\"" + strCountryName + "\"" + (char)13);                 // Country
                Tracking.Append(");" + (char)13);

                ndtvTracking.Append("pageTracker._addTrans(" + (char)13);
                ndtvTracking.Append("\"" + strOrderNumber + "\"," + (char)13);                // Order ID
                ndtvTracking.Append("\"Main Store\"," + (char)13);                            // Affiliation
                ndtvTracking.Append("\"" + strTotPrice + "\"," + (char)13);                   // Total
                ndtvTracking.Append("\"0.00\"," + (char)13);                                  // Tax
                ndtvTracking.Append("\"0.00\"," + (char)13);                                  // Shipping
                ndtvTracking.Append("\"" + strCityName + "\"," + (char)13);                   // City
                ndtvTracking.Append("\"" + strStateName + "\"," + (char)13);                  // State
                ndtvTracking.Append("\"" + strCountryName + "\"" + (char)13);                 // Country
                ndtvTracking.Append(");" + (char)13);

                if (conn.State != ConnectionState.Open) conn.Open();
                if (ComboProduct)
                {
                    strSql = "SELECT " +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[SBillNo]," +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[Product_Id]," +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[QOS]," +
                                 "  [" + strSchema + "].[SalesDetails_BothWay].[Price]," +
                                 "  [" + strSchema + "].[ItemMaster_Server].[Product_Id]," +
                                 "  [" + strSchema + "].[ItemMaster_Server].[Item_CartName]," +
                                 "  [" + strSchema + "].[ItemMaster_Server].[Item_ImagePath]" +
                                 " FROM" +
                                 "  [" + strSchema + "].[SalesDetails_BothWay] " +
                                 "INNER JOIN " +
                                    "[" + strSchema + "].[ItemMaster_Server] " +
                                 "ON " +
                                    "([" + strSchema + "].[SalesDetails_BothWay].[Product_Id]=[" + strSchema + "].[ItemMaster_Server].[Product_Id]) " +
                                 "INNER JOIN " +
                                    "[" + strSchema + "].[ComboSbill_Relation] " +
                                 "ON " +
                                    "([" + strSchema + "].[SalesDetails_BothWay].[SBillNo]=[" + strSchema + "].[ComboSbill_Relation].[SbillNo]) " +
                                 "WHERE " +
                                    "([" + strSchema + "].[ComboSbill_Relation].[ComboId]='" + strOrderNumber + "');";
                }
                else
                {
                    strSql = "SELECT " +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[SBillNo]," +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[Product_Id]," +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[QOS]," +
                                     "  [" + strSchema + "].[SalesDetails_BothWay].[Price]," +
                                     "  [" + strSchema + "].[ItemMaster_Server].[Product_Id]," +
                                     "  [" + strSchema + "].[ItemMaster_Server].[Item_CartName]," +
                                     "  [" + strSchema + "].[ItemMaster_Server].[Item_ImagePath]" +
                                     " FROM" +
                                     "  [" + strSchema + "].[SalesDetails_BothWay]," +
                                     "  [" + strSchema + "].[ItemMaster_Server]" +
                                     " WHERE" +
                                       "  ([" + strSchema + "].[SalesDetails_BothWay].[Product_Id] = [" + strSchema + "].[ItemMaster_Server].[Product_Id]) AND" +
                                       "  ([" + strSchema + "].[SalesDetails_BothWay].[SBillNo] = '" + strOrderNumber.ToString() + "')";
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        //string strOutput = "UTM:I|" + strOrderNumber + "|" + dr["Product_Id"].ToString() + "|" + dr["Item_CartName"].ToString() + "|Main|" + dr["Price"].ToString() + "|" + dr["QOS"].ToString() + "<br/>";
                        //strOutputFinal = strOutputFinal + strOutput;
                        Tracking.Append("pageTracker._addItem(" + (char)13);
                        Tracking.Append("\"" + dr["SBillNo"].ToString() + "\"," + (char)13);         // Order ID
                        Tracking.Append("\"" + dr["Product_Id"].ToString() + "\"," + (char)13);      // SKU
                        Tracking.Append("\"" + dr["Item_CartName"].ToString() + "\", " + (char)13);   // Product Name
                        Tracking.Append("\"Main\"," + (char)13);                                     // Category
                        Tracking.Append("\"" + dr["Price"].ToString() + "\"," + (char)13);           // Price
                        Tracking.Append("\"" + dr["QOS"].ToString() + "\"" + (char)13);              // Quantity
                        Tracking.Append(");" + (char)13);

                        ndtvTracking.Append("pageTracker._addItem(" + (char)13);
                        ndtvTracking.Append("\"" + dr["SBillNo"].ToString() + "\"," + (char)13);         // Order ID
                        ndtvTracking.Append("\"" + dr["Product_Id"].ToString() + "\"," + (char)13);      // SKU
                        ndtvTracking.Append("\"" + dr["Item_CartName"].ToString() + "\"," + (char)13);   // Product Name 
                        ndtvTracking.Append("\"Main\"," + (char)13);                                     // Category
                        ndtvTracking.Append("\"" + dr["Price"].ToString() + "\"," + (char)13);           // Price
                        ndtvTracking.Append("\"" + dr["QOS"].ToString() + "\"" + (char)13);              // Quantity
                        ndtvTracking.Append(");" + (char)13);
                    }
                }
            }
            catch (SqlException ex)
            {
                //strOutputFinal = strOutputFinal + ex.Message;
                Tracking.Append("Error");
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        ndtvTracking.Append("pageTracker._trackTrans();" + (char)13);
        ndtvTracking.Append("} catch(err) {}</script>" + (char)13);
        ndtvTracking.Append("<!-- Individual Ends -->" + (char)13);
        if (strSiteId != "" && strSiteId == "154")
        {
            //ndtvOrderTrack.InnerHtml = Convert.ToString(ndtvTracking);
        }
        //return strOutputFinal;
        return Convert.ToString(Tracking);
    }
    public string GetTotPrice(string strOrderNumber)
    {
        string strtotAmount = "";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            if (strOrderNo.ToString().Substring(0, 1).Equals("C"))
            {
                strSql = "SELECT " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[SBillNo], " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT], " +
                                "[" + strSchema + "].[City_Server].[City_Name], " +
                                "[" + strSchema + "].[State_Server].[State_Name], " +
                                "[" + strSchema + "].[Country_Server].[Country_Name] " +
                            "FROM " +
                                "[" + strSchema + "].[SalesMaster_BothWay] " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ShippingDetails_Bothway] " +
                            "ON " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[City_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId]=[" + strSchema + "].[City_Server].[City_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[State_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId]=[" + strSchema + "].[State_Server].[State_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[Country_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId]=[" + strSchema + "].[Country_Server].[Country_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ComboSbill_Relation] " +
                            "ON " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ComboSbill_Relation].[SbillNo]) " +
                            "WHERE " +
                                "([" + strSchema + "].[ComboSbill_Relation].[ComboId]='" + strOrderNumber + "');";
            }
            else
            {
                strSql = "SELECT " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[SBillNo], " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT], " +
                                "[" + strSchema + "].[City_Server].[City_Name], " +
                                "[" + strSchema + "].[State_Server].[State_Name], " +
                                "[" + strSchema + "].[Country_Server].[Country_Name] " +
                            "FROM " +
                                "[" + strSchema + "].[SalesMaster_BothWay] " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ShippingDetails_Bothway] " +
                            "ON " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[City_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId]=[" + strSchema + "].[City_Server].[City_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[State_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId]=[" + strSchema + "].[State_Server].[State_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[Country_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId]=[" + strSchema + "].[Country_Server].[Country_Id]) " +
                            "WHERE " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo] = '" + strOrderNumber + "')";
            }
            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                double dblTotal = 0.00;
                while (dr.Read())
                {
                    dblTotal = dblTotal + Convert.ToDouble(dr["Sales_ATOT"]);
                    //strtotAmount = dr["Sales_ATOT"].ToString();
                }
                strtotAmount = dblTotal.ToString("0.00");
            }
        }
        catch (SqlException ex)
        {
            strtotAmount = "0";
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return strtotAmount;
    }
    public bool GetTotPriceAndShipInfo(string strOrderNumber, ref string strTotalAmount, ref string strCityName, ref string strStateName, ref string strCountryName, ref string strShipError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                            "[" + strSchema + "].[SalesMaster_BothWay].[SBillNo], " +
                            "[" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT], " +
                            "[" + strSchema + "].[City_Server].[City_Name], " +
                            "[" + strSchema + "].[State_Server].[State_Name], " +
                            "[" + strSchema + "].[Country_Server].[Country_Name] " +
                        "FROM " +
                            "[" + strSchema + "].[SalesMaster_BothWay] " +
                        "INNER JOIN " +
                            "[" + strSchema + "].[ShippingDetails_Bothway] " +
                        "ON " +
                            "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
                        "INNER JOIN " +
                            "[" + strSchema + "].[City_Server] " +
                        "ON " +
                            "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId]=[" + strSchema + "].[City_Server].[City_Id]) " +
                        "INNER JOIN " +
                            "[" + strSchema + "].[State_Server] " +
                        "ON " +
                            "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId]=[" + strSchema + "].[State_Server].[State_Id]) " +
                        "INNER JOIN " +
                            "[" + strSchema + "].[Country_Server] " +
                        "ON " +
                            "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId]=[" + strSchema + "].[Country_Server].[Country_Id]) " +
                        "WHERE " +
                            "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo] = '" + strOrderNumber + "')";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                blFlag = true;
                if (dr.Read())
                {
                    strTotalAmount = Convert.ToString(dr["Sales_ATOT"].ToString());
                    strCityName = Convert.ToString(dr["City_Name"].ToString());
                    strStateName = Convert.ToString(dr["State_Name"].ToString());
                    strCountryName = Convert.ToString(dr["Country_Name"].ToString());
                }
            }
            else
            {
                blFlag = false;
                strShipError = "No data found.";
            }
        }
        catch (SqlException ex)
        {
            strShipError = ex.Message;
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
    public bool GetTotPriceAndShipInfo(string strOrderNumber, bool ComboProduct, ref string strTotalAmount, ref string strCityName, ref string strStateName, ref string strCountryName, ref string strShipError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            if (ComboProduct)
            {
                strSql = "SELECT " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[SBillNo], " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT], " +
                                "[" + strSchema + "].[City_Server].[City_Name], " +
                                "[" + strSchema + "].[State_Server].[State_Name], " +
                                "[" + strSchema + "].[Country_Server].[Country_Name] " +
                            "FROM " +
                                "[" + strSchema + "].[SalesMaster_BothWay] " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ShippingDetails_Bothway] " +
                            "ON " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[City_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId]=[" + strSchema + "].[City_Server].[City_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[State_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId]=[" + strSchema + "].[State_Server].[State_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[Country_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId]=[" + strSchema + "].[Country_Server].[Country_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ComboSbill_Relation] " +
                            "ON " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ComboSbill_Relation].[SbillNo]) " +
                            "WHERE " +
                                "([" + strSchema + "].[ComboSbill_Relation].[ComboId]='" + strOrderNumber + "');";
            }
            else
            {
                strSql = "SELECT " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[SBillNo], " +
                                "[" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT], " +
                                "[" + strSchema + "].[City_Server].[City_Name], " +
                                "[" + strSchema + "].[State_Server].[State_Name], " +
                                "[" + strSchema + "].[Country_Server].[Country_Name] " +
                            "FROM " +
                                "[" + strSchema + "].[SalesMaster_BothWay] " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[ShippingDetails_Bothway] " +
                            "ON " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[City_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId]=[" + strSchema + "].[City_Server].[City_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[State_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId]=[" + strSchema + "].[State_Server].[State_Id]) " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[Country_Server] " +
                            "ON " +
                                "([" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId]=[" + strSchema + "].[Country_Server].[Country_Id]) " +
                            "WHERE " +
                                "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo] = '" + strOrderNumber + "')";
            }
            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                blFlag = true;
                if (ComboProduct)
                {
                    double dblTotal = 0.00;
                    while (dr.Read())
                    {
                        dblTotal = dblTotal + Convert.ToDouble(dr["Sales_ATOT"]);
                        strCityName = Convert.ToString(dr["City_Name"].ToString());
                        strStateName = Convert.ToString(dr["State_Name"].ToString());
                        strCountryName = Convert.ToString(dr["Country_Name"].ToString());
                    }
                    strTotalAmount = dblTotal.ToString("0.00");
                }
                else
                {
                    if (dr.Read())
                    {
                        strTotalAmount = Convert.ToString(dr["Sales_ATOT"].ToString());
                        strCityName = Convert.ToString(dr["City_Name"].ToString());
                        strStateName = Convert.ToString(dr["State_Name"].ToString());
                        strCountryName = Convert.ToString(dr["Country_Name"].ToString());
                    }
                }
            }
            else
            {
                blFlag = false;
                strShipError = "No data found.";
            }
        }
        catch (SqlException ex)
        {
            strShipError = ex.Message;
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
    public void sendMailtoBuyer(string orderno)
    {
        string strSQL = "";
        string strBody = "";
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }

        if (orderno.Substring(0, 1).Equals("C"))
        {
            mailWait(orderno, 0); // means the order is combo.
        }
        else
        {
            mailWait(orderno, 1); // means the order is single.
        }
    }
    public void mailWait(string strComboId, int sbillCombo)
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
                " <title>:: " + strGenSitename + " ::</title></head><body><font face='verdana' size=2> " +
                " <table width='100%' height='330' border='0'>";
        //"<tr><td colspan='2'><img src='http://" + strGenSitename + "/Pictures/InvoiveHeader.jpg' width='600' height='108'></td></tr>";
        //"<tr><td height='37' width='100%' cellpadding='0' cellspacing='0'>";

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
            //strSQL = "SELECT " +
            //        "rgcards_gti24x7.ComboSbill_Relation.SBillNo," +
            //        "(rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name+" +
            //        "' '+CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1)+" +
            //        "' '+CONVERT(varchar(100),rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2)+" +
            //        "'  '+rgcards_gti24x7.City_Server.City_Name+" +
            //        "'  '+rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode +" +
            //        "'  '+rgcards_gti24x7.State_Server.State_Name +" +
            //        "'  '+rgcards_gti24x7.Country_Server.Country_Name) as Address," +
            //        " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
            //        "rgcards_gti24x7.Payment_Gateway_Master.ChargeNameToShow," +
            //        " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile ," +
            //        " rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email," +
            //        " CONVERT(CHAR(19),rgcards_gti24x7.SalesMaster_BothWay.DOD,106) AS DOD, " +
            //        " rgcards_gti24x7.SalesMaster_BothWay.MWG," +
            //        " rgcards_gti24x7.SalesMaster_BothWay.SInstruction " +
            //        " FROM rgcards_gti24x7.ComboSbill_Relation " +
            //        " INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo) " +
            //        " INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id) " +
            //        " INNER JOIN rgcards_gti24x7.Payment_Gateway_Master ON (rgcards_gti24x7.SalesMaster_BothWay.Bank_Id = rgcards_gti24x7.Payment_Gateway_Master.ID)" +
            //        " INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id) " +
            //        " INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id) " +
            //        " INNER JOIN rgcards_gti24x7.SalesMaster_BothWay ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.SalesMaster_BothWay.SBillNo)" +
            //        " WHERE  (rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";

            strSQL = "SELECT " +
                    "rgcards_gti24x7.ComboSbill_Relation.SBillNo, " +
                    "(rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Name + ' ' + CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address1) + ' ' + CONVERT(varchar(100), rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Address2) + '  ' + rgcards_gti24x7.City_Server.City_Name + '  ' + rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PinCode + '  ' + rgcards_gti24x7.State_Server.State_Name + '  ' + rgcards_gti24x7.Country_Server.Country_Name) AS Address, " +
                    "rgcards_gti24x7.ShippingDetails_Bothway.Shipping_PhNo, " +
                    "rgcards_gti24x7.Payment_Gateway_Master.ChargeNameToShow, " +
                    "rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Mobile, " +
                    "rgcards_gti24x7.ShippingDetails_Bothway.Shipping_Email, " +
                    "CONVERT(CHAR(19), rgcards_gti24x7.SalesMaster_BothWay.DOD, 106) AS DOD, " +
                    "rgcards_gti24x7.SalesMaster_BothWay.MWG, " +
                    "rgcards_gti24x7.payment_gateway_master.name AS [BankName]," +
                    "rgcards_gti24x7.SalesMaster_BothWay.SInstruction " +
                    "FROM " +
                    "rgcards_gti24x7.ComboSbill_Relation " +
                    "INNER JOIN rgcards_gti24x7.ShippingDetails_Bothway ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.ShippingDetails_Bothway.SBillNo) " +
                    "INNER JOIN rgcards_gti24x7.City_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CityId = rgcards_gti24x7.City_Server.City_Id) " +
                    "INNER JOIN rgcards_gti24x7.State_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_StateId = rgcards_gti24x7.State_Server.State_Id) " +
                    "INNER JOIN rgcards_gti24x7.Country_Server ON (rgcards_gti24x7.ShippingDetails_Bothway.Shipping_CountryId = rgcards_gti24x7.Country_Server.Country_Id) " +
                    "INNER JOIN rgcards_gti24x7.SalesMaster_BothWay ON (rgcards_gti24x7.ComboSbill_Relation.SBillNo = rgcards_gti24x7.SalesMaster_BothWay.SBillNo) " +
                    "INNER JOIN rgcards_gti24x7.Payment_Gateway_Master ON (rgcards_gti24x7.SalesMaster_BothWay.Bank_Id = rgcards_gti24x7.Payment_Gateway_Master.ID) " +
                    "WHERE " +
                    "(rgcards_gti24x7.ComboSbill_Relation.ComboId = '" + strComboId + "')";


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
                    "rgcards_gti24x7.payment_gateway_master.name AS [BankName]," +
                    "rgcards_gti24x7.payment_gateway_master.chargenametoshow," +
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
                    " INNER JOIN rgcards_gti24x7.payment_gateway_master ON (rgcards_gti24x7.payment_gateway_master.id = rgcards_gti24x7.SalesMaster_BothWay.bank_id)" +
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
                    "<tr><td><strong>Email</strong>:<a href='mailTo:sales@giftstoindia24x7.com'>sales@" + strGenSitename + "</a></td></tr>" +
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
                strChangeNametoShow = dtDetails.Rows[0]["ChargeNameToShow"].ToString();//======get declaration line
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
                                    "<tr><td colspan='2' align=\"center\">Discount:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + objRakhi.fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()) + "</td></tr>" +
                                    "<tr><td colspan='2' align=\"center\">Payable:&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td><td align=\"center\">" + (totalProPrice - Convert.ToDecimal(objRakhi.fnGetDiscount(dtDetails.Rows[i]["SBillNo"].ToString()))) + "</td></tr>" +
                                    "</table></td></tr></table></td></tr>";
                intTotQty = 0;
                dtProduct.Reset();
            }

            ////string status = "";
            ////if (Request.QueryString.Get("PT") != null)
            ////{
            ////    status = Request.QueryString["PT"].ToString();
            ////}
            ////else
            ////{
            ////    status = "2";
            ////}
            ////if (status == "1")
            ////{
            // EBS
            strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
                            "<tr><td colspan='2' ><li>Thank you for placing an order with " + strGenSitename + ". Please preseve this order for future reference.</li></td></tr>" +
                            "<tr><td colspan='2' ><li>" + strChangeNametoShow + "</li></td></tr>" +
                            "<tr><td colspan='2' ><li>The date of delivery selected by you will only applicable for perishable gifts like flowers, cakes, fruits etc. </li></td></tr>" +
                            "<tr><td colspan='2' ><li>Perishable products (like Gift vouchers, perfumes, rakhis etc) are sent by courier and cannot be delivered on a specific day. We will email you a tracking no. once the items are couriered.</li></td></tr>" +
                            "<tr><td colspan='2' ><li>All deliveries are done during the day time between 10 AM to 6.30 PM. if you have mentioned a specific time of delivery above, we will try our level best to follow the same. Products can only be delivered at a residence or a business address. they can not be delivered to a wedding hall or a function hall.</li></td></tr>" +
                            "<tr><td colspan='2' ><li> Midnight orders are delivered between 23.45 Hrs to 00.15 Hrs.</li></td></tr>" +
                            "<tr><td colspan='2' ><li>For any assistance, please email us at sales@giftstoindia24x7.com starting your Order no.</li></td></tr>" +
                            "<tr><td colspan='2' ><li>All online purchase will be billed in your local currency.</li></ul></td></tr>";
            ////}
            ////else
            ////{
            ////    //PAYPAL
            ////    strBody = strBody + "<tr><td colspan='2'><ul type='circle'></td></tr>" +
            ////                    "<tr><td colspan='2' ><li>Thank you for placing an order with Rakhi24x7.com. Please preseve this order for future reference.</li></td></tr>" +
            ////                    "<tr><td colspan='2' ><li>Your Credit Card statement will show a charge as PAYPAL *GIFTSTOINDI. www.rakhi24x7.com is a part of Giftstoindia24x7.com.</li></td></tr>" +
            ////                    "<tr><td colspan='2' ><li>The date of delivery selected by you will only applicable for perishable gifts like flowers, cakes, fruits etc. </li></td></tr>" +
            ////                    "<tr><td colspan='2' ><li>Perishable products (like Gift vouchers, perfumes, rakhis etc) are sent by courier and cannot be delivered on a specific day. We will email you a tracking no. once the items are couriered.</li></td></tr>" +
            ////                    "<tr><td colspan='2' ><li>All deliveries are done during the day time between 10 AM to 6.30 PM. if you have mentioned a specific time of delivery above, we will try our level best to follow the same. Products can only be delivered at a residence or a business address. they can not be delivered to a wedding hall or a function hall.</li></td></tr>" +
            ////                    "<tr><td colspan='2' ><li> Midnight orders are delivered between 23.45 Hrs to 00.15 Hrs.</li></td></tr>" +
            ////                    "<tr><td colspan='2' ><li>For any assistance, please email us at sales@giftstoindia24x7.com starting your Order no.</li></td></tr>" +
            ////                    "<tr><td colspan='2' ><li>All online purchase will be billed in your local currency.</li></ul></td></tr>";
            ////}

            strBody = strBody + "</td></tr><tr><td width='100%' colspan='2'><strong>Declaration:</strong><ul type='circle'><li>We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.</li></ul>" +
                    "</td></tr><tr><td width='100%' colspan='2'>Sales Team</td></tr>" +
                    "<tr><td width='100%' colspan='2'>" + strGenSitename + "</td></tr><tr><td width='100%' colspan='2'>+91.933.953.0030</td></tr>" +
                    "</tr></table></font></body></html>";
            sendingMail(dtBillDetails.Rows[0]["Billing_Email"].ToString(), "" + strGenSitename + "<sales@giftstoindia24x7.com>", strBody, "Success Against your order", strComboId);
        }
        conn.Close();
    }
    public void sendingMail(string strMailTo, string strMailFrom, string strMailBody, string strMailSubject, string id)
    {
        string strPaymentGateNm = "";
        if (strOrderNo.ToString().Substring(0, 1).Equals("C"))
        {
            DataTable dt = objRakhi.fnGrtOrderCombo(id);
            //strPaymentGateNm = objRakhi.fnGetGateWayName(dt.Rows[0]["SBillNo"].ToString());
        }
        else
        {
           // strPaymentGateNm = objRakhi.fnGetGateWayName(strOrderNo);

        }
        string strSubject = "Order No. : " + id + " - " + strGenSitename + " - " + "" + strPaymentGateNm + "";
        string strMailError = "";

        /////////////////////////////////////////////////////////////////////////////
        // Twitter and Facebook Implementation
        /////////////////////////////////////////////////////////////////////////////
        //chkpartnerdetails obj = new chkpartnerdetails(Convert.ToString(ConfigurationManager.AppSettings["dbCon"]), Convert.ToString(ConfigurationManager.AppSettings["Schema"]));
        //if (obj.CheckStatusByOrderNumber(strOrderNo))
        //{
        //    if (obj.Message == "RGCARDS.COM")
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        sb.Append("<table width=\"650\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">");
        //        sb.Append("<tr>");
        //        sb.Append("<td width=\"22\" align=\"left\" valign=\"top\" style=\"line-height:24px; background-color:#39badd; font-size:21px; font-weight:bold; font-family:Verdana, Geneva, sans-serif; color:#fff; text-align:center;\"><a href=\"http://twitter.com/gifts2india24x7\" style=\"color:#fff; text-decoration:none;\" title=\"twitter\">t</a></td>");
        //        sb.Append("<td width=\"55\" align=\"left\" valign=\"middle\" style=\"font-size:12px; font-weight:bold; font-family:Arial, Helvetica, sans-serif; color:#3a3a3a; padding-left:4px;\"><a href=\"http://twitter.com/gifts2india24x7\" target=\"_blank\" style=\"color:#3a3a3a; text-decoration:none;\">Twitter</a></td>");
        //        sb.Append("<td width=\"19\" align=\"left\" valign=\"top\" style=\"line-height:24px; background-color:#3a5896; font-size:21px; font-weight:bold; font-family:Verdana, Geneva, sans-serif; color:#fff; text-align:right; padding-right:3px\"><a href=\"http://www.facebook.com/GiftstoIndia?v=app_4949752878\" style=\"color:#fff; text-decoration:none;\" title=\"facebook\">f</a></td>");
        //        sb.Append("<td align=\"left\" valign=\"middle\" style=\"font-size:12px; font-weight:bold; font-family:Arial, Helvetica, sans-serif; color:#3a3a3a; padding-left:4px;\"><a href=\"http://www.facebook.com/GiftstoIndia?v=app_4949752878\" target=\"_blank\" style=\"color:#3a3a3a; text-decoration:none;\">Facebook</a></td>");
        //        sb.Append("</tr>");
        //        sb.Append("</table>");

        //        strMailBody += "<div align=\"center\">" + Convert.ToString(sb) + "</div>";
        //    }
        //}

        ///////////////////////////////////////////////////////////////////////////////

        //////if (objCommonFunction.SendMail(strMailTo, strMailTo + "<" + strMailTo + ">", "sales@rakhi24x7.com", strMailBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
        //if (objCommonFunction.SendMail(strMailTo, "" + strGenSitename + "<sales@giftstoindia24x7.com>", "sales@giftstoindia24x7.com", strMailBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
        //{
        //    strMailError = "The Success mail is been sent.";
        //}
        //else
        //{
        //    strMailError = strError;
        //}
        //==============to admin===============
        strSubject = "Order No. : " + id + " - " + strGenSitename + " - " + strPaymentGateNm + " (Success)";
        //if (objCommonFunction.SendMail("sales@giftstoindia24x7.com", "" + strGenSitename + "" + "<sales@giftstoindia24x7.com>", strMailTo, strMailBody, true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(Application["STMPPort"].ToString()), false, Application["STMPEmailAccountName"].ToString(), Application["STMPEmailAccountPassword"].ToString(), ref strError))
        //{
        //    strMailError = "The Success mail is been sent.";
        //}
        //else
        //{
        //    strMailError = strError;
        //}

    }
    public string fnsChangeNametoShow(string strorder)
    {
        string strret = "";
        strSql = "select rgcards_gti24x7.payment_gateway_master.chargenametoshow" +
                 " from " +
                 "rgcards_gti24x7.SalesMaster_BothWay" +
                 " INNER JOIN rgcards_gti24x7.payment_gateway_master ON (rgcards_gti24x7.payment_gateway_master.id = rgcards_gti24x7.SalesMaster_BothWay.bank_id)" +
                 " where (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = '" + strorder + "')";

        SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            strret = dt.Rows[0]["chargenametoshow"].ToString();
        }
        da.Dispose();
        return strret;
    }
    public string fnUserName(string strOrderNumber)
    {
        string strOutput = "";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[billingdetails_bothway].[Billing_Name] " +
                    "FROM " +
                        "[" + strSchema + "].[billingdetails_bothway] " +
                    "WHERE " +
                        "([" + strSchema + "].[billingdetails_bothway].[SBillNo]='" + strOrderNumber + "')";
            SqlCommand cmdSite = new SqlCommand(strSql, conn);
            SqlDataReader drSite = cmdSite.ExecuteReader(CommandBehavior.CloseConnection);
            if (drSite.HasRows)
            {
                if (drSite.Read())
                {
                    strOutput = drSite["Billing_Name"].ToString();
                }
            }
            else
            {

            }
            drSite.Dispose();
        }
        catch (SqlException ex)
        {
            strOutput = ex.Message;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return strOutput;
    }
    protected string PrintNdtvTracking()
    {
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        str.Append("<!-- Google Analytics Starts -->" + (char)13);
        str.Append("<script type=\"text/javascript\">" + (char)13);
        str.Append("var gaJsHost = ((\"https:\" == document.location.protocol) ? \"https://ssl.\" : \"http://www.\");" + (char)13);
        str.Append("document.write(unescape(\"%3Cscript src='\" + gaJsHost + \"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E\"));" + (char)13);
        str.Append("</script>" + (char)13);
        str.Append("<script type=\"text/javascript\">" + (char)13);
        str.Append("try {" + (char)13);
        str.Append("var pageTracker = _gat._getTracker(\"UA-2598638-54\");" + (char)13);
        str.Append("pageTracker._trackPageview();" + (char)13);
        str.Append("} catch(err) {}" + (char)13);
        str.Append("</script>" + (char)13);
        str.Append("<!-- Google Analytics Ends -->" + (char)13);

        str.Append("<!-- Overall Starts -->" + (char)13);
        str.Append("<script src=\"http://www.google-analytics.com/urchin.js\" type=\"text/javascript\"></script>" + (char)13);
        str.Append("<script type=\"text/javascript\">" + (char)13);
        str.Append("_uacct = \"UA-1270228-1\";" + (char)13);
        str.Append("urchinTracker();" + (char)13);
        str.Append("</script>" + (char)13);
        //str.Append("<script type=\"text/javascript\" language=\"javascript\">" + (char)13);
        //str.Append("var sc_project=2405578;" + (char)13);
        //str.Append("var sc_invisible=0;" + (char)13);
        //str.Append("var sc_partition=22;" + (char)13);
        //str.Append("var sc_security=\"1f4c0eaa\";" + (char)13);
        //str.Append("var sc_remove_link=1;" + (char)13);
        //str.Append("</script>" + (char)13);
        //str.Append("<script type=\"text/javascript\" language=\"javascript\" src=\"http://www.statcounter.com/counter/counter.js\"></script><noscript><img  src=\"http://c23.statcounter.com/counter.php?sc_project=2405578&java=0&security=1f4c0eaa&invisible=0\" alt=\"website hit counter\" border=\"0\"> </noscript>" + (char)13);
        str.Append("<!-- Overall Ends -->" + (char)13);

        return Convert.ToString(str);
    }
}