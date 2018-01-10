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

public partial class updatepannel_js : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPageDescription.Value = "hi";
        //txtPageDescription.Text = "hi";

        //Request.Form.Get("txtPageDescription") = "hi";

        //System.Text.StringBuilder sbjs=new System.Text.StringBuilder();
        //       sbjs.Append("function displaylimit(thename, theid, thelimit)"+char(13);
        //sbjs.Append("{"+char(13);
        //    sbjs.Append("var theform=theid!=\"\"? document.getElementById(theid) : thename"+char(13);
        //    sbjs.Append("var limit_text='<p><b><span  id=\"'+theform.toString()+'\">'+thelimit+'</span></b> characters remaining on your input limit</p>'"+char(13);
        //    sbjs.Append("if (document.all||ns6)"+char(13);
        //        sbjs.Append("document.write(limit_text)"+char(13);
        //    sbjs.Append("if (document.all)"+char(13);
        //    sbjs.Append("{\"+char(13);
        //        sbjs.Append("eval(theform).onkeypress=function(){ return restrictinput(thelimit,event,theform)"+char(13);
        //                                            //alert("Max 10 character");
        //                                            }"+char(13);
        //        sbjs.Append("eval(theform).onkeyup=function(){ countlimit(thelimit,event,theform)}"+char(13);
        //    sbjs.Append("}\"+char(13);
        //    sbjs.Append("else if (ns6)"+char(13);
        //    sbjs.Append("{\"+char(13);
        //        sbjs.Append("document.body.addEventListener('keypress', function(event) { restrictinput(thelimit,event,theform) }, true); "+char(13);
        //        sbjs.Append("document.body.addEventListener('keyup', function(event) { countlimit(thelimit,event,theform) }, true);"+char(13); 
        //    sbjs.Append("}"+char(13);
        //sbjs.Append("}"+char(13);
        //    sbjs.Append("}"+char(13);
    }
}
