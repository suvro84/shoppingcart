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

public partial class frmAjaxCommonFunctions : System.Web.UI.Page
{
    commonclass myobj = new commonclass();
    string strSQL = null;
    DataTable dtitem = new DataTable();
    DataView dv = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        dtitem = (DataTable)Session["dtitem"];

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

            Session["dtitem"] = dtitem;

            //dtitem.AcceptChanges();
            Response.Write("1");
            Response.Write("~");
            //bindgv();
            //GridView gv = new GridView();
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
            Response.Write("0");
            Response.Write("~");

        }
    }

}
