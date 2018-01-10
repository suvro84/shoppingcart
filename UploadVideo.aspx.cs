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

public partial class UploadVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GetData();
        }
        //return;
    }







    protected void UploadFile(object sender, EventArgs e)
    {
        try
        {
            string strpath = Server.MapPath("UploadedVideos/");
            string stExtension = Path.GetExtension(FileVideo.FileName);
            Random rnd = new Random();
            int num = rnd.Next();
            FileVideo.PostedFile.SaveAs(strpath + "Video" + num + stExtension);
            lblUploadResult.Text = "File uploaded Successfully";
        }
        catch (Exception ex)
        {
            lblUploadResult.Text = ex.Message;
        }
    }
}
