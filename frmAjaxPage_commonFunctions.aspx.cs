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
using System.Collections.Generic;

public partial class frmAjaxPage_commonFunctions : System.Web.UI.Page
{
    DataTable dtitem = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        dtitem = (DataTable)Session["dtitem"];
        if (!IsPostBack)
        {
            if (Request.Params["mode"] != null && Request.Params["recId"] != null && Request.Params["item_id"] != null)
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
            }
            GridView1.DataSource = (DataTable)Session["dtitem"];
            GridView1.DataBind();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Image im = (Image)gr.FindControl("image_name");

                Label lblimage_name = (Label)gr.FindControl("lblimage_name");
                im.ImageUrl = "images\\" + lblimage_name.Text;
                //im.ImageUrl = ".images/" + lblid.Text;
            }

            //System.Threading.Thread.Sleep(3000);
            //for updating

            string mode = Convert.ToString(Request.Params["mode"]);
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
                            }
                        }
                    }
                    dtitem.AcceptChanges();
                }
                GridView1.DataSource = (DataTable)Session["dtitem"];
                //GridView1.DataSource = dtitem;
                GridView1.DataBind();


                foreach (GridViewRow gr in GridView1.Rows)
                {
                    Image im = (Image)gr.FindControl("image_name");

                    Label lblimage_name = (Label)gr.FindControl("lblimage_name");
                    im.ImageUrl = "images\\" + lblimage_name.Text;
                    //im.ImageUrl = ".images/" + lblid.Text;
                }


            }


        }
    }

    public double gettotal(double price, int qty)
    {
        return price * qty;
    }
}
