using System;
namespace CTCT.Auth
{
    /// <summary>
    /// Interface for OAuth2 class.
    /// </summary>
    public interface ICtctOAuth2
    {
        /// <summary>
        /// Request the access token.
        /// </summary>
        /// <returns>Returns the access token.</returns>
        string GetAccessToken();
        /// <summary>
        /// Request the access token based on provided login
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="APIKey">API Key value</param>
        /// <param name="redirectURL">redirect URL</param>
        /// <returns>Returns the access token</returns>
        string GetAccessToken(string username, string password, string APIKey, string redirectURL);
        /// <summary>
        /// Get the URL at which the user can authenticate and authorize the requesting application.
        /// </summary>
        /// <param name="server">Whether or not to use OAuth2 server flow, alternative is client flow.</param>
        /// <param name="APIKey">If not null, use this APIKey, otherwise use value from configuration file</param>
        /// <param name="redirectURL">If not null, use this URL, otherwise use value from configuration file</param>
        /// <returns>Returns the authorization URL.</returns>
        string GetAuthorizationUrl(bool server, string APIKey, string redirectURL);
    }
}
