using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Gti24x7_CommonFunction
/// </summary>
public class Gti24x7_CommonFunction
{
    public Gti24x7_CommonFunction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    internal int getStateIdByName(string p)
    {
        throw new NotImplementedException();
    }

    internal int getCountryIdByName(string p)
    {
        throw new NotImplementedException();
    }

    internal bool SendMail(string strMailTo, string p, string p_3, string p_4, bool p_5, string strSubject, System.Net.Mail.MailPriority mailPriority, int p_8, bool p_9, string p_10, string p_11, ref string strError)
    {
        throw new NotImplementedException();
    }

    public bool CurrencyValue(int intCurrencyId, ref double dblCurrencyValue, ref string strCurrencySymbol)
    {
        return true;
    }




    internal DateTime convertCurrency(double dblRowPrice, int intCurrencyId)
    {
        throw new NotImplementedException();
    }

    internal string ReturnRemainString(string p, int p_2)
    {
        throw new NotImplementedException();
    }

    //public bool SendMail(string strSiteMailId, string p, string strRecipient, string strBody, bool p_5, string strSubject, System.Net.Mail.MailPriority mailPriority, int p_8, bool p_9, string p_10, string p_11, ref string strError)
    //{
    //    throw new NotImplementedException();
    //}

    //public bool SendMail(string strSiteMailId, string p, string strRecipient, string strBody, bool p_5, string strSubject, System.Net.Mail.MailPriority mailPriority, int p_8, bool p_9, string p_10, string p_11, ref string strError)
    //{
    //    throw new NotImplementedException();
    //}

    public bool IsNumeric(string strSiteId)
    {
        throw new NotImplementedException();
    }

    public bool returnSiteName(int p, ref string strSiteName)
    {
        throw new NotImplementedException();
    }

    public string FetchCurrencyName(int p)
    {
        throw new NotImplementedException();
    }

    ////public string ReturnRemainString(string p, int p_2)
    ////{
    ////    throw new NotImplementedException();
    ////}

    public bool getMailFomat(int p, string OrderId, bool ComboProduct, ref string strSiteMailId, ref string strRecipient, ref string strFrom, ref string strSenderName, ref string strSubject, ref string strBody, ref string strError)
    {
        throw new NotImplementedException();
    }

    //public string ReturnRemainString(string p, int p_2)
    //{
    //    throw new NotImplementedException();
    //}


}
