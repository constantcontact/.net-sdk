using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Util;
using System.Web;
using CTCT.Exceptions;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;

namespace CTCT.Auth
{
    /// <summary>
    /// Class that implements necessary functionality to obtain an access token from a user.
    /// </summary>
    public class CtctOAuth2 : ICtctOAuth2
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public CtctOAuth2() { }

        /// <summary>
        /// Get the URL at which the user can authenticate and authorize the requesting application.
        /// </summary>
        /// <param name="server">Whether or not to use OAuth2 server flow, alternative is client flow.</param>
        /// <returns>Returns the authorization URL.</returns>
        public string GetAuthorizationUrl(bool server)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Config.Auth.BaseUrl);
            sb.Append(Config.Auth.AuthorizationEndpoint);
            sb.AppendFormat("?response_type={0}&", (server) ? Config.Auth.ResponseTypeCode : Config.Auth.ResponseTypeToken);
            sb.AppendFormat("client_id={0}&", ConfigurationManager.AppSettings["APIKey"]);
            sb.AppendFormat("redirect_uri={0}", HttpUtility.UrlEncode(ConfigurationManager.AppSettings["RedirectURL"]));

            return HttpUtility.UrlPathEncode(sb.ToString());
        }

        /// <summary>
        /// Request the access token.
        /// </summary>
        /// <returns>Returns the access token.</returns>
        public string GetAccessToken()
        {
            string token = null;
            string responseText;
            CookieContainer cc = new CookieContainer();

            try
            {
                // Access the authorization URL
                string url = this.GetAuthorizationUrl(false);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = WebRequestMethods.Http.Get;
                request.CookieContainer = cc;
                request.UserAgent = Config.HeaderUserAgent;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseText = reader.ReadToEnd();
                }
                // Construct the login URL
                StringBuilder sb = new StringBuilder();
                sb.Append(Config.Login.BaseUrl);
                sb.Append("?reauth=false");
                sb.AppendFormat("&gotoUrl={0}", HttpUtility.UrlEncode(String.Concat(Config.Auth.BaseUrl, Config.Login.LoginEndpoint, "?response_type=", Config.Auth.ResponseTypeToken)));
                sb.Append("&cookiesEnabled=true");
                sb.AppendFormat("&luser={0}&lpass={1}", ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]);
                sb.Append("&_remuser=on&_save=");
                url = sb.ToString();
                // POST request to login URL
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = WebRequestMethods.Http.Post;
                request.CookieContainer = cc;
                request.Accept = Config.HeaderAccept;
                request.Host = Config.Login.Host;
                request.UserAgent = Config.HeaderUserAgent;
                request.ContentType = Config.HeaderContentType;
                // Get the response
                response = request.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseText = reader.ReadToEnd();
                }
                // Grant access to redirect URL
                sb.Clear();
                sb.Append(Config.Auth.BaseUrl);
                sb.Append(Config.Auth.AuthorizationEndpoint);
                sb.Append("?user_oauth_approval=true&preregistered_redirect_uri=");
                sb.Append(HttpUtility.UrlEncode(ConfigurationManager.AppSettings["RedirectURL"]));
                url = sb.ToString();
                // Place request
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = WebRequestMethods.Http.Post;
                request.CookieContainer = cc;
                request.Accept = Config.HeaderAccept;
                request.Host = Config.Auth.Host;
                request.UserAgent = Config.HeaderUserAgent;
                request.ContentType = Config.HeaderContentType;
                // Read the response
                response = request.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseText = reader.ReadToEnd();
                }
                // Get the token from response redirect Uri
                NameValueCollection query = HttpUtility.ParseQueryString(response.ResponseUri.AbsoluteUri);
                foreach (string key in query.AllKeys)
                {
                    if (key.Contains("access_token"))
                    {
                        token = query[key];
                        break;
                    }
                }
            }
            catch { }

            return token;
        }

        /// <summary>
        /// Get the URL to obtain an access token.
        /// </summary>
        /// <param name="code">Code returned from Constant Contact after a user has granted access to their account.</param>
        /// <returns>Returns an URL string.</returns>
        public string GetAccessTokenUrl(string code)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Config.Auth.BaseUrl);
            sb.Append(Config.Auth.TokenEndpoint);
            sb.AppendFormat("?grant_type={0}&", Config.Auth.AuthorizationCodeGrantType);
            sb.AppendFormat("client_id={0}&", ConfigurationManager.AppSettings["APIKey"]);
            sb.AppendFormat("client_secret={0}&", ConfigurationManager.AppSettings["ConsumerSecret"]);
            sb.AppendFormat("code={0}&", code);
            sb.AppendFormat("redirect_uri={0}", ConfigurationManager.AppSettings["RedirectURL"]);

            return HttpUtility.UrlPathEncode(sb.ToString());
        }
    }
}
