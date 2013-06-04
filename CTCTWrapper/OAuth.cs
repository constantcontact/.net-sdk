using System;
using System.Runtime.Serialization;
using System.Web;
using CTCT.Authentication.WebPage;

namespace CTCT
{
    /// <summary>
    /// Main class meant to be used by users to obtain access token by authenticating their app
    /// </summary>
    public static class OAuth
    {
        /// <summary>
        /// Returns access token obtained after authenticating client app
        /// </summary>
        /// <param name="state">state query parameter</param>
        /// <returns>string representing access token if authentication succeded, null otherwise</returns>
        public static string AuthenticateFromWinProgram(ref string state){
            var authform = new AuthenticationForm {State = state};

            authform.ShowDialog();
            state = authform.State; 

            return authform.AccessToken;
        }

        /// <summary>
        /// Sends an authorization request to Constant Contact API
        /// (if access to application is granted, a code is send to Redirect URL field)
        /// (Redirect URL is one of web application url pages/methods)
        /// </summary>
        /// <param name="httpContext">current application context</param>
        /// <param name="state">state query parameter</param>
        public static void AuthorizeFromWebApplication(HttpContext httpContext, string state)
        {
             var webForm = new AuthenticationWebPage(httpContext, state);
             webForm.GetAuthorizationCode();
        }

        /// <summary>
        /// Request access token for an app key, client secret and authorization code
        /// </summary>
        /// <param name="httpContext">current application context</param>
        /// <param name="code">authorization code</param>
        /// <returns>access token</returns>
        public static string GetAccessTokenByCode(HttpContext httpContext, string code)
        {
            var webForm = new AuthenticationWebPage(httpContext, null);
            return webForm.GetAccessTokenByCode(code);
        }
    }

    /// <summary>
    /// AuthenticationRequest class.
    /// </summary>
    [DataContract]
    [Serializable]
    class AuthenticationRequest
    {
        /// <summary>
        /// Activity id.
        /// </summary>
        [DataMember(Name = "access_token", EmitDefaultValue = false)]
        public string AccessToken { get; set; }

        /// <summary>
        /// Activity id.
        /// </summary>
        [DataMember(Name = "expires_in", EmitDefaultValue = false)]
        public string ExpiresIn { get; set; }

        /// <summary>
        /// Activity id.
        /// </summary>
        [DataMember(Name = "token_type", EmitDefaultValue = false)]
        public string TokenType { get; set; }
    }
}
