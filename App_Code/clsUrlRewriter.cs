using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for clsUrlRewriter
/// </summary>
public class clsUrlRewriter
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBCON"].ToString());
    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);
    //Admin_Module_Works_Select myobj = new Admin_Module_Works_Select();
    string _extensionToShow = Convert.ToString(ConfigurationManager.AppSettings["extToShow"]);
    public string getReWritePath(String strCurrentPath)
    {
        int flag = 0;
        String strCustomPath = "";
        string strLastCharWithAspx;
        string strLastCharWithoutAspx = "";
        string withoutaspx = "";

        string[] arrCatOrProdIdWithAspx = strCurrentPath.Split(new char[] { '/' });
        //if (arrCatOrProdIdWithAspx.Length == 5)
        //{
        //    strLastCharWithoutAspx = arrCatOrProdIdWithAspx[2] + "/" + arrCatOrProdIdWithAspx[3];
        //}
        //else if (arrCatOrProdIdWithAspx.Length == 8)
        //{
        //    strLastCharWithoutAspx = arrCatOrProdIdWithAspx[2] + "/" + arrCatOrProdIdWithAspx[3] + "/" + arrCatOrProdIdWithAspx[4] + "/" + arrCatOrProdIdWithAspx[5] + "/" + arrCatOrProdIdWithAspx[6];
        //}

        //else
        //{
        //    //strLastCharWithAspx = arrCatOrProdIdWithAspx[arrCatOrProdIdWithAspx.Length - 2];
        //    strLastCharWithoutAspx = arrCatOrProdIdWithAspx[arrCatOrProdIdWithAspx.Length - 2];
        //}
        for (int k = 2; k < arrCatOrProdIdWithAspx.Length - 1; k++)
        {
            withoutaspx += arrCatOrProdIdWithAspx[k] + "/";
        }

        //string strLastCharWithAspx = arrCatOrProdIdWithAspx[arrCatOrProdIdWithAspx.Length - 1];

        //string[] arrCatOrProdIdWithoutAspx = strLastCharWithAspx.Split(new char[] { '.' });
        //strLastCharWithoutAspx = arrCatOrProdIdWithoutAspx[0];

        try
        {
            //HttpContext.Current.Response.Write("hi");
            if (Convert.ToInt32(strLastCharWithoutAspx) >= 1)
            {
                flag = 1;
                string strCategoryId = getCategoryId(arrCatOrProdIdWithAspx);
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "category.aspx?cat=" + strCategoryId + "&pageno=" + strLastCharWithoutAspx + "";
            }
            else
            {
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
            }
        }
        catch (Exception ex)
        {
            if (flag == 0)
            {
                try
                {
                    //string strCategoryId = getCategoryId(arrCatOrProdIdWithAspx);
                    string strCategoryId = getCategoryId_New(withoutaspx);
                    // Console.WriteLine(strCategoryId);
                    // HttpContext.Current.Response.Write("Category id:"+strCategoryId+"<br/>");
                    // string strProductId = getProductId(strLastCharWithoutAspx);
                    string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                    //strCustomPath = "" + strCalculatedFolder + "mainProduct.aspx?cat=" + strCategoryId + "&pid=" + strProductId + "";
                    if (strCategoryId != "")
                    {

                        string sql = "SELECT " +
  " rgcards_gti24x7.SiteCatgory_Web_Server.CategoryPage" +
" FROM " +
  " rgcards_gti24x7.ItemCategory_Web_Server" +
 " INNER JOIN rgcards_gti24x7.SiteCatgory_Web_Server ON (rgcards_gti24x7.ItemCategory_Web_Server.Category_Id = rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id)" +
" where   rgcards_gti24x7.ItemCategory_Web_Server.Category_Id=" + strCategoryId + "" +
" and rgcards_gti24x7.SiteCatgory_Web_Server.site_Id=" + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + "";
                        DataTable dtpage = new DataTable();
                        string strerror = "";
                        string StaticPage = "";
                        Admin_Module_Works_Select myobj = new Admin_Module_Works_Select();
                        //try
                        //{
                        myobj.GetDataTable(dtpage, sql, strSchema, ref strerror);
                        if (strerror == null)
                        {
                            if (dtpage.Rows.Count > 0)
                            {
                                StaticPage = Convert.ToString(dtpage.Rows[0]["CategoryPage"]);
                            }
                        }
                        //}
                        //catch (Exception ex)
                        //{
                        //    strerror = "Error!<br/>" + ex.Message;
                        //}



                        if (StaticPage == "")
                        {
                            //strCustomPath = "" + strCalculatedFolder + "category.aspx?cat=" + strCategoryId + "&pid=" + strProductId + "";
                            strCustomPath = "" + strCalculatedFolder + "category.aspx?cat=" + strCategoryId + "&pageno=1";
                            //strCustomPath = "category.aspx?cat=" + strCategoryId + "&pageno=1";

                        }
                        else
                        {
                            strCustomPath = "" + strCalculatedFolder + "" + StaticPage + "?cat=" + strCategoryId + "&pageno=1";

                        }

                    }
                }
                catch
                {
                    string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                    strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
                }
            }
            else if (flag == 1)
            {
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
            }
        }
        return strCustomPath;
    }



    //for item page reditection//////

    public string getItem_ReWritePath(String strCurrentPath)
    {
        int flag = 0;
        String strCustomPath = "";
        string strLastCharWithAspx;
        string strLastCharWithoutAspx = "";
        string withoutaspx = "";
        string strError = "";
        string strcid = "";

        string[] arrCatOrProdIdWithAspx = strCurrentPath.Split(new char[] { '/' });

        for (int k = 2; k < arrCatOrProdIdWithAspx.Length; k++)
        {
            withoutaspx += arrCatOrProdIdWithAspx[k] + "/";
        }

        //string strLastCharWithAspx = arrCatOrProdIdWithAspx[arrCatOrProdIdWithAspx.Length - 1];

        //string[] arrCatOrProdIdWithoutAspx = strLastCharWithAspx.Split(new char[] { '.' });
        //strLastCharWithoutAspx = arrCatOrProdIdWithoutAspx[0];

        try
        {
            if (Convert.ToInt32(strLastCharWithoutAspx) >= 1)
            {
                flag = 1;
                string strCategoryId = getCategoryId(arrCatOrProdIdWithAspx);
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "category.aspx?cat=" + strCategoryId + "&pageno=" + strLastCharWithoutAspx + "";
            }
            else
            {
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
            }
        }
        catch (Exception ex)
        {
            if (flag == 0)
            {
                try
                {
                    //string strCategoryId = getCategoryId(arrCatOrProdIdWithAspx);
                    //string without = "";
                    //if(withoutaspx.Contains("_"))
                    //{
                    //without = withoutaspx.Replace("_", " ");
                    //}


                    //string strCategoryId = getCategoryId_New(without.Substring(0,withoutaspx.Length-6));
                    string item_nameURL = Convert.ToString(HttpContext.Current.Request.RawUrl.ToString());

                    string[] url = item_nameURL.Split(new char[] { '/' });


                    string urlpath = "";
                    for (int l = 2; l < url.Length - 1; l++)
                    {
                        urlpath += url[l] + "/";
                    }

                    strcid = getCategoryId_New(urlpath);


                    string item_name_withspace = url[url.Length - 1];
                    string item_name_withoutspace = "";

                    //if (item_name_withspace.Contains("_"))
                    //{
                    //item_name_withoutspace = item_name_withspace.Replace("--", "/-").Replace("~", "_") ;
                    item_name_withoutspace = item_name_withspace;
                    //}




                    string[] itemwithoutaspx = item_name_withoutspace.Split(new char[] { '.' });
                    //  string[] itemwithoutaspx = item_name_withspace.Split(new char[] { '.' });

                    //string Urlname = itemwithoutaspx[0].Replace("@", ".");
                    //string Urlname = itemwithoutaspx[0].Replace("@", ".");
                    string Urlname = itemwithoutaspx[0];
                    string strProductId = getProductId(Urlname);
                    string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                    //strCustomPath = "" + strCalculatedFolder + "mainProduct.aspx?cat=" + strCategoryId + "&proID=" + strProductId + "";
                    //if (strCategoryId == "")
                    //{



                    //string sql = "select top 1 Category_Id from " + strSchema + ".ItemCategoryRelation_Web_Server where Product_Id ='" + strProductId + "' and[" + strSchema + "].[ItemCategoryRelation_Web_Server].[Record_Status]=1";
                    ////string sql = "select icr.Category_Id   from rgcards_gti24x7.ItemCategoryRelation_Web_Server icr inner join rgcards_gti24x7.ItemMaster_Server ims on icr.Product_Id =ims.Product_Id  where ims.item_name='"+pname+"'";

                    //Admin_Module_Works_Select myobj = new Admin_Module_Works_Select();
                    //DataTable dtproduct = new DataTable();

                    //myobj.GetDataTable(dtproduct, sql, strSchema, ref strError);
                    //if (strError == null)
                    //{
                    //    if (dtproduct.Rows.Count > 0)
                    //    {
                    //        strcid = Convert.ToString(dtproduct.Rows[0]["Category_Id"]);

                    //    }
                    //}
                    //}


                    DataTable dtpath = new DataTable();
                    //string path = "";
                    //string strsql = "select RewriteUrlPath from rgcards_gti24x7.ItemCategory_Web_Server where Category_Id=" + strcid + "";
                    //string strsql = "select Item_Name from rgcards_gti24x7.ItemCategory_Web_Server where Product_Id=" + strProductId + "";
                    //string strsql = "select Category_Id from rgcards_gti24x7.ItemCategory_Web_Server where Product_Id=" + strProductId + "";

                    //myobj.GetDataTable(dtpath, strsql, strSchema, ref strError);
                    //if (strError == null)
                    //{
                    //    if (dtpath.Rows.Count > 0)
                    //    {
                    //        path = Convert.ToString(dtpath.Rows[0]["Item_Name"]);

                    //    }
                    //}


                    //}
                    if (strProductId != "" && strcid != "")
                    {
                        //  strCustomPath = "item.aspx?catId=" + strcid + "&proId=" + strProductId + "";
                        strCustomPath = "" + strCalculatedFolder + "gifts.aspx?catId=" + strcid + "&proId=" + strProductId + "";
                        //strCustomPath = "category.aspx?cat=" + strCategoryId + "&pageno=1";

                    }
                }
                catch
                {
                    string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                    //strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
                }
            }
            else if (flag == 1)
            {
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
            }
        }
        return strCustomPath;
    }



    public string getReWritePathNew(String strCurrentPath, int cat)
    {
        int flag = 0;
        String strCustomPath = "";

        string[] arrCatOrProdIdWithAspx = strCurrentPath.Split(new char[] { '/' });
        string strLastCharWithAspx = arrCatOrProdIdWithAspx[arrCatOrProdIdWithAspx.Length - 1];

        string[] arrCatOrProdIdWithoutAspx = strLastCharWithAspx.Split(new char[] { '.' });
        string strLastCharWithoutAspx = arrCatOrProdIdWithoutAspx[0];

        try
        {
            if (Convert.ToInt32(strLastCharWithoutAspx) >= 1)
            {
                flag = 1;
                string strCategoryId = getCategoryId(arrCatOrProdIdWithAspx);
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                //strCustomPath = "" + strCalculatedFolder + "category.aspx?cat=" + strCategoryId + "&pageno=" + strLastCharWithoutAspx + "";
                strCustomPath = "category.aspx?cat=" + strCategoryId + "&pageno=" + strLastCharWithoutAspx + "";

                //strCustomPath = "" + strCalculatedFolder + "category.aspx?cat=" + cat +"&pageno=1";
            }
            else
            {
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
            }
        }
        catch (Exception ex)
        {
            if (flag == 0)
            {
                try
                {
                    string strCategoryId = getCategoryId(arrCatOrProdIdWithAspx);
                    string strProductId = getProductId(strLastCharWithoutAspx);
                    string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                    strCustomPath = "" + strCalculatedFolder + "mainProduct.aspx?cat=" + strCategoryId + "&pid=" + strProductId + "";
                    strCustomPath = "" + strCalculatedFolder + "mainProduct.aspx?cat=" + strCategoryId + "&pid=" + strProductId + "";

                    //strCustomPath = "" + strCalculatedFolder + "category.aspx?cat=" + cat + "&pageno=1";
                }
                catch
                {
                    string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                    strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
                }
            }
            else if (flag == 1)
            {
                string strCalculatedFolder = getCalculatedFolder(arrCatOrProdIdWithAspx);
                strCustomPath = "" + strCalculatedFolder + "DefaultErrorPage.aspx";
            }
        }
        return strCustomPath;
    }
    private string getCategoryId(string[] strCategoryPartision)
    {
        string strErr = "";
        string strCategoryId = "";
        try
        {
            string strDbUrlPath = "/";
            //strDbUrlPath = string.Join("/", strCategoryPartision, 2, (strCategoryPartision.Length - 1) - 2);        
            strDbUrlPath += strCategoryPartision[2];
            strDbUrlPath.Insert(strDbUrlPath.Length - 1, "/");

            HttpContext.Current.Response.Write("strDbUrlPath:" + strDbUrlPath + "<br/>");

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "rgcards_gti24x7.getCategoryIdAccordingToUrlPath";
            cmd.Parameters.Add(new SqlParameter("@RewriteUrlPath", SqlDbType.VarChar));
            cmd.Parameters["@RewriteUrlPath"].Value = strDbUrlPath;
            cmd.Parameters["@RewriteUrlPath"].Direction = ParameterDirection.Input;
            conn.Open();
            strCategoryId = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();








        }
        catch (Exception ex)
        {
            strErr = "Error!<br/>" + ex.Message;
        }
        return strCategoryId;

    }
    private string getProductId(string strUrlName)
    {
        string strErr = "";
        string strProductId = "";
        try
        {
            //SqlCommand cmd = conn.CreateCommand();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "rgcards_gti24x7.getProductIdAccordingToUrlName";
            //cmd.Parameters.Add(new SqlParameter("@UrlName", SqlDbType.VarChar, 8000));
            //cmd.Parameters["@UrlName"].Value = strUrlName;
            //cmd.Parameters["@UrlName"].Direction = ParameterDirection.Input;

            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();
            //}
            //strProductId = Convert.ToString(cmd.ExecuteScalar());

            Admin_Module_Works_Select myobj = new Admin_Module_Works_Select();
            DataTable dtproductid = new DataTable();
            if (strUrlName != null)
            {

                string sql = " SELECT rgcards_gti24x7.ItemMaster_Server.Product_Id  FROM rgcards_gti24x7.ItemMaster_Server WHERE rgcards_gti24x7.ItemMaster_Server.UrlName='" + strUrlName + "' or rgcards_gti24x7.ItemMaster_Server.UrlName='" + strUrlName + "_'";

                myobj.GetDataTable(dtproductid, sql, strSchema, ref strErr);
                if (strErr == null)
                {
                    if (dtproductid.Rows.Count > 0)
                    {
                        strProductId = Convert.ToString(dtproductid.Rows[0]["Product_Id"]);

                    }
                }
            }
            //else
            //{
            //    string sql = " SELECT rgcards_gti24x7.ItemMaster_Server.Product_Id  FROM rgcards_gti24x7.ItemMaster_Server WHERE rgcards_gti24x7.ItemMaster_Server.item_name='" + strUrlName.Replace("_", " ") + "'";

            //    myobj.GetDataTable(dtproductid, sql, strSchema, ref strErr);
            //    if (strErr == null)
            //    {
            //        if (dtproductid.Rows.Count > 0)
            //        {
            //            strProductId = Convert.ToString(dtproductid.Rows[0]["Product_Id"]);

            //        }
            //    }

            //}



        }
        catch (Exception ex)
        {
            strErr = "Error!<br/>" + ex.Message;
        }
        finally
        {

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }
        return strProductId;
    }


    private string getCategoryId_New(string strProductName)
    {
        //string strDbUrlPath = "/";
        //strDbUrlPath = string.Join("/", strCategoryPartision, 2, (strCategoryPartision.Length - 1) - 2);        
        //strDbUrlPath += strCategoryPartision[2];
        //strProductName =   strProductName +"/";
        //strProductName.Insert(0, "/");
        //strProductName.Insert(strProductName.Length - 1, "/");
        string strErr = "";
        string strCategoryId = "";
        try
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "rgcards_gti24x7.getCategoryIdAccordingToUrlPath";
            cmd.Parameters.Add(new SqlParameter("@RewriteUrlPath", SqlDbType.VarChar));
            cmd.Parameters["@RewriteUrlPath"].Value = strProductName;
            cmd.Parameters["@RewriteUrlPath"].Direction = ParameterDirection.Input;
            conn.Open();
            strCategoryId = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();


            //string sql = "SELECT TOP 1  rgcards_gti24x7.ItemCategory_Web_Server.Category_Id  FROM  rgcards_gti24x7.ItemCategory_Web_Server  WHERE rgcards_gti24x7.ItemCategory_Web_Server.RewriteUrlPath LIKE '" + strProductName + "'";
            //DataTable dtcatid = new DataTable();
            //string strError = "";
            //Admin_Module_Works_Select myobj = new Admin_Module_Works_Select();
            //myobj.GetDataTable(dtcatid, sql, strSchema, ref strError);
            //if (strError == null)
            //{
            //    if (dtcatid.Rows.Count > 0)
            //    {
            //        strCategoryId = Convert.ToString(dtcatid.Rows[0]["Category_Id"]);
            //        HttpContext.Current.Response.Write("strCategoryId:" + strCategoryId + "<br/>");

            //    }
            //}

        }
        catch (Exception ex)
        {
            strErr = "Error!<br/>" + ex.Message;
        }
        return strCategoryId;

    }
    private string getCalculatedFolder(string[] strCategoryPartision)
    {
        string strCalculatedFolder = "";
        for (int i = 0; i < (strCategoryPartision.Length - 1) - 2; i++)
        {
            strCalculatedFolder = strCalculatedFolder + "../";
        }
        return strCalculatedFolder;
    }

    public void monthWiseEndDay(string strMatch, int year, ref string Month, ref string Endday)
    {
        switch (strMatch)
        {
            case "January":
                Month = "1";
                Endday = "31";
                break;
            case "February":
                Month = "2";
                if (year % 4 == 0)
                {
                    Endday = "29";
                }
                else
                {
                    Endday = "28";
                }
                break;
            case "March":
                Month = "3";
                Endday = "31";
                break;
            case "April":
                Month = "4";
                Endday = "30";
                break;
            case "May":
                Endday = "31";
                Month = "5";
                break;
            case "June":
                Month = "6";
                Endday = "30";
                break;
            case "July":
                Month = "7";
                Endday = "31";
                break;
            case "August":
                Month = "8";
                Endday = "31";
                break;
            case "September":
                Month = "9";
                Endday = "30";
                break;
            case "October":
                Month = "10";
                Endday = "31";
                break;
            case "November":
                Month = "11";
                Endday = "30";
                break;
            case "December":
                Month = "12";
                Endday = "31";
                break;
        }
    }

    public string getRewriteUrlPath(string categoryId)
    {
        string strUrlPath = "";

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT " +
                          " rgcards_gti24x7.ItemCategory_Web_Server.RewriteUrlPath " +
                          " FROM " +
                          " rgcards_gti24x7.ItemCategory_Web_Server " +
                          " WHERE " +
                          " (rgcards_gti24x7.ItemCategory_Web_Server.Category_Id ='" + categoryId + "')";

        conn.Open();
        SqlDataReader drUrlPath = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (drUrlPath.HasRows)
        {
            if (drUrlPath.Read())
            {
                strUrlPath = Convert.ToString(drUrlPath["RewriteUrlPath"]);
            }
        }
        drUrlPath.Dispose();
        cmd.Dispose();
        return strUrlPath;
    }
}
