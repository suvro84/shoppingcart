using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for NewColletionofClasses
/// </summary>
public class NewColletionofClasses
{
}

// -----------------------------------------------------------------------------------------
// Don't change the codes of the given Class and Interface.
// - GCommon<T>
// - ICommonInterface
// -----------------------------------------------------------------------------------------
public class GCommon<T> : Collection<T> where T : ICommonInterface
{
    public void Add(T item)
    {
        base.Add(item);
    }
    public void Clear(T item)
    {
        base.Clear();
    }
}
public interface ICommonInterface
{
}
// -----------------------------------------------------------------------------------------


//public class Login : ICommonInterface
//{
//    public string LoginId
//    {
//        get { return _id; }
//        set { _id = value; }
//    }
//    public Login()
//    {
//        _status = false;
//    }

//    public string LoginEmail
//    {
//        get { return _email; }
//        set { _email = value; }
//    }
//    public string LoginPassword
//    {
//        get { return _pass; }
//        set { _pass = value; }
//    }
//    public bool LoginStatus
//    {
//        get { return _status; }
//        set { _status = value; }
//    }
//    public string LoginScreenName
//    {
//        get { return _screenname; }
//        set { _screenname = value; }
//    }
//    public UserType LoginType
//    {
//        get { return _type; }
//        set { _type = value; }
//    }

//    private string _id;
//    private string _email;
//    private string _pass;
//    private bool _status;
//    private string _screenname;
//    private UserType _type;
//}
public class UserRegistration : ICommonInterface
{
    private string _id;
    private string _UserName;
    private string _password;
    private int _type;
    private string _fname;
    private string _Lname;
    private string _email;
    private string _email1;
    private string _SMTPServer;
    private string _SMTP_Port;
    private string _SMTP_UserName;
    private string _SMTP_Password;
    private string _DisplayName;
    private bool _SSL;

    public UserRegistration()
    {
        //_type = UserType.OnlineUsers;
    }
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }
    public int UserType
    {
        get { return _type; }
        set { _type = value; }
    }
    public string FirstName
    {
        get { return _fname; }
        set { _fname = value; }
    }
    public string LastName
    {
        get { return _Lname; }
        set { _Lname = value; }
    }
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    public string Email1
    {
        get { return _email1; }
        set { _email1 = value; }
    }

    public string SMTPServer
    {
        get { return _SMTPServer; }
        set { _SMTPServer = value; }
    }

    public string SMTP_Port
    {
        get { return _SMTP_Port; }
        set { _SMTP_Port = value; }
    }

    public string SMTP_UserName
    {
        get { return _SMTP_UserName; }
        set { _SMTP_UserName = value; }
    }
    public string SMTP_Password
    {
        get { return _SMTP_Password; }
        set { _SMTP_Password = value; }
    }

    public string DisplayName
    {
        get { return _DisplayName; }
        set { _DisplayName = value; }
    }

    public bool SSL
    {
        get { return _SSL; }
        set { _SSL = value; }
    }



}


public class WebSiteDetail : ICommonInterface
{
    private int _id;
    private string _Name;
    private string _Description;
    private string _SiteURL;
    private int _UserId;
    private int _AdId;
    private string _SMTPServer;
    private string _SMTP_Port;
    private string _SMTP_UserName;

    private string _SMTP_Password;
    private string _ImgLogoPath;
    private string _DisplayName;
    private string _signature;
    private bool _SSL;
    public WebSiteDetail()
    {
        //_type = UserType.OnlineUsers;
    }
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }
    public string SiteURL
    {
        get { return _SiteURL; }
        set { _SiteURL = value; }
    }
    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }
    public int AdId
    {
        get { return _AdId; }
        set { _AdId = value; }
    }
    public string SMTPServer
    {
        get { return _SMTPServer; }
        set { _SMTPServer = value; }
    }

    public string SMTP_Port
    {
        get { return _SMTP_Port; }
        set { _SMTP_Port = value; }
    }

    public string SMTP_UserName
    {
        get { return _SMTP_UserName; }
        set { _SMTP_UserName = value; }
    }
    public string SMTP_Password
    {
        get { return _SMTP_Password; }
        set { _SMTP_Password = value; }
    }
    public string ImgLogoPath
    {
        get { return _ImgLogoPath; }
        set { _ImgLogoPath = value; }
    }
    public string DisplayName
    {
        get { return _DisplayName; }
        set { _DisplayName = value; }
    }
    public string Signature
    {
        get { return _signature; }
        set { _signature = value; }
    }
    public bool SSL
    {
        get { return _SSL; }
        set { _SSL = value; }
    }
}



public class LinkDetail : ICommonInterface
{
    private int _id;
    private string _heading;
    private string _description;
    private string _siteURL;
    private int _userId;
    private int _webSiteId;





    public LinkDetail()
    {
        //_type = UserType.OnlineUsers;
    }
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string Heading
    {
        get { return _heading; }
        set { _heading = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    public string SiteURL
    {
        get { return _siteURL; }
        set { _siteURL = value; }
    }
    public int UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }
    public int WebSiteId
    {
        get { return _webSiteId; }
        set { _webSiteId = value; }
    }






}


public class EmailTemplate : ICommonInterface
{
    private int _templateID;
    private string _templateName;
    private string _templateSubject;
    private string _templateCode;





    public EmailTemplate()
    {
        //_type = UserType.OnlineUsers;
    }
    public int TemplateID
    {
        get { return _templateID; }
        set { _templateID = value; }
    }
    public string TemplateName
    {
        get { return _templateName; }
        set { _templateName = value; }
    }
    public string TemplateSubject
    {
        get { return _templateSubject; }
        set { _templateSubject = value; }
    }
    public string TemplateCode
    {
        get { return _templateCode; }
        set { _templateCode = value; }
    }







}

public class Login : ICommonInterface
{
    private string _id;
    private string _AddressId;
    private string _screenname;
    private string _username;
    private string _pass;
    //private UserType _type;
    private bool _status;

