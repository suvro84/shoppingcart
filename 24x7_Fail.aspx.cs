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

public partial class _24x7_Fail : System.Web.UI.Page
{
    public string strOrderNo = "";
    public string strBankId = "";
    public string strOptionId = "";
    public int intSiteId = 0;
    public string strSiteName = "";
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["dbCon"].ToString());
    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["Schema"].ToString());
    //Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
    //string strSql = "";
    string strError = "";
    string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
    public string strImagePath = "";
    public string strpageCSS = "";
    public string strRedirectBaseDomain = "";
    public string strpageheaderimage = "";
    public string strGenSitename = "";
    cartFunctions objCartFunc = new cartFunctions();
    orderDetail objOrderDetail = new orderDetail();
    Hashtable htPayDetail = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtCombo = new DataTable();

        if (Request.QueryString["sbillno"] != null && (!Page.IsPostBack))
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

            System.Data.DataTable dtCart = (DataTable)Session["dtCart"];


            if (dtCart.Rows.Count > 0)
            {
                dtCart.Rows.Clear();
            }
            strError = "";

            if (strOrderNo.ToString().Substring(0, 1).Equals("C") && (!Page.IsPostBack))//checking for combo order
            {
                dtCombo = fnGrtOrderCombo(strOrderNo);
                objOrderDetail.getUserDetails(dtCombo.Rows[0]["SBillNo"].ToString(), ref htPayDetail, ref strError);
                //=========put combo id in hash table==================
                if (htPayDetail.Contains("ordNo"))
                {
                    htPayDetail.Remove("ordNo");
                    htPayDetail.Add("comboid", strOrderNo);//same as combo
                    hdnCombo.Value = strOrderNo;
                    hdnOrder.Value = "";
                }
                //========end==========================================

                for (int i = 0; i < dtCombo.Rows.Count; i++)
                {
                    strOrderNo = dtCombo.Rows[i]["SBillNo"].ToString();
                    fnPopCart(dtCart, strOrderNo);
                }
                if (!htPayDetail.Contains("disCode"))
                {
                    htPayDetail.Add("disCode", "0");
                }
            }
            else
            {
                objOrderDetail.getUserDetails(strOrderNo, ref htPayDetail, ref strError);
                fnPopCart(dtCart, strOrderNo);
                hdnOrder.Value = strOrderNo;
                hdnCombo.Value = "";
            }

        }
        if ((strOrderNo != "") && (!Page.IsPostBack))
        {
            strError = "";
            if (objOrderDetail.getSiteIdNameFromOrderNumber(strOrderNo, ref intSiteId, ref strSiteName, ref strError))
            {
                hdnSiteId.Value = intSiteId.ToString();
                clsRakhi objRakhiHd = new clsRakhi();
                objRakhiHd.functionforImageHead(intSiteId, ref strpageCSS, ref strpageheaderimage, ref strRedirectBaseDomain, ref strGenSitename);
                strGenSitename = objRakhiHd.SentenceCase(strGenSitename.Trim());
                if ((intSiteId == 132) || (intSiteId == 154) || (intSiteId == 1))      // If its giftbhejo, ndtv.giftstoindia24x7 or giftstoindia24x7 dont show the payment message
                {
                    topMsg.Visible = false;
                }
                else
                {
                    topMsg.Visible = true;
                }
            }
            else
            {
                lblError.Text = strError;
                dvBtnSame.Visible = false;
                dvBtnNext.Visible = false;
                dvBtnOther.Visible = false;
                dvMain.Visible = false;
            }
        }
        else
        {
            dvBtnSame.Visible = false;
            dvBtnNext.Visible = false;
            dvBtnOther.Visible = false;
            dvMain.Visible = false;
            lblError.Text = "<br/><br/><center><b>There is some error on retrievation of your order number or selected bank.</br>We are sorry for that!</b></center><br/>Please go to HomePage.<br/>";
        }
    }
    protected string showNoitem(string strSiteName)
    {
        string strOutPut = "";
        StringBuilder strCartOutput = new StringBuilder();
        strCartOutput.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"> ");
        strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strCartOutput.Append("<tr>");
        strCartOutput.Append("<td valign=\"top\" align=\"center\" class=\"style1\">");
        strCartOutput.Append("There are no items in your cart. Please continue shopping by clicking on the 'continue shopping' button below.");
        strCartOutput.Append("</td>");
        strCartOutput.Append("<tr class=\"clear10\"><td class=\"clear10\"></td></tr>");
        strCartOutput.Append("<tr align=\"center\" valign=\"middle\">");
        strCartOutput.Append("<td class=\"style1\" valign=\"middle\" align=\"center\">");
        strCartOutput.Append("<div align=\"center\"><a href=\"http://www." + strSiteName + "\"><img src=\"Pictures/but_continue_shopping.gif\" border=\"0\" /></a> </div>");
        strCartOutput.Append("</td>");
        strCartOutput.Append("</tr>");
        strCartOutput.Append("</table>");
        strOutPut = strCartOutput.ToString();
        return strOutPut;
    }









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
    protected void fnPopCart(DataTable dtCart, string strOrderNo)
    {
        if (Session["dtCart"] != null)
        {

            strError = "";
            if (!objCartFunc.makeTheCart(intSiteId, strOrderNo, ref dtCart, ref strError))
            {
                lblError.Text = strError;
                dvBtnSame.Visible = false;
                dvBtnNext.Visible = false;
                dvBtnOther.Visible = false;
                dvMain.Visible = false;
            }
            else
            {
                if (dtCart.Rows.Count > 0)
                {
                    if (htPayDetail.Count > 0)
                    {
                        Session["dtCart"] = dtCart;
                        Session["userDetails"] = htPayDetail;
                        string strPoId = "";
                        string strPgId = "";
                        string strPoName = "";
                        string strPgName = "";
                        string strPoPgRank = "";
                        if (objOrderDetail.getPoPg(strOrderNo, ref strPgId, ref strPgName, ref strPoId, ref strPoName, ref strPoPgRank, ref strError) == false)
                        {
                            lblError.Text = strError;
                            dvBtnSame.Visible = false;
                            dvBtnNext.Visible = false;
                            dvBtnOther.Visible = false;
                            dvMain.Visible = false;
                        }
                        else
                        {
                            if (objOrderDetail.CheckMaxGateway(strPoPgRank, strPoId, strPgId, ref strError))
                            {
                                dvBtnNext.Visible = false;
                            }
                            else
                            {
                                dvBtnNext.Visible = true;
                            }

                            hdnPgId.Value = strPgId;
                            hdnPgName.Value = strPgName;
                            hdnPoId.Value = strPoId;
                            hdnPoName.Value = strPoName;
                            hdPoPgRank.Value = strPoPgRank;
                            // Modify the hash tables pg details
                            if (!htPayDetail.Contains("poId"))
                            {
                                htPayDetail.Add("poId", strPoId);
                            }
                            else
                            {
                                htPayDetail.Remove("poId");
                                htPayDetail.Add("poId", strPoId);
                            }

                            if (!htPayDetail.Contains("poName"))
                            {
                                htPayDetail.Add("poName", strPoName);
                            }
                            else
                            {
                                htPayDetail.Remove("poName");
                                htPayDetail.Add("poName", strPoName);
                            }

                            if (!htPayDetail.Contains("pgId"))
                            {
                                htPayDetail.Add("pgId", strPgId);
                            }
                            else
                            {
                                htPayDetail.Remove("pgId");
                                htPayDetail.Add("pgId", strPgId);
                            }

                            if (!htPayDetail.Contains("pgName"))
                            {
                                htPayDetail.Add("pgName", strPgName);
                            }
                            else
                            {
                                htPayDetail.Remove("pgName");
                                htPayDetail.Add("pgName", strPgName);
                            }

                            dvBtnSame.Visible = true;
                            //dvBtnNext.Visible = true;
                            dvBtnOther.Visible = true;
                            dvMain.Visible = true;
                        }
                    }
                }
                else
                {
                    lblError.Text = showNoitem(strSiteName);
                    dvBtnSame.Visible = false;
                    dvBtnNext.Visible = false;
                    dvBtnOther.Visible = false;
                    dvMain.Visible = false;
                }
            }
        }
        else
        {
            lblError.Text = showNoitem(strSiteName);
            dvBtnSame.Visible = false;
            dvBtnNext.Visible = false;
            dvBtnOther.Visible = false;
            dvMain.Visible = false;
        }
    }

    protected void btnTryAgainSame_Click(object sender, EventArgs e)
    {
        if ((hdnSiteId.Value.ToString().Length > 0) && (hdnPgId.Value.ToString().Length > 0) && (hdnPoId.Value.ToString().Length > 0))
        {
            //Response.Write("Payment Gateway Id: " + hdnPgId.Value.ToString() + " Gateway Name: " + hdnPgName.Value.ToString() + " Payment Option Id : " + hdnPoId.Value.ToString() + " Option Name: " +hdnPoName.Value.ToString());
            Response.Redirect("24x7_CancelRedirect.aspx?siteId=" + hdnSiteId.Value.ToString() + "&pgId=" + hdnPgId.Value.ToString() + "&poId=" + hdnPoId.Value.ToString() + "&flag=1");
        }
        else
        {
            lblError.Text = "There is some error to retrieve the payment gateway details. Please refresh the page.";
        }
    }
    protected void btnTryAgainNext_Click(object sender, EventArgs e)
    {
        if ((hdnSiteId.Value.ToString().Length > 0) && (hdnPgId.Value.ToString() != "") && (hdnPoId.Value.ToString() != "") && (hdPoPgRank.Value.ToString() != ""))
        {
            //Response.Write("Payment Gateway Id: " + hdnPgId.Value.ToString() + " Gateway Name: " + hdnPgName.Value.ToString() + " Payment Option Id : " + hdnPoId.Value.ToString() + " Option Name: " + hdnPoName.Value.ToString());
            Response.Redirect("24x7_CancelRedirect.aspx?siteId=" + hdnSiteId.Value.ToString() + "&pgId=" + hdnPgId.Value.ToString() + "&poId=" + hdnPoId.Value.ToString() + "&PoPgRank=" + hdPoPgRank.Value.ToString() + "&flag=2");
        }
        else
        {
            lblError.Text = "There is some error to retrieve the payment gateway details. Please refresh the page.";
        }
    }
    protected void btnTryAgainPoption_Click(object sender, EventArgs e)
    {
        if (hdnCombo.Value != "")
        {
            Response.Redirect("24x7_paymentoption.aspx?siteId=" + hdnSiteId.Value.ToString() + "&comboid=" + hdnCombo.Value + "&cancel=1");
        }
        else
        {
            Response.Redirect("24x7_paymentoption.aspx?siteId=" + hdnSiteId.Value.ToString() + "&SbillNo=" + hdnOrder.Value + "&cancel=1");

        }
    }
}