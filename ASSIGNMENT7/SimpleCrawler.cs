using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WinFormsCrawler
{
    class SimpleCrawler
    {
        private Hashtable urls = new Hashtable();
        private int count = 1;
        private string rootUrl;
        private string startUrl;
        public bool noFinish { get; set; }
        public StringBuilder right;
        public StringBuilder wrong;
        public  SimpleCrawler(string starturl)
        {
            right = new StringBuilder();
            wrong = new StringBuilder();
            startUrl = starturl;
            urls.Add(startUrl, false);//加入初始页面
            rootUrl = GetRootUrl(startUrl);
            noFinish = true;
            new Thread(Crawl).Start();
        }

        private void Crawl()
        {
            string current = startUrl;
            string html = DownLoad(current); // 下载
            urls[current] = true;
            Parse(html);
            while (true)
            {
                foreach (string url in urls.Keys)
                {
                    if (!(bool)urls[url])
                    {
                        current = url;
                        break;
                    }
                }
                count++;
                if (current == null ) break;
                DownLoad(current);
                urls[current] = true;
                current = null;
            }
            noFinish = false;
        }
        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                right.Append(url);
                right.Append(" success!");
                return html;
            }
            catch (Exception ex)
            {
                wrong.Append(url + "wrong");
                return "";
            }
        }

        private void Parse(string html)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (IsUrlLegal(strRef))
                {
                    string completeUrl = GetCompleteUrl(strRef);
                    if (urls[completeUrl] == null) urls[completeUrl] = false;
                }
            }
        }

        private bool IsUrlLegal(string url)
        {
            // 判断URL是否合法
            string[] legalExtensions = { ".htm", ".html", ".aspx", ".php", ".jsp" };
            if (!url.StartsWith("http")) return false;
            string extension = Path.GetExtension(url);
            if (string.IsNullOrEmpty(extension)) return false;
            if (Array.IndexOf(legalExtensions, extension.ToLower()) == -1) return false;
            if (!url.StartsWith(rootUrl)) return false;
            return true;
        }

        private string GetCompleteUrl(string url)
        {
            // 将相对地址转换为绝对地址
            Uri baseUri = new Uri(rootUrl);
            Uri completeUri = new Uri(baseUri, url);
            return completeUri.AbsoluteUri;
        }

        public static string GetRootUrl(string url)
        {
            Uri uri = new Uri(url);
            return uri.GetLeftPart(UriPartial.Authority);

        }

    }
}