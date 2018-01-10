using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for commonclass
/// </summary>
public class commonclass
{
    SqlConnection con;
    SqlDataAdapter DA;
    DataSet DS;
    SqlCommand CMD;
    SqlDataReader DR;



    public commonclass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void DBopen()
    {
        con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);
    try
    {
   if(con.State == ConnectionState.Open)
   {
   con.Close();
   }
        con.Open();
    }
        catch (Exception ex)
        {
        }

    }
    public void DBClose()
    {

        con.Close();
        con.Dispose();
    }
    public DataSet ExecuteDS(string sql)
    {

        DBopen();
        try
        {

            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            return DS;
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        finally
        {
            DBClose();
        }
        return DS;
    }


    public DataTable Fetchrecords(string sql)
    {
        DBopen();
        try
        {

            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            return DS.Tables[0];
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        finally
        {
            DBClose();
        }
        return DS.Tables[0];
    }
    public void PopulateCombo(string sql, string textfiled, string valuefield, DropDownList combo)
    {
        DBopen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            //combo.Items.Add(0, "Please Select");
        
            if (DS.Tables[0].Rows.Count > 0)
            {
                combo.DataSource = DS;
                combo.DataTextField = textfiled;
                combo.DataValueField = valuefield;
                combo.DataBind();

            }
            ListItem li = new ListItem();
            li.Value = "0";
            li.Text="Please Select";
            combo.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
        }
        finally
        {

            DBClose();
        }
    }

    public void PopulateCombo1(string sql, string textfiled, string valuefield, HtmlSelect combo)
    {
        DBopen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            //combo.Items.Add(0, "Please Select");
            if (DS.Tables[0].Rows.Count > 0)
            {
                combo.DataSource = DS;
                combo.DataTextField = textfiled;
                combo.DataValueField = valuefield;
                combo.DataBind();

            }
        }
        catch (Exception ex)
        {
        }
        finally
        {

            DBClose();
        }
    }

    
    public SqlDataReader ExecuteDR(string sql,ref SqlDataReader dr)
    {
        DBopen();
        try
        {
            CMD = new SqlCommand(sql, con);
            dr = CMD.ExecuteReader();
            return dr;
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        finally
        {
            DBClose();
        }
        return dr;
    }
    public int ReturnExecute(string sql)
    {
        //int z;
        DBopen();
        try
        {
            CMD = new SqlCommand(sql, con);
            //z = CMD.ExecuteNonQuery();
            return CMD.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        finally
        {
            DBClose();
        }

        return CMD.ExecuteNonQuery();
    }
}
