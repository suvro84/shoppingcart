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

public partial class frmAjaxPage : System.Web.UI.Page
{
    commonclass myobj = new commonclass();
    string strSQL = null;
    DataTable dtitem = new DataTable();
    DataView dv = null;
    System.Data.DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        string mode = Convert.ToString(Request.Params["mode"]);
        //if(mode!=null && Request.Params["pid"]!=null && Request.Params["cid"]!=null)
        //{
        //    DataTable dtitem = new DataTable();


        //}
       // string[] arr = Request.Params["strBoxIds"].ToString().Split(new char[] { ',' });
        if (mode != null && Request.Params["item_id"] != null)
        {
            DataTable mydt = new DataTable();

            strSQL = "select * from item where item_id=" + Convert.ToInt32(Request.Params["item_id"]) + " ";
            mydt = myobj.Fetchrecords(strSQL);



            //dtitem.Columns.Add("id", typeof(int));
            //dtitem.PrimaryKey = new DataColumn[] {dtitem.Columns["id"] };
            //dtitem.Columns["id"].AutoIncrement = true;
            //dtitem.Columns.Add("item_id", typeof(int));
            //dtitem.Columns.Add("image_name", typeof(string));
            //dtitem.Columns.Add("item_name",typeof(string));
            //dtitem.Columns.Add("item_cost", typeof(double));
            //dtitem.Columns.Add("qty", typeof(int));
            //dtitem.Columns.Add("tot_cost", typeof(double));
            dtitem = (DataTable)Session["dtitem"];
            //dr = dtitem.NewRow();
            //dr = dtitem.NewRow();
            if (checkDuplicate() == true)
            {
                for (int i = 0; i < mydt.Rows.Count; i++)
                {
                    dr = dtitem.NewRow();

                    dr["item_id"] = Convert.ToInt32(mydt.Rows[i]["item_id"]);
                    dr["image_name"] = mydt.Rows[i]["image_name"].ToString();
                    dr["item_name"] = mydt.Rows[i]["item_name"].ToString();
                    dr["item_cost"] = mydt.Rows[i]["item_cost"].ToString();
                    //dr["qty"] = 1;
                    dr["qty"] = 1;
                    dr["tot_cost"] = Convert.ToDouble(mydt.Rows[i]["item_cost"]) * Convert.ToInt32(dr["qty"]);
                   // dr["tot_cost"] = dr["tot_cost"];

                    dtitem.Rows.Add(dr);
                }
            }
            Response.Write("t");
            //Session["dt"] = (DataTable)dtitem;


        }
        else
        {
            Response.Write("f");
        }


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

//public bool getProductData(string pid, int cid, string item_name, double price, string item_description)
//{
//    //strSQL = "select * from ";

//}

