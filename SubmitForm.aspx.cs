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

public partial class SubmitForm : System.Web.UI.Page
{
    commonclass objCommonClass = new commonclass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            populateddlBillingCountry();
            populateddlShippingState();

            if (Request.QueryString["senddata"] != null)
            {
                string senddata = Convert.ToString(Request.QueryString["senddata"]);
            }


        }
    }

    //protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);
    //    Hashtable htorder = new Hashtable();
    //    string billFName=Request.Form.Get("billFName").Replace("'","");
    //    htorder.Add("", billFName);
    //    string billLName = Request.Form.Get("billLName").Replace("'", "");
    //    htorder.Add("", billLName);
    //    string billAddress1 = Request.Form.Get("billAddress1").Replace("'", "");
    //    try
    //    {

    //        SqlCommand cmdorder = con.CreateCommand();

    //        cmdorder.CommandText = "stp_orderinsert";
    //        cmdorder.CommandType = CommandType.StoredProcedure;


    //        SqlParameter spcatid = cmdorder.Parameters.Add("@catid", SqlDbType.Int);
    //        //spcatid.Value = catid;
    //        spcatid.Direction = ParameterDirection.Input;
    //        cmdorder.Parameters["@catid"].Value = catid;
    //        SqlParameter spsiteid = cmdorder.Parameters.Add("@siteid", SqlDbType.Int);
    //        //spsiteid.Value = siteid;
    //        cmdorder.Parameters["@siteid"].Value = siteid;
    //        spsiteid.Direction = ParameterDirection.Input;
    //        SqlParameter spbreadcrum = cmdorder.Parameters.Add("@breadcrum", SqlDbType.VarChar, 8000);

    //        spbreadcrum.Direction = ParameterDirection.Output;
    //        con.Open();
    //        if (con.State == ConnectionState.Closed)
    //        {
    //            con.Open();
    //        }
    //        SqlDataReader drbreadcrum = cmdorder.ExecuteReader();



    //    }
    //    catch (SqlException ex)
    //    {
    //        //lblbreadcrum.Text = "Error:" + ex.Message;
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }


    //}



    public void populateddlBillingCountry()
    {
        string strsql = "select countryId,CountryName from tblBillingCountryServer order by CountryName";
        objCommonClass.PopulateCombo(strsql, "CountryName", "countryId", billCountry);

        //billCountry.Items.Insert(0, "Select");
    }
    public void populateddlShippingState()
    {
        string strsql = "select ShippingSateId,ShippingSateName from tblShippingStateMaster order by ShippingSateName";
        objCommonClass.PopulateCombo(strsql, "ShippingSateName", "ShippingSateId", shipState);


    }
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtCart = (DataTable)Session["dtitem"];
        Hashtable htUserDetails = new Hashtable();

        if (billFName.Value != "")
        {
            htUserDetails.Add("billFName", billFName.Value.Replace("'", "''"));

        }
        if (billLName.Value != "")
        {
            htUserDetails.Add("billLName", billLName.Value.Replace("'", "''"));

        }
        if (billAddress1.Value != "")
        {
            htUserDetails.Add("billAddress1", billAddress1.Value.Replace("'", "''"));
        }
        if (billAddress2.Value != "")
        {
            htUserDetails.Add("billAddress2", billAddress2.Value.Replace("'", "''"));
        }
        if (billZip.Value != "")
        {
            htUserDetails.Add("billZip", billZip.Value.Replace("'", "''"));
        }
        if (billCountry.SelectedValue != "0")
        {
            htUserDetails.Add("billCountry_id", billCountry.SelectedValue.Replace("'", "''"));
        }
        if (hdnBillStateName.Value != "")
        {
            htUserDetails.Add("billState_id", hdnBillStateName.Value.Replace("'", "''"));
        }
        if (billCity.Value != "")
        {
            htUserDetails.Add("billCity", billCity.Value.Replace("'", "''"));
        }
        if (billPhNo.Value != "")
        {
            htUserDetails.Add("billPhNo", billPhNo.Value.Replace("'", "''"));
        }

        if (billMobNo.Value != "")
        {
            htUserDetails.Add("billMobNo", billMobNo.Value.Replace("'", "''"));
        }
        if (billEmail.Value != "")
        {
            htUserDetails.Add("billEmail", billEmail.Value.Replace("'", "''"));
        }
        if (billInstructions.Value != "")
        {
            htUserDetails.Add("billInstructions", billInstructions.Value.Replace("'", "''"));
        }




        if (shipFName.Value != "")
        {
            htUserDetails.Add("shipFName", shipFName.Value.Replace("'", "''"));

        }
        if (shipLName.Value != "")
        {
            htUserDetails.Add("shipLName", shipLName.Value.Replace("'", "''"));

        }
        if (shipAddress1.Value != "")
        {
            htUserDetails.Add("shipAddress1", shipAddress1.Value.Replace("'", "''"));
        }
        if (shipAddress2.Value != "")
        {
            htUserDetails.Add("shipAddress2", shipAddress2.Value.Replace("'", "''"));
        }
        if (shipZip.Value != "")
        {
            htUserDetails.Add("shipZip", shipZip.Value.Replace("'", "''"));
        }
        if (shipCountry.Value != "")
        {
            htUserDetails.Add("shipCountry", shipCountry.Value.Replace("'", "''"));
        }
        if (shipState.SelectedValue != "0")
        {
            htUserDetails.Add("shipState_id", shipState.SelectedValue.Replace("'", "''"));
        }
        if (hdnShipCityName.Value != "")
        {
            htUserDetails.Add("shipCity_id", hdnShipCityName.Value.Replace("'", "''"));
        }
        if (shipPhNo.Value != "")
        {
            htUserDetails.Add("shipPhNo", shipPhNo.Value.Replace("'", "''"));

        }

        if (shipMobNo.Value != "")
        {
            htUserDetails.Add("shipMobNo", shipMobNo.Value.Replace("'", "''"));
        }
        if (shipEmail.Value != "")
        {
            htUserDetails.Add("shipEmail", shipEmail.Value.Replace("'", "''"));
        }
        if (shipMsg.Value != "")
        {
            htUserDetails.Add("shipMsg", shipMsg.Value.Replace("'", "''"));
        }

      
            if (Convert.ToBoolean(dtCart.Rows[0]["disc"]) == false)
            {
                htUserDetails["disc"] = false;
            }
            else
            {
                htUserDetails["disc"] = true;
            }


      

        if (Convert.ToString(dtCart.Rows[0]["disc_code"]) != null)
        {
            htUserDetails.Add("disc_code", Convert.ToString(dtCart.Rows[0]["disc_code"]).Replace("'", "''"));
          //  htUserDetails["disc_code"] = Convert.ToString(dtCart.Rows["0"]["disc_code"]);
        }
        else
        {

        }
        if (Convert.ToString(dtCart.Rows[0]["disc_amt"]) != "" || Convert.ToString(dtCart.Rows[0]["disc_amt"]) != null) 
        {
            htUserDetails.Add("disc_amt", Convert.ToString(dtCart.Rows[0]["disc_amt"]));
            //  htUserDetails["disc_code"] = Convert.ToString(dtCart.Rows["0"]["disc_code"]);
        }
        else
        {

        }


        Session["UserDetails"] = htUserDetails;
        fnorderinsert(dtCart, htUserDetails);
    }
    public void fnorderinsert(DataTable dtCart, Hashtable htUserDetails)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);

        try
        {
            SqlCommand cmdOrderinsert = con.CreateCommand();

            cmdOrderinsert.CommandText = "stp_orderinsert";
            cmdOrderinsert.CommandType = CommandType.StoredProcedure;




            SqlParameter spbillFName = cmdOrderinsert.Parameters.Add("@billFName", SqlDbType.VarChar, 500);
            spbillFName.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@billFName"].Value = htUserDetails["billFName"];

            SqlParameter spbillLName = cmdOrderinsert.Parameters.Add("@billLName", SqlDbType.VarChar, 500);
            spbillLName.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@billLName"].Value = htUserDetails["billLName"];

            SqlParameter spbillAddress1 = cmdOrderinsert.Parameters.Add("@billAddress1", SqlDbType.VarChar, 500);
            spbillAddress1.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@billAddress1"].Value = htUserDetails["billAddress1"];

            SqlParameter spbillAddress2 = cmdOrderinsert.Parameters.Add("@billAddress2", SqlDbType.VarChar, 500);
            spbillAddress2.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@billAddress2"].Value = htUserDetails["billAddress2"];


            SqlParameter spbillZip = cmdOrderinsert.Parameters.Add("@billZip", SqlDbType.VarChar);
            spbillZip.Direction = ParameterDirection.Input;
            spbillZip.Value = htUserDetails["billZip"];


            SqlParameter spbillCountry_id = cmdOrderinsert.Parameters.Add("@billCountry_id", SqlDbType.Int);
            spbillCountry_id.Direction = ParameterDirection.Input;
            spbillCountry_id.Value = htUserDetails["billCountry_id"];

            SqlParameter spbillState_id = cmdOrderinsert.Parameters.Add("@billState_id", SqlDbType.VarChar);
            spbillState_id.Direction = ParameterDirection.Input;
            spbillState_id.Value = htUserDetails["billState_id"];

            SqlParameter spbillCity = cmdOrderinsert.Parameters.Add("@billCity", SqlDbType.VarChar);
            spbillCity.Direction = ParameterDirection.Input;
            spbillCity.Value = htUserDetails["billCity"];

            SqlParameter spbillEmail = cmdOrderinsert.Parameters.Add("@billEmail", SqlDbType.VarChar);
            spbillEmail.Direction = ParameterDirection.Input;
            spbillEmail.Value = htUserDetails["billEmail"];

            SqlParameter spbillPhNo = cmdOrderinsert.Parameters.Add("@billPhNo", SqlDbType.VarChar);
            spbillPhNo.Direction = ParameterDirection.Input;
            spbillPhNo.Value = htUserDetails["billPhNo"];

            SqlParameter spbillMobNo = cmdOrderinsert.Parameters.Add("@billMobNo", SqlDbType.VarChar);
            spbillMobNo.Direction = ParameterDirection.Input;
            spbillMobNo.Value = htUserDetails["billMobNo"];

            SqlParameter spbillInstructions = cmdOrderinsert.Parameters.Add("@billInstructions", SqlDbType.VarChar, 8000);
            spbillInstructions.Direction = ParameterDirection.Input;
            spbillInstructions.Value = htUserDetails["billInstructions"];


            ////////////////

            SqlParameter spshipFName = cmdOrderinsert.Parameters.Add("@shipFName", SqlDbType.VarChar, 500);
            spshipFName.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@shipFName"].Value = htUserDetails["shipFName"];

            SqlParameter spshipLName = cmdOrderinsert.Parameters.Add("@shipLName", SqlDbType.VarChar, 500);
            spshipLName.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@shipLName"].Value = htUserDetails["shipLName"];

            SqlParameter spshipAddress1 = cmdOrderinsert.Parameters.Add("@shipAddress1", SqlDbType.VarChar, 500);
            spshipAddress1.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@shipAddress1"].Value = htUserDetails["shipAddress1"];

            SqlParameter spshipAddress2 = cmdOrderinsert.Parameters.Add("@shipAddress2", SqlDbType.VarChar, 500);
            spshipAddress2.Direction = ParameterDirection.Input;
            cmdOrderinsert.Parameters["@shipAddress2"].Value = htUserDetails["shipAddress2"];


            SqlParameter spshipZip = cmdOrderinsert.Parameters.Add("@shipZip", SqlDbType.VarChar);
            spshipZip.Direction = ParameterDirection.Input;
            spshipZip.Value = htUserDetails["shipZip"];


            SqlParameter spshipState_id = cmdOrderinsert.Parameters.Add("@shipState_id", SqlDbType.VarChar);
            spshipState_id.Direction = ParameterDirection.Input;
            spshipState_id.Value = htUserDetails["shipState_id"];

            SqlParameter spshipCity_id = cmdOrderinsert.Parameters.Add("@shipCity_id", SqlDbType.VarChar);
            spshipCity_id.Direction = ParameterDirection.Input;
            spshipCity_id.Value = htUserDetails["shipCity_id"];

            htUserDetails["shipCountry_id"] = 1;
            SqlParameter spshipCountry_id = cmdOrderinsert.Parameters.Add("@shipCountry_id", SqlDbType.VarChar);
            spshipCountry_id.Direction = ParameterDirection.Input;
            spshipCountry_id.Value = htUserDetails["shipCountry_id"];


            SqlParameter spshipEmail = cmdOrderinsert.Parameters.Add("@shipEmail", SqlDbType.VarChar);
            spshipEmail.Direction = ParameterDirection.Input;
            spshipEmail.Value = htUserDetails["shipEmail"];

            SqlParameter spshipPhNo = cmdOrderinsert.Parameters.Add("@shipPhNo", SqlDbType.VarChar);
            spshipPhNo.Direction = ParameterDirection.Input;
            spshipPhNo.Value = htUserDetails["shipPhNo"];

            SqlParameter spshipMobNo = cmdOrderinsert.Parameters.Add("@shipMobNo", SqlDbType.VarChar);
            spshipMobNo.Direction = ParameterDirection.Input;
            spshipMobNo.Value = htUserDetails["shipMobNo"];

            SqlParameter spshipMsg = cmdOrderinsert.Parameters.Add("@shipMsg", SqlDbType.VarChar);
            spshipMsg.Direction = ParameterDirection.Input;
            spshipMsg.Value = htUserDetails["shipMsg"];

            SqlParameter spdateofdelivery = cmdOrderinsert.Parameters.Add("@dateofdelivery", SqlDbType.VarChar, 500);
            spdateofdelivery.Direction = ParameterDirection.Input;
            spdateofdelivery.Value = Convert.ToDateTime(Request.Form.Get("shipDate")).ToString("MM/dd/yyyy");

            SqlParameter spipaddress = cmdOrderinsert.Parameters.Add("@ipaddress", SqlDbType.VarChar);
            spipaddress.Direction = ParameterDirection.Input;
            spipaddress.Value = Request.UserHostAddress.ToString();

            SqlParameter sppoid = cmdOrderinsert.Parameters.Add("@poid", SqlDbType.Int);
            sppoid.Direction = ParameterDirection.Input;
            sppoid.Value = 0;

            SqlParameter sppgid = cmdOrderinsert.Parameters.Add("@pgid", SqlDbType.Int);
            sppgid.Direction = ParameterDirection.Input;
            sppgid.Value = 0;


            //SqlParameter spSiteID = cmdOrderinsert.Parameters.Add("@SiteID", SqlDbType.Int);
            //spSiteID.Direction = ParameterDirection.Input;
            //spSiteID.Value = 0;


            string nvarchardetails = "";
            for (int i = 0; i < dtCart.Rows.Count; i++)
            {
                nvarchardetails += dtCart.Rows[i]["pid"].ToString() + "," + dtCart.Rows[i]["qty"].ToString() + "|";
            }

            SqlParameter spnvarchardetails = cmdOrderinsert.Parameters.Add("@nvarchardetails", SqlDbType.VarChar);
            spnvarchardetails.Direction = ParameterDirection.Input;
            spnvarchardetails.Value = nvarchardetails;


            SqlParameter spdisc = cmdOrderinsert.Parameters.Add("@disc", SqlDbType.Bit);
            spdisc.Direction = ParameterDirection.Input;
            spdisc.Value = htUserDetails["disc"];

           

            SqlParameter spdisc_code = cmdOrderinsert.Parameters.Add("@disc_code", SqlDbType.VarChar);
            spdisc_code.Direction = ParameterDirection.Input;
            spdisc_code.Value = htUserDetails["disc_code"];

            //SqlParameter spdisc_amt = cmdOrderinsert.Parameters.Add("@disc_amt", SqlDbType.Decimal);
            //spdisc_amt.Direction = ParameterDirection.Input;
            //spdisc_amt.Value = Convert.ToDouble(htUserDetails["disc_amt"]);

            double sales_aot = 0.00;
            if (gettotal(dtCart, ref sales_aot))
            {
                SqlParameter spsales_aot = cmdOrderinsert.Parameters.Add("@sales_aot", SqlDbType.Decimal);
                spsales_aot.Direction = ParameterDirection.Input;
                cmdOrderinsert.Parameters["@sales_aot"].Value = sales_aot;
            }
            con.Open();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string[] sbillno_tot = Convert.ToString(cmdOrderinsert.ExecuteScalar()).Split(new char[] { ',' });
            if (sbillno_tot.Length > 0)
            {
                //htUserDetails["sbillno"] = sbillno_tot[0];
                //htUserDetails["@sales_aot"] = sbillno_tot[1];
                htUserDetails.Add("sbillno", sbillno_tot[0].Replace("'", "''"));
                htUserDetails.Add("sales_aot", sbillno_tot[1].Replace("'", "''"));
            }
            Response.Redirect("PaymentOption.aspx?sbillno=" + htUserDetails["sbillno"]);


        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }



    }
    public bool gettotal(DataTable dtCart, ref double total)
    {
        bool bflag = false;
        if (dtCart.Rows.Count > 0)
        {
            for (int i = 0; i < dtCart.Rows.Count; i++)
            {

                total += Convert.ToDouble(dtCart.Rows[i]["item_cost"]) * Convert.ToInt32(dtCart.Rows[i]["qty"]);
                bflag = true;
            }
        }
        return bflag;
    }
}
