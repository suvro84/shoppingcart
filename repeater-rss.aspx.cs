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
using System.Collections.Generic;
using System.Xml.XPath;

public partial class data_repeater_rss : System.Web.UI.Page
{
    // public object for XmlNamespaceManager
    public XmlNamespaceManager xmlN;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // XmlNamespaceManager initialized by passing the Xml Document NameTable
            xmlN = new XmlNamespaceManager(XmlDataSource1.GetXmlDocument().NameTable);
            xmlN.AddNamespace("media", "http://search.yahoo.com/mrss/");

            // XmlNodeList generated by passing XPath expression and XmlNamespaceManager Object
            XmlNodeList xmlNodes = XmlDataSource1.GetXmlDocument().SelectNodes("rss/channel/item/media:group", xmlN);
            
            Repeater1.DataSource = xmlNodes;
            Repeater1.DataBind();

        }
    }
}

