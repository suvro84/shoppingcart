<%@ Application Language="C#" %>

<script RunAt="server">
      # region giftbhejo
    //void Application_BeginRequest(Object sender, EventArgs e)
    //{
    //String strCurrentPath = System.IO.Path.GetFileName(HttpContext.Current.Request.Path.ToString());
    //    if (System.IO.Path.GetExtension(strCurrentPath) == Convert.ToString(ConfigurationManager.AppSettings["extToShow"]))
    //    {
    //        string[] strSplittedPath = System.Web.HttpContext.Current.Request.RawUrl.ToString().Split(new char[] { '?' });
    //        if (!System.IO.File.Exists(Server.MapPath(strCurrentPath)))
    //        {
    //            Context.RewritePath("category.aspx?" + strSplittedPath[strSplittedPath.Length - 1]);
    //        }
    //    }
     //}
      #endregion
    
    //gti global

    # region gti global
    //void Application_BeginRequest(Object sender, EventArgs e)
    //{
    //    String strCustomPath = "";
    //    String strCurrentPath = System.IO.Path.GetFileName(HttpContext.Current.Request.Path.ToString());
    //    //clsUrlRewriter objclsUrlRewriter = new clsUrlRewriter();
    //    urlRewrite objUrlRewriter = new urlRewrite(HttpContext.Current.Request.Path.ToString());
    //    if (System.IO.Path.GetExtension(strCurrentPath) == Convert.ToString(ConfigurationManager.AppSettings["extToShow"]))
    //    {
    //        if ((strCurrentPath != "default.aspx") && (strCurrentPath != "Default.aspx"))
    //        {
    //            //strCustomPath = objclsUrlRewriter.getReWritePath(Convert.ToString(strCurrentPath));
    //            strCustomPath = objUrlRewriter.rewritePath();
    //            if (strCustomPath != "")
    //            {
    //                //HttpContext.Current.RewritePath(strCustomPath);
    //                Context.RewritePath(strCustomPath);
    //                //HttpContext.Current.Response.Redirect("~/test.aspx?u=" + strCustomPath);
    //            }
    //        }
    //    }
    //}
    #endregion
    # region series global

    //void Application_BeginRequest(Object sender, EventArgs e)
    //{
    //    //String strCurrentPath1;
    //    //strCurrentPath1 = Request.Path.ToString().ToLower();

    //    Gti24x7_CommonFunction objCommonFunction = new Gti24x7_CommonFunction();
    //    String strCustomPath = "";
    //    string originalpath = "";
    //    String strCurrentPath = System.IO.Path.GetFileName(HttpContext.Current.Request.Path.ToString());
    //    string[] pages = Request.Path.ToString().Split(new char[] { '/' });
    //    string static_pages = "";
    //    string item_pagewith_dot_aspx = pages[pages.Length - 1];
    //    string[] withaspx = item_pagewith_dot_aspx.Split(new char[] { '.' });
    //    string page_number = withaspx[0];
    //    string month = "";
    //    string endday = "";

    //    string strSchema = Convert.ToString(ConfigurationManager.AppSettings["SCHEMA"]);
    //    if (strCurrentPath.IndexOf("TestiMonials_Add.aspx") != -1)
    //    {
    //        Context.RewritePath("~/TestiMonials_Add.aspx");
    //        return;
    //    }


    //    if (HttpContext.Current.Request.Path.ToString().IndexOf("Testimonials") != -1)
    //    {
    //        if (HttpContext.Current.Request.Path.ToString().IndexOf(".html") != -1)
    //        {
    //            if (new Gti24x7_CommonFunction().IsMonth(page_number))
    //            {
    //                int year = Convert.ToInt32(pages[pages.Length - 2]);
    //                new clsUrlRewriter().monthWiseEndDay(page_number, year, ref month, ref endday);
    //                Context.RewritePath("~/giftsTestiMonials.aspx?SearchFrom=" + year + "-" + month + "-" + "01&SearchTo=" + year + "-" + month + "-" + endday);
    //            }
    //            else
    //            {
    //                if (objCommonFunction.IsNumeric(page_number))
    //                {
    //                    Context.RewritePath("~/giftsTestiMonials.aspx");
    //                    return;
    //                }
    //                else if (objCommonFunction.IsNumeric(pages[3]))
    //                {
    //                    Context.RewritePath("~/giftsTestiMonials.aspx?Id=" + Convert.ToString(pages[3]) + "");
    //                }
    //            }
    //        }
    //    }

    //    string strDomain = Convert.ToString(ConfigurationManager.AppSettings["Domain"].ToString());

    //    //if (strCurrentPath.IndexOf("default.aspx") != -1)
    //    //{

    //    //    Context.RewritePath("~/default.html");
    //    //    return;
    //    //}
    //    if (strCurrentPath.IndexOf("frmAjaxCart.aspx") != -1)
    //    {

    //        Context.RewritePath("~/frmAjaxCart.aspx");
    //        return;
    //    }
    //    if (HttpContext.Current.Request.Path.ToString().IndexOf("default.html") != -1)
    //    {
    //        Context.RewritePath("~/default.aspx");
    //        return;
    //    }

    //       if (strCurrentPath.IndexOf("ajaxSubmitForm.aspx") != -1)
    //    {
    //        Context.RewritePath("~/ajaxSubmitForm.aspx");
    //        return;
    //    }


    // //   if (HttpContext.Current.Request.Path.ToString().IndexOf("SearchFound.html") != -1)
    //        if (strCurrentPath.IndexOf("SearchFound.html") != -1)
    //    {
    //        Context.RewritePath("~/SearchFound.aspx");
    //        return;
    //    }
    //    //if (strCurrentPath.IndexOf("default.aspx") != -1)
    //    //{
    //    //    Context.RewritePath("~/SearchFound.aspx");
    //    //    return;
    //    //}

    //    if (strCurrentPath.IndexOf("cart.html") != -1)
    //    {
    //        Context.RewritePath("~/cart.aspx");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("terms.html") != -1)
    //    {
    //        Context.RewritePath("~/terms.aspx");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("contactus.html") != -1)
    //    {
    //        Context.RewritePath("~/contactus.aspx");
    //        return;
    //    }
    //    if (objCommonFunction.IsNumeric(page_number) == true || page_number.ToLower() == "all")
    //    {
    //        if (HttpContext.Current.Request.Path.ToString().IndexOf("UpToRs500") != -1)
    //        {
    //            Context.RewritePath("~/GiftsByPrice.aspx?r=1&pageno=" + page_number + "");
    //            return;
    //        }
    //        if (HttpContext.Current.Request.Path.ToString().IndexOf("UpToRs1000") != -1)
    //        {
    //            Context.RewritePath("~/GiftsByPrice.aspx?r=2&pageno=" + page_number + "");
    //            return;
    //        }

    //        if (HttpContext.Current.Request.Path.ToString().IndexOf("UpToRs2000") != -1)
    //        {
    //            Context.RewritePath("~/GiftsByPrice.aspx?r=3&pageno=" + page_number + "");
    //            return;
    //        }
    //        if (HttpContext.Current.Request.Path.ToString().IndexOf("Rs2000AndAbove") != -1)
    //        {
    //            Context.RewritePath("~/GiftsByPrice.aspx?r=4&pageno=" + page_number + "");
    //            return;
    //        }
    //    }

    //    if (HttpContext.Current.Request.Path.ToString().IndexOf("weekly_bestseller") != -1)
    //   // if (strCurrentPath.IndexOf("weekly_bestseller/1.html") != -1)
    //    {

    //        Context.RewritePath("~/weekly_bestseller.aspx?pageno=" +page_number+"");
    //        return;
    //    }
    //    if (HttpContext.Current.Request.Path.ToString().IndexOf("yesterday_bestseller") != -1)

    //    {

    //        Context.RewritePath("~/yesterday_bestseller.aspx?pageno=" + page_number + "");
    //        return;
    //    }
    //    if (HttpContext.Current.Request.Path.ToString().IndexOf("fortnight_bestseller") != -1)

    //    {

    //        Context.RewritePath("~/fortnight_bestseller.aspx?pageno=" + page_number + "");
    //        return;
    //    }
    //    if (HttpContext.Current.Request.Path.ToString().IndexOf("month_bestseller") != -1)

    //    {

    //        Context.RewritePath("~/month_bestseller.aspx?pageno=" + page_number + "");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("ajaxCartFunction.aspx") != -1)
    //    {

    //        Context.RewritePath("~/ajaxCartFunction.aspx");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("ajaxCommonFunction.aspx") != -1)
    //    {

    //        Context.RewritePath("~/ajaxCommonFunction.aspx");
    //        return;
    //    }
    //    //if (strCurrentPath.IndexOf("cart.aspx") != -1)
    //    //{

    //    //    Context.RewritePath("cart.aspx");
    //    //    return;
    //    //}
    //    if (strCurrentPath.IndexOf("frmOption.aspx") != -1)
    //    {

    //        Context.RewritePath("frmOption.aspx");
    //        return;
    //    }

    //    if (strCurrentPath.IndexOf("category.aspx") != -1)
    //    {

    //        Context.RewritePath("~/category.aspx");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("GiftsByPrice.aspx") != -1)
    //    {

    //        Context.RewritePath("~/GiftsByPrice.aspx");
    //        return;
    //    }

    //    if (strCurrentPath.IndexOf("item.aspx") != -1)
    //    {

    //        Context.RewritePath("~/item.aspx");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("jsCommonFunction.js") != -1)
    //    {

    //        Context.RewritePath("jsCommonFunction.js");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("AjaxHttpRequest.js") != -1)
    //    {

    //        Context.RewritePath("AjaxHttpRequest.js");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("jsCartFunction_ab.js") != -1)
    //    {

    //        Context.RewritePath("jsCartFunction_ab.js");
    //        return;
    //    }



    //    if (strCurrentPath.IndexOf("demo.aspx") != -1)
    //    {

    //        Context.RewritePath("demo.aspx");
    //        return;
    //    }
    //    if (strCurrentPath.IndexOf("gifts.aspx") != -1)
    //    {

    //        Context.RewritePath("~/gifts.aspx");
    //        return;
    //    }
    //    //if (strCurrentPath.IndexOf("index.aspx") != -1)
    //    //{

    //    //    Context.RewritePath("~/index.aspx");
    //    //    return;
    //    //}
    //    clsUrlRewriter objclsUrlRewriter = new clsUrlRewriter();
    //    //string all = "all";
    //    try
    //    {
    //        if (objCommonFunction.IsNumeric(page_number) == true || page_number.ToLower() == "all")
    //        {

    //            if (strCurrentPath != "default.html")
    //            {

    //                //originalpath = Convert.ToString(Request.Path);
    //                strCustomPath = objclsUrlRewriter.getReWritePath(Convert.ToString(Request.Path));
    //                if (strCustomPath == "")
    //                {
    //                    //Context.RewritePath("~/index.aspx");
    //                    //Context.RewritePath("../test.aspx?path=" + strCustomPath);
    //                }
    //                else
    //                {
    //                    Context.RewritePath(strCustomPath);
    //                  //  Context.RewritePath("../test.aspx?path=" + strCustomPath);
    //                    // Context.RewritePath("../index.aspx?path=" + strCustomPath);
    //                    //string pagepath = strCustomPath.Replace(".html", ".aspx");
    //                    //Response.TransmitFile(pagepath);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write("Error: " + ex.Message);

    //    }


    //    try
    //    {

    //        originalpath = Convert.ToString(Request.Path);
    //        if (objCommonFunction.IsNumeric(page_number) == false)
    //        {
    //            String strCustomItemPath = objclsUrlRewriter.getItem_ReWritePath(originalpath);
    //            if (strCustomItemPath != "")
    //            {
    //                Context.RewritePath(strCustomItemPath);
    //                // Context.RewritePath("../../test1.aspx?path=" + strCustomItemPath);
    //            }
    //            else
    //            {
    //                //Context.RewritePath("index.aspx");
    //            }
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        Response.Write("Error: " + ex.Message);

    //    }

    #endregion
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        int SiteId = 1;

    }



    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        System.Data.DataTable dtitem = new System.Data.DataTable();
        dtitem.Columns.Add("recid", typeof(int));
        //dtitem.PrimaryKey = new System.Data.DataColumn[] { dtitem.Columns["recid"] };

        dtitem.Columns["recid"].AutoIncrement = true;
        //dtitem.Columns["recid"].AutoIncrementStep = 1;
        dtitem.Columns["recid"].AutoIncrementSeed = 1;
        dtitem.PrimaryKey = new System.Data.DataColumn[] { dtitem.Columns["recid"] };

        dtitem.Columns.Add("item_id", typeof(int));
        dtitem.Columns.Add("pid", typeof(string));
        dtitem.Columns.Add("cid", typeof(int));
        dtitem.Columns.Add("image_name", typeof(string));
        dtitem.Columns.Add("item_name", typeof(string));
        dtitem.Columns.Add("item_cost", typeof(double));
        dtitem.Columns.Add("qty", typeof(int));
        dtitem.Columns.Add("tot_cost", typeof(double));
        dtitem.Columns.Add("disc", typeof(bool));
        dtitem.Columns.Add("disc_code", typeof(string));
        dtitem.Columns.Add("disc_amt", typeof(double));
        //System.Data.DataRow dr = dtitem.NewRow();
        //dtitem.Rows.Add(dr);
        Session["dtitem"] = dtitem;
        Session["flagDiscount"] = false;

        //Session["qty"] = 1;
        Session["SiteID"] = 1;
        Session["bflag"] = false;
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session["flagDiscount"] = false;
        Session["dtitem"] = "";
        Session["f"] = null;
        Session["t"] = null;
    }
       
</script>

