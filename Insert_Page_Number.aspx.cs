using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Insert_Page_Number : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["UserId"] = 2;

            if (Convert.ToString(Session["UserId"]) != "")
            {
                populateSite(Convert.ToInt32(Session["UserId"]));

                if (Request.QueryString["flag"] != null)
                {
                    if (Request.QueryString["flag"] == "update")
                    {
                        lblMsg.Text = "Page id inserted Successfully";
                    }
                }

            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            //populateSite(2);
        }
    }
    private void populateSite(int UserId)
    {

        GCommon<WebSiteDetail> objLink = new DataManipulationClass().GetSiteCollection(UserId);

        if (objLink.Count > 0)
        {
            System.Collections.Generic.IEnumerator<WebSiteDetail> ie = objLink.GetEnumerator();
            ddlSite.Items.Clear();
            ListItem li = new ListItem();
            li.Text = "Please Select";
            li.Value = "";
            ddlSite.Items.Add(li);

            while (ie.MoveNext())
            {

                li = new ListItem();
                li.Text = Convert.ToString(ie.Current.SiteURL);
                li.Value = Convert.ToString(ie.Current.Id);
                ddlSite.Items.Add(li);

            }
        }

    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSite.SelectedValue != "")
        {
            dvData.Visible = true;
            GridView1.Visible = false;
            lblMsg.Text = "";
            lblSearch.Text = "";
            string sql = "SELECT " +
                                       "LinkExchange.SubPage.ID, " +
                                       "LinkExchange.SubPage.LinkURL " +
                                       "FROM " +
                                       "LinkExchange.SubPage " +
                                       "WHERE LinkExchange.SubPage.Websiteid='" + ddlSite.SelectedValue + "'";
            string strError = "";
            string _schema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);

            DataManipulationClass objdata = new DataManipulationClass();

            objdata.DropDownFill(ddlSubPage, sql, "LinkURL", "ID", _schema, ref strError);
        }
        else
        {
            lblMsg.Text = "Please select a site";
            ddlSite.Focus();
            dvData.Visible = false;
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert1", "<script>alert('Please select a site');</script>");

        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Upd")
        {
            TextBox txtpageid = (TextBox)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtpageid");

            Label lblid = (Label)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid");
            //Button btnUpdate = (Button)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnUpdate");
            LinkButton btnUpdate = (LinkButton)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnUpdate");

            btnUpdate.Attributes.Add("onclick", "javascript:return ValidateText('" + txtpageid.ClientID + "');");

            //btnGo.Attributes.Add("onclick", "javascript:ValidateText(" + btnGo.ID + ")");
            //ScriptManager sm = ScriptManager.GetCurrent(Page);
            //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "MYScript", "ValidateText(" + txtUrlName.ClientID + ")", true);

            DataManipulationClass objData = new DataManipulationClass();
            string stError = "";
            if (txtpageid.Text != "")
            {
                if (objData.UpdateLinkExchangeMaster_Pageid(Convert.ToInt32(lblid.Text), Convert.ToInt32(txtpageid.Text), ref stError))
                {

                    Response.Redirect("Insert-Page-Number.aspx?flag=update");
                }
                else
                {

                }
            }
            else
            {
                lblMsg.Text = "Please enter Page Id";
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert13", "<script>alert('Please enter Page Id');</script>");
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert22", "<script>alert('Please enter Page Id');</script>", true);

                string[] _strContNameAll = txtpageid.ClientID.ToString().Split(new char[] { '_' });
                int _intTempId = Convert.ToInt32(_strContNameAll[1].Substring(3)) + 1;
                string _strId = "";
                if (_intTempId < 10)
                {
                    _strId = "GridView1_ctl0" + Convert.ToString(_intTempId) + "_txtpageid";
                }
                else
                {
                    _strId = "GridView1_ctl" + Convert.ToString(_intTempId) + "_txtpageid";
                }

                ScriptManager sm = ScriptManager.GetCurrent(Page);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "MYScript", "alert('Please enter Page Id');document.getElementById(\"" + _strId + "\").focus();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "selectAndFocus", "$get('" + txtpageid.ClientID + "').focus();$get('" + txtpageid.ClientID + "').select();", true);
                return;
            }



        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSubPage.SelectedValue != "")
        {
            lblSearch.Text = "";
            lblMsg.Text = "";
            //string strSQL = "SELECT " +
            //             "LinkExchange.LinkExchangeMaster.Id AS [id], " +
            //             "LinkExchange.SubPage.SubPageName AS [subpagename], " +
            //             "LinkExchange.SubPage.LinkURL AS [linkurl], " +
            //             "LinkExchange.WebSite_Master.Name AS [sitename], " +
            //             "LinkExchange.WebSite_Master.SiteURL AS [siteurl], " +
            //             "LinkExchange.LinkExchangeMaster.HTMLcode AS [htmlcode], " +
            //             "LinkExchange.LinkExchangeMaster.OurAdId AS [adid], " +
            //             "LinkExchange.LinkExchangeMaster.email AS [email], " +
            //             "LinkExchange.LinkExchangeMaster.Reciprocal AS [reci], " +
            //             "LinkExchange.LinkExchangeMaster.Status AS [status] " +
            //             "FROM " +
            //             "LinkExchange.LinkExchangeMaster " +
            //             "INNER JOIN LinkExchange.SubPage ON (LinkExchange.LinkExchangeMaster.SubPageID = LinkExchange.SubPage.Id) " +
            //             "INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId = LinkExchange.WebSite_Master.ID) " +
            //             "WHERE " +
            //             "(LinkExchange.LinkExchangeMaster.HTMLCode LIKE '%" + txtSearchString.Value + "%') AND  (LinkExchange.LinkExchangeMaster.websiteid ='" + ddlSite.SelectedValue + "') AND LinkExchange.LinkExchangeMaster.SubPageID ='" + ddlSubPage.SelectedValue + "' AND " +
            //             "(LinkExchange.WebSite_Master.UserId = '" + Convert.ToString(Session["UserID"]) + "') " +
            //             " and LinkExchange.LinkExchangeMaster.pageid is null "+
            //             " ORDER BY " +
            //             "LinkExchange.LinkExchangeMaster.Status, " +
            //             "LinkExchange.LinkExchangeMaster.Type";
            string strSQL = "SELECT " +
                        "LinkExchange.LinkExchangeMaster.Id AS [id], " +
                        "LinkExchange.LinkExchangeMaster.HTMLcode AS [htmlcode], " +
                         "LinkExchange.LinkExchangeMaster.pageid , " +
                        "LinkExchange.LinkExchangeMaster.Reciprocal AS [reci], " +
                         "LinkExchange.LinkExchangeMaster.email AS [email] " +
                        "FROM " +
                        "LinkExchange.LinkExchangeMaster " +
                //   "INNER JOIN LinkExchange.SubPage ON (LinkExchange.LinkExchangeMaster.SubPageID = LinkExchange.SubPage.Id) " +
                        "INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId = LinkExchange.WebSite_Master.ID) " +
                        "WHERE " +
                        "(LinkExchange.LinkExchangeMaster.HTMLCode LIKE '%" + txtSearchString.Value + "%') AND  (LinkExchange.LinkExchangeMaster.websiteid ='" + ddlSite.SelectedValue + "') AND LinkExchange.LinkExchangeMaster.SubPageID ='" + ddlSubPage.SelectedValue + "' AND " +
                        "(LinkExchange.WebSite_Master.UserId = '" + Convert.ToString(Session["UserID"]) + "') " +
                         " and  LinkExchange.LinkExchangeMaster.Status=2 " +
                //  " and LinkExchange.LinkExchangeMaster.pageid is null " +
                        " ORDER BY " +
                        "LinkExchange.LinkExchangeMaster.Status, " +
                        "LinkExchange.LinkExchangeMaster.Type";

            string strError = "";
            DataManipulationClass objdata = new DataManipulationClass();
            DataTable dtGrid = new DataTable();
            dtGrid = objdata.FillDataTable(strSQL, ref strError);
            if (dtGrid.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = dtGrid;
                GridView1.DataBind();
            }
            else
            {
                lblSearch.Text = "No Record found";
                GridView1.Visible = false;

            }
        }
        else
        {
            lblMsg.Text = "Please select SubPage ";
            ddlSubPage.Focus();
            return;
        }

    }


    public string get_data(string reci, string email)
    {
        string email_Reci = "";
        if (email.Length > 0)
        {
            email_Reci = email + "<br>";
        }
        else
        {
            email_Reci += "<br>";
        }
        if (reci.Length > 0)
        {
            email_Reci += "<a href='" + reci + "' target='_blank'>" + reci + "</a><br>";
        }
        else
        {
            email_Reci += "<br>";
        }
        return email_Reci;
    }
    protected void ddlSite_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSite.SelectedValue != "")
        {
            dvData.Visible = true;
            GridView1.Visible = false;
            lblMsg.Text = "";
            lblSearch.Text = "";
            string sql = "SELECT " +
                                       "LinkExchange.SubPage.ID, " +
                                       "LinkExchange.SubPage.LinkURL " +
                                       "FROM " +
                                       "LinkExchange.SubPage " +
                                       "WHERE LinkExchange.SubPage.Websiteid='" + ddlSite.SelectedValue + "'";
            string strError = "";
            string _schema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);

            DataManipulationClass objdata = new DataManipulationClass();

            objdata.DropDownFill(ddlSubPage, sql, "LinkURL", "ID", _schema, ref strError);
        }
        else
        {
            lblMsg.Text = "Please select a site";
            ddlSite.Focus();
            dvData.Visible = false;
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert1", "<script>alert('Please select a site');</script>");

        }
    }
}
