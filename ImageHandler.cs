using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace BuildFusion.AssetTracker.Web.Common
{
    public class ImageHandler : IHttpHandler 
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string relativePath = context.Request.AppRelativeCurrentExecutionFilePath;
            string absolutePath = context.Server.MapPath(relativePath);
            try
            {
                
                FileInfo info = new FileInfo(absolutePath);
                if (info.Exists)
                {
                    context.Response.Cache.SetCacheability(HttpCacheability.Public);
                    context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));

                    SetContentType(relativePath, context);
                    SetConditionalGetHeaders(info.CreationTimeUtc);

                    context.Response.TransmitFile(absolutePath);
                }
                else
                {
                    //Nothing to do
                }
            }
            catch
            { 
                //Transmit file normally
                context.Response.TransmitFile(absolutePath);
            }
        }

        private void SetContentType(string fileName, HttpContext context)
        {
            int index = fileName.LastIndexOf(".") + 1;
            string extension = fileName.Substring(index).ToUpperInvariant();

            // IE FIX
            if (string.Compare(extension, "JPG") == 0)
                context.Response.ContentType = "image/jpeg";
            else
                context.Response.ContentType = "image/" + extension;
        }

        public static void SetConditionalGetHeaders(DateTime date)
        {
            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;

            if (date > DateTime.Now)
                date = DateTime.Now;
            string etag = "\"" + date.Ticks + "\"";
            string incomingEtag = request.Headers["If-None-Match"];

            response.Cache.SetETag(etag);
            response.Cache.SetLastModified(date);

            if (String.Compare(incomingEtag, etag) == 0)
            {
                response.Clear();
                response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                response.End();
            }
        }

        #endregion
    }
}
