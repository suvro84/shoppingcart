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

public partial class ajaxSubmitForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        commonclass objstate = new commonclass();
        string mode = Request.Params["mode"].ToString();
       
        //string strsql = "";
        if (mode == "1" && Request.Params["countryId"] != null)
        {
            if (Convert.ToString(Request.Params["countryId"]) != "Select")
            {
                string strsql = "select BillingStateId,BillingStateName from tblBillingSateServer where BillingCountryId=" + Convert.ToInt32(Request.Params["countryId"]) + "";
                objstate.PopulateCombo(strsql, "BillingStateName", "BillingStateId", billState);
                //billState.Items.Insert(0, "Select");
                shipState.Visible = false;
                Response.Write("1");
                Response.Write("~");
            }

        }
      


        else if (mode == "2" && Request.Params["stateId"] != null)
        {
            shipState.Visible = true;
            billState.Visible = false;
            string strsql = "select ShippingCityId,ShippingCityName from tblShippingCityMaster where ShippingStateId=" + Convert.ToInt32(Request.Params["stateId"]) + "";
            objstate.PopulateCombo(strsql, "ShippingCityName", "ShippingCityId", shipState);
            //shipState.Items.Insert(0, "Select");
            Response.Write("1");
            Response.Write("~");

        }
        else
        {
            Response.Write("0");
            Response.Write("~");

        }
    }
}