    public Login()
    {
        _status = false;
    }

    public string LoginId
    {
        get { return _id; }
        set { _id = value; }
    }
    public string AddressId
    {
        get { return _AddressId; }
        set { _AddressId = value; }
    }
    public string LoginScreenName
    {
        get { return _screenname; }
        set { _screenname = value; }
    }

    public string LoginUserName
    {
        get { return _username; }
        set { _username = value; }
    }
    public string LoginPassword
    {
        get { return _pass; }
        set { _pass = value; }
    }
    ////public UserType LoginType
    ////{
    ////    get { return _type; }
    ////    set { _type = value; }
    ////}
    public bool LoginStatus
    {
        get { return _status; }
        set { _status = value; }
    }
}


public class LoginCooky : ICommonInterface
{
    private string _id;
  
    private string _loginUserName;
    private string _loginPassword;
    private UserType _loginType;
    private bool _status;

    public LoginCooky()
    {
        _status = false;
    }

    public string LoginId
    {
        get { return _id; }
        set { _id = value; }
    }
    //public string AddressId
    //{
    //    get { return _AddressId; }
    //    set { _AddressId = value; }
    //}
    //public string LoginScreenName
    //{
    //    get { return _screenname; }
    //    set { _screenname = value; }
    //}

    public string LoginUserName
    {
        get { return _loginUserName; }
        set { _loginUserName = value; }
    }
    public string LoginPassword
    {
        get { return _loginPassword; }
        set { _loginPassword = value; }
    }
    public UserType LoginType
    {
        get { return _loginType; }
        set { _loginType = value; }
    }
    public bool LoginStatus
    {
        get { return _status; }
        set { _status = value; }
    }
}

public class SubPage : ICommonInterface
{
    private int _id;
    private int _webSiteId;
    private string _subPageName;
    private string _linkURL;
    private int _pageRank;
    private int _status;
    private string _codeinpage;
    private string _pageDescription;
    private string _othercode;





    public SubPage()
    {

    }
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public int WebSiteId
    {
        get { return _webSiteId; }
        set { _webSiteId = value; }
    }
    public string SubPageName
    {
        get { return _subPageName; }
        set { _subPageName = value; }
    }
    public string LinkURL
    {
        get { return _linkURL; }
        set { _linkURL = value; }
    }
    public int PageRank
    {
        get { return _pageRank; }
        set { _pageRank = value; }
    }
    public int Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public string CODEINPAGE
    {
        get { return _codeinpage; }
        set { _codeinpage = value; }
    }
    public string PageDescription
    {
        get { return _pageDescription; }
        set { _pageDescription = value; }
    }
    public string OTHERCODE
    {
        get { return _othercode; }
        set { _othercode = value; }
    }






}


public class EmailCorrespondence : ICommonInterface
{
    private int _id;
    private int _linkid;
    private string _subject;
    private string _displayName;
    private string _fromEmail;
    private string _toEmail;
    private string _body;
    //private DateTime DateTime;
    private int _templateId;




    public EmailCorrespondence()
    {
        //_type = UserType.OnlineUsers;
    }
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public int Linkid
    {
        get { return _linkid; }
        set { _linkid = value; }
    }
    public string Subject
    {
        get { return _subject; }
        set { _subject = value; }
    }
    public string Body
    {
        get { return _body; }
        set { _body = value; }
    }
    public int TemplateId
    {
        get { return _templateId; }
        set { _templateId = value; }
    }

    public string DisplayName
    {
        get { return _displayName; }
        set { _displayName = value; }
    }
    public string FromEmail
    {
        get { return _fromEmail; }
        set { _fromEmail = value; }
    }
    public string ToEmail
    {
        get { return _toEmail; }
        set { _toEmail = value; }
    }



}

public class LinkExchange : ICommonInterface
{
    private int _id;
    private int _subPageId;
    private int _ourAdId;
    private int _status;
    private int _webSiteId;
    private int _type;
    private string _email;
    private string _HTMLcode;
    private string _reciprocal;
    private string _pageRank;
    private string _from_url;
    private string _fName;
    private string _lName;
    private int _pageid;

    public LinkExchange()
    {
        //_type = UserType.OnlineUsers;
    }
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public int SubPageId
    {
        get { return _subPageId; }
        set { _subPageId = value; }
    }
    public int OurAdId
    {
        get { return _ourAdId; }
        set { _ourAdId = value; }
    }
    public int Status
    {
        get { return _status; }
        set { _status = value; }
    }
    public int WebSiteId
    {
        get { return _webSiteId; }
        set { _webSiteId = value; }
    }
    public int Type
    {
        get { return _type; }
        set { _type = value; }
    }
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Reciprocal
    {
        get { return _reciprocal; }
        set { _reciprocal = value; }
    }

    public string HTMLcode
    {
        get { return _HTMLcode; }
        set { _HTMLcode = value; }
    }


    public string PageRank
    {
        get { return _pageRank; }
        set { _pageRank = value; }
    }
    public string From_url
    {
        get { return _from_url; }
        set { _from_url = value; }
    }
    public string FName
    {
        get { return _fName; }
        set { _fName = value; }
    }
    public string LName
    {
        get { return _lName; }
        set { _lName = value; }
    }
    public int Pageid
    {
        get { return _pageid; }
        set { _pageid = value; }
    }
}
