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

public partial class CreateProductXLS : System.Web.UI.Page
{
    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);
    Admin_Module_Works_Select myobj = new Admin_Module_Works_Select();
    string strSql = null;
    string strError = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillSiteddl();
            if (Convert.ToString(HttpContext.Current.Request.Params["catId"]) != null && Convert.ToString(HttpContext.Current.Request.Params["SiteId"]) != null)
            {
                //string strSQL = " SELECT DISTINCT " +
                //             " rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id," +
                //             " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
                //             " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
                //             " rgcards_gti24x7.ItemCategory_Web_Server.Category_Name," +
                //             " rgcards_gti24x7.ItemCategory_Web_Server.Category_Id," +
                //             " 'http://www.giftstoindia24x7.com/Gifts.aspx?proid='+rgcards_gti24x7.ItemMaster_Server.Product_Id+'&CatId='+convert(varchar(250),rgcards_gti24x7.ItemCategory_Web_Server.Category_Id) as url" +
                //             " FROM " +
                //             " rgcards_gti24x7.ItemCategoryRelation_Web_Server " +
                //             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
                //             " INNER JOIN rgcards_gti24x7.ItemCategory_Web_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id = rgcards_gti24x7.ItemCategory_Web_Server.Category_Id)" +
                //             " INNER JOIN rgcards_gti24x7.SiteCatgory_Web_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id = rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id)" +
                //             " WHERE " +
                //             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id = '" + Convert.ToString(HttpContext.Current.Request.Params["catId"]) + "') AND " +
                //             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = '1') AND " +
                //             " ((rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL) OR " +
                //             " (rgcards_gti24x7.ItemMaster_Server.Item_colour = '0') OR " +
                //             " (rgcards_gti24x7.ItemMaster_Server.Item_colour = '1'))" +
                //             " and  rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id='" + Convert.ToString(HttpContext.Current.Request.Params["SiteId"]) + "'";

                //  string strSQL = "SELECT DISTINCT " +
                //  "" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
                //  "" + strSchema + ".ItemMaster_Server.Item_Name, " +
                //  "" + strSchema + ".ItemMaster_Server.Item_Price, " +
                //  "" + strSchema + ".ItemCategory_Web_Server.Category_Name," +
                //  " " + strSchema + ".SiteCatgory_Web_Server.Category_Id," +
                //  " 'http://www.giftstoindia24x7.com/Gifts.aspx?proid='+rgcards_gti24x7.ItemMaster_Server.Product_Id+'&CatId='+convert(varchar(250),rgcards_gti24x7.ItemCategory_Web_Server.Category_Id) as url" +
                //" FROM " +
                //  "" + strSchema + ".ItemCategoryRelation_Web_Server " +
                //  "INNER JOIN " + strSchema + ".ItemMaster_Server ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id = " + strSchema + ".ItemMaster_Server.Product_Id) " +
                //  "INNER JOIN " + strSchema + ".SiteCatgory_Web_Server ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id = " + strSchema + ".SiteCatgory_Web_Server.Category_Id) " +
                //  "INNER JOIN " + strSchema + ".Category_Category_Relation ON (" + strSchema + ".SiteCatgory_Web_Server.Category_Id = " + strSchema + ".Category_Category_Relation.FetchCategoryId)" +
                //  "INNER JOIN rgcards_gti24x7.ItemCategory_Web_Server ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id = " + strSchema + ".ItemCategory_Web_Server.Category_Id) " +
                //"WHERE " +
                //  "(" + strSchema + ".Category_Category_Relation.BaseCategoryId =" + Convert.ToString(HttpContext.Current.Request.Params["catId"]) + ") AND " +
                //  "(" + strSchema + ".SiteCatgory_Web_Server.Site_Id =" + Convert.ToString(HttpContext.Current.Request.Params["SiteId"]) + ") AND " +
                //  "((" + strSchema + ".ItemMaster_Server.Item_colour IS NULL) OR " +
                //  "(" + strSchema + ".ItemMaster_Server.Item_colour = '0') OR " +
                //  "(" + strSchema + ".ItemMaster_Server.Item_colour = '1')) " +
                //  " and (rgcards_gti24x7.ItemMaster_Server.Record_Status = '1')" +
                //"ORDER BY " +
                //  " " + strSchema + ".ItemMaster_Server.Item_Price DESC ";


                string strSQL = "SELECT DISTINCT " +
                             "" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
                             "" + strSchema + ".ItemMaster_Server.Item_Name, " +
                             "" + strSchema + ".ItemMaster_Server.Item_Price, " +
                             "" + strSchema + ".ItemCategory_Web_Server.Category_Name," +
                             " " + Convert.ToString(HttpContext.Current.Request.Params["catId"]) + "," +
                             "'http://www.'+LOWER(rgcards_gti24x7.POS_BothWay.POS_OName)+'/Gifts.aspx?proid='+rgcards_gti24x7.ItemMaster_Server.Product_Id+'&CatId=" + Convert.ToString(HttpContext.Current.Request.Params["catId"]) + "' as url " +
                             " FROM " +
                             "" + strSchema + ".ItemCategoryRelation_Web_Server " +
                             "INNER JOIN " + strSchema + ".ItemMaster_Server ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id = " + strSchema + ".ItemMaster_Server.Product_Id) " +
                             "INNER JOIN " + strSchema + ".SiteCatgory_Web_Server ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id = " + strSchema + ".SiteCatgory_Web_Server.Category_Id) " +
                             "INNER JOIN " + strSchema + ".Category_Category_Relation ON (" + strSchema + ".SiteCatgory_Web_Server.Category_Id = " + strSchema + ".Category_Category_Relation.FetchCategoryId)" +
                             "INNER JOIN rgcards_gti24x7.ItemCategory_Web_Server ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id = " + strSchema + ".ItemCategory_Web_Server.Category_Id) " +
                             " INNER JOIN rgcards_gti24x7.POS_BothWay ON (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = rgcards_gti24x7.POS_BothWay.POS_Id) " +
                             "WHERE " +
                             "(" + strSchema + ".Category_Category_Relation.BaseCategoryId =" + Convert.ToString(HttpContext.Current.Request.Params["catId"]) + ") AND " +
                             "(" + strSchema + ".SiteCatgory_Web_Server.Site_Id =" + Convert.ToString(HttpContext.Current.Request.Params["SiteId"]) + ") AND " +
                             "((" + strSchema + ".ItemMaster_Server.Item_colour IS NULL) OR " +
                             "(" + strSchema + ".ItemMaster_Server.Item_colour = '0') OR " +
                             "(" + strSchema + ".ItemMaster_Server.Item_colour = '1')) " +
                             " and (rgcards_gti24x7.ItemMaster_Server.Record_Status = '1')" +
                           "ORDER BY " +
                             " " + strSchema + ".ItemMaster_Server.Item_Price DESC ";



                string strError = "";
                DataTable dtProd = new DataTable();
               // Admin_Module_Works_Select objSelect = new Admin_Module_Works_Select(dtProd, strSQL, strSchema, ref strError);
                Admin_Module_Works_Select objSelect = null;
                if (strError == null)
                {
                    if (dtProd.Rows.Count > 0)
                    {
                        string strFile = Convert.ToString(dtProd.Rows[0]["Category_Name"]) + ".xls";
                        int[] iColumns = null;
                        string[] sHeaders = { "Product Id", "Item Name", "Item Price", "Category Name", "Category Id", "URL" };

                        iColumns = new int[sHeaders.Length];
                        for (int k = 0; sHeaders.Length > k; k++)
                        {
                            iColumns[k] = k;
                        }
                        Export objExport = new Export();

                        objExport.ExportDetails(dtProd, iColumns, sHeaders, Export.ExportFormat.Excel, strFile);
                    }
                    else
                    {
                        Response.Write("No Record found");
                    }
                }
                else
                {
                    Response.Write("Error:<br>" + strError);
                }
            }
        }
    }
    public void fillSiteddl()
    {
        strSql = " SELECT " + strSchema + ".POS_BothWay.POS_OName AS Name, " +
                              " " + strSchema + ".POS_BothWay.POS_Id AS ID " +
                              " FROM " + strSchema + ".POS_BothWay " +
                              " WHERE (" + strSchema + ".POS_BothWay.POS_BRANCH_HO_CORPO IS NULL) AND (" + strSchema + ".POS_BothWay.Record_Status=1) " +
                              " ORDER BY " + strSchema + ".POS_BothWay.POS_OName;";
      //  DropDownFill objdd = new DropDownFill(ddlSite, strSql, "Name", "Id", strSchema);
    }
    //protected void btnGo_ServerClick(object sender, EventArgs e)
    //{
    //    if (ddlSite.SelectedValue != "")
    //    {
    //        string strError = "";
    //        generateServerControlCategoryTree(1, ref tvCategory, ref  strError);
    //        if (strError != null)
    //        {
    //            lblError.Text = strError;
    //        }
    //    }
    //    else
    //    {
    //        lblError.Text = "Please select a Site";
    //    }
    //}
    public void generateServerControlCategoryTree(int intCheckBox, ref TreeView objTreeView, ref string strTreeviewError)
    {
        DataTable dtCategory = new DataTable();
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DBCON"].ToString());
        string strSchema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);
        //string strSql = "SELECT " +
        //            " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Id]," +
        //            " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Name]," +
        //            " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Lavel]," +
        //            " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Page]," +
        //            " [" + strSchema + "].[ItemCategory_Web_Server].[Category_ParentID] " +
        //        "FROM " +
        //            " [" + strSchema + "].[ItemCategory_Web_Server] " +
        //        "WHERE" +
        //            " ([" + strSchema + "].[ItemCategory_Web_Server].[Record_Status]=1)" +
        //        "AND " +
        //            " ([" + strSchema + "].[ItemCategory_Web_Server].[Category_Id]!=0)" +
        //        "AND " +
        //            " ([" + strSchema + "].[ItemCategory_Web_Server].[Category_Name]!='')" +
        //        "AND " +
        //            " ([" + strSchema + "].[ItemCategory_Web_Server].[Category_Name] IS NOT NULL)" +
        //        " ORDER BY " +
        //            "[" + strSchema + "].[ItemCategory_Web_Server].[Category_Name];";


        string strSql = "SELECT " +
                    " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Id]," +
                    " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Name]," +
                    " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Lavel]," +
                    " [" + strSchema + "].[ItemCategory_Web_Server].[Category_ParentID], " +
                     " [" + strSchema + "].[ItemCategory_Web_Server].[Category_Page] " +
                "FROM " +
                    " [" + strSchema + "].[ItemCategory_Web_Server] " +
                "INNER JOIN " +
                    "[" + strSchema + "].[SiteCatgory_Web_Server] " +
                "ON " +
                    "[" + strSchema + "].[ItemCategory_Web_Server].[Category_Id]=[" + strSchema + "].[SiteCatgory_Web_Server].[Category_Id]" +
                "WHERE" +
                    " ([" + strSchema + "].[ItemCategory_Web_Server].[Record_Status]=1)" +
                "AND " +
                    " ([" + strSchema + "].[SiteCatgory_Web_Server].[Record_Status]=1)" +
                "AND " +
                    " ([" + strSchema + "].[ItemCategory_Web_Server].[Category_Id]!=0)" +
                "AND " +
                    " ([" + strSchema + "].[SiteCatgory_Web_Server].[Site_Id]='" + Convert.ToString(ddlSite.SelectedValue) + "')" +
                "AND " +
                    " ([" + strSchema + "].[ItemCategory_Web_Server].[Category_Name] IS NOT NULL)" +
                " ORDER BY " +
                    "[" + strSchema + "].[ItemCategory_Web_Server].[Category_Name];";


        string strError = "";
        //Admin_Module_Works_Select objSelect = new Admin_Module_Works_Select(dtCategory, strSql, strSchema, ref strError);
        Admin_Module_Works_Select objSelect = null;
 
        if (strError != null)
        {
            strTreeviewError = "There is some error on load...Please press refresh.<br>" + strError;
        }
        else
        {
            switch (intCheckBox)
            {
                case 1:     // fill the dropdown with all checkbox
                    strError = "";
                    CreateServerControlCategoryTreeWithAllCheckBox(dtCategory, ref objTreeView, ref strError);
                    if (strError != null)
                    {
                        strTreeviewError = "There is some error on loading of the tree..Please press refresh.<br>" + strError;
                    }
                    else
                    {
                        strError = "";
                    }
                    break;
                default:
                    strTreeviewError = "Invalid parameter";
                    break;
            }
        }
    }
    public void CreateServerControlCategoryTreeWithAllCheckBox(DataTable dtCategoryTree, ref TreeView objTreeView, ref string strError)
    {
        strError = null;
        DataRow[] drRoot = dtCategoryTree.Select("[Category_Lavel]=0");
        if (drRoot.Length > 0)
        {
            objTreeView.Nodes.Clear();
            foreach (DataRow dRow in drRoot)
            {
                TreeNode root = new TreeNode();
                root.Text = dRow["Category_Name"].ToString();
                root.Value = dRow["Category_Id"].ToString();
                root.ToolTip = dRow["Category_Page"].ToString();
                root.NavigateUrl = "javascript:FetchUrl(" + Convert.ToString(dRow["Category_Id"]) + ")";
                root.ShowCheckBox = false;
                objTreeView.Nodes.Add(root);
                int intParent = Convert.ToInt32(dRow["Category_Id"].ToString());
                int intTemp = Convert.ToInt32(dRow["Category_Lavel"].ToString()) + 1;
                DataRow[] drChildCollection = dtCategoryTree.Select("[Category_ParentId]='" + intParent.ToString() + "'AND([Category_Lavel]=" + intTemp + ")");
                if (drChildCollection.Length > 0)
                {
                    foreach (DataRow drChild in drChildCollection)
                    {
                        int intLvl = Convert.ToInt32(drChild["Category_Lavel"].ToString()) + 1;
                        int intTempChild = Convert.ToInt32(drChild["Category_Id"].ToString());
                        AddChildTreeServerControl(dtCategoryTree, root, drChild["Category_Name"].ToString(), intTempChild, intLvl, Convert.ToString(drChild["Category_Page"]));
                    }
                    objTreeView.CollapseAll();
                }
            }
        }
        else
        {
            strError = "No data in the table...";
        }
    }
    protected void AddChildTreeServerControl(DataTable dtCategory, TreeNode mnuParent, string strChildText, int intChildId, int intLvl, string Category_PageName)
    {
        if (dtCategory.Rows.Count > 0)
        {
            //"Category_ParentID='" + intChildId.ToString() + "'"; //AND(Category_Lavel=" + intLvl + ")";
            DataRow[] drChildCollection = dtCategory.Select("Category_ParentID='" + intChildId.ToString() + "'");
            TreeNode mnuChild = new TreeNode();
            mnuChild.Text = strChildText.ToString();
            mnuChild.Value = intChildId.ToString();
            mnuChild.ToolTip = Category_PageName;
            mnuChild.ShowCheckBox = false;
            mnuChild.NavigateUrl = "javascript:FetchUrl(" + Convert.ToString(intChildId) + ")";
            mnuParent.ChildNodes.Add(mnuChild);
            if (drChildCollection.Length > 0)
            {
                foreach (DataRow drChild in drChildCollection)
                {
                    int intChildLvl = Convert.ToInt32(drChild["Category_Lavel"].ToString());
                    int intParentChild = Convert.ToInt32(drChild["Category_Id"].ToString());
                    AddChildTreeServerControl(dtCategory, mnuChild, drChild["Category_Name"].ToString(), intParentChild, intChildLvl, Convert.ToString(drChild["Category_Page"]));
                }
            }
        }
    }






    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlSite.SelectedValue != "")
        {
            string strError = "";
            generateServerControlCategoryTree(1, ref tvCategory, ref  strError);
            if (strError != null)
            {
                lblError.Text = strError;
            }
        }
        else
        {
            lblError.Text = "Please select a Site";
        }
    }
}
