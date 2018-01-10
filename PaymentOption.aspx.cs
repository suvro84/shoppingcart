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

public partial class PaymentOption : System.Web.UI.Page
{
    string strError = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getPaymentOption();
        }
    }


    public void getPaymentOption()
    {
        DataTable dtPoption = new DataTable();
        commonclass objPo = new commonclass();

        string strSQL = "select * from tblPaymentOptionMaster";
        dtPoption = objPo.Fetchrecords(strSQL);
        rptPo.DataSource = dtPoption;
        rptPo.DataBind();


    }
    protected void btnSubmit_ServerClick(object sender, ImageClickEventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            Hashtable htUserDetails = (Hashtable)Session["UserDetails"];
            //foreach (RepeaterItem rpt in rptPo.Items)
            //{
            //    RadioButton control = (RadioButton)rpt.FindControl("radPayOpt");
            //    Label lblPOptionName = (Label)rpt.FindControl("lblPOptionName");

            //    if (control.Checked)
            //    {
            //       // checkedButton = control;
            //        htUserDetails.Add("PoId", hdnPoId.Value);
            //        htUserDetails.Add("PoName", lblPOptionName.Text);

            //        break;
            //    }


            //}
            htUserDetails.Add("PoId", hdnPoId.Value);
            htUserDetails.Add("PoName", hdnPoName.Value);

            htUserDetails.Add("PgId", hdnPgId.Value);
            //htUserDetails.Add("PgName", "PayPal");
            Session["UserDetails"] = htUserDetails;
            if (Execute(ref strError))
            {
                Response.Redirect("OrderDisplay.aspx");
            }
        }
    }

    public bool Execute(ref string strError)
    {
        string sbillno = "";
        if (Convert.ToString(Request.Params["sbillno"]) != "")
        {
            sbillno = Convert.ToString(Request.Params["sbillno"]);
        }

        bool bflag = false;
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sql = "insert into tblOrder_Pg_Details(sbillno,pgid,poid)values('" + sbillno + "'," + hdnPgId.Value + "," + hdnPoId.Value + ")";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    bflag = true;

                }

            }
        }
        catch (Exception ex)
        {
            bflag = true;
        }
        return bflag;

    }
}
