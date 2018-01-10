using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;

/// <summary>
/// Summary description for DataManipulationClass
/// </summary>
public class DataManipulationClass
{
    // holds database database connection name.
    private string _conn = string.Empty;

    // holds database database schema name.
    private string _schema = string.Empty;
    public DataManipulationClass()
    {
        _conn = Convert.ToString(ConfigurationManager.AppSettings["DBCON_LinkExchange"]);
        _schema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);
    }
    public bool CheckAdminLogin(ref LoginCooky objRef)
    {
        bool flag = false;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strsql = "SELECT " +
                            _schema + ".user_master.Id AS [id], " +
                            _schema + ".user_master.Name AS [Name], " +
                            _schema + ".user_master.Type AS [Type] " +
                            "FROM " +
                            _schema + ".user_master " +
                            "WHERE " +
                            "(" + _schema + ".user_master.name='" + objRef.LoginUserName + "' AND " +
                            _schema + ".user_master.Password='" + objRef.LoginPassword + "') AND " +
                            _schema + ".user_master.recordStatus=" + 1 + "";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strsql, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            objRef.LoginId = Convert.ToString(rdr["id"]);
                            objRef.LoginUserName = Convert.ToString(rdr["Name"]);
                            //objRef.LoginType = (UserType)Convert.ToInt32(rdr["Type"]);
                            objRef.LoginStatus = true;
                        }
                    }
                    else
                    {
                        objRef.LoginStatus = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
                //new MailOnExceptions().SendMail("Error : -" + ex.Message, "Error in CheckAdminLogin() in DataManipulationClass.");
            }
        }
        return flag;
    }
    public bool InsertORUpdateIntoTable(string strSQL)
    {
        {
            bool flag = false;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                    {
                        cmd.Transaction = trans;
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        flag = true;
                    }
                }
                catch (SqlException ex)
                {
                    flag = false;
                    trans.Rollback();
                    //new MailOnExceptions().SendMail("Error : -" + ex.Message, "Error in InsertIntoTable() in DataManipulationClass.");
                }
            }
            return flag;
        }
    }
    public DataTable FillDataTable(string strSQL)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSQL, conn))
                {
                    da.Fill(dt);
                }
            }
        }
        catch (SqlException ex)
        {
            //  new MailOnExceptions().SendMail("Error : -" + ex.Message, "Error in FillDataTable() in DataManipulationClass.");
        }
        return dt;
    }

    public DataSet FillDataSet(string strSQL)
    {
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSQL, conn))
                {
                    da.Fill(ds);
                }
            }
        }
        catch (SqlException ex)
        {
            //new MailOnExceptions().SendMail("Error : -" + ex.Message, "Error in FillDataTable() in DataManipulationClass.");
        }
        return ds;

    }
    public DataTable FillDataTable(string strSQL, ref string strError)
    {
        strError = null;
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSQL, conn))
                {
                    da.Fill(dt);
                }
            }
        }
        catch (SqlException ex)
        {
            strError = ex.Message;
            //  new MailOnExceptions().SendMail("Error : -" + ex.Message, "Error in FillDataTable() in DataManipulationClass.");
        }
        return dt;
    }
    public void DropDownFill(object ddlDropDown, string strSQL, string strTextField, string strValueField, string strschema, ref string strError)
    {
        strError = null;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                //strsql = Regex.Replace(strSQL, strschema, _schema);
                DropDownList ddlToFill = (DropDownList)ddlDropDown;
                ddlToFill.Items.Clear();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                conn.Open();
                da.Fill(ds, "Table_DropDown");
                ListItem liSelect = new ListItem();
                liSelect.Text = "[---Please Select---]";
                liSelect.Value = "";
                ddlToFill.Items.Add(liSelect);
                foreach (DataRow dr in ds.Tables["Table_DropDown"].Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = dr[strTextField].ToString();
                    li.Value = dr[strValueField].ToString();
                    ddlToFill.Items.Add(li);
                }
                ddlToFill.DataBind();
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
    }




    public string AddUser(UserRegistration objreg, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spAddUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = objreg.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = objreg.LastName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = objreg.Password;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = objreg.Email;
                    cmd.Parameters.Add("@Email1", SqlDbType.VarChar).Value = objreg.Email1;
                    cmd.Parameters.Add("@UserType", SqlDbType.VarChar).Value = objreg.UserType;
                    cmd.Parameters.Add("@SMTPServer", SqlDbType.VarChar).Value = objreg.SMTPServer;
                    cmd.Parameters.Add("@SMTP_Port", SqlDbType.VarChar).Value = objreg.SMTP_Port;
                    cmd.Parameters.Add("@SMTP_UserName", SqlDbType.VarChar).Value = objreg.SMTP_UserName;
                    cmd.Parameters.Add("@SMTP_Password", SqlDbType.VarChar).Value = objreg.SMTP_Password;
                    // cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = objreg.DisplayName;
                    cmd.Parameters.Add("@SSL", SqlDbType.Bit).Value = objreg.SSL;
                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }
                    else if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "f")
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                        strError = "The name " + objreg.Email + " is already registered with us. If you have forgotten your password, <a style=\"color:#DB0101\" href=\"forgot-password.aspx\">Click here</a>";
                    }
                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }
    public string AddWebSiteDetail(WebSiteDetail objsite, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spAddWebsiteDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = objsite.Name;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = objsite.Description;
                    cmd.Parameters.Add("@SiteURL", SqlDbType.VarChar).Value = objsite.SiteURL;
                    cmd.Parameters.Add("@SMTPServer", SqlDbType.VarChar).Value = objsite.SMTPServer;
                    cmd.Parameters.Add("@SMTP_Port", SqlDbType.VarChar).Value = objsite.SMTP_Port;
                    cmd.Parameters.Add("@SMTP_UserName", SqlDbType.VarChar).Value = objsite.SMTP_UserName;
                    cmd.Parameters.Add("@SMTP_Password", SqlDbType.VarChar).Value = objsite.SMTP_Password;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = objsite.UserId;
                    cmd.Parameters.Add("@AdId", SqlDbType.Int).Value = objsite.AdId;
                    cmd.Parameters.Add("@Signature", SqlDbType.VarChar).Value = objsite.Signature;
                    cmd.Parameters.Add("@SSL", SqlDbType.Bit).Value = objsite.SSL;
                    cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = objsite.DisplayName;

                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }
                    else if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "f")
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                        strError = "This URL already exists in your account. Duplicate entries for the same URL is not permitted.";
                    }
                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }



    public string AddInitiateLink(LinkExchange objLinkExchange, string mode, string SiteURL, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spAddInitiateLink", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OurAdId", SqlDbType.VarChar).Value = objLinkExchange.OurAdId;
                    cmd.Parameters.Add("@SubPageId", SqlDbType.VarChar).Value = objLinkExchange.SubPageId;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = objLinkExchange.Status;
                    cmd.Parameters.Add("@Reciprocal", SqlDbType.VarChar).Value = objLinkExchange.Reciprocal;
                    cmd.Parameters.Add("@HTMLcode", SqlDbType.VarChar).Value = objLinkExchange.HTMLcode;
                    cmd.Parameters.Add("@PageRank", SqlDbType.VarChar).Value = objLinkExchange.PageRank;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = objLinkExchange.Type;
                    cmd.Parameters.Add("@WebSiteId", SqlDbType.VarChar).Value = objLinkExchange.WebSiteId;
                    cmd.Parameters.Add("@From_url", SqlDbType.VarChar).Value = objLinkExchange.From_url;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = objLinkExchange.Email;
                    cmd.Parameters.Add("@fName", SqlDbType.VarChar).Value = objLinkExchange.FName;
                    cmd.Parameters.Add("@lName", SqlDbType.VarChar).Value = objLinkExchange.LName;
                    cmd.Parameters.Add("@mode", SqlDbType.VarChar).Value = mode;
                    cmd.Parameters.Add("@SiteURL", SqlDbType.VarChar).Value = SiteURL;
                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }
                    else if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "f")
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                        strError = "Link exchange exists against this URL. Duplicate entry not permitted.";
                    }
                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                        strError = "Link Exchange request initiated.Please continue to add more links";

                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }

    public string UpdateWebSiteDetail(WebSiteDetail objsite, ref string strError, string type)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spEditWebsiteDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = objsite.Id;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = objsite.Name;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = objsite.Description;
                    cmd.Parameters.Add("@SiteURL", SqlDbType.VarChar).Value = objsite.SiteURL;
                    cmd.Parameters.Add("@SMTPServer", SqlDbType.VarChar).Value = objsite.SMTPServer;
                    cmd.Parameters.Add("@SMTP_Port", SqlDbType.VarChar).Value = objsite.SMTP_Port;
                    cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = objsite.DisplayName;
                    cmd.Parameters.Add("@SMTP_UserName", SqlDbType.VarChar).Value = objsite.SMTP_UserName;
                    cmd.Parameters.Add("@SMTP_Password", SqlDbType.VarChar).Value = objsite.SMTP_Password;
                    cmd.Parameters.Add("@Signature", SqlDbType.VarChar).Value = objsite.Signature;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 250).Value = type;
                    cmd.Parameters.Add("@SSL", SqlDbType.Bit).Value = objsite.SSL;
                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }

                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }

    public string UpdateSubPageDetail(SubPage objSubPage, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spEditSubPage", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = objSubPage.Id;
                    cmd.Parameters.Add("@SubPageName", SqlDbType.VarChar).Value = objSubPage.SubPageName;
                    cmd.Parameters.Add("@PageDescription", SqlDbType.VarChar).Value = objSubPage.PageDescription;
                    cmd.Parameters.Add("@LinkURL", SqlDbType.VarChar).Value = objSubPage.LinkURL;
                    //cmd.Parameters.Add("@OTHERCODE", SqlDbType.VarChar).Value = objSubPage.OTHERCODE;
                    //cmd.Parameters.Add("@CODEINPAGE", SqlDbType.VarChar).Value = objSubPage.CODEINPAGE;

                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }

                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }
    public string AddLinkDetail(LinkDetail objLink, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spAddLink", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Heading", SqlDbType.VarChar).Value = objLink.Heading;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = objLink.Description;
                    cmd.Parameters.Add("@SiteURL", SqlDbType.VarChar).Value = objLink.SiteURL;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = objLink.UserId;
                    cmd.Parameters.Add("@WebSiteId", SqlDbType.Int).Value = objLink.WebSiteId;
                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }

                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }



    public string AddEmailCorrespondence(EmailCorrespondence objEmail, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spEmailCorrespondence", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LinkId", SqlDbType.VarChar).Value = objEmail.Linkid;
                    cmd.Parameters.Add("@Subject", SqlDbType.VarChar).Value = objEmail.Subject;
                    cmd.Parameters.Add("@Body", SqlDbType.VarChar).Value = objEmail.Body;
                    cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = objEmail.DisplayName;
                    cmd.Parameters.Add("@FromEmail", SqlDbType.VarChar).Value = objEmail.FromEmail;
                    cmd.Parameters.Add("@ToEmail", SqlDbType.VarChar).Value = objEmail.ToEmail;
                    cmd.Parameters.Add("@TemplateId", SqlDbType.Int).Value = objEmail.TemplateId;
                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }

                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }


    public GCommon<WebSiteDetail> GetSiteCollection(int UserId)
    {
        GCommon<WebSiteDetail> coll = new GCommon<WebSiteDetail>();
        WebSiteDetail objSite = null;

        //string strSQL = "SELECT " +
        //                _schema + ".Country_Master.id, " +
        //                _schema + ".Country_Master.Name, " +
        //                _schema + ".Country_Master.RecordStatus " +
        //                "FROM " +
        //                _schema + ".Country_Master ORDER BY " + _schema + ".Country_Master.Name ";

        //string strSQL = "SELECT ID,SiteURL FROM LinkExchange.WebSite_Master WHERE UserID=" + UserId + "";
        string strSQL = "SELECT ID,SiteURL FROM LinkExchange.WebSite_Master where UserId=" + UserId + " ";

        DataTable dt = FillDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objSite = new WebSiteDetail();

                objSite.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                objSite.SiteURL = Convert.ToString(dt.Rows[i]["SiteURL"]);


                coll.Add(objSite);
            }
        }
        return coll;
    }


    public GCommon<LinkDetail> GetHeadingCollection(string WebSiteID)
    {
        GCommon<LinkDetail> coll = new GCommon<LinkDetail>();
        LinkDetail objLink = null;

        //string strSQL = "SELECT " +
        //                _schema + ".Country_Master.id, " +
        //                _schema + ".Country_Master.Name, " +
        //                _schema + ".Country_Master.RecordStatus " +
        //                "FROM " +
        //                _schema + ".Country_Master ORDER BY " + _schema + ".Country_Master.Name ";

        //string strSQL = "SELECT ID,SiteURL FROM LinkExchange.WebSite_Master WHERE UserID=" + UserId + "";
        string strSQL = "SELECT ID,Heading FROM LinkExchange.LinkMaster where WebSiteID=" + WebSiteID + " ";

        DataTable dt = FillDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objLink = new LinkDetail();

                objLink.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                objLink.Heading = Convert.ToString(dt.Rows[i]["Heading"]);


                coll.Add(objLink);
            }
        }
        return coll;
    }



    public GCommon<EmailTemplate> GetEmailTemplateCollection()
    {
        GCommon<EmailTemplate> coll = new GCommon<EmailTemplate>();
        EmailTemplate objEmailTemplate = null;


        string strSQL = "SELECT TemplateID,TemplateName FROM LinkExchange.EmailTemplate where LinkExchange.EmailTemplate.RecordStatus=1  order by TemplateName";

        DataTable dt = FillDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objEmailTemplate = new EmailTemplate();

                objEmailTemplate.TemplateID = Convert.ToInt32(dt.Rows[i]["TemplateID"]);
                objEmailTemplate.TemplateName = Convert.ToString(dt.Rows[i]["TemplateName"]);
                coll.Add(objEmailTemplate);
            }
        }
        return coll;
    }

    public LinkDetail fetch_InsertedData(int UserId, ref  string SiteURL)
    {




        string strSQL = "  SELECT  LinkExchange.LinkMaster.Heading," +
                        " LinkExchange.LinkMaster.SiteURL, " +
                        " LinkExchange.LinkMaster.WebSiteId, " +
                        " LinkExchange.LinkMaster.Description, " +
                        "  LinkExchange.WebSite_Master.SiteURL " +
                         " FROM LinkExchange.LinkMaster " +
                        " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkMaster.WebSiteID = LinkExchange.WebSite_Master.ID)" +
                        " WHERE   LinkExchange.LinkMaster.id = (" +
                        " select max(LinkExchange.LinkMaster.id) from LinkExchange.LinkMaster " +
                        " ) " +
                        " and " +
                        " LinkExchange.LinkMaster.UserId=" + UserId + " ";

        //        SELECT 
        //  LinkExchange.LinkMaster.Heading,
        //  LinkExchange.LinkMaster.SiteURL,
        //  LinkExchange.LinkMaster.Description,
        //  LinkExchange.LinkMaster.WebSiteID,
        //  LinkExchange.WebSite_Master.Name
        //FROM
        //  LinkExchange.LinkMaster
        //  INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkMaster.WebSiteID = LinkExchange.WebSite_Master.ID)
        //WHERE
        //  (LinkExchange.LinkMaster.id = (SELECT max(LinkExchange.LinkMaster.id) AS FIELD_1 FROM LinkExchange.LinkMaster)) AND 
        //  (LinkExchange.LinkMaster.UserId = 1) 


        LinkDetail objLink = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objLink = new LinkDetail();

                    objLink.Heading = Convert.ToString(dt.Rows[i]["Heading"]);
                    objLink.SiteURL = Convert.ToString(dt.Rows[i]["SiteURL"]);
                    objLink.WebSiteId = Convert.ToInt32(dt.Rows[i]["WebSiteId"]);
                    objLink.Description = Convert.ToString(dt.Rows[i]["Description"]);
                    SiteURL = Convert.ToString(dt.Rows[i]["SiteURL"]);

                }
            }
        }

        return objLink;
    }
    public LinkDetail fetch_LinkDetailData(int id)
    {




        string strSQL = "  SELECT  LinkExchange.LinkMaster.Heading," +
                        " LinkExchange.LinkMaster.SiteURL, " +
                        " LinkExchange.LinkMaster.WebSiteId, " +
                        " LinkExchange.LinkMaster.Description " +
                         " FROM LinkExchange.LinkMaster " +
                        " WHERE   LinkExchange.LinkMaster.id = " + id + " ";

        LinkDetail objLink = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objLink = new LinkDetail();

                    objLink.Heading = Convert.ToString(dt.Rows[i]["Heading"]);
                    objLink.SiteURL = Convert.ToString(dt.Rows[i]["SiteURL"]);
                    objLink.WebSiteId = Convert.ToInt32(dt.Rows[i]["WebSiteId"]);
                    objLink.Description = Convert.ToString(dt.Rows[i]["Description"]);

                }
            }
        }

        return objLink;
    }


    public GCommon<LinkDetail> fetch_LinkDetailData_website(string id)
    {
        GCommon<LinkDetail> coll = new GCommon<LinkDetail>();
        LinkDetail objLink = null;
        string strSQL = "  SELECT  LinkExchange.LinkMaster.id,LinkExchange.LinkMaster.Heading," +
                        " LinkExchange.LinkMaster.SiteURL, " +
                        " LinkExchange.LinkMaster.WebSiteId, " +
                        " LinkExchange.LinkMaster.Description " +
                         " FROM LinkExchange.LinkMaster " +
                        " WHERE   LinkExchange.LinkMaster.WebSiteId = " + id + " ";

        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objLink = new LinkDetail();
                    objLink.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    objLink.Heading = Convert.ToString(dt.Rows[i]["Heading"]);
                    objLink.SiteURL = Convert.ToString(dt.Rows[i]["SiteURL"]);
                    objLink.Description = Convert.ToString(dt.Rows[i]["Description"]);
                    coll.Add(objLink);
                }
            }
        }

        return coll;
    }

    public EmailTemplate fetch_EmailTemplateData(int TemplateID)
    {

        string strSQL = "  SELECT  LinkExchange.EmailTemplate.TemplateId,LinkExchange.EmailTemplate.TemplateName," +
                        " LinkExchange.EmailTemplate.TemplateSubject, " +
                        " LinkExchange.EmailTemplate.TemplateCode " +
                         " FROM LinkExchange.EmailTemplate " +
                        " WHERE   LinkExchange.EmailTemplate.TemplateID = " + TemplateID + "";
        EmailTemplate objEmailTemplate = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objEmailTemplate = new EmailTemplate();
                    objEmailTemplate.TemplateID = Convert.ToInt32(dt.Rows[i]["TemplateID"]);
                    objEmailTemplate.TemplateName = Convert.ToString(dt.Rows[i]["TemplateName"]);
                    objEmailTemplate.TemplateSubject = Convert.ToString(dt.Rows[i]["TemplateSubject"]);
                    objEmailTemplate.TemplateCode = Convert.ToString(dt.Rows[i]["TemplateCode"]);
                }
            }
        }

        return objEmailTemplate;
    }

    public WebSiteDetail fetch_WebSiteDetailData(string SiteId)
    {

        string strSQL = "  SELECT  LinkExchange.WebSite_Master.Name," +
                        " LinkExchange.WebSite_Master.Description, " +
                        " LinkExchange.WebSite_Master.SiteURL, " +
                        " LinkExchange.WebSite_Master.SMTPServer, " +
                        " LinkExchange.WebSite_Master.SMTP_Port, " +
                        " LinkExchange.WebSite_Master.DisplayName, " +
                        " LinkExchange.WebSite_Master.SMTP_UserName, " +
                        " LinkExchange.WebSite_Master.SMTP_Password, " +
                        " LinkExchange.WebSite_Master.Signature, " +
                        " CASE WHEN LinkExchange.WebSite_Master.SSL IS NULL THEN '0' ELSE LinkExchange.WebSite_Master.SSL END as SSl " +
                        " FROM LinkExchange.WebSite_Master " +
                        " WHERE  LinkExchange.WebSite_Master.id = " + SiteId + "";
        WebSiteDetail objWebSite = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objWebSite = new WebSiteDetail();
                    objWebSite.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    objWebSite.Description = Convert.ToString(dt.Rows[i]["Description"]);
                    objWebSite.SiteURL = Convert.ToString(dt.Rows[i]["SiteURL"]);
                    objWebSite.SMTPServer = Convert.ToString(dt.Rows[i]["SMTPServer"]);
                    objWebSite.SMTP_Port = Convert.ToString(dt.Rows[i]["SMTP_Port"]);
                    objWebSite.DisplayName = Convert.ToString(dt.Rows[i]["DisplayName"]);
                    objWebSite.SMTP_UserName = Convert.ToString(dt.Rows[i]["SMTP_UserName"]);
                    objWebSite.SMTP_Password = Convert.ToString(dt.Rows[i]["SMTP_Password"]);
                    objWebSite.Signature = Convert.ToString(dt.Rows[i]["Signature"]);
                    objWebSite.SSL = Convert.ToBoolean(dt.Rows[i]["SSL"]);
                }
            }
        }

        return objWebSite;
    }


    public SubPage fetch_SubPageData(string id)
    {

        string strSQL = "  SELECT  LinkExchange.SubPage.SubPageName," +
                        " LinkExchange.SubPage.LinkURL, " +
                        " LinkExchange.SubPage.PageRank, " +
                        " LinkExchange.SubPage.CODEINPAGE, " +
                        " LinkExchange.SubPage.PageDescription, " +
                        " LinkExchange.SubPage.OTHERCODE " +
                        " FROM LinkExchange.SubPage " +
                        " WHERE  LinkExchange.SubPage.id = " + id + "";
        SubPage objSubPage = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objSubPage = new SubPage();
                    objSubPage.SubPageName = Convert.ToString(dt.Rows[i]["SubPageName"]);
                    objSubPage.LinkURL = Convert.ToString(dt.Rows[i]["LinkURL"]);
                    objSubPage.PageRank = Convert.ToInt32(dt.Rows[i]["PageRank"]);
                    objSubPage.CODEINPAGE = Convert.ToString(dt.Rows[i]["CODEINPAGE"]);
                    objSubPage.PageDescription = Convert.ToString(dt.Rows[i]["PageDescription"]);
                    objSubPage.OTHERCODE = Convert.ToString(dt.Rows[i]["OTHERCODE"]);
                    //  objSubPage.SMTP_UserName = Convert.ToString(dt.Rows[i]["OTHERCODE"]);
                }
            }
        }

        return objSubPage;
    }

    public GCommon<SubPage> PopulateSubPage(string SiteId)
    {
        GCommon<SubPage> coll = new GCommon<SubPage>();
        string strSQL = "  SELECT  LinkExchange.SubPage.SubPageName," +
                        " LinkExchange.SubPage.id " +
                        " FROM LinkExchange.SubPage " +
                        " WHERE  LinkExchange.SubPage.websiteid = " + SiteId + "";
        SubPage objSubPage = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objSubPage = new SubPage();
                    objSubPage.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    objSubPage.SubPageName = Convert.ToString(dt.Rows[i]["SubPageName"]);
                    coll.Add(objSubPage);
                }
            }
        }

        return coll;
    }

    public bool get_siteid(string SiteURL, ref  int siteid)
    {
        bool flag = false;
        int id = 0;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.LinkMaster.id" +
                      " FROM LinkExchange.LinkMaster " +
                      " WHERE   LinkExchange.LinkMaster.SiteURL = '" + SiteURL + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            siteid = Convert.ToInt32(rdr["id"]);

                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public bool get_siteidfromWebsiteMaster(string SiteUrl, ref  string siteid)
    {
        bool flag = false;
        int id = 0;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.WebSite_Master.id" +
                      " FROM LinkExchange.WebSite_Master " +
                      " WHERE   LinkExchange.WebSite_Master.SiteUrl = '" + SiteUrl + "' and UserId=" + Convert.ToString(HttpContext.Current.Session["UserId"]) + "";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            siteid = Convert.ToString(rdr["ID"]);

                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }


    public bool get_siteNamefromWebsiteMaster(string siteid, ref  string SiteUrl)
    {
        bool flag = false;
        int id = 0;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.WebSite_Master.SiteUrl" +
                      " FROM LinkExchange.WebSite_Master " +
                      " WHERE   LinkExchange.WebSite_Master.id = '" + siteid + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            SiteUrl = Convert.ToString(rdr["SiteUrl"]);

                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }
    public void Execute(string sql, ref string strError)
    {

        using (SqlConnection conn = new SqlConnection(_conn))
        {

            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {


                    //adp.Fill(dt);

                    // SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
                conn.Close();
            }
            finally
            {
                //dr.Close();
                conn.Close();
            }
        }
    }



    public bool get_ImgLogoPath(string SiteId, ref  string ImgLogoPath)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.WebSite_Master.ImgLogoPath" +
                      " FROM LinkExchange.WebSite_Master " +
                      " WHERE   LinkExchange.WebSite_Master.Id = '" + SiteId + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            ImgLogoPath = Convert.ToString(rdr["ImgLogoPath"]);
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
            }
        }
        return flag;
    }


    public string getloginId(string siteid)
    {


        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.WebSite_Master.userid" +
                      " FROM LinkExchange.WebSite_Master " +
                      " WHERE   LinkExchange.WebSite_Master.id = '" + siteid + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {

                        if (rdr.Read())
                        {
                            siteid = Convert.ToString(rdr["userid"]);

                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return siteid;
    }


    public string getSiteName(string siteid)
    {
        string SiteName = "";

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.WebSite_Master.Name" +
                      " FROM LinkExchange.WebSite_Master " +
                      " WHERE   LinkExchange.WebSite_Master.id = '" + siteid + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {

                        if (rdr.Read())
                        {
                            SiteName = Convert.ToString(rdr["Name"]);

                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return SiteName;
    }

    public string getSubPageName(string SubPageId)
    {
        string SubPageName = "";

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.SubPage.SubPageName" +
                      " FROM LinkExchange.SubPage " +
                      " WHERE   LinkExchange.SubPage.id = '" + SubPageId + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {

                        if (rdr.Read())
                        {
                            SubPageName = Convert.ToString(rdr["SubPageName"]);

                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return SubPageName;
    }


    public bool getForgotPwd(string email, ref string Password, ref string UserName, ref string LastName)
    {
        bool flag = false;
        string strSQL = "select LinkExchange.UserRegistration.username,LinkExchange.UserRegistration.lastname,LinkExchange.UserRegistration.password from LinkExchange.UserRegistration where LinkExchange.UserRegistration.email='" + email + "' ";

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            Password = Convert.ToString(rdr["password"]);
                            UserName = Convert.ToString(rdr["UserName"]);
                            LastName = Convert.ToString(rdr["LastName"]);

                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public bool SendMail(string mailTo, string mailFrom, string mailFromDisplayName, string mailCC, string mailBody, string mailSubject, ref string strError)
    {
        bool flag = true;
        MailMessage objMail = new MailMessage();
        objMail.To.Add(mailTo);
        //objMail.To.Add("ravi@giftstoindia24x7.com");
        objMail.From = new MailAddress(mailFrom, mailFromDisplayName);
        //objMail.CC.Add(mailCC);
        objMail.Subject = mailSubject;
        objMail.Body = mailBody;
        objMail.IsBodyHtml = true;
        objMail.Priority = MailPriority.High;
        //objMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        try
        {
            SmtpClient client = new SmtpClient("mail.reliablelinkexchange.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("noreply@reliablelinkexchange.com", "reply8521");
            client.Port = 25;
            client.Send(objMail);
            flag = true;

        }
        catch (SmtpException ex)
        {
            flag = false;
            strError = ex.Message;
        }
        return flag;
    }
    public string makeMailBody(ref string strUserName, ref string strLastName, ref string password, string email)
    {
        string retValue = string.Empty;
        retValue = "<table cellpadding='0' cellspacing='0' width='100%' border='0' style=\"font-size: 10pt; font-family: Verdana\">";

        retValue += "<tr>";
        retValue += "<td colspan='2'>Dear " + strUserName + "  " + strLastName + ",</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td colspan='2'>Below is the password requested by you for the username -" + email + "</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td colspan='2'>" + password + "</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td colspan='2'>Please do not reply to this email id as it is not monitored by anyone. To write to us <a href=\"http://www.reliablelinkexchange.com/Ask.aspx\">Click here</a></td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td colspan='2'>Regards,</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td colspan='2'>Team ReliableLinkExchange</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td colspan='2'>www.reliablelinkexchange.com</td>";
        retValue += "</tr>";



        //retValue += "<tr>";
        //retValue += "<td width='15%'>&nbsp;</td>";
        //retValue += "<td width='85%'>&nbsp;</td>";
        //retValue += "</tr>";

        //retValue += "<tr>";
        //retValue += "<td> </td>";
        //retValue += "<td>" + password + "</td>";
        //retValue += "</tr>";

        //retValue += "<tr>";
        //retValue += "<td>Last Name</td>";
        //retValue += "<td>" + txtlastName.Text + "</td>";
        //retValue += "</tr>";

        retValue += "</table>";
        return retValue;
    }

    public bool getUserName(string UserId, ref string UserName, ref string LastName)
    {
        bool flag = false;
        string strSQL = "select LinkExchange.UserRegistration.username,LinkExchange.UserRegistration.lastname from LinkExchange.UserRegistration where LinkExchange.UserRegistration.UserId='" + UserId + "' ";

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {

                            UserName = Convert.ToString(rdr["UserName"]);
                            LastName = Convert.ToString(rdr["LastName"]);

                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public string replaceHtmltoText(string strHTML)
    {
        return System.Text.RegularExpressions.Regex.Replace(strHTML, "<[^>]*>", "");
    }



    public string AddEmailTemplate(EmailTemplate objEmailTemplate, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spAddEmailTemplate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar).Value = objEmailTemplate.TemplateName;
                    cmd.Parameters.Add("@TemplateSubject", SqlDbType.VarChar).Value = objEmailTemplate.TemplateSubject;
                    cmd.Parameters.Add("@TemplateCode", SqlDbType.Text).Value = objEmailTemplate.TemplateCode;


                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }
                    else if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "f")
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                        strError = "The Template Name " + objEmailTemplate.TemplateName + " has already  been inserted.Please try again";
                    }
                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }


    public bool UpdateEmailTemplate(int TemplateID, string TemplateName, string TemplateSubject, string TemplateCode, ref string strError)
    {
        string strSQL = "update LinkExchange.EmailTemplate set LinkExchange.EmailTemplate.TemplateName='" + TemplateName + "',LinkExchange.EmailTemplate.TemplateSubject='" + TemplateSubject + "',LinkExchange.EmailTemplate.TemplateCode='" + TemplateCode + "'  where TemplateID=" + TemplateID + "";
        //Execute(strSQL, strError);
        bool flag = false;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
                conn.Close();
                flag = false;
            }
            finally
            {
                //dr.Close();
                conn.Close();
            }
        }
        return flag;

    }

    public bool UpdateLinkDetailData(int id, string Heading, string Description, string SiteURL, ref string strError)
    {
        string strSQL = "update LinkExchange.LinkMaster set LinkExchange.LinkMaster.Heading='" + Heading + "',LinkExchange.LinkMaster.Description='" + Description + "',LinkExchange.LinkMaster.SiteURL='" + SiteURL + "'  where id=" + id + "";
        //Execute(strSQL, strError);
        bool flag = false;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
                conn.Close();
                flag = false;
            }
            finally
            {
                //dr.Close();
                conn.Close();
            }
        }
        return flag;

    }


    public bool UpdateStatus(int LinkId, string status, ref string strError)
    {
        string strSQL = "update LinkExchange.LinkExchangeMaster set LinkExchange.LinkExchangeMaster.status='" + status + "'  where id=" + LinkId + "";
        //Execute(strSQL, strError);
        bool flag = false;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
                conn.Close();
                flag = false;
            }
            finally
            {
                //dr.Close();
                conn.Close();
            }
        }
        return flag;

    }


    public bool get_MailDetail(string SiteId, string LnkexcMasterId, ref  string SiteName, ref string SMTP_UserName, ref string Signature, ref string fName, ref string lName, ref string email, ref string Reciprocal, ref string HTMLcode, ref string Description, ref string Heading, ref string SiteURL)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }

            string strSQL = "SELECT " +
                          " LinkExchange.LinkExchangeMaster.fName," +
                          " LinkExchange.LinkExchangeMaster.lName," +
                          " LinkExchange.LinkExchangeMaster.Reciprocal," +
                          " LinkExchange.LinkExchangeMaster.HTMLcode," +
                          " LinkExchange.LinkExchangeMaster.email," +
                          " LinkExchange.WebSite_Master.Name," +
                          " LinkExchange.WebSite_Master.SMTP_UserName," +
                          " LinkExchange.WebSite_Master.Signature," +
                          " LinkExchange.LinkExchangeMaster.email," +
                          " LinkExchange.LinkMaster.Description, " +
                          " LinkExchange.LinkMaster.Heading, " +
                          " LinkExchange.LinkMaster.SiteURL " +
                          " FROM " +
                          " LinkExchange.LinkExchangeMaster" +
                          " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId = LinkExchange.WebSite_Master.ID)" +
                          " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
                          " WHERE   LinkExchange.WebSite_Master.id = '" + SiteId + "'" +
                          " and LinkExchange.LinkExchangeMaster.id=" + LnkexcMasterId + "";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            SiteName = Convert.ToString(rdr["name"]);
                            SMTP_UserName = Convert.ToString(rdr["SMTP_UserName"]);
                            fName = Convert.ToString(rdr["fName"]);
                            lName = Convert.ToString(rdr["lName"]);
                            email = Convert.ToString(rdr["email"]);
                            Reciprocal = Convert.ToString(rdr["Reciprocal"]);
                            HTMLcode = Convert.ToString(rdr["HTMLcode"]);
                            Description = Convert.ToString(rdr["Description"]);
                            Heading = Convert.ToString(rdr["Heading"]);
                            SiteURL = Convert.ToString(rdr["SiteURL"]);
                            Signature = Convert.ToString(rdr["Signature"]);

                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
            }
        }
        return flag;
    }

    public bool get_MailDetail_SubPage(string SubPageId, string LnkexcMasterId, ref  string SiteName, ref string SMTP_UserName, ref string Signature, ref string fName, ref string lName, ref string email, ref string Reciprocal, ref string HTMLcode, ref string Description, ref string Heading, ref string SiteURL)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }


            string strSQL = " SELECT " +
                           " LinkExchange.LinkExchangeMaster.fName, " +
                           " LinkExchange.LinkExchangeMaster.lName, " +
                           " LinkExchange.LinkExchangeMaster.Reciprocal," +
                           " LinkExchange.LinkExchangeMaster.HTMLcode," +
                           " LinkExchange.LinkExchangeMaster.email," +
                           "  LinkExchange.WebSite_Master.Name, " +
                           " LinkExchange.WebSite_Master.SMTP_UserName, " +
                           " LinkExchange.WebSite_Master.Signature," +
                           " LinkExchange.LinkExchangeMaster.email, " +
                           " LinkExchange.LinkMaster.Description, " +
                           " LinkExchange.LinkMaster.Heading, " +
                           " LinkExchange.LinkMaster.SiteURL " +
                           " FROM " +
                          " LinkExchange.LinkExchangeMaster " +
                          " INNER JOIN LinkExchange.SubPage ON (LinkExchange.LinkExchangeMaster.SubPageID = LinkExchange.SubPage.Id) " +
                          " INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.SubPage.WebSiteId = LinkExchange.WebSite_Master.ID)" +
                          " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
                          " WHERE   LinkExchange.SubPage.id = '" + SubPageId + "'" +
                          " and LinkExchange.LinkExchangeMaster.id=" + LnkexcMasterId + "";

            //string strSQL = "SELECT " +
            //              " LinkExchange.LinkExchangeMaster.fName," +
            //              " LinkExchange.LinkExchangeMaster.lName," +
            //              "  LinkExchange.WebSite_Master.Name," +
            //              "  LinkExchange.WebSite_Master.SMTP_UserName," +
            //              "  LinkExchange.LinkExchangeMaster.email" +
            //              "  FROM " +
            //              "   LinkExchange.LinkExchangeMaster" +
            //              "  INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.LinkExchangeMaster.WebSiteId = LinkExchange.WebSite_Master.ID)" +
            //              " WHERE   LinkExchange.WebSite_Master.id = '" + SiteId + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            SiteName = Convert.ToString(rdr["name"]);
                            SMTP_UserName = Convert.ToString(rdr["SMTP_UserName"]);
                            fName = Convert.ToString(rdr["fName"]);
                            lName = Convert.ToString(rdr["lName"]);
                            email = Convert.ToString(rdr["email"]);
                            Reciprocal = Convert.ToString(rdr["Reciprocal"]);
                            HTMLcode = Convert.ToString(rdr["HTMLcode"]);
                            Description = Convert.ToString(rdr["Description"]);
                            Heading = Convert.ToString(rdr["Heading"]);
                            SiteURL = Convert.ToString(rdr["SiteURL"]);
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
            }
        }
        return flag;
    }


    public bool get_MailDetail_InitiateLink(string SiteId, string SubPageId, ref string SMTPServer, ref string SMTP_Port, ref string SMTP_UserName, ref string SMTP_Password, ref string Signature, ref string DisplayName, ref string Email, ref bool SSL)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "";
            if (SiteId != "")
            {
                if (Convert.ToString(HttpContext.Current.Session["Type"]) != "2")
                {

                    strSQL = " SELECT " +
                            "LinkExchange.WebSite_Master.SMTPServer , " +
                            "LinkExchange.WebSite_Master.SMTP_Port , " +
                            "LinkExchange.WebSite_Master.SMTP_UserName , " +
                            "LinkExchange.WebSite_Master.SMTP_Password , " +
                            "LinkExchange.WebSite_Master.DisplayName ," +
                            "LinkExchange.UserRegistration.Email, " +
                            " LinkExchange.WebSite_Master.Signature, " +
                           " CASE WHEN LinkExchange.WebSite_Master.SSL IS NULL THEN '0' ELSE LinkExchange.WebSite_Master.SSL END as SSl " +
                            "FROM " +
                            "LinkExchange.WebSite_Master " +
                            "INNER JOIN LinkExchange.UserRegistration ON (LinkExchange.WebSite_Master.UserId=LinkExchange.UserRegistration.UserId) " +
                            "WHERE " +
                            "LinkExchange.WebSite_Master.UserId='" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "' " +
                            "AND LinkExchange.WebSite_Master.ID='" + SiteId + "' ";
                }
                else
                {
                    strSQL = " SELECT  " +
                             " LinkExchange.WebSite_Master.SMTPServer,  " +
                             " LinkExchange.WebSite_Master.SMTP_Port,  " +
                             " LinkExchange.WebSite_Master.SMTP_UserName,  " +
                             " LinkExchange.WebSite_Master.SMTP_Password, " +
                             " LinkExchange.WebSite_Master.DisplayName,  " +
                             " LinkExchange.WebSite_Master.Signature, " +
                             " CASE WHEN LinkExchange.WebSite_Master.SSL IS NULL THEN '0' ELSE LinkExchange.WebSite_Master.SSL END as SSl, " +
                             " LinkExchange.UserRegistration.Email  " +
                             " FROM  " +
                             " LinkExchange.WebSite_Master  " +
                             " INNER JOIN LinkExchange.User_WebSite_Relation ON (LinkExchange.WebSite_Master.ID = LinkExchange.User_WebSite_Relation.WebSiteId) " +
                             " INNER JOIN LinkExchange.UserRegistration ON (LinkExchange.User_WebSite_Relation.UserId = LinkExchange.UserRegistration.UserId)  " +
                             " WHERE LinkExchange.User_WebSite_Relation.UserId='" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "' " +
                             " AND  " +
                             " LinkExchange.WebSite_Master.ID='" + SiteId + "' " +
                             " and LinkExchange.UserRegistration.type=2  ";
                }
            }
            else
            {

                if (Convert.ToString(HttpContext.Current.Session["Type"]) != "2")
                {
                    strSQL = "SELECT " +
                                "LinkExchange.WebSite_Master.SMTPServer , " +
                                "LinkExchange.WebSite_Master.SMTP_Port , " +
                                "LinkExchange.WebSite_Master.SMTP_UserName , " +
                                "LinkExchange.WebSite_Master.SMTP_Password , " +
                                "LinkExchange.WebSite_Master.DisplayName ," +
                                "LinkExchange.UserRegistration.Email, " +
                                " LinkExchange.WebSite_Master.Signature, " +
                               " CASE WHEN LinkExchange.WebSite_Master.SSL IS NULL THEN '0' ELSE LinkExchange.WebSite_Master.SSL END as SSl " +
                                "FROM " +
                                "LinkExchange.WebSite_Master " +
                                "INNER JOIN LinkExchange.UserRegistration ON (LinkExchange.WebSite_Master.UserId=LinkExchange.UserRegistration.UserId) " +
                                "WHERE " +
                                "LinkExchange.WebSite_Master.UserId='" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "' " +
                                "AND LinkExchange.WebSite_Master.ID=(select LinkExchange.SubPage.WebSiteId from LinkExchange.SubPage where LinkExchange.SubPage.Id='" + SubPageId + "')";
                }
                else
                {
                    strSQL = " SELECT LinkExchange.WebSite_Master.SMTPServer ," +
                            " LinkExchange.WebSite_Master.SMTP_Port , " +
                            " LinkExchange.WebSite_Master.SMTP_UserName ," +
                            "  LinkExchange.WebSite_Master.SMTP_Password , " +
                            " LinkExchange.WebSite_Master.DisplayName ," +
                            " LinkExchange.UserRegistration.Email, " +
                            "  LinkExchange.WebSite_Master.Signature, " +
                            "  CASE WHEN LinkExchange.WebSite_Master.SSL IS NULL THEN '0' ELSE LinkExchange.WebSite_Master.SSL END as SSl FROM " +
                            " LinkExchange.WebSite_Master " +
                            " INNER JOIN LinkExchange.User_WebSite_Relation ON (LinkExchange.WebSite_Master.ID = LinkExchange.User_WebSite_Relation.WebSiteId)" +
                            " INNER JOIN LinkExchange.UserRegistration ON (LinkExchange.User_WebSite_Relation.UserId = LinkExchange.UserRegistration.UserId)" +
                            " WHERE LinkExchange.User_WebSite_Relation.UserId='" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "' " +
                            " AND LinkExchange.WebSite_Master.ID=" +
                            " (select LinkExchange.SubPage.WebSiteId from LinkExchange.SubPage where LinkExchange.SubPage.Id='" + SubPageId + "')";

                    //strSQL = " SELECT " +
                    //         "  LinkExchange.WebSite_Master.SMTP_Port," +
                    //         " LinkExchange.WebSite_Master.SMTPServer," +
                    //         "  LinkExchange.WebSite_Master.SMTP_UserName," +
                    //         " LinkExchange.WebSite_Master.SMTP_Password," +
                    //         " LinkExchange.WebSite_Master.DisplayName," +
                    //         " LinkExchange.WebSite_Master.Signature," +
                    //         " CASE WHEN LinkExchange.WebSite_Master.SSL IS NULL THEN '0' ELSE LinkExchange.WebSite_Master.SSL END as SSl," +
                    //         " LinkExchange.UserRegistration.Email " +
                    //         " FROM " +
                    //         " LinkExchange.WebSite_Master " +
                    //         " INNER JOIN LinkExchange.UserRegistration ON (LinkExchange.WebSite_Master.ID = LinkExchange.UserRegistration.UserId)" +
                    //         " INNER JOIN LinkExchange.SubPage ON (LinkExchange.WebSite_Master.ID = LinkExchange.SubPage.WebSiteId)" +
                    //         " WHERE  " +
                    //         " LinkExchange.WebSite_Master.UserId='" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "' " +
                    //         " and LinkExchange.SubPage.Id='" + SubPageId + "' ";
                }
            }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            SMTPServer = Convert.ToString(rdr["SMTPServer"]);
                            SMTP_Port = Convert.ToString(rdr["SMTP_Port"]);
                            SMTP_UserName = Convert.ToString(rdr["SMTP_UserName"]);
                            SMTP_Password = Convert.ToString(rdr["SMTP_Password"]);
                            DisplayName = Convert.ToString(rdr["DisplayName"]);
                            Email = Convert.ToString(rdr["Email"]);
                            Signature = Convert.ToString(rdr["Signature"]);
                            SSL = Convert.ToBoolean(rdr["SSL"]);
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
            }
        }
        return flag;
    }

    public bool get_MailDetail_User(string SiteId, ref string SMTPServer, ref string SMTP_Port, ref string SMTP_UserName, ref string SMTP_Password, ref string Signature, ref string DisplayName, ref string Email, ref bool SSL)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }


            string strSQL = "SELECT " +
                    "LinkExchange.UserRegistration.SMTPServer , " +
                    "LinkExchange.UserRegistration.SMTP_Port , " +
                    "LinkExchange.UserRegistration.SMTP_UserName , " +
                    "LinkExchange.UserRegistration.SMTP_Password , " +
                    "LinkExchange.UserRegistration.UserName ,LinkExchange.UserRegistration.LastName ," +
                //   "LinkExchange.UserRegistration.UserName  as DisplayName," +
                  " CASE WHEN LinkExchange.UserRegistration.SSL IS NULL THEN '0' ELSE LinkExchange.UserRegistration.SSL END as SSl, " +
                    "LinkExchange.UserRegistration.Email " +
                    "FROM " +
                    "LinkExchange.UserRegistration " +
                    "WHERE " +
                    "LinkExchange.UserRegistration.UserId='" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "' ";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            SMTPServer = Convert.ToString(rdr["SMTPServer"]);
                            SMTP_Port = Convert.ToString(rdr["SMTP_Port"]);
                            SMTP_UserName = Convert.ToString(rdr["SMTP_UserName"]);
                            SMTP_Password = Convert.ToString(rdr["SMTP_Password"]);
                            DisplayName = Convert.ToString(rdr["UserName"]) + " " + Convert.ToString(rdr["LastName"]);
                            Email = Convert.ToString(rdr["Email"]);
                            SSL = Convert.ToBoolean(rdr["SSL"]);
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
            }
        }
        return flag;
    }




    public bool SendMail_InitiateLink(string mailTo, string mailFrom, string mailFromDisplayName, string mailCC, string mailBody, string mailSubject, string SMTPServer, string SMTP_UserName, string SMTP_Port, string SMTP_Password, ref string strError)
    {
        bool flag = true;
        MailMessage objMail = new MailMessage();
        objMail.To.Add(mailTo);
        //objMail.To.Add("ravi@giftstoindia24x7.com");
        objMail.Bcc.Add("adesai12@gmail.com");
        // objMail.Bcc.Add("suvrojyoti.kundu@reliablewebtechnologies.com");
        objMail.From = new MailAddress(mailFrom, mailFromDisplayName);
        //objMail.CC.Add(mailCC);
        objMail.Subject = mailSubject;
        objMail.Body = mailBody;
        objMail.IsBodyHtml = true;
        objMail.Priority = MailPriority.High;
        //objMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        try
        {
            SmtpClient client = new SmtpClient(SMTPServer);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(SMTP_UserName, SMTP_Password);
            client.Port = Convert.ToInt32(SMTP_Port);
            client.Send(objMail);
            flag = true;

        }
        catch (SmtpException ex)
        {
            flag = false;
            strError = ex.Message;
        }
        return flag;
    }


    public EmailTemplate get_Reject_EmailTemplateData()
    {

        string strSQL = "  SELECT  LinkExchange.EmailTemplate.TemplateID,LinkExchange.EmailTemplate.TemplateName," +
                        " LinkExchange.EmailTemplate.TemplateSubject, " +
                        " LinkExchange.EmailTemplate.TemplateCode " +
                         " FROM LinkExchange.EmailTemplate " +
                        " WHERE   LinkExchange.EmailTemplate.TemplateID = 3";
        EmailTemplate objEmailTemplate = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objEmailTemplate = new EmailTemplate();
                    objEmailTemplate.TemplateID = Convert.ToInt32(dt.Rows[i]["TemplateID"]);
                    objEmailTemplate.TemplateName = Convert.ToString(dt.Rows[i]["TemplateName"]);
                    objEmailTemplate.TemplateSubject = Convert.ToString(dt.Rows[i]["TemplateSubject"]);
                    objEmailTemplate.TemplateCode = Convert.ToString(dt.Rows[i]["TemplateCode"]);
                }
            }
        }
        return objEmailTemplate;
    }


    public bool UpdateLinkExchangeMaster_Pageid(int id, int pageid, ref string strError)
    {
        string strSQL = "update LinkExchange.LinkExchangeMaster set LinkExchange.LinkExchangeMaster.pageid=" + pageid + "  where LinkExchange.LinkExchangeMaster.id=" + id + "";
        //Execute(strSQL, strError);
        bool flag = false;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
                conn.Close();
                flag = false;
            }
            finally
            {
                //dr.Close();
                conn.Close();
            }
        }
        return flag;

    }

    public bool checkDuplicateSubPage(string SubPageName, string WebSiteId)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "select SubPageName from LinkExchange.Subpage where SubPageName='" + SubPageName + "' and WebSiteId=" + WebSiteId + " and status=1";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        if (rdr.Read())
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }
    public bool checkDuplicateUser(string email)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "select Email from LinkExchange.UserRegistration where LinkExchange.UserRegistration.Email='" + email + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        if (rdr.Read())
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }


    public void AddUser_WebSite_Relation(string uid, string WebSiteIds, string mode, ref string strError)
    {

        strError = null;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spAddEdit_User_WebSite_Relation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = uid;
                    cmd.Parameters.Add("@ids", SqlDbType.VarChar, 250).Value = WebSiteIds;
                    cmd.Parameters.Add("@mode", SqlDbType.VarChar, 250).Value = mode;

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public GCommon<UserRegistration> GetUserCollection(int Type)
    {
        GCommon<UserRegistration> coll = new GCommon<UserRegistration>();
        UserRegistration objreg = null;
        string strSQL = "SELECT UserId,Email FROM LinkExchange.UserRegistration where type=" + Type + " ";

        DataTable dt = FillDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objreg = new UserRegistration();

                objreg.Id = Convert.ToString(dt.Rows[i]["UserId"]);
                objreg.Email = Convert.ToString(dt.Rows[i]["Email"]);


                coll.Add(objreg);
            }
        }
        return coll;
    }

    public bool get_UserName(string uid, ref  string UserName)
    {
        bool flag = false;

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strSQL = "  SELECT  LinkExchange.UserRegistration.Email" +
                      " FROM LinkExchange.UserRegistration " +
                      " WHERE   LinkExchange.UserRegistration.UserId = '" + uid + "'";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            UserName = Convert.ToString(rdr["email"]);

                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public bool SendMail_SSL(string mailTo, string mailFrom, string mailFromDisplayName, string mailCC, string mailBody, string mailSubject, ref string strError)
    {
        bool flag = true;
        try
        {
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailFrom, mailFromDisplayName);
            mailMessage.To.Add(mailTo);
            mailMessage.Subject = mailSubject;
            mailMessage.Body = mailBody;
            mailMessage.IsBodyHtml = true;
            // Create the credentials to login to the gmail account associated with my custom domain
            // string sendEmailsFrom = "noreply@reliablelinkexchange.com";
            string sendEmailsFrom = settings.Smtp.Network.UserName;
            //string sendEmailsFromPassword = "reply8521";
            string sendEmailsFromPassword = settings.Smtp.Network.Password;
            NetworkCredential cred = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);
            // SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            //SmtpClient mailClient = new SmtpClient("smtp.gmail.com");
            // SmtpClient mailClient = new SmtpClient(settings.Smtp.Network.Host,settings.Smtp.Network.Port);
            SmtpClient mailClient = new SmtpClient(settings.Smtp.Network.Host);
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Timeout = 20000;
            mailClient.Credentials = cred;
            mailClient.Send(mailMessage);
            flag = true;
        }
        catch (Exception ex)
        {
            flag = false;
            strError = ex.Message;
        }
        return flag;
    }

    public bool SendMail_SSLChecking(string mailTo, string mailFrom, string mailFromDisplayName, string SMTP_UserName, string SMTP_Password, string SMTP_Port, string SMTPServer, string mailBody, string mailSubject, ref string strError)
    {
        bool flag = true;
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailFrom, mailFromDisplayName);
            mailMessage.To.Add(mailTo);
            mailMessage.Bcc.Add("adesai12@gmail.com");
            mailMessage.Subject = mailSubject;
            mailMessage.Body = mailBody;
            mailMessage.IsBodyHtml = true;
            // Create the credentials to login to the gmail account associated with my custom domain
            // string sendEmailsFrom = "noreply@reliablelinkexchange.com";

            //string sendEmailsFromPassword = "reply8521";

            NetworkCredential cred = new NetworkCredential(SMTP_UserName, SMTP_Password);
            // SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            //SmtpClient mailClient = new SmtpClient("smtp.gmail.com");
            SmtpClient mailClient = new SmtpClient(SMTPServer, Convert.ToInt32(SMTP_Port));
            //SmtpClient mailClient = new SmtpClient(SMTPServer);
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Timeout = 20000;
            mailClient.Credentials = cred;
            mailClient.Send(mailMessage);
            flag = true;
        }
        catch (Exception ex)
        {
            flag = false;
            strError = ex.Message;
        }
        return flag;
    }

    public UserRegistration fetch_UserDetails(string UserId)
    {

        string strSQL = " SELECT  LinkExchange.UserRegistration.Email,LinkExchange.UserRegistration.UserName," +
                         " LinkExchange.UserRegistration.LastName, " +
                         " LinkExchange.UserRegistration.Password, " +
                        " LinkExchange.UserRegistration.SMTPServer, " +
                        " LinkExchange.UserRegistration.SMTP_Port, " +
                        " LinkExchange.UserRegistration.SMTP_UserName, " +
                        " LinkExchange.UserRegistration.SMTP_Password, " +
                        " CASE WHEN LinkExchange.UserRegistration.SSL IS NULL THEN '0' ELSE LinkExchange.UserRegistration.SSL END as SSl " +
                        " FROM LinkExchange.UserRegistration " +
                        " WHERE  LinkExchange.UserRegistration.UserId = " + UserId + "";
        UserRegistration objreg = null;
        using (DataTable dt = FillDataTable(strSQL))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objreg = new UserRegistration();
                    objreg.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    objreg.UserName = Convert.ToString(dt.Rows[i]["UserName"]);
                    objreg.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    objreg.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    objreg.SMTPServer = Convert.ToString(dt.Rows[i]["SMTPServer"]);
                    objreg.SMTP_Port = Convert.ToString(dt.Rows[i]["SMTP_Port"]);
                    objreg.SMTP_UserName = Convert.ToString(dt.Rows[i]["SMTP_UserName"]);
                    objreg.SMTP_Password = Convert.ToString(dt.Rows[i]["SMTP_Password"]);
                    objreg.SSL = Convert.ToBoolean(dt.Rows[i]["SSL"]);
                }
            }
        }

        return objreg;
    }

    public bool UpdateUser(UserRegistration objreg, string UserId, ref string strError, string type)
    {
        string strSQL = "";
        switch (type)
        {
            case "UserDetails":
                strSQL = "update LinkExchange.UserRegistration set LinkExchange.UserRegistration.UserName='" + objreg.UserName + "',LinkExchange.UserRegistration.LastName='" + objreg.LastName + "'  where LinkExchange.UserRegistration.UserId=" + UserId + "";
                break;
            case "PwdDetails":
                strSQL = "update LinkExchange.UserRegistration set LinkExchange.UserRegistration.Password='" + objreg.Password + "'  where LinkExchange.UserRegistration.UserId=" + UserId + "";
                break;
            case "SMTPDetails":
                strSQL = "update LinkExchange.UserRegistration set LinkExchange.UserRegistration.SMTPServer='" + objreg.SMTPServer + "',LinkExchange.UserRegistration.SMTP_Port='" + objreg.SMTP_Port + "' ,LinkExchange.UserRegistration.SMTP_Password='" + objreg.SMTP_Password + "' ,LinkExchange.UserRegistration.SMTP_UserName='" + objreg.SMTP_UserName + "',LinkExchange.UserRegistration.SSL='" + objreg.SSL + "'  where LinkExchange.UserRegistration.UserId=" + UserId + "";
                break;
        }

        bool flag = false;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
                conn.Close();
                flag = false;
            }
            finally
            {
                //dr.Close();
                conn.Close();
            }
        }
        return flag;

    }

    public bool getPassword(string UserId, ref string Password)
    {
        bool flag = false;
        string strSQL = "select LinkExchange.UserRegistration.Password from LinkExchange.UserRegistration where LinkExchange.UserRegistration.UserId='" + UserId + "' ";

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            Password = Convert.ToString(rdr["password"]);
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public bool get_MailCount(int LinkId, ref string countmail, ref string mailtime)
    {
        bool flag = false;
        //string strSQL = " select CASE WHEN convert(varchar(250),count(LinkExchange.EmailCorrespondenceDetails.id)) = 0 THEN 'No' "+
        //              " ELSE "+
        //              " convert(varchar(250),count(LinkExchange.EmailCorrespondenceDetails.id))"+
        //              " END as countmail "+
        //              " from LinkExchange.EmailCorrespondenceDetails "+
        //              " where LinkExchange.EmailCorrespondenceDetails.LinkId='" + LinkId + "' ";
        string strSQL = " select countmail,mailtime " +
                       " from LinkExchange.fnMailDetails('" + LinkId + "') ";

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            countmail = Convert.ToString(rdr["countmail"]);
                            mailtime = Convert.ToString(rdr["mailtime"]);
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public string MailCount(int LinkId, string SlNo)
    {
        string mailcount = "";
        string mailtime = "";
        StringBuilder sbMail = new StringBuilder();
        if (get_MailCount(LinkId, ref mailcount, ref mailtime))
        {
            if (mailtime != "")
            {
                sbMail.Append("<a href=\"javascript:funOpenMailContentDiv(" + LinkId + "," + SlNo + ");\">" + mailcount + " Mails send against this Exchange. Last email sent " + Convert.ToDateTime(mailtime).ToString("MMMM dd, yyyy h:mm tt") + "</a>");
            }
            else
            {
                sbMail.Append(mailcount + " Mails send against this Exchange.");
            }
        }
        return sbMail.ToString();
    }

    public bool FetchMailContenttData(string LinkId, Hashtable htMail, string SiteId, string SubPageId, string Status)
    {
        bool flag = false;
        string strSQL = "";
        if (SiteId != "")
        {
            strSQL = " SELECT " +
                   " ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName) AS 'SlNo'," +
                   " LinkExchange.LinkExchangeMaster.Id," +
                   "  LinkExchange.SubPage.SubPageName," +
                //  " LinkExchange.LinkExchangeMaster.HTMLcode," +
                   " CASE WHEN LEN(LinkExchange.LinkExchangeMaster.HTMLcode) > 170 " +
                    " THEN " +
                    " REPLACE(SUBSTRING(LinkExchange.LinkExchangeMaster.HTMLcode, 0, 170),'~','''')+'..' " +
                   " ELSE " +
                  "  LinkExchange.LinkExchangeMaster.HTMLcode END as HTMLcode, " +
                   " CASE WHEN LinkExchange.LinkExchangeMaster.Reciprocal IS NULL THEN '' ELSE " +
                   " '<a href=\"'+LinkExchange.LinkExchangeMaster.Reciprocal + '\" target=\"_blank\">Our Link</a>' END as Reciprocal ," +
                  " '<a href=\"'+LinkExchange.LinkMaster.SiteURL + '\" target=\"_blank\">'+LinkExchange.LinkMaster.Heading +'</a>'+ " +
                  " CASE WHEN LEN(LinkExchange.LinkMaster.Description) > 170 " +
                  "   THEN" +
                   " REPLACE(SUBSTRING(LinkExchange.LinkMaster.Description, 0, 170),'~','''')+'..' " +
                  " ELSE  " +
                  " LinkExchange.LinkMaster.Description END as ouradd " +
                //     " ,LinkExchange.LinkMaster.Description, " +
                //    " LinkExchange.LinkMaster.Heading, " +
                //   " LinkExchange.LinkMaster.SiteURL " +
                   " FROM " +
                   " LinkExchange.SubPage " +
                   "  INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID)" +
                   " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
                   " WHERE " +
                   "  (LinkExchange.LinkExchangeMaster.WebSiteId = " + SiteId + ") AND " +
                   "(LinkExchange.LinkExchangeMaster.Status = " + Status + ") and (LinkExchange.LinkExchangeMaster.Id = " + LinkId + ")";
        }
        else
        {
            //strSQL = " SELECT  " +
            //        " ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName) AS 'SlNo'," +
            //            "  LinkExchange.SubPage.SubPageName," +
            //            " LinkExchange.LinkExchangeMaster.HTMLcode," +
            //            "  LinkExchange.LinkExchangeMaster.Reciprocal," +
            //          " LinkExchange.LinkExchangeMaster.Id," +
            //         " LinkExchange.LinkMaster.Description, " +
            //           " LinkExchange.LinkMaster.Heading, " +
            //           " LinkExchange.LinkMaster.SiteURL " +
            //         " FROM " +
            //         " LinkExchange.SubPage " +
            //         "  INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID)" +
            //         " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
            //         " WHERE " +
            //         "  (LinkExchange.SubPage.Id = " + SubPageId + ") AND " +
            //         "(LinkExchange.LinkExchangeMaster.Status = " + Status + ")";

            strSQL = " SELECT " +
                   " ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName) AS 'SlNo'," +
                   " LinkExchange.LinkExchangeMaster.Id," +
                   "  LinkExchange.SubPage.SubPageName," +
                //  " LinkExchange.LinkExchangeMaster.HTMLcode," +
                   " CASE WHEN LEN(LinkExchange.LinkExchangeMaster.HTMLcode) > 170 " +
                    " THEN " +
                    " REPLACE(SUBSTRING(LinkExchange.LinkExchangeMaster.HTMLcode, 0, 170),'~','''')+'..' " +
                   " ELSE " +
                  "  LinkExchange.LinkExchangeMaster.HTMLcode END as HTMLcode, " +
                   " CASE WHEN LinkExchange.LinkExchangeMaster.Reciprocal IS NULL THEN '' ELSE " +
                   " '<a href=\"'+LinkExchange.LinkExchangeMaster.Reciprocal + '\" target=\"_blank\">Our Link</a>' END as Reciprocal ," +
                  " '<a href=\"'+LinkExchange.LinkMaster.SiteURL + '\" target=\"_blank\">'+LinkExchange.LinkMaster.Heading +'</a>'+ " +
                  " CASE WHEN LEN(LinkExchange.LinkMaster.Description) > 170 " +
                  "   THEN" +
                   " REPLACE(SUBSTRING(LinkExchange.LinkMaster.Description, 0, 170),'~','''')+'..' " +
                  " ELSE  " +
                  " LinkExchange.LinkMaster.Description END as ouradd " +
                //     " ,LinkExchange.LinkMaster.Description, " +
                //    " LinkExchange.LinkMaster.Heading, " +
                //   " LinkExchange.LinkMaster.SiteURL " +
                   " FROM " +
                   " LinkExchange.SubPage " +
                   "  INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID)" +
                   " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
                   " WHERE " +
                    "  (LinkExchange.SubPage.Id = " + SubPageId + ") AND " +
                     "(LinkExchange.LinkExchangeMaster.Status = " + Status + ")" +
                   " and (LinkExchange.LinkExchangeMaster.Id = " + LinkId + ")";
        }

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            htMail.Add("SubPageName", Convert.ToString(rdr["SubPageName"]));
                            htMail.Add("HTMLcode", Convert.ToString(rdr["HTMLcode"]));
                            htMail.Add("Reciprocal", Convert.ToString(rdr["Reciprocal"]));
                            htMail.Add("ouradd", Convert.ToString(rdr["ouradd"]));
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public DataTable FetchSendMailDetails(string LinkId, DataTable dtSendMails)
    {
        string strError = "";
        //string strSQL = "  SELECT " +
        //                  " LinkExchange.EmailTemplate.TemplateName,"+
        //                  " CONVERT(VARCHAR(10),LinkExchange.EmailCorrespondenceDetails.DateTime, 7) AS maildate,"+
        //                  "  DATEDIFF(day, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()) AS datediff, "+
        //                  " LinkExchange.EmailCorrespondenceDetails.DisplayName, " +
        //                   "  LinkExchange.EmailCorrespondenceDetails.FromEmail, " +
        //                   "  LinkExchange.EmailCorrespondenceDetails.ToEmail, " +
        //                   "  LinkExchange.EmailCorrespondenceDetails.Body, " +
        //                   " LinkExchange.EmailCorrespondenceDetails.id" +
        //                  " FROM" +
        //                  " LinkExchange.EmailTemplate "+
        //                  " INNER JOIN LinkExchange.EmailCorrespondenceDetails ON (LinkExchange.EmailTemplate.TemplateID = LinkExchange.EmailCorrespondenceDetails.TemplateId)"+
        //                  " where LinkExchange.EmailCorrespondenceDetails.LinkId='"+LinkId+"'";

        //string strSQL = " SELECT " +
        //                " LinkExchange.EmailTemplate.TemplateName," +
        //    // " CONVERT(VARCHAR(10),LinkExchange.EmailCorrespondenceDetails.DateTime, 7) AS maildate," +
        //                " CASE WHEN CONVERT(VARCHAR(10),LinkExchange.EmailCorrespondenceDetails.DateTime, 7) =  CONVERT(VARCHAR(10),getdate(), 7) " +
        //                " THEN " +
        //    //" SUBSTRING(CONVERT(VARCHAR,dateadd(minute,(28),dateadd(hour,(10),LinkExchange.EmailCorrespondenceDetails.DateTime)),100), 13, 20) " +
        //                " SUBSTRING(CONVERT(VARCHAR,EmailCorrespondenceDetails.DateTime,100), 13, 20) " +
        //                " else " +
        //                " CONVERT(VARCHAR(10),LinkExchange.EmailCorrespondenceDetails.DateTime, 7) end AS maildate, " +
        //                " CASE WHEN DATEDIFF(day, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()) = 0 " +
        //                " THEN " +
        //                " case when DATEDIFF(hour, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()) =0 " +
        //               " then " +
        //               " case when DATEDIFF(minute, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()) =0 " +
        //                " then " +
        //               " CONVERT(VARCHAR(100),DATEDIFF(second, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()))+'seconds' " +
        //               " else " +
        //               " CONVERT(VARCHAR(100),DATEDIFF(minute, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()))+' minutes' " +
        //                " end " +
        //               " else " +
        //               " CONVERT(VARCHAR(100),DATEDIFF(hour, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()))+' hours' " +
        //               " end " +
        //               " ELSE " +
        //               " CONVERT(VARCHAR(100),DATEDIFF(day, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()))+ ' days' " +
        //               " END as datediff, " +
        //    //  " DATEDIFF(day, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()) AS diff, " +
        //               " LinkExchange.EmailCorrespondenceDetails.DisplayName,  " +
        //               " LinkExchange.EmailCorrespondenceDetails.FromEmail,  " +
        //               "  LinkExchange.EmailCorrespondenceDetails.ToEmail,  " +
        //               " LinkExchange.EmailCorrespondenceDetails.Body,  " +
        //               " LinkExchange.EmailCorrespondenceDetails.id " +
        //               " FROM " +
        //               " LinkExchange.EmailTemplate " +
        //               " INNER JOIN LinkExchange.EmailCorrespondenceDetails ON (LinkExchange.EmailTemplate.TemplateID = LinkExchange.EmailCorrespondenceDetails.TemplateId)" +
        //               " where LinkExchange.EmailCorrespondenceDetails.LinkId='" + LinkId + "'" +
        //               " order by LinkExchange.EmailCorrespondenceDetails.DateTime ;";
        string strSQL = " SELECT " +
                    " LinkExchange.EmailTemplate.TemplateName," +
            // " CONVERT(VARCHAR(10),LinkExchange.EmailCorrespondenceDetails.DateTime, 7) AS maildate," +
                    " CASE WHEN CONVERT(VARCHAR(10),LinkExchange.EmailCorrespondenceDetails.DateTime, 7) =  CONVERT(VARCHAR(10),dateadd(minute,(30),dateadd(hour,(5),getutcdate())), 7) " +
                    " THEN " +
            //" SUBSTRING(CONVERT(VARCHAR,dateadd(minute,(28),dateadd(hour,(10),LinkExchange.EmailCorrespondenceDetails.DateTime)),100), 13, 20) " +
                    " SUBSTRING(CONVERT(VARCHAR,EmailCorrespondenceDetails.DateTime,100), 13, 20) " +
                    " else " +
                    " CONVERT(VARCHAR(10),LinkExchange.EmailCorrespondenceDetails.DateTime, 7) end AS maildate, " +
                    " CASE WHEN DATEDIFF(day, LinkExchange.EmailCorrespondenceDetails.DateTime, dateadd(minute,(30),dateadd(hour,(5),getutcdate()))) = 0 " +
                    " THEN " +
                    " case when DATEDIFF(hour, LinkExchange.EmailCorrespondenceDetails.DateTime, dateadd(minute,(30),dateadd(hour,(5),getutcdate()))) =0 " +
                   " then " +
                   " case when DATEDIFF(minute, LinkExchange.EmailCorrespondenceDetails.DateTime, dateadd(minute,(30),dateadd(hour,(5),getutcdate()))) =0 " +
                    " then " +
                   " CONVERT(VARCHAR(100),DATEDIFF(second, LinkExchange.EmailCorrespondenceDetails.DateTime, dateadd(minute,(30),dateadd(hour,(5),getutcdate())))) + ' seconds' " +
                   " else " +
                   " CONVERT(VARCHAR(100),DATEDIFF(minute, LinkExchange.EmailCorrespondenceDetails.DateTime, dateadd(minute,(30),dateadd(hour,(5),getutcdate()))))+ ' minutes' " +
                    " end " +
                   " else " +
                   " CONVERT(VARCHAR(100),DATEDIFF(hour, LinkExchange.EmailCorrespondenceDetails.DateTime, dateadd(minute,(30),dateadd(hour,(5),getutcdate()))))+ ' hours' " +
                   " end " +
                   " ELSE " +
                   " CONVERT(VARCHAR(100),DATEDIFF(day, LinkExchange.EmailCorrespondenceDetails.DateTime, dateadd(minute,(30),dateadd(hour,(5),getutcdate()))))+ ' days' " +
                   " END as datediff, " +
            //  " DATEDIFF(day, LinkExchange.EmailCorrespondenceDetails.DateTime, GETDATE()) AS diff, " +
                   " LinkExchange.EmailCorrespondenceDetails.DisplayName,  " +
                   " LinkExchange.EmailCorrespondenceDetails.FromEmail,  " +
                   "  LinkExchange.EmailCorrespondenceDetails.ToEmail,  " +
                   " LinkExchange.EmailCorrespondenceDetails.Body,  " +
                   " LinkExchange.EmailCorrespondenceDetails.id " +
                   " FROM " +
                   " LinkExchange.EmailTemplate " +
                   " INNER JOIN LinkExchange.EmailCorrespondenceDetails ON (LinkExchange.EmailTemplate.TemplateID = LinkExchange.EmailCorrespondenceDetails.TemplateId)" +
                   " where LinkExchange.EmailCorrespondenceDetails.LinkId='" + LinkId + "'" +
                   " order by LinkExchange.EmailCorrespondenceDetails.DateTime ;";




        dtSendMails = FillDataTable(strSQL, ref strError);
        return dtSendMails;
    }


    //public DataTable Fetch_EmailCorrespondenceDetails(string id, DataTable dtSendMails)
    //{
    //    string strError = "";
    //    string strSQL = "  SELECT " +
    //                      " LinkExchange.EmailCorrespondenceDetails.DisplayName, " +
    //                       "  LinkExchange.EmailCorrespondenceDetails.FromEmail, " +
    //                       "  LinkExchange.EmailCorrespondenceDetails.ToEmail, " +
    //                       "  LinkExchange.EmailCorrespondenceDetails.Body, " +
    //                       " LinkExchange.EmailCorrespondenceDetails.id, " +
    //                      " FROM" +
    //                      " LinkExchange.EmailCorrespondenceDetails " +
    //                      " where LinkExchange.EmailCorrespondenceDetails.id='" + id + "'";
    //    dtSendMails = FillDataTable(strSQL, ref strError);
    //    return dtSendMails;
    //}
    //public GCommon<EmailCorrespondence> Fetch_EmailCorrespondenceDetails(string id)
    //{
    //    GCommon<EmailCorrespondence> coll = new GCommon<EmailCorrespondence>();
    //    EmailCorrespondence objEmail = null;

    //    string strSQL = "  SELECT " +
    //                          " LinkExchange.EmailCorrespondenceDetails.DisplayName, " +
    //                           "  LinkExchange.EmailCorrespondenceDetails.FromEmail, " +
    //                           "  LinkExchange.EmailCorrespondenceDetails.ToEmail, " +
    //                           "  LinkExchange.EmailCorrespondenceDetails.Body " +
    //                          " FROM" +
    //                          " LinkExchange.EmailCorrespondenceDetails " +
    //                          " where LinkExchange.EmailCorrespondenceDetails.id='" + id + "'";
    //    DataTable dt = FillDataTable(strSQL);
    //    if (dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            objEmail = new EmailCorrespondence();

    //            objEmail.Id = Convert.ToInt32(dt.Rows[i]["id"]);
    //            objEmail.DisplayName = Convert.ToString(dt.Rows[i]["DisplayName"]);
    //            objEmail.FromEmail = Convert.ToString(dt.Rows[i]["FromEmail"]);
    //            objEmail.ToEmail = Convert.ToString(dt.Rows[i]["ToEmail"]);
    //            objEmail.Body = Convert.ToString(dt.Rows[i]["Body"]);


    //            coll.Add(objEmail);
    //        }
    //    }
    //    return coll;
    //}


    public bool Fetch_EmailCorrespondenceDetails(string id, Hashtable htMail)
    {
        bool flag = false;
        string strSQL = "  SELECT " +
                               " LinkExchange.EmailCorrespondenceDetails.DisplayName, " +
                                "  LinkExchange.EmailCorrespondenceDetails.FromEmail, " +
                                "  LinkExchange.EmailCorrespondenceDetails.ToEmail, " +
                                "  LinkExchange.EmailCorrespondenceDetails.Body " +
                               " FROM" +
                               " LinkExchange.EmailCorrespondenceDetails " +
                               " where LinkExchange.EmailCorrespondenceDetails.id='" + id + "'";

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSQL, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {

                            htMail.Add("DisplayName", Convert.ToString(rdr["DisplayName"]));
                            htMail.Add("FromEmail", Convert.ToString(rdr["FromEmail"]));
                            htMail.Add("ToEmail", Convert.ToString(rdr["ToEmail"]));
                            htMail.Add("Body", Convert.ToString(rdr["Body"]));
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }
        return flag;
    }

    public string AddRequestLinkExchange(LinkExchange objLinkExchange, ref string strStatus, string SiteURL, ref string strError)
    {
        string returnStatus = "";
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (SqlCommand cmd = new SqlCommand(_schema + ".spAddRequestLinkExchange", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OurAdId", SqlDbType.VarChar).Value = objLinkExchange.OurAdId;
                    cmd.Parameters.Add("@SubPageId", SqlDbType.VarChar).Value = objLinkExchange.SubPageId;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = objLinkExchange.Status;
                    cmd.Parameters.Add("@Reciprocal", SqlDbType.VarChar).Value = objLinkExchange.Reciprocal;
                    cmd.Parameters.Add("@HTMLcode", SqlDbType.VarChar).Value = objLinkExchange.HTMLcode;
                    cmd.Parameters.Add("@PageRank", SqlDbType.VarChar).Value = objLinkExchange.PageRank;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = objLinkExchange.Type;
                    cmd.Parameters.Add("@WebSiteId", SqlDbType.VarChar).Value = objLinkExchange.WebSiteId;
                    cmd.Parameters.Add("@From_url", SqlDbType.VarChar).Value = objLinkExchange.From_url;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = objLinkExchange.Email;
                    cmd.Parameters.Add("@fName", SqlDbType.VarChar).Value = objLinkExchange.FName;
                    cmd.Parameters.Add("@lName", SqlDbType.VarChar).Value = objLinkExchange.LName;
                    cmd.Parameters.Add("@SiteURL", SqlDbType.VarChar).Value = SiteURL;

                    //cmd.Parameters.Add("@mode", SqlDbType.VarChar).Value = "HTMLCode";
                    cmd.Parameters.Add("@returnvalue", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnvalue"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@returnstatus", SqlDbType.VarChar, 250);
                    cmd.Parameters["@returnstatus"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "0")
                    {
                        returnStatus = "0";
                        strError = "Some error in procedure";
                    }
                    else if (Convert.ToString(cmd.Parameters["@returnvalue"].Value) == "f")
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                        strError = "Link exchange exists against this URL. Duplicate entry not permitted.";
                        strStatus = Convert.ToString(cmd.Parameters["@returnstatus"].Value);
                        strStatus = Convert.ToString(cmd.ExecuteScalar());

                    }
                    else
                    {
                        returnStatus = Convert.ToString(cmd.Parameters["@returnvalue"].Value);
                        strError = "Link Exchange request initiated.Please continue to add more links";

                    }

                }
                return returnStatus;
            }
            catch (Exception ex)
            {
                strError = "Error!<br>" + ex.Message;
            }
            finally
            {
                conn.Close();
            }



            return "";
        }
    }

    public string makeMailBody_ContactUs(string strUserName, string strLastName, string email, string query)
    {
        string retValue = string.Empty;
        retValue = "<table cellpadding='0' cellspacing='0' width='100%' border='0' style=\"font-size: 10pt; font-family: Verdana\">";



        retValue += "<tr>";
        if (strLastName != "")
        {
            retValue += "<td colspan='2'>Name:" + strUserName + "  " + strLastName + "</td>";
        }
        else
        {
            retValue += "<td colspan='2'>Name -" + strUserName + "</td>";

        }
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td colspan='2'>Email:" + email + ",</td>";
        retValue += "</tr>";
        retValue += "<tr>";


        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";

        retValue += "<tr>";
        retValue += "<td colspan='2'>Query:" + query + "</td>";
        retValue += "</tr>";
        retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";



        //retValue += "<tr>";
        //retValue += "<td colspan='2'>" + password + "</td>";
        //retValue += "</tr>";
        //retValue += "<tr>";
        retValue += "<td width='15%'>&nbsp;</td>";
        retValue += "<td width='85%'>&nbsp;</td>";
        retValue += "</tr>";




        //retValue += "<tr>";
        //retValue += "<td width='15%'>&nbsp;</td>";
        //retValue += "<td width='85%'>&nbsp;</td>";
        //retValue += "</tr>";

        //retValue += "<tr>";
        //retValue += "<td> </td>";
        //retValue += "<td>" + password + "</td>";
        //retValue += "</tr>";

        //retValue += "<tr>";
        //retValue += "<td>Last Name</td>";
        //retValue += "<td>" + txtlastName.Text + "</td>";
        //retValue += "</tr>";

        retValue += "</table>";
        return retValue;
    }


    public DataTable Fetch_MailIdsOFUser_SubUser(string SiteId)
    {

        string strError = "";
        string strSQL = @"select ZZ.*,xx.username as UName,xx.lastname as ULanme,xx.email as UEmail from 
(
SELECT
  ROW_NUMBER ( )     OVER (  order by LinkExchange.UserRegistration.UserName )as RN,
  LinkExchange.WebSite_Master.SMTP_UserName,
  LinkExchange.WebSite_Master.SMTP_Port,
  LinkExchange.WebSite_Master.SMTPServer,
  LinkExchange.WebSite_Master.SMTP_Password,
  LinkExchange.WebSite_Master.DisplayName,
  CASE WHEN LinkExchange.WebSite_Master.SSL IS NULL THEN '0' ELSE LinkExchange.WebSite_Master.SSL END as SSl ,

  LinkExchange.UserRegistration.UserName,
  LinkExchange.UserRegistration.LastName,
  LinkExchange.UserRegistration.Email
FROM
  LinkExchange.WebSite_Master
  INNER JOIN LinkExchange.User_WebSite_Relation ON (LinkExchange.WebSite_Master.ID = LinkExchange.User_WebSite_Relation.WebSiteId)
  INNER JOIN LinkExchange.UserRegistration ON (LinkExchange.User_WebSite_Relation.UserId = LinkExchange.UserRegistration.UserId)
WHERE " +
        " (LinkExchange.User_WebSite_Relation.WebSiteId = " + SiteId + ") and LinkExchange.UserRegistration.type=2 " +
       @" )ZZ left outer join

(
SELECT 
  ROW_NUMBER ( )     OVER (  order by LinkExchange.UserRegistration.UserName )as RNN,
  
LinkExchange.UserRegistration.UserName,
 LinkExchange.UserRegistration.LastName,
  LinkExchange.UserRegistration.Email
FROM
  LinkExchange.UserRegistration
  INNER JOIN LinkExchange.WebSite_Master ON (LinkExchange.UserRegistration.UserId = LinkExchange.WebSite_Master.UserId)
WHERE " +
        " (LinkExchange.WebSite_Master.id = " + SiteId + ")" +
       @"and LinkExchange.UserRegistration.type=1
)XX on xx.rnn=zz.rn ";

        DataTable dtMail = FillDataTable(strSQL, ref  strError);
        return dtMail;

    }


    public bool FetchLinkDetailsViewData(ref DataTable dtLink, string SiteId, string SubPageId, string Status, ref int intTotalProd, int intPageNo, int numberofProduct, ref string strPagination, ref string strError)
    {
        bool flag = false;
        string strSQL = "";
        //int intTotalProd = 0;
        string strAllProd = "";
        if (SiteId != null)
        {
            //strSQL = " SELECT " +
            //       " ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName) AS 'SlNo'," +
            //       " LinkExchange.LinkExchangeMaster.Id," +
            //       "  LinkExchange.SubPage.SubPageName," +
            //    //  " LinkExchange.LinkExchangeMaster.HTMLcode," +
            //       " CASE WHEN LEN(LinkExchange.LinkExchangeMaster.HTMLcode) > 170 " +
            //        " THEN " +
            //        " REPLACE(SUBSTRING(LinkExchange.LinkExchangeMaster.HTMLcode, 0, 170),'~','''')+'..' " +
            //       " ELSE " +
            //      "  LinkExchange.LinkExchangeMaster.HTMLcode END as HTMLcode, " +
            //       " CASE WHEN LinkExchange.LinkExchangeMaster.Reciprocal IS NULL THEN '' ELSE " +
            //       " '<a href=\"'+LinkExchange.LinkExchangeMaster.Reciprocal + '\" target=\"_blank\">Our Link</a>' END as Reciprocal ," +
            //      " '<a href=\"'+LinkExchange.LinkMaster.SiteURL + '\" target=\"_blank\">'+LinkExchange.LinkMaster.Heading +'</a>'+ " +
            //      " CASE WHEN LEN(LinkExchange.LinkMaster.Description) > 170 " +
            //      "   THEN" +
            //       " REPLACE(SUBSTRING(LinkExchange.LinkMaster.Description, 0, 170),'~','''')+'..' " +
            //      " ELSE  " +
            //      " LinkExchange.LinkMaster.Description END as ouradd " +
            //       " FROM " +
            //       " LinkExchange.SubPage " +
            //       "  INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID)" +
            //       " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
            //       " WHERE " +
            //       "  (LinkExchange.LinkExchangeMaster.WebSiteId = " + SiteId + ") AND " +
            //       "(LinkExchange.LinkExchangeMaster.Status = " + Status + ") and (LinkExchange.LinkExchangeMaster.Id = " + LinkId + ")";

            strSQL = " select * from " +
   "( " +
   " SELECT  ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName) AS 'SlNo'," +
   " (((ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName)-1)/25)+1) AS 'PageNumber', " +
   " LinkExchange.LinkExchangeMaster.Id, " +
   "  LinkExchange.SubPage.SubPageName," +

   "  CASE WHEN LEN(LinkExchange.LinkExchangeMaster.HTMLcode) > 170  THEN  REPLACE(SUBSTRING(LinkExchange.LinkExchangeMaster.HTMLcode, 0, 170),'~','''')+'..'  " +
   " ELSE   LinkExchange.LinkExchangeMaster.HTMLcode END as HTMLcode, " +
   " CASE WHEN LinkExchange.LinkExchangeMaster.Reciprocal IS NULL THEN '' ELSE  '<a href=\"'+LinkExchange.LinkExchangeMaster.Reciprocal + '\" target=\"_blank\">Our Link</a>' END as Reciprocal , " +
   " '<a href=\"'+LinkExchange.LinkMaster.SiteURL + '\" target=\"_blank\">'+LinkExchange.LinkMaster.Heading +'</a>'+  CASE WHEN LEN(LinkExchange.LinkMaster.Description) > 170    THEN REPLACE(SUBSTRING(LinkExchange.LinkMaster.Description, 0, 170),'~','''')+'..'  ELSE   LinkExchange.LinkMaster.Description END as ouradd " +
   "  FROM  LinkExchange.SubPage   " +
   " INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID) " +
   " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID) " +
   " WHERE   " +
   " (LinkExchange.LinkExchangeMaster.WebSiteId =  " + SiteId + ") AND (LinkExchange.LinkExchangeMaster.Status = " + Status + ") " +
   " )Q " +
   "where Q.PageNumber=" + intPageNo + " " +
   " order by Q.SubPageName";

            strAllProd = "SELECT  " +
           " count(LinkExchange.LinkExchangeMaster.Id) as cnt " +
           " FROM  LinkExchange.SubPage   " +
           " INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID) " +
           " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID) " +
           " WHERE   " +
           " (LinkExchange.LinkExchangeMaster.WebSiteId =" + SiteId + ") AND (LinkExchange.LinkExchangeMaster.Status = " + Status + ") ";

            // Execute(strAllProd,ref str
            if (CountProd_Pagination("SiteId", SiteId, SubPageId, Status, strAllProd, intPageNo, numberofProduct, ref  strPagination, ref  intTotalProd, ref  strError))
            {

                dtLink = FillDataTable(strSQL, ref strError);
                if (strError == null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

            }

        }
        else
        {

            //strSQL = " SELECT " +
            //      " ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName) AS 'SlNo'," +
            //      " LinkExchange.LinkExchangeMaster.Id," +
            //      "  LinkExchange.SubPage.SubPageName," +
            //      " CASE WHEN LEN(LinkExchange.LinkExchangeMaster.HTMLcode) > 170 " +
            //       " THEN " +
            //       " REPLACE(SUBSTRING(LinkExchange.LinkExchangeMaster.HTMLcode, 0, 170),'~','''')+'..' " +
            //      " ELSE " +
            //     "  LinkExchange.LinkExchangeMaster.HTMLcode END as HTMLcode, " +
            //      " CASE WHEN LinkExchange.LinkExchangeMaster.Reciprocal IS NULL THEN '' ELSE " +
            //      " '<a href=\"'+LinkExchange.LinkExchangeMaster.Reciprocal + '\" target=\"_blank\">Our Link</a>' END as Reciprocal ," +
            //     " '<a href=\"'+LinkExchange.LinkMaster.SiteURL + '\" target=\"_blank\">'+LinkExchange.LinkMaster.Heading +'</a>'+ " +
            //     " CASE WHEN LEN(LinkExchange.LinkMaster.Description) > 170 " +
            //     "   THEN" +
            //      " REPLACE(SUBSTRING(LinkExchange.LinkMaster.Description, 0, 170),'~','''')+'..' " +
            //     " ELSE  " +
            //     " LinkExchange.LinkMaster.Description END as ouradd " +
            //      " FROM " +
            //      " LinkExchange.SubPage " +
            //      "  INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID)" +
            //      " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
            //      " WHERE " +
            //       "  (LinkExchange.SubPage.Id = " + SubPageId + ") AND " +
            //        "(LinkExchange.LinkExchangeMaster.Status = " + Status + ")" +
            //      " and (LinkExchange.LinkExchangeMaster.Id = " + LinkId + ")";



            strSQL = " select * from " +
"( " +
 " SELECT " +
                   " ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName) AS 'SlNo'," +
                     " (((ROW_NUMBER() OVER(ORDER BY LinkExchange.SubPage.SubPageName)-1)/25)+1) AS 'PageNumber', " +
                   " LinkExchange.LinkExchangeMaster.Id," +
                   "  LinkExchange.SubPage.SubPageName," +

                   " CASE WHEN LEN(LinkExchange.LinkExchangeMaster.HTMLcode) > 170 " +
                    " THEN " +
                    " REPLACE(SUBSTRING(LinkExchange.LinkExchangeMaster.HTMLcode, 0, 170),'~','''')+'..' " +
                   " ELSE " +
                  "  LinkExchange.LinkExchangeMaster.HTMLcode END as HTMLcode, " +
                   " CASE WHEN LinkExchange.LinkExchangeMaster.Reciprocal IS NULL THEN '' ELSE " +
                   " '<a href=\"'+LinkExchange.LinkExchangeMaster.Reciprocal + '\" target=\"_blank\">Our Link</a>' END as Reciprocal ," +
                  " '<a href=\"'+LinkExchange.LinkMaster.SiteURL + '\" target=\"_blank\">'+LinkExchange.LinkMaster.Heading +'</a>'+ " +
                  " CASE WHEN LEN(LinkExchange.LinkMaster.Description) > 170 " +
                  "   THEN" +
                   " REPLACE(SUBSTRING(LinkExchange.LinkMaster.Description, 0, 170),'~','''')+'..' " +
                  " ELSE  " +
                  " LinkExchange.LinkMaster.Description END as ouradd " +
                   " FROM " +
                   " LinkExchange.SubPage " +
                   "  INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID)" +
                   " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
                   " WHERE " +
                    "  (LinkExchange.SubPage.Id = " + SubPageId + ") AND " +
                     "(LinkExchange.LinkExchangeMaster.Status = " + Status + ")" +

                " )Q " +
            "where Q.PageNumber=" + intPageNo + " " +
            " order by Q.SubPageName";

            strAllProd = "SELECT  " +
           " count(LinkExchange.LinkExchangeMaster.Id) as cnt " +
           " FROM  LinkExchange.SubPage   " +
           "  INNER JOIN LinkExchange.LinkExchangeMaster ON (LinkExchange.SubPage.Id = LinkExchange.LinkExchangeMaster.SubPageID)" +
           " INNER JOIN LinkExchange.LinkMaster ON (LinkExchange.LinkExchangeMaster.OurAdId = LinkExchange.LinkMaster.ID)" +
           " WHERE   " +
          "  (LinkExchange.SubPage.Id = " + SubPageId + ") AND " +
           "(LinkExchange.LinkExchangeMaster.Status = " + Status + ")";

            if (CountProd_Pagination("SubPageId", SiteId, SubPageId, Status, strAllProd, intPageNo, numberofProduct, ref  strPagination, ref  intTotalProd, ref  strError))
            {

                dtLink = FillDataTable(strSQL, ref strError);
                if (strError == null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

            }
        }



        // Pagination logic starts


        return flag;



    }
    public bool CountProd_Pagination(string type, string SiteId, string SubPageId, string Status, string strSql, int intPageNo, int numberofProduct, ref string strPagination, ref int intTotalProd, ref string strError)
    {
        bool flag = false;
        string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());

        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strSql, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            intTotalProd = Convert.ToInt32(rdr["cnt"]);
                            if (intPageNo == 0)
                            {
                                //strPagination = "<li>" + intTotalProd + " of " + intTotalProd + "</li>";
                            }
                            else
                            {
                                if (intTotalProd > 1)
                                {
                                    int intRowUpto = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(intTotalProd) / numberofProduct));
                                    StringBuilder strReturnPagination = new StringBuilder();
                                    strReturnPagination.Append("<ul class=\"pagination\">");
                                    //strReturnPagination.Append(intPageNo + " - " +  + " of " + intTotalProd + "&nbsp;&nbsp;");
                                    if (intPageNo == 1)
                                    {
                                        if (intTotalProd < numberofProduct)
                                        {
                                            //strReturnPagination.Append("<li>" + intPageNo + " - " + intTotalProd + " of " + intTotalProd + "</li>");
                                            //strReturnPagination.Append("<li><img src=\"images/prev_arrow.gif\"alt=\"Previous\" border=\"0\" title=\"Previous\"></li>");
                                            strReturnPagination.Append("<li>Previous</li>");
                                        }
                                        else
                                        {
                                            // strReturnPagination.Append("<li>" + intPageNo + " - " + numberofProduct + " of " + intTotalProd + "</li>");
                                            //strReturnPagination.Append("<li><img src=\"images/prev_arrow.gif\"alt=\"Previous\" border=\"0\" title=\"Previous\"></li>");
                                            strReturnPagination.Append("<li>Previous</li>");

                                        }
                                    }
                                    else
                                    {
                                        //if ((intPageNo * numberofProduct) > intTotalProd)
                                        //{
                                        //    strReturnPagination.Append("<li>" + (((intPageNo - 1) * numberofProduct) + 1) + " - " + intTotalProd + " of " + intTotalProd + "</li>");
                                        //}
                                        //else
                                        //{
                                        //    strReturnPagination.Append("<li>" + (((intPageNo - 1) * numberofProduct) + 1) + " - " + (intPageNo * numberofProduct) + " of " + intTotalProd + "</li>");
                                        //}
                                        if (type == "SiteId")
                                        {
                                            strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?" + type + "=" + SiteId + "&Status=" + Status + "&pageno=" + (intPageNo - 1) + "\" title=\"Page " + Convert.ToString(intPageNo - 1) + "\"> Previous </a></li>");
                                        }
                                        else
                                        {
                                            strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?" + type + "=" + SubPageId + "&Status=" + Status + "&pageno=" + (intPageNo - 1) + "\" title=\"Page " + Convert.ToString(intPageNo - 1) + "\"> Previous </a></li>");

                                        }

                                    }
                                    for (int i = 0; i < intRowUpto; i++)
                                    {
                                        if ((i + 1) == intPageNo)
                                        {
                                            strReturnPagination.Append("<li><strong>" + (i + 1) + "</strong></li>");
                                        }
                                        else if (i == intRowUpto)
                                        {
                                            strReturnPagination.Append("<li ><strong>" + (i + 1) + "</strong></li>");
                                        }
                                        else
                                        {
                                            // strReturnPagination.Append("<li><a href=\"" + strDomain + "/" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?r=" + rangeGiftsbyPrice + "&pageno=" + (i + 1) + "\" title=\"Page " + Convert.ToString(i + 1) + "\"><strong>" + (i + 1) + "</strong></a></li>");

                                            if (type == "SiteId")
                                            {
                                                strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?" + type + "=" + SiteId + "&Status=" + Status + "&pageno=" + (i + 1) + "\" title=\"Page " + (i + 1) + "\">" + (i + 1) + "&nbsp;</a></li>");
                                            }
                                            else
                                            {
                                                strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?" + type + "=" + SubPageId + "&Status=" + Status + "&pageno=" + (i + 1) + "\" title=\"Page " + (i + 1) + "\">" + (i + 1) + "&nbsp;</a></li>");

                                            }

                                        }
                                    }
                                    if (intPageNo == intRowUpto)
                                    {
                                        // strReturnPagination.Append("<li><img src=\"images/next_arrow.gif\" alt=\"Next\" border=\"0\" title=\"Next\"></li>");
                                        strReturnPagination.Append("<li>Next</li>");
                                    }
                                    else
                                    {
                                        //strReturnPagination.Append("<li><a href=\"" + strDomain + "/" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?r=" + rangeGiftsbyPrice + "&pageno=" + (intPageNo + 1) + "\" title=\"Next\"><img src=\"images/next_arrow.gif\" alt=\"Next\" border=\"0\" title=\"Next\"></a></li>");
                                        if (type == "SiteId")
                                        {
                                            strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?" + type + "=" + SiteId + "&Status=" + Status + "&pageno=" + (intPageNo + 1) + "\" title=\"Page " + Convert.ToString(intPageNo + 1) + "\">Next</a></li>");
                                        }
                                        else
                                        {
                                            strReturnPagination.Append("<li><a href=\"" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?" + type + "=" + SubPageId + "&Status=" + Status + "&pageno=" + (intPageNo + 1) + "\" title=\"Page " + Convert.ToString(intPageNo + 1) + "\">Next</a></li>");

                                        }
                                    }
                                    //strReturnPagination.Append("<li class=\"noMarg\"><a href=\"" + strDomain + "/" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?r=" + rangeGiftsbyPrice + "&pageno=0\"><img src=\"images/view_all_img.gif\" alt=\"View All\" border=\"0\" title=\"View All\"></a></li>");
                                    strReturnPagination.Append("</ul>");
                                    strPagination = strReturnPagination.ToString();

                                }
                            }

                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                strError = ex.Message;
            }
        }
        return flag;
    }

   // # region GooglePagination_GiftsbyPrice
   // public bool getProduct_GiftsbyPricePage_CategoryPage(string categoryId, int rangeGiftsbyPrice, int numberofProduct, int intPageNo, ref string strOutPut, ref string strPagination, ref string strToppage, ref string strError)
   // {
   //     bool blFlag = false;
   //     int intCurrencyId = Convert.ToInt32(HttpContext.Current.Session["CurrencyId"]);
   //     double dblCurrencyValue = 1;
   //     string strCurrencySymbol = Convert.ToString(HttpContext.Current.Session["CurrencySymbol"]);
   //     string strSql = "";
   //     string strAllProd = "";
   //     int intTotalProd = 0;
   //     StringBuilder strProdOutPut = new StringBuilder();
   //     int numberOfProducttoshow = Convert.ToInt32(ConfigurationManager.AppSettings["noOfProduct"]); // 20;
   //     if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol))
   //     {
   //         switch (rangeGiftsbyPrice)
   //         {
   //             case 1:         // Upto 500
   //                 strAllProd = "SELECT count(" + strSchema + ".ndtv_giftstoindia24x7_GBP5004.Product_Id ) as 'TotCount' " +
   //                        "FROM " + strSchema + ".ndtv_giftstoindia24x7_GBP5004 ";
   //                 strAllProd = "SELECT COUNT(" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id) AS [TotCount]" +
   //                         " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                         " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                         " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1'))" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') ";


   //                 strAllProd = " SELECT count(distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)) as TotCount " +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                           " WHERE (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND  (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                           " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                           " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                           " and (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1 and 500 ";
   //                 if (intPageNo == 0)
   //                 {
   //                     strSql = "SELECT DISTINCT " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id," +
   //                             strSchema + ".ItemMaster_Server.Item_Name, " +
   //                             " '" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                             "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                             strSchema + ".ItemMaster_Server.Item_Price " +
   //                             " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                             " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                             " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                             " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                             " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                             " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') " +
   //                             " ORDER BY " +
   //                                 "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                                 "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";

   //                     strSql = " SELECT  " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                            "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                           " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                           " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                           " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                          " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND  " +
   //                             " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1 and 500 " +
   //                             " ORDER BY rgcards_gti24x7.ItemMaster_Server.Item_Price DESC ";
   //                 }
   //                 else
   //                 {
   //                     strSql = "SELECT TOP " + numberofProduct + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Name, " +
   //                         "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                         "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                         "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Price " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id NOT IN " +
   //                         "(SELECT TOP " + intInner + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1') " +
   //                         "AND(" + strSchema + ".ItemMaster_Server.Record_Status='1')) " +
   //                         "ORDER BY " + strSchema + ".ItemMaster_Server.Item_Price DESC) " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') " +
   //                         " ORDER BY " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                             "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";

   //                     strSql = " select * from " +
   //                             " (" +
   //                             " SELECT *, " +
   //                             " ROW_NUMBER() OVER(ORDER BY tbl1.Product_Id) AS 'SlNo'" +
   //                             " from (" +
   //                             " SELECT " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                             "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                            " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                           " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                             " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1 and 500 " +
   //                             " ) as tbl1 ) tab2 where tab2.SlNo<=" + numberOfProducttoshow + "*" + intPageNo + " AND " +
   //                             " tab2.SlNo >" + numberOfProducttoshow + "*(" + intPageNo + "-" + 1 + ") ORDER BY tab2.Item_Price DESC   ";
   //                 }
   //                 break;
   //             case 2:         // 501 - 1000
   //                 strAllProd = "SELECT count(" + strSchema + ".ndtv_giftstoindia24x7_GBP100004.Product_Id ) as 'TotCount' " +
   //                        "FROM " + strSchema + ".ndtv_giftstoindia24x7_GBP100004 ";
   //                 strAllProd = "SELECT COUNT(" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id) AS [TotCount]" +
   //                         " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                         " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '500') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '1000') " +
   //                         " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1'))" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '500') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '1000') ";
   //                 strAllProd = " SELECT count(distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)) as TotCount " +
   //                             " FROM " +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND  (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                             "(rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                           " and (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                           "  rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                            " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 501 and 1000 ";
   //                 if (intPageNo == 0)
   //                 {
   //                     strSql = "SELECT DISTINCT " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id," +
   //                             strSchema + ".ItemMaster_Server.Item_Name, " +
   //                             " '" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                             "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                             strSchema + ".ItemMaster_Server.Item_Price " +
   //                             " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                             " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                             " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                             " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                             " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '500') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '1000') " +
   //                             " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '500') " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '1000') " +
   //                             " ORDER BY " +
   //                                 "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                                 "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";
   //                     strSql = " SELECT  " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                           "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                           " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                            " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                            " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                             " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 501 and 1000 " +
   //                             " ORDER BY rgcards_gti24x7.ItemMaster_Server.Item_Price DESC ";
   //                 }
   //                 else
   //                 {
   //                     strSql = "SELECT TOP " + numberofProduct + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Name, " +
   //                         "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                         "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                         "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Price " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id NOT IN " +
   //                         "(SELECT TOP " + intInner + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '500') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '1000') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1') " +
   //                         "AND(" + strSchema + ".ItemMaster_Server.Record_Status='1')) " +
   //                         "ORDER BY " + strSchema + ".ItemMaster_Server.Item_Price DESC) " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '500') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '1000') " +
   //                         " ORDER BY " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                             "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";
   //                     strSql = " select * from " +
   //                             " (" +
   //                             " SELECT *, " +
   //                             " ROW_NUMBER() OVER(ORDER BY tbl1.Product_Id) AS 'SlNo' " +
   //                             " from (" +
   //                             " SELECT " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                             " '" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                            " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                            "  rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                           "(rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                          " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                          " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND  " +
   //                           "  (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                            " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null " +
   //                            " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 501 and 1000 " +
   //                           " ) as tbl1 ) tab2 where tab2.SlNo<=" + numberOfProducttoshow + "*" + intPageNo + " AND " +
   //                           " tab2.SlNo >" + numberOfProducttoshow + "*(" + intPageNo + "-" + 1 + ") ORDER BY tab2.Item_Price DESC   ";
   //                 }
   //                 break;
   //             case 3:         //1001 - 2000
   //                 strAllProd = "SELECT count(" + strSchema + ".ndtv_giftstoindia24x7_GBP200004.Product_Id ) as 'TotCount' " +
   //                        "FROM " + strSchema + ".ndtv_giftstoindia24x7_GBP200004 ";
   //                 strAllProd = "SELECT COUNT(" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id) AS [TotCount]" +
   //                         " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                         " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '1000') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '2000') " +
   //                         " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1'))" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '1000') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '2000') ";
   //                 strAllProd = " SELECT count(distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)) as TotCount " +
   //                             " FROM " +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                            " WHERE (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND  (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                           " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                            " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                           " and (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                            " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null " +
   //                            " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1001 and 2000 ";

   //                 if (intPageNo == 0)
   //                 {
   //                     strSql = "SELECT DISTINCT " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id," +
   //                             strSchema + ".ItemMaster_Server.Item_Name, " +
   //                             " '" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                             "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                             strSchema + ".ItemMaster_Server.Item_Price " +
   //                             " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                             " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                             " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                             " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                             " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '1000') " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '2000') " +
   //                             " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                             " ORDER BY " +
   //                                 "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                                 "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";
   //                     strSql = " SELECT " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                             "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                            " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                             " FROM " +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                            " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                            " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1001 and 2000 " +
   //                             " ORDER BY rgcards_gti24x7.ItemMaster_Server.Item_Price DESC ";
   //                 }
   //                 else
   //                 {
   //                     strSql = "SELECT TOP " + numberofProduct + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Name, " +
   //                         "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                         "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                         "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Price " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id NOT IN " +
   //                         "(SELECT TOP " + intInner + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '1000') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '2000') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1') " +
   //                         "AND(" + strSchema + ".ItemMaster_Server.Record_Status='1')) " +
   //                         "ORDER BY " + strSchema + ".ItemMaster_Server.Item_Price DESC) " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '1000') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '2000') " +
   //                         " ORDER BY " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                             "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";

   //                     strSql = " select * from " +
   //                             "(" +
   //                            "  SELECT *, " +
   //                            " ROW_NUMBER() OVER(ORDER BY tbl1.Product_Id) AS 'SlNo' " +
   //                            "  from (" +
   //                             " SELECT " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                            "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                           " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status " +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id) " +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                             " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                             " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null " +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1001 and 2000 " +
   //                            " ) as tbl1 ) tab2 where tab2.SlNo<=" + numberOfProducttoshow + "*" + intPageNo + " AND " +
   //                             " tab2.SlNo >" + numberOfProducttoshow + "*(" + intPageNo + "-" + 1 + ") ORDER BY tab2.Item_Price DESC   ";
   //                 }
   //                 break;
   //             case 4:             //> 2001
   //                 strAllProd = "SELECT count(" + strSchema + ".ndtv_giftstoindia24x7_GBPAbove200004.Product_Id ) as 'TotCount' " +
   //                        "FROM " + strSchema + ".ndtv_giftstoindia24x7_GBPAbove200004 ";
   //                 strAllProd = "SELECT COUNT(" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id) AS [TotCount]" +
   //                         " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                         " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                         " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1'))" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '2000') ";
   //                 strAllProd = " SELECT count(distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)) as TotCount " +
   //                             " FROM " +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id) " +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                            " WHERE (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND  (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                            " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                            "  (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                            " and (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                            "   rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null " +
   //                            " and rgcards_gti24x7.ItemMaster_Server.Item_Price >2000 ";
   //                 if (intPageNo == 0)
   //                 {
   //                     strSql = "SELECT DISTINCT " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id," +
   //                             strSchema + ".ItemMaster_Server.Item_Name, " +
   //                             " '" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                             "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                             strSchema + ".ItemMaster_Server.Item_Price " +
   //                             " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                             " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                             " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                             " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                             " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                             " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '2000') " +
   //                             " ORDER BY " +
   //                                 "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                                 "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";
   //                     strSql = " SELECT  " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                           "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                          " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                             "(rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                           " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND         " +
   //                            " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                            " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null " +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price > 2000 " +
   //                             " ORDER BY rgcards_gti24x7.ItemMaster_Server.Item_Price DESC ";
   //                 }
   //                 else
   //                 {
   //                     strSql = "SELECT TOP " + numberofProduct + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Name, " +
   //                         "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                         "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                         "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Price " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id NOT IN " +
   //                         "(SELECT TOP " + intInner + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '2000') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1') " +
   //                         "AND(" + strSchema + ".ItemMaster_Server.Record_Status='1')) " +
   //                         "ORDER BY " + strSchema + ".ItemMaster_Server.Item_Price DESC) " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '2000') " +
   //                         " ORDER BY " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                             "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";
   //                     strSql = " select * from " +
   //                             " (" +
   //                             " SELECT *, " +
   //                             " ROW_NUMBER() OVER(ORDER BY tbl1.Product_Id) AS 'SlNo'" +
   //                             " from (" +
   //                             " SELECT " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                        "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                        " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                          "   rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                            " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                            "  rgcards_gti24x7.ItemMaster_Server.Record_Status " +
   //                             " FROM " +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                            " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                            "  INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                          " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                          " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                         " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND  " +
   //                         " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                         " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                          " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                           " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null " +
   //                           " and rgcards_gti24x7.ItemMaster_Server.Item_Price > 2000 " +
   //                        " ) as tbl1 ) tab2 where tab2.SlNo<=" + numberOfProducttoshow + "*" + intPageNo + " AND " +
   //                        " tab2.SlNo >" + numberOfProducttoshow + "*(" + intPageNo + "-" + 1 + ") ORDER BY tab2.Item_Price DESC   ";
   //                 }
   //                 break;
   //             default:
   //                 strAllProd = "SELECT count(" + strSchema + ".ndtv_giftstoindia24x7_GBP5004.Product_Id ) as 'TotCount' " +
   //                        "FROM " + strSchema + ".ndtv_giftstoindia24x7_GBP5004 ";
   //                 strAllProd = "SELECT COUNT(" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id) AS [TotCount]" +
   //                         " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                         " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                         " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1'))" +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') ";
   //                 strAllProd = " SELECT count(distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)) as TotCount " +
   //                               " FROM" +
   //                               " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                               " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                               " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND  (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                             " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                               " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                             " and (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                               " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                               " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1 and 500 ";
   //                 if (intPageNo == 0)
   //                 {
   //                     strSql = "SELECT DISTINCT " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id," +
   //                             strSchema + ".ItemMaster_Server.Item_Name, " +
   //                             " '" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                             "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                             strSchema + ".ItemMaster_Server.Item_Price " +
   //                             " FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                             " INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                             " ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                             " WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "')" +
   //                             " AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1')" +
   //                             " AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                             "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') " +
   //                             " ORDER BY " +
   //                                 "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                                 "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";
   //                     strSql = " SELECT  " +
   //                              " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                             "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                            " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                              " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                              " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                              " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                              " FROM" +
   //                              " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                              " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                              " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                              " WHERE " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                           " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND  " +
   //                              " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                              " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                              " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                              " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                              " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1 and 500 " +
   //                              " ORDER BY rgcards_gti24x7.ItemMaster_Server.Item_Price DESC ";


   //                 }
   //                 else
   //                 {
   //                     strSql = "SELECT TOP " + numberofProduct + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Name, " +
   //                         "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                         "" + strSchema + ".ItemMaster_Server.Record_Status, " +
   //                         "" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id, " +
   //                         "" + strSchema + ".ItemMaster_Server.Item_Price " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id NOT IN " +
   //                         "(SELECT TOP " + intInner + " " + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id " +
   //                         "FROM " + strSchema + ".ItemCategoryRelation_Web_Server " +
   //                         "INNER JOIN " + strSchema + ".ItemMaster_Server " +
   //                         "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
   //                         "WHERE ((" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1') " +
   //                         "AND(" + strSchema + ".ItemMaster_Server.Record_Status='1')) " +
   //                         "ORDER BY " + strSchema + ".ItemMaster_Server.Item_Price DESC) " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id='" + categoryId + "') " +
   //                         "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
   //                         "AND  (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0' OR " + strSchema + ".ItemMaster_Server.Item_colour='1')) " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] > '0') " +
   //                         "AND ([" + strSchema + "].[ItemMaster_Server].[Item_Price] <= '500') " +
   //                         " ORDER BY " +
   //                             "" + strSchema + ".ItemMaster_Server.Record_Status DESC, " +
   //                             "" + strSchema + ".ItemMaster_Server.Item_Price DESC;";
   //                     strSql = " select * from " +
   //                             " (" +
   //                             " SELECT *, " +
   //                             " ROW_NUMBER() OVER(ORDER BY tbl1.Product_Id) AS 'SlNo'" +
   //                             " from (" +
   //                             " SELECT " +
   //                             " distinct(rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id)," +
   //                             "'" + strSitePath + "/ASP_Img/small_img/' + " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
   //                            " rgcards_gti24x7.ItemMaster_Server.Item_Name," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Item_Price," +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory," +
   //                             " rgcards_gti24x7.ItemMaster_Server.Record_Status" +
   //                             " FROM" +
   //                             " rgcards_gti24x7.SiteCatgory_Web_Server" +
   //                             " INNER JOIN rgcards_gti24x7.ItemCategoryRelation_Web_Server ON (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id = rgcards_gti24x7.ItemCategoryRelation_Web_Server.Category_Id)" +
   //                             " INNER JOIN rgcards_gti24x7.ItemMaster_Server ON (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Product_Id = rgcards_gti24x7.ItemMaster_Server.Product_Id)" +
   //                             " WHERE " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Site_Id = " + Convert.ToInt32(HttpContext.Current.Application["SiteId"]) + " ) AND " +
   //                            " (rgcards_gti24x7.SiteCatgory_Web_Server.Category_Id='" + categoryId + " ')" +
   //                           " and (rgcards_gti24x7.ItemMaster_Server.Item_colour IS NULL OR rgcards_gti24x7.ItemMaster_Server.Item_colour='0' OR rgcards_gti24x7.ItemMaster_Server.Item_colour='1') AND " +
   //                             " (rgcards_gti24x7.ItemMaster_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.ItemCategoryRelation_Web_Server.Record_Status = 1) AND " +
   //                             " (rgcards_gti24x7.SiteCatgory_Web_Server.Record_Status = 1) AND " +
   //                             " rgcards_gti24x7.ItemMaster_Server.DefaultCategory is not null" +
   //                             " and rgcards_gti24x7.ItemMaster_Server.Item_Price between 1 and 500 " +
   //                             " ) as tbl1 ) tab2 where tab2.SlNo<=" + numberOfProducttoshow + "*" + intPageNo + " AND " +
   //                             " tab2.SlNo >" + numberOfProducttoshow + "*(" + intPageNo + "-" + 1 + ") ORDER BY tab2.Item_Price DESC   ";
   //                 }
   //                 break;
   //         }
   //         try
   //         {
   //             if (_conn.State != ConnectionState.Open)
   //             {
   //                 _conn.Open();

   //             }
   //             SqlCommand cmdProdCat = new SqlCommand(strAllProd.Replace("rgcards_gti24x7", strSchema), conn);
   //             SqlDataReader drProdCatAll = cmdProdCat.ExecuteReader(CommandBehavior.CloseConnection);
   //             if (drProdCatAll.HasRows)
   //             {
   //                 if (drProdCatAll.Read())
   //                 {
   //                     intTotalProd = Convert.ToInt32(drProdCatAll["TotCount"].ToString());
   //                 }
   //             }
   //             drProdCatAll.Dispose();
   //             cmdProdCat.Dispose();
   //             DataTable dtAllProduct = new DataTable();
   //             SqlDataAdapter daProdCat = new SqlDataAdapter(strSql.Replace("rgcards_gti24x7", strSchema), conn);
   //             daProdCat.Fill(dtAllProduct);
   //             if (dtAllProduct.Rows.Count > 0)
   //             {
   //                 int intTotalCount = 0;
   //                 int intCount = 0;
   //                 blFlag = true;
   //                 strProdOutPut.Append("<ul class=\"category\">");
   //                 string strProdPrice = "";
   //                 string strLink = "";
   //                 string strCurrProdId = "";
   //                 foreach (DataRow drProdCat in dtAllProduct.Rows)
   //                 {
   //                     strCurrProdId = Convert.ToString(drProdCat["Product_Id"].ToString());
   //                     int intCategoryId = Convert.ToInt32(drProdCat["DefaultCategory"].ToString());
   //                     strProdPrice = "Rs." + drProdCat["Item_Price"].ToString() + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(double.Parse(drProdCat["Item_Price"].ToString()) / dblCurrencyValue).ToString("0.00");
   //                     strLink = strDomain + "/Gifts.aspx?proid=" + drProdCat["Product_Id"].ToString() + "&CatId=" + intCategoryId;
   //                     if ((intTotalCount + 1) >= numberofProduct)
   //                     {
   //                         if ((intTotalCount % 4) != 0)
   //                         {
   //                             strProdOutPut.Append("<li class=\"noMarg\">");
   //                             strProdOutPut.Append("<a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\"><img alt=\"" + drProdCat["Item_Name"].ToString() + "\"  border=\"0\" title=\"" + drProdCat["Item_Name"].ToString() + "\" src=\"" + drProdCat["Product_Image"].ToString() + "\"/></a>");
   //                             strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
   //                             strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
   //                             strProdOutPut.Append("<span id=\"spnImg_" + drProdCat["Product_Id"].ToString() + "\" class=\"addCart\"><img id=\"add_" + drProdCat["Product_Id"].ToString() + "\" onClick=\"javascript:addToCartNew('spnImg_" + drProdCat["Product_Id"].ToString() + "', 'spnAjax_" + drProdCat["Product_Id"].ToString() + "', '" + drProdCat["Product_Id"].ToString() + "', '" + intCategoryId + "', 1);\" src=\"../images/mday_cartBtn.gif\" alt=\"Add to Cart\" title=\"Add to Cart\" /></span><span id=\"spnAjax_" + drProdCat["Product_Id"].ToString() + "\" style=\"display:none\" class=\"loadImg\"><img alt=\"Wait...\" src=\"Pictures/loading.gif\" width=\"16\" height=\"16\" />Wait...</span>");
   //                             strProdOutPut.Append("</li>");
   //                         }
   //                         else
   //                         {
   //                             strProdOutPut.Append("<li class=\"noMarg\">");
   //                             strProdOutPut.Append("<a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\"><img alt=\"" + drProdCat["Item_Name"].ToString() + "\"  border=\"0\" title=\"" + drProdCat["Item_Name"].ToString() + "\" src=\"" + drProdCat["Product_Image"].ToString() + "\"/></a>");
   //                             strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
   //                             strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
   //                             strProdOutPut.Append("<span id=\"spnImg_" + drProdCat["Product_Id"].ToString() + "\" class=\"addCart\"><img id=\"add_" + drProdCat["Product_Id"].ToString() + "\" onClick=\"javascript:addToCartNew('spnImg_" + drProdCat["Product_Id"].ToString() + "', 'spnAjax_" + drProdCat["Product_Id"].ToString() + "', '" + drProdCat["Product_Id"].ToString() + "', '" + intCategoryId + "', 1);\" src=\"../images/mday_cartBtn.gif\" alt=\"Add to Cart\" title=\"Add to Cart\" /></span><span id=\"spnAjax_" + drProdCat["Product_Id"].ToString() + "\" style=\"display:none\" class=\"loadImg\"><img alt=\"Wait...\" src=\"Pictures/loading.gif\" width=\"16\" height=\"16\" />Wait...</span>");
   //                             strProdOutPut.Append("</li>");
   //                         }
   //                         intCount++;
   //                     }
   //                     else if (intCount == 0)
   //                     {
   //                         strProdOutPut.Append("<li>");
   //                         strProdOutPut.Append("<a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\"><img alt=\"" + drProdCat["Item_Name"].ToString() + "\"  border=\"0\" title=\"" + drProdCat["Item_Name"].ToString() + "\" src=\"" + drProdCat["Product_Image"].ToString() + "\"/></a>");
   //                         strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
   //                         strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
   //                         strProdOutPut.Append("<span id=\"spnImg_" + drProdCat["Product_Id"].ToString() + "\" class=\"addCart\"><img id=\"add_" + drProdCat["Product_Id"].ToString() + "\" onClick=\"javascript:addToCartNew('spnImg_" + drProdCat["Product_Id"].ToString() + "', 'spnAjax_" + drProdCat["Product_Id"].ToString() + "', '" + drProdCat["Product_Id"].ToString() + "', '" + intCategoryId + "', 1);\" src=\"../images/mday_cartBtn.gif\" alt=\"Add to Cart\" title=\"Add to Cart\" /></span><span id=\"spnAjax_" + drProdCat["Product_Id"].ToString() + "\" style=\"display:none\" class=\"loadImg\"><img alt=\"Wait...\" src=\"Pictures/loading.gif\" width=\"16\" height=\"16\" />Wait...</span>");
   //                         strProdOutPut.Append("</li>");
   //                         intCount++;
   //                     }
   //                     else if (((intCount + 1) % 4) == 0)
   //                     {
   //                         strProdOutPut.Append("<li class=\"noMarg\">");

   //                         strProdOutPut.Append("<a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\"><img alt=\"" + drProdCat["Item_Name"].ToString() + "\"  border=\"0\" title=\"" + drProdCat["Item_Name"].ToString() + "\" src=\"" + drProdCat["Product_Image"].ToString() + "\"/></a>");
   //                         strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
   //                         strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
   //                         strProdOutPut.Append("<span id=\"spnImg_" + drProdCat["Product_Id"].ToString() + "\" class=\"addCart\"><img id=\"add_" + drProdCat["Product_Id"].ToString() + "\" onClick=\"javascript:addToCartNew('spnImg_" + drProdCat["Product_Id"].ToString() + "', 'spnAjax_" + drProdCat["Product_Id"].ToString() + "', '" + drProdCat["Product_Id"].ToString() + "', '" + intCategoryId + "', 1);\" src=\"../images/mday_cartBtn.gif\" alt=\"Add to Cart\" title=\"Add to Cart\" /></span><span id=\"spnAjax_" + drProdCat["Product_Id"].ToString() + "\" style=\"display:none\" class=\"loadImg\"><img alt=\"Wait...\" src=\"Pictures/loading.gif\" width=\"16\" height=\"16\" />Wait...</span>");
   //                         strProdOutPut.Append("</li>");
   //                         intCount = 0;
   //                     }
   //                     else
   //                     {
   //                         strProdOutPut.Append("<li>");
   //                         strProdOutPut.Append("<a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\"><img alt=\"" + drProdCat["Item_Name"].ToString() + "\"  border=\"0\" title=\"" + drProdCat["Item_Name"].ToString() + "\" src=\"" + drProdCat["Product_Image"].ToString() + "\"/></a>");
   //                         strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + drProdCat["Item_Name"].ToString() + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
   //                         strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
   //                         strProdOutPut.Append("<span id=\"spnImg_" + drProdCat["Product_Id"].ToString() + "\" class=\"addCart\"><img id=\"add_" + drProdCat["Product_Id"].ToString() + "\" onClick=\"javascript:addToCartNew('spnImg_" + drProdCat["Product_Id"].ToString() + "', 'spnAjax_" + drProdCat["Product_Id"].ToString() + "', '" + drProdCat["Product_Id"].ToString() + "', '" + intCategoryId + "', 1);\" src=\"../images/mday_cartBtn.gif\" alt=\"Add to Cart\" title=\"Add to Cart\" /></span><span id=\"spnAjax_" + drProdCat["Product_Id"].ToString() + "\" style=\"display:none\" class=\"loadImg\"><img alt=\"Wait...\" src=\"Pictures/loading.gif\" width=\"16\" height=\"16\" />Wait...</span>");
   //                         strProdOutPut.Append("</li>");
   //                         intCount++;
   //                     }
   //                     intTotalCount++;



   //                 }
   //                 strProdOutPut.Append("</ul>");
   //                 strOutPut = strProdOutPut.ToString();

   //                  Pagination logic starts
   //                 if (intPageNo == 0)
   //                 {
   //                     strPagination = "<li>" + intTotalProd + " of " + intTotalProd + "</li>";
   //                 }
   //                 else
   //                 {
   //                     if (intTotalProd > 25)
   //                     {
   //                         int intRowUpto = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(intTotalProd) / numberofProduct));
   //                         StringBuilder strReturnPagination = new StringBuilder();
   //                         StringBuilder sbToppage = new StringBuilder();
   //                         strReturnPagination.Append(intPageNo + " - " +  + " of " + intTotalProd + "&nbsp;&nbsp;");
   //                         if (intPageNo == 1)
   //                         {
   //                             if (intTotalProd < numberofProduct)
   //                             {
   //                                 sbToppage.Append("" + intPageNo + " - " + intTotalProd + " of " + intTotalProd + "");
   //                                  strReturnPagination.Append("<img class=\"catpaginbtn\" src=\"images/prev_arrow.gif\"alt=\"Previous\" border=\"0\" title=\"Previous\">");
   //                             }
   //                             else
   //                             {
   //                                 sbToppage.Append("" + intPageNo + " - " + numberofProduct + " of " + intTotalProd + "</li>");
   //                                  strReturnPagination.Append("<img class=\"catpaginbtn\" src=\"images/prev_arrow.gif\"alt=\"Previous\" border=\"0\" title=\"Previous\">");
   //                             }
   //                         }
   //                         else
   //                         {
   //                             if ((intPageNo * numberofProduct) > intTotalProd)
   //                             {
   //                                 sbToppage.Append("" + (((intPageNo - 1) * numberofProduct) + 1) + " - " + intTotalProd + " of " + intTotalProd + "");
   //                             }
   //                             else
   //                             {
   //                                 sbToppage.Append("" + (((intPageNo - 1) * numberofProduct) + 1) + " - " + (intPageNo * numberofProduct) + " of " + intTotalProd + "");
   //                             }
   //                             strReturnPagination.Append("<a href=\"" + strDomain + "/" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?r=" + rangeGiftsbyPrice + "&cat=" + categoryId + "&pageno=" + (intPageNo - 1) + "\" title=\"Page " + Convert.ToString(intPageNo - 1) + "\"><img src=\"images/prev_arrow.gif\"alt=\"Previous\" border=\"0\" title=\"Previous\"></a>");
   //                         }


   //                         int from = 1;

   //                         int to = 0;
   //                         if (intPageNo != 0)
   //                         {
   //                             to = (intPageNo - 1) + 10;
   //                             from = to - 19;
   //                             if (to > intRowUpto)
   //                             {
   //                                 to = intRowUpto;
   //                             }
   //                               to - from = 19;

   //                             if (from <= 0)
   //                             {
   //                                 from = 1;
   //                             }

   //                         }
   //                         for (int i = from; i <= to; i++)
   //                         {
   //                             if ((i) == intPageNo)
   //                             {
   //                                 strReturnPagination.Append("<span class=\"selectTxt\">" + (i) + "</span>");
   //                             }
   //                             else if (i == intRowUpto)
   //                             {
   //                                 strReturnPagination.Append("<li class=\"greySelect\"><strong>" + (i + 1) + "</strong></li>");
   //                             }
   //                             else
   //                             {
   //                                 strReturnPagination.Append("<a href=\"" + strDomain + "/" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?r=" + rangeGiftsbyPrice + "&cat=" + categoryId + "&pageno=" + (i) + "\" title=\"Page " + Convert.ToString(i) + "\">" + (i) + "</a>");
   //                             }
   //                         }
   //                         if (intPageNo == intRowUpto)
   //                         {
   //                               strReturnPagination.Append("<img class=\"catpaginbtn\" src=\"images/next_arrow.gif\" alt=\"Next\" border=\"0\" title=\"Next\">");
   //                         }
   //                         else
   //                         {
   //                             strReturnPagination.Append("<a href=\"" + strDomain + "/" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?r=" + rangeGiftsbyPrice + "&cat=" + categoryId + "&pageno=" + (intPageNo + 1) + "\" title=\"Next\"><img src=\"images/next_arrow.gif\" alt=\"Next\" border=\"0\" title=\"Next\"></a>");
   //                         }
   //                          strReturnPagination.Append("<li class=\"noMarg\"><a href=\"" + strDomain + "/" + System.IO.Path.GetFileName(HttpContext.Current.Request.CurrentExecutionFilePath.ToString()) + "?r=" + rangeGiftsbyPrice + "&cat=" + categoryId + "&pageno=0\"><img src=\"images/view_all_img.gif\" alt=\"View All\" border=\"0\" title=\"View All\"></a></li>");
   //                         strPagination = strReturnPagination.ToString();
   //                         strToppage = sbToppage.ToString();
   //                     }
   //                 }
   //             }
   //             else
   //             {
   //                 strError = "No product found for this category.";
   //                 blFlag = false;
   //             }
   //         }
   //         catch (SqlException ex)
   //         {
   //             strError = "Error!<br/>" + ex.Message;
   //             blFlag = false;
   //         }
   //         finally
   //         {
   //             if (_conn.State != ConnectionState.Closed)
   //             {
   //                 _conn.Close();
   //             }
   //         }
   //     }
   //     return blFlag;
   // }
   //#endregion

    public bool CheckAdminLogin(ref Login objRef)
    {
        bool flag = false;
        using (SqlConnection conn = new SqlConnection(_conn))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string strsql = "SELECT " +
                            _schema + ".user_master.Id AS [id], " +
                            _schema + ".user_master.Name AS [Name], " +
                            _schema + ".user_master.Type AS [Type] " +
                            "FROM " +
                            _schema + ".user_master " +
                            "WHERE " +
                            "(" + _schema + ".user_master.name='" + objRef.LoginUserName + "' AND " +
                            _schema + ".user_master.Password='" + objRef.LoginPassword + "') AND " +
                            _schema + ".user_master.recordStatus=" + 1 + "";
            try
            {
                using (SqlDataReader rdr = new SqlCommand(strsql, conn).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (rdr.HasRows)
                    {
                        flag = true;
                        if (rdr.Read())
                        {
                            objRef.LoginId = Convert.ToString(rdr["id"]);
                            objRef.LoginUserName = Convert.ToString(rdr["Name"]);
                            //objRef.LoginType = (UserType)Convert.ToInt32(rdr["Type"]);
                            objRef.LoginStatus = true;
                        }
                    }
                    else
                    {
                        objRef.LoginStatus = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
                //new MailOnExceptions().SendMail("Error : -" + ex.Message, "Error in CheckAdminLogin() in DataManipulationClass.");
            }
        }
        return flag;
    }

    internal string getCategoryIdOrUrl(short p)
    {
        throw new NotImplementedException();
    }


    //# region Search_Datatable_Duplictae_Removal  
    //protected bool GetProduct(int intSiteId, string strSearchProduct, string strSearchCategory, ref string strOutPut, ref string strProdError)
    //{
    //    bool blFlag = false;
    //    int intCurrencyId = Convert.ToInt32(Session["CurrencyId"]);
    //    double dblCurrencyValue = 1;
    //    string strCurrencySymbol = Convert.ToString(Session["CurrencySymbol"]);
    //    dynamicContent objDynContent = new dynamicContent();

    //    string lkw = objDynContent.getSiteMetaDetails(Convert.ToInt32(intSiteId))[1];
    //    StringBuilder strProdOutPut = new StringBuilder();
    //    if (objCommonFunction.CurrencyValue(intCurrencyId, ref dblCurrencyValue, ref strCurrencySymbol) == true)
    //    {
    //        if (strSearchCategory == "All")
    //        {
    //            strSql = "SELECT " +
    //                strSchema + ".SiteCatgory_Web_Server.Category_Id, " +
    //                strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +

    //                "" + strSchema + ".ItemMaster_Server.UrlName," +
    //                "" + strSchema + ".ItemCategory_Web_Server.RewriteUrlPath," +

    //                strSchema + ".ItemMaster_Server.Item_Name, " +
    //                "'" + strSitePath + "/ASP_Img/' +  " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
    //                strSchema + ".ItemMaster_Server.Item_Price, " +
    //                strSchema + ".SiteCatgory_Web_Server.Category_Id " +
    //                "FROM " + strSchema + ".SiteCatgory_Web_Server " +
    //                "INNER JOIN " + strSchema + ".ItemCategoryRelation_Web_Server " +
    //                "ON (" + strSchema + ".SiteCatgory_Web_Server.Category_Id=" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id) " +
    //                "INNER JOIN " + strSchema + ".ItemCategory_Web_Server " +
    //                "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id=" + strSchema + ".ItemCategory_Web_Server.Category_Id) " +
    //                "INNER JOIN " + strSchema + ".ItemMaster_Server " +
    //                "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
    //                "WHERE ((" + strSchema + ".SiteCatgory_Web_Server.Site_Id='" + intSiteId + "') " +
    //                "AND(" + strSchema + ".SiteCatgory_Web_Server.Record_Status='1') " +
    //                "AND(" + strSchema + ".ItemMaster_Server.Record_Status='1') " +
    //                "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
    //                "AND (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0') " +
    //                "AND(" + strSchema + ".ItemMaster_Server.Item_Name LIKE '%" + strSearchProduct.Replace("'", "''") + "%')" +
    //                " AND(" + strSchema + ".ItemMaster_Server.Record_Status='1'))" +
    //                "ORDER BY " + strSchema + ".ItemMaster_Server.Item_Price DESC;";
    //        }
    //        else
    //        {
    //            strSql = "SELECT " +
    //                    strSchema + ".SiteCatgory_Web_Server.Category_Id, " +
    //                    strSchema + ".ItemCategoryRelation_Web_Server.Product_Id, " +
    //                    strSchema + ".ItemMaster_Server.Item_Name, " +

    //                    "" + strSchema + ".ItemMaster_Server.UrlName," +
    //                    "" + strSchema + ".ItemCategory_Web_Server.RewriteUrlPath," +

    //                    "'" + strSitePath + "/ASP_Img/' +  " + strSchema + ".ItemMaster_Server.Product_Id + '.jpg' AS 'Product_Image', " +
    //                    strSchema + ".ItemMaster_Server.Item_Price, " +
    //                    strSchema + ".SiteCatgory_Web_Server.Category_Id " +
    //                //strSchema + ".ItemCategory_Web_Server.Category_Lavel, " + 
    //                //strSchema + ".ItemCategory_Web_Server.Category_ParentId, " +
    //                //strSchema + ".ItemCategory_Web_Server.Category_Page, " +
    //                //strSchema + ".ItemCategory_Web_Server.Category_Name " +
    //                    "FROM " + strSchema + ".SiteCatgory_Web_Server " +
    //                    "INNER JOIN " + strSchema + ".ItemCategoryRelation_Web_Server " +
    //                    "ON (" + strSchema + ".SiteCatgory_Web_Server.Category_Id=" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id) " +
    //                    "INNER JOIN " + strSchema + ".ItemCategory_Web_Server " +
    //                    "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Category_Id=" + strSchema + ".ItemCategory_Web_Server.Category_Id) " +
    //                    "INNER JOIN " + strSchema + ".ItemMaster_Server " +
    //                    "ON (" + strSchema + ".ItemCategoryRelation_Web_Server.Product_Id=" + strSchema + ".ItemMaster_Server.Product_Id) " +
    //                    "WHERE ((" + strSchema + ".SiteCatgory_Web_Server.Site_Id='" + intSiteId + "') " +
    //                    "AND(" + strSchema + ".SiteCatgory_Web_Server.Record_Status='1') " +
    //                    "AND(" + strSchema + ".ItemCategoryRelation_Web_Server.Record_Status='1') " +
    //                    "AND(" + strSchema + ".ItemMaster_Server.Record_Status='1') " +
    //                    "AND (" + strSchema + ".ItemMaster_Server.Item_colour IS NULL OR " + strSchema + ".ItemMaster_Server.Item_colour='0') " +
    //                    "AND(" + strSchema + ".ItemMaster_Server.Item_Name LIKE '%" + strSearchProduct.Replace("'", "''") + "%') " +
    //                    "AND(" + strSchema + ".ItemCategory_Web_Server.Category_Name LIKE '%" + strSearchCategory + "%')" +
    //                    " AND(" + strSchema + ".ItemMaster_Server.Record_Status='1'))" +
    //                    "ORDER BY " + strSchema + ".ItemMaster_Server.Item_Price DESC;";
    //        }
    //        try
    //        {
    //            DataTable dtAllProduct = new DataTable();
    //            SqlDataAdapter daProdCat = new SqlDataAdapter(strSql, conn);
    //            daProdCat.Fill(dtAllProduct);
    //            if (dtAllProduct.Rows.Count > 0)
    //            {
    //                ArrayList arrExistProd = new ArrayList();
    //                string strCurrProdId = "";
    //                StringBuilder strRow1 = new StringBuilder();
    //                StringBuilder strRow2 = new StringBuilder();
    //                int intCount = 0;
    //                int intTotalRowCount = 0;
    //                blFlag = true;
    //                //////
    //                string strLink = "";
    //                //////
    //                strProdOutPut.Append("<ul class=\"category\">");
    //                foreach (DataRow drProdCat in dtAllProduct.Rows)
    //                {
    //                    strCurrProdId = Convert.ToString(drProdCat["Product_Id"].ToString());
    //                    strLink = strDomain + Convert.ToString(drProdCat["RewriteUrlPath"]) + Convert.ToString(drProdCat["UrlName"]) + _extensionToShow;
    //                    bool blFlagExist = false;
    //                    for (int i = 0; i < arrExistProd.Count; i++)
    //                    {
    //                        if (arrExistProd[i].ToString() == strCurrProdId)
    //                        {
    //                            blFlagExist = true;
    //                        }
    //                    }
    //                    if (blFlagExist == false)
    //                    {
    //                        arrExistProd.Add(strCurrProdId);
    //                        string strProdPrice = "Rs." + drProdCat["Item_Price"].ToString() + " / " + strCurrencySymbol.ToString() + Convert.ToDouble(double.Parse(drProdCat["Item_Price"].ToString()) / dblCurrencyValue).ToString("0.00");
    //                        int intCategoryId = Convert.ToInt32(drProdCat["Category_Id"].ToString());
    //                        string strTitle = "Send " + drProdCat["Item_Name"].ToString() + " " + lkw + "";
    //                        if (intCount == 0)
    //                        {
    //                            strProdOutPut.Append("<li>");
    //                            strProdOutPut.Append("<a href=\"" + strLink + "\"><img alt=\"" + strTitle + "\" title=\"" + strTitle + "\" border=\"0\" src=\"" + drProdCat["Product_Image"].ToString() + "\" /></a>");
    //                            strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + strTitle + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
    //                            strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
    //                            strProdOutPut.Append("</li>");
    //                            intCount++;
    //                        }
    //                        else if ((intCount + 1) % 4 == 0)
    //                        {
    //                            strProdOutPut.Append("<li class=\"noPadd2\">");
    //                            strProdOutPut.Append("<a href=\"" + strLink + "\"><img alt=\"" + strTitle + "\" title=\"" + strTitle + "\" border=\"0\" src=\"" + drProdCat["Product_Image"].ToString() + "\" /></a>");
    //                            strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + strTitle + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
    //                            strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
    //                            strProdOutPut.Append("</li>");
    //                            intCount++;

    //                        }
    //                        else
    //                        {
    //                            strProdOutPut.Append("<li>");
    //                            strProdOutPut.Append("<a href=\"" + strLink + "\"><img alt=\"" + strTitle + "\" title=\"" + strTitle + "\" border=\"0\" src=\"" + drProdCat["Product_Image"].ToString() + "\" /></a>");
    //                            strProdOutPut.Append("<span class=\"catNam\"><a href=\"" + strLink + "\" title=\"" + strTitle + "\">" + drProdCat["Item_Name"].ToString() + "</a></span>");
    //                            strProdOutPut.Append("<span><div title=\"" + drProdCat["Item_Price"].ToString() + "\" id=\"ProductPrice\" align=\"center\">" + strProdPrice + "</div></span>");
    //                            strProdOutPut.Append("</li>");
    //                            intCount++;
    //                        }
    //                        if (intTotalRowCount == dtAllProduct.Rows.Count)
    //                        {
    //                            strProdOutPut.Append("</ul>");
    //                        }
    //                        intTotalRowCount++;
    //                    }
    //                    else
    //                    {
    //                        intTotalRowCount++;
    //                    }
    //                }
    //                if (intTotalRowCount == dtAllProduct.Rows.Count)
    //                {
    //                    strProdOutPut.Append("</ul>");
    //                }
    //                strOutPut = strProdOutPut.ToString();
    //            }
    //            else
    //            {
    //                strProdError = "Sorry! No match found for this search string... Try again";
    //                blFlag = false;
    //            }
    //        }
    //        catch (SqlException ex)
    //        {
    //            strProdError = "Error!<br/>" + ex.Message;
    //            blFlag = false;
    //        }
    //    }
    //    return blFlag;
    //}
    //#endregion
}

