using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class EditQantity : System.Web.UI.Page
{
    //protected void Page_Load(object sender, EventArgs e)
    //{

    //}

    //public void GetData()
    //{
    //    DataTable dtIP = new DataTable();
    //    int intPageNo = 1;
    //    string searchip = "";
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        intPageNo = Convert.ToInt32(Request.QueryString["pageno"]);
    //    }
    //    if (Request.QueryString["searchip"] != null)
    //    {
    //        if (txtIP_Address.Value != "")
    //        {
    //            searchip = txtIP_Address.Value;
    //            intPageNo = 1;
    //        }
    //        else
    //        {
    //            searchip = Convert.ToString(Request.QueryString["searchip"]);
    //            txtIP_Address.Value = searchip;

    //        }
    //    }
    //    else
    //    {
    //        searchip = Convert.ToString(txtIP_Address.Value).Replace("'", "''");
    //    }
    //    string strAllProd = " SELECT " +
    //                   " count(rgcards_gti24x7.SalesMaster_BothWay.SBillNo) as cnt  " +
    //                   " FROM  rgcards_gti24x7.SalesMaster_BothWay  " +
    //                   " where IP_Address='" + searchip + "'";

    //    strSql = " select * from " +
    //             " (select  ROW_NUMBER() OVER(ORDER BY rgcards_gti24x7.SalesMaster_BothWay.SBillNo) AS 'SlNo', " +
    //             " (((ROW_NUMBER() OVER(ORDER BY rgcards_gti24x7.SalesMaster_BothWay.SBillNo)-1)/25)+1) AS 'PageNumber', " +
    //             " SBillNo," +
    //             " case convert(varchar(10),Sbill_Status)" +
    //             " when '0' then 'success'" +
    //             " when '1' then 'wait' " +
    //             " when '2' then 'Shipped'" +
    //             " when '3' then 'Delivered'" +
    //             " when '4' then 'Payment Taking'" +
    //             " when '5' then 'Payment Received'" +
    //             " when '6' then 'Cancelled'" +
    //             " when '7' then 'Deleted'" +
    //             " when '8' then 'Acknowledge'" +
    //             " when '9' then 'Sent'" +
    //             " ELSE 'Other' " +
    //             " END as 'Status', " +
    //            " IP_Address, CONVERT(VARCHAR(11), Sbill_DOS, 106)as Sbill_DOS from rgcards_gti24x7.SalesMaster_BothWay where IP_Address='" + searchip + "' )as tab" +
    //             " where tab.PageNumber=" + intPageNo + "";

    //    Admin_Module_Works_Select objSelect = new Admin_Module_Works_Select(dtIP, strSql, strSchema, ref strError);
    //    if (strError == null)
    //    {
    //        string strPagination = "";
    //        int intTotalProd = 0;
    //        if (CountProd_Pagination(strAllProd, searchip, intPageNo, 25, ref  strPagination, ref  intTotalProd, ref strError))
    //        {
    //            if (dtIP.Rows.Count > 0)
    //            {
    //                rptIP.Visible = true;
    //                rptIP.DataSource = dtIP;
    //                rptIP.DataBind();
    //                foreach (RepeaterItem rpt in rptIP.Items)
    //                {
    //                    Literal Status = (Literal)rpt.FindControl("ltStatus");
    //                    if (Status.Text == "Cancelled")
    //                    {
    //                        HtmlGenericControl Path = (HtmlGenericControl)rpt.FindControl("dvPath");
    //                        Path.InnerHtml = "Visitor Path";
    //                    }
    //                }
    //                lblPage.Text = strPagination;
    //                lblMsg.Text = "";
    //            }
    //            else
    //            {
    //                lblMsg.Text = "No Orders for this IP  found";
    //                lblPage.Text = "";
    //                rptIP.Visible = false;
    //            }
    //        }
    //    }
    //}
    //public bool CountProd_Pagination(string strSql, string searchip, int intPageNo, int numberofProduct, ref string strPagination, ref int intTotalProd, ref string strError)
    //{
    //    bool flag = false;

    //    using (SqlConnection conn = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["dbCon"])))
    //    {
    //        if (conn.State == ConnectionState.Closed) { conn.Open(); }
    //        try
    //        {
    //            using (SqlDataReader rdr = new SqlCommand(strSql, conn).ExecuteReader(CommandBehavior.CloseConnection))
    //            {
    //                if (rdr.HasRows)
    //                {
    //                    flag = true;
    //                    if (rdr.Read())
    //                    {
    //                        intTotalProd = Convert.ToInt32(rdr["cnt"]);
    //                        if (intPageNo == 0)
    //                        {
    //                            //strPagination = "<li>" + intTotalProd + " of " + intTotalProd + "</li>";
    //                        }
    //                        else
    //                        {
    //                            if (intTotalProd > numberofProduct)
    //                            {
    //                                int intRowUpto = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(intTotalProd) / numberofProduct));
    //                                StringBuilder strReturnPagination = new StringBuilder();
    //                                strReturnPagination.Append("<ul class=\"pagination\">");
    //                                if (intPageNo == 1)
    //                                {
    //                                    if (intTotalProd < numberofProduct)
    //                                    {
    //                                        strReturnPagination.Append("<li>Previous</li>");
    //                                    }
    //                                    else
    //                                    {
    //                                        strReturnPagination.Append("<li>Previous</li>");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?searchip=" + searchip + "&pageno=" + (intPageNo - 1) + "\" title=\"Page " + Convert.ToString(intPageNo - 1) + "\"> Previous </a></li>");
    //                                }
    //                                for (int i = 0; i < intRowUpto; i++)
    //                                {
    //                                    if ((i + 1) == intPageNo)
    //                                    {
    //                                        strReturnPagination.Append("<li><strong>" + (i + 1) + "</strong></li>");
    //                                    }
    //                                    else if (i == intRowUpto)
    //                                    {
    //                                        strReturnPagination.Append("<li ><strong>" + (i + 1) + "</strong></li>");
    //                                    }
    //                                    else
    //                                    {
    //                                        strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?searchip=" + searchip + "&pageno=" + (i + 1) + "\" title=\"Page " + (i + 1) + "\">" + (i + 1) + "&nbsp;</a></li>");
    //                                    }
    //                                }
    //                                if (intPageNo == intRowUpto)
    //                                {
    //                                    strReturnPagination.Append("<li>Next</li>");
    //                                }
    //                                else
    //                                {
    //                                    strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?searchip=" + searchip + "&pageno=" + (intPageNo + 1) + "\" title=\"Page " + Convert.ToString(intPageNo + 1) + "\">Next</a></li>");
    //                                }
    //                                strReturnPagination.Append("</ul>");
    //                                strPagination = strReturnPagination.ToString();


    //                            }
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    flag = false;
    //                }
    //            }
    //        }
    //        catch (SqlException ex)
    //        {
    //            strError = ex.Message;
    //        }
    //    }
    //    return flag;
    //}

    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    BindGV();
    //}
    //public void BindGV()
    //{

    //    string sbillno = "";

    //    if (GetSbillNo(ref sbillno))
    //    {

    //        hdSales_ATOT.Value = "";
    //        ViewState["hdDisc"] = null;
    //        ViewState["hdCode"] = null;
    //        ViewState["hdType"] = null;
    //        strSql = " SELECT " +
    //                         " ROW_NUMBER() OVER(ORDER BY rgcards_gti24x7.SalesDetails_BothWay.RecId) AS 'SlNo'," +
    //                         " rgcards_gti24x7.SalesDetails_BothWay.RecId," +
    //                         " rgcards_gti24x7.SalesDetails_BothWay.Price," +
    //                         " rgcards_gti24x7.SalesDetails_BothWay.Product_Id," +
    //                         " rgcards_gti24x7.SalesDetails_BothWay.QOS," +
    //                         " rgcards_gti24x7.SalesMaster_BothWay.Sales_ATOT, " +
    //                         " rgcards_gti24x7.ItemMaster_Server.Item_Name, " +
    //                         " rgcards_gti24x7.Order_Pg_Details.pgCommission," +
    //                         " rgcards_gti24x7.Order_Pg_Details.serviceTax, " +
    //                         " rgcards_gti24x7.Payment_Gateway_Master.Name, " +
    //                         " rgcards_gti24x7.Payment_Gateway_Master.ChargePercent," +
    //                         " rgcards_gti24x7.Payment_Gateway_Master.ServiceTaxPercent," +
    //                         " rgcards_gti24x7.Discount_Code_Master.Code, " +
    //            //  " rgcards_gti24x7.Discount_Code_Master.Type, " +
    //                         " rgcards_gti24x7.Discount_Code_Master.TypeValue, " +
    //                        " CASE WHEN rgcards_gti24x7.Discount_Code_Master.Type =1 THEN 'Fixed' ELSE 'Percentage' END as Type, " +
    //                         " rgcards_gti24x7.Discount_Code_Master.Value " +
    //                         " FROM" +
    //                         " rgcards_gti24x7.SalesMaster_BothWay " +
    //                         " INNER JOIN rgcards_gti24x7.SalesDetails_BothWay ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.SalesDetails_BothWay.SBillNo) " +
    //                         " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.SalesDetails_BothWay.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id) " +
    //                         " left outer JOIN rgcards_gti24x7.Order_Pg_Details ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.Order_Pg_Details.SBillNo) " +
    //                         " left outer JOIN rgcards_gti24x7.Payment_Gateway_Master ON (rgcards_gti24x7.Order_Pg_Details.gatewayId = rgcards_gti24x7.Payment_Gateway_Master.ID)" +
    //                         " left outer JOIN rgcards_gti24x7.Discount_Code_Master ON (rgcards_gti24x7.SalesMaster_BothWay.SBillNo = rgcards_gti24x7.Discount_Code_Master.OrderId)" +
    //                         " WHERE" +
    //                         " rgcards_gti24x7.SalesMaster_BothWay.SBillNo ='" + sbillno + "'";
    //        DataTable dtQty = new DataTable();
    //        Admin_Module_Works_Select objSelect = new Admin_Module_Works_Select(dtQty, strSql, strSchema, ref strError);
    //        if (strError == null)
    //        {
    //            if (dtQty.Rows.Count > 0)
    //            {
    //                btnUpdate.Visible = true;
    //                GridView1.Visible = true;
    //                if (Convert.ToString(dtQty.Rows[0]["Value"]) != "")
    //                {

    //                    ViewState["hdDisc"] = Convert.ToString(dtQty.Rows[0]["Value"]);
    //                    ViewState["hdCode"] = Convert.ToString(dtQty.Rows[0]["Code"]);
    //                    ViewState["hdType"] = Convert.ToString(dtQty.Rows[0]["Type"]);
    //                    ViewState["TypeValue"] = Convert.ToString(dtQty.Rows[0]["TypeValue"]);

    //                }
    //                hdSales_ATOT.Value = (dtQty.Rows[0]["Sales_ATOT"] == DBNull.Value ? "0" : Convert.ToString(dtQty.Rows[0]["Sales_ATOT"]));
    //                string strPayment = "";
    //                if (Convert.ToString(dtQty.Rows[0]["Name"]) != "" && Convert.ToString(dtQty.Rows[0]["ChargePercent"]) != "" && Convert.ToString(dtQty.Rows[0]["ServiceTaxPercent"]) != "" && Convert.ToString(dtQty.Rows[0]["pgCommission"]) != "" && Convert.ToString(dtQty.Rows[0]["serviceTax"]) != "")
    //                {
    //                    strPayment = "Payment Gateway:<b>" + Convert.ToString(dtQty.Rows[0]["Name"]) + "</b> [ChargePercent:<b>" + Convert.ToString(dtQty.Rows[0]["ChargePercent"]) + " </b>, ServiceTaxPercent: <b>" + Convert.ToString(dtQty.Rows[0]["ServiceTaxPercent"]) + "</b>]<br><br> pgCommission: <b>" + Convert.ToString(dtQty.Rows[0]["pgCommission"]) + "</b> <br><br>ServiceTax: <b>" + Convert.ToString(dtQty.Rows[0]["serviceTax"] + "</b>");
    //                    if (ViewState["hdDisc"] == null && ViewState["hdCode"] == null && ViewState["hdType"] == null)
    //                    {
    //                        dvPayment.InnerHtml = strPayment;
    //                    }
    //                    else
    //                    {
    //                        dvPayment.InnerHtml = strPayment + "<br><br>Discount Code:<b>" + Convert.ToString(ViewState["hdCode"]) + "</b></b><br><br>" + "Discount Value:<b>" + Convert.ToString(ViewState["hdDisc"]) + "</b></b><br><br>" + "Discount Type:<b>" + Convert.ToString(ViewState["hdType"]) + "</b><br>" + "<br>Discount TypeValue:<b>" + Convert.ToString(ViewState["TypeValue"]) + "</b></b>";
    //                    }
    //                }
    //                else
    //                {
    //                    dvPayment.InnerHtml = "";
    //                }
    //                hdTotQty.Value = Convert.ToString(dtQty.Compute("SUM(QOS)", ""));
    //                GridView1.DataSource = dtQty;
    //                GridView1.DataBind();
    //                lblMsg.Text = "";
    //            }
    //            else
    //            {
    //                lblMsg.Text = "No Record in found";
    //                hdSales_ATOT.Value = "";
    //                dvPayment.InnerHtml = "";
    //                GridView1.Visible = false;
    //                btnUpdate.Visible = false;
    //            }
    //        }

    //    }
    //    else
    //    {
    //        lblMsg.Text = "No Record in found";
    //        hdSales_ATOT.Value = "";
    //        dvPayment.InnerHtml = "";
    //        GridView1.Visible = false;
    //        btnUpdate.Visible = false;
    //    }

    //}
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    if (GridView1.Rows.Count > 0)
    //    {
    //        lblMsg.Text = "";
    //        double sales_ATOT = 0.00;
    //        string sbillno = "";
    //        if (GetSbillNo(ref sbillno))
    //        {
    //            foreach (GridViewRow gr in GridView1.Rows)
    //            {
    //                Label RecId = (Label)gr.FindControl("lblRecId");
    //                Label Product_Id = (Label)gr.FindControl("lblProduct_Id");
    //                Label Price = (Label)gr.FindControl("lblPrice");
    //                Label DiscValue = (Label)gr.FindControl("lblValue");
    //                TextBox QOS = (TextBox)gr.FindControl("txtQOS");
    //                sales_ATOT += Convert.ToDouble(Price.Text) * Convert.ToInt32(QOS.Text);

    //                if (QOS.Text.Trim() != "0")
    //                {
    //                    if (!UpdateSaleDetails(Convert.ToInt32(QOS.Text.Trim()), Convert.ToInt32(RecId.Text), sbillno, ref  strError))
    //                    {
    //                        lblMsg.Text = strError;
    //                    }
    //                }
    //                else
    //                {
    //                    lblMsg.Text = "Quantity Cannot be 0";
    //                    return;
    //                }
    //            }
    //            if (Convert.ToString(ViewState["hdDisc"]) != "")
    //            {
    //                sales_ATOT = sales_ATOT - Convert.ToDouble(ViewState["hdDisc"]);

    //            }
    //            if (EditQty(sales_ATOT, sbillno, ref strError))
    //            {

    //            }
    //            else
    //            {
    //                lblMsg.Text = strError;
    //            }
    //            BindGV();
    //            lblMsg.Text = "Quantity Updated Successfully";
    //        }
    //    }
    //}


    //public bool EditQty(double sales_ATOT, string sbillno, ref string strError)
    //{
    //    string returnStatus = "";
    //    bool bflag = false;
    //    using (SqlConnection conn = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["DBCON"])))
    //    {
    //        try
    //        {
    //            if (conn.State == ConnectionState.Closed) { conn.Open(); }
    //            using (SqlCommand cmd = new SqlCommand(strSchema + ".spEditQuantity", conn))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                //cmd.Parameters.Add("@RecId", SqlDbType.Int).Value = Convert.ToInt32(htQty["RecId"]);
    //                cmd.Parameters.Add("@sales_ATOT", SqlDbType.Float).Value = sales_ATOT;
    //                // cmd.Parameters.Add("@QOS", SqlDbType.Int).Value = Convert.ToInt32(htQty["QOS"]);
    //                cmd.Parameters.Add("@SBillNo", SqlDbType.VarChar, 255).Value = sbillno;
    //                cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
    //                cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
    //                cmd.ExecuteNonQuery();
    //                if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
    //                {
    //                    returnStatus = "0";
    //                    strError = "Some error in procedure";
    //                    bflag = false;
    //                }
    //                else
    //                {
    //                    returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
    //                    bflag = true;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            strError = "Error!<br>" + ex.Message;
    //            bflag = false;
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //        return bflag;
    //    }
    //}
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        GridViewRow gr = (GridViewRow)e.Row;
    //        Label lblSum = (Label)gr.FindControl("lblSales_ATOT");
    //        Label TotQty = (Label)gr.FindControl("lblTotQty");
    //        if (ViewState["hdDisc"] == null)
    //        {
    //            lblSum.Text = "Total:" + hdSales_ATOT.Value;
    //        }
    //        else
    //        {
    //            lblSum.Text = "Discount: " + Convert.ToDouble(ViewState["hdDisc"]) + " Total:" + hdSales_ATOT.Value;
    //        }
    //        TotQty.Text = hdTotQty.Value;
    //    }
    //}

    //public double getpAmount(double price, int Qty)
    //{

    //    return price * Qty;
    //}

    //public bool UpdateSaleDetails(int qty, int recID, string sbillno, ref string strError)
    //{
    //    string strSQL = "update rgcards_gti24x7.salesdetails_bothway set QOS=" + qty + " where sbillno='" + sbillno + "' and recID=" + recID + "";
    //    bool flag = false;
    //    using (SqlConnection conn = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["DBCON"])))
    //    {
    //        try
    //        {
    //            conn.Open();

    //            using (SqlCommand cmd = new SqlCommand(strSQL, conn))
    //            {
    //                cmd.CommandType = CommandType.Text;
    //                cmd.CommandText = strSQL;
    //                cmd.ExecuteNonQuery();
    //                flag = true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            strError = "Error!<br>" + ex.Message;
    //            conn.Close();
    //            flag = false;
    //        }
    //        finally
    //        {
    //            //dr.Close();
    //            conn.Close();
    //        }
    //    }
    //    return flag;

    //}


    //public bool GetSbillNo(ref string sbillno)
    //{
    //    bool flag = false;
    //    if (!txtSBillNo.Text.ToLower().Trim().Replace("'", "''").Contains("gti"))
    //    {

    //        string strSQL = "select * from  [rgcards_gti24x7].salesmaster_bothway where sbillno like '%GTI" + txtSBillNo.Text.ToLower().Trim().Replace("'", "''") + "/%' ";
    //        using (SqlConnection conn = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["DBCON"])))
    //        {
    //            if (conn.State == ConnectionState.Closed) { conn.Open(); }
    //            try
    //            {
    //                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
    //                {
    //                    if (rdr.HasRows)
    //                    {
    //                        flag = true;
    //                        if (rdr.Read())
    //                        {
    //                            sbillno = Convert.ToString(rdr["sbillno"]);

    //                        }
    //                    }
    //                    else
    //                    {
    //                        flag = false;
    //                    }
    //                }
    //            }
    //            catch (SqlException ex)
    //            {
    //                flag = false;
    //            }
    //        }

    //    }
    //    else
    //    {
    //        sbillno = txtSBillNo.Text.Trim().Replace("'", "''");
    //        flag = true;
    //    }
    //    return flag;
    //} 
}
