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

public partial class image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {




    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString());

        try
        {



            SqlCommand cmd = con.CreateCommand();
            cmd = con.CreateCommand();
            cmd.CommandText = "[stp_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter sppackage_cost = cmd.Parameters.Add("@package_cost", SqlDbType.Float);

            sppackage_cost.Direction = ParameterDirection.Input;
            sppackage_cost.Value = txtpackage_cost.Text.Replace("'", "''");


            SqlParameter sppackage_name = cmd.Parameters.Add("@package_name", SqlDbType.NVarChar);
            sppackage_name.Direction = ParameterDirection.Input;
            sppackage_name.Value = txtpackage_name.Text.Replace("'", "''");


            SqlParameter spreturn_status = cmd.Parameters.Add("@return_status", SqlDbType.Int);

            spreturn_status.Direction = ParameterDirection.Output;


            con.Open();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            int return_status = Convert.ToInt32(cmd.ExecuteScalar());
            if (return_status == 0)
            {
                lblMsg.Text = "error:";
            }
            else
            {
                lblMsg.Text = "successfull insertion with emp_id=" + return_status;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        hdnids.Value = "1|2|3|";
        string[] strids = hdnids.Value.Split(new char[] { '|' });
        for (int i = 0; i < hdnids.Value.Length; i++)
        {
        
        }
        //if (strids.Length > 0)
       

       string ddlselect= Request.Form.Get("ddl1");

    }
}
