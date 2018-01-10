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
using System.Xml;
using System.IO;

public partial class RTracking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (rtTracking obj = new rtTracking())
        {
            if (Request.Params["a"] != "")
            {
                obj.ProjectId = Convert.ToString(Request.Params["a"]);
            }

            if (Request.Params["b"] != "" && Request.Params["c"] != "")
            {
                obj.Resolution = Convert.ToString(Request.Params["b"]) + "x" + Convert.ToString(Request.Params["c"]);
            }

            if (Request.Params["d"] != "")
            {
                obj.Camefrom = Convert.ToString(Request.Params["d"]);
            }

            if (Request.Params["e"] != "")
            {
                obj.Landson = Convert.ToString(Request.Params["e"]);
            }

            if (Request.Params["f"] != "")
            {
                obj.IP = Convert.ToString(Request.Params["f"]);
            }

            if (Request.Params["g"] != "")
            {
                obj.Browser = Convert.ToString(Request.Params["g"]);
            }

            if (Request.Params["h"] != "")
            {
                obj.OperatingSystem = Convert.ToString(Request.Params["h"]);
            }


            //System.Threading.Semaphore sphore = new System.Threading.Semaphore(1, 50);
            //sphore.WaitOne();
            //sphore.Release();
            //try
            //{
            //System.Threading.Semaphore su = new System.Threading.Semaphore(1, 25);
            //su.WaitOne();
            createandupdatexml(obj);
            //su.Release();
            //su.Close();
            //}
            //catch (Exception ex)
            //{
            //    string mailBody = "<table cellpadding='0' cellspacing='0' width='0' border='0'>";

            //    mailBody += "<tr>";
            //    mailBody += "<td width='20%'>&nbsp;</td>";
            //    mailBody += "<td width='80%'>&nbsp;</td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td>I.P.</td>";
            //    mailBody += "<td><font color='red'>" + obj.IP + "</font></td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td colspan='2'>&nbsp;</td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td>Project Id</td>";
            //    mailBody += "<td>" + obj.ProjectId + " [Semaphore]</td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td colspan='2'>&nbsp;</td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td>Exception</td>";
            //    mailBody += "<td><font color='red'>" + ex.Message + "</font></td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td colspan='2'>&nbsp;</td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td>StackTrace</td>";
            //    mailBody += "<td>" + ex.StackTrace + "</td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td colspan='2'>&nbsp;</td>";
            //    mailBody += "</tr>";

            //    mailBody += "<tr>";
            //    mailBody += "<td>Sever DateTime</td>";
            //    mailBody += "<td>" + DateTime.Now + "</td>";
            //    mailBody += "</tr>";

            //    mailBody += "</table>";

            //    string mailSubject = "Tracking Error - IP :" + obj.IP;

            //    new MailOnExceptions().SendMail(mailBody, mailSubject);

            //}

        }

    }

    private void createandupdatexml(rtTracking obj)
    {
        System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml");

        if (fi.Exists)
        {
            updateXML(obj);

        }
        else
        {
            createXML(obj);
        }

        #region comment

        /*try
        {
            System.Threading.Thread.Sleep(1000);
            System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml");
            if (fi.Exists)
            {
                XmlTextReader xtr = new XmlTextReader(Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml");

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xtr);
                xtr.Close();

                XmlDocumentFragment xdf1 = xdoc.CreateDocumentFragment();
                xdf1.InnerXml = "<path>" +
                                "<camefrom>" + Convert.ToString(obj.Camefrom) + "</camefrom>" +
                                "<landson>" + Convert.ToString(obj.Landson) + "</landson>" +
                                "<landingtime>" + Convert.ToString(obj.LandingDateTime.ToString("dd-MMMM-yyyy hh:mm:ss tt")) + "</landingtime>" +
                                "</path>";


                XmlNode xn = xdoc.ChildNodes[1].ChildNodes[1];

                xn.InsertAfter(xdf1, xn.LastChild);

                xdoc.Save(Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml");

            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml");
                sw.AutoFlush = true;
                sw.Write(string.Empty);
                sw.Flush();
                sw.Dispose();
                sw.Close();



                XmlTextWriter xmlWriter = new XmlTextWriter(Server.MapPath(".\\Tracking\\" + obj.IP.Replace(".", "_") + ".xml"), System.Text.Encoding.UTF8);
                xmlWriter.WriteStartDocument();
                xmlWriter.Formatting = Formatting.Indented;

                xmlWriter.WriteStartElement("root");

                xmlWriter.WriteStartElement("userinfo");

                xmlWriter.WriteStartElement("source");
                xmlWriter.WriteValue(obj.Source);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("keyword");
                xmlWriter.WriteValue(obj.KeyWords);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ip");
                xmlWriter.WriteValue(obj.IP);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("browser");
                xmlWriter.WriteValue(obj.Browser);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("resolution");
                xmlWriter.WriteValue(obj.Resolution);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("operatingsystem");
                xmlWriter.WriteValue(obj.OperatingSystem);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("landingtime");
                xmlWriter.WriteValue(Convert.ToString(obj.LandingDateTime.ToString("dd-MMMM-yyyy hh:mm:ss tt")));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("pathinfo");

                xmlWriter.WriteStartElement("path");

                xmlWriter.WriteStartElement("camefrom");
                xmlWriter.WriteValue(Convert.ToString(obj.Camefrom));
                xmlWriter.WriteEndElement();


                xmlWriter.WriteStartElement("landson");
                xmlWriter.WriteValue(Convert.ToString(obj.Landson));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("landingtime");
                xmlWriter.WriteValue(Convert.ToString(obj.LandingDateTime.ToString("dd-MMMM-yyyy hh:mm:ss tt")));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();

            }
        }
        catch (Exception ex)
        {

            string mailBody = "<table cellpadding='0' cellspacing='0' width='0' border='0'>";

            mailBody += "<tr>";
            mailBody += "<td width='20%'>&nbsp;</td>";
            mailBody += "<td width='80%'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>I.P.</td>";
            mailBody += "<td><font color='red'>" + obj.IP + "</font></td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>Project Id</td>";
            mailBody += "<td>" + obj.ProjectId + "</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>Exception</td>";
            mailBody += "<td><font color='red'>" + ex.Message + "</font></td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>StackTrace</td>";
            mailBody += "<td>" + ex.StackTrace + "</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>Sever DateTime</td>";
            mailBody += "<td>" + DateTime.Now + "</td>";
            mailBody += "</tr>";

            //mailBody += "<tr>";
            //mailBody += "<td>Spl Msg</td>";
            //mailBody += "<td>" + err + "</td>";
            //mailBody += "</tr>";

            mailBody += "</table>";

            string mailSubject = "Tracking Error - IP :" + obj.IP;

            new MailOnExceptions().SendMail(mailBody, mailSubject);

        }*/

        #endregion
    }

    private void createXML(rtTracking obj)
    {
        //System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml");

        //sw.AutoFlush = true;
        //sw.Write(string.Empty);
        //sw.Flush();
        //sw.Dispose();
        //sw.Close();

        string filename = Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml";

        //FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
        //fs.Close();
        //fs.Dispose();

        //TextWriter tt = new StreamWriter(filename);
        //tt.Close();
        //tt.Dispose();

        XmlTextWriter xmlWriter = null;
        try
        {

            xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);

            xmlWriter.WriteStartDocument();
            xmlWriter.Formatting = Formatting.Indented;

            xmlWriter.WriteStartElement("root");

            xmlWriter.WriteStartElement("userinfo");

            xmlWriter.WriteStartElement("source");
            xmlWriter.WriteValue(Convert.ToString(obj.Source));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("keyword");
            xmlWriter.WriteValue(Convert.ToString(obj.KeyWords));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("ip");
            xmlWriter.WriteValue(Convert.ToString(obj.IP));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("browser");
            xmlWriter.WriteValue(Convert.ToString(obj.Browser));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("resolution");
            xmlWriter.WriteValue(Convert.ToString(obj.Resolution));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("operatingsystem");
            xmlWriter.WriteValue(Convert.ToString(obj.OperatingSystem));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("landingtime");
            xmlWriter.WriteValue(Convert.ToString(obj.LandingDateTime.ToString("dd-MMMM-yyyy hh:mm:ss tt")));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("pathinfo");

            xmlWriter.WriteStartElement("path");

            xmlWriter.WriteStartElement("camefrom");
            xmlWriter.WriteValue(Convert.ToString(obj.Camefrom));
            xmlWriter.WriteEndElement();


            xmlWriter.WriteStartElement("landson");
            xmlWriter.WriteValue(Convert.ToString(obj.Landson));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("landingtime");
            xmlWriter.WriteValue(Convert.ToString(obj.LandingDateTime.ToString("dd-MMMM-yyyy hh:mm:ss tt")));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
            xmlWriter.Close();
        }
        catch (Exception ex)
        {
            xmlWriter.Flush();
            xmlWriter.Close();

            string mailBody = "<table cellpadding='0' cellspacing='0' width='0' border='0'>";

            mailBody += "<tr>";
            mailBody += "<td width='20%'>&nbsp;</td>";
            mailBody += "<td width='80%'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>I.P.</td>";
            mailBody += "<td><font color='red'>" + obj.IP + "</font></td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>Project Id</td>";
            mailBody += "<td>" + obj.ProjectId + "</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>Exception</td>";
            mailBody += "<td><font color='red'>" + ex.Message + "</font></td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>StackTrace</td>";
            mailBody += "<td>" + ex.StackTrace + "</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td colspan='2'>&nbsp;</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>Sever DateTime</td>";
            mailBody += "<td>" + DateTime.Now + "</td>";
            mailBody += "</tr>";

            mailBody += "<tr>";
            mailBody += "<td>Method</td>";
            mailBody += "<td>createXML()</td>";
            mailBody += "</tr>";

            mailBody += "</table>";

            string mailSubject = "Tracking Error - IP :" + obj.IP;

            //new MailOnExceptions().SendMail(mailBody, mailSubject);
        }
    }

    private void updateXML(rtTracking obj)
    {
        string filename = Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml";
        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        XmlTextReader xtr = null;
        try
        {
            xtr = new XmlTextReader(fs);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xtr);
            xtr.Close();

            fs.Close();
            fs.Dispose();

            XmlDocumentFragment xdf1 = xdoc.CreateDocumentFragment();
            xdf1.InnerXml = "<path>" +
                            "<camefrom>" + Convert.ToString(obj.Camefrom) + "</camefrom>" +
                            "<landson>" + Convert.ToString(obj.Landson) + "</landson>" +
                            "<landingtime>" + Convert.ToString(obj.LandingDateTime.ToString("dd-MMMM-yyyy hh:mm:ss tt")) + "</landingtime>" +
                            "</path>";


            XmlNode xn = xdoc.ChildNodes[1].ChildNodes[1];

            xn.InsertAfter(xdf1, xn.LastChild);

            xdoc.Save(Server.MapPath(".\\") + "Tracking\\" + obj.IP.Replace(".", "_") + ".xml");
        }
        catch (Exception ex)
        {
            xtr.Close();
            fs.Close();
            fs.Dispose();
            if (ex.Message.Contains("Root element is missing."))
            {
                xtr.Close();
                createXML(obj);
            }
            else
            {
                string mailBody = "<table cellpadding='0' cellspacing='0' width='0' border='0'>";

                mailBody += "<tr>";
                mailBody += "<td width='20%'>&nbsp;</td>";
                mailBody += "<td width='80%'>&nbsp;</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td>I.P.</td>";
                mailBody += "<td><font color='red'>" + obj.IP + "</font></td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td colspan='2'>&nbsp;</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td>Project Id</td>";
                mailBody += "<td>" + obj.ProjectId + "</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td colspan='2'>&nbsp;</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td>Exception</td>";
                mailBody += "<td><font color='red'>" + ex.Message + "</font></td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td colspan='2'>&nbsp;</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td>StackTrace</td>";
                mailBody += "<td>" + ex.StackTrace + "</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td colspan='2'>&nbsp;</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td>Sever DateTime</td>";
                mailBody += "<td>" + DateTime.Now + "</td>";
                mailBody += "</tr>";

                mailBody += "<tr>";
                mailBody += "<td>Method</td>";
                mailBody += "<td>updateXML()</td>";
                mailBody += "</tr>";

                mailBody += "</table>";

                string mailSubject = "Tracking Error - IP :" + obj.IP;

                //new MailOnExceptions().SendMail(mailBody, mailSubject);
            }
        }
    }
}
