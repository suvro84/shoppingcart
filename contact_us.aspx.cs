using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

public partial class contact_us : System.Web.UI.Page
{
    private Random random = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Session["CaptchaImageText"] = GenerateRandomCode();
        }
    }

    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 6; i++)
        {
            s = String.Concat(s, this.random.Next(10).ToString());
        }
        return s;
    }
    protected void Submit_Click(object sender, ImageClickEventArgs e)
    {
        if (this.txtCaptcha.Text == this.Session["CaptchaImageText"].ToString())
        {
            string strError = string.Empty;
            lblError.Text = "";

            if (SendMail("adesai12@gmail.com", txtEmail.Text.Trim(), txtfirstName.Text.Trim(), string.Empty, makeMailBody(), "New query from " + txtfirstName.Text.Trim() + " for Reliable Technology.", ref strError))
            {
                Response.Redirect("thank_you.html");
            }
            else
            {
                lblError.Text = strError;
            }
        }
        else
        {
            lblError.Text = "<font>Enter the letters as they are shown in the image above is not matched.</font>";
        }
    }
    protected bool SendMail(string mailTo, string mailFrom, string mailFromDisplayName, string mailCC, string mailBody, string mailSubject, ref string strError)
    {
        bool flag = true;
        MailMessage objMail = new MailMessage();
        objMail.To.Add(mailTo);
        objMail.From = new MailAddress(mailFrom, mailFromDisplayName);
        //objMail.CC.Add(mailCC);
        objMail.Subject = mailSubject;
        objMail.Body = mailBody;
        objMail.IsBodyHtml = true;
        objMail.Priority = MailPriority.High;
        //objMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        try
        {
            //Application["SMTPServer"] = "mail.reliabletechnologies.in";
            //Application["SMTPPort"] = "25";
            //Application["SMTPEmailAccountName"] = "mailer@reliabletechnologies.in";
            //Application["SMTPEmailAccountPassword"] = " chatlive58";


            SmtpClient client = new SmtpClient("mail.reliabletechnologies.in");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("mailer@reliabletechnologies.in", "chatlive58");
            client.Port = 25;
            //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            client.Send(objMail);
            flag = true;
        }
        catch (SmtpException ex) { flag = false; strError = ex.Message; }
        return flag;
    }
    protected string makeMailBody()
    {
        string retValue = string.Empty;
        retValue = "<table cellpadding='0' cellspacing='0' width='100%' border='0' style=\"font-size: 10pt; font-family: Verdana\">";

        retValue += "<tr>";
        retValue += "<td colspan='2'>Dear Sir,</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td colspan='2'>&nbsp;</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td colspan='2'>Below are the details of the query submitted on the website.</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td>First Name</td>";
        retValue += "<td>" + txtfirstName.Text + "</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td>Last Name</td>";
        retValue += "<td>" + txtlastName.Text + "</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td>Email Id</td>";
        retValue += "<td>" + txtEmail.Text + "</td>";
        retValue += "</tr>";

        //retValue += "<tr>";
        //retValue += "<td>Company Name</td>";
        //retValue += "<td>" + txtCompanyName.Text + "</td>";
        //retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td>Query</td>";
        retValue += "<td>" + txtQuery.Text + "</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td colspan='2'><hr></td>";
        retValue += "</tr>";

        retValue += "</table>";
        return retValue;
    }
}