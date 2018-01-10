using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class LinkDetails_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["UserId"]) == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                string SubPageId = "";
                string SiteId = "";
                DataManipulationClass objdata = new DataManipulationClass();
                DataTable dtLinkDetail = new DataTable();
                string strPagination = "";
                string strError = "";
                int intPageNo = 1;
                string Status = "";
                int intTotalProd = 0;
                if (Request.QueryString["SiteId"] != null && Request.QueryString["Status"] != null)
                {

                    Status = Convert.ToString(Request.QueryString["Status"]);
                    string strCurrentSubPageName = "";
                    ArrayList arrExistProd = new ArrayList();
                    if (Request.QueryString["pageno"] != null)
                    {
                        intPageNo = Convert.ToInt32(Request.QueryString["pageno"]);
                    }

                    if (objdata.FetchLinkDetailsViewData(ref dtLinkDetail, Convert.ToString(Request.QueryString["SiteId"]), Convert.ToString(Request.QueryString["SubPageId"]), Request.QueryString["Status"], ref intTotalProd, intPageNo, 25, ref  strPagination, ref  strError))
                    {

                        rptLinkDetail.Visible = true;
                        rptLinkDetail.DataSource = dtLinkDetail;
                        rptLinkDetail.DataBind();

                        foreach (RepeaterItem rpt in rptLinkDetail.Items)
                        {
                            HtmlImage btnSendMail = (HtmlImage)rpt.FindControl("btnSendMail");
                            HtmlGenericControl spReject = (HtmlGenericControl)rpt.FindControl("spReject");
                            HtmlGenericControl spApproved = (HtmlGenericControl)rpt.FindControl("spApproved");
                            HtmlGenericControl spViewOurLink = (HtmlGenericControl)rpt.FindControl("spViewOurLink");
                            HtmlGenericControl hdReciprocal = (HtmlGenericControl)rpt.FindControl("hdReciprocal");
                            //if (hdReciprocal.InnerHtml.ToString().Trim() != "")
                            //{
                            //    spViewOurLink.InnerHtml = "<a href=" + hdReciprocal.InnerHtml + " target=\"_blank\">Our Link</a>";
                            //}
                            //else
                            //{
                            //}
                            Literal lit_SlNo = (Literal)rpt.FindControl("lit_SlNo");
                            if (Status == "2")
                            {
                                //btnApproved.Visible = false;
                                spApproved.Visible = false;
                            }
                            if (Status == "3")
                            {
                                //btnApproved.Visible = false;
                                spApproved.Visible = false;
                                spReject.Visible = false;
                            }
                            HtmlGenericControl spSubPageName = (HtmlGenericControl)rpt.FindControl("spSubPageName");
                            //  strCurrentSubPageName = Convert.ToString(drCat["SubPageName"].ToString());
                            strCurrentSubPageName = spSubPageName.InnerHtml.ToString().Trim();
                            if (!arrExistProd.Contains(strCurrentSubPageName))
                            {
                                spSubPageName.InnerHtml = strCurrentSubPageName;
                                arrExistProd.Add(strCurrentSubPageName);
                            }
                            else
                            {
                                spSubPageName.InnerHtml = "";
                            }

                        }

                        lblPage.Text = strPagination;

                    }

                    hdSiteId.Value = Convert.ToString(Request.QueryString["SiteId"]);
                    hdStatus.Value = Convert.ToString(Request.QueryString["Status"]);
                    hdPagecount.Value = Convert.ToString(intTotalProd);
                    hdFrom.Value = Convert.ToString(((intPageNo - 1) * 25) + 1);
                    hdTo.Value = Convert.ToString(intPageNo * 25);
                    lblSiteName.Text = objdata.getSiteName(Convert.ToString(Request.QueryString["SiteId"]));
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert1", "<script>document.getElementById(\"dvRpt\").style.display=\"block\"</script>");
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert2", "<script>document.getElementById(\"dvSubPageWise\").style.display=\"none\"</script>");


                }
                else if (Request.QueryString["SubPageId"] != null && Request.QueryString["Status"] != null)
                {
                    if (Request.QueryString["pageno"] != null)
                    {
                        intPageNo = Convert.ToInt32(Request.QueryString["pageno"]);
                    }


                    if (objdata.FetchLinkDetailsViewData(ref dtLinkDetail, Convert.ToString(Request.QueryString["SiteId"]), Convert.ToString(Request.QueryString["SubPageId"]), Request.QueryString["Status"], ref intTotalProd, intPageNo, 25, ref  strPagination, ref  strError))
                    {

                        if (dtLinkDetail.Rows.Count > 0)
                        {
                            rptSubPageWise.Visible = true;
                            //  dtLinkDetail = getSerialNumberToDataTable(dtLinkDetail, intInner);
                            rptSubPageWise.DataSource = dtLinkDetail;
                            rptSubPageWise.DataBind();

                            foreach (RepeaterItem rpt in rptSubPageWise.Items)
                            {
                                HtmlImage btnSendMail = (HtmlImage)rpt.FindControl("btnSendMail");
                                HtmlGenericControl spReject = (HtmlGenericControl)rpt.FindControl("spReject");
                                HtmlGenericControl spApproved = (HtmlGenericControl)rpt.FindControl("spApproved");

                                HtmlGenericControl spViewOurLink = (HtmlGenericControl)rpt.FindControl("spViewOurLink");
                                HtmlGenericControl hdReciprocal = (HtmlGenericControl)rpt.FindControl("hdReciprocal");

                                //if (hdReciprocal.InnerHtml.ToString().Trim() != "")
                                //{
                                //    spViewOurLink.InnerHtml = "<a href=" + hdReciprocal.InnerHtml + " target=\"_blank\">Our Link</a>";
                                //}
                                //else
                                //{
                                //}
                                if (Status == "2")
                                {
                                    spApproved.Visible = false;
                                }
                                if (Status == "3")
                                {
                                    spApproved.Visible = false;
                                    spReject.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            rptSubPageWise.Visible = false;
                        }

                        lblPageSubPageWise.Text = strPagination;

                    }
                    hdSubPageId.Value = Convert.ToString(Request.QueryString["SubPageId"]);
                    hdStatus.Value = Convert.ToString(Request.QueryString["Status"]);
                    hdPagecount.Value = Convert.ToString(intTotalProd);
                    hdFrom.Value = Convert.ToString(((intPageNo - 1) * 25) + 1);
                    hdTo.Value = Convert.ToString(intPageNo * 25);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert1", "<script>document.getElementById(\"dvRpt\").style.display=\"none\"</script>");
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert2", "<script>document.getElementById(\"dvSubPageWise\").style.display=\"block\"</script>");
                    lblSiteName.Text = objdata.getSubPageName(Convert.ToString(Request.QueryString["SubPageId"]));
                }
            }
        }

    }
    public string get_MailCount(int LinkId, string SlNo)
    {
        DataManipulationClass objdata = new DataManipulationClass();
        string mailcount = "";
        string mailtime = "";
        StringBuilder sbMail = new StringBuilder();
        if (objdata.get_MailCount(LinkId, ref mailcount, ref mailtime))
        {
            if (mailtime != "")
            {
                sbMail.Append("<a href=\"javascript:funOpenMailContentDiv(" + LinkId + "," + SlNo + ");\">" + mailcount + " Mails send against this Exchange. Last email sent " + Convert.ToDateTime(mailtime).ToString("MMMM dd, yyyy h:mm tt") + "</a>");
            }
            else
            {
                sbMail.Append(mailcount + " Mails send against this Exchange.");
            }
        }
        return sbMail.ToString();
    }
}
