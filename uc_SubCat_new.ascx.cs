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

public partial class uc_SubCat_new : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        commonclass objcat = new commonclass();
        string Category_Id = "";
        if (Request.QueryString["catid"] != null)
        {
            Category_Id = Request.QueryString["catid"].ToString();
        }
        if (Category_Id != null)
        {
            DataTable dtCat = getDatable(Category_Id);

            string Parent_Category_id = "";
            string Category_Name = "";
            StringBuilder sboutput = new StringBuilder();

            if (getCatName(dtCat, ref Parent_Category_id, ref Category_Name, Category_Id))
            {
                DataView dvCat = new DataView(dtCat);
                //  dvCat.RowFilter = "Parent_Category_id='" + Category_Id + "'";
                dvCat.RowFilter = "Parent_Category_id='" + Category_Id + "'and Category_Id<>'" + Category_Id + "'";

                dvCat.Sort = "Category_Name";

                if (dvCat.ToTable().Rows.Count > 0)
                {
                    sboutput.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                    sboutput.Append("<tr><td><h1>SubCategories</h1></td></tr>");
                    foreach (DataRow drcat in dvCat.ToTable().Rows)
                    {
                        sboutput.Append("<tr>");
                        sboutput.Append("<td>");
                        if (drcat["Category_Id"].ToString() == Category_Id)
                        {

                            sboutput.Append("<a style=\"background-color:Red\" href=\"category.aspx?catid=" + drcat["Category_Id"].ToString() + "&pageno=1\">" + drcat["Category_Name"].ToString() + "</a>");

                        }
                        else
                        {
                            sboutput.Append("<a href=\"category.aspx?catid=" + drcat["Category_Id"].ToString() + "&pageno=1\">" + drcat["Category_Name"].ToString() + "</a>");

                        }
                      //  sboutput.Append("<a href=\"category.aspx?catid=" + drcat["Category_Id"].ToString() + "&pageno=1\">" + drcat["Category_Name"].ToString() + "</a>");
                        sboutput.Append("</td>");
                        sboutput.Append("</tr>");


                    }
                    sboutput.Append("</table>");
                    sboutput.Append("<br>");


                }


                if (Parent_Category_id != "0")
                {
                    // dvCat.RowFilter = "Parent_Category_id='" + Parent_Category_id + "'";
                    dvCat.RowFilter = "Parent_Category_id='" + Parent_Category_id + "'and Category_id <>'" + Category_Id + "'";

                    dvCat.Sort = "Category_Name";
                    if (getCatName(dtCat, ref Parent_Category_id, ref Category_Name, Parent_Category_id))
                    {

                        //dvCat.RowFilter = "Parent_Category_id='" + Parent_Category_id + "'";
                        //dvCat.Sort = "Category_Name";

                        if (dvCat.ToTable().Rows.Count > 0)
                        {
                            sboutput.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                            sboutput.Append("<tr><td><h1>" + Category_Name + "</h1></td></tr>");
                            foreach (DataRow drcat in dvCat.ToTable().Rows)
                            {
                                sboutput.Append("<tr>");
                                sboutput.Append("<td>");
                              //  sboutput.Append("<a href=\"category.aspx?catid=" + drcat["Category_Id"].ToString() + "&pageno=1\">" + drcat["Category_Name"].ToString() + "</a>");
                                if (drcat["Category_Id"].ToString() == Category_Id)
                                {

                                    sboutput.Append("<a style=\"background-color:Red\" href=\"category.aspx?catid=" + drcat["Category_Id"].ToString() + "&pageno=1\">" + drcat["Category_Name"].ToString() + "</a>");

                                }
                                else
                                {
                                    sboutput.Append("<a href=\"category.aspx?catid=" + drcat["Category_Id"].ToString() + "&pageno=1\">" + drcat["Category_Name"].ToString() + "</a>");

                                }
                                
                                
                                sboutput.Append("</td>");
                                sboutput.Append("</tr>");


                            }
                            sboutput.Append("</table>");
                            sboutput.Append("<br>");


                        }

                    }
                }




                lblSubCat.Text = sboutput.ToString();

            }


        }
    }

    public DataTable getDatable(string Category_Id)
    {
        commonclass objcat = new commonclass();
        //string   strsql = "select Category_id,Category_Name,Parent_Category_id from tblItemCategory_Web_Server where Parent_Category_id=" + Category_Id + "";
        string strsql = "select Category_id,Category_Name,Parent_Category_id from tblItemCategory_Web_Server";


        DataTable dtcat = objcat.Fetchrecords(strsql);
        return dtcat;
    }
    public bool getCatName(DataTable dt, ref string Parent_Category_id,ref  string Category_Name,string Category_Id)
    {
        bool bflag = false;
        if (dt.Rows.Count > 0)
        {
            DataRow[] drcat = dt.Select("Category_Id='" + Category_Id + "'");
            if (drcat.Length > 0)
            {
                Category_Name = drcat[0]["Category_Name"].ToString();
                Parent_Category_id = drcat[0]["Parent_Category_id"].ToString();
                bflag = true;
            }

        }
        else
        {
            bflag = false;

        }
        return bflag;
    }
}
