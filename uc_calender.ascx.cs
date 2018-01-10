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

public partial class uc_calender : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["CreateDateTime"] != null)
            {
                //  calSource.SelectedDate = Convert.ToDateTime(Request.QueryString["CreateDateTime"]);
                calSource.VisibleDate = Convert.ToDateTime(Request.QueryString["CreateDateTime"]);
               // Session["VisibleDate"] = calSource.VisibleDate;
            }
            //Calendar cal = new Calendar();
            //hdVisibleDate.Value = cal.SelectedDate;



          //  calTextLbl.Text = calSource.TodaysDate.Month.ToString();
            get_NextValue();
            hdNext.Value = calSource.TodaysDate.Month.ToString();
            Response.Write(hdNext.Value);
        }

      

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
        Session["VisibleDate"] = calSource.VisibleDate;
        hdNext.Value =Convert.ToString(e.Day.Date.Month);


    }
    protected void lnkPrev_Click(object sender, EventArgs e)
    {
       
       // calSource.VisibleDate = calSource.VisibleDate.AddMonths(-1);
     //   calSource.VisibleDate = new DateTime(calSource.VisibleDate.Year - 1, calSource.VisibleDate.Month - 1, calSource.VisibleDate.Day - 1);

        //calSource.SelectedDates.Add(new DateTime(calSource.VisibleDate.Year, calSource.VisibleDate.Month-1, calSource.VisibleDate.Day));
      //  calSource.SelectedDates.Add(new DateTime(calSource.VisibleDate.Year, calSource.VisibleDate.Month, calSource.VisibleDate.Day));

        int intChangedMonth;

        if (calSource.VisibleDate == DateTime.MinValue)
        {
            calSource.VisibleDate = DateTime.Today.AddMonths(-1);
        }
        else
        {
            calSource.VisibleDate = calSource.VisibleDate.AddMonths(-1);
        }
        if (calTextLbl.Text == "")
        {
            calTextLbl.Text = calSource.TodaysDate.Month.ToString();
        }
        else
        {
            intChangedMonth = Convert.ToInt32(calTextLbl.Text);
            if (intChangedMonth == 1) //Jan. becoming Dec.
            {
                intChangedMonth = 12;
            }
            else
            {
                intChangedMonth--;
            }
            calTextLbl.Text = intChangedMonth.ToString();
        }
    
    
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
       
    }


    public void get_NextValue()
    {
     int intChangeMonth;

        if (calSource.VisibleDate == DateTime.MinValue)
        {
            calSource.VisibleDate = DateTime.Today.AddMonths(1);
        }
        else
        {
            calSource.VisibleDate = calSource.VisibleDate.AddMonths(1);
        }
        if (calTextLbl.Text == "")
        {
            calTextLbl.Text = calSource.TodaysDate.Month.ToString();
            hdNext.Value = calSource.TodaysDate.Month.ToString();
        }
        else
        {
            intChangeMonth = Convert.ToInt32(calTextLbl.Text);
            if (intChangeMonth == 12) //Dec. becoming Jan.
            {
                intChangeMonth = 1;
            }
            else
            {
                intChangeMonth++;
            }
          //  calTextLbl.Text = intChangeMonth.ToString();
            hdNext.Value = intChangeMonth.ToString();

        }
    }
}
