using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for orderDetail
/// </summary>
public class orderDetail
{
    Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["Schema"].ToString());
    string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
    string strSql = "";
    string strError = "";
    public orderDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string printOrderDetailHtml(string strOrderNo)
    {
        string strOutPut = "";
        StringBuilder sbFormOutput = new StringBuilder();
        try
        {
            DataTable dtUserDetails = new DataTable();
            strError = "";
            if (getUserDetails(strOrderNo, ref dtUserDetails, ref strError))
            {

                if (dtUserDetails.Rows.Count > 0)
                {
                    foreach (DataRow drUserDetail in dtUserDetails.Rows)
                    {
                        sbFormOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#ffffff\"> ");
                        sbFormOutput.Append("<tr class=\"style8\"><td align=\"center\" valign=\"middle\" class=\"style8\" style=\"height:20px\">Thank you for placing your order with us.</td></tr>");
                        sbFormOutput.Append("<tr>");
                        sbFormOutput.Append("<td  align=\"center\" valign=\"top\" class=\"style4\">");
                        sbFormOutput.Append("<div align=\"justify\">Dear " + drUserDetail["Billing_Name"].ToString() + ",<br/>&nbsp;&nbsp;&nbsp;Thank you for choosing to complete the payment. Kindly review the information below and click the Make Payment button below. Upon successful completion of the order, you will receive an email with order details.");
                        sbFormOutput.Append("</div>");
                        sbFormOutput.Append("</td>");
                        sbFormOutput.Append("</tr>");
                        sbFormOutput.Append("<tr class=\"clear10\">");
                        sbFormOutput.Append("<td class=\"clear10\">");
                        sbFormOutput.Append("&nbsp;");
                        sbFormOutput.Append("</td>");
                        sbFormOutput.Append("</tr>");
                        sbFormOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                        sbFormOutput.Append("<tr>");
                        sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                        sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");

                        StringBuilder strShipBillOutput = new StringBuilder();
                        strShipBillOutput.Append("<table id=\"tabShipBill\" width=\"100%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"> ");
                        strShipBillOutput.Append("<tr>");
                        strShipBillOutput.Append("<td colspan=\"3\" align=\"center\" valign=\"middle\" bgcolor=\"#B20000\" class=\"style3\" style=\"height: 20px\">");
                        strShipBillOutput.Append("<b>Order No: " + strOrderNo + "</b>");
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
                        if (Convert.ToString(drUserDetail["Shipping_Address2"].ToString()) != "")
                        {
                            strShipBillOutput.Append("" + drUserDetail["Shipping_Name"].ToString() + " <br/>" + drUserDetail["Shipping_Address1"].ToString() + "<br/>" + drUserDetail["Shipping_Address2"].ToString() + "<br/>" + drUserDetail["Shipping_CityName"].ToString() + " - " + drUserDetail["Shipping_PinCode"].ToString() + "<br/>" + drUserDetail["Shipping_StateName"].ToString() + "<br/>" + drUserDetail["Shipping_CountryName"].ToString() + ".");
                        }
                        else
                        {
                            strShipBillOutput.Append("" + drUserDetail["Shipping_Name"].ToString() + " <br/>" + drUserDetail["Shipping_Address1"].ToString() + "<br/>" + drUserDetail["Shipping_CityName"].ToString() + " - " + drUserDetail["Shipping_PinCode"].ToString() + "<br/>" + drUserDetail["Shipping_StateName"].ToString() + "<br/>" + drUserDetail["Shipping_CountryName"].ToString() + ".");
                        }
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("</tr>");
                        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                        strShipBillOutput.Append("<b>Phone :</b>");
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                        strShipBillOutput.Append(drUserDetail["Shipping_PhNo"].ToString());
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("</tr>");
                        if (Convert.ToString(drUserDetail["Shipping_Mobile"].ToString()) != "")
                        {
                            strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                            strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                            strShipBillOutput.Append("<b>Mobile :</b>");
                            strShipBillOutput.Append("</td>");
                            strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                            strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                            strShipBillOutput.Append(drUserDetail["Shipping_Mobile"].ToString());
                            strShipBillOutput.Append("</td>");
                            strShipBillOutput.Append("</tr>");
                        }
                        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                        strShipBillOutput.Append("<b>Email :</b>");
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                        strShipBillOutput.Append(drUserDetail["Shipping_Email"].ToString());
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("</tr>");
                        strShipBillOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>	");
                        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                        strShipBillOutput.Append("<b> Delivery Date:</b>");
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                        strShipBillOutput.Append(Convert.ToDateTime(drUserDetail["DOD"]).ToString("dd MMM yyyy"));
                        //strShipBillOutput.Append(drUserDetail["DOD"].ToString());
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("</tr>");
                        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                        strShipBillOutput.Append("<b>Message:</b>");
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                        strShipBillOutput.Append(drUserDetail["Shipping_Msg"].ToString());
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
                        if (Convert.ToString(drUserDetail["Billing_Address2"].ToString()) != "")
                        {
                            strShipBillOutput.Append(drUserDetail["Billing_Name"].ToString() + "<br/>" + drUserDetail["Billing_Address1"].ToString() + "<br/>" + drUserDetail["Billing_Address2"].ToString() + "<br/>" + drUserDetail["Billing_City"].ToString() + " - " + drUserDetail["Billing_PinCode"].ToString() + "<br/>" + drUserDetail["Billing_StateName"].ToString() + "<br/>" + drUserDetail["Billing_CountryName"].ToString() + ".");
                        }
                        else
                        {
                            strShipBillOutput.Append(drUserDetail["Billing_Name"].ToString() + "<br/>" + drUserDetail["Billing_Address1"].ToString() + "<br/>" + drUserDetail["Billing_City"].ToString() + " - " + drUserDetail["Billing_PinCode"].ToString() + "<br/>" + drUserDetail["Billing_StateName"].ToString() + "<br/>" + drUserDetail["Billing_CountryName"].ToString() + ".");
                        }
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("</tr>");
                        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                        strShipBillOutput.Append("<b>Phone :</b>");
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                        strShipBillOutput.Append(drUserDetail["Billing_PhNo"].ToString());
                        strShipBillOutput.Append("</td>");
                        if (Convert.ToString(drUserDetail["Billing_Mobile"].ToString()) != "")
                        {
                            strShipBillOutput.Append("</tr>");
                            strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                            strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                            strShipBillOutput.Append("<b>Mobile :</b>");
                            strShipBillOutput.Append("</td>");
                            strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                            strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                            strShipBillOutput.Append(drUserDetail["Billing_Mobile"].ToString());
                            strShipBillOutput.Append("</td>");
                            strShipBillOutput.Append("</tr>");
                        }
                        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\">");
                        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\">");
                        strShipBillOutput.Append("<b>Email :</b>");
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                        strShipBillOutput.Append(drUserDetail["Billing_Email"].ToString());
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("</tr>");
                        strShipBillOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
                        strShipBillOutput.Append("<tr bgcolor=\"#ffffff\"> ");
                        strShipBillOutput.Append("<td width=\"25%\" class=\"style4\" align=\"right\" valign=\"middle\"> <b>Spl. Instruction :</b> </td> ");
                        strShipBillOutput.Append("<td width=\"1%\" class=\"style4\"></td>");
                        strShipBillOutput.Append("<td width=\"74%\" class=\"style4\" align=\"left\" valign=\"top\">");
                        strShipBillOutput.Append(drUserDetail["Billing_Instructions"].ToString());
                        strShipBillOutput.Append("</td> ");
                        strShipBillOutput.Append("</tr>");
                        strShipBillOutput.Append("</table>");
                        strShipBillOutput.Append("<!--- The shipping details table Starts--->");
                        strShipBillOutput.Append("</td>");
                        strShipBillOutput.Append("</tr>");
                        strShipBillOutput.Append("</table>");
                        sbFormOutput.Append(strShipBillOutput.ToString());
                        sbFormOutput.Append("<!--- The main shipping / billing details table Starts--->");
                        sbFormOutput.Append("</td>");
                        sbFormOutput.Append("</tr>");
                        //sbFormOutput.Append("<tr>");
                        //sbFormOutput.Append("<td valign=\"top\" align=\"left\">");
                        //sbFormOutput.Append("<!--- The main Cart Table Starts--->");
                        //sbFormOutput.Append(printCartHtml(dtCart));
                        //sbFormOutput.Append("<!--- The main Cart Table Ends--->");
                        //sbFormOutput.Append("</td>");
                        //sbFormOutput.Append("</tr>");
                        sbFormOutput.Append("</table>");
                        strOutPut = sbFormOutput.ToString();
                    }
                }
                else
                {
                    strOutPut = "No details against this order number.";
                }
            }
            else
            {
                strOutPut = strError;
            }

        }
        catch (SqlException ex)
        {
            strOutPut = ex.Message;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return strOutPut;
    }
    public bool getUserDetails(string strOrderNo, ref Hashtable htUserDetail, ref string strUserDetailError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Name], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Address1], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Address2], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_PinCode], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_PhNo], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Mobile], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Email], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_City], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_State] AS [Billing_StateName], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Country] AS [Billing_CountryName], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Name], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address1], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address2], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PinCode], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PhNo], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Mobile], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Email], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId], " +
                        "[" + strSchema + "].[City_Server].[City_Name] AS [Shipping_CityName], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId], " +
                        "[" + strSchema + "].[State_Server].[State_Name] AS [Shipping_StateName], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId], " +
                        "[" + strSchema + "].[Country_Server].[Country_Name] AS [Shipping_CountryName], " +
                        "CONVERT(DECIMAL(10, 2), [" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT]) AS [OrderTotal], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[Tax_Percentage] AS [flagDiscount], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[DOD], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[MWG] AS [Shipping_Msg], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[SInstruction] AS [Billing_Instructions]" +
                    "FROM " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[BillingDetails_Bothway] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[BillingDetails_Bothway].[SBillNo]) " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[ShippingDetails_Bothway] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
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
                    "INNER JOIN " +
                        "[" + strSchema + "].[POS_BothWay] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[POS_Id]=[" + strSchema + "].[POS_BothWay].[POS_Id])" +
                    "WHERE " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNo + "')";
            SqlDataAdapter daUserDetail = new SqlDataAdapter(strSql, conn);
            DataTable dtUserDetail = new DataTable();
            daUserDetail.Fill(dtUserDetail);
            if (dtUserDetail.Rows.Count > 0)
            {
                blFlag = true;
                if (!htUserDetail.Contains("ordNo"))
                {
                    htUserDetail.Add("ordNo", strOrderNo);
                }
                else
                {
                    htUserDetail.Remove("ordNo");
                    htUserDetail.Add("ordNo", strOrderNo);
                }
                string strColumnName = "";
                string strColumnValue = "";
                foreach (DataRow drUserDetail in dtUserDetail.Rows)
                {
                    foreach (DataColumn dcUserDetail in dtUserDetail.Columns)
                    {
                        strColumnName = dcUserDetail.ColumnName.ToString();
                        strColumnValue = drUserDetail[dcUserDetail.ColumnName.ToString()].ToString();
                        htUserDetail.Add(strColumnName, strColumnValue);
                    }
                }

                if (htUserDetail.Contains("Billing_Name"))
                {
                    string[] strBillName = Convert.ToString(htUserDetail["Billing_Name"].ToString()).Split(new char[] { ' ' });
                    if (!htUserDetail.Contains("Billing_FName"))
                    {
                        htUserDetail.Add("Billing_FName", strBillName[0].ToString().Trim());
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_FName");
                        htUserDetail.Add("Billing_FName", strBillName[0].ToString().Trim());
                    }
                    if (!htUserDetail.Contains("Billing_LName"))
                    {
                        htUserDetail.Add("Billing_LName", strBillName[1].ToString().Trim());
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_LName");
                        htUserDetail.Add("Billing_LName", strBillName[1].ToString().Trim());
                    }
                }
                else
                {
                    if (!htUserDetail.Contains("Billing_FName"))
                    {
                        htUserDetail.Add("Billing_FName", "");
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_FName");
                        htUserDetail.Add("Billing_FName", "");
                    }
                    if (!htUserDetail.Contains("Billing_LName"))
                    {
                        htUserDetail.Add("Billing_LName", "");
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_LName");
                        htUserDetail.Add("Billing_LName", "");
                    }
                }

                if (htUserDetail.Contains("Shipping_Name"))
                {
                    string[] strShipName = Convert.ToString(htUserDetail["Shipping_Name"].ToString()).Split(new char[] { ' ' });
                    if (!htUserDetail.Contains("Shipping_FName"))
                    {
                        htUserDetail.Add("Shipping_FName", strShipName[0].ToString().Trim());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_FName");
                        htUserDetail.Add("Shipping_FName", strShipName[0].ToString().Trim());
                    }
                    if (!htUserDetail.Contains("Shipping_LName"))
                    {
                        htUserDetail.Add("Shipping_LName", strShipName[1].ToString().Trim());
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_LName");
                        htUserDetail.Add("Shipping_LName", strShipName[1].ToString().Trim());
                    }
                }
                else
                {
                    if (!htUserDetail.Contains("Billing_FName"))
                    {
                        htUserDetail.Add("Billing_FName", "");
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_FName");
                        htUserDetail.Add("Billing_FName", "");
                    }
                    if (!htUserDetail.Contains("Billing_LName"))
                    {
                        htUserDetail.Add("Billing_LName", "");
                    }
                    else
                    {
                        htUserDetail.Remove("Billing_LName");
                        htUserDetail.Add("Billing_LName", "");
                    }
                }

                int intBillStateId = objCommonFunction.getStateIdByName(htUserDetail["Billing_StateName"].ToString());
                if (!htUserDetail.Contains("Billing_StateId"))
                {
                    htUserDetail.Add("Billing_StateId", intBillStateId);
                }
                else
                {
                    htUserDetail.Remove("Billing_StateId");
                    htUserDetail.Add("Billing_StateId", intBillStateId);
                }
                int intBillCountryId = objCommonFunction.getCountryIdByName(htUserDetail["Billing_CountryName"].ToString());
                if (!htUserDetail.Contains("Billing_CountryId"))
                {
                    htUserDetail.Add("Billing_CountryId", intBillCountryId);
                }
                else
                {
                    htUserDetail.Remove("Billing_CountryId");
                    htUserDetail.Add("Billing_CountryId", intBillCountryId);
                }
                double dblOrdTotal = Convert.ToDouble(getOrderTotal(strOrderNo));
                if (!htUserDetail.Contains("ordTotal"))
                {
                    htUserDetail.Add("ordTotal", dblOrdTotal.ToString("######.00"));
                }
                else
                {
                    htUserDetail.Remove("ordTotal");
                    htUserDetail.Add("ordTotal", dblOrdTotal.ToString("######.00"));
                }

                // Newly added to implement other city concept 07.08.08
                if (htUserDetail["Shipping_CityId"].ToString() == "9999")
                {
                    if (!htUserDetail.Contains("Shipping_OtherCityName"))
                    {
                        htUserDetail.Add("Shipping_OtherCityName", getShipOtherCityName(strOrderNo));
                    }
                    else
                    {
                        htUserDetail.Remove("Shipping_OtherCityName");
                        htUserDetail.Add("Shipping_OtherCityName", getShipOtherCityName(strOrderNo));
                    }
                }
                // Newly added to implement other city concept 07.08.08

                int intSiteId = 0;
                string strSiteName = "";
                if (getSiteIdNameFromOrderNumber(strOrderNo, ref intSiteId, ref strSiteName, ref strError))
                {
                    if (!htUserDetail.Contains("siteId"))
                    {
                        htUserDetail.Add("siteId", intSiteId);
                    }
                    else
                    {
                        htUserDetail.Remove("siteId");
                        htUserDetail.Add("siteId", intSiteId);
                    }

                    if (!htUserDetail.Contains("siteName"))
                    {
                        htUserDetail.Add("siteName", strSiteName);
                    }
                    else
                    {
                        htUserDetail.Remove("siteName");
                        htUserDetail.Add("siteName", strSiteName);
                    }
                }
                // Check the discount
                if (htUserDetail.Contains("flagDiscount"))
                {
                    if (Convert.ToString(htUserDetail["flagDiscount"].ToString()) == "1")
                    {
                        HttpContext.Current.Session["flagDiscount"] = 1;
                        // Add the discount
                        string strDiscCode = "";
                        double dblDiscVal = 0.00;
                        double dblDiscGot = 0.00;
                        int intType = 0;
                        double dblLimit = 0.00;
                        if (!getDiscountDetail(strOrderNo, ref strDiscCode, ref dblDiscVal, ref dblDiscGot, ref intType, ref dblLimit, ref strError))
                        {
                            if (!htUserDetail.Contains("disCode"))
                            {
                                htUserDetail.Add("disCode", "-");
                            }
                            else
                            {
                                htUserDetail.Remove("disCode");
                                htUserDetail.Add("disCode", "-");
                            }
                        }
                        else
                        {
                            if (!htUserDetail.Contains("disCode"))
                            {
                                htUserDetail.Add("disCode", strDiscCode);
                            }
                            else
                            {
                                htUserDetail.Remove("disCode");
                                htUserDetail.Add("disCode", strDiscCode);
                            }

                            if (!htUserDetail.Contains("discVal"))
                            {
                                htUserDetail.Add("discVal", dblDiscVal);
                            }
                            else
                            {
                                htUserDetail.Remove("discVal");
                                htUserDetail.Add("discVal", dblDiscVal);
                            }

                            if (!htUserDetail.Contains("discGot"))
                            {
                                htUserDetail.Add("discGot", dblDiscGot);
                            }
                            else
                            {
                                htUserDetail.Remove("discGot");
                                htUserDetail.Add("discGot", dblDiscGot);
                            }

                            if (!htUserDetail.Contains("discLimit"))
                            {
                                htUserDetail.Add("discLimit", dblLimit);
                            }
                            else
                            {
                                htUserDetail.Remove("discLimit");
                                htUserDetail.Add("discLimit", dblLimit);
                            }

                            if (!htUserDetail.Contains("disType"))
                            {
                                htUserDetail.Add("disType", intType);
                            }
                            else
                            {
                                htUserDetail.Remove("disType");
                                htUserDetail.Add("disType", intType);
                            }
                        }
                    }
                    else if (Convert.ToString(htUserDetail["flagDiscount"].ToString()) == "2")
                    {
                        HttpContext.Current.Session["flagDiscount"] = 1;
                        if (!htUserDetail.Contains("disCode"))
                        {
                            htUserDetail.Add("disCode", "-");
                        }
                        else
                        {
                            htUserDetail.Remove("disCode");
                            htUserDetail.Add("disCode", "-");
                        }

                    }
                    else
                    {
                        HttpContext.Current.Session["flagDiscount"] = 0;
                        if (!htUserDetail.Contains("disCode"))
                        {
                            htUserDetail.Add("disCode", "-");
                        }
                        else
                        {
                            htUserDetail.Remove("disCode");
                            htUserDetail.Add("disCode", "-");
                        }

                        if (!htUserDetail.Contains("discVal"))
                        {
                            htUserDetail.Add("discVal", 0.00);
                        }
                        else
                        {
                            htUserDetail.Remove("discVal");
                            htUserDetail.Add("discVal", 0.00);
                        }

                        if (!htUserDetail.Contains("discGot"))
                        {
                            htUserDetail.Add("discGot", 0.00);
                        }
                        else
                        {
                            htUserDetail.Remove("discGot");
                            htUserDetail.Add("discGot", 0.00);
                        }

                        if (!htUserDetail.Contains("discLimit"))
                        {
                            htUserDetail.Add("discLimit", 0.00);
                        }
                        else
                        {
                            htUserDetail.Remove("discLimit");
                            htUserDetail.Add("discLimit", 0.00);
                        }

                        if (!htUserDetail.Contains("disType"))
                        {
                            htUserDetail.Add("disType", 0);
                        }
                        else
                        {
                            htUserDetail.Remove("disType");
                            htUserDetail.Add("disType", 0);
                        }
                    }
                }
                else
                {
                    if (!htUserDetail.Contains("disCode"))
                    {
                        htUserDetail.Add("disCode", "-");
                    }
                    else
                    {
                        htUserDetail.Remove("disCode");
                        htUserDetail.Add("disCode", "-");
                    }
                    if (!htUserDetail.Contains("discVal"))
                    {
                        htUserDetail.Add("discVal", 0.00);
                    }
                    else
                    {
                        htUserDetail.Remove("discVal");
                        htUserDetail.Add("discVal", 0.00);
                    }

                    if (!htUserDetail.Contains("discGot"))
                    {
                        htUserDetail.Add("discGot", 0.00);
                    }
                    else
                    {
                        htUserDetail.Remove("discGot");
                        htUserDetail.Add("discGot", 0.00);
                    }

                    if (!htUserDetail.Contains("discLimit"))
                    {
                        htUserDetail.Add("discLimit", 0.00);
                    }
                    else
                    {
                        htUserDetail.Remove("discLimit");
                        htUserDetail.Add("discLimit", 0.00);
                    }

                    if (!htUserDetail.Contains("disType"))
                    {
                        htUserDetail.Add("disType", 0);
                    }
                    else
                    {
                        htUserDetail.Remove("disType");
                        htUserDetail.Add("disType", 0);
                    }
                }

            }
            else
            {
                blFlag = false;
                strUserDetailError = "No details found for the order number.";
            }
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strUserDetailError = ex.Message;
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
    public bool getUserDetails(string strOrderNo, ref DataTable dtUserDetail, ref string strUserDetailError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            if (dtUserDetail.Rows.Count > 0)
            {
                dtUserDetail.Rows.Clear();
            }
            strSql = "SELECT " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Name], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Address1], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Address2], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_PinCode], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_PhNo], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Mobile], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Email], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_City], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_State] AS [Billing_StateName], " +
                        "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Country] AS [Billing_CountryName], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Name], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address1], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address2], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PinCode], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PhNo], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Mobile], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Email], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId], " +
                        "[" + strSchema + "].[City_Server].[City_Name] AS [Shipping_CityName], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId], " +
                        "[" + strSchema + "].[State_Server].[State_Name] AS [Shipping_StateName], " +
                        "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId], " +
                        "[" + strSchema + "].[Country_Server].[Country_Name] AS [Shipping_CountryName], " +
                        "CONVERT(DECIMAL(10, 2), [" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT]) AS [OrderTotal], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[Tax_Percentage] AS [flagDiscount], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[DOD], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[MWG] AS [Shipping_Msg], " +
                        "[" + strSchema + "].[SalesMaster_BothWay].[SInstruction] AS [Billing_Instructions]" +
                    "FROM " +
                        "[" + strSchema + "].[SalesMaster_BothWay] " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[BillingDetails_Bothway] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[BillingDetails_Bothway].[SBillNo]) " +
                    "INNER JOIN " +
                        "[" + strSchema + "].[ShippingDetails_Bothway] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]=[" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]) " +
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
                    "INNER JOIN " +
                        "[" + strSchema + "].[POS_BothWay] " +
                    "ON " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[POS_Id]=[" + strSchema + "].[POS_BothWay].[POS_Id])" +
                    "WHERE " +
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNo + "')";
            SqlDataAdapter daUserDetail = new SqlDataAdapter(strSql, conn);
            daUserDetail.Fill(dtUserDetail);
            if (dtUserDetail.Rows.Count > 0)
            {
                blFlag = true;
                dtUserDetail.Columns.Add(new DataColumn("siteId", typeof(int)));
                dtUserDetail.Columns.Add(new DataColumn("siteName", typeof(string)));

                dtUserDetail.Columns.Add(new DataColumn("Billing_FName", typeof(string)));
                dtUserDetail.Columns.Add(new DataColumn("Billing_LName", typeof(string)));

                dtUserDetail.Columns.Add(new DataColumn("Shipping_FName", typeof(string)));
                dtUserDetail.Columns.Add(new DataColumn("Shipping_LName", typeof(string)));

                dtUserDetail.Columns.Add(new DataColumn("Billing_StateId", typeof(string)));
                dtUserDetail.Columns.Add(new DataColumn("Billing_CountryId", typeof(string)));
                dtUserDetail.Columns.Add(new DataColumn("Shipping_OtherCityName", typeof(string)));

                dtUserDetail.Columns.Add(new DataColumn("ordNo", typeof(string)));

                dtUserDetail.Columns.Add(new DataColumn("disCode", typeof(string)));
                dtUserDetail.Columns.Add(new DataColumn("discVal", typeof(Decimal)));
                dtUserDetail.Columns.Add(new DataColumn("discGot", typeof(Decimal)));
                dtUserDetail.Columns.Add(new DataColumn("discLimit", typeof(Decimal)));
                dtUserDetail.Columns.Add(new DataColumn("disType", typeof(Decimal)));

                dtUserDetail.Columns.Add(new DataColumn("ordTotal", typeof(Decimal)));

                int intSiteId = 0;
                string strSiteName = "";
                if (!getSiteIdNameFromOrderNumber(strOrderNo, ref intSiteId, ref strSiteName, ref strError))
                {
                    intSiteId = 0;
                    strSiteName = "";
                }
                double dblOrdTotal = Convert.ToDouble(getOrderTotal(strOrderNo));
                for (int i = 0; i < dtUserDetail.Rows.Count; i++)
                {
                    dtUserDetail.Rows[i]["siteId"] = intSiteId;
                    dtUserDetail.Rows[i]["siteName"] = strSiteName;
                    dtUserDetail.Rows[i]["Billing_StateId"] = objCommonFunction.getStateIdByName(dtUserDetail.Rows[i]["Billing_StateName"].ToString());
                    dtUserDetail.Rows[i]["Billing_CountryId"] = objCommonFunction.getCountryIdByName(dtUserDetail.Rows[i]["Billing_CountryName"].ToString());
                    dtUserDetail.Rows[i]["ordNo"] = strOrderNo;
                    dtUserDetail.Rows[i]["ordTotal"] = dblOrdTotal.ToString("######.00");
                    string[] strBillName = Convert.ToString(dtUserDetail.Rows[i]["Billing_Name"].ToString()).Split(new char[] { ' ' });
                    string[] strShipName = Convert.ToString(dtUserDetail.Rows[i]["Shipping_Name"].ToString()).Split(new char[] { ' ' });
                    dtUserDetail.Rows[i]["Billing_FName"] = strBillName[0].ToString().Trim();
                    dtUserDetail.Rows[i]["Shipping_OtherCityName"] = getShipOtherCityName(strOrderNo);
                    dtUserDetail.Rows[i]["Billing_LName"] = strBillName[1].ToString().Trim();
                    dtUserDetail.Rows[i]["Shipping_FName"] = strShipName[0].ToString().Trim();
                    dtUserDetail.Rows[i]["Shipping_LName"] = strShipName[1].ToString().Trim();

                    if (Convert.ToString(dtUserDetail.Rows[i]["flagDiscount"].ToString()) == "1")
                    {
                        HttpContext.Current.Session["flagDiscount"] = 1;
                        // Add the discount
                        string strDiscCode = "";
                        double dblDiscVal = 0.00;
                        double dblDiscGot = 0.00;
                        int intType = 0;
                        double dblLimit = 0.00;

                        //dtUserDetail.Columns.Add(new DataColumn("disCode", typeof(string)));
                        //dtUserDetail.Columns.Add(new DataColumn("discVal", typeof(Decimal)));
                        //dtUserDetail.Columns.Add(new DataColumn("discGot", typeof(Decimal)));
                        //dtUserDetail.Columns.Add(new DataColumn("discLimit", typeof(Decimal)));
                        //dtUserDetail.Columns.Add(new DataColumn("disType", typeof(Decimal)));

                        if (!getDiscountDetail(strOrderNo, ref strDiscCode, ref dblDiscVal, ref dblDiscGot, ref intType, ref dblLimit, ref strError))
                        {
                            dtUserDetail.Rows[i]["disCode"] = "-";
                            dtUserDetail.Rows[i]["discVal"] = 0.00;
                            dtUserDetail.Rows[i]["discGot"] = 0.00;
                            dtUserDetail.Rows[i]["discLimit"] = 0.00;
                            dtUserDetail.Rows[i]["disType"] = 0;
                        }
                        else
                        {
                            dtUserDetail.Rows[i]["disCode"] = strDiscCode;
                            dtUserDetail.Rows[i]["discVal"] = dblDiscVal;
                            dtUserDetail.Rows[i]["discGot"] = dblDiscGot;
                            dtUserDetail.Rows[i]["discLimit"] = dblLimit;
                            dtUserDetail.Rows[i]["disType"] = intType;
                        }
                    }
                    else
                    {
                        HttpContext.Current.Session["flagDiscount"] = 0;
                        dtUserDetail.Rows[i]["disCode"] = "-";
                        dtUserDetail.Rows[i]["discVal"] = 0.00;
                        dtUserDetail.Rows[i]["discGot"] = 0.00;
                        dtUserDetail.Rows[i]["discLimit"] = 0.00;
                        dtUserDetail.Rows[i]["disType"] = 0;
                    }
                }


            }
            else
            {
                blFlag = false;
                strUserDetailError = "No details found for the order number.";
            }
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strUserDetailError = ex.Message;
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
    /// Method to check if a particular order number has got discount. (True/False)
    /// </summary>
    /// <param name="strOrderNo"></param>
    /// <returns>True / False</returns>
    public bool gotDiscount(string strOrderNo)
    {
        bool blFlag = false;
        strSql = "SELECT " +
                    "[" + strSchema + "].[SalesMaster_BothWay].[Tax_Percentage] " +
                "FROM " +
                    "[" + strSchema + "].[SalesMaster_BothWay] " +
                "WHERE " +
                    "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNo + "');";
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
                    if (drDiscount["Tax_Percentage"].ToString() == "1")
                    {
                        blFlag = true;
                    }
                    else
                    {
                        blFlag = false;
                    }
                }
            }
            else
            {
                blFlag = false;
            }
        }
        catch (SqlException ex)
        {
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
    public double getOrderTotal(string strOrderNo)
    {
        double dblOrderTotal = 0.00;
        strSql = "SELECT " +
                    "[" + strSchema + "].[SalesMaster_BothWay].[Sales_ATOT] " +
                "FROM " +
                    "[" + strSchema + "].[SalesMaster_BothWay] " +
                "WHERE " +
                    "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNo + "');";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdOrdTotal = new SqlCommand(strSql, conn);
            SqlDataReader drOrdTotal = cmdOrdTotal.ExecuteReader(CommandBehavior.CloseConnection);
            if (drOrdTotal.HasRows)
            {
                if (drOrdTotal.Read())
                {
                    dblOrderTotal = Convert.ToDouble(drOrdTotal["Sales_ATOT"]);
                }
            }
            else
            {
                dblOrderTotal = 0.00;
            }
        }
        catch (SqlException ex)
        {
            dblOrderTotal = 0.00;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return dblOrderTotal;
    }
    public int getSbillStatus(string strOrderNo)
    {
        int intSbillStatus = 9999;
        strSql = "SELECT " +
                    "[" + strSchema + "].[SalesMaster_BothWay].[Sbill_Status] " +
                "FROM " +
                    "[" + strSchema + "].[SalesMaster_BothWay] " +
                "WHERE " +
                    "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNo + "');";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdStatus = new SqlCommand(strSql, conn);
            SqlDataReader drStatus = cmdStatus.ExecuteReader(CommandBehavior.CloseConnection);
            if (drStatus.HasRows)
            {
                if (drStatus.Read())
                {
                    intSbillStatus = Convert.ToInt32(drStatus["Sbill_Status"]);
                }
            }
            else
            {
                intSbillStatus = 9999;
            }
        }
        catch (SqlException ex)
        {
            intSbillStatus = 9999;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return intSbillStatus;
    }
    public string getBillingName(string strOrderNo)
    {
        string strBillingName = "";
        strSql = "SELECT " +
                    "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Name] " +
                "FROM " +
                    "[" + strSchema + "].[BillingDetails_Bothway] " +
                "WHERE " +
                    "([" + strSchema + "].[BillingDetails_Bothway].[SBillNo]='" + strOrderNo + "');";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmdBillngName = new SqlCommand(strSql, conn);
            SqlDataReader drBillngName = cmdBillngName.ExecuteReader(CommandBehavior.CloseConnection);
            if (drBillngName.HasRows)
            {
                if (drBillngName.Read())
                {
                    strBillingName = Convert.ToString(drBillngName["Billing_Name"]);
                }
            }
            else
            {
                strBillingName = "";
            }
        }
        catch (SqlException ex)
        {
            strBillingName = "";
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        return strBillingName;
    }
    public bool getPoPg(string strOrderNumber, ref string strPaymentGatewayId, ref string strPaymentGatewayName, ref string strPaymentOptionId, ref string strPaymentOptionName, ref string strPoPgRank, ref string strPoPgError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //strSql = "SELECT " +
            //            "[" + strSchema + "].[Order_Pg_Details].[ID], " +
            //            "[" + strSchema + "].[Order_Pg_Details].[gatewayId], " +
            //            "[" + strSchema + "].[Payment_Gateway_Master].[Name] AS [GatewayName], " +
            //            "[" + strSchema + "].[Order_Pg_Details].[optionId], " +
            //            "[" + strSchema + "].[Payment_Option_Master].[Name] as [OptionName]" +

            //        "FROM " +
            //            "[" + strSchema + "].[Order_Pg_Details] " +
            //        "INNER JOIN " +
            //            "[" + strSchema + "].[Payment_Gateway_Master] " +
            //        "ON " +
            //            "([" + strSchema + "].[Order_Pg_Details].[gatewayId]=[" + strSchema + "].[Payment_Gateway_Master].[ID]) " +
            //        "INNER JOIN " +
            //            "[" + strSchema + "].[Payment_Option_Master] " +
            //        "ON " +
            //            "([" + strSchema + "].[Order_Pg_Details].[optionId]=[" + strSchema + "].[Payment_Option_Master].[ID])" +
            //        "WHERE " +
            //            "([" + strSchema + "].[Order_Pg_Details].[SBillNo]='" + strOrderNumber + "')";

            //strSql = "SELECT " +
            //            " top(1)[" + strSchema + "].[Order_Pg_Details].[ID], " +
            //            "[" + strSchema + "].[Order_Pg_Details].[gatewayId], " +
            //            "[" + strSchema + "].[Payment_Gateway_Master].[Name] AS [GatewayName], " +
            //            "[" + strSchema + "].[Order_Pg_Details].[optionId], " +
            //            "[" + strSchema + "].[Payment_Option_Master].[Name] as [OptionName]," +
            //             "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank] " +
            //        "FROM " +
            //            "[" + strSchema + "].[Order_Pg_Details] " +
            //        "INNER JOIN " +
            //            "[" + strSchema + "].[Payment_Gateway_Master] " +
            //        "ON " +
            //            "([" + strSchema + "].[Order_Pg_Details].[gatewayId]=[" + strSchema + "].[Payment_Gateway_Master].[ID]) " +
            //        "INNER JOIN " +
            //            "[" + strSchema + "].[Payment_Option_Master] " +
            //        "ON " +
            //            "([" + strSchema + "].[Order_Pg_Details].[optionId]=[" + strSchema + "].[Payment_Option_Master].[ID])" +
            //         " INNER JOIN " + strSchema + ".Payment_Option_Gateway_Relation ON (" + strSchema + ".Order_Pg_Details.gatewayId = " + strSchema + ".Payment_Option_Gateway_Relation.PgId)" +
            //         //"  INNER JOIN rgcards_gti24x7.Payment_Option_Gateway_Relation ON (rgcards_gti24x7.Order_Pg_Details.optionId = rgcards_gti24x7.Payment_Option_Gateway_Relation.PoId) "+

            //         "WHERE " +
            //            "([" + strSchema + "].[Order_Pg_Details].[SBillNo]='" + strOrderNumber + "')";


            strSql = " SELECT " +
  " rgcards_gti24x7.Order_Pg_Details.ID," +
  " rgcards_gti24x7.Order_Pg_Details.gatewayId," +
  " rgcards_gti24x7.Payment_Gateway_Master.Name AS GatewayName," +
  " rgcards_gti24x7.Order_Pg_Details.optionId," +
  " rgcards_gti24x7.Payment_Option_Master.Name AS OptionName," +
  " rgcards_gti24x7.Payment_Option_Gateway_Relation.Rank" +
" FROM" +
  " rgcards_gti24x7.Order_Pg_Details" +
  " INNER JOIN rgcards_gti24x7.Payment_Gateway_Master ON (rgcards_gti24x7.Order_Pg_Details.gatewayId = rgcards_gti24x7.Payment_Gateway_Master.ID)" +
  " INNER JOIN rgcards_gti24x7.Payment_Option_Master ON (rgcards_gti24x7.Order_Pg_Details.optionId = rgcards_gti24x7.Payment_Option_Master.ID)" +
  " INNER JOIN rgcards_gti24x7.Payment_Option_Gateway_Relation ON (rgcards_gti24x7.Order_Pg_Details.optionId = rgcards_gti24x7.Payment_Option_Gateway_Relation.PoId)" +
  " AND (rgcards_gti24x7.Order_Pg_Details.gatewayId = rgcards_gti24x7.Payment_Option_Gateway_Relation.PgId)" +
" WHERE" +
  " (rgcards_gti24x7.Order_Pg_Details.SBillNo ='" + strOrderNumber + "')";



            SqlCommand cmdPoPg = new SqlCommand(strSql, conn);
            SqlDataReader drPoPg = cmdPoPg.ExecuteReader(CommandBehavior.CloseConnection);
            if (drPoPg.HasRows)
            {
                blFlag = true;
                if (drPoPg.Read())
                {
                    strPaymentGatewayId = drPoPg["gatewayId"].ToString();
                    strPaymentGatewayName = drPoPg["GatewayName"].ToString();
                    strPaymentOptionId = drPoPg["optionId"].ToString();
                    strPaymentOptionName = drPoPg["OptionName"].ToString();
                    strPoPgRank = drPoPg["Rank"].ToString();

                }
            }
            else
            {
                strPoPgError = "PoPg not found for this order number.";
                blFlag = false;
            }
            drPoPg.Dispose();
        }
        catch (SqlException ex)
        {
            strPoPgError = ex.Message;
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

    public bool CheckMaxGateway(string strPoPgRank, string strPoId, string strPgId, ref string strPoPgError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //strSql = "select max(Rank)as max  from rgcards_gti24x7.Payment_Option_Gateway_Relation where PoId=" + strPoId + "   and pgid =" + strPgId + " and Record_Status=1";
            strSql = "select max(Rank)as max  from rgcards_gti24x7.Payment_Option_Gateway_Relation where PoId=" + strPoId + "  and Record_Status=1";

            SqlCommand cmdPoPg = new SqlCommand(strSql, conn);
            SqlDataReader drPoPg = cmdPoPg.ExecuteReader(CommandBehavior.CloseConnection);
            if (drPoPg.HasRows)
            {
                //blFlag = true;
                if (drPoPg.Read())
                {
                    if (strPoPgRank == drPoPg["max"].ToString())
                    {
                        blFlag = true;
                    }

                }
            }
            else
            {

                blFlag = false;
            }
            drPoPg.Dispose();
        }
        catch (SqlException ex)
        {
            strPoPgError = ex.Message;
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


    public bool getSiteIdNameFromOrderNumber(string strOrderNumber, ref int intSiteId, ref string strSiteName, ref string strSiteError)
    {
        bool blFlag = false;
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
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
                        "([" + strSchema + "].[SalesMaster_BothWay].[SBillNo]='" + strOrderNumber.Trim() + "')";
            SqlCommand cmdSite = new SqlCommand(strSql, conn);
            SqlDataReader drSite = cmdSite.ExecuteReader(CommandBehavior.CloseConnection);
            if (drSite.HasRows)
            {
                blFlag = true;
                if (drSite.Read())
                {
                    intSiteId = Convert.ToInt32(drSite["POS_Id"]);
                    strSiteName = drSite["POS_OName"].ToString();
                }
            }
            else
            {
                intSiteId = 0;
                strSiteName = "";
                blFlag = false;
                strSiteError = "No data found against this order number.";
            }
            drSite.Dispose();
        }
        catch (SqlException ex)
        {
            intSiteId = 0;
            strSiteName = "";
            blFlag = false;
            strSiteError = ex.Message;
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
    public bool getDiscountDetail(string strOrderNo)
    {
        bool blFlag = false;
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
                blFlag = true;
                if (drDiscount.Read())
                {
                    HttpContext.Current.Session["discLimit"] = Convert.ToDouble(drDiscount["Limit"]);
                    HttpContext.Current.Session["flagDiscount"] = 1;
                }
            }
            else
            {
                blFlag = false;
                HttpContext.Current.Session["discLimit"] = null;
                HttpContext.Current.Session["flagDiscount"] = null;
            }
        }
        catch (SqlException ex)
        {
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
    public bool getDiscountDetail(string strOrderNo, ref string strDiscCode, ref double dblDiscValue, ref double dblDiscGot, ref int intType, ref double dblLimit, ref string strDiscError)
    {
        bool blFlag = false;
        strSql = "SELECT " +
                    "[" + strSchema + "].[Discount_Code_Master].[Code], " +
                    "[" + strSchema + "].[Discount_Code_Master].[Value] AS [DiscountGot], " +
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
                blFlag = true;
                if (drDiscount.Read())
                {
                    HttpContext.Current.Session["discLimit"] = Convert.ToDouble(drDiscount["Limit"]);
                    HttpContext.Current.Session["flagDiscount"] = 1;
                    strDiscCode = Convert.ToString(drDiscount["Code"].ToString());
                    dblDiscGot = Convert.ToDouble(drDiscount["DiscountGot"]);
                    dblDiscValue = Convert.ToDouble(drDiscount["TypeValue"]);
                    intType = Convert.ToInt32(drDiscount["Type"]);
                    dblLimit = Convert.ToDouble(drDiscount["Limit"]);
                }
                else
                {
                    blFlag = false;
                    HttpContext.Current.Session["discLimit"] = null;
                    HttpContext.Current.Session["flagDiscount"] = 0;
                }
            }
            else
            {
                blFlag = false;
                strDiscError = "No data found.";
                HttpContext.Current.Session["discLimit"] = null;
                HttpContext.Current.Session["flagDiscount"] = 0;
            }
        }
        catch (SqlException ex)
        {
            blFlag = false;
            strDiscError = ex.Message;
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
    public bool updateShippingBillingDetail(Hashtable htUserDetail, ref string strShipBillError)
    {
        bool blFlag = false;
        if (htUserDetail.Count > 0)
        {
            string strOrderNo = Convert.ToString(htUserDetail["ordNo"].ToString());
            if (strOrderNo != "")
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    SqlTransaction tranShipBill = conn.BeginTransaction();
                    try
                    {
                        // Update the order date in the sales master table
                        strSql = "UPDATE " +
                            "[" + strSchema + "].[SalesMaster_BothWay]" +
                        "SET " +
                            "[" + strSchema + "].[SalesMaster_BothWay].[DOD]='" + Convert.ToDateTime(htUserDetail["DOD"]) + "', " +
                            "[" + strSchema + "].[SalesMaster_BothWay].[Sbill_DOS]=DATEADD(MINUTE,30,DATEADD(HOUR,10,GETDATE())) " +
                        "WHERE " +
                            "([" + strSchema + "].[SalesMaster_BothWay].[SbillNo]='" + strOrderNo + "')";
                        SqlCommand cmdSbillDOS = new SqlCommand(strSql, conn, tranShipBill);
                        cmdSbillDOS.ExecuteNonQuery();

                        // Update the Billing Details
                        strSql = "UPDATE " +
                                        "[" + strSchema + "].[BillingDetails_Bothway] " +
                                  "SET " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Name]='" + htUserDetail["Billing_Name"].ToString() + "', " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Address1]='" + htUserDetail["Billing_Address1"].ToString() + "', " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Address2]='" + htUserDetail["Billing_Address2"].ToString() + "', " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_PinCode]='" + htUserDetail["Billing_PinCode"].ToString() + "', " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_PhNo]='" + htUserDetail["Billing_PhNo"].ToString() + "', " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Mobile]='" + htUserDetail["Billing_Mobile"].ToString() + "', " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Email]='" + htUserDetail["Billing_Email"].ToString() + "', " +
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_City]='" + htUserDetail["Billing_City"].ToString() + "', " +	        // Name
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_State]='" + htUserDetail["Billing_StateName"].ToString() + "', " +	    // Name
                                         "[" + strSchema + "].[BillingDetails_Bothway].[Billing_Country]='" + htUserDetail["Billing_CountryName"].ToString() + "', " +	// Name
                                         "[" + strSchema + "].[BillingDetails_Bothway].[UpdateDateTime]=GETDATE() " +
                                  "WHERE " +
                                         "([" + strSchema + "].[BillingDetails_Bothway].[SBillNo]='" + strOrderNo + "');";
                        SqlCommand cmdBill = new SqlCommand(strSql, conn, tranShipBill);
                        cmdBill.ExecuteNonQuery();

                        // Update the shipping details
                        strSql = "UPDATE " +
                                    "[" + strSchema + "].[ShippingDetails_Bothway] " +
                                 "SET " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Name]='" + htUserDetail["Shipping_Name"].ToString() + "', " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address1]='" + htUserDetail["Shipping_Address1"].ToString() + "', " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Address2]='" + htUserDetail["Shipping_Address2"].ToString() + "', " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PinCode]='" + htUserDetail["Shipping_PinCode"].ToString() + "', " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_PhNo]='" + htUserDetail["Shipping_PhNo"].ToString() + "', " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Mobile]='" + htUserDetail["Shipping_Mobile"].ToString() + "', " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_Email]='" + htUserDetail["Shipping_Email"].ToString() + "', " +
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CityId]=" + htUserDetail["Shipping_CityId"].ToString() + ", " +	    // Id
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_StateId]=" + htUserDetail["Shipping_StateId"].ToString() + ", " +	    // Id
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[Shipping_CountryId]=" + htUserDetail["Shipping_CountryId"].ToString() + ", " +	// Id
                                     "[" + strSchema + "].[ShippingDetails_Bothway].[UpdateDateTime]=GETDATE() " +
                                 "WHERE " +
                                     "([" + strSchema + "].[ShippingDetails_Bothway].[SBillNo]='" + strOrderNo + "');";
                        SqlCommand cmdShip = new SqlCommand(strSql, conn, tranShipBill);
                        cmdShip.ExecuteNonQuery();
                        tranShipBill.Commit();
                        blFlag = true;
                    }
                    catch (SqlException tranEx)
                    {
                        tranShipBill.Rollback();
                        blFlag = false;
                        strShipBillError = tranEx.Message;
                    }
                }
                catch (SqlException ex)
                {
                    blFlag = false;
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
                blFlag = false;
                strShipBillError = "Order number cannot be null. Please try again.";
            }
        }
        else
        {
            blFlag = false;
            strShipBillError = "No shipping billing detail found. Please try again.";
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
    public void paymentGatewayDownMail(string strOrderNumber, int intPaymentGatewayId)
    {
        string strMailError = "";
        try
        {
            strSql = "SELECT " +
                        "[" + strSchema + "].[paymentGatewayDownCalculation].[SLNO], " +
                        "[" + strSchema + "].[paymentGatewayDownCalculation].[OrderId], " +
                        "[" + strSchema + "].[paymentGatewayDownCalculation].[BillEmail], " +
                        "[" + strSchema + "].[paymentGatewayDownCalculation].[OrderValue], " +
                        "[" + strSchema + "].[paymentGatewayDownCalculation].[pgName] " +
                    "FROM " +
                        "[" + strSchema + "].[paymentGatewayDownCalculation] " +
                    "('" + strOrderNumber + "', " + intPaymentGatewayId + ", 3)";
            DataTable dtPgMail = new DataTable();
            SqlDataAdapter daPgMail = new SqlDataAdapter(strSql, conn);
            daPgMail.Fill(dtPgMail);
            daPgMail.Dispose();
            if (dtPgMail.Rows.Count > 1)
            {
                string strNowOrderid = "";
                string strNowBillEmail = "";
                string strNowOrderValue = "";
                string strNowPgName = "";
                for (int i = 0; i < 1; i++)
                {
                    strNowOrderid = dtPgMail.Rows[i]["OrderId"].ToString();
                    strNowBillEmail = dtPgMail.Rows[i]["BillEmail"].ToString();
                    strNowOrderValue = dtPgMail.Rows[i]["OrderValue"].ToString();
                    strNowPgName = dtPgMail.Rows[i]["pgName"].ToString();
                }
                DataTable dtFinalMail = new DataTable();
                dtFinalMail.Columns.Add("SlNo", typeof(int));
                dtFinalMail.Columns["SlNo"].AutoIncrement = true;
                dtFinalMail.Columns["SlNo"].AutoIncrementSeed = 1;
                dtFinalMail.Columns.Add("OrderId", typeof(string));
                dtFinalMail.Columns.Add("BillEmail", typeof(string));
                dtFinalMail.Columns.Add("OrderValue", typeof(string));
                dtFinalMail.Columns.Add("PgName", typeof(string));
                ArrayList arrExist = new ArrayList();
                for (int i = 1; i < dtPgMail.Rows.Count; i++)
                {
                    string strComingEmail = dtPgMail.Rows[i]["BillEmail"].ToString();
                    bool blExist = false;

                    for (int j = 0; j < arrExist.Count; j++)
                    {
                        if (arrExist[j].ToString().ToUpper() == strComingEmail.ToUpper())
                        {
                            blExist = true;
                        }
                    }
                    if (!blExist)
                    {
                        arrExist.Add(strComingEmail);
                        DataRow drFinalMail = dtFinalMail.NewRow();
                        drFinalMail["OrderId"] = dtPgMail.Rows[i]["OrderId"].ToString();
                        drFinalMail["BillEmail"] = strComingEmail;
                        drFinalMail["OrderValue"] = dtPgMail.Rows[i]["OrderValue"].ToString();
                        drFinalMail["PgName"] = dtPgMail.Rows[i]["pgName"].ToString();
                        dtFinalMail.Rows.Add(drFinalMail);
                    }
                }
                if (dtFinalMail.Rows.Count > 2)
                {
                    StringBuilder strMail = new StringBuilder();
                    strMail.Append("<table style=\"font-family:Verdana, Arial, Helvetica, sans-serif; padding-left:3px; padding-right:3px; padding-top:0px; padding-bottom:0px; border-color:#eaeaea; border-style:solid; border-width:1px; \" align=\"center\" bgcolor=\"#ffffff\" cellpadding=\"0\" cellspacing=\"0\" width=\"650px\"> ");
                    strMail.Append("<tr> ");
                    strMail.Append("<td align=\"center\" colspan=\"4\" valign=\"middle\"><a href=\"http://www.giftstoindia24x7.com\" title=\"giftstoindia24x7.com\"><img src=\"http://www.giftstoindia24x7.com/Pictures/InvoiveHeader.jpg\" alt=\"giftstoindia24x7.com\" border=\"0\" /></a></td></tr>");
                    strMail.Append("<tr style=\"height:5px;\"><td style=\"height:5px;\" align=\"left\" valign=\"middle\" colspan=\"3\"></td></tr>");
                    strMail.Append("<tr align=\"center\" valign=\"middle\">");
                    strMail.Append("<td style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size: 15px;line-height: 130%; text-align:justify; color:#ff0000; \" colspan=\"4\"><div align=\"center\"><strong>" + strNowPgName + " seems to be down.</strong></div></td></tr>");
                    strMail.Append("<tr style=\"height:1px; background-color:#aeaeae;\"><td style=\"height:1px;\" align=\"left\" valign=\"middle\" colspan=\"3\"></td></tr>");
                    strMail.Append("<tr style=\"height:20px;\" >");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:1%; \" >&nbsp;</td>");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:92%;\" ><strong>The following order has triggered this mail :</strong></td>");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:1%; \" >&nbsp;</td>");
                    strMail.Append("</tr>");
                    strMail.Append("<tr style=\"height:20px;\" >");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:1%; \" >&nbsp;</td>");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:92%;\" ><strong>Order no. </strong> " + strNowOrderid + "&nbsp;&nbsp;<strong>Bill Email:</strong> " + strNowBillEmail + "&nbsp;&nbsp;<strong>Order Value:</strong> " + strNowOrderValue + "</td>");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:1%; \" >&nbsp;</td>");
                    strMail.Append("</tr>");
                    strMail.Append("<tr style=\"height:5px;\"><td style=\"height:5px;\" align=\"left\" valign=\"middle\" colspan=\"3\"></td></tr>");
                    strMail.Append("<tr >");
                    strMail.Append("<td align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:1%; \" colspan=\"3\" >");
                    strMail.Append("<!-- Details Start -->");
                    strMail.Append("<table style=\"font-family:Verdana, Arial, Helvetica, sans-serif; padding-left:3px; padding-right:3px; padding-top:0px; padding-bottom:0px; border-color:#eaeaea; border-style:solid; border-width:1px; \" align=\"center\" bgcolor=\"#ffffff\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                    strMail.Append("<tr><td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:25%;\" colspan=\"4\" ><b>The status of the following orders is on wait.</b></td></tr>");
                    strMail.Append("<tr style=\"height:20px; background-color:#eaeaea;\" > ");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:5%; \"><b>Sl.No</b></td> ");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:20%;\" ><b>Order Id</b></td>");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:45%; \" ><b>Bill. Email Id</b></td>");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:30%; \" ><b>Order Value</b></td>");
                    strMail.Append("</tr>");
                    for (int i = 0; i < 3; i++)
                    {
                        strMail.Append("<tr style=\"height:20px;\" > ");
                        strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:5%; \">" + dtFinalMail.Rows[i]["SlNo"].ToString() + "</td> ");
                        strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:20%;\" >" + dtFinalMail.Rows[i]["OrderId"].ToString() + "</td>");
                        strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:45%; \" >" + dtFinalMail.Rows[i]["BillEmail"].ToString() + "</td>");
                        strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:30%; \" >" + dtFinalMail.Rows[i]["OrderValue"].ToString() + "</td>");
                        strMail.Append("</tr>");
                    }
                    strMail.Append("</table>");
                    strMail.Append("<!-- Details End -->");
                    strMail.Append("</td>");
                    strMail.Append("</tr>");
                    strMail.Append("<tr style=\"height:10px;\"><td style=\"height:10px;\" align=\"left\" valign=\"middle\" colspan=\"3\"></td></tr>");
                    strMail.Append("<tr >");
                    strMail.Append("<td height=\"20\" align=\"center\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; width:1%; \" >&nbsp;</td>");
                    strMail.Append("<td align=\"left\" colspan=\"3\" valign=\"middle\" style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px;line-height: 130%; color: #515151\">");
                    strMail.Append("Sales Team<br/>giftstoindia24x7.com<br/>+91.93395.30030");
                    strMail.Append("</td>");
                    strMail.Append("</tr>");
                    strMail.Append("<tr style=\"height:10px;\"><td style=\"height:10px;\"  colspan=\"4\" align=\"left\" valign=\"middle\"></td></tr>");
                    strMail.Append("</table>");
                    string strSubject = strNowPgName + " seems to be Down.";
                    strError = "";
                    //string strMailTo = "biswasamit1978@gmail.com, jalwa_dekho@yahoo.com, matirswarga@gmail.com, igrag2008@gmail.com";
                    string strMailTo = "e2pdown@gmail.com, desaitina@gmail.com, giftstoindia24x7@gmail.com, rgcards@gmail.com, sude80@gmail.com";
                    if (!objCommonFunction.SendMail(strMailTo, "Giftstoindia24x7.com <sales@giftstoindia24x7.com>", "sales@giftstoindia24x7.com", strMail.ToString(), true, strSubject, System.Net.Mail.MailPriority.High, Convert.ToInt32(HttpContext.Current.Application["STMPPort"].ToString()), false, HttpContext.Current.Application["STMPEmailAccountName"].ToString(), HttpContext.Current.Application["STMPEmailAccountPassword"].ToString(), ref strError))
                    {
                        strMailError = strError;
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            strMailError = ex.Message;
        }
    }
    public string getShipOtherCityName(string strOrderNumber)
    {
        string strOutput = "";
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            strSql = "SELECT " +
                    "[" + strSchema + "].[Order_Other_City].[ID], " +
                    "[" + strSchema + "].[Order_Other_City].[City_Name] " +
                "FROM " +
                    "[" + strSchema + "].[Order_Other_City] " +
                "WHERE " +
                    "[" + strSchema + "].[Order_Other_City].[SBillNo]='" + strOrderNumber + "'";
            SqlDataAdapter daOtherCity = new SqlDataAdapter(strSql, conn);
            DataTable dtOtherCity = new DataTable();
            daOtherCity.Fill(dtOtherCity);
            if (dtOtherCity.Rows.Count > 0)
            {
                foreach (DataRow drOtherCity in dtOtherCity.Rows)
                {
                    strOutput = drOtherCity["City_Name"].ToString();
                }
            }
            else
            {
                strOutput = "0";
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
    public bool checkCancelReturn(string strOrderNumber, string strPaymentGatewayId, string strPaymentOptionId, ref int intCounter, ref string strCancelError)
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
            cmdSamePoPg.CommandText = "[" + strSchema + "].[checkCancelReturn]";

            SqlParameter paramOrderNo = cmdSamePoPg.Parameters.Add(new SqlParameter("@strSBillNo", SqlDbType.VarChar));
            paramOrderNo.Direction = ParameterDirection.Input;
            paramOrderNo.Value = strOrderNumber;

            SqlParameter paramPgId = cmdSamePoPg.Parameters.Add(new SqlParameter("@intPgId", SqlDbType.Int));
            paramPgId.Direction = ParameterDirection.Input;
            paramPgId.Value = strPaymentGatewayId;

            SqlParameter paramPoId = cmdSamePoPg.Parameters.Add(new SqlParameter("@intPoId", SqlDbType.Int));
            paramPoId.Direction = ParameterDirection.Input;
            paramPoId.Value = strPaymentOptionId;

            SqlParameter paramCounter = cmdSamePoPg.Parameters.Add(new SqlParameter("@intReturnCounter", SqlDbType.Int));
            paramCounter.Direction = ParameterDirection.Output;
            cmdSamePoPg.ExecuteNonQuery();
            intCounter = Convert.ToInt32(paramCounter.Value);
            blFlag = true;

        }
        catch (SqlException ex)
        {
            blFlag = false;
            strCancelError = ex.Message;
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
    public bool checkSamePoPg(string strOrderNumber, string strPaymentGatewayId, string strPaymentOptionId, ref string strPoPgError)
    {
        bool blFlag = false;
        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            System.Data.SqlClient.SqlCommand cmdSamePoPg = conn.CreateCommand();
            cmdSamePoPg.CommandType = CommandType.StoredProcedure;
            cmdSamePoPg.CommandText = "[" + ConfigurationManager.AppSettings["Schema"].ToString() + "].[checkSamePoPg_os]";

            System.Data.SqlClient.SqlParameter paramOrderNo = cmdSamePoPg.Parameters.Add(new System.Data.SqlClient.SqlParameter("@strSBillNo", SqlDbType.VarChar));
            paramOrderNo.Direction = ParameterDirection.Input;
            paramOrderNo.Value = strOrderNumber;

            System.Data.SqlClient.SqlParameter paramPgId = cmdSamePoPg.Parameters.Add(new System.Data.SqlClient.SqlParameter("@intPgId", SqlDbType.Int));
            paramPgId.Direction = ParameterDirection.Input;
            paramPgId.Value = strPaymentGatewayId;

            System.Data.SqlClient.SqlParameter paramPoId = cmdSamePoPg.Parameters.Add(new System.Data.SqlClient.SqlParameter("@intPoId", SqlDbType.Int));
            paramPoId.Direction = ParameterDirection.Input;
            paramPoId.Value = strPaymentOptionId;

            cmdSamePoPg.ExecuteNonQuery();
            blFlag = true;

        }
        catch (System.Data.SqlClient.SqlException ex)
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
    public void FetchSiteCss(int SiteId, ref string PageCss, ref string HeaderImage, ref string RedirectBaseDomain, ref string SiteName)
    {
        strSql = "SELECT  " + strSchema + ".SiteCss_Server.SiteImage,  " +
                            "" + strSchema + ".SiteCss_Server.SiteCss, " +
                            "'http://'+" + strSchema + ".pos_bothway.pos_oname as pos_oname, " +
                            "" + strSchema + ".SiteCss_Server.sitename FROM  " +
                            "" + strSchema + ".SiteCss_Server " +
                            " INNER JOIN " +
                            " " + strSchema + ".pos_bothway " +
                            " ON " + strSchema + ".SiteCss_Server.pos_id=" + strSchema + ".pos_bothway.pos_id " +
                            " WHERE  (" + strSchema + ".SiteCss_Server.POS_Id = '" + SiteId + "') ";
        SqlDataReader dr = null;
        string ErrorString = "";
        //Admin_Module_Works_Select objSelect = new Admin_Module_Works_Select(ref dr, strSql, strSchema, ref ErrorString);
        Admin_Module_Works_Select objSelect = null;

        if (ErrorString == null)
        {
            if (dr.Read())
            {
                HeaderImage = dr["SiteImage"].ToString();
                PageCss = dr["SiteCss"].ToString();
                RedirectBaseDomain = dr["pos_oname"].ToString();
                SiteName = dr["sitename"].ToString();
            }
            else
            {
                HeaderImage = "";
                PageCss = "";
                RedirectBaseDomain = "";
                SiteName = "";
            }
        }
        else
        {
            HeaderImage = "";
            PageCss = "";
            RedirectBaseDomain = "";
            SiteName = "";
        }
        if (dr != null) dr.Dispose();
    }
    public string SentenceCase(string StringToChange)
    {
        StringToChange = StringToChange.Trim();
        CharEnumerator _charEnu = StringToChange.GetEnumerator();
        StringBuilder _str = new StringBuilder();
        bool blFlag = false;
        int count = 0;
        while (_charEnu.MoveNext())
        {
            if (count == 0)
            {
                if (!blFlag)
                {
                    _str.Append(_charEnu.Current.ToString().ToUpper());
                }
            }
            else
            {
                if (blFlag)
                {
                    _str.Append(_charEnu.Current.ToString().ToUpper());
                    blFlag = false;
                }
                else
                {
                    _str.Append(_charEnu.Current.ToString());
                }
            }
            if (_charEnu.Current == 32)
            {
                blFlag = true;
            }
            count++;
        }
        return _str.ToString();
    }
    public bool GetDiscountDetail_os(string OrderNumber, ref string DiscountCode, ref int DiscountType, ref double DiscountValue, ref double DiscountGot)
    {
        bool flag = false;
        strSql = "SELECT " +
                  "rgcards_gti24x7.OS_Order_Master.DiscountType, " +
                  "rgcards_gti24x7.OS_Order_Master.DiscountCode, " +
                  "rgcards_gti24x7.OS_Order_Details.Price, " +
                  "rgcards_gti24x7.OS_Order_Details.DiscountPercent, " +
                  "rgcards_gti24x7.OS_Order_Details.DiscountPrice, " +
                  "rgcards_gti24x7.OS_Order_Details.Qty, " +
                  "rgcards_gti24x7.OS_Order_Details.Total " +
                "FROM " +
                  "rgcards_gti24x7.OS_Order_Master " +
                  "INNER JOIN rgcards_gti24x7.OS_Order_Details ON (rgcards_gti24x7.OS_Order_Master.OrderId = rgcards_gti24x7.OS_Order_Details.OrderId) " +
                "WHERE " +
                  "(rgcards_gti24x7.OS_Order_Master.InvoiceNo = '" + OrderNumber + "')" +
                "AND " +
                  "(rgcards_gti24x7.OS_Order_Details.InvoiceNo = '" + OrderNumber + "')";
        strError = "";
        System.Data.SqlClient.SqlDataReader dr = null;
        //Admin_Module_Works_Select objSelect = new Admin_Module_Works_Select(ref dr, strSql, strSchema, ref strError);
        Admin_Module_Works_Select objSelect = null;

        if (strError == null)
        {
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    DiscountCode = Convert.ToString(dr["DiscountCode"]);
                    DiscountType = Convert.ToInt32(dr["DiscountType"]);
                    DiscountValue = Convert.ToDouble(dr["DiscountPercent"]);
                    DiscountGot = Convert.ToDouble(dr["DiscountPrice"]);
                    flag = true;
                }
            }
        }
        return flag;
    }

}
