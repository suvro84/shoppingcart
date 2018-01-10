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

public partial class ucMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             //dvMenu.InnerHtml = generatemenu();
            //dvMenu.InnerHtml = generaRootLevel();
            //generatemenu();
            commonclass objmenu = new commonclass();
            DataTable dtmenu = new DataTable();
            string strSql = "select  * from tblMenuMaster";
            dtmenu = objmenu.Fetchrecords(strSql);

            dvMenu.InnerHtml = generaRootLevel(dtmenu);
           
        }
    }

   
    public string generaRootLevel(DataTable dtmenu)
    {

 
  
        StringBuilder sbMenu = new StringBuilder();

    
        if (dtmenu.Rows.Count > 0)
        {

            sbMenu.Append("<ul class=\"MM\" id=\"Menu1\">");
         
            foreach (DataRow drmenu in dtmenu.Rows)
     
            {
              
            
                if (Convert.ToInt32(drmenu["pid"]) == 0)
                {
                    sbMenu.Append("<li>");
                    sbMenu.Append("<a href=\"" + drmenu["url"] + "\">" + drmenu["title"] + "</a>");
                    //sbMenu.Append("<a href=\"" + dtmenu.Rows[i]["url"] + "\">" + dtmenu.Rows[i]["title"] + "</a>");


                    sbMenu.Append("<ul>");
                     sbMenu.Append(generatechild(Convert.ToString(drmenu["menuid"]), dtmenu));
                    // generatechild(Convert.ToString(dtmenu.Rows[i]["menuid"]), dtmenu);
                    sbMenu.Append("</ul>");
                    sbMenu.Append("</li>");

                
                }


          
            }
            sbMenu.Append("</ul>");

        }
        return sbMenu.ToString();
        //dvMenu.InnerHtml = sbMenu.ToString();

    }

    public string generatechild(string pid, DataTable dtmenu)
    {

        StringBuilder sbMenu = new StringBuilder();
        DataRow[] drmenu = dtmenu.Select("pid='" + pid + "'");
        if (drmenu.Length > 0)
        {

            for (int i = 0; i < drmenu.Length; i++)
            {
                sbMenu.Append("<li>");
                sbMenu.Append("<a href=\"" + drmenu[i]["url"].ToString() + "\">" + drmenu[i]["title"].ToString() + "</a>");
            

                DataRow[] drmenu1 = dtmenu.Select("pid='" + drmenu[i]["menuid"].ToString() + "'");
                if (drmenu1.Length > 0)
                {
                    sbMenu.Append("<ul>");
                     sbMenu.Append(generatechild(Convert.ToString(drmenu[i]["menuid"]), dtmenu));
                    sbMenu.Append("</ul>");
                }
                sbMenu.Append("</li>");


            }


        }
        return sbMenu.ToString();
    }
}
