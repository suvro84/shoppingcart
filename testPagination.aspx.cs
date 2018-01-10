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


public partial class testPagination : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Chapter_Id = "1";
        string strError = "";
        string strPage = "";
        int currentpagenumber = 1;
        int inner = 0;
        int numberofProd = 20;
        if (!IsPostBack)
        {
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
                inner = (currentpagenumber - 1) * numberofProd;
            }
            if (getProduct(Chapter_Id, inner, currentpagenumber, numberofProd, ref strPage))
            {
                dvPage.InnerHtml = strPage;

            }
        }



    }

    public bool getProduct(string Chapter_Id, int inner, int pagenumber, int numberofProd, ref string strPage)
    {
        bool bflag = false;
        int totproduct = 0;
        commonclass objcat = new commonclass();
        int f = 1;
        int t = 6;
        int upto = 0;
        int prev = 5;
        string strsql = "select count(pid) as totproduct from testProduct  ";
        DataTable dtAllprod = objcat.Fetchrecords(strsql);
        if (dtAllprod.Rows.Count > 0)
        {
            totproduct = Convert.ToInt32(dtAllprod.Rows[0]["totproduct"]);
        }
        string strSql = "";
        if (pagenumber == 0)
        {

            strSql = "select pname from testProduct";

        }
        else
        {
            strSql = "select top (" + numberofProd + ")pname" +
   " from testProduct  " +
   " where pid not in (select top (" + inner + ")pid " +
   " from testProduct )";

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
                        sbpage.Append(intfrompage + "-" + totproduct + " of " + totproduct + "&nbsp");
                    }
                    else
                    {
                        sbpage.Append(intfrompage + "-" + inttopage + " of " + totproduct + "&nbsp");

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
                       //  btnprev.Enabled = false;
                        sbpage.Append("<image src=\"images/prev_arrow.gif\">");
                        //sbpage.Append("Previous");

                    }
                    else
                    {
                        sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (pagenumber - 1) + "\"><image src=\"images/prev_arrow.gif\">");
                       // sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (pagenumber - 1) + "\">Previous</a>&nbsp");
                    }

                    if (nopages > 6)
                    {

                        if (pagenumber % 6 == 0)
                        {
                            upto = pagenumber / 6 + 1;
                            f = pagenumber + 1;
                            t = 6 * upto;
                            if (nopages < t)
                            {
                                t = nopages;
                            }
                            else
                            {
                                t = 6 * upto;
                            }




                            Session["f"] = f;
                            Session["t"] = t;

                            if (Session["f"] == null && Session["f"] == null || pagenumber < 6)
                            {
                                f = 1;
                                t = 6;
                            }
                            else
                            {

                                f = Convert.ToInt32(Session["f"]);
                                t = Convert.ToInt32(Session["t"]);



                            }

                            for (int i = f - 1; i <= t; i++)
                            {
                                if (pagenumber == (i))
                                {
                                    sbpage.Append(i);
                                }
                                else
                                {
                                    sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i) + "\">" + (i) + "</a>&nbsp");

                                }
                            }




                        }
                        else if (pagenumber - 5 == 0)
                        {

                            f = Convert.ToInt32(Session["f"]) - 6;
                            t = Convert.ToInt32(Session["t"]) - 1;

                            for (int i = 1; i <= 6; i++)
                            {
                                if (pagenumber == (i))
                                {
                                    sbpage.Append(i);
                                }
                                else
                                {
                                    sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i) + "\">" + (i) + "</a>&nbsp");

                                }
                            }

                        }
                        else if ((pagenumber - 5) % 6 == 0)
                        {
                            int p = Convert.ToInt32(Session["t"]) + 1;
                            if (pagenumber < Convert.ToInt32(Session["f"]))//prev
                            {


                                f = Convert.ToInt32(Session["f"]) - 6;
                                t = Convert.ToInt32(Session["t"]) - 6;
                                //if(t-f

                                if (t - f != 5)
                                {
                                    int m = 5 - (t - f);
                                    t = t + m;

                                }


                                for (int i = f - 1; i <= t; i++)
                                {
                                    if (pagenumber == (i))
                                    {
                                        sbpage.Append(i);
                                    }
                                    else
                                    {
                                        sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i) + "\">" + (i) + "</a>&nbsp");

                                    }
                                }
                                Session["f"] = f;
                                Session["t"] = t;
                            }
                            else
                            {

                                f = Convert.ToInt32(Session["f"]);
                                t = Convert.ToInt32(Session["t"]);


                                for (int i = f - 1; i <= t; i++)
                                {
                                    if (pagenumber == (i))
                                    {
                                        sbpage.Append(i);
                                    }
                                    else
                                    {
                                        sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i) + "\">" + (i) + "</a>&nbsp");

                                    }
                                }



                            }


                        }






                        else
                        {
                            if (Session["f"] == null && Session["f"] == null || pagenumber < 6)
                            {
                                f = 1;
                                t = 6;

                                for (int i = f; i <= t; i++)
                                {
                                    if (pagenumber == (i))
                                    {
                                        sbpage.Append(i);
                                    }
                                    else
                                    {
                                        sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i) + "\">" + (i) + "</a>&nbsp");

                                    }
                                }


                            }
                            else
                            {

                                f = Convert.ToInt32(Session["f"]);
                                t = Convert.ToInt32(Session["t"]);


                                for (int i = f - 1; i <= t; i++)
                                {
                                    if (pagenumber == (i))
                                    {
                                        sbpage.Append(i);
                                    }
                                    else
                                    {
                                        sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i) + "\">" + (i) + "</a>&nbsp");

                                    }
                                }



                            }


                        }








                    }
                    else
                    {

                        for (int i = 0; i < nopages; i++)
                        {
                            if (pagenumber == (i + 1))
                            {
                                sbpage.Append(i + 1);
                            }
                            else
                            {
                                sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (i + 1) + "\">" + (i + 1) + "</a>&nbsp");

                            }
                        }


                    }

                    if (pagenumber == nopages)
                    {

                        // btnnext.Enabled = false;
                        sbpage.Append("<image src=\"images/next_arrow.gif\">&nbsp");

                    }
                    else
                    {
                        sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=" + (pagenumber + 1) + "\"><image src=\"images/next_arrow.gif\">&nbsp");

                    }
                    sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=0\"><image src=\"images/view_all_img.gif\"></a>");
                }
                //sbpage.Append("<a href=\"testPagination.aspx?Chapter_Id=" + Chapter_Id + "&pageno=0\"><image src=\"images/view_all_img.gif\"></a>");


            }
            else
            {
                sbpage.Append(totproduct + " of " + totproduct + "&nbsp");

            }
            strPage = sbpage.ToString();

            bflag = true;

        }
        return bflag;


    }
}
