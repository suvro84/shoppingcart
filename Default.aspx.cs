using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class _Default : System.Web.UI.Page
{

    commonclass myobj = new commonclass();
    public static int cur;

    string strSQL = null;
    DataView dv = null;
    DataTable dt = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDatalist();
        }
    }

    public void bindDatalist()
    {
        int intfrompage = 0;
        int inttopage=0 ;
        int intlimit = 10;
        strSQL = "select * from item";

        //dt=  MyDLL.FetchRecords(strSQL);
        dt = myobj.Fetchrecords(strSQL);
        dv = dt.DefaultView;
        //PagedDataSource pd = new PagedDataSource();


        //pd.AllowPaging = true;
        //pd.DataSource = dv;
        //pd.PageSize = 5;

        //int tot = pd.PageCount;
        //pd.CurrentPageIndex = cur;

        //btnnext.Enabled = !pd.IsLastPage;
        //btnprev.Enabled = !pd.IsFirstPage;
        //if (dt.Rows.Count > 0)
        //{
        //dtlstdisplay.DataSource = pd;
        //dtlstdisplay.DataBind();
        //}


        int selectedpage = 1;
        if (Request.QueryString["selectedpageno"] != null)
        {
            selectedpage = Convert.ToInt32(Request.QueryString["selectedpageno"]);
        }

        if (Convert.ToInt32(dt.Rows.Count) > 10)
        {

            lblpage.Text = get_pagination(Convert.ToInt32(dt.Rows.Count), ref intfrompage, ref inttopage, intlimit, selectedpage);
            DataTable dtnew = new DataTable();


            dtnew = dt.Clone();
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        int counter = 1;
            //        while (counter >= intfrompage && counter <= inttopage)
            //        {
            //            counter++;
            //            dtnew.ImportRow(dr);
            //        }


            //    }
            //}
            //if (intfrompage == 1)
            //{
            //    for (int i = 1; i <= intlimit; i++)
            //    {
            //        DataRow dr = dt.Rows[i];


            //        dtnew.ImportRow(dr);

            //    }

            //}
            if (intfrompage >= Convert.ToInt32(dt.Rows.Count))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = intfrompage - 1; i < Convert.ToInt32(dt.Rows.Count); i++)
                    {
                        DataRow dr = dt.Rows[i];


                        dtnew.ImportRow(dr);

                    }
                }

            }





            else
            {
                for (int i = intfrompage - 1; i <= inttopage - 1; i++)
                {
                    DataRow dr = dt.Rows[i];


                    dtnew.ImportRow(dr);

                }
            }
            dtlstdisplay.DataSource = dtnew;
            dtlstdisplay.DataBind();
        }
        else
        {
            dtlstdisplay.DataSource = dt;
            dtlstdisplay.DataBind();
        }

    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        //cur = cur + 1;
        //bindDatalist();
        int next = Convert.ToInt32(Request.QueryString["selectedpageno"]) + 1;
        Response.Redirect("Default.aspx?selectedpageno=" + next);
    }
    protected void btnprev_Click(object sender, EventArgs e)
    {
        //cur = cur - 1;
        //bindDatalist();
        int prev = Convert.ToInt32(Request.QueryString["selectedpageno"]) - 1;
        Response.Redirect("Default.aspx?selectedpageno=" + prev);

    }
    protected string get_pagination(int totproduct,ref int intfrompage,ref int  inttopage, int intlimit,int selectedpage)
    {
       
       


      intfrompage = (selectedpage - 1) * intlimit + 1;
        inttopage = selectedpage * intlimit;
        StringBuilder sbpage = new StringBuilder();
        sbpage.Append(intfrompage + "-" + inttopage + "of" + totproduct);
        int nopages = 0;
        if (totproduct > intlimit)
        {
           
            if (totproduct % intlimit != 0)
            {
                nopages = totproduct / intlimit + 1;
            }
            else
            {

                nopages = totproduct / intlimit;
            }
        }
        for (int i = 0; i < nopages; i++)
        {
            if (selectedpage == (i + 1))
            {
                sbpage.Append(i + 1);
            }
            else
            {
                sbpage.Append("<a href=\"Default.aspx?selectedpageno=" + (i + 1) + "\">" + (i + 1) + "</a>");

            }
        }

        if (intfrompage == 1)
        {
            btnprev.Enabled = false;
        }
        if (selectedpage == nopages)
        {

            btnnext.Enabled = false;
        }
        return sbpage.ToString();


    }
}