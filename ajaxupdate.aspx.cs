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
using System.Data.SqlClient;
using System.Text;

public partial class ajaxupdate : System.Web.UI.Page
{
    commonclass myobj = new commonclass();
    string strSQL = null;
    DataTable dtitem = new DataTable();
    DataView dv = null;
    System.Data.DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        // DataTable dtitem = new DataTable();
        dtitem = (DataTable)Session["dtitem"];
        string mode = Convert.ToString(Request.Params["mode"]);
        bool flagDiscount = false;
        if (dtitem.Rows.Count == 0)
        {
            Session["flagDiscount"] = false;
        }
        if (mode == "3")
        {
            //List<string> arr = new List<string>();

            if (Request.Params["strBoxIds"].ToString().Length > 0)
            {
                // arr.AddRange(Request.Params["strBoxIds"].ToString().Split(','));

                //if (arr.Length!= 0)
                //{
                //    //arr.RemoveAt(arr.Count - 1);
                //    foreach (string str in arr)
                //    {
                //        string item_id = str.Split('#')[0];
                //        string value = str.Split('#')[1];
                //        foreach (DataRow dr in dtitem.Rows)
                //        {
                //            if (dr["item_id"].ToString().Equals(item_id))
                //                //dr["item_cost"] = Convert.ToDouble(dr["item_cost"]) * Convert.ToInt32(value);
                //                dr["tot_cost"] = Convert.ToDouble(dr["item_cost"]) * Convert.ToInt32(value);
                //        }
                //    }
                //    dtitem.AcceptChanges();
                //}

                string[] arr = Request.Params["strBoxIds"].ToString().Split(new char[] { ',' });
                //  string[] arr = Request.QueryString["strBoxIds"].ToString().Split(new char[] { ',' });
                if (arr.Length != 0)
                {
                    for (int j = 0; j < arr.Length - 1; j++)
                    {
                        string[] arr1 = arr[j].Split(new char[] { '#' });
                        string item_id = arr1[0];
                        string value = arr1[1];

                        foreach (DataRow dr in dtitem.Rows)
                        {
                            //dr["tot_cost"] = Convert.ToDouble(dr["item_cost"]) * Convert.ToInt32(value);
                            //i++;
                            if (dr["item_id"].ToString().Equals(item_id))
                                //dr["item_cost"] = Convert.ToDouble(dr["item_cost"]) * Convert.ToInt32(value);
                                dr["tot_cost"] = Convert.ToDouble(dr["item_cost"]) * Convert.ToInt32(value);
                            dr["qty"] = Convert.ToInt32(value);

                        }
                    }
                }
                dtitem.AcceptChanges();
            }
            bindGV();

            loadTotal();
        }
        if (mode == "4")
        {
            if (Request.Params["discCode"] != null && Request.Params["SiteId"] != null)
            {
                double tot_cost = calculate_tot(dtitem);
                //  objtot = dtitem.Compute("Sum(tot_cost)", "");
                //Convert.ToDouble(objdisc).ToString("00.00");
                object objtot;
                double disc_amt = 0.00;
                if (check_discount(dtitem) == true)
                {
                    Response.Write("1");
                    Response.Write("~");
                    bindGV();
                    loadTotal();
                }
                else
                {
                    Response.Write("2");
                    Response.Write("~");
                    Response.Write("Please verify the discount code");
                    Response.Write("~");
                    bindGV();
                    loadTotal();

                }


                // tot_cost = (100 - disc_amt) / 100 * tot_cost;

            }
            else
            {

                Response.Write("0");
                Response.Write("~");

            }
        }
       




        //for delete

        if (mode == "2")
        {
            if (Request.Params["mode"] != null && Request.Params["recId"] != null && Request.Params["item_id"] != null)
            {
                if (dtitem != null)
                {
                    if (dtitem.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtitem.Rows.Count; i++)
                        {
                            DataRow dr = dtitem.Rows[i];
                            if (Convert.ToInt32(dr["recId"]) == Convert.ToInt32(Request.Params["recId"]))
                            {
                                //dr.Delete();
                                dtitem.Rows.Remove(dr);
                            }
                        }
                    }


                    Response.Write("1");
                    Response.Write("~");

                    bindGV();
                    loadTotal();
                }
                else
                {
                    Response.Write("2");
                    Response.Write("~");
                    show_noitem();
                }
            }

