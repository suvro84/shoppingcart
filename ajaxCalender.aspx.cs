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

public partial class ajaxCalender : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            if (Request.Params["mode"] == "1")
            {
                // bool bflag = false;

               if (Convert.ToBoolean(Session["bflag"]) == false)
                {

                    calSource.VisibleDate = DateTime.Today.AddMonths(-1);
                    Session["bflag"] = true;
                    Session["vd"] = DateTime.Today.AddMonths(-1);
                }
                else
                {
                    calSource.VisibleDate = Convert.ToDateTime(Session["vd"]).AddMonths(-1);
                    Session["vd"] = calSource.VisibleDate;
                }


                Response.Write("1");
                Response.Write("~");


            }



            ///next


            else if (Request.Params["mode"] == "2")
            {

                if (Convert.ToBoolean(Session["bflag"]) == false)
                {

                    calSource.VisibleDate = DateTime.Today.AddMonths(+1);
                    Session["bflag"] = true;
                    Session["vd"] = DateTime.Today.AddMonths(+1);
                }
                else
                {
                    calSource.VisibleDate = Convert.ToDateTime(Session["vd"]).AddMonths(+1);
                    Session["vd"] = calSource.VisibleDate;
                }


                Response.Write("1");
                Response.Write("~");


            }
            else
            {
                Response.Write("0");
                Response.Write("~");
            }



        }
    }


    protected void calSource_DayRender(object sender, DayRenderEventArgs e)
    {

        string strError = "";
        DataTable _dtChapterArticle = new DataTable();

        _dtChapterArticle = FillDataTable();


        if (_dtChapterArticle.Rows.Count > 0)
        {

            if (!e.Day.IsOtherMonth)
            {

                foreach (DataRow dr in _dtChapterArticle.Rows)
                {
                    if ((dr["CreateDateTime"].ToString() != DBNull.Value.ToString()))
                    {
                        DateTime dtEvent = (DateTime)dr["CreateDateTime"];
                        if (dtEvent.Equals(e.Day.Date))
                        {
                            e.Day.IsSelectable = true;
                            e.Cell.BackColor = System.Drawing.Color.PaleVioletRed;
                            e.Cell.Controls.Clear();
                            HyperLink link = new HyperLink();
                            link.Text = e.Day.Date.Day.ToString();
                            link.ToolTip = e.Day.Date.Day.ToString();
                            //  link.NavigateUrl =  "image.aspx" + "/"+dr["RewriteURLPath"].ToString() + "/"+Convert.ToDateTime(dr["CreateDateTime"]).ToString("MM-dd-yyyy") + "/1.aspx";
                            link.NavigateUrl = "image.aspx?CreateDateTime=" + Convert.ToDateTime(dr["CreateDateTime"]).ToString("MM-dd-yyyy");

                            e.Cell.Controls.Add(link);


                        }
                        else
                        {
                            e.Day.IsSelectable = false;


                        }
                    }
                }
            }
            //If the month is not CurrentMonth then hide the Dates
            else
            {
                e.Cell.Text = "";
            }
        }
        else
        {
            calSource.Visible = false;

        }
        //if (Request.QueryString["CreateDateTime"] != null)
        //{

        //    e.Day.Date.AddDays(30);

        //}

    }


    protected DataTable FillDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ApplicationID", typeof(int));
        dt.Columns.Add("RewriteURLPath", typeof(string));
        dt.Columns.Add("CreateDateTime", typeof(DateTime));

        DataRow dr = dt.NewRow();
        dr[0] = "1";
        dr[1] = "Application1";
        dr[2] = "6-20-2009";
        dt.Rows.Add(dr);

        DataRow dr1 = dt.NewRow();
        dr1[0] = "2";
        dr1[1] = "Application2";
        dr1[2] = "6-15-2009";
        dt.Rows.Add(dr1);

        DataRow dr2 = dt.NewRow();
        dr2[0] = "3";
        dr2[1] = "Application3";
        dr2[2] = "6-17-2009";
        dt.Rows.Add(dr2);

        DataRow dr3 = dt.NewRow();
        dr3[0] = "4";
        dr3[1] = "Application4";
        dr3[2] = "5-01-2009";
        dt.Rows.Add(dr3);

        DataRow dr4 = dt.NewRow();
        dr4[0] = "5";
        dr4[1] = "Application5";
        dr4[2] = "5-10-2009";
        dt.Rows.Add(dr4);

        DataRow dr5 = dt.NewRow();
        dr5[0] = "6";
        dr5[1] = "Application6";
        dr5[2] = "5-15-2009";
        dt.Rows.Add(dr5);

        return dt;
        //hdtot.Value = Convert.ToString(dt.Rows.Count);
    }
}
