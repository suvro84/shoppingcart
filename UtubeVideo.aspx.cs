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
using System.Collections.ObjectModel;

public partial class UtubeVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Video newVideo = new Video();
       
        //newVideo.Title = "My Test Movie";
        //newVideo.Tags.Add(new MediaCategory("Autos", YouTubeNameTable.CategorySchema));
        //newVideo.Keywords = "cars, funny";
        //newVideo.Description = "My description";
        //newVideo.YouTubeEntry.Private = false;
        //newVideo.Tags.Add(new MediaCategory("mydevtag, anotherdevtag",
        //  YouTubeNameTable.DeveloperTagSchema));

        //newVideo.YouTubeEntry.Location = new GeoRssWhere(37, -122);
        //// alternatively, you could just specify a descriptive string
        //// newVideo.YouTubeEntry.setYouTubeExtension("location", "Mountain View, CA");

        //FormUploadToken token = request.CreateFormUploadToken(newVideo);
        //Console.WriteLine(token.Url);
        //Console.WriteLine(token.Token);
    }
}