            else
            {
                Response.Write("0");
                Response.Write("~");

            }

            

        }


        //add to cart

        if (mode == "1")
        {
            if (mode != null && Request.Params["item_id"] != null)
            {
                DataTable mydt = new DataTable();

                strSQL = "select * from item where item_id=" + Convert.ToInt32(Request.Params["item_id"]) + " ";
                mydt = myobj.Fetchrecords(strSQL);



                if (checkDuplicate() == true)
                {
                    for (int i = 0; i < mydt.Rows.Count; i++)
                    {
                        dr = dtitem.NewRow();

                        dr["item_id"] = Convert.ToInt32(mydt.Rows[i]["item_id"]);
                        dr["pid"] = Convert.ToString(mydt.Rows[i]["pid"]);
                        dr["cid"] = Convert.ToInt32(mydt.Rows[i]["item_id"]);

                        dr["image_name"] = mydt.Rows[i]["image_name"].ToString();
                        dr["item_name"] = mydt.Rows[i]["item_name"].ToString();
                        dr["item_cost"] = mydt.Rows[i]["item_cost"].ToString();
                        //dr["qty"] = 1;
                        dr["qty"] = 1;
                        dr["tot_cost"] = Convert.ToDouble(mydt.Rows[i]["item_cost"]) * Convert.ToInt32(dr["qty"]);
                        // dr["tot_cost"] = dr["tot_cost"];
                        dr["disc"] = false;
                        dr["disc_code"] = " ";
                        dr["disc_amt"] = DBNull.Value;
                        //flagDiscount = false;
                        dtitem.Rows.Add(dr);
                    }
                }
              
               

                 flagDiscount = Convert.ToBoolean(Session["flagDiscount"]);
                if (flagDiscount == true)
                {
                    
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        //disc_amt = Convert.ToDouble(dr["disc_amt"]);
                        dr["disc"] = Convert.ToBoolean(dtitem.Rows[0]["disc"]);
                        dr["disc_code"] = dtitem.Rows[0]["disc_code"].ToString();
                        dr["disc_amt"] = Convert.ToDouble(dtitem.Rows[0]["disc_amt"]);
                        //dr["tot_cost"] = (100 - disc_amt) / 100 * tot_cost;
                        //  dr["tot_cost"] = (100 - Convert.ToDouble(dr["disc_amt"])) / 100 * tot_cost;
                    }
                }
                Response.Write("1");
                Response.Write("~");
                //Session["dt"] = (DataTable)dtitem;


            }
            else
            {
                Response.Write("0");
                Response.Write("~");
            }
        }
       











    }

    public bool check_discount(DataTable dtitem)
    {
        string strSql = "select disc_code,disc_amt,disc from tblDiscountMaster where SiteId='" + Convert.ToString(Request.Params["SiteId"]) + "' and disc_code='" + Convert.ToString(Request.Params["discCode"]) + "'";
        commonclass objdisc = new commonclass();

        DataTable dtdisc = objdisc.Fetchrecords(strSql);
        if (dtdisc.Rows.Count > 0)
        {
            foreach (DataRow dr in dtitem.Rows)
            {

                //disc_amt = Convert.ToDouble(dr["disc_amt"]);
                dr["disc"] = true;
                dr["disc_code"] = Convert.ToString(dtdisc.Rows[0]["disc_code"]);
                dr["disc_amt"] = Convert.ToDouble(dtdisc.Rows[0]["disc_amt"]);
                //dr["tot_cost"] = (100 - disc_amt) / 100 * tot_cost;
                //  dr["tot_cost"] = (100 - Convert.ToDouble(dr["disc_amt"])) / 100 * tot_cost;
            }
            Session["flagDiscount"] = true;
            return true;
           
        }
        else
        {
            //Response.Write("2");
            //Response.Write("~");
            //Response.Write("Please verify the discount code");
            Session["flagDiscount"] = false;
            return false;
           
        }
    }
    public void loadTotal()
    {
        // DataTable dtitem = new DataTable();
        object ob;
        dtitem = (DataTable)Session["dtitem"];
        bool bldisc = false;
        if (dtitem != null)
        {
            if (dtitem.Rows.Count > 0)
            {
                foreach (DataRow dr in dtitem.Rows)
                {
                    if (dr["disc"] != DBNull.Value)
                    {
                        bldisc = Convert.ToBoolean(dr["disc"]);
                    }

                }
                if (bldisc == false)
                {

                    double tot_cost = calculate_tot(dtitem);
                    ob = dtitem.Compute("Sum(tot_cost)", "");
                    lblSubTot.Text = tot_cost.ToString("00.00");
                    lblGrndTot.Text = tot_cost.ToString("00.00");
                    lblSavings.Text = "00.00";
                }
                else
                {
                    ob = dtitem.Compute("Sum(tot_cost)", "");

                    double tot_cost = calculate_tot(dtitem);
                    lblSubTot.Text = tot_cost.ToString("00.00");
                    //lblSubTot.Text = tot_cost.ToString("00.00");
                    lblGrndTot.Text = Convert.ToString((100 - Convert.ToDouble(dtitem.Rows[0]["disc_amt"])) / 100 * tot_cost);
                    lblSavings.Text = Convert.ToString(Convert.ToDouble(lblSubTot.Text) - Convert.ToDouble(lblGrndTot.Text));
                    //dvdiscount.Style["Display"] = "";
                    //dvdiscount.InnerHtml = "";
                    dvdiscount.InnerHtml = " " + Convert.ToString(dtitem.Rows[0]["disc_code"]);

                }
            }

        }
    }
    public void bindGV()
    {
        if (Session["dtitem"] != null)
        {
            GridView1.DataSource = (DataTable)Session["dtitem"];
            GridView1.DataBind();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Image im = (Image)gr.FindControl("image_name");

                Label lblimage_name = (Label)gr.FindControl("lblimage_name");
                im.ImageUrl = "images\\" + lblimage_name.Text;
                //im.ImageUrl = ".images/" + lblid.Text;
            }
        }
        else
        {
            Response.Write("There is no items in the cart");
        }
    }

    public void show_noitem()
    {
        StringBuilder sboutput = new StringBuilder();
        sboutput.Append("<table cellscpacing=\"0\"cellpadding=\"0\" width=\"100%\"><tr><td colspan=\"2\"><a href=\"default.aspx\">continue shopping</td></tr>");

    }
    double calculate_tot(DataTable dtitem)
    {
        double tot_cost = 0.00;
        for (int i = 0; i < dtitem.Rows.Count; i++)
        {
            int qty = Convert.ToInt32(dtitem.Rows[i]["qty"]);
            double item_cost = Convert.ToDouble(dtitem.Rows[i]["item_cost"]);
            tot_cost = tot_cost + qty * item_cost;
        }
        return tot_cost;
    }
    public double gettotal(double price, int qty)
    {
        return price * qty;
    }
    public bool checkDuplicate()
    {
        bool bflag = true;

        dtitem = (DataTable)Session["dtitem"];
        for (int i = 0; i < dtitem.Rows.Count; i++)
        {
            if (Convert.ToInt32(dtitem.Rows[i]["item_id"]) == Convert.ToInt32(Request.Params["item_id"]))
            {
                //dr = dtitem.NewRow();
                //foreach (DataRow dr in dtitem.Rows)
                //{
                //dr["item_id"] = Convert.ToInt32(dtitem.Rows[i]["item_id"]);
                //dr["image_name"] = dtitem.Rows[i]["image_name"].ToString();
                //dr["item_name"] = dtitem.Rows[i]["item_name"].ToString();
                //dr["item_cost"] = dtitem.Rows[i]["item_cost"].ToString();
                dtitem.Rows[i]["qty"] = Convert.ToInt32(dtitem.Rows[i]["qty"]) + 1;
                dtitem.Rows[i]["tot_cost"] = Convert.ToInt32(dtitem.Rows[i]["item_cost"]) * Convert.ToInt32(dtitem.Rows[i]["qty"]);
                //dtitem.Rows.Add(dr);
                bflag = false;
                //}
            }

        }
        return bflag;
    }
}
