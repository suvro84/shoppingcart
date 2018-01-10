using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Xml;

namespace BuildFusion.AssetTracker.Web.Common
{
    /// <summary>
    /// Summary description for Optimizer
    /// </summary>
    public class ScriptOptimizer : IHttpHandler
    {
        // Usage : BFResourse.axd?type=JS&setname=setname&urlset=a,b,c
        private const int DAYS_IN_CACHE = 30;

        public ScriptOptimizer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string root = context.Request.Url.GetLeftPart(UriPartial.Authority);
            string type = context.Request.QueryString["type"];
            string setname = context.Request.QueryString["setname"];
            string urlset = context.Request.QueryString["urlset"];

            byte[] encodedBytes;
            if (type.ToUpper().Equals("JS"))
            {
                encodedBytes = this.GetJSResponse(setname, urlset);
            }
            else if (type.ToUpper().Equals("CSS"))
            {
                encodedBytes = this.GetCSSResponse(setname, urlset);
            }

            //string path = context.Request.QueryString["type"];
            //string content = string.Empty;
            //List<string> localFiles = new List<string>();

            //if (!string.IsNullOrEmpty(path))
            //{
            //    string[] scripts = path.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (string script in scripts)
            //    {
            //        // We only want to serve resource files for security reasons.
            //        if (script.Contains("Resource.axd") || script.Contains("asmx/js") || script.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            //            content += RetrieveRemoteScript(root + script) + Environment.NewLine;
            //        else
            //            content += RetrieveLocalScript(script, localFiles) + Environment.NewLine;
            //    }

            //    content = StripWhitespace(content);
            //}

            //if (!string.IsNullOrEmpty(content))
            //{
            //    context.Response.Write(content);
            //    SetHeaders(context, localFiles.ToArray());

            //    WebHelper.Compress(context);
            //}
        }

        private byte[] GetJSResponse(string setname, string urls)
        {
            byte[] encodedBytes = null;
            UrlMapSet set = CombineScripts.LoadSets().Single<UrlMapSet>(s => s.Name == setname && s.Type.ToUpper().Equals("JS"));
            
            return encodedBytes;
        }

        private byte[] GetCSSResponse(string setname, string urls)
        {
            byte[] encodedBytes = null;
            UrlMapSet set = CombineScripts.LoadSets().Single<UrlMapSet>(s => s.Name == setname && s.Type.ToUpper().Equals("CSS"));
            return encodedBytes;
        }

        internal static List<UrlMapSet> LoadSets()
        {
            List<UrlMapSet> sets = new List<UrlMapSet>();

            using (XmlReader reader = new XmlTextReader(new StreamReader(HttpContext.Current.Server.MapPath("~/Admin/FileSets.xml"))))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if ("set" == reader.Name)
                    {
                        string setName = reader.GetAttribute("name");
                        string setType = reader.GetAttribute("type");
                        string isIncludeAll = reader.GetAttribute("includeAll");

                        UrlMapSet mapSet = new UrlMapSet();
                        mapSet.Name = setName;
                        mapSet.Type = setType;
                        if (isIncludeAll == "true")
                            mapSet.IsIncludeAll = true;

                        while (reader.Read())
                        {
                            if ("url" == reader.Name)
                            {
                                string urlName = reader.GetAttribute("name");
                                string url = reader.ReadElementContentAsString();
                                mapSet.Urls.Add(new UrlMap(urlName, url));
                            }
                            else if ("set" == reader.Name)
                                break;
                        }

                        sets.Add(mapSet);
                    }
                }
            }

            return sets;
        }

        /// <summary>
        /// Retrieves the local script from the disk
        /// </summary>
        private static string RetrieveLocalScript(string file, List<string> localFiles)
        {
            if (!file.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
            {
                throw new System.Security.SecurityException("No access");
            }

            string path = HttpContext.Current.Server.MapPath(file);
            string script = null;

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    script = reader.ReadToEnd();
                }

                localFiles.Add(path);
            }

            return script;
        }

        /// <summary>
        /// Retrieves the specified remote script using a WebClient.
        /// </summary>
        /// <param name="file">The remote URL</param>
        private static string RetrieveRemoteScript(string file)
        {
            string script = null;

            try
            {
                Uri url = new Uri(file, UriKind.Absolute);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    script = reader.ReadToEnd();
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                // The remote site is currently down. Try again next time.
            }
            catch (UriFormatException)
            {
                // Only valid absolute URLs are accepted
            }

            return script;
        }

        /// <summary>
        /// Strips the whitespace from any .js file.
        /// </summary>
        private static string StripWhitespace(string body)
        {
            string[] lines = body.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder emptyLines = new StringBuilder();
            foreach (string line in lines)
            {
                string s = line.Trim();
                if (s.Length > 0 && !s.StartsWith("//"))
                    emptyLines.AppendLine(s.Trim());
            }

            body = emptyLines.ToString();

            // remove C styles comments
            body = Regex.Replace(body, "/\\*.*?\\*/", String.Empty, RegexOptions.Compiled | RegexOptions.Singleline);
            //// trim left
            body = Regex.Replace(body, "^\\s*", String.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
            //// trim right
            body = Regex.Replace(body, "\\s*[\\r\\n]", "\r\n", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of left curly braced
            body = Regex.Replace(body, "\\s*{\\s*", "{", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of coma
            body = Regex.Replace(body, "\\s*,\\s*", ",", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of semicolon
            body = Regex.Replace(body, "\\s*;\\s*", ";", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove newline after keywords
            body = Regex.Replace(body, "\\r\\n(?<=\\b(abstract|boolean|break|byte|case|catch|char|class|const|continue|default|delete|do|double|else|extends|false|final|finally|float|for|function|goto|if|implements|import|in|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|super|switch|synchronized|this|throw|throws|transient|true|try|typeof|var|void|while|with)\\r\\n)", " ", RegexOptions.Compiled | RegexOptions.ECMAScript);

            return body;
        }

        /// <summary>
        /// This will make the browser and server keep the output
        /// in its cache and thereby improve performance.
        /// </summary>
        private static void SetHeaders(HttpContext context, string[] files)
        {
            //return;
            context.Response.ContentType = "text/javascript";
            context.Response.AddFileDependencies(files);
            context.Response.Cache.VaryByParams["path"] = true;
            context.Response.Cache.SetETagFromFileDependencies();
            context.Response.Cache.SetLastModifiedFromFileDependencies();
            context.Response.Cache.SetValidUntilExpires(true);
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(DAYS_IN_CACHE));
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
        }


        #endregion
    }
}