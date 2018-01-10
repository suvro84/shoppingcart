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
//using ImageHandler;


public partial class Scroll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hdReports.Value = getReports();
    }

    public string getReports()
    {
        StringBuilder sbReports = new StringBuilder();

        sbReports.Append("titlea[0]=\"Commodity Outlook <a href='calender.aspx?id=5'><img border='0' alt='' src='images/icon1.gif' /></a>\";\n");
       // sbReports.Append("titlea[0] = \"Commodity Outlook\";\n");
          // sbReports.Append("texta[0] = "<img alt=\"\" src=\"images/Citibank.jpg\" />";

        //sbReports.Append("texta[0] = \"<img  src= 'images/Citibank.jpg'  />\";\n");

        sbReports.Append("texta[0] = \"First\";\n");

        //sbReports.Append("linka[0] = \"first\";\n");
        // sbReports.Append("trgfrma[0] = \"_blank\";\n");

         sbReports.Append("titlea[1] = \"nifty view..\";\n");
         sbReports.Append("texta[1] = \"nifty view..\";\n");
         sbReports.Append("linka[1] = \"InterestingArticles.aspx?id=48\";\n");
         sbReports.Append("trgfrma[1] = \"_blank\";\n");

          sbReports.Append("titlea[2] = \"Stock Market calls dt 24-4-09\";\n");
          sbReports.Append("texta[2] = \"Stock Market calls dt 24-4-09\";\n");
          sbReports.Append("linka[2] = \"InterestingArticles.aspx?id=47\";\n");
          sbReports.Append("trgfrma[2] = \"_blank\";\n");

         sbReports.Append("titlea[3] = \"Gold View\";\n");
         sbReports.Append("texta[3] = \"Gold View\";\n");
         sbReports.Append("linka[3] = \"InterestingArticles.aspx?id=46\";\n");
         sbReports.Append("trgfrma[3] = \"_blank\";\n");

         sbReports.Append("titlea[4] = \"The 7 new rules of financial security\";\n");
         sbReports.Append("texta[4] = \"The 7 new rules of financial security\";\n");
         sbReports.Append("linka[4] = \"http://money.cnn.com/galleries/2009/moneymag/0903/gallery.financial_rules.moneymag/index.html\";\n");
         sbReports.Append("trgfrma[4] = \"_blank\";\n");
        
        
        
        
        return sbReports.ToString();
    
    }
}
