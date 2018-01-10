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

public partial class uc_Articles : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Chapter_Id = "";
        commonclass objcat = new commonclass();

        if (Request.QueryString["Chapter_Id"] != null)
        {
            Chapter_Id = Convert.ToString(Request.QueryString["Chapter_Id"]);
        }

        if (Chapter_Id == "")
        {
            //  strsql = "select Category_id,Category_Name from tblItemCategory_Web_Server where Parent_Category_id=" + Request.QueryString["catid"] + "";
            string strsql = "select c.Chapter_Id,c.Chapter_Name, s.chapter_level,s.Chapter_Path,s.Site_id,s.Parent_Id from tblChapter_Master c " +
            " INNER JOIN tblSiteChapterMaster s ON s.Chapter_Id=c.Chapter_Id where s.chapter_level=0 ";


            DataTable dtArticle = objcat.Fetchrecords(strsql);

            //rptArticles.DataSource = dtArticle;
            //rptArticles.DataBind();
            StringBuilder sboutput = new StringBuilder();


            sboutput.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
            sboutput.Append("<tr><td><h1>Interesting Articles</h1></td></tr>");
            foreach (DataRow drcat in dtArticle.Rows)
            {
                sboutput.Append("<tr>");
                sboutput.Append("<td>");
                sboutput.Append("<a href=\"chapter.aspx?Chapter_Id=" + drcat["Chapter_Id"].ToString() + "&pageno=1\">" + drcat["Chapter_Name"].ToString() + "</a>");
                sboutput.Append("</td>");
                sboutput.Append("</tr>");


            }
            sboutput.Append("</table>");
            lblCategory.Text = sboutput.ToString();

        }

        else
        {
            string strsql = "select c.Chapter_Id,c.Chapter_Name, s.chapter_level,s.Chapter_Path,s.Site_id,s.Parent_Id from tblChapter_Master c " +
    " INNER JOIN tblSiteChapterMaster s ON s.Chapter_Id=c.Chapter_Id ";



            DataTable dtArticle = objcat.Fetchrecords(strsql);
            lblCategory.Text = "";
            StringBuilder sboutput = new StringBuilder();
            lblCategory.Text = BindLevel1(dtArticle, Chapter_Id);

        }


    }

    public string BindLevel1(DataTable dtArticle, string Chapter_Id)
    {
        DataView dvSubArticle = new DataView(dtArticle);
        dvSubArticle.RowFilter = "Parent_Id='" + Chapter_Id + "'";
        StringBuilder sboutput = new StringBuilder();

        DataView subname = new DataView(dtArticle);
        subname.RowFilter = "Chapter_Id='" + Chapter_Id + "'";
        string parent_id = "";
        string headername = "";
        if (subname.ToTable().Rows.Count > 0)
        {
            parent_id = subname.ToTable().Rows[0]["Parent_id"].ToString();
            headername = subname.ToTable().Rows[0]["Chapter_Name"].ToString();

            if (dvSubArticle.ToTable().Rows.Count > 0)
            {
                //rptArticles.DataSource = dvSubArticle.ToTable();
                //rptArticles.DataBind();
                sboutput.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                sboutput.Append("<tr><td><h1>" + headername + "</h1></td></tr>");
                foreach (DataRow drcat in dvSubArticle.ToTable().Rows)
                {
                    sboutput.Append("<tr>");
                    sboutput.Append("<td>");
                    sboutput.Append("<a href=\"chapter.aspx?Chapter_Id=" + drcat["Chapter_Id"].ToString() + "&pageno=1\">" + drcat["Chapter_Name"].ToString() + "</a>");
                    sboutput.Append("</td>");
                    sboutput.Append("</tr>");


                }
                sboutput.Append("</table>");
                sboutput.Append("<br>");



            }
            sboutput.Append(BindLevel2(parent_id, headername, dtArticle));


        }
        return sboutput.ToString();



    }

    public string BindLevel2(string parent_id, string headername, DataTable dtArticle)
    {
        StringBuilder sboutput = new StringBuilder();
        if (parent_id != "0")
        {
            sboutput.Append(BindLevel1(dtArticle, parent_id));

        }


        else if (parent_id == "0")
        {
            DataView dvchild = new DataView(dtArticle);
            dvchild.RowFilter = "Parent_Id=0";
            //rptSubArticles.DataSource = dvchild.ToTable();
            //rptSubArticles.DataBind();

            sboutput.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
            sboutput.Append("<tr><td><h1>Interesting Articles</h1></td></tr>");
            foreach (DataRow drcat in dvchild.ToTable().Rows)
            {
                sboutput.Append("<tr>");
                sboutput.Append("<td>");
                sboutput.Append("<a href=\"chapter.aspx?Chapter_Id=" + drcat["Chapter_Id"].ToString() + "&pageno=1\">" + drcat["Chapter_Name"].ToString() + "</a>");
                sboutput.Append("</td>");
                sboutput.Append("</tr>");


            }
            sboutput.Append("</table>");




        }
        return sboutput.ToString();

    }

}
