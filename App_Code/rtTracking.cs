using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for rtTracking
/// </summary>
public class rtTracking : IDisposable
{
    public rtTracking()
    {
        _dt = DateTime.Now;
        _source = string.Empty;
        _key = string.Empty;
        _reff = string.Empty;
        _lands = string.Empty;


        _ip = string.Empty;
        _pid = string.Empty;
        _res = string.Empty;
        _bro = string.Empty;
        _os = string.Empty;

    }

    public string IP
    {
        get { return _ip; }
        set { _ip = value; }
    }

    public string ProjectId
    {
        get { return _pid; }
        set { _pid = value; }
    }

    public string Resolution
    {
        get { return _res; }
        set { _res = value; }
    }

    public string Camefrom
    {
        get { return _reff; }
        set
        {
            _reff = value;
            _reff = _reff.Replace("&", "&amp;");
        }
    }

    public string Landson
    {
        get { return _lands; }
        set
        {
            _lands = value;
            _lands = _lands.Replace("&", "&amp;");
        }
    }

    public string Browser
    {
        get { return _bro; }
        set { _bro = value; }
    }

    public string OperatingSystem
    {
        get { return _os; }
        set { _os = value; }
    }

    public DateTime LandingDateTime
    {
        get { return _dt; }
    }

    public string Source
    {
        // Source : -
        // Google.
        // Yahoo.
        // MSN.
        // GMAIL.
        // AdWords.
        // Bing.

        get
        {
            if (_lands.IndexOf("?gclid") > -1)
            {
                _source = "Google-AdWords";
            }
            else if (_reff != string.Empty && _reff != null)
            {
                // Check Source name in Refferer_URL
                if (_reff.IndexOf("www.google") > -1)
                {
                    _source = "Google";
                }
                else if (_reff.IndexOf("mail.yahoo.co") > -1)
                {
                    _source = "Yahoo Mail";
                }
                else if (_reff.IndexOf("yahoo") > -1)
                {
                    _source = "Yahoo";
                }
                else if (_reff.IndexOf("msn") > -1)
                {
                    _source = "MSN";
                }
                else if (_reff.IndexOf("mail.google") > -1)
                {
                    _source = "Gmail";
                }
                else if (_reff.IndexOf("www.bing") > -1)
                {
                    _source = "Bing";
                }
                else
                {
                    _source = "Others";
                }
            }
            else
            {
                _source = "Others";
            }
            return _source;
        }
        set { _source = value; }
    }

    public string KeyWords
    {
        get
        {
            if (_source != string.Empty)
            {
                // Check Keyword of Refferer URL
                switch (_source)
                {
                    case "Google-AdWords":
                        Get_GoogleKeyWords();
                        break;

                    case "Google":
                        Get_GoogleKeyWords();
                        break;

                    case "Yahoo":
                        Get_Yahoo_KeyWords();
                        break;

                    case "MSN":
                        Get_MSN_KeyWords();
                        break;

                    case "Bing":
                        Get_BingKeyWords();
                        break;
                }
            }
            return _key;
        }
    }

    private void Get_BingKeyWords()
    {
        if (_reff != string.Empty)
        {
            _reff = _reff.Replace("&amp;", "&");
            string[] s;
            s = _reff.Split(new char[] { '&' });
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains("search"))
                {
                    if (s[i].IndexOf("?q=") >= 0)
                    {
                        _key = s[i].Substring(s[i].IndexOf("?q=") + 3).Replace("+", " ");
                        break;
                    }
                }
                if (s[i].IndexOf("q=") >= 0)
                {
                    _key = s[i].Substring(s[i].IndexOf("q=") + 2).Replace("+", " ");
                    break;
                }
            }
        }
    }

    private void Get_GoogleKeyWords()
    {
        if (_reff != string.Empty)
        {
            string[] s;
            s = _reff.Split(new char[] { '&' });
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].StartsWith("amp;q="))
                {
                    _key = s[i].Replace("amp;q=", "").Replace("+", " ");
                    break;
                }
                if (s[i].Contains("search"))
                {
                    if (s[i].IndexOf("?q=") >= 0)
                    {
                        _key = s[i].Substring(s[i].IndexOf("?q=") + 3).Replace("+", " ");
                        break;
                    }
                }
                if (s[i].IndexOf("?q=") >= 0)
                {
                    _key = s[i].Substring(s[i].IndexOf("?q=") + 3).Replace("+", " ");
                    break;
                }
            }
        }

        //return _key;
    }

    private void Get_MSN_KeyWords()
    {
        if (_reff != string.Empty)
        {
            string[] s;
            s = _reff.Split(new char[] { '?' });
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains("q="))
                {
                    string[] temp;
                    temp = s[i].Split(new char[] { '&' });
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (temp[j].Contains("q="))
                        {
                            _key = temp[j].Replace("q=", "");
                            break;
                        }
                    }
                    break;
                }
            }
        }

        //return _key;
    }

    private void Get_Yahoo_KeyWords()
    {
        if (_reff != string.Empty)
        {
            string[] s;
            s = _reff.Split(new char[] { '?' });
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains("p="))
                {
                    string[] temp;
                    temp = s[i].Split(new char[] { '&' });
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (temp[j].Contains("p="))
                        {
                            _key = temp[j].Replace("p=", "");
                            break;
                        }
                    }
                    break;
                }
            }
        }

        //return _key;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    private string _ip;
    private string _pid;
    private string _res;
    private string _reff;
    private string _lands;
    private string _bro;
    private string _os;
    private DateTime _dt;
    private string _source;
    private string _key;
}
