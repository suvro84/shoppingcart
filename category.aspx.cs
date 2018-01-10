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

public partial class category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int catid = 0;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);
        //int siteid = Convert.ToInt32(Application["siteid"]);
        int siteid = 1;
        if (Request.QueryString["catid"] != null)
        {
             catid = Convert.ToInt32(Request.QueryString["catid"]);
        }
        try
        {
            //con.Open();
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}
            SqlCommand cmdbreadcrum = con.CreateCommand();
           
            cmdbreadcrum.CommandText = "stp_breadcrum_new";
            cmdbreadcrum.CommandType = CommandType.StoredProcedure;


            SqlParameter spcatid = cmdbreadcrum.Parameters.Add("@catid", SqlDbType.Int);
            //spcatid.Value = catid;
            spcatid.Direction = ParameterDirection.Input;
            cmdbreadcrum.Parameters["@catid"].Value = catid;
            SqlParameter spsiteid = cmdbreadcrum.Parameters.Add("@siteid", SqlDbType.Int);
            //spsiteid.Value = siteid;
            cmdbreadcrum.Parameters["@siteid"].Value = siteid;
            spsiteid.Direction = ParameterDirection.Input;
            SqlParameter spbreadcrum = cmdbreadcrum.Parameters.Add("@breadcrum", SqlDbType.VarChar, 8000);

            spbreadcrum.Direction = ParameterDirection.Output;
            con.Open();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader drbreadcrum = cmdbreadcrum.ExecuteReader();
          

            //if (drbreadcrum.HasRows)
            //{
                //while (drbreadcrum.Read())
                //{
                    lblbreadcrum.Text = Convert.ToString(spbreadcrum.Value);
                //}
            //}
          //  drbreadcrum.Close();
        }
        catch (SqlException ex)
        {
            lblbreadcrum.Text = "Error:" + ex.Message;
        }
        finally
        {
            con.Close();
        }

    }
}