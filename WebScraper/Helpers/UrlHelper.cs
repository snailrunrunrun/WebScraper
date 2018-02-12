using System;
using System.Net;

namespace WebScraper.Helpers
{
    public class UrlHelper
    {
        public static string GetWikiUrl(string uri)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = @"en.wikipedia.org",
                Path = "wiki/" + uri
            };
            return uriBuilder.ToString();
        }


        /// <summary>
        /// Verify provided url.
        /// </summary>
        /// <param name="url">The url to validate.</param>
        /// <returns>True if http status is 200 OK.</returns>
        public static bool IsValidUrl(string url)
        {
            var myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            using (var myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
            {
                return myHttpWebResponse.StatusCode == HttpStatusCode.OK;
            }
        }
    }
}