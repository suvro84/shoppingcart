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
using System.IO;
using System.Text;

public partial class FileCreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //FileInfo t = new FileInfo("Collin.txt");
        //StreamWriter Tex = t.CreateText();
        //Tex.WriteLine("Collin has launced another article");
        //Tex.WriteLine("csharpfriends is the new url for c-sharp");
        //Tex.Write(Tex.NewLine);
        //Tex.close();

        //Console.WriteLine(" The Text file named Collin is created ");



        //string sFile = HttpContext.Current.Request.ApplicationPath;
        //if (File.Exists(sFile))
        //{
        //    Console.WriteLine("{0} already exists.", sFile);
        //    return;
        //}
        //StreamWriter sr = File.CreateText(sFile);
        //sr.WriteLine("This is my file.");
        //sr.WriteLine("I can write ints {0} or floats {1}, and so on.", 1, 4.2);
        //sr.Close();


        //Dim fp As StreamWriter
        StringBuilder sbMyScript = new StringBuilder();

        try
        {
            
            StreamWriter fp;
            fp = File.CreateText(Server.MapPath(".\\Create\\") + "test.js");

            sbMyScript.Append("Epoch.prototype.setDays = function ()");
            sbMyScript.Append("{");
            sbMyScript.Append("this.daynames = new Array();");
            sbMyScript.Append("var j=0;");
            sbMyScript.Append("for(var i=this.startDay; i< this.startDay + 7;i++) {");
            sbMyScript.Append("this.daynames[j++] = this.daylist[i];");
            sbMyScript.Append("}");
            sbMyScript.Append("this.monthDayCount = new Array(31,((this.curDate.getFullYear() - 2000) % 4 ? 28 : 29),31,30,31,30,31,31,30,31,30,31);");
            sbMyScript.Append("};");

            
            fp.WriteLine(sbMyScript.ToString());
            fp.Close();
        }
        catch (Exception ex)
        {

        }

       
    }
}
