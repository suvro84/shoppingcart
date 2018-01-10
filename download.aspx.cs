using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Configuration;

public partial class download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string filename = "";

            if (Request.QueryString["file"] != null)
            {

                filename = Convert.ToString(Request.QueryString["file"]);
                if (ReadDownloadFile(filename))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "alert2", "<script type='text/javascript' language='javascript'>window.close();</script>");
                }
            }
            //lblReports.Text = getReports();
            hdReports.Value = "";
            hdReports.Value = getReports();
        }

    }


    private bool ReadDownloadFile(string filename)
    {
        bool bflag = false;
        //try
        //{
        //string path = "F:/office project/mjAdmin/Reports/" + filename;
        //  string path = "F:/office project/MoneyJugglers/Reports/" + filename;

        string path = "C:/Inetpub/vhosts/moneyjugglers.com/httpdocs/Reports/" + filename;


        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);


            Response.End();
            //  Response.Write("<script type='text/javascript' language='javascript'>window.close();</script>");
            bflag = true;
        }
        else
        {
            bflag = false;
            //Response.Write("This file does not exist.");
        }
        //}
        //catch (Exception exp)
        //{
        //    bflag = false;
        //}
        return bflag;
    }

    //public string getReports()
    //{
    //    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DB_Conn"].ToString());
    //    StringBuilder sbmyscript = new StringBuilder();

    //    StringBuilder sbReports = new StringBuilder();
    //    //sbReports.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#FDECF3\">");

    //    int intcounter = 0;
    //    string FileType = string.Empty;
    //    string strSQL = "select top(10)* from Company_Report_Master where Status=1 order by ID desc";
    //    if (conn.State == ConnectionState.Closed)
    //    {
    //        conn.Open();
    //    }
    //    try
    //    {
    //        //SqlCommand cmd = new SqlCommand(strSQL, conn);
    //        //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //        DataTable dtReports = new DataTable();
    //        SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
    //        da.Fill(dtReports);


    //        //if (dr.HasRows)
    //        //{
    //        if (dtReports.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dtReports.Rows.Count; i++)
    //            {

    //                switch (Convert.ToString(dtReports.Rows[i]["FileType"]))
    //                {
    //                    case ".xls":
    //                        FileType = "icon2.gif";
    //                        break;
    //                    case ".doc":
    //                        FileType = "icon.gif";
    //                        break;
    //                    case ".pdf":
    //                        FileType = "icon1.gif";
    //                        break;
    //                }

    //                sbmyscript.Append("titlea[" + i + "] = " + Convert.ToString(dtReports.Rows[i]["ReportName"]) + ";" + (char)13);
    //                sbmyscript.Append("texta[" + i + "] = " + Convert.ToString(dtReports.Rows[i]["CompanyName"]) + ";" + (char)13);
    //                sbmyscript.Append("linka[" + i + "] = <a href=\"download1.aspx?file=" + Convert.ToString(dtReports.Rows[i]["CompanyName"]) + "_" + Convert.ToString(dtReports.Rows[i]["ID"]) + Convert.ToString(dtReports.Rows[i]["FileType"]) + "\"><img src=\"Pictures/" + FileType + "\" alt=\"\" width=\"19\" height=\"19\" border=\"0\" /></a>;" + (char)13);



    //            }
    //        }

    //        hdReports.Value = sbmyscript.ToString();


    //        sbReports.Append("<div id=\"disspageie\" style=\"position:absolute;background:#667DB3;width:180; height:220;left:0; top:0;margin:0px;overflow:hidden;padding:0px;border-style:solid; border-width:1px; border-color:#5C5C5C;\">");
    //        sbReports.Append("<div id=\"spageie\" style=\"position:absolute; width:180; height:220; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\">");
    //        sbReports.Append("</div> ");
    //        sbReports.Append("</div> ");


    //        //StringBuilder sbPageScript = new StringBuilder();

    //        //sbPageScript.Append("<script language=\"javascript\">" + (char)13);
    //        //sbPageScript.Append("var OPB=false;uagent = window.navigator.userAgent.toLowerCase();" + (char)13);
    //        //sbPageScript.Append("OPB=(uagent.indexOf('opera') != -1)?true:false;if((document.all)&&(OPB==false))" + (char)13);
    //        //sbPageScript.Append("{document.write(\"<div id=\"spage\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-style:solid; border-width:1px; border-color:#5C5C5C;overflow:hidden;\"><div id=\"spagens\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\"></div></div>\");" + (char)13);
    //        //sbPageScript.Append("document.write(\"<scr\"+\"ipt language=\"javascript\" sr" + "c=\"js/scroll.js\"></scr\"+\"ipt>\");}" + (char)13);
    //        //sbPageScript.Append("else{document.write(\"<div id=\"spagensbrd\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-style:solid; border-width:1px; border-color:#5C5C5C;overflow:hidden;\"><div id=\"spagens\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\"></div></div>\");" + (char)13);
    //        //sbPageScript.Append("document.write(\"<scr\"+\"ipt language=\"javascript\" sr\"+\"c=\"js/scroll.js\"></scr\"+\"ipt>\");}" + (char)13);
    //        //sbPageScript.Append("</script>");

    //        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sbPageScript.ToString());


    //    }


    //    catch (Exception ex)
    //    {
    //        sbReports.Remove(0, sbReports.Length);
    //        sbReports.Append("");
    //        conn.Close();
    //        Response.Write(ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        conn.Close();

    //    }
    //    return sbReports.ToString();

    //}

    public string getReports()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DB_Conn"].ToString());
        StringBuilder sbmyscript = new StringBuilder();

        StringBuilder sbReports = new StringBuilder();
        //sbReports.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#FDECF3\">");

        int intcounter = 0;
        string FileType = string.Empty;
        string strSQL = "select top(20)* from Company_Report_Master where Status=1 order by ID desc";
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        try
        {
            //SqlCommand cmd = new SqlCommand(strSQL, conn);
            //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable dtReports = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
            da.Fill(dtReports);


            //if (dr.HasRows)
            //{
            if (dtReports.Rows.Count > 0)
            {
                for (int i = 0; i < dtReports.Rows.Count; i++)
                {

                    switch (Convert.ToString(dtReports.Rows[i]["FileType"]))
                    {
                        case ".xls":
                            FileType = "icon2.gif";
                            break;
                        case ".doc":
                            FileType = "icon.gif";
                            break;
                        case ".pdf":
                            FileType = "icon1.gif";
                            break;
                    }

                    //sbmyscript.Append("titlea[" + i + "] = \"" + Convert.ToString(dtReports.Rows[i]["ReportName"]) + "\";" + (char)13);

                    //   titlea[0] = "Morning Buzz 22<a href='download1.aspx?file='Morning Buzz 2222_15.pdf'><img src='Pictures' +icon1.gif alt='' width='19' height='19' border='0'></a>;
                    // texta[0] = "<a href=\"test.aspx?id=5\"><img border=\"0\" alt=\"\" src=\"images/test.jpg\" /></a>";
                    //sbmyscript.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");




                    //sbmyscript.Append("titlea[" + i + "] = \"" + Convert.ToString(dtReports.Rows[i]["ReportName"]) + "<a href='download1.aspx?file=" + Convert.ToString(dtReports.Rows[i]["CompanyName"]) + "_" + Convert.ToString(dtReports.Rows[i]["ID"]) + Convert.ToString(dtReports.Rows[i]["FileType"]) + "'><img src='Pictures/" + FileType + "' alt='' width='19' height='19' border='0'></a>\"" + (char)13);
                    sbmyscript.Append("titlea[" + i + "] = \"<a href='download1.aspx?file=" + Convert.ToString(dtReports.Rows[i]["CompanyName"]) + "_" + Convert.ToString(dtReports.Rows[i]["ID"]) + Convert.ToString(dtReports.Rows[i]["FileType"]) + "'><img src='Pictures/" + FileType + "' alt='' width='19' height='19' border='0' style='margin:10px 5px 0 0;'></a>" + Convert.ToString(dtReports.Rows[i]["ReportName"]) + "\"" + (char)13);


                    sbmyscript.Append("texta[" + i + "] =\"<span style='margin:0 0 0 30px; font-weight:bold; display:block;'>" + Convert.ToString(dtReports.Rows[i]["CompanyName"]) + "</span>\";" + (char)13);
                    //  sbmyscript.Append("linka[" + i + "] = \"<a href=\"download1.aspx?file=" + Convert.ToString(dtReports.Rows[i]["CompanyName"]) + "_" + Convert.ToString(dtReports.Rows[i]["ID"]) + Convert.ToString(dtReports.Rows[i]["FileType"]) + "\"><img src=\"Pictures/" + FileType + "\" alt=\"\" width=\"19\" height=\"19\" border=\"0\" /></a>\";" + (char)13);
                    //  sbmyscript.Append("linka[" + i + "] = \"<a href='download1.aspx?file=" + Convert.ToString(dtReports.Rows[i]["CompanyName"]) + "_" + Convert.ToString(dtReports.Rows[i]["ID"]) + Convert.ToString(dtReports.Rows[i]["FileType"]) + "'><img src='Pictures/" + FileType + "' alt='' width='19' height='19' border='' /></a>\";" + (char)13);

                    sbmyscript.Append("linka[" + i + "] = \"#\";" + (char)13);
                    sbmyscript.Append("trgfrma[" + i + "] = \"#\";" + (char)13);

                    //sbmyscript.Append("</table>");

                }
            }

            //hdReports.Value = sbmyscript.ToString();


            //sbReports.Append("<div id=\"disspageie\" style=\"position:absolute;background:#667DB3;width:180; height:220;left:0; top:0;margin:0px;overflow:hidden;padding:0px;border-style:solid; border-width:1px; border-color:#5C5C5C;\">");
            //sbReports.Append("<div id=\"spageie\" style=\"position:absolute; width:180; height:220; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\">");
            //sbReports.Append("</div> ");
            //sbReports.Append("</div> ");


            //StringBuilder sbPageScript = new StringBuilder();

            //sbPageScript.Append("<script language=\"javascript\">" + (char)13);
            //sbPageScript.Append("var OPB=false;uagent = window.navigator.userAgent.toLowerCase();" + (char)13);
            //sbPageScript.Append("OPB=(uagent.indexOf('opera') != -1)?true:false;if((document.all)&&(OPB==false))" + (char)13);
            //sbPageScript.Append("{document.write(\"<div id=\"spage\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-style:solid; border-width:1px; border-color:#5C5C5C;overflow:hidden;\"><div id=\"spagens\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\"></div></div>\");" + (char)13);
            //sbPageScript.Append("document.write(\"<scr\"+\"ipt language=\"javascript\" sr" + "c=\"js/scroll.js\"></scr\"+\"ipt>\");}" + (char)13);
            //sbPageScript.Append("else{document.write(\"<div id=\"spagensbrd\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-style:solid; border-width:1px; border-color:#5C5C5C;overflow:hidden;\"><div id=\"spagens\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\"></div></div>\");" + (char)13);
            //sbPageScript.Append("document.write(\"<scr\"+\"ipt language=\"javascript\" sr\"+\"c=\"js/scroll.js\"></scr\"+\"ipt>\");}" + (char)13);
            //sbPageScript.Append("</script>");

            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sbPageScript.ToString());


        }


        catch (Exception ex)
        {
            sbReports.Remove(0, sbReports.Length);
            sbReports.Append("");
            conn.Close();
            Response.Write(ex.Message.ToString());
        }
        finally
        {
            conn.Close();

        }
        //  return sbReports.ToString();
        return sbmyscript.ToString();


    }
}
