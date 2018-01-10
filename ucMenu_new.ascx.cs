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

public partial class ucMenu_new : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Convert.ToString(Session["userId"]) != "")
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        if (Session["userId"].ToString() != "")
        //        {
        //            string strUserId = Session["userId"].ToString();
        //            generateMenu(strUserId);
        //        }
        //    }
        //}
        //else
        //{
        //    if (HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.LastIndexOf("/")+1).ToString().ToUpper() != "DEFAULT.ASPX")
        //    {
        //        Response.Redirect("default.aspx");
        //    }
        //}

        if (Request.Cookies["UserInfo"] != null)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["UserInfo"]["userId"] != null)
                {
                    string strUserId = Request.Cookies["UserInfo"]["userId"].ToString();
                    generateMenu(strUserId);
                }
            }
        }
        else
        {
            if (HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.LastIndexOf("/") + 1).ToString().ToUpper() != "DEFAULT.ASPX")
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    private void generateMenu(string id)
    {
        string strSql = "SELECT " +
                        "dbo.Menu_Master.Id AS [ID],  " +
                        "dbo.Menu_Master.Title AS [Title], " +
                        "dbo.Menu_Master.ParentId AS [pid],  " +
                        "dbo.Menu_Master.Level AS [lvl],  " +
            //"dbo.Menu_Master.SQL AS [SQL], " +
                        "CASE WHEN dbo.Menu_Master.PageLink IS NULL THEN '#' ELSE dbo.Menu_Master.PageLink END AS [URL]  " +
                        "FROM  " +
                        "dbo.Menu_Login_Relation  " +
                        "INNER JOIN dbo.Menu_Master ON (dbo.Menu_Login_Relation.MenuId = dbo.Menu_Master.Id)  " +
                        "WHERE  " +
                        "(dbo.Menu_Master.RecordStatus = '1')   " +
                        "AND  " +
                        "(dbo.Menu_Login_Relation.LoginId='" + id + "') ORDER BY dbo.Menu_Master.Title";

        DataTable dt = new DataManipulationClass().FillDataTable(strSql);
        dvMenu.InnerHtml = generateRootMenu(dt);
    }
    private string generateRootMenu(DataTable dtt)
    {
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        str.Append("<ul id=\"Menu1\" class=\"MM\">");
        for (int i = 0; i < dtt.Rows.Count; i++)
        {
            if (Convert.ToString(dtt.Rows[i]["pid"]) == "0")
            {
                str.Append("<li><a href=\"" + Convert.ToString(dtt.Rows[i]["URL"]) + "\">" + Convert.ToString(dtt.Rows[i]["Title"]) + "</a>");
                str.Append("<ul>");
                str.Append(generateChild(dtt, Convert.ToString(dtt.Rows[i]["ID"])));
                str.Append("</ul>");
                str.Append("</li>");
            }
        }
        str.Append("</ul>");
        return str.ToString();
    }
    private string generateChild(DataTable dtt, string menuId)
    {
        System.Text.StringBuilder menuChild = new System.Text.StringBuilder();
        DataRow[] dRow = dtt.Select("pid='" + menuId + "'");
        for (int i = 0; i < dRow.Length; i++)
        {
            menuChild.Append("<li>");
            //menuChild.Append("<a href=\"" + Convert.ToString(dRow[i].ItemArray[4]) + "\">" + Convert.ToString(dRow[i].ItemArray[1]) + "</a>");
            menuChild.Append("<a href=\"" + Convert.ToString(dRow[i]["URL"]) + "\">" + Convert.ToString(dRow[i]["Title"]) + "</a>");
            DataRow[] dRow1 = dtt.Select("pid='" + Convert.ToString(dRow[i]["ID"]) + "'");
            if (dRow1.Length != 0)
            {
                menuChild.Append("<ul>");
                menuChild.Append(generateChild(dtt, Convert.ToString(dRow[i]["ID"])));
                menuChild.Append("</ul>");
            }

            menuChild.Append("</li>");
        }
        return menuChild.ToString();
    }
}
