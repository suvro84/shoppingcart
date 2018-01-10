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

public partial class uc_subCat : System.Web.UI.UserControl
{
    public string parent_Category_Name="";

    protected void Page_Load(object sender, EventArgs e)
    {
        commonclass objcat = new commonclass();
        string strsql = "";
        int Parent_Category_id=0;
        string parent_Category_Name = string.Empty;
      string  Category_Id = "";
        if (Request.QueryString["catid"] != null)
        {
            Category_Id = Request.QueryString["catid"].ToString(); 
        }


        if (Request.QueryString["catid"] != null)
        {
            strsql = "select Category_id,Category_Name from tblItemCategory_Web_Server where Parent_Category_id=" + Request.QueryString["catid"] + "";

            DataTable dtcat = objcat.Fetchrecords(strsql);

            rptSubcatname.DataSource = dtcat;
            rptSubcatname.DataBind();

            //DataView subname = new DataView(dtcat);
            //subname.RowFilter = "Category_id='" + Category_Id + "'";
           
             strsql = "select Category_Id,Category_Name from tblItemCategory_Web_Server where Category_id=(select Parent_Category_id from tblItemCategory_Web_Server where Category_Id=" + Request.QueryString["catid"] + ")";

            
            //  dr = objcat.ExecuteDR(strsql,ref dr);
             DataTable dtParent_Category_id = objcat.Fetchrecords(strsql);

             if (dtParent_Category_id.Rows.Count>0)
            {

                Parent_Category_id = Convert.ToInt32(dtParent_Category_id.Rows[0]["Category_Id"]);
                parent_Category_Name = Convert.ToString(dtParent_Category_id.Rows[0]["Category_Name"]); 
            }

            if (Parent_Category_id != 0)
            {
               // DataTable dtsubcat = objcat.Fetchrecords(strsql);

                strsql = "select Category_Id,Category_Name from tblItemCategory_Web_Server where Parent_Category_id=" + Parent_Category_id + " and Category_Id!=" + Request.QueryString["catid"] + "";
                DataTable dtsubcat = objcat.Fetchrecords(strsql);
              //  rptSubcatname.HeaderTemplate.ToString() = parent_Category_Name;


                rptSubSubcatname.DataSource = dtsubcat;
                rptSubSubcatname.DataBind();

                foreach (RepeaterItem repeatItem in rptSubSubcatname.Items)
                {
                    // if condition to add HeaderTemplate Dynamically only Once
                    if (repeatItem.ItemIndex == 0)
                    {
                        RepeaterItem headerItem = new RepeaterItem(repeatItem.ItemIndex, ListItemType.Header);
                        HtmlGenericControl hTag = new HtmlGenericControl("h4");
                        hTag.InnerHtml = parent_Category_Name;
                        repeatItem.Controls.Add(hTag);
                    }
                }
                //rptSubSubcatname.DataSource = dtsubcat;
                //rptSubSubcatname.DataBind();

            }


        }
        else
        {
        
        }
    }
}
