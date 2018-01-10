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
using System.Text;
using System.Data.SqlClient;

public partial class chapter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Chapter_Id = string.Empty;
        string strError = "";
        string strPage = "";
        int currentpagenumber = 0;
        int inner = 0;
        int numberofProd = 5;
        if (Request.QueryString["Chapter_Id"] != null)
        {
            Chapter_Id = Convert.ToString(Request.QueryString["Chapter_Id"]);

        }
        if (Request.QueryString["pageno"] != null)
        {
            currentpagenumber = Convert.ToInt32(Request.QueryString["pageno"]);
        }

        if (Convert.ToInt32(Request.QueryString["pageno"]) == 1)
        {
            inner = 0;
        }
        else if (Convert.ToInt32(Request.QueryString["pageno"]) == 0)
        {
            inner = 0;
        }
        else
        {
            inner = (currentpagenumber - 1) * numberofProd +1;
        }
        if (getProduct(Chapter_Id, inner, currentpagenumber, numberofProd, ref strPage))
        {
            dvPage.InnerHtml = strPage;

        }



    }

    public bool getProduct(string Chapter_Id, int inner, int pagenumber, int numberofProd, ref string strPage)
    {
        bool bflag = false;
        int totproduct = 0;
        commonclass objcat = new commonclass();

        string strsql = "select count(a.Article_Id) as totproduct" +
" from tblArticle_Chapter_Relation r " +
" inner join tblArticleMaster a on a.Article_Id=r.Article_Id " +
" inner join tblChapter_Master c on c.Chapter_Id=r.Chapter_Id" +
" where c.Chapter_Id=" + Chapter_Id + " ";
        DataTable dtAllprod = objcat.Fetchrecords(strsql);
        if (dtAllprod.Rows.Count > 0)
        {
            totproduct = Convert.ToInt32(dtAllprod.Rows[0]["totproduct"]);
        }
        string strSql = "";
        if (pagenumber == 0)
        {

            strSql = "select  a.Article_Id,a.Article_Name," +
      " a.Article_Matter " +
      " from tblArticle_Chapter_Relation r " +
      " inner join tblArticleMaster a on a.Article_Id=r.Article_Id " +
      " inner join tblChapter_Master c on c.Chapter_Id=r.Chapter_Id " +
      " where  c.Chapter_Id=" + Chapter_Id + " ";
        }
        else
        {
            strSql = "select top (" + numberofProd + ") a.Article_Id,a.Article_Name," +
   " a.Article_Matter " +
   " from tblArticle_Chapter_Relation r " +
   " inner join tblArticleMaster a on a.Article_Id=r.Article_Id " +
   " inner join tblChapter_Master c on c.Chapter_Id=r.Chapter_Id " +
   " where a.Article_Id not in (select top (" + inner + ")a.Article_Id " +
   " from tblArticle_Chapter_Relation r" +
   " inner join tblArticleMaster a on a.Article_Id=r.Article_Id " +
   " inner join tblChapter_Master c on c.Chapter_Id=r.Chapter_Id) " +
   " and c.Chapter_Id=" + Chapter_Id + " ";
        }


        DataTable dtArticle = objcat.Fetchrecords(strSql);
        if (dtArticle.Rows.Count > 0)
        {
            dtlArticle.DataSource = dtArticle;
            dtlArticle.DataBind();
            StringBuilder sbpage = new StringBuilder();

            if (pagenumber != 0)
            {
                if (totproduct > numberofProd)
                {
                    int intfrompage = (pagenumber - 1) * numberofProd + 1;
                    int inttopage = pagenumber * numberofProd;
                    if (inttopage > totproduct)
                    {
                        sbpage.Append(intfrompage + "-" + totproduct + " of " + totproduct);
                    }
                    else
                    {
                        sbpage.Append(intfrompage + "-" + inttopage + " of " + totproduct);

                    }

                    int nopages = 0;
                    if (totproduct > numberofProd)
                    {

                        if (totproduct % numberofProd != 0)
                        {
                            nopages = totproduct / numberofProd + 1;
                        }
                        else
                        {

                            nopages = totproduct / numberofProd;
                        }
                    }

                    if (intfrompage == 1)
                    {
                        // btnprev.Enabled = false;
                        sbpage.Append("<image src=\"images/prev_arrow.gif\">");

                    }
                    else
                    {
                        sbpage.Append("<a href=\"chapter.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (pagenumber - 1) + "\"><image src=\"images/prev_arrow.gif\">");

                    }



                    for (int i = 0; i < nopages; i++)
                    {
                        if (pagenumber == (i + 1))
                        {
                            sbpage.Append(i + 1);
                        }
                        else
                        {
                            sbpage.Append("<a href=\"chapter.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i + 1) + "\">" + (i + 1) + "</a>");

                        }
                    }
                    if (pagenumber == nopages)
                    {

                        // btnnext.Enabled = false;
                        sbpage.Append("<image src=\"images/next_arrow.gif\">");

                    }
                    else
                    {
                        sbpage.Append("<a href=\"chapter.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (pagenumber + 1) + "\"><image src=\"images/next_arrow.gif\">");

                    }
                    //sbpage.Append("<a href=\"chapter.aspx?Chapter_Id=" + Chapter_Id + "&pageno=0\"><image src=\"images/view_all_img.gif\">");
                }


            }
            else
            {
                sbpage.Append(totproduct + " of " + totproduct);

            }
            strPage = sbpage.ToString();

            bflag = true;

        }
        return bflag;


    }
}
