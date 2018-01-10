using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["log"] == "logout")
        {
            if (Request.Cookies["UserInfo"] != null)
            {

                if (Request.Cookies["UserInfo"]["userId"] != null)
                {

                    string[] cookies = Request.Cookies.AllKeys;
                    foreach (string cookie in cookies)
                    {
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }
                    Session.Abandon();
                    Session.Clear();
                    Response.Redirect("Index.aspx");
                }
            }

        }


        string strError = "";

        if (Request.Cookies["UserInfo"] != null)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["UserInfo"]["userId"] != null)
                {
                    int intUserId = Convert.ToInt32(Request.Cookies["UserInfo"]["userId"]);
                    DataManipulationClass objdata = new DataManipulationClass();
                   // string name = objdata.GetName(intUserId, ref strError);
                    string name = "";
                    dvlogin.InnerHtml = "";
                    if (strError == "")
                    {
                        Welcome.InnerHtml = "Welcome " + name;
                        dvlogin.Visible = false;
                        dvlogout.Visible = true;
                    }
                    else
                    {
                        dvlogin.InnerHtml = strError;
                    }

                }
            }
        }
        else
        {

            dvlogin.Visible = true;
            dvlogout.Visible = false;
        }


    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        LoginCooky ObjLogin = new LoginCooky();
        ObjLogin.LoginUserName = txtusername.Text.Trim().Replace("'", "''");
        ObjLogin.LoginPassword = txtpwd.Text.Trim().Replace("'", "''");
        //string strError = "";
        if (new DataManipulationClass().CheckAdminLogin(ref ObjLogin))
        {
            //if (ObjLogin.LoginType == UserType.Admin)
            //{
            HttpCookie aCookie = new HttpCookie("UserInfo");
            aCookie["userId"] = ObjLogin.LoginId;
            aCookie["name"] = ObjLogin.LoginUserName;
            Response.Cookies.Add(aCookie);
            //aCookie.Expires = DateTime.Now.AddDays(1);
            //HttpContext.Current.Session["userId"] = ObjLogin.LoginId;
            //HttpContext.Current.Session["screenName"] = ObjLogin.LoginScreenName;
            Response.Redirect("AdminHome.aspx");
            //}
            //else
            //{
            //    lblError.Text = "You are not authorised for admin login";
            //}

        }
        else
        {
            lblError.Text = "You are not authorised for admin login";
        }
    }
}
