using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Xml;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Text;

public partial class index_InfomineAjax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strMode = "";
        if (Request.Params["mode"] != null)
        {
            strMode = Request.Params["mode"].ToString();
        }
        switch (strMode)
        {
            case "infomineCapitalMkt":
                Response.Write(getCapitalMkt() + "|");
                break;
            case "infomineCommodities":
                Response.Write(getCommodities() + "|");
                break;
            case "infomineWorldIndices":
                Response.Write(getWorldIndices() + "|");
                break;
            case "infomineADRPrices":
                Response.Write(getADRPrices() + "|");
                break;
            case "infomineCurrencies":
                Response.Write(getCurrencies() + "|");
                break;
            case "infomineDataMine":
                Response.Write(getDataMine() + "|");
                break;


        }
    }
    public string getCapitalMkt()
    {
        String strImagPath = ConfigurationManager.AppSettings["Image_Path"].ToString();
        int rowcount = 0;
        int tdcount = 0;
        string strScriptName = "";
        System.Text.StringBuilder strBuilBody = new System.Text.StringBuilder();
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("ScriptName", typeof(string));
            dt.Columns.Add("ChangeAbsolute", typeof(decimal));
            dt.Columns.Add("ChangePercent", typeof(string));
            dt.Columns.Add("Volume", typeof(int));
            dt.Columns.Add("LastPrice", typeof(decimal));
            dt.Columns.Add("UpdateDateTime", typeof(DateTime));


            Hashtable hashCode = new Hashtable();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(strImagPath + "XML/CapitalMarket.xml");
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("Stock");
            for (int i = 0; i < nodeList.Count; i++)
            {
                hashCode.Add("code" + i, (object)nodeList[i].Attributes.Item(0).ChildNodes[0].InnerText.ToString());
                //hashName.Add("name" + i, (object)nodeList[i].Attributes.Item(1).ChildNodes[0].InnerText.ToString());
            }
            int stockCount = 0;
            if (hashCode.Count < 5)
            {
                stockCount = hashCode.Count;
            }
            else
            {
                stockCount = 5;
            }
            //string stockCode = "^N225";
           // StockData m_Index2Data = new StockData();
            string serverUrl = "";
            for (int i = 0; i < stockCount; i++)
            {
                serverUrl = @"http://in.finance.yahoo.com/d/quotes.csv?s=" + hashCode["code" + i].ToString() +
                "&f=sl1d1t1c1ohgvj1pp2owern&e=.csv";
               // dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "CM");
            }
            int ii = 0;
            if (dt.Rows.Count < 5)
            {
                for (int j = 0; j < 5 - (dt.Rows.Count); )
                {
                    ii = (dt.Rows.Count - 1) + 1;
                    DataRow drr = dt.NewRow();
                    drr["ID"] = 0;
                    drr["ScriptName"] = "------";
                    drr["ChangeAbsolute"] = 0;
                    drr["ChangePercent"] = 0;
                    drr["Volume"] = 0;
                    drr["LastPrice"] = 0;
                    dt.Rows.Add(drr);
                }
            }


            //strBuilBody.Append("<h3 class=\"captalHead\">");
            //strBuilBody.Append("Capital Market</h3>");
            strBuilBody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            strBuilBody.Append("<tr>");
            strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg\">");
            strBuilBody.Append("Company");
            strBuilBody.Append("</td>");
            strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg noborder\">");
            strBuilBody.Append("Last Price</td>");
            strBuilBody.Append("</tr>");
            for (int i = 0; i < 5; i++)
            {
                if (dt.Rows[i]["ScriptName"].ToString().Length >= 11)
                {
                    strScriptName = dt.Rows[i]["ScriptName"].ToString().Substring(0, 11);
                }
                else
                {
                    strScriptName = dt.Rows[i]["ScriptName"].ToString();
                }


                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                strBuilBody.Append("" + strScriptName + "</td>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                strBuilBody.Append("" + dt.Rows[i]["LastPrice"].ToString() + "</td>");
                strBuilBody.Append("</tr>");
            }
            strBuilBody.Append("<tr>");
            strBuilBody.Append("<td colspan=\"2\" align=\"right\" valign=\"top\"><a target=_blank href=\"http://www.moneyjugglers.com/infomine_view_more.aspx?name=capitalmarket\" title=\"view more\" class=\"captextbtn\">view more &raquo;</a></td>");
            strBuilBody.Append("<tr>");

            strBuilBody.Append("</table>");

        }
        catch (Exception ex)
        {
            strBuilBody.Remove(0, strBuilBody.Length);
            Response.Write(ex.Message);
        }
        finally
        {

        }
        return strBuilBody.ToString();
    }

    public string getCommodities()
    {
        String strImagPath = ConfigurationManager.AppSettings["Image_Path"].ToString();
        int rowcount = 0;
        int tdcount = 0;
        string strScriptName = "";
        System.Text.StringBuilder strBuilBody = new System.Text.StringBuilder();
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Expiry", typeof(decimal));
            dt.Columns.Add("ClosingPrice", typeof(decimal));
            dt.Columns.Add("ChangePercent", typeof(string));
            dt.Columns.Add("UpdateDateTime", typeof(DateTime));

            Hashtable hashCode = new Hashtable();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(strImagPath + "XML/Commodities.xml");
            //xmldoc.Load(Server.MapPath("XML/Commodities.xml"));
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("Stock");
            for (int i = 0; i < nodeList.Count; i++)
            {
                hashCode.Add("code" + i, (object)nodeList[i].Attributes.Item(0).ChildNodes[0].InnerText.ToString());
                //hashName.Add("name" + i, (object)nodeList[i].Attributes.Item(1).ChildNodes[0].InnerText.ToString());
            }
            int stockCount = 0;
            if (hashCode.Count < 5)
            {
                stockCount = hashCode.Count;
            }
            else
            {
                stockCount = 5;
            }
            //string stockCode = "^N225";
            StockData m_Index2Data = new StockData();
            string serverUrl = "";
            for (int i = 0; i < stockCount; i++)
            {
                serverUrl = @"http://in.finance.yahoo.com/d/quotes.csv?s=" + hashCode["code" + i].ToString() +
                "&f=sl1d1t1c1ohgvj1pp2owern&e=.csv";
                dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "COM");
            }
            int ii = 0;
            if (dt.Rows.Count < 10)
            {
                for (int j = 0; j < 5 - (dt.Rows.Count); )
                {
                    ii = (dt.Rows.Count - 1) + 1;
                    DataRow drr = dt.NewRow();
                    drr["ID"] = 0;
                    drr["Name"] = "------";
                    drr["Expiry"] = 0;
                    drr["ClosingPrice"] = 0.00;
                    drr["ChangePercent"] = 0.00;
                    dt.Rows.Add(drr);
                }
            }

            if (dt.Rows.Count < 10)
            {

                //strBuilBody.Append("<h3 class=\"captalHead\">");
                //strBuilBody.Append("Commodities</h3>");
                strBuilBody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg\">");
                strBuilBody.Append("Commodity");
                strBuilBody.Append("</td>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg noborder\">");
                strBuilBody.Append("Last Price</td>");
                strBuilBody.Append("</tr>");
                for (int i = 0; i < 5; i++)
                {
                    if (dt.Rows[i]["Name"].ToString().Length >= 11)
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString().Substring(0, 11);
                    }
                    else
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString();
                    }

                    strBuilBody.Append("<tr>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + strScriptName + "</td>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + dt.Rows[i]["ClosingPrice"].ToString() + "</td>");
                    strBuilBody.Append("</tr>");
                }
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td colspan=\"2\" align=\"right\" valign=\"top\"><a target=_blank href=\"http://www.moneyjugglers.com/infomine_view_more.aspx?name=commodities\" title=\"view more\" class=\"captextbtn\">view more &raquo;</a></td>");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("</table>");

            }
        }
        catch (Exception ex)
        {
            strBuilBody.Remove(0, strBuilBody.Length);
            Response.Write(ex.Message);
        }
        finally
        {

        }
        return strBuilBody.ToString();
    }

    public string getWorldIndices()
    {
        String strImagPath = ConfigurationManager.AppSettings["Image_Path"].ToString();
        int rowcount = 0;
        int tdcount = 0;
        string strScriptName = "";
        System.Text.StringBuilder strBuilBody = new System.Text.StringBuilder();
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("ChangeAbsolute", typeof(decimal));
            dt.Columns.Add("ChangePercent", typeof(string));
            dt.Columns.Add("ClosingPrice", typeof(decimal));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("ClosingDate", typeof(DateTime));
            dt.Columns.Add("UpdateDateTime", typeof(DateTime));
            dt.Columns.Add("CurrentPrice", typeof(decimal));


            Hashtable hashCode = new Hashtable();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(strImagPath + "XML/WorldIndices.xml");
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("Stock");
            for (int i = 0; i < nodeList.Count; i++)
            {
                hashCode.Add("code" + i, (object)nodeList[i].Attributes.Item(0).ChildNodes[0].InnerText.ToString());
            }
            int stockCount = 0;
            if (hashCode.Count < 5)
            {
                stockCount = hashCode.Count;
            }
            else
            {
                stockCount = 5;
            }
            StockData m_Index2Data = new StockData();
            string serverUrl = "";
            for (int i = 0; i < stockCount; i++)
            {
                serverUrl = @"http://in.finance.yahoo.com/d/quotes.csv?s=" + hashCode["code" + i].ToString() +
                "&f=sl1d1t1c1ohgvj1pp2owern&e=.csv";
                dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "WIND");
            }
            int ii = 0;
            if (dt.Rows.Count < 5)
            {
                for (int j = 0; j < 5 - (dt.Rows.Count); )
                {
                    ii = (dt.Rows.Count - 1) + 1;
                    DataRow drr = dt.NewRow();
                    drr["ID"] = 0;
                    drr["Name"] = "------";
                    drr["ChangeAbsolute"] = 0.00;
                    drr["ChangePercent"] = 0.00;
                    drr["ClosingPrice"] = 0.00;
                    drr["Country"] = 0;
                    drr["ClosingDate"] = System.DateTime.Now;
                    drr["CurrentPrice"] = 0.00;
                    dt.Rows.Add(drr);
                }
            }
            if (dt.Rows.Count > 0)
            {



                //strBuilBody.Append("<h3 class=\"captalHead\">");
                //strBuilBody.Append("World Indices </h3>");
                strBuilBody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg\">");
                strBuilBody.Append("Indices");
                strBuilBody.Append("</td>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg noborder\">");
                strBuilBody.Append("Index</td>");
                strBuilBody.Append("</tr>");
                for (int i = 0; i < 5; i++)
                {
                    if (dt.Rows[i]["Name"].ToString().Length >= 11)
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString().Substring(0, 11);
                    }
                    else
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString();
                    }
                    strBuilBody.Append("<tr>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + strScriptName + "</td>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + dt.Rows[i]["CurrentPrice"].ToString() + "</td>");
                    strBuilBody.Append("</tr>");
                }
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td colspan=\"2\" align=\"right\" valign=\"top\"><a target=_blank href=\"http://www.moneyjugglers.com/infomine_view_more.aspx?name=worldindices\" title=\"view more\" class=\"captextbtn\">view more &raquo;</a></td>");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("</table>");



            }
        }
        catch (Exception ex)
        {
            strBuilBody.Remove(0, strBuilBody.Length);
            Response.Write(ex.Message);
        }
        finally
        {

        }
        return strBuilBody.ToString();
    }

    public string getADRPrices()
    {
        String strImagPath = ConfigurationManager.AppSettings["Image_Path"].ToString();
        int rowcount = 0;
        int tdcount = 0;
        string strScriptName = "";
        System.Text.StringBuilder strBuilBody = new System.Text.StringBuilder();
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Symbol", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("LastPrice", typeof(decimal));
            dt.Columns.Add("ChangeAbsolute", typeof(decimal));
            dt.Columns.Add("ChangePercent", typeof(string));
            dt.Columns.Add("Volume", typeof(int));
            dt.Columns.Add("UpdateDateTime", typeof(DateTime));


            Hashtable hashCode = new Hashtable();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(strImagPath + "XML/ADRPrices.xml");
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("Stock");
            for (int i = 0; i < nodeList.Count; i++)
            {
                hashCode.Add("code" + i, (object)nodeList[i].Attributes.Item(0).ChildNodes[0].InnerText.ToString());
                //hashName.Add("name" + i, (object)nodeList[i].Attributes.Item(1).ChildNodes[0].InnerText.ToString());
            }
            int stockCount = 0;
            if (hashCode.Count < 5)
            {
                stockCount = hashCode.Count;
            }
            else
            {
                stockCount = 5;
            }
            //string stockCode = "^N225";
            StockData m_Index2Data = new StockData();
            string serverUrl = "";
            for (int i = 0; i < stockCount; i++)
            {
                serverUrl = @"http://in.finance.yahoo.com/d/quotes.csv?s=" + hashCode["code" + i].ToString() +
                "&f=sl1d1t1c1ohgvj1pp2owern&e=.csv";
                dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "ADR");
            }
            int ii = 0;
            if (dt.Rows.Count < 5)
            {
                for (int j = 0; j < 5 - (dt.Rows.Count); )
                {
                    ii = (dt.Rows.Count - 1) + 1;
                    DataRow drr = dt.NewRow();
                    drr["ID"] = 0;
                    drr["Symbol"] = "------";
                    drr["Name"] = "------";
                    drr["LastPrice"] = 0.00;
                    drr["ChangeAbsolute"] = 0.00;
                    drr["ChangePercent"] = 0.00;
                    drr["Volume"] = 0;
                    dt.Rows.Add(drr);
                }
            }


            if (dt.Rows.Count > 0)
            {

                //strBuilBody.Append("<h3 class=\"captalHead\">");
                //strBuilBody.Append("ADR</h3>");
                strBuilBody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg\">");
                strBuilBody.Append("Company");
                strBuilBody.Append("</td>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg noborder\">");
                strBuilBody.Append("Last Price</td>");
                strBuilBody.Append("</tr>");


                for (int i = 0; i < 5; i++)
                {
                    if (dt.Rows[i]["Name"].ToString().Length >= 10)
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString().Substring(0, 10);
                    }
                    else
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString();
                    }

                    strBuilBody.Append("<tr>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + strScriptName + "</td>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + dt.Rows[i]["LastPrice"].ToString() + "</td>");
                    strBuilBody.Append("</tr>");
                }
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td colspan=\"2\" align=\"right\" valign=\"top\"><a target=_blank href=\"http://www.moneyjugglers.com/infomine_view_more.aspx?name=adrprices\" title=\"view more\" class=\"captextbtn\">view more &raquo;</a></td>");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("</table>");

            }
        }
        catch (Exception ex)
        {
            strBuilBody.Remove(0, strBuilBody.Length);
            Response.Write(ex.Message);
        }
        finally
        {

        }
        return strBuilBody.ToString();
    }

    public string getCurrencies()
    {
        String strImagPath = ConfigurationManager.AppSettings["Image_Path"].ToString();
        int rowcount = 0;
        int tdcount = 0;
        string strScriptName = "";
        System.Text.StringBuilder strBuilBody = new System.Text.StringBuilder();
        try
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID", typeof(int));
            //dt.Columns.Add("Name", typeof(string));
            //dt.Columns.Add("Country", typeof(string));
            //dt.Columns.Add("Currency", typeof(string));
            //dt.Columns.Add("Rate", typeof(decimal));
            //dt.Columns.Add("ChangePercent", typeof(string));
            //dt.Columns.Add("UpdateDateTime", typeof(DateTime));


            //Hashtable hashCode = new Hashtable();
            //Hashtable hashName = new Hashtable();
            //Hashtable hashSecondndCurrName = new Hashtable();

            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(strImagPath + "XML/Currencies.xml");
            ////xmldoc.Load("E:/MoneyJugglerss/XML/Currencies.xml");
            //XmlNodeList nodeList = xmldoc.GetElementsByTagName("Stock");
            //for (int i = 0; i < nodeList.Count; i++)
            //{
            //    hashCode.Add("code" + i, (object)nodeList[i].Attributes.Item(0).ChildNodes[0].InnerText.ToString());
            //    hashName.Add("name" + i, (object)nodeList[i].Attributes.Item(1).ChildNodes[0].InnerText.ToString());
            //    hashSecondndCurrName.Add("SecondndCurrName" + i, (object)nodeList[i].Attributes.Item(2).ChildNodes[0].InnerText.ToString());
            //}
            //int stockCount = 0;
            //if (hashCode.Count < 5)
            //{
            //    stockCount = hashCode.Count;
            //}
            //else
            //{
            //    stockCount = 5;
            //}
            //StockData m_Index2Data = new StockData();
            //string serverUrl = "";
            //for (int i = 0; i < stockCount; i++)
            //{
            //    serverUrl = @"http://finance.yahoo.com/d/quotes.csv?s=" + hashCode["code" + i].ToString() +
            //                "&f=sl1d1t1c1ohgv&e=.csv";
            //    dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "CUR", hashName["name" + i].ToString(), hashSecondndCurrName["SecondndCurrName" + i].ToString());
            //}

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Country", typeof(string));//Country is second currency name
            dt.Columns.Add("Currency", typeof(string));//Currency is first currency name
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("ChangePercent", typeof(string));
            dt.Columns.Add("UpdateDateTime", typeof(DateTime));


            Hashtable hashCode = new Hashtable();
            Hashtable hashName = new Hashtable();
            Hashtable hashSecondndCurrName = new Hashtable();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(strImagPath + "XML/Currencies.xml");
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("Stock");
            for (int i = 0; i < nodeList.Count; i++)
            {
                hashCode.Add("code" + i, (object)nodeList[i].Attributes.Item(0).ChildNodes[0].InnerText.ToString());
                hashName.Add("name" + i, (object)nodeList[i].Attributes.Item(1).ChildNodes[0].InnerText.ToString());
                hashSecondndCurrName.Add("SecondndCurrName" + i, (object)nodeList[i].Attributes.Item(2).ChildNodes[0].InnerText.ToString());
            }
            StockData m_Index2Data = new StockData();
            string serverUrl = "";
            for (int i = 0; i < hashCode.Count; i++)
            {
                serverUrl = @"http://finance.yahoo.com/d/quotes.csv?s=" + hashCode["code" + i].ToString() +
                            "&f=sl1d1t1c1ohgv&e=.csv";
                // dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "CUR", hashName["name" + i].ToString(), hashSecondndCurrName["SecondndCurrName" + i].ToString());
                // dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "CUR", hashName["name" + i].ToString(), hashSecondndCurrName["SecondndCurrName" + i].ToString());
                dt = GetStockData(hashCode["code" + i].ToString(), m_Index2Data, dt, serverUrl, "CUR");

            }




            int ii = 0;
            if (dt.Rows.Count < 5)
            {
                for (int j = 0; j < 5 - (dt.Rows.Count); )
                {
                    ii = (dt.Rows.Count - 1) + 1;
                    DataRow drr = dt.NewRow();
                    drr["ID"] = 0;
                    drr["Name"] = "------";
                    drr["Country"] = 0;
                    drr["Currency"] = "-----";
                    drr["Rate"] = 0.00;
                    drr["ChangePercent"] = 0.00;
                    dt.Rows.Add(drr);
                }
            }


            if (dt.Rows.Count > 0)
            {

                //strBuilBody.Append("<h3 class=\"captalHead\">");
                //strBuilBody.Append("Currencies </h3>");
                strBuilBody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg\">");
                strBuilBody.Append("Symbol");
                strBuilBody.Append("</td>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg noborder\">");
                strBuilBody.Append("Rate</td>");
                strBuilBody.Append("</tr>");

                for (int i = 0; i < 5; i++)
                {

                    if (dt.Rows[i]["Name"].ToString().Length >= 11)
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString().Substring(0, 11);
                    }
                    else
                    {
                        strScriptName = dt.Rows[i]["Name"].ToString();
                    }
                    strBuilBody.Append("<tr>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + strScriptName + "</td>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("1-" + dt.Rows[i]["Rate"].ToString() + "</td>");

                    strBuilBody.Append("</tr>");
                }
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td colspan=\"2\" align=\"right\" valign=\"top\"><a target=_blank href=\"http://www.moneyjugglers.com/infomine_view_more.aspx?name=currencies\" title=\"view more\" class=\"captextbtn\">view more &raquo;</a></td>");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("</table>");


                dt.Dispose();
                dt.Clear();
            }
        }
        catch (Exception ex)
        {
            strBuilBody.Remove(0, strBuilBody.Length);
            Response.Write(ex.Message);
        }
        finally
        {

        }
        return strBuilBody.ToString();
    }

    public string getDataMine()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DB_Conn"].ToString());

        int rowcount = 0;
        int tdcount = 0;
        string strScriptName = "";
        System.Text.StringBuilder strBuilBody = new System.Text.StringBuilder();
        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            System.Text.StringBuilder strBuilSql = new System.Text.StringBuilder();
            strBuilSql.Append("SELECT TOP 5 ");
            strBuilSql.Append("dbo.Datamine.ID, ");
            strBuilSql.Append("dbo.Datamine.Name, ");
            strBuilSql.Append("dbo.Datamine.Closing, ");
            strBuilSql.Append("dbo.Datamine.Tenure, ");
            strBuilSql.Append("dbo.Datamine.[Current], ");
            strBuilSql.Append("dbo.Datamine.Period, ");
            strBuilSql.Append("dbo.Datamine.UpdateDateTime, ");
            strBuilSql.Append("dbo.Datamine.ChangePercentage ");
            strBuilSql.Append("FROM ");
            strBuilSql.Append("dbo.Datamine ");
            strBuilSql.Append("WHERE ");
            strBuilSql.Append("(dbo.Datamine.ActiveStatus = 1) ");
            strBuilSql.Append("ORDER BY dbo.Datamine.UpdateDateTime DESC");
            SqlCommand cmd = new SqlCommand(strBuilSql.ToString(), conn);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);
            int ii = 0;
            if (dt.Rows.Count < 5)
            {
                for (int j = 0; j < 5 - (dt.Rows.Count); )
                {
                    ii = (dt.Rows.Count - 1) + 1;
                    DataRow drr = dt.NewRow();
                    drr["ID"] = 0;
                    drr["Name"] = "------";
                    drr["Closing"] = 0.00;
                    drr["Period"] = "0.00";
                    drr["Current"] = 0.00;
                    dt.Rows.Add(drr);
                }
            }
            if (dt.Rows.Count > 0)
            {


                String[] allowedData = { "Inflation", "GDP", "BOP Curve", "CPI-IW", "Bank Rate" };


                //strBuilBody.Append("<h3 class=\"captalHead\">");
                //strBuilBody.Append("Datamines</h3>");
                strBuilBody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg\">");
                strBuilBody.Append("Heads");
                strBuilBody.Append("</td>");
                strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"companybg noborder\">");
                strBuilBody.Append("Current</td>");
                strBuilBody.Append("</tr>");



                for (int i = 0; i < allowedData.Length; i++)
                {

                    //if (dt.Rows[i]["Name"].ToString().Length >= 9)
                    //{
                    //    strScriptName = dt.Rows[i]["Name"].ToString().Substring(0, 9);
                    //}
                    //else
                    //{
                    //    strScriptName = dt.Rows[i]["Name"].ToString();
                    //}

                    strBuilBody.Append("<tr>");
                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    //strBuilBody.Append("" + strScriptName + "</td>");

                    strBuilBody.Append("" + allowedData[i] + "</td>");


                    strBuilBody.Append("<td width=\"50%\" align=\"center\" valign=\"top\" class=\"captext\">");
                    strBuilBody.Append("" + dt.Rows[i]["Current"].ToString() + "</td>");
                    strBuilBody.Append("</tr>");

                }
                strBuilBody.Append("<tr>");
                strBuilBody.Append("<td colspan=\"2\" align=\"right\" valign=\"top\"><a target=_blank href=\"http://www.moneyjugglers.com/infomine_view_more.aspx?name=datamine\" title=\"view more\" class=\"captextbtn\">view more &raquo;</a></td>");
                strBuilBody.Append("<tr>");
                strBuilBody.Append("</table>");


                dt.Dispose();
                dt.Clear();

            }
        }
        catch (Exception ex)
        {
            strBuilBody.Remove(0, strBuilBody.Length);
            Response.Write(ex.Message);
        }
        finally
        {

        }
        return strBuilBody.ToString();
    }





    private DataTable GetStockData(string stockCode, StockData stockData, DataTable dt, string serverUrl, string ID)
    {

        //string serverUrl = @"http://in.finance.yahoo.com/d/quotes.csv?s=" + stockCode +
        //        "&f=sl1d1t1c1ohgvj1pp2owern&e=.csv";
        //string serverUrl = @"http://finance.yahoo.com/d/quotes.csv?s=" + stockCode +
        //        "&f=sl1d1t1c1ohgv&e=.csv";
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);

            string stockDataString = reader.ReadLine();
            string[] stockDataContents = stockDataString.Split(',');

            //stockData.Code = stockCode;
            //stockData.Last = stockDataContents[1];
            //stockData.Date = stockDataContents[2];
            //stockData.Time = stockDataContents[3];
            //stockData.Change = stockDataContents[4];
            //stockData.Open = stockDataContents[5];
            //stockData.High = stockDataContents[6];
            //stockData.Low = stockDataContents[7];
            //stockData.Volume = stockDataContents[8];
            if (ID != "CUR")
            {
                //stockData.MarketCapital = stockDataContents[9];
                //stockData.PreviousClose = stockDataContents[10];
                //stockData.PctChange = stockDataContents[11];
                //stockData.AnnRange = stockDataContents[12];
                //stockData.Earnings = stockDataContents[13];
                //stockData.PERatio = stockDataContents[14];
            }
            DataRow drr = dt.NewRow();
            if (ID == "CM")
            {
                drr["ID"] = 0;
                drr["ScriptName"] = stockDataContents[16].ToString().Replace("\"", "");
                drr["ChangeAbsolute"] = stockDataContents[4].ToString().Replace("\"", "");
                drr["ChangePercent"] = stockDataContents[11].ToString().Replace("\"", "").Replace("%", "");
                if (stockDataContents[11] != "N/A")
                {
                    drr["Volume"] = stockDataContents[8].ToString();
                    drr["LastPrice"] = stockDataContents[1].ToString();
                    drr["UpdateDateTime"] = Convert.ToDateTime(stockDataContents[2].ToString().Replace("\"", "") + " " + stockDataContents[3].ToString().Replace("\"", ""));
                }
                else
                {
                    drr["Volume"] = "0";
                    drr["LastPrice"] = "0.00";
                    drr["UpdateDateTime"] = System.DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }
            if (ID == "COM")
            {
                drr["ID"] = 0;
                drr["Name"] = stockDataContents[16].ToString().Replace("\"", "");
                drr["Expiry"] = stockDataContents[4].ToString().Replace("\"", "");
                drr["ClosingPrice"] = stockDataContents[1].ToString();
                drr["ChangePercent"] = stockDataContents[11].ToString().Replace("\"", "").Replace("%", "");
                if (stockDataContents[11].ToString() != "N/A")
                {
                    drr["UpdateDateTime"] = Convert.ToDateTime(stockDataContents[2].ToString().Replace("\"", "") + " " + stockDataContents[3].ToString().Replace("\"", ""));
                }
                else
                {
                    drr["UpdateDateTime"] = System.DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }
            if (ID == "ADR")
            {
                drr["ID"] = 0;
                drr["Symbol"] = stockCode.ToString().Replace("\"", "");
                drr["Name"] = stockDataContents[16].ToString().Replace("\"", "");
                if (stockDataContents[4].ToString() != "N/A")
                {
                    drr["ChangeAbsolute"] = stockDataContents[4].ToString().Replace("\"", "");
                }
                else
                {
                    drr["ChangeAbsolute"] = "0.00";
                }
                drr["ChangePercent"] = stockDataContents[11].ToString().Replace("\"", "").Replace("%", "");
                if (stockDataContents[8].ToString() != "N/A")
                {
                    drr["Volume"] = stockDataContents[8].ToString();
                    drr["LastPrice"] = stockDataContents[1].ToString();
                    drr["UpdateDateTime"] = Convert.ToDateTime(stockDataContents[2].ToString().Replace("\"", "") + " " + stockDataContents[3].ToString().Replace("\"", ""));
                }
                else
                {
                    drr["Volume"] = "0";
                    drr["LastPrice"] = "0.00";
                    drr["UpdateDateTime"] = System.DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }
            if (ID == "CUR")
            {
                drr["ID"] = 0;
                drr["Name"] = stockDataContents[0].ToString().Replace("\"", "").Replace("=X", "");
                drr["Country"] = stockDataContents[0].ToString().Replace("\"", "").Replace("=X", "");
                drr["Currency"] = stockDataContents[0].ToString();
                drr["Rate"] = stockDataContents[1].ToString();
                drr["ChangePercent"] = stockDataContents[8].ToString().Replace("\"", "").Replace("%", "");
                drr["UpdateDateTime"] = Convert.ToDateTime(stockDataContents[2].ToString().Replace("\"", "") + " " + stockDataContents[3].ToString().Replace("\"", ""));
            }
            if (ID == "WIND")
            {
                drr["ID"] = 0;
                drr["Name"] = stockDataContents[16].ToString().Replace("\"", "");
                if (stockDataContents[4].ToString() != "N/A")
                {
                    drr["ChangeAbsolute"] = stockDataContents[4].ToString().Replace("\"", "");
                }
                else
                {
                    drr["ChangeAbsolute"] = "0.00";
                }
                drr["ChangePercent"] = stockDataContents[11].ToString().Replace("\"", "").Replace("%", "");
                if (stockDataContents[10].ToString() != "N/A")
                {
                    drr["ClosingPrice"] = stockDataContents[10].ToString();
                    drr["Country"] = stockDataContents[16].ToString();
                    drr["ClosingDate"] = Convert.ToDateTime(stockDataContents[2].ToString().Replace("\"", ""));
                    drr["UpdateDateTime"] = Convert.ToDateTime(stockDataContents[2].ToString().Replace("\"", "") + " " + stockDataContents[3].ToString().Replace("\"", ""));
                    drr["CurrentPrice"] = stockDataContents[1].ToString();
                }
                else
                {
                    drr["ClosingPrice"] = "0.00";
                    drr["Country"] = "---";
                    drr["ClosingDate"] = System.DateTime.Now.ToString("dd-MMM-yyyy");
                    drr["UpdateDateTime"] = System.DateTime.Now.ToString("dd-MMM-yyyy");
                    drr["CurrentPrice"] = "0.00";
                }

            }
            dt.Rows.Add(drr);

            response.Close();
        }
        catch (Exception ex)
        {
            //Response.Write("Error in GetStockData() 1st :: "+ex.Message.ToString());
        }
        return dt;
    }

    private DataTable GetStockData(string stockCode, StockData stockData, DataTable dt, string serverUrl, string ID, string strfirstCurrName, string strsecondCurrName)
    {

        //string serverUrl = @"http://in.finance.yahoo.com/d/quotes.csv?s=" + stockCode +
        //        "&f=sl1d1t1c1ohgvj1pp2owern&e=.csv";
        //string serverUrl = @"http://finance.yahoo.com/d/quotes.csv?s=" + stockCode +
        //        "&f=sl1d1t1c1ohgv&e=.csv";
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);

            string stockDataString = reader.ReadLine();
            string[] stockDataContents = stockDataString.Split(',');

            //stockData.Code = stockCode;
            //stockData.Last = stockDataContents[1];
            //stockData.Date = stockDataContents[2];
            //stockData.Time = stockDataContents[3];
            //stockData.Change = stockDataContents[4];
            //stockData.Open = stockDataContents[5];
            //stockData.High = stockDataContents[6];
            //stockData.Low = stockDataContents[7];
            //stockData.Volume = stockDataContents[8];
            if (ID != "CUR")
            {
                //stockData.MarketCapital = stockDataContents[9];
                //stockData.PreviousClose = stockDataContents[10];
                //stockData.PctChange = stockDataContents[11];
                //stockData.AnnRange = stockDataContents[12];
                //stockData.Earnings = stockDataContents[13];
                //stockData.PERatio = stockDataContents[14];
            }
            DataRow drr = dt.NewRow();

            if (ID == "CUR")
            {
                drr["ID"] = 0;
                drr["Name"] = strfirstCurrName.Trim();
                drr["Country"] = strsecondCurrName.Trim();
                drr["Currency"] = stockDataContents[0].ToString();
                drr["Rate"] = stockDataContents[1].ToString();
                if (stockDataContents[8].ToString() != "N/A")
                {
                    drr["ChangePercent"] = stockDataContents[8].ToString().Replace("\"", "").Replace("%", "");
                    drr["UpdateDateTime"] = Convert.ToDateTime(stockDataContents[2].ToString().Replace("\"", "") + " " + stockDataContents[3].ToString().Replace("\"", ""));
                }
                else
                {
                    drr["ChangePercent"] = "0.00";
                    drr["UpdateDateTime"] = System.DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }
            dt.Rows.Add(drr);

            response.Close();
        }
        catch (Exception ex)
        {
            //Response.Write("Error in GetStockData() 2nd :: "+ex.Message.ToString());
        }
        return dt;
    }
}
