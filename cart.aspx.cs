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

public partial class cart : System.Web.UI.Page
{
    object ob;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindgv();
            //loadTotal();
        }

        
       
    }
    //public void loadTotal()
    //{
    //    DataTable dtitem = new DataTable();
    //    dtitem = (DataTable)Session["dtitem"];
    //    bool bldisc = false;
    //    if (dtitem.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in dtitem.Rows)
    //        {
    //            if (dr["disc"] != DBNull.Value)
    //            {
    //                bldisc = Convert.ToBoolean(dr["disc"]);
    //            }

    //        }
    //    }
    //    if (bldisc == false)
    //    {
    //        ob = dtitem.Compute("Sum(tot_cost)", "");
    //        lblSubTot.Text = Convert.ToDouble(ob).ToString("00.00");
    //        lblGrndTot.Text = Convert.ToDouble(ob).ToString("00.00");
    //        lblSavings.Text = "00.00";
    //    }
    //    else
    //    {
    //        ob = dtitem.Compute("Sum(tot_cost)", "");
    //        lblSubTot.Text = Convert.ToDouble(ob).ToString("00.00");
    //        lblGrndTot.Text = Convert.ToString((100 - Convert.ToDouble(dtitem.Rows[0]["disc_amt"])) / 100 * Convert.ToDouble(lblSubTot.Text));
    //        lblSavings.Text = Convert.ToString(Convert.ToDouble(lblSubTot.Text) - Convert.ToDouble(lblGrndTot.Text));
    //        //dvdiscount.Style["Display"] = "";
    //        //dvdiscount.InnerHtml = "";
    //        dvdiscount.InnerHtml = " " + Convert.ToString(dtitem.Rows[0]["disc_code"]);

    //    }
    //}
    public void loadTotal()
    {
         DataTable dtitem = new DataTable();
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
    double calculate_tot(DataTable dtitem)
    {
        double tot_cost = 0.00;
        for (int i = 0; i < dtitem.Rows.Count; i++)
        {
            int qty = Convert.ToInt32(dtitem.Rows[i]["qty"]);
            double item_cost = Convert.ToDouble(dtitem.Rows[i]["item_cost"]);
            //tot_cost = +qty * item_cost;
            tot_cost = tot_cost + qty * item_cost;
        }
        return tot_cost;
    }
    public void bindgv()
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
            loadTotal();
        }
       
    }
    public double gettotal(double price, int qty)
    {
        return price * qty;
    }
}
