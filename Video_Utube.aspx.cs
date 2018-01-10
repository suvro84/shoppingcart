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

public partial class Video_Utube : System.Web.UI.Page
{
    public string myUrl = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        myUrl = "http://www.youtube.com/watch?v=fZ9WiuJPnNA";
        Panel1.Visible = true;

    }
}
