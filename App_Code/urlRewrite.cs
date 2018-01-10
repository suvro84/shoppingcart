using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for urlRewrite
/// </summary>
public class urlRewrite
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DBCON"].ToString());
    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);
    String _rawUrl = null;
    public urlRewrite()
    {
        // No initialization.
    }
    public urlRewrite(string rawUrl)
    {
        _rawUrl = rawUrl;
    }

    public string rewritePath()
    {
        string _customPath = "";
        try
        {
            string _extensiontoShow = Convert.ToString(ConfigurationManager.AppSettings["extToShow"]);
            string requestPath = Convert.ToString(_rawUrl);
            string[] arrReqCatProd = requestPath.Split(new char[] { '/' });
            if (arrReqCatProd.Length > 0)
            {
                // ***********************************************************************
                //  Please decrement the counter by 1 of the below mentioned lines array
                //  Ln: 44(arrReqCatProd), Ln: 46(i=)
                // ***********************************************************************
                string withAspx = arrReqCatProd[arrReqCatProd.Length - 1];
                string requestType = arrReqCatProd[1];
                string beforeAspx = "";
                for (int i = 2; i < (arrReqCatProd.Length - 1); i++)
                {
                    beforeAspx += arrReqCatProd[i] + "/";
                }
                string[] arrSplitedAspx = withAspx.Split(new char[] { '.' });
                if (arrSplitedAspx.Length > 0)
                {
                    //if (Convert.ToString(requestType) == "Chapter")
                    if (Convert.ToString(requestType).ToUpper() == "CHAPTER")
                    {
                        //Gti24x7_CommonFunction objCommonfunction = new Gti24x7_CommonFunction();
                        ////  If its true means its a category page
                        //if ((Convert.ToString(arrSplitedAspx[0]) == "all") || (objCommonfunction.IsNumeric(arrSplitedAspx[0])))
                        //{
                        //    string strCategoryId = getCategoryId(Convert.ToString(arrReqCatProd[2]));
                        //    string strCalculatedFolder = getCalculatedFolder(arrReqCatProd);
                        //    //category.aspx?cat=66&pageno=1
                        //    _customPath = "" + strCalculatedFolder + "category.aspx?cat=" + strCategoryId + "&pageno=" + Convert.ToString(arrSplitedAspx[0]) + "";
                        //}
                        //else                   // It is a item page request
                        //{
                        //    // Yet to code
                        //    string strCategoryId = getCategoryId(beforeAspx);
                        //    string strProductId = getProductId(Convert.ToString(arrSplitedAspx[0]));
                        //    string strCalculatedFolder = getCalculatedFolder(arrReqCatProd);
                        //    //Gifts.aspx?proid=GTI0197&CatId=66
                        //    _customPath = "" + strCalculatedFolder + "gifts.aspx?proid=" + strProductId + "&CatId=" + strCategoryId + "";
                        //}
                        _customPath = getChapterPath(beforeAspx, Convert.ToString(arrSplitedAspx[0]));

                    }
                    //else if (Convert.ToString(requestType) == "Article")
                    else if (Convert.ToString(requestType).ToUpper() == "ARTICLE")
                    {
                        string strCalculatedFolder = getCalculatedFolder(arrReqCatProd);
                        _customPath = getArticlePath(beforeAspx, Convert.ToString(arrSplitedAspx[0]));
                    }
                    else if (Convert.ToString(requestType).ToUpper() == "CONTACT_US")
                    {
                        if (Convert.ToString(arrSplitedAspx[0]).Trim().ToUpper() == "24X7_CONTACTUS")
                        {
                            if (HttpContext.Current.Request.QueryString.Get("siteId") != null)
                            {
                                _customPath = "~/24x7_contactus.aspx?siteId=" + Convert.ToString(HttpContext.Current.Request.QueryString.Get("siteId"));
                            }
                            else
                            {
                                _customPath = "";
                            }
                        }
                        else
                        {

                            if (arrSplitedAspx.Length == 2)        // For without subdomain sites
                            //if (Convert.ToString(arrSplitedAspx[0]).Trim().ToUpper() == "WWW")
                            {
                                _customPath = getContactUs24x7(Convert.ToString(arrSplitedAspx[0]) + ".com");
                            }
                            else if (arrSplitedAspx.Length == 3)    // For subdomains
                            {
                                if (Convert.ToString(arrSplitedAspx[1]) == "in")
                                {
                                    _customPath = getContactUs24x7(Convert.ToString(arrSplitedAspx[0]) + "." + Convert.ToString(arrSplitedAspx[1]));
                                }
                                else
                                {
                                    _customPath = getContactUs24x7(Convert.ToString(arrSplitedAspx[0]) + "." + Convert.ToString(arrSplitedAspx[1]) + ".com");
                                }
                            }
                            else
                            {
                                _customPath = "";
                            }
                        }
                    }
                    else if (Convert.ToString(requestType).ToUpper() == "TERMS")
                    {
                        if (arrSplitedAspx.Length == 2)        // For without subdomain sites
                        //if (Convert.ToString(arrSplitedAspx[0]).Trim().ToUpper() == "WWW")
                        {
                            _customPath = getTerms24x7(Convert.ToString(arrSplitedAspx[0]) + ".com");
                        }
                        else if (arrSplitedAspx.Length == 3)    // For subdomains
                        {
                            if (Convert.ToString(arrSplitedAspx[1]) == "in")
                            {
                                _customPath = getTerms24x7(Convert.ToString(arrSplitedAspx[0]) + "." + Convert.ToString(arrSplitedAspx[1]));
                            }
                            else
                            {
                                _customPath = getTerms24x7(Convert.ToString(arrSplitedAspx[0]) + "." + Convert.ToString(arrSplitedAspx[1]) + ".com");
                            }
                        }
                        else
                        {
                            _customPath = "";
                        }
                    }
                    else
                    {
                        _customPath = "";
                    }
                }


            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Redirect("ErrServerErr.aspx");
        }

        return _customPath;
    }

    private string getCategoryId(string[] strCategoryPartision)
    {
        string strDbUrlPath = "/";
        //strDbUrlPath = string.Join("/", strCategoryPartision, 2, (strCategoryPartision.Length - 1) - 2);        
        strDbUrlPath += strCategoryPartision[2];
        strDbUrlPath.Insert(strDbUrlPath.Length - 1, "/");
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "" + strSchema + ".getCategoryIdAccordingToUrlPath";
        cmd.Parameters.Add(new SqlParameter("@RewriteUrlPath", SqlDbType.VarChar));
        cmd.Parameters["@RewriteUrlPath"].Value = strDbUrlPath;
        cmd.Parameters["@RewriteUrlPath"].Direction = ParameterDirection.Input;
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        string strCategoryId = Convert.ToString(cmd.ExecuteScalar());
        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        return strCategoryId;
    }
    private string getCategoryId(string strCategoryName)
    {
        string strDbUrlPath = strCategoryName + "/";
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "" + strSchema + ".getCategoryIdAccordingToUrlPath";
        cmd.Parameters.Add(new SqlParameter("@RewriteUrlPath", SqlDbType.VarChar));
        cmd.Parameters["@RewriteUrlPath"].Value = strDbUrlPath;
        cmd.Parameters["@RewriteUrlPath"].Direction = ParameterDirection.Input;
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        string strCategoryId = Convert.ToString(cmd.ExecuteScalar());
        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        return strCategoryId;
    }
    private string getProductId(string strProductName)
    {
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "" + strSchema + ".getProductIdAccordingToUrlName";
        cmd.Parameters.Add(new SqlParameter("@UrlName", SqlDbType.VarChar, 255));
        cmd.Parameters["@UrlName"].Value = strProductName;
        cmd.Parameters["@UrlName"].Direction = ParameterDirection.Input;
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        string strProductId = Convert.ToString(cmd.ExecuteScalar());
        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        return strProductId;
    }
    private string getCalculatedFolder(string[] strCategoryPartision)
    {
        string strCalculatedFolder = "";
        for (int i = 0; i < (strCategoryPartision.Length - 1) - 2; i++)
        {
            strCalculatedFolder = strCalculatedFolder + "~/";
        }
        return strCalculatedFolder;
    }
    protected string getChapterPath(string urlString, string pageNo)
    {
        string _OutPut = "";
        try
        {
            string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
            string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());
            if (conn.State == ConnectionState.Closed) conn.Open();
            string strSql = "SELECT " +
                                "[" + strSchema + "].[Chapter_Master].[ID], " +
                                "[" + strSchema + "].[Chapter_Master].[Name] " +
                            "FROM " +
                                "[" + strSchema + "].[Chapter_Master] " +
                            "INNER JOIN " +
                                "[" + strSchema + "].[Site_Chapter_Master] " +
                            "ON " +
                                "([" + strSchema + "].[Chapter_Master].[ID]=[" + strSchema + "].[Site_Chapter_Master].[ChapterId]) " +
                            "WHERE " +
                                "([" + strSchema + "].[Site_Chapter_Master].[RewriteURLPath]='" + urlString + "') " +
                            "AND " +
                                "([" + strSchema + "].[Site_Chapter_Master].[SiteId]='" + Convert.ToString(HttpContext.Current.Application["SiteId"]) + "');";
            SqlCommand _cmd = new SqlCommand(strSql, conn);
            SqlDataReader _dr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (_dr.HasRows)
            {
                if (_dr.Read())
                {
                    _OutPut = "~/chapter.aspx?c=" + Convert.ToString(_dr["ID"]) + "&p=" + pageNo;
                }
            }
        }
        catch (Exception ex)
        {
            _OutPut = ex.Message.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return _OutPut;
    }
    protected string getArticlePath(string urlChapterString, string urlArticleString)
    {
        string _OutPut = "";
        try
        {
            string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
            string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());
            if (conn.State == ConnectionState.Closed) conn.Open();
            string strSql = "SELECT " +
                            "[" + strSchema + "].[Article_Master].[ID], " +
                            "[" + strSchema + "].[Article_Chapter_Relation].[ChapterId], " +
                            "[" + strSchema + "].[Article_Master].[RewriteURLPath] as [ArticleURL], " +
                            "[" + strSchema + "].[Site_Chapter_Master].[RewriteURLPath] AS [ChapterURL], " +
                            "[" + strSchema + "].[Article_Master].[Name] " +
                        "FROM " +
                            "[" + strSchema + "].[Article_Master] " +
                        "INNER JOIN " +
                            "[" + strSchema + "].[Article_Chapter_Relation] " +
                        "ON " +
                            "([" + strSchema + "].[Article_Master].[ID]=[" + strSchema + "].[Article_Chapter_Relation].[ArticleId]) " +
                        "INNER JOIN " +
                            "[" + strSchema + "].[Chapter_Master] " +
                        "ON " +
                            "([" + strSchema + "].[Article_Chapter_Relation].[ChapterId]=[" + strSchema + "].[Chapter_Master].[Id]) " +
                        "INNER JOIN " +
                            "[" + strSchema + "].[Site_Chapter_Master] " +
                        "ON " +
                            "([" + strSchema + "].[Article_Chapter_Relation].[ChapterId]=[" + strSchema + "].[Site_Chapter_Master].[ChapterId]) " +
                        "WHERE " +
                            "([" + strSchema + "].[Site_Chapter_Master].[RewriteURLPath]='" + urlChapterString + "') " +
                        "AND " +
                            "([" + strSchema + "].[Article_Master].[RewriteURLPath]='" + urlArticleString + "')";
            SqlCommand _cmd = new SqlCommand(strSql, conn);
            SqlDataReader _dr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (_dr.HasRows)
            {
                if (_dr.Read())
                {
                    //_OutPut = "~/article.aspx?c=" + Convert.ToString(_dr["ChapterId"]) + "&a=" + Convert.ToString(_dr["ID"]);
                    _OutPut = "~/article.aspx?c=" + Convert.ToString(_dr["ChapterId"]) + "&a=" + Convert.ToString(_dr["ID"]);
                }
            }
        }
        catch (Exception ex)
        {
            _OutPut = ex.Message.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return _OutPut;
    }
    protected string getContactUs24x7(string urlString)
    {
        string _OutPut = "";
        try
        {
            string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
            string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());
            if (conn.State == ConnectionState.Closed) conn.Open();
            string strSql = "SELECT " +
                                "[" + strSchema + "].[SiteCss_Server].[siteName], " +
                                "[" + strSchema + "].[SiteCss_Server].[POS_Id] AS [SiteId] " +
                            "FROM " +
                                "[" + strSchema + "].[SiteCss_Server] " +
                            "WHERE " +
                                "([" + strSchema + "].[SiteCss_Server].[siteName]='" + urlString + "'); ";
            SqlCommand _cmd = new SqlCommand(strSql, conn);
            SqlDataReader _dr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (_dr.HasRows)
            {
                if (_dr.Read())
                {
                    _OutPut = "~/24x7_contactus.aspx?siteId=" + Convert.ToString(_dr["SiteId"]);
                }
            }
        }
        catch (Exception ex)
        {
            _OutPut = ex.Message.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return _OutPut;
    }
    protected string getTerms24x7(string urlString)
    {
        string _OutPut = "";
        try
        {
            string strSitePath = Convert.ToString(ConfigurationManager.AppSettings["siteDomain"].ToString());
            string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());
            if (conn.State == ConnectionState.Closed) conn.Open();
            string strSql = "SELECT " +
                                "[" + strSchema + "].[SiteCss_Server].[siteName], " +
                                "[" + strSchema + "].[SiteCss_Server].[POS_Id] AS [SiteId] " +
                            "FROM " +
                                "[" + strSchema + "].[SiteCss_Server] " +
                            "WHERE " +
                                "([" + strSchema + "].[SiteCss_Server].[siteName]='" + urlString + "'); ";
            SqlCommand _cmd = new SqlCommand(strSql, conn);
            SqlDataReader _dr = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (_dr.HasRows)
            {
                if (_dr.Read())
                {
                    _OutPut = "~/24x7_terms.aspx?siteId=" + Convert.ToString(_dr["SiteId"]);
                }
            }
        }
        catch (Exception ex)
        {
            _OutPut = ex.Message.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return _OutPut;
    }











    // Method written by AB starts
    public string ReplaceUrl(string URLPath)
    {
        string _strOutput = URLPath;
        try
        {
            if (_strOutput.Contains(" "))
            {
                _strOutput = _strOutput.Replace(" ", "_");
            }
            if (_strOutput.Contains("/-"))
            {
                _strOutput = _strOutput.Replace("/-", "");
            }
            if (_strOutput.Contains("&"))
            {
                _strOutput = _strOutput.Replace("&", "and");
            }

        }
        catch (Exception ex)
        {
            _strOutput = ex.Message.ToString();
        }
        return _strOutput;
    }
    // Method written by AB ends

}



