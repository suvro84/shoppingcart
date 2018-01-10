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

public partial class c_form : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       //sbilno coming from query string     
        //get prodid from salesmasterbothway against sbill bo,
        //get catid from itemcategoryrelation 
        //get proddetails (iemname,price,image) from item table to form cart 

        //get poid,pgid,rank from tblorder_Popg_Details against sbillno
        //get user  details billing details and shipping details against sbillno from tblBillingDetailsBothway and tblSalesDetailsBothway
    //and store userdetails in hashtable in session 
    
    }
}
