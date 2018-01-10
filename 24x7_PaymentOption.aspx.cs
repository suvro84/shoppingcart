using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class _24x7_PaymentOption : System.Web.UI.Page
{
    SqlConnection conn;
    public string[] strArrSbill;
    public string strcomboid = "";
    public string currid = "";
    public string strsenddata = "";
    public string strsiteid = "";
    public string struserid = "";
    public string strpageheaderimage = "";
    public string strRedirectBaseDomain = "";
    public string strGenSitename = "";
    public string strpageCSS = "";
    string strSql = "", strError = "";
    string strSchema = ConfigurationManager.AppSettings["Schema"].ToString();
    cartFunctions objCart = new cartFunctions();
    string SbillNo = "";

    clsRakhi objRakhiHd = new clsRakhi();
    System.Collections.Hashtable htUserDetail = new Hashtable();
    System.Data.DataTable dtCart = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //conn = new SqlConnection(Application["ConnectionString"].ToString());
        conn = new SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
        dtCart = (DataTable)Session["dtCart"];
        if ((Session["userDetails"] != null))
        {
            htUserDetail = (Hashtable)Session["userDetails"];
        }
        if (Request.QueryString.Get("SbillNo") != null)
        {
            SbillNo = Request.QueryString.Get("SbillNo").ToString();
            strArrSbill = SbillNo.Split(new char[] { ',' });
            HiddenSbill.Value = strArrSbill[0].ToString();
        }

        if ((Request.QueryString.Get("comboid") != null) && (Request.QueryString.Get("comboid") != ""))
        {
            strcomboid = Request.QueryString.Get("comboid").ToString();
            HiddenCombo.Value = strcomboid;
        }
        if (Request.QueryString.Get("currencyid") != null)
        {
            currid = Request.QueryString.Get("currencyid").ToString();
        }
        if (Request.QueryString.Get("senddata") != null)
        {
            strsenddata = Request.QueryString.Get("senddata").ToString();
        }
        if (Request.QueryString["SiteId"] != null)
        {
            strsiteid = Request.QueryString["SiteId"].ToString();
            HttpContext.Current.Session["SiteId"] = strsiteid;
        }
        if (Request.Params["userid"] != null)
        {
            struserid = Request.QueryString["userid"].ToString();
            if (struserid == "")
            {
                //tblAddMOre.Visible = false;
                //tblAddNew.Visible = false;
                ////tblAddMOre.Disabled = true;
                ////tblAddNew.Disabled = true;
                ////ImgSaveContinue.Enabled = false;
                ////ImgSaveMakeNewOrder.Enabled = false;
                tblAddMOre.Visible = false;
                tblAddNew.Visible = false;
                trOption1.Visible = false;
                ptext.Visible = false;
                divPayoption.Attributes.Add("class", "");
            }
        }
        else
        {
            divPayoption.Attributes.Add("class", "rightBg");
        }
        if ((Request.Params["frmCart"] != null) && (Request.Params["frmCartCombo"] != null))
        {
            strcomboid = Request.Params["frmCartCombo"].ToString();
        }
        objRakhiHd.functionforImageHead(Convert.ToInt32(strsiteid), ref strpageCSS, ref strpageheaderimage, ref strRedirectBaseDomain, ref strGenSitename);
        strGenSitename = objRakhiHd.SentenceCase(strGenSitename.Trim());
        if ((strsiteid == "132") || (strsiteid == "154") || (strsiteid == "1"))      // If its giftbhejo, ndtv.giftstoindia24x7 or giftstoindia24x7 dont show the payment message
        {
            topMsg.Visible = false;
        }
        else
        {
            topMsg.Visible = true;
        }
        if (!Page.IsPostBack)
        {

            //=======the condition is for if user click from cart page to show details
            if ((Request.Params["frmCart"] != "") && (Request.Params["frmCart"] != null))
            {
                string strOutPut = "", frmCartCombo = "", frmCartSbill = "";
                if ((Request.Params["frmCartSbill"] != "") && (Request.Params["frmCartSbill"] != null))
                {
                    frmCartSbill = Request.Params["frmCartSbill"].ToString();
                }
                if ((Request.Params["frmCartCombo"] != "") && (Request.Params["frmCartCombo"] != null))
                {
                    frmCartCombo = Request.Params["frmCartCombo"].ToString();
                }
                if (!showPaymentOptions(ref strOutPut))
                {
                    dvPaymentOptList.InnerHtml = strOutPut;
                    dvBtn.Visible = false;
                }
                else
                {
                    dvPaymentOptList.InnerHtml = strOutPut;
                    dvBtn.Visible = true;
                }
                tblAddMOre.Visible = false;
                tblAddNew.Visible = false;
                trOption1.Visible = false;
                ptext.Visible = false;
                orderDetail obj = new orderDetail();
                cartFunctions obj1 = new cartFunctions();
                string strUserDetailError = "", strCartError = "";
                obj.getUserDetails(frmCartSbill, ref htUserDetail, ref strUserDetailError);
                obj1.makeTheCart(Convert.ToInt32(HttpContext.Current.Session["SiteId"]), frmCartSbill, ref dtCart, ref strCartError);

                if (!htUserDetail.Contains("comboid"))
                {
                    htUserDetail.Add("comboid", frmCartCombo);
                    htUserDetail.Remove("ordNo");
                }

                Session["dtCart"] = dtCart;
                Session["userDetails"] = htUserDetail;
            }
            else
            {
                if ((Session["dtCart"] != null) && (Convert.ToInt32(Request.QueryString["SiteId"]) != 0))
                {

                    if (dtCart.Rows.Count > 0)
                    {
                        if (htUserDetail.Count > 0)
                        {
                            string strOutPut = "";
                            if (!showPaymentOptions(ref strOutPut))
                            {
                                dvPaymentOptList.InnerHtml = strOutPut;
                                dvBtn.Visible = false;
                            }
                            else
                            {
                                dvPaymentOptList.InnerHtml = strOutPut;
                                dvBtn.Visible = true;
                            }
                            if (!htUserDetail.Contains("siteId"))
                            {
                                htUserDetail.Add("siteId", strsiteid);
                            }
                            else
                            {
                                htUserDetail.Remove("siteId");
                                htUserDetail.Add("siteId", strsiteid);
                            }
                        }
                        else
                        {
                            //Response.Redirect("cart.aspx");
                            dvPaymentOptList.InnerHtml = objCart.showNoitem(false);
                            ImgSaveContinue.Visible = false;
                            ImgSaveMakeNewOrder.Visible = false;
                        }
                        Session["dtCart"] = dtCart;
                        Session["userDetails"] = htUserDetail;
                    }
                    else
                    {
                        //Response.Redirect("cart.aspx");
                        dvPaymentOptList.InnerHtml = objCart.showNoitem(false);
                        ImgSaveContinue.Visible = false;
                        ImgSaveMakeNewOrder.Visible = false;
                    }
                }
                else
                {
                    //Response.Redirect("Default.aspx");
                    dvPaymentOptList.InnerHtml = objCart.showNoitem(false);
                    ImgSaveContinue.Visible = false;
                    ImgSaveMakeNewOrder.Visible = false;
                }
            }

            //functionforImageHead();
            //=========checking if the page loading from cancel page or from submit page================
            if (Request.Params["cancel"] != null)
            {
                if (Request.Params["cancel"] != "")
                {
                    ////tblAddMOre.Disabled = true;
                    ////tblAddNew.Disabled = true;
                    ////ImgSaveContinue.Enabled = false;
                    ////ImgSaveMakeNewOrder.Enabled = false;
                    tblAddMOre.Visible = false;
                    tblAddNew.Visible = false;
                    trOption1.Visible = false;
                    ptext.Visible = false;
                    //ImgSaveContinue.Visible = false;
                    //ImgSaveMakeNewOrder.Visible = false;
                }
                else
                {
                    tblAddMOre.Visible = true;
                    tblAddNew.Visible = true;
                    trOption1.Visible = true;
                    ptext.Visible = true;
                    ////tblAddMOre.Disabled = false;
                    ////tblAddNew.Disabled = false;
                    ////ImgSaveContinue.Enabled = true;
                    ////ImgSaveMakeNewOrder.Enabled = true;
                }
            }
            //===================end=============================
        }
    }
    public void functionforImageHead()
    {
        // *** Change Requied *** //
        //string connectionstring = "Data Source=SUPRIYO\\MSSQL2005;Initial Catalog=rgcards_webcounter;User ID=sa; Password=tiger; ";
        //SqlConnection conn = new SqlConnection(connectionstring);
        // *** Change Requied *** //
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        string strSQL = " SELECT " +
                        " rgcards_gti24x7.SiteCss_Server.SiteImage, " +
                        " rgcards_gti24x7.SiteCss_Server.SiteCss" +
                        " FROM " +
                        " rgcards_gti24x7.SiteCss_Server " +
                        " WHERE " +
                        " (rgcards_gti24x7.SiteCss_Server.POS_Id = '" + strsiteid + "')";
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.HasRows)
        {
            if (dr.Read())
            {
                strpageheaderimage = dr["SiteImage"].ToString();
                strpageCSS = dr["SiteCss"].ToString();
            }
        }
        dr.Close();
        conn.Close();
    }

    //protected void ImgSaveMakeNewOrder_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //private bool showPaymentOptions(ref string strPaymentOptionHtml)
    //{
    //    bool blFlag = false;
    //    string strOutPut = "";
    //    strSql = "SELECT " +
    //                "[" + strSchema + "].[Payment_Option_Master].[ID], " +
    //                "[" + strSchema + "].[Payment_Option_Master].[Name], " +
    //                "[" + strSchema + "].[Payment_Option_Master].[Rank], " +
    //                "[" + strSchema + "].[Payment_Option_Master].[Description], " +
    //                "[" + strSchema + "].[Payment_Option_Master].[Image] " +
    //            "FROM " +
    //                "[" + strSchema + "].[Payment_Option_Master] " +
    //            "WHERE " +
    //                "([" + strSchema + "].[Payment_Option_Master].[ID] IN " +
    //                "(SELECT DISTINCT " +
    //                    "[" + strSchema + "].[Payment_Option_Gateway_Relation].[PoID] " +
    //                "FROM " +
    //                    "[" + strSchema + "].[Payment_Option_Gateway_Relation] " +
    //                "WHERE " +
    //                    "([" + strSchema + "].[Payment_Option_Gateway_Relation].[PGId] IN " +
    //                    "(SELECT " +
    //                        "[" + strSchema + "].[Payment_Gateway_Master].[Id] " +
    //                    "FROM " +
    //                        "[" + strSchema + "].[Payment_Gateway_Master] " +
    //                    "WHERE " +
    //                        "([" + strSchema + "].[Payment_Gateway_Master].[Record_Status]=1))))) " +
    //            "AND " +
    //                "([" + strSchema + "].[Payment_Option_Master].[Record_Status]=1) " +
    //            "ORDER BY [" + strSchema + "].[Payment_Option_Master].[Rank];";

    //    try
    //    {
    //        if (conn.State != ConnectionState.Open)
    //        {
    //            conn.Open();
    //        }
    //        SqlCommand cmdPo = new SqlCommand(strSql, conn);
    //        SqlDataReader drPo = cmdPo.ExecuteReader(CommandBehavior.CloseConnection);
    //        if (drPo.HasRows)
    //        {
    //            blFlag = true;
    //            int intCount = 1;
    //            StringBuilder strPaymentOption = new StringBuilder();
    //            strPaymentOption.Append("<table id=\"tabPOMain\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tableborder\">");
    //            strPaymentOption.Append("<tr>");
    //            //strPaymentOption.Append("<td class=\"style8\" align=\"center\" valign=\"top\" width=\"100%\">");
    //            strPaymentOption.Append("<td style=\"padding:0 0 20px 0; text-align:left;\" align=\"left\" valign=\"top\" width=\"100%\">");
    //            strPaymentOption.Append("You can make the payment by selecting any of the following methods. We support all major credit cards & transfers through Paypal.");
    //            strPaymentOption.Append("</td>");
    //            strPaymentOption.Append("</tr>");
    //            strPaymentOption.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
    //            strPaymentOption.Append("<tr>");
    //            strPaymentOption.Append("<td align=\"center\" valign=\"top\">");
    //            strPaymentOption.Append("<!-- Payment Options Table Starts -->");
    //            strPaymentOption.Append("<table id=\"tabPOMain\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
    //            strPaymentOption.Append("<!-- Payment Options genaration starts -->");

    //            while (drPo.Read())
    //            {
    //                strPaymentOption.Append("<tr>");
    //                strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"5%\">");
    //                strPaymentOption.Append(intCount + ".");
    //                strPaymentOption.Append("</td>");
    //                strPaymentOption.Append("<td align=\"right\" valign=\"middle\" class=\"style1\" width=\"15%\">");
    //                strPaymentOption.Append("<input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["ID"].ToString() + "\" value=\"" + drPo["ID"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgId(this, 'dvPaymentOptDescription');\">");
    //                strPaymentOption.Append("</td>");
    //                strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style8\" width=\"49%\">");
    //                strPaymentOption.Append(drPo["Name"].ToString());
    //                strPaymentOption.Append("</td>");
    //                strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"30%\">");
    //                strPaymentOption.Append("<img src=\"" + drPo["Image"].ToString() + "\" alt=\"" + drPo["Name"].ToString() + "\" align=\"absmiddle\">");
    //                strPaymentOption.Append("</td>");
    //                strPaymentOption.Append("</tr>");
    //                intCount++;
    //            }
    //            strPaymentOption.Append("<!-- Payment Options genaration Ends -->");
    //            strPaymentOption.Append("</table>");
    //            strPaymentOption.Append("<!-- Payment Options Table Ends -->");
    //            strPaymentOption.Append("</td>");
    //            strPaymentOption.Append("</tr>");
    //            strPaymentOption.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
    //            //strPaymentOption.Append("<tr>");
    //            //strPaymentOption.Append("<td align=\"center\" valign=\"middle\">");
    //            //strPaymentOption.Append("<a href=\"orderDisplay.aspx\" onclick=\"return validateForm('frmPo');\"><img src=\"Pictures/btn_proceed_checkout.gif\" alt=\"Proceed to Checkout\" border=\"0\"/></a>");
    //            //strPaymentOption.Append("</td>");
    //            //strPaymentOption.Append("</tr>");
    //            strPaymentOption.Append("</table>");
    //            strOutPut = strPaymentOption.ToString();
    //        }
    //        else
    //        {
    //            blFlag = false;
    //            strOutPut = "No payment options found.";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        strOutPut = "Error !<br>" + ex.Message;
    //        blFlag = false;
    //    }
    //    finally
    //    {
    //        if (conn.State != ConnectionState.Closed)
    //        {
    //            conn.Close();
    //        }
    //    }
    //    strPaymentOptionHtml = strOutPut;
    //    return blFlag;
    //}
    private bool showPaymentOptions(ref string strPaymentOptionHtml)
    {
        bool blFlag = false;
        string strOutPut = "";
        strSql = "SELECT " +
                    "[" + strSchema + "].[Payment_Option_Master].[ID] AS [PoId], " +
                    "[" + strSchema + "].[Payment_Option_Gateway_Relation].[PgId], " +
                    "[" + strSchema + "].[Payment_Option_Master].[Name], " +
                    "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank], " +
                    "[" + strSchema + "].[Payment_Option_Master].[Description], " +
                    "[" + strSchema + "].[Payment_Option_Master].[Image] " +
                "FROM " +
                    "[" + strSchema + "].[Payment_Option_Master] " +
                "INNER JOIN " +
                    "[" + strSchema + "].[Payment_Option_Gateway_Relation] " +
                "ON " +
                    "([" + strSchema + "].[Payment_Option_Master].[ID]=[" + strSchema + "].[Payment_Option_Gateway_Relation].[PoId]) " +
                "INNER JOIN " +
                    "[" + strSchema + "].[Payment_Gateway_Master] " +
                "ON " +
                    "([" + strSchema + "].[Payment_Option_Gateway_Relation].[PgId]=[" + strSchema + "].[Payment_Gateway_Master].[ID]) " +
                "WHERE " +
                    "([" + strSchema + "].[Payment_Gateway_Master].[Record_Status]=1) " +
                "AND " +
                    "([" + strSchema + "].[Payment_Option_Master].[Record_Status]=1) " +
                "AND " +
                    "([" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank]!=0) " +
                "ORDER BY " +
                    "[rgcards_gti24x7].[Payment_Option_Master].[Rank], " +
                    "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank];";

        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlDataAdapter _daPoPgMain = new SqlDataAdapter(strSql, conn);
            DataTable _dtPoPgMain = new DataTable();
            _daPoPgMain.Fill(_dtPoPgMain);
            DataTable _dtPoPgtoPrint = new DataTable();
            if (_dtPoPgMain.Rows.Count > 0)
            {
                _dtPoPgtoPrint.Columns.Add(new DataColumn("PoId", typeof(int)));
                _dtPoPgtoPrint.Columns.Add(new DataColumn("PgId", typeof(int)));
                _dtPoPgtoPrint.Columns.Add(new DataColumn("Name", typeof(string)));
                _dtPoPgtoPrint.Columns.Add(new DataColumn("Rank", typeof(int)));
                _dtPoPgtoPrint.Columns.Add(new DataColumn("Description", typeof(string)));
                _dtPoPgtoPrint.Columns.Add(new DataColumn("Image", typeof(string)));

                for (int i = 0; i < _dtPoPgMain.Rows.Count; i++)
                {
                    string strPoId = _dtPoPgMain.Rows[i]["PoId"].ToString();
                    bool blExist = false;
                    foreach (DataRow _drExist in _dtPoPgtoPrint.Rows)
                    {
                        if (_drExist["PoId"].ToString() == strPoId)
                        {
                            blExist = true;
                        }
                    }
                    if (!blExist)
                    {
                        DataView _dv = new DataView(_dtPoPgMain);
                        _dv.RowFilter = "[PoId]=" + strPoId + "";
                        DataTable _dtRepeatPo = _dv.ToTable();
                        if (_dtRepeatPo.Rows.Count > 0)
                        {
                            for (int x = 0; x < 1; x++)
                            {
                                DataRow _dr = _dtPoPgtoPrint.NewRow();
                                _dr["PoId"] = strPoId;
                                _dr["PgId"] = _dtRepeatPo.Rows[x]["PgId"].ToString();
                                _dr["Name"] = _dtRepeatPo.Rows[x]["Name"].ToString();
                                _dr["Rank"] = _dtRepeatPo.Rows[x]["Rank"].ToString();
                                _dr["Description"] = _dtRepeatPo.Rows[x]["Description"].ToString();
                                _dr["Image"] = _dtRepeatPo.Rows[x]["Image"].ToString();
                                _dtPoPgtoPrint.Rows.Add(_dr);
                            }
                        }
                    }
                }
            }
            else
            {
                blFlag = false;
                strOutPut = "No payment options found.";
            }
            if (_dtPoPgtoPrint.Rows.Count > 0)
            {
                blFlag = true;
                StringBuilder strPaymentOption = new StringBuilder();
                int intTotalCount = 0;
                int intCount = 0;
                strPaymentOption.Append("<!-- Payment Options genaration starts -->");
                // If userid is coming then show the payment option in single row else in double row
                if (struserid == "")
                {
                    strPaymentOption.Append("<table id=\"tabPOMain\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tableborder\">");
                    strPaymentOption.Append("<tr>");
                    strPaymentOption.Append("<td colspan=\"3\" class=\"style8\" align=\"center\" valign=\"top\" width=\"100%\">");
                    strPaymentOption.Append("You can make the payment by selecting any of the following methods. We support all major credit cards & transfers through Paypal.");
                    strPaymentOption.Append("</td>");
                    strPaymentOption.Append("</tr>");
                    strPaymentOption.Append("<tr ><td colspan=\"3\" height=\"15\"></td></tr>");
                    strPaymentOption.Append("<tr>");
                    foreach (DataRow drPo in _dtPoPgtoPrint.Rows)
                    {

                        if ((intTotalCount + 1) >= _dtPoPgtoPrint.Rows.Count)
                        {
                            if ((intTotalCount % 2) != 0)
                            {
                                strPaymentOption.Append("<td align=\"center\" valign=\"top\" width=\"1%\" style=\"background-image: url(Pictures/hr-h2.jpg); background-repeat:repeat-y;\"></td>");
                                strPaymentOption.Append("<td align=\"center\" valign=\"top\" width=\"49%\">");
                                strPaymentOption.Append("<table id=\"tabPO" + intTotalCount + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
                                strPaymentOption.Append("<tr>");
                                strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"5%\">" + Convert.ToString(intTotalCount + 1) + "." + "</td>");
                                strPaymentOption.Append("<td align=\"right\" valign=\"middle\" class=\"style1\" width=\"5%\"><input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["PoId"].ToString() + "\" value=\"" + drPo["PoId"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgIdNew('" + drPo["PoId"].ToString() + "', '" + drPo["PgId"].ToString() + "');\"></td>");
                                strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style8\" width=\"59%\" style=\"padding:0 5px 0 5px;\">" + drPo["Name"].ToString() + "</td>");
                                strPaymentOption.Append("<td align=\"center\" valign=\"left\" class=\"style1\" width=\"30%\"><img src=\"" + drPo["Image"].ToString() + "\" alt=\"" + drPo["Name"].ToString() + "\" align=\"left\"></td>");
                                strPaymentOption.Append("</tr>");
                                strPaymentOption.Append("</table>");
                                strPaymentOption.Append("</td>");
                                strPaymentOption.Append("</tr>");
                            }
                            else
                            {
                                strPaymentOption.Append("<tr>");
                                strPaymentOption.Append("<td align=\"center\" valign=\"top\" width=\"49%\">");
                                strPaymentOption.Append("<table id=\"tabPO" + intTotalCount + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
                                strPaymentOption.Append("<tr>");
                                strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"5%\">" + Convert.ToString(intTotalCount + 1) + "." + "</td>");
                                strPaymentOption.Append("<td align=\"right\" valign=\"middle\" class=\"style1\" width=\"5%\"><input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["PoId"].ToString() + "\" value=\"" + drPo["PoId"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgIdNew('" + drPo["PoId"].ToString() + "', '" + drPo["PgId"].ToString() + "');\"></td>");
                                strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style8\" width=\"59%\" style=\"padding:0 5px 0 5px;\">" + drPo["Name"].ToString() + "</td>");
                                strPaymentOption.Append("<td align=\"center\" valign=\"left\" class=\"style1\" width=\"30%\"><img src=\"" + drPo["Image"].ToString() + "\" alt=\"" + drPo["Name"].ToString() + "\" align=\"left\"></td>");
                                strPaymentOption.Append("</tr>");
                                strPaymentOption.Append("</table>");
                                strPaymentOption.Append("</td>");
                                strPaymentOption.Append("</tr>");
                            }
                            intCount++;
                        }
                        else if (intCount == 0)
                        {
                            strPaymentOption.Append("<tr>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"top\" width=\"49%\">");
                            strPaymentOption.Append("<table id=\"tabPO" + intTotalCount + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
                            strPaymentOption.Append("<tr>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"5%\">" + Convert.ToString(intTotalCount + 1) + "." + "</td>");
                            strPaymentOption.Append("<td align=\"right\" valign=\"middle\" class=\"style1\" width=\"5%\"><input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["PoId"].ToString() + "\" value=\"" + drPo["PoId"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgIdNew('" + drPo["PoId"].ToString() + "', '" + drPo["PgId"].ToString() + "');\"></td>");
                            strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style8\" width=\"59%\" style=\"padding:0 5px 0 5px;\">" + drPo["Name"].ToString() + "</td>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"left\" class=\"style1\" width=\"30%\"><img src=\"" + drPo["Image"].ToString() + "\" alt=\"" + drPo["Name"].ToString() + "\" align=\"left\"></td>");
                            strPaymentOption.Append("</tr>");
                            strPaymentOption.Append("</table>");
                            strPaymentOption.Append("</td>");

                            intCount++;
                        }
                        else if (((intCount + 1) % 2) == 0)
                        {
                            strPaymentOption.Append("<td align=\"center\" valign=\"top\" width=\"1%\" style=\"background-image: url(Pictures/hr-h2.jpg); background-repeat:repeat-y;\"></td>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"top\" width=\"49%\">");
                            strPaymentOption.Append("<table id=\"tabPO" + intTotalCount + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
                            strPaymentOption.Append("<tr>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"5%\">" + Convert.ToString(intTotalCount + 1) + "." + "</td>");
                            strPaymentOption.Append("<td align=\"right\" valign=\"middle\" class=\"style1\" width=\"5%\"><input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["PoId"].ToString() + "\" value=\"" + drPo["PoId"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgIdNew('" + drPo["PoId"].ToString() + "', '" + drPo["PgId"].ToString() + "');\"></td>");
                            strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style8\" width=\"59%\" style=\"padding:0 5px 0 5px;\">" + drPo["Name"].ToString() + "</td>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"left\" class=\"style1\" width=\"30%\"><img src=\"" + drPo["Image"].ToString() + "\" alt=\"" + drPo["Name"].ToString() + "\" align=\"left\"></td>");
                            strPaymentOption.Append("</tr>");
                            strPaymentOption.Append("</table>");
                            strPaymentOption.Append("</td>");
                            strPaymentOption.Append("</tr>");
                            intCount = 0;
                        }
                        else
                        {
                            strPaymentOption.Append("<td align=\"center\" valign=\"top\" width=\"49%\">");
                            strPaymentOption.Append("<table id=\"tabPO" + intTotalCount + "\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
                            strPaymentOption.Append("<tr>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"5%\">" + Convert.ToString(intTotalCount + 1) + "." + "</td>");
                            strPaymentOption.Append("<td align=\"right\" valign=\"middle\" class=\"style1\" width=\"5%\"><input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["PoId"].ToString() + "\" value=\"" + drPo["PoId"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgIdNew('" + drPo["PoId"].ToString() + "', '" + drPo["PgId"].ToString() + "');\"></td>");
                            strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style8\" width=\"59%\" style=\"padding:0 5px 0 5px;\">" + drPo["Name"].ToString() + "</td>");
                            strPaymentOption.Append("<td align=\"center\" valign=\"left\" class=\"style1\" width=\"30%\"><img src=\"" + drPo["Image"].ToString() + "\" alt=\"" + drPo["Name"].ToString() + "\" align=\"left\"></td>");
                            strPaymentOption.Append("</tr>");
                            strPaymentOption.Append("</table>");
                            strPaymentOption.Append("</td>");
                            intCount++;
                        }
                        intTotalCount++;
                    }
                    strPaymentOption.Append("<!-- Payment Options genaration Ends -->");
                    strPaymentOption.Append("</tr>");
                    strPaymentOption.Append("<tr ><td colspan=\"3\" height=\"15\"></td></tr>");
                    strPaymentOption.Append("</table>");
                }
                else
                {
                    strPaymentOption.Append("<table id=\"tabPOMain\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tableborder\">");
                    strPaymentOption.Append("<tr>");
                    strPaymentOption.Append("<td colspan=\"4\" class=\"style8\" align=\"center\" valign=\"top\" width=\"100%\">");
                    strPaymentOption.Append("You can make the payment by selecting any of the following methods. We support all major credit cards & transfers through Paypal.");
                    strPaymentOption.Append("</td>");
                    strPaymentOption.Append("</tr>");
                    strPaymentOption.Append("<tr ><td colspan=\"4\" height=\"15\"></td></tr>");
                    strPaymentOption.Append("<tr>");
                    foreach (DataRow drPo in _dtPoPgtoPrint.Rows)
                    {
                        strPaymentOption.Append("<tr>");
                        strPaymentOption.Append("<td align=\"center\" valign=\"middle\" class=\"style1\" width=\"5%\">");
                        strPaymentOption.Append(Convert.ToString(intTotalCount + 1) + ".");
                        strPaymentOption.Append("</td>");
                        strPaymentOption.Append("<td align=\"right\" valign=\"middle\" class=\"style1\" width=\"5%\">");
                        //strPaymentOption.Append("<input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["ID"].ToString() + "\" value=\"" + drPo["ID"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:showPODesc(this, 'dvPaymentOptDescription');\">");
                        //strPaymentOption.Append("<input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["ID"].ToString() + "\" value=\"" + drPo["ID"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgId(this, 'dvPaymentOptDescription');\">");
                        strPaymentOption.Append("<input type=\"radio\" name=\"radPayOpt\" id=\"" + drPo["PoId"].ToString() + "\" value=\"" + drPo["PoId"].ToString() + "\" checked=\"false\" class=\"input\" onClick=\"javascript:setPoPgIdNew('" + drPo["PoId"].ToString() + "', '" + drPo["PgId"].ToString() + "');\">");
                        strPaymentOption.Append("</td>");
                        strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style8\" width=\"60%\">");
                        strPaymentOption.Append(drPo["Name"].ToString());
                        //strPaymentOption.Append(drPo["Name"].ToString() + " - " + drPo["PgId"].ToString());
                        strPaymentOption.Append("</td>");
                        strPaymentOption.Append("<td align=\"left\" valign=\"middle\" class=\"style1\" width=\"30%\">");
                        strPaymentOption.Append("<img src=\"" + drPo["Image"].ToString() + "\" alt=\"" + drPo["Name"].ToString() + "\" align=\"left\">");
                        strPaymentOption.Append("</td>");
                        strPaymentOption.Append("</tr>");
                        intTotalCount++;
                    }
                    strPaymentOption.Append("<!-- Payment Options genaration Ends -->");
                    strPaymentOption.Append("</tr>");
                    strPaymentOption.Append("<tr ><td colspan=\"4\" height=\"15\"></td></tr>");
                    strPaymentOption.Append("</table>");
                }

                strOutPut = strPaymentOption.ToString();
            }
            else
            {
                blFlag = false;
                strOutPut = "No payment options found.";
            }
        }
        catch (Exception ex)
        {
            strOutPut = "Error !<br>" + ex.Message;
            blFlag = false;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        strPaymentOptionHtml = strOutPut;
        return blFlag;
    }
    public void fnInsertComboIfExists()
    {
        int flag = 0;
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        try
        {
            string strSQL =
                "SELECT " +
                "" + strSchema + ".ComboSbill_Relation.SBillNo " +
                "FROM " +
                "" + strSchema + ".ComboSbill_Relation " +
                "WHERE " +
                "(" + strSchema + ".ComboSbill_Relation.ComboId='" + strcomboid + "')";

            SqlCommand cmd1 = new SqlCommand(strSQL, conn);
            SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr["SBillNo"].ToString().Equals(strArrSbill[0].ToString()))
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            rdr.Close();
        }
        catch (SqlException ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            conn.Close();
        }
        string Temp_ComboId = "";
        if (flag == 0)
        {
            if (conn.State.ToString() == "Closed")
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("" + strSchema + ".ComboIdInsert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();

            param = cmd.Parameters.Add("@ComboId", SqlDbType.VarChar, 18);
            param.Direction = ParameterDirection.Input;
            if (strcomboid == "")
            {
                param.Value = "";
            }
            else
            {
                param.Value = strcomboid.Trim();
            }
            param = cmd.Parameters.Add("@SBillNo", SqlDbType.VarChar, 18);
            param.Direction = ParameterDirection.Input;
            param.Value = strArrSbill[0].Trim();

            Temp_ComboId = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();
        }

    }
    //protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //    //if((SbillNo!="") && (strcomboid!=""))
       
    //}
    public DataTable fnGrtOrderCombo(string strOrderNo)//to get order nos wrt combo id
    {
        string strComboid = strOrderNo;
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
        conn.Close();
        return dt;
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


    protected void ImgSaveContinue_Click(object sender, EventArgs e)
    {
        int flag = 0;
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        try
        {
            string strSQL =
                "SELECT " +
                "" + strSchema + ".ComboSbill_Relation.SBillNo " +
                "FROM " +
                "" + strSchema + ".ComboSbill_Relation " +
                "WHERE " +
                "(" + strSchema + ".ComboSbill_Relation.ComboId='" + strcomboid + "')";

            SqlCommand cmd1 = new SqlCommand(strSQL, conn);
            SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr["SBillNo"].ToString().Equals(strArrSbill[0].ToString()))
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            rdr.Close();
        }
        catch (SqlException ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            conn.Close();
        }
        string Temp_ComboId = "";
        if (flag == 0)
        {
            if (conn.State.ToString() == "Closed")
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("" + strSchema + ".ComboIdInsert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();

            param = cmd.Parameters.Add("@ComboId", SqlDbType.VarChar, 18);
            param.Direction = ParameterDirection.Input;
            if (strcomboid == "")
            {
                param.Value = "";
            }
            else
            {
                param.Value = strcomboid.Trim();
            }
            param = cmd.Parameters.Add("@SBillNo", SqlDbType.VarChar, 18);
            param.Direction = ParameterDirection.Input;
            param.Value = strArrSbill[0].Trim();

            Temp_ComboId = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();
        }
        if (Temp_ComboId == "")
        {
            //Response.Redirect("Submit_Form.aspx?comboid=" + strcomboid + "&currencyid=" + currid + "&senddata=" + strsenddata + "&userid=" + struserid + "&SiteId=" + strsiteid);
            Response.Redirect("24x7_SubmitForm.aspx?comboid=" + strcomboid + "&currencyid=" + currid + "&senddata=" + strsenddata + "&userid=" + struserid + "&SiteId=" + strsiteid + "&disc=" + Convert.ToString(((DataTable)Session["dtCart"]).Rows[0]["discCode"]));
        }
        else
        {
            //Response.Redirect("Submit_Form.aspx?comboid=" + Temp_ComboId + "&currencyid=" + currid + "&senddata=" + strsenddata + "&userid=" + struserid + "&SiteId=" + strsiteid);
            Response.Redirect("24x7_SubmitForm.aspx?comboid=" + Temp_ComboId + "&currencyid=" + currid + "&senddata=" + strsenddata + "&userid=" + struserid + "&SiteId=" + strsiteid + "&disc=" + Convert.ToString(((DataTable)Session["dtCart"]).Rows[0]["discCode"]));
        }
    }
    protected void ImgSaveMakeNewOrder_Click(object sender, EventArgs e)
    {
        int flag = 0;
        if (conn.State.ToString() == "Closed")
        {
            conn.Open();
        }
        try
        {
            string strSQL =
                "SELECT " +
                "rgcards_gti24x7.ComboSbill_Relation.SBillNo " +
                "FROM " +
                "rgcards_gti24x7.ComboSbill_Relation " +
                "WHERE " +
                "(rgcards_gti24x7.ComboSbill_Relation.ComboId='" + strcomboid + "')";

            SqlCommand cmd1 = new SqlCommand(strSQL, conn);
            SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr["SBillNo"].ToString().Equals(strArrSbill[0].ToString()))
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            rdr.Close();
        }
        catch (SqlException ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            conn.Close();
        }
        string Temp_ComboId = "";
        if (flag == 0)
        {
            if (conn.State.ToString() == "Closed")
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("rgcards_gti24x7.ComboIdInsert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();

            param = cmd.Parameters.Add("@ComboId", SqlDbType.VarChar, 18);
            param.Direction = ParameterDirection.Input;
            if (strcomboid == "")
            {
                param.Value = "";
            }
            else
            {
                param.Value = strcomboid.Trim();
            }
            param = cmd.Parameters.Add("@SBillNo", SqlDbType.VarChar, 18);
            param.Direction = ParameterDirection.Input;
            param.Value = strArrSbill[0].Trim();

            Temp_ComboId = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();
        }
        string strRedirectRakhi = "";
        string strDisCode = ((DataTable)Session["dtCart"]).Rows[0]["discCode"].ToString();
        if (Temp_ComboId == "")
        {
            strRedirectRakhi = "http://www." + strGenSitename + "/index.aspx?comboid=" + strcomboid + "&currencyid=" + currid + "&userid=" + struserid + "&val=" + strDisCode;
            Session.Remove("dtCart");
            Session.Abandon();
            Response.Redirect(strRedirectRakhi);
            //Response.Redirect("http://localhost:1360/rakhi24x7_new/index.aspx?comboid=" + strcomboid + "&currencyid=" + currid + "&userid=" + struserid);
        }
        else
        {
            strRedirectRakhi = "http://www." + strGenSitename + "/index.aspx?comboid=" + Temp_ComboId + "&currencyid=" + currid + "&userid=" + struserid + "&val=" + strDisCode;
            Session.Remove("dtCart");
            Session.Abandon();
            Response.Redirect(strRedirectRakhi);
            //Response.Redirect("http://localhost:1360/rakhi24x7_new/index.aspx?comboid=" + Temp_ComboId + "&currencyid=" + currid + "&userid=" + struserid);
        }
    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if ((strcomboid != "") && (Request.Params["frmCart"] == null) && ((Request.Params["cancel"] == null) || (Request.Params["cancel"] == "")))
        {
            fnInsertComboIfExists();
        }
        if ((Session["dtCart"] != null) && (Session["userDetails"] != null))
        {
            System.Data.DataTable dtCart = (DataTable)Session["dtCart"];
            System.Collections.Hashtable htUserDetail = (Hashtable)Session["userDetails"];
            if (dtCart.Rows.Count > 0)
            {
                if (htUserDetail.Count > 0)
                {
                    string strPoId = Convert.ToString(hdnPoId.Value.ToString());
                    string strPgId = Convert.ToString(hdnPgId.Value.ToString());
                    if ((strPoId != "") && (strPgId != ""))
                    {
                        strSql = "SELECT " +
                                "[" + strSchema + "].[Payment_Gateway_Master].[ID] AS [GatewayId], " +
                                "[" + strSchema + "].[Payment_Gateway_Master].[Name] AS [GatewayName], " +
                                "[" + strSchema + "].[Payment_Gateway_Master].[GatewayNameToShow] AS [GatewayNametoShow], " +
                                "[" + strSchema + "].[Payment_Gateway_Master].[ChargeNameToShow] AS [ChargeName], " +
                                "[" + strSchema + "].[Payment_Option_Master].[ID], " +
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
                                "([" + strSchema + "].[Payment_Option_Master].[ID]=" + strPoId + ")" +
                            " AND " +
                                "([" + strSchema + "].[Payment_Gateway_Master].[ID]=" + strPgId + ") " +
                            " AND " +
                                "([" + strSchema + "].[Payment_Option_Master].[Record_Status]=1) " +
                            "AND " +
                                "([" + strSchema + "].[Payment_Gateway_Master].[Record_Status]=1) " +
                            " ORDER BY " +
                                "[" + strSchema + "].[Payment_Option_Gateway_Relation].[Rank]";
                        try
                        {
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
                            SqlCommand cmdPo = new SqlCommand(strSql, conn);
                            SqlDataReader drPo = cmdPo.ExecuteReader(CommandBehavior.CloseConnection);
                            if (drPo.HasRows)
                            {
                                if (drPo.Read())
                                {
                                    if (!htUserDetail.Contains("poId"))
                                    {
                                        htUserDetail.Add("poId", strPoId);
                                    }
                                    else
                                    {
                                        htUserDetail.Remove("poId");
                                        htUserDetail.Add("poId", strPoId);
                                    }

                                    if (!htUserDetail.Contains("poName"))
                                    {
                                        htUserDetail.Add("poName", drPo["PoName"].ToString());
                                    }
                                    else
                                    {
                                        htUserDetail.Remove("poName");
                                        htUserDetail.Add("poName", drPo["PoName"].ToString());
                                    }

                                    if (!htUserDetail.Contains("poRank"))
                                    {
                                        htUserDetail.Add("poRank", drPo["Rank"].ToString());
                                    }
                                    else
                                    {
                                        htUserDetail.Remove("poRank");
                                        htUserDetail.Add("poRank", drPo["Rank"].ToString());
                                    }

                                    if (!htUserDetail.Contains("pgId"))
                                    {
                                        htUserDetail.Add("pgId", drPo["GatewayId"].ToString());
                                    }
                                    else
                                    {
                                        htUserDetail.Remove("pgId");
                                        htUserDetail.Add("pgId", drPo["GatewayId"].ToString());
                                    }

                                    if (!htUserDetail.Contains("pgName"))
                                    {
                                        htUserDetail.Add("pgName", drPo["GatewayName"].ToString());
                                    }
                                    else
                                    {
                                        htUserDetail.Remove("pgName");
                                        htUserDetail.Add("pgName", drPo["GatewayName"].ToString());
                                    }

                                    if (!htUserDetail.Contains("pgNametoShow"))
                                    {
                                        htUserDetail.Add("pgNametoShow", drPo["GatewayNametoShow"].ToString());
                                    }
                                    else
                                    {
                                        htUserDetail.Remove("pgNametoShow");
                                        htUserDetail.Add("pgNametoShow", drPo["GatewayNametoShow"].ToString());
                                    }

                                    if (!htUserDetail.Contains("pgChargeName"))
                                    {
                                        htUserDetail.Add("pgChargeName", drPo["ChargeName"].ToString());
                                    }
                                    else
                                    {
                                        htUserDetail.Remove("pgChargeName");
                                        htUserDetail.Add("pgChargeName", drPo["ChargeName"].ToString());
                                    }
                                    //===========either combo or sbill====================
                                    if (strcomboid != "")
                                    {
                                        if (!htUserDetail.Contains("comboid"))
                                        {
                                            htUserDetail.Add("comboid", strcomboid.ToString());
                                            htUserDetail.Remove("ordNo");
                                        }

                                    }
                                    else
                                    {
                                        //htUserDetail.Add("ordNo", SbillNo.ToString());
                                        htUserDetail.Remove("comboid");
                                    }

                                    //=====================================================

                                    Session["userDetails"] = htUserDetail;
                                }
                                drPo.Dispose();
                                //============here put code 4 prev update===============
                                if ((htUserDetail.Contains("disCode")) && (Request.Params["frmCart"] == null))
                                {
                                    if ((Request.Params["cancel"] != null) && (Request.Params["cancel"] != ""))
                                    {

                                    }
                                    else
                                    {
                                        if (htUserDetail.Contains("comboid"))
                                        {
                                            if ((htUserDetail["disCode"].ToString() != "-") && (htUserDetail["disCode"].ToString() != "0"))
                                            {
                                                //rakhi_new obj = new rakhi_new();
                                                //obj.sbill = Request.Params["SbillNo"].ToString();
                                                //obj.discount = htUserDetail["disCode"].ToString();
                                                //obj.poid = Convert.ToInt32(htUserDetail["poId"]);
                                                //obj.pgid = Convert.ToInt32(htUserDetail["pgId"]);
                                                //obj.updaterecordforprevdiscount();
                                            }
                                        }
                                    }
                                }
                                //===========end==============================
                                if (htUserDetail.Contains("ordNo"))
                                {
                                    if (checkSamePoPg(Convert.ToString(htUserDetail["ordNo"].ToString()), Convert.ToString(htUserDetail["pgId"].ToString()), Convert.ToString(htUserDetail["poId"].ToString()), ref strError))
                                    {
                                        //update order
                                        SqlCommand cmdOrderUpdate = conn.CreateCommand();
                                        cmdOrderUpdate.CommandText = "" + strSchema + ".updateorder";
                                        cmdOrderUpdate.CommandType = CommandType.StoredProcedure;

                                        SqlParameter paramgateway = cmdOrderUpdate.Parameters.Add(new SqlParameter("@pgateway", SqlDbType.Int));
                                        paramgateway.Direction = ParameterDirection.Input;
                                        paramgateway.Value = Convert.ToInt32(htUserDetail["pgId"]);

                                        SqlParameter paramoptionid = cmdOrderUpdate.Parameters.Add(new SqlParameter("@poptionid", SqlDbType.Int));
                                        paramoptionid.Direction = ParameterDirection.Input;
                                        paramoptionid.Value = Convert.ToInt32(htUserDetail["poId"]);

                                        SqlParameter paramSbillNo = cmdOrderUpdate.Parameters.Add(new SqlParameter("@pSbillNo", SqlDbType.VarChar, 50));
                                        paramSbillNo.Direction = ParameterDirection.Input;
                                        paramSbillNo.Value = SbillNo;
                                        if (conn.State != ConnectionState.Open) { conn.Open(); }
                                        cmdOrderUpdate.ExecuteScalar();
                                        if (conn.State != ConnectionState.Closed) { conn.Close(); }
                                        Response.Redirect("24x7_OrderDisplay.aspx");
                                    }
                                    else
                                    {
                                        dvBtn.Visible = false;
                                        dvPaymentOptList.InnerHtml = strError;
                                    }
                                }
                                else
                                {
                                    if ((Request.Params["frmCart"] == null))
                                    {
                                        DataTable dtCombo = fnGrtOrderCombo(htUserDetail["comboid"].ToString());
                                        //update order
                                        for (int i = 0; i < dtCombo.Rows.Count; i++)
                                        {
                                            SqlCommand cmdOrderUpdate = conn.CreateCommand();
                                            cmdOrderUpdate.CommandText = "[" + strSchema + "].[updateorder]";
                                            cmdOrderUpdate.CommandType = CommandType.StoredProcedure;

                                            SqlParameter paramgateway = cmdOrderUpdate.Parameters.Add(new SqlParameter("@pgateway", SqlDbType.Int));
                                            paramgateway.Direction = ParameterDirection.Input;
                                            paramgateway.Value = Convert.ToInt32(htUserDetail["pgId"]);

                                            SqlParameter paramoptionid = cmdOrderUpdate.Parameters.Add(new SqlParameter("@poptionid", SqlDbType.Int));
                                            paramoptionid.Direction = ParameterDirection.Input;
                                            paramoptionid.Value = Convert.ToInt32(htUserDetail["poId"]);

                                            SqlParameter paramSbillNo = cmdOrderUpdate.Parameters.Add(new SqlParameter("@pSbillNo", SqlDbType.VarChar, 50));
                                            paramSbillNo.Direction = ParameterDirection.Input;
                                            paramSbillNo.Value = dtCombo.Rows[i]["SBillNo"].ToString();
                                            if (conn.State != ConnectionState.Open) { conn.Open(); }
                                            cmdOrderUpdate.ExecuteScalar();
                                            if (conn.State != ConnectionState.Closed) { conn.Close(); }
                                        }
                                        Response.Redirect("24x7_OrderDisplay.aspx");
                                    }
                                    else
                                    {
                                        dvBtn.Visible = false;
                                        dvPaymentOptList.InnerHtml = strError;
                                    }
                                    //end
                                    //Response.Redirect("orderDisplay3.aspx");
                                    ////Response.Redirect("InvoicePage.aspx");


                                }
                            }
                            else
                            {
                                dvPaymentOptList.InnerHtml = "No payment gate way details found. Please press refresh button.";
                                dvBtn.Visible = false;
                            }
                        }
                        catch (SqlException ex)
                        {
                            dvPaymentOptList.InnerHtml = "Error!<br/>" + ex.Message + "<br/>Please press refresh button.";
                            dvBtn.Visible = false;
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
                        dvPaymentOptList.InnerHtml = "Payment option cannot be determined. Please press refresh button.";
                        dvBtn.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect(objCart.showNoitem());
                }
            }
            else
            {
                Response.Redirect(objCart.showNoitem());
            }
        }
        else
        {
            Response.Redirect(objCart.showNoitem());
        }
    }
}

