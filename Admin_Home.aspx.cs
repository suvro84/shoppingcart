using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Admin_Home : System.Web.UI.Page
{

    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DBCON_LinkExchange"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["UserId"]) == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                BindDatalist(Convert.ToString(Session["UserId"]), Convert.ToString(Session["Type"]));
            }
        }

        //Session["UserId"] = 2;
        //if (!IsPostBack)
        //{
        //    BindDatalist();
        //}

    }

    public void BindDatalist(string UserId, string Type)
    {

        DataManipulationClass objdata = new DataManipulationClass();
        string strError = "";
        string strSQL = "";
        if (Type != "2")
        {
            strSQL = " SELECT " +
                           " LinkExchange.WebSite_Master.Id  ,  " +
                            " LinkExchange.WebSite_Master.Name, " +
                            "  LinkExchange.LinkExchangeMaster.Status," +
                            " count(LinkExchange.WebSite_Master.Name)as Wait " +
                            " FROM LinkExchange.LinkExchangeMaster  " +
                            " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId=LinkExchange.WebSite_Master.Id)" +
                            " WHERE " +
                            " (LinkExchange.WebSite_Master.UserId = '" + UserId + "') " +
                            " group by " +
                            " LinkExchange.WebSite_Master.Id  ,  " +
                            " LinkExchange.WebSite_Master.Name,  " +
                            " LinkExchange.LinkExchangeMaster.Status " +
                            " having LinkExchange.LinkExchangeMaster.Status=2 ";
        }
        else
        {
            strSQL = " SELECT " +
                              " LinkExchange.WebSite_Master.Id  ,  " +
                               " LinkExchange.WebSite_Master.Name, " +
                               "  LinkExchange.LinkExchangeMaster.Status," +
                               " count(LinkExchange.WebSite_Master.Name)as Wait " +
                               " FROM LinkExchange.LinkExchangeMaster  " +
                               " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId=LinkExchange.WebSite_Master.Id)" +
                               " INNER JOIN LinkExchange.User_WebSite_Relation ON (LinkExchange.WebSite_Master.id = LinkExchange.User_WebSite_Relation.WebSiteId)" +
                               " WHERE " +
                               " (LinkExchange.User_WebSite_Relation.UserId = '" + UserId + "') " +
                               " group by " +
                               " LinkExchange.WebSite_Master.Id  ,  " +
                               " LinkExchange.WebSite_Master.Name,  " +
                               " LinkExchange.LinkExchangeMaster.Status " +
                               " having LinkExchange.LinkExchangeMaster.Status=2 ";
        }
        DataTable dtWait = new DataTable();
        //dtWait.Columns.Add("SNo", typeof(int));
        //dtWait.PrimaryKey = new System.Data.DataColumn[] { dtWait.Columns["Id"] };

        dtWait = objdata.FillDataTable(strSQL, ref strError);

        if (Type != "2")
        {

            strSQL = " SELECT " +
                           " LinkExchange.WebSite_Master.Id  ,  " +
                            " LinkExchange.WebSite_Master.Name, " +
                            "  LinkExchange.LinkExchangeMaster.Status," +
                            " count(LinkExchange.WebSite_Master.Name)as Approved " +
                            " FROM LinkExchange.LinkExchangeMaster  " +
                            " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId=LinkExchange.WebSite_Master.Id)" +
                            " WHERE " +
                             " (LinkExchange.WebSite_Master.UserId = '" + UserId + "') " +
                            " group by " +
                            " LinkExchange.WebSite_Master.Id  ,  " +
                            " LinkExchange.WebSite_Master.Name,  " +
                            " LinkExchange.LinkExchangeMaster.Status " +
                            " having LinkExchange.LinkExchangeMaster.Status=1 ";
        }
        else
        {

            strSQL = " SELECT " +
                          " LinkExchange.WebSite_Master.Id  ,  " +
                           " LinkExchange.WebSite_Master.Name, " +
                           "  LinkExchange.LinkExchangeMaster.Status," +
                           " count(LinkExchange.WebSite_Master.Name)as Approved " +
                           " FROM LinkExchange.LinkExchangeMaster  " +
                           " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId=LinkExchange.WebSite_Master.Id)" +
                            " INNER JOIN LinkExchange.User_WebSite_Relation ON (LinkExchange.WebSite_Master.id = LinkExchange.User_WebSite_Relation.WebSiteId)" +
                           " WHERE " +
                            " (LinkExchange.User_WebSite_Relation.UserId = '" + UserId + "') " +
                           " group by " +
                           " LinkExchange.WebSite_Master.Id  ,  " +
                           " LinkExchange.WebSite_Master.Name,  " +
                           " LinkExchange.LinkExchangeMaster.Status " +
                           " having LinkExchange.LinkExchangeMaster.Status=1 ";

        }

        DataTable dtApproved = objdata.FillDataTable(strSQL, ref strError);
        if (Type != "2")
        {

            strSQL = " SELECT " +
                           " LinkExchange.WebSite_Master.Id  ,  " +
                            " LinkExchange.WebSite_Master.Name, " +
                            "  LinkExchange.LinkExchangeMaster.Status," +
                            " count(LinkExchange.WebSite_Master.Name)as Reject " +
                            " FROM LinkExchange.LinkExchangeMaster  " +
                            " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId=LinkExchange.WebSite_Master.Id)" +
                            " WHERE " +
                            " (LinkExchange.WebSite_Master.UserId = '" + UserId + "') " +
                            " group by " +
                            " LinkExchange.WebSite_Master.Id  ,  " +
                            " LinkExchange.WebSite_Master.Name,  " +
                            " LinkExchange.LinkExchangeMaster.Status " +
                            " having LinkExchange.LinkExchangeMaster.Status=3 ";
        }
        else
        {

            strSQL = " SELECT " +
                              " LinkExchange.WebSite_Master.Id  ,  " +
                               " LinkExchange.WebSite_Master.Name, " +
                               "  LinkExchange.LinkExchangeMaster.Status," +
                               " count(LinkExchange.WebSite_Master.Name)as Reject " +
                               " FROM LinkExchange.LinkExchangeMaster  " +
                               " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId=LinkExchange.WebSite_Master.Id)" +
                                " INNER JOIN LinkExchange.User_WebSite_Relation ON (LinkExchange.WebSite_Master.id = LinkExchange.User_WebSite_Relation.WebSiteId)" +
                               " WHERE " +
                               " (LinkExchange.User_WebSite_Relation.UserId = '" + UserId + "') " +
                               " group by " +
                               " LinkExchange.WebSite_Master.Id  ,  " +
                               " LinkExchange.WebSite_Master.Name,  " +
                               " LinkExchange.LinkExchangeMaster.Status " +
                               " having LinkExchange.LinkExchangeMaster.Status=3 ";
        }

        DataTable dtReject = objdata.FillDataTable(strSQL, ref strError);

        //DataRow drWait = null;
        //dtWait.Columns.Add("Approved", typeof(string));
        //for (int i = 0; i < dtWait.Rows.Count; i++)
        //{
        //    for (int j = 0; j < dtApproved.Rows.Count; j++)
        //    {
        //        if (dtWait.Rows[i]["id"].ToString() == dtApproved.Rows[j]["id"].ToString())
        //        {
        //            drWait = dtApproved.NewRow();
        //            drWait["Approved"] = dtApproved.Rows[j]["Approved"].ToString();

        //        }
        //    }
        //}
        //dtWait.AcceptChanges();


        dtWait.PrimaryKey = new DataColumn[1] { dtWait.Columns["Id"] };
        dtApproved.PrimaryKey = new DataColumn[1] { dtApproved.Columns["Id"] };
        dtReject.PrimaryKey = new DataColumn[1] { dtReject.Columns["Id"] };


        dtWait.Merge(dtApproved);
        dtWait.Merge(dtReject);


        DataTable dtFinal = new DataTable();

        DataRow drFinalRow;
        dtFinal.Columns.Add("SlNo", typeof(int));
        dtFinal.Columns["SlNo"].AutoIncrement = true;
        dtFinal.Columns["SlNo"].AutoIncrementSeed = 1;
        dtFinal.Columns.Add("Id", typeof(string));
        dtFinal.Columns.Add("Name", typeof(string));
        dtFinal.Columns.Add("Wait", typeof(string));
        dtFinal.Columns.Add("Approved", typeof(string));
        dtFinal.Columns.Add("Reject", typeof(string));

        // dtFinal = dtWait.Copy();

        for (int j = 0; j < dtWait.Rows.Count; j++)
        {
            drFinalRow = dtFinal.NewRow();
            drFinalRow["Id"] = Convert.ToString(dtWait.Rows[j]["Id"]);
            drFinalRow["Name"] = Convert.ToString(dtWait.Rows[j]["Name"]);
            drFinalRow["Wait"] = dtWait.Rows[j]["Wait"].ToString();
            drFinalRow["Approved"] = dtWait.Rows[j]["Approved"].ToString();

            drFinalRow["Reject"] = dtWait.Rows[j]["Reject"].ToString();
            dtFinal.Rows.Add(drFinalRow);


        }
        if (dtFinal.Rows.Count > 0)
        {
            rptLink.Visible = true;
            rptLink.DataSource = dtFinal;
            rptLink.DataBind();
            foreach (RepeaterItem rpt in rptLink.Items)
            {
                //HtmlControl sp = rpt.FindControl("spApproved");
                HtmlGenericControl spWait = (HtmlGenericControl)rpt.FindControl("spWait");
                HtmlGenericControl spWaitView = (HtmlGenericControl)rpt.FindControl("spWaitView");

                HtmlGenericControl spApproved = (HtmlGenericControl)rpt.FindControl("spApproved");
                HtmlGenericControl spApprovedView = (HtmlGenericControl)rpt.FindControl("spApprovedView");

                HtmlGenericControl spReject = (HtmlGenericControl)rpt.FindControl("spReject");
                HtmlGenericControl spRejectView = (HtmlGenericControl)rpt.FindControl("spRejectView");


                if (spWait.InnerHtml.ToString().Trim() == "")
                {
                    spWaitView.InnerHtml = "";

                }
                if (spApproved.InnerHtml.ToString().Trim() == "")
                {
                    spApprovedView.InnerHtml = "";

                }
                if (spReject.InnerHtml.ToString().Trim() == "")
                {
                    spRejectView.InnerHtml = "";

                }
            }
        }
        else
        {
            strSQL = "SELECT  ROW_NUMBER() OVER(ORDER BY LinkExchange.WebSite_Master.Id) AS 'SlNo',LinkExchange.WebSite_Master.Id ," +
                     " LinkExchange.WebSite_Master.Name," +
                     " 0 as 'Wait',0 as 'Approved',0 as 'Reject'" +
                     " FROM " +
                     " LinkExchange.WebSite_Master" +
                     " WHERE  (LinkExchange.WebSite_Master.UserId = '" + UserId + "')";
            dtWait = objdata.FillDataTable(strSQL, ref strError);
            rptLink.Visible = true;
            rptLink.DataSource = dtWait;
            rptLink.DataBind();

            foreach (RepeaterItem rpt in rptLink.Items)
            {
                HtmlGenericControl spWait = (HtmlGenericControl)rpt.FindControl("spWait");
                HtmlGenericControl spWaitView = (HtmlGenericControl)rpt.FindControl("spWaitView");

                HtmlGenericControl spApproved = (HtmlGenericControl)rpt.FindControl("spApproved");
                HtmlGenericControl spApprovedView = (HtmlGenericControl)rpt.FindControl("spApprovedView");

                HtmlGenericControl spReject = (HtmlGenericControl)rpt.FindControl("spReject");
                HtmlGenericControl spRejectView = (HtmlGenericControl)rpt.FindControl("spRejectView");


                if (spWait.InnerHtml.ToString().Trim() == "0")
                {
                    spWaitView.InnerHtml = "";

                }
                if (spApproved.InnerHtml.ToString().Trim() == "0")
                {
                    spApprovedView.InnerHtml = "";

                }
                if (spReject.InnerHtml.ToString().Trim() == "0")
                {
                    spRejectView.InnerHtml = "";

                }
            }

        }

    }

    public string get_data(string id, string Name, string Wait, string Approved, string Reject)
    {
        string sitelink = "";

        if (Wait == "0" && Approved == "0" && Reject == "0")
        {
            //sitelink = Name;
            sitelink = "<a class=\"siteName\" href=\"#\">" + Name + "</a>";


        }
        else
        {
            sitelink = "<a class=\"siteName\" href=\"WebSite-Details.aspx?SiteId=" + id + "\">" + Name + "</a>";


        }
        return sitelink;
    }
}
