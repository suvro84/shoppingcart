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
using System.Drawing;

public partial class calender : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
    //{

    //    int idCount = 1;

    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("Id", Type.GetType("System.Int32"));
    //    dt.Columns.Add("EventStartDate", Type.GetType("System.DateTime"));
    //    DataRow dr;

    //    // Yesterday's Events
    //    dr = dt.NewRow();
    //    dr["Id"] = idCount++;
    //    dr["EventStartDate"] = DateTime.Now.AddDays(1);
    //    dt.Rows.Add(dr);

    //    //foreach (DataRow drdate in dt.Rows)
    //    //{
    //    //    if (e.Day.Date == Convert.ToDateTime(drdate["EventStartDate"]))
    //    //    {
    //    //        e.Cell.BackColor = System.Drawing.Color.Yellow;
    //    //    }
 

    //    //}


    //    //if (!e.Day.IsOtherMonth)
    //    //{
    //    //    foreach (DataRow drdate in dt.Rows)
    //    //    {
    //    //        if ((drdate["EventStartDate"].ToString() != DBNull.Value.ToString()))
    //    //        {
    //    //            DateTime dtEvent = (DateTime)drdate["EventStartDate"];
    //    //            if (dtEvent.Equals(e.Day.Date))
    //    //            {
    //    //                e.Cell.BackColor = Color.PaleVioletRed;
    //    //            }
    //    //        }
    //    //    }
    //    //}

    //    foreach (DataRow drdate in dt.Rows)
    //    {
    //        if (e.Day.Date == Convert.ToDateTime(drdate["EventStartDate"]))
    //        {
    //            e.Cell.BackColor = Color.PaleVioletRed;
    //            e.Day.IsSelectable = false;
    //        }
    //    }
    //    //foreach(DateTime dt in Calendar2.dates

    //    //if (e.Day.Date.Day == 5 && e.Day.Date.Month == 5)
    //    //{
    //    //    e.Cell.BackColor = System.Drawing.Color.Yellow;

    //    //    // Add some static text to the cell.
    //    //    Label lbl = new Label();
    //    //    lbl.Text = "<br>My Birthday!";
    //    //    e.Cell.Controls.Add(lbl);
    //    //}
    //}



    //private DataTable GetEvents()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("Id", Type.GetType("System.Int32"));
    //    dt.Columns.Add("EventStartDate", Type.GetType("System.DateTime"));
    //    dt.Columns.Add("EventEndDate", Type.GetType("System.DateTime"));
    //    dt.Columns.Add("EventHeader", Type.GetType("System.String"));
    //    dt.Columns.Add("EventDescription", Type.GetType("System.String"));
    //    dt.Columns.Add("EventForeColor", Type.GetType("System.String"));
    //    dt.Columns.Add("EventBackColor", Type.GetType("System.String"));

    //    int idCount = 1;

    //    DataRow dr;

    //    // Yesterday's Events
    //    dr = dt.NewRow();
    //    dr["Id"] = idCount++;
    //    dr["EventStartDate"] = DateTime.Now.AddDays(-1);
    //    dr["EventEndDate"] = DateTime.Now.AddDays(-1);
    //    dr["EventHeader"] = "My Yesterday's Single Day Event";
    //    dr["EventDescription"] = "My Yesterday's Single Day Event Details";
    //    dr["EventForeColor"] = "White";
    //    dr["EventBackColor"] = "Navy";
    //    dt.Rows.Add(dr);

    //    // Three Day's Event Starting Tomorrow
    //    dr = dt.NewRow();
    //    dr["Id"] = idCount++;
    //    dr["EventStartDate"] = DateTime.Now.AddDays(1);
    //    dr["EventEndDate"] = DateTime.Now.AddDays(+3);
    //    dr["EventHeader"] = "My Three Days Event";
    //    dr["EventDescription"] = "My Three Days Event Details, which starts tomorrow";
    //    dr["EventForeColor"] = "White";
    //    dr["EventBackColor"] = "Green";
    //    dt.Rows.Add(dr);


    //    return dt;
    //}



    protected DataTable FillDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ApplicationID",typeof(int));
        dt.Columns.Add("RewriteURLPath",typeof(string));
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

        //       if (Request.QueryString["CreateDateTime"] != null)
        //{
            calSource.SelectedDate = Convert.ToDateTime(Request.QueryString["CreateDateTime"]);

           // string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());

          //  string strSchema = Convert.ToString(ConfigurationManager.AppSettings["Schema"].ToString());
           
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
            }
     



    //}
}
