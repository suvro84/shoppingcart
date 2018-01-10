using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Security;
using System.Configuration;
using System.Web.Mail;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Net;
using System.Diagnostics;

/// <summary>
/// Summary description for DLL
/// </summary>
public class DLL
{

    double ID;
    double IDD;
    string PID;
    string sql;
    DataSet DS;
    SqlConnection con;
    SqlDataAdapter DA;
    SqlCommand CMD;
    SqlDataReader DR;
    //	StringWriter stringWrite;
    DataGrid dg;

    public DLL()
    {
        //
        // TODO: Add constructor logic here
        //

    }


    public void DBOpen()
    {
        con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);

        try
        {

            if (con.State == ConnectionState.Open)
            {

                con.Close();

            }


            con.Open();

        }


        catch (Exception EX)
        {


        }
    }


    public void DBClose()
    {
        con.Close();
        con.Dispose();

    }

    ///cboPriority.SelectedIndex =  System.Convert.ToInt32 (DS.Tables[0].Rows[0][3].ToString ())-1;
    ///
    public void SetPopulateCombo(string sql, string TextField, string ValueField, DropDownList Combo)
    {

        DBOpen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            Combo.Items.Add(new ListItem("Please Select", ""));
            if (DS.Tables[0].Rows.Count > 0)
            {

                Combo.DataSource = DS;

                Combo.DataTextField = TextField;
                Combo.DataValueField = ValueField;
                Combo.DataBind();

            }

        }

        catch (Exception EX)
        {
        }

        finally
        {
            DBClose();
            DS.Dispose();

        }

    }


    //  modi by ....................prabir...............

    //		public void SetPopulateList(string sql,ListBox list)
    //		{
    //
    //			DBOpen();
    //			try
    //			{
    //				DA=new SqlDataAdapter (sql,con);
    //				DS=new DataSet();
    //				DA.Fill (DS);
    //				//list.Add(new ListItem("Please Select",""));
    //				if (DS.Tables[0].Rows.Count >0  )
    //				{
    //					
    //					list.DataSource =DS;
    //				
    ////					list.DataTextField =TextField;
    ////					list.DataValueField =ValueField;
    //					list.DataBind ();
    //				
    //				}
    //
    //			}
    //
    //			catch(Exception EX)
    //			{
    //			}
    //
    //			finally
    //			{
    //				DBClose ();
    //				DS.Dispose ();
    //
    //			}
    //
    //		}

    //.................................modi..........ends...........

    public void PopulateCombo(string sql, string TextField, string ValueField, DropDownList Combo)
    {

        DBOpen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            Combo.Items.Add(new ListItem("Please Select", ""));
            if (DS.Tables[0].Rows.Count > 0)
            {

                Combo.DataSource = DS;

                Combo.DataTextField = TextField;
                Combo.DataValueField = ValueField;
                Combo.DataBind();


            }


        }

        catch (Exception EX)
        {
        }

        finally
        {
            DBClose();
            DS.Dispose();

        }

    }



    public void PopulateCombo1(string sql, string TextField, string ValueField, HtmlSelect Combo)
    {

        DBOpen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            Combo.Items.Add(new ListItem("Please Select", ""));
            if (DS.Tables[0].Rows.Count > 0)
            {

                Combo.DataSource = DS;

                Combo.DataTextField = TextField;
                Combo.DataValueField = ValueField;
                Combo.DataBind();


            }


        }

        catch (Exception EX)
        {
        }

        finally
        {
            DBClose();
            DS.Dispose();

        }

    }
    public void BindGrid(GridView DG, string sql)
    {

        DBOpen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DG.DataSource = DS.Tables[0].DefaultView;
                DG.DataBind();
            }




        }
        catch (Exception EX)
        {
        }
        finally
        {
            DBClose();
            DS.Dispose();

        }
    }

    public void BindRep(DataList rep, string sql)
    {

        DBOpen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                rep.DataSource = DS.Tables[0].DefaultView;
                rep.DataBind();
            }




        }
        catch (Exception EX)
        {
        }
        finally
        {
            DBClose();
            DS.Dispose();

        }
    }

    public void BindGrid(GridView DG, string sql, ref string msg)
    {

        DBOpen();
        try
        {
            DA = new SqlDataAdapter(sql, con);
            DS = new DataSet();
            DA.Fill(DS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DG.DataSource = DS.Tables[0].DefaultView;
                DG.DataBind();
            }
            else
            {
                msg = "No record in data base";
                DG.Visible = false;
            }



        }
        catch (Exception EX)
        {
        }
        finally
        {
            DBClose();
            DS.Dispose();

        }
    }






    public SqlDataReader ReturnDR()
    {

        DBOpen();
        try
        {

            CMD = new SqlCommand(sql, con);


            DR = CMD.ExecuteReader(CommandBehavior.CloseConnection);


            if (DR.Read())
            {

                return DR;
            }




        }

        catch (Exception EX)
        {


        }

        finally
        {


            DBClose();



        }
        return DR;
    }



    public void Upload(HtmlInputFile fname, string fpath)
    {
        string serverfilename;
        //string err ;
        try
        {
            serverfilename = Path.GetFileName(fname.PostedFile.FileName);
            // Do While File.Exists(fpath & serverfilename)

            //serverfilename = Trim("1") & serverfilename
            //								 Loop
            fname.PostedFile.SaveAs(fpath + serverfilename);
            //===============================================================================================
            //ImageResize IR=new ImageResize ();
            //IR.imgResize(fpath,serverfilename);
            //IR.ImgRes (fpath + serverfilename);
            ///==============================================================================================
            ///==============================================================================================


            ///===============================================================================================
        }

        catch (Exception e)
        {

        }


    }

    public void Execute(string sql)
    {

        DBOpen();
        try
        {
            CMD = new SqlCommand(sql, con);

            CMD.ExecuteNonQuery();
        }

        catch (Exception EX)
        {

        }

        finally
        {

            DBClose();

        }


    }
    public Int32 returnExecuteNonQuery(string sql)
    {

        DBOpen();
        try
        {
            CMD = new SqlCommand(sql, con);

            return CMD.ExecuteNonQuery();
        }

        catch (Exception EX)
        {
            throw EX;
        }
        finally
        {

            DBClose();

        }


    }
    //public Int32 returnExecuteNonQuery(String query)
    //{
    //    try
    //    {
    //        CMD = new SqlCommand(query, con);
    //        CMD.CommandType = CommandType.Text;
    //        return CMD.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}

    public bool ThumbnailCallback()
    {
        return false;
    }

    public int Executeint(string sql)
    {

        DBOpen();
        try
        {

            CMD = new SqlCommand(sql, con);


            CMD.ExecuteNonQuery();
            return 1;

        }

        catch (Exception EX)
        {
            return 0;

        }

        finally
        {
            DBClose();

        }


    }


    public DataSet ExecuteDS(string sSql)
    {

        DataSet ds = new DataSet();
        try
        {

            DBOpen();

            SqlDataAdapter DA = new SqlDataAdapter(sSql, con);
            ds = new DataSet();

            DA.Fill(ds);
            return ds;

        }
        catch (Exception e)
        {
            //Syntax  <class name><method name > + Exception Message + exception trace

        }

        finally
        {

            DBClose();

        }

        return ds;
        //DBClose();

    }




    public SqlDataReader ExecuteDR(string sql)
    {

        //int tcred ;

        try
        {
            DBOpen();

            SqlCommand cmd = new SqlCommand(sql, con);
            DR = cmd.ExecuteReader();
            //return DR;


        }
        catch (Exception e)
        {
            //Syntax  <class name><method name > + Exception Message + exception trace
            //System.Diagnostics.Debug.WriteLine("<DataGate><ExecuteDS><"+ e.Message+"> "+e.ToString());
            //Insert_Into_Log("DataGet","ExecuteDS",e.Message,e.ToString(),"REXADMIN");



        }
        //			finally
        //			{
        //				
        //				DBClose ();
        //			
        //			}
        return DR;
    }


    public string Quote(string Param)
    {
        if (Param == null || Param.Length == 0)
        {
            return "";
        }
        else
        {
            return Param.Replace("'", "''");
        }
    }

    public string DQuote(string Param)
    {
        if (Param == null || Param.Length == 0)
        {
            return "";
        }
        else
        {
            return Param.Replace("''", "'");
        }
    }

    ////==============================================================================================

    //Public Shared Sub Convert(ByVal ds As System.Data.DataSet, ByVal response As System.Web.HttpResponse)
    public void Convert(DataSet DS, HttpResponse response)
    {
        response.Clear();
        response.Charset = "";
        //'set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        //	'create a string writer
        //Dim stringWrite As New System.IO.




        //Report Header
        //				hw.WriteLine("<b><u><font size='5'>" + 
        //					"Report for the Fiscal year: " + txtFY.Text + 
        //;
        //  'create an htmltextwriter which uses the stringwriter
        //Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        //	System.Web.UI.HtmlTextWriter hw = 
        //				//	new System.Web.UI.HtmlTextWriter(tw);
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        //	'instantiate a datagrid
        dg = new DataGrid();

        //'set the datagrid datasource to the dataset passed in
        dg.DataSource = DS.Tables[0].DefaultView;
        //'bind the datagrid
        dg.DataBind();
        //'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite);
        //	'all that's left is to output the html
        response.Write(stringWrite.ToString());
        response.End();



    }



    ////======================================================================================
    public void poplist(string qry, DropDownList vlist)
    {

        SqlDataReader DR;
        SqlCommand sqlComm;
        DBOpen();
        try
        {

            vlist.Items.Add(new ListItem("Please Select", ""));

            //con=new OleDbConnection(ConfigurationSettings.AppSettings["con"]);
            //																																																
            sqlComm = new SqlCommand(qry, con);

            //																																																						


            DR = sqlComm.ExecuteReader();

            //int reccount=0;
            if (DR.Read())
            {
                do
                {

                    //vlist.Items.Add(DR.GetString(1) );
                    vlist.Items.Add(new ListItem(DR.GetString(1), DR.GetInt32(0).ToString()));
                    //drpCategory.Items.Add(new ListItem(dr[1].ToString(),dr[0].ToString()));
                }

                while (DR.Read());
            }

            ////=======================================================================================


            ///==================================================================================================


        }
        catch (Exception EX)
        {


        }

        finally
        {

            DBClose();

        }


    }


    public void popDropDown(string qry, DropDownList vlist)
    {

        SqlDataReader DR;
        SqlCommand sqlComm;
        DBOpen();
        try
        {

            vlist.Items.Add(new ListItem("Please Select", ""));

            //con=new OleDbConnection(ConfigurationSettings.AppSettings["con"]);
            //																																																
            sqlComm = new SqlCommand(qry, con);

            //																																																						


            DR = sqlComm.ExecuteReader();

            //int reccount=0;
            if (DR.Read())
            {
                do
                {

                    //vlist.Items.Add(DR.GetString(1) );
                    vlist.Items.Add(new ListItem(DR.GetValue(1).ToString(), DR.GetValue(0).ToString()));
                    //drpCategory.Items.Add(new ListItem(dr[1].ToString(),dr[0].ToString()));
                }

                while (DR.Read());
            }

            ////=======================================================================================


            ///==================================================================================================


        }
        catch (Exception EX)
        {


        }

        finally
        {

            DBClose();

        }


    }


    public void poplistMod(string qry, DropDownList vlist)
    {

        SqlDataReader DR;
        SqlCommand sqlComm;
        DBOpen();
        try
        {

            vlist.Items.Add(new ListItem("Please Select", ""));

            //con=new OleDbConnection(ConfigurationSettings.AppSettings["con"]);
            //																																																
            sqlComm = new SqlCommand(qry, con);

            //																																																						


            DR = sqlComm.ExecuteReader();

            //int reccount=0;
            if (DR.Read())
            {
                do
                {

                    //vlist.Items.Add(DR.GetString(1) );
                    vlist.Items.Add(new ListItem(DR.GetString(3), DR.GetInt32(0).ToString()));
                    //drpCategory.Items.Add(new ListItem(dr[1].ToString(),dr[0].ToString()));
                }

                while (DR.Read());
            }

            ////=======================================================================================


            ///==================================================================================================


        }
        catch (Exception EX)
        {


        }

        finally
        {

            DBClose();

        }


    }





    public void poplistCat(string qry, DropDownList vlist)
    {

        SqlDataReader DR;
        SqlCommand sqlComm;
        DBOpen();
        try
        {

            vlist.Items.Add(new ListItem("--Root Category--", ""));

            //con=new OleDbConnection(ConfigurationSettings.AppSettings["con"]);
            //																																																
            sqlComm = new SqlCommand(qry, con);

            //																																																						


            DR = sqlComm.ExecuteReader();

            //int reccount=0;
            if (DR.Read())
            {
                do
                {

                    //vlist.Items.Add(DR.GetString(1) );
                    vlist.Items.Add(new ListItem(DR.GetString(1), DR.GetInt32(0).ToString()));
                    //drpCategory.Items.Add(new ListItem(dr[1].ToString(),dr[0].ToString()));
                }

                while (DR.Read());
            }

            ////=======================================================================================


            ///==================================================================================================


        }
        catch (Exception EX)
        {


        }

        finally
        {

            DBClose();

        }

    }








    //================================================================================
    //Handle session out event.

    //		public sealed class SessionBag	
    //		{
    //
    //			public static object Get(string key) 
    //			{
    //
    //				if (HttpContext.Current.Session[key] == null) 
    //				{
    //
    //					HttpContext.Current.Response.Redirect("/NoSession.aspx",true);
    //
    //					return false;
    //
    //				} 
    //				else 
    //				{
    //
    //					return HttpContext.Current.Session[key];
    //
    //				}
    //
    //			}
    //
    //		}


    //================================================================================



    public String GetPropID(string ABB)
    {

        try
        {
            double ID = GetMaxID();
            PID = ABB + ID;
        }
        catch
        {
        }

        finally
        {

            DBClose();

        }

        return PID;

    }




    public double GetMaxID()
    {
        DBOpen();
        sql = "Select ISNULL(MAX(Prop_Code),100000)+1 From tblProperty_Details";

        try
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            ID = double.Parse(cmd.ExecuteScalar().ToString());
        }
        catch
        {
        }

        finally
        {

            DBClose();

        }
        return ID;
    }


    public double CalMaxID()
    {
        DBOpen();
        sql = "Select ISNULL(MAX(OfferDetails_ID),0)+1 From tblOfferDetails";

        try
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            ID = double.Parse(cmd.ExecuteScalar().ToString());
        }
        catch
        {
        }

        finally
        {

            DBClose();

        }
        return ID;
    }


    public double FeedBackID()
    {
        DBOpen();
        sql = "Select ISNULL(MAX(IDD),0)+1 From tblContactFeedback_Details";

        try
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            IDD = double.Parse(cmd.ExecuteScalar().ToString());
        }
        catch
        {
        }

        finally
        {

            DBClose();

        }
        return IDD;
    }


    public string convertDate(string dt)
    {
        string strOpenDate = dt;
        string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
        string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
        month = month.Substring(1, month.Length - 1);
        string year = month.Substring(month.IndexOf("/"));
        month = month.Substring(0, month.IndexOf("/"));
        year = year.Substring(1, year.Length - 1);
        dt = month + "/" + day + "/" + year;
        return dt;
    }
    public void populateYear(DropDownList ddYear)
    {
        ddYear.Items.Add(new ListItem("2006", "2006"));
        ddYear.Items.Add(new ListItem("2007", "2007"));
        ddYear.Items.Add(new ListItem("2008", "2008"));
        ddYear.Items.Add(new ListItem("2009", "2009"));
        ddYear.Items.Add(new ListItem("2010", "2010"));
    }
    public void populateMonth(DropDownList ddMonth)
    {
        ddMonth.Items.Add(new ListItem("Jan", "1"));
        ddMonth.Items.Add(new ListItem("Feb", "2"));
        ddMonth.Items.Add(new ListItem("Mar", "3"));
        ddMonth.Items.Add(new ListItem("Apr", "4"));
        ddMonth.Items.Add(new ListItem("May", "5"));
        ddMonth.Items.Add(new ListItem("Jun", "6"));
        ddMonth.Items.Add(new ListItem("Jul", "7"));
        ddMonth.Items.Add(new ListItem("Aug", "8"));
        ddMonth.Items.Add(new ListItem("Sep", "9"));
        ddMonth.Items.Add(new ListItem("Oct", "10"));
        ddMonth.Items.Add(new ListItem("Nov", "11"));
        ddMonth.Items.Add(new ListItem("Dec", "12"));
    }
    public int getLastDate(int month, int year)
    {
        int day = 0;
        switch (month)
        {
            case 1: day = 31;
                break;
            case 2: if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                {
                    day = 29;
                }
                else
                {
                    day = 28;
                }
                break;
            case 3: day = 31; break; //march
            case 4: day = 30; break;//aPR
            case 5: day = 31; break;//may
            case 6: day = 30; break;//june
            case 7: day = 31; break;//july
            case 8: day = 31; break;//august 
            case 9: day = 30; break;//sep
            case 10: day = 31; break; ;//oct
            case 11: day = 30; break;//nov
            case 12: day = 31; break;//dec  			
        }
        return day;
    }
    public void SetPopulateCombo(string sql, DropDownList Combo)
    {


        try
        {
            DBOpen();

            SqlCommand cmd = new SqlCommand(sql, con);
            DR = cmd.ExecuteReader();


            Combo.Items.Add(new ListItem("Please Select", ""));
            if (DR.Read())
            {
                do
                {
                    //vlist.Items.Add(DR.GetString(1) );
                    Combo.Items.Add(new ListItem(DR.GetString(1), DR.GetValue(0).ToString()));
                    //drpCategory.Items.Add(new ListItem(dr[1].ToString(),dr[0].ToString()));
                }
                while (DR.Read());
            }

        }

        catch (Exception EX)
        {
            Debug.WriteLine(EX.ToString());
        }

    }
    public void WriteError(string Offending_URL, string Source, string Message, string Stack_trace, string Innner_Exception, string Time, string filename)
    {
        //try
        //{
        //pick filename with .xml extension
        //string filename = Server.MapPath("Log/Error"+DateTime.Now.ToString("dd_MM_yyyy") + ".xml");

        XmlDocument xmlDoc = new XmlDocument();

        try
        {
            xmlDoc.Load(filename);
        }
        catch (System.IO.FileNotFoundException)
        {
            //if file is not found, create a new xml file
            XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            xmlWriter.WriteStartElement("ICA");
            //If WriteProcessingInstruction is used as above,
            //Do not use WriteEndElement() here
            //xmlWriter.WriteEndElement();
            //it will cause the <Root></Root> to be <Root />
            xmlWriter.Close();
            xmlDoc.Load(filename);
        }
        XmlNode root = xmlDoc.DocumentElement;
        XmlElement childNode = xmlDoc.CreateElement("Error");
        XmlElement childNode2 = xmlDoc.CreateElement("Offending_URL");
        XmlElement childNode3 = xmlDoc.CreateElement("Source");
        XmlElement childNode4 = xmlDoc.CreateElement("Message");
        XmlElement childNode5 = xmlDoc.CreateElement("Stack_trace");
        XmlElement childNode6 = xmlDoc.CreateElement("Innner_Exception");
        XmlElement childNode7 = xmlDoc.CreateElement("Time");

        root.AppendChild(childNode);
        childNode.AppendChild(childNode2);
        //childNode2.SetAttribute("Name", "Value");
        XmlText urlNode = xmlDoc.CreateTextNode("url");
        urlNode.Value = Offending_URL;
        childNode2.AppendChild(urlNode);

        childNode.AppendChild(childNode3);
        //childNode3.SetAttribute("Name", "Value");
        XmlText srcNode = xmlDoc.CreateTextNode("src");
        srcNode.Value = Source;
        childNode3.AppendChild(srcNode);

        childNode.AppendChild(childNode4);
        //childNode4.SetAttribute("Name", "Value");
        XmlText msgNode = xmlDoc.CreateTextNode("msg");
        msgNode.Value = Message;
        childNode4.AppendChild(msgNode);

        childNode.AppendChild(childNode5);
        //childNode5.SetAttribute("Name", "Value");
        XmlText stNode = xmlDoc.CreateTextNode("st");
        stNode.Value = Stack_trace;
        childNode5.AppendChild(stNode);

        childNode.AppendChild(childNode6);
        //childNode6.SetAttribute("Name", "Value");
        XmlText iexpNode = xmlDoc.CreateTextNode("iexp");
        iexpNode.Value = Innner_Exception;
        childNode6.AppendChild(iexpNode);

        childNode.AppendChild(childNode7);
        //childNode7.SetAttribute("Name", "Value");
        XmlText timeNode = xmlDoc.CreateTextNode("time");
        timeNode.Value = Time;
        childNode7.AppendChild(timeNode);

        xmlDoc.Save(filename);
        //			}
        //			catch(Exception ex)
        //			{
        //				Debug.WriteLine(ex);

        //			}
    }
    //public void CreateConfirmBox(System.Web.UI.WebControls.Button btn, string strMessage){
    //btn.Attributes.Add("onclick","return confirm('"+strMessage+"');");
    //}
    //public void CreateMessageAlert(System.Web.UI.Page myPage,string strMessage,string strKey){
    //    string strScript = "<script language=JavaScript>"+
    //        "alert('"+strMessage+"');"+
    //        "</script>";
    //    if (!myPage.IsStartupScriptRegistered(strKey))
    //    {
    //        myPage.RegisterStartupScript(strKey,strScript);
    //    }
    //}
    //public void CreateMessageAlertRedirect(System.Web.UI.Page myPage,string strMessage,string strKey,string redirect)
    //{
    //    string strScript = "<script language=JavaScript>"+
    //        "alert('"+strMessage+"');"+
    //        "location.href('"+redirect+"')"+
    //        "</script>";
    //    if (!myPage.IsStartupScriptRegistered(strKey))
    //    {
    //        myPage.RegisterStartupScript(strKey,strScript);
    //    }
    //}
    public string pageAuth(string pageName, string user_id)
    {
        string sSQLPerm;
        string sPermission = "N";
        sSQLPerm = "select sp.file_name, rsp.select_flag, rsp.insert_flag, rsp.update_flag, rsp.delete_flag, rsp.password_view_flag " +
            " from roles r, role_site_page rsp, site_page sp" +
            " where r.role_id = rsp.role_id" +
            " and rsp.site_page_id = sp.site_page_id" +
            " and sp.file_name = '" + pageName + "'" +
            " and r.role_id in (select ur.role_id from user_roles ur where user_id = " + user_id + ")";
        SqlDataReader dr = ExecuteDR(sSQLPerm);
        while (dr.Read())
        {
            if (dr["select_flag"].ToString() == "1")
            {
                sPermission = "Y";
            }
        }

        return sPermission;

    }
    public string populate_menu(string user_id)
    {
        string sqlOpen;
        string sSQL;
        string sSai, sai;
        //
        DBOpen();
        sSQL = "SELECT (n.navigation_id), n.navigation_type, n.navigation_priority, n.navigation_desc, ";
        sSQL += "n.navigation_url, n.navigation_selector, n.active_flag,ur.user_id,n.menu_id,n.sub_navigation_id ";
        sSQL += "FROM Navigation AS n, Navigation_Roles as nr, User_Roles as ur ";
        sSQL += "WHERE n.navigation_selector = 'Navigation' ";
        sSQL += "AND n.active_flag = 'T' ";
        sSQL += "AND nr.role_id = ur.role_id ";
        sSQL += "AND nr.navigation_id = n.navigation_id ";
        sSQL += "ORDER BY n.navigation_selector, n.sub_navigation_priority, n.navigation_desc ";

        DA = new SqlDataAdapter(sSQL, con);
        DS = new DataSet();
        DA.Fill(DS, "Navigation");
        DataView myDV = DS.Tables[0].DefaultView;
        //
        sqlOpen = "SELECT distinct(n.navigation_id), n.navigation_type, n.navigation_priority, n.navigation_desc, ";
        sqlOpen += "n.navigation_url, n.navigation_selector, n.active_flag,ur.user_id,n.menu_id,n.sub_navigation_id ";
        sqlOpen += "FROM Navigation AS n, Navigation_Roles as nr, User_Roles as ur ";
        sqlOpen += "WHERE n.sub_navigation_id = 0 ";
        sqlOpen += "AND n.navigation_type = 'main' ";
        sqlOpen += "AND n.navigation_selector = 'Navigation' ";
        sqlOpen += "AND n.active_flag = 'T' ";
        sqlOpen += "AND nr.role_id = ur.role_id ";
        sqlOpen += "AND nr.navigation_id = n.navigation_id ";
        sqlOpen += "ORDER BY n.navigation_selector, n.navigation_priority, n.navigation_desc ";

        DA = new SqlDataAdapter(sqlOpen, con);
        DS = new DataSet();
        DA.Fill(DS, "Navigation");
        DataView mySai = DS.Tables[0].DefaultView;
        // //
        sSai = "SELECT distinct(n.navigation_id), n.navigation_type, n.navigation_priority, n.navigation_desc, ";
        sSai += "n.navigation_url, n.navigation_selector, n.active_flag,n.Images_URL ";
        sSai += "FROM Navigation AS n ";
        sSai += "WHERE n.navigation_type = 'menu' ";
        sSai += "AND n.active_flag = 'T' ";
        sSai += "ORDER BY n.navigation_selector, n.navigation_priority, n.navigation_desc ";
        SqlDataReader DRS = ExecuteDR(sSai);

        sai = "<div id=testHtmlMenu>";
        sai += "<ul style=VISIBILITY: hidden>";
        while (DRS.Read())
        {
            sai += "<li>";
            sai += "<span></span><A href=" + DRS[4].ToString() + "><IMG src=" + DRS[7].ToString() + " border=0></A>";
            sai += "<ul>";
            //
            mySai.RowFilter = "";
            mySai.RowFilter = "user_id = '" + user_id + "' and menu_id = '" + DRS[0].ToString() + "'";
            //
            foreach (DataRowView rowvi in mySai)
            {
                sai += "<li>";
                sai += "<span></span><A href=" + rowvi.Row["navigation_url"].ToString() + ">" + rowvi.Row["navigation_desc"].ToString() + "</A>";

                myDV.RowFilter = "";
                myDV.RowFilter = "user_id = '" + user_id + "' and menu_id = '" + DRS[0].ToString() + "' and sub_navigation_id= '" + rowvi.Row["navigation_id"].ToString() + "'";
                if (myDV.Count > 0)
                {
                    sai += "<ul>";

                    foreach (DataRowView rowview in myDV)
                    {
                        sai += "<li>";
                        //sai+="<span></span><A href="+DRR[4].ToString()+">"+DRR[3].ToString()+"</A>";
                        sai += "<span></span><A href=" + rowview.Row["navigation_url"].ToString() + ">" + rowview.Row["navigation_desc"].ToString() + "</A>";
                    }
                    sai += "</li>";
                    sai += "</ul>";
                }
            }
            sai += "</li>";
            sai += "</ul>";
        }
        sai += "</li>";
        sai += "</ul>";
        sai += "</div>";
        DBClose();
        con.Close();
        return sai;
    }
    public DataTable FetchRecords(string strSQL)
    {
        try
        {
            //Dim conSQL As New SqlConnection(m_ConString)
            //Dim da As New SqlDataAdapter(strSQL, conSQL)
            //Dim ds As New DataSet("mDS")
            con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);

            DA = new SqlDataAdapter(strSQL, con);
            DS = new DataSet();
            DA.Fill(DS);
            return DS.Tables[0];
        }
        catch (Exception ex)
        {
            //LogError(ex)
            return null;
        }
    }



    public void DeleteShoppingCart(string sessionId)
    {
        DBOpen();
        sql = "Delete From ShoppingCart_Products Where cartid='" + sessionId + "' ";
        try
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            DR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
        }

        finally
        {

            DBClose();

        }

    }


}


