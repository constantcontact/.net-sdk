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
        /// Get the URL to obtain an access token.
        /// </summary>
        /// <param name="code">Code returned from Constant Contact after a user has granted access to their account.</param>
        /// <returns>Returns an URL string.</returns>
        string GetAccessTokenUrl(string code);
        /// <summary>
        /// Get the URL at which the user can authenticate and authorize the requesting application.
        /// </summary>
        /// <param name="server">Whether or not to use OAuth2 server flow, alternative is client flow.</param>
        /// <returns>Returns the authorization URL.</returns>
        string GetAuthorizationUrl(bool server);
    }
}
