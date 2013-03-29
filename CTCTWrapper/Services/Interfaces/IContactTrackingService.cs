using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components;
using CTCT.Components.Tracking;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for ContactTrackingService class.
    /// </summary>
    public interface IContactTrackingService
    {
        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, string contactId, int? limit);

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, Pagination pag);

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, string contactId, int? limit);

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, Pagination pag);

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, string contactId, int? limit);

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, Pagination pag);

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, string contactId, int? limit);

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, Pagination pag);

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        ResultSet<SendActivity> GetSends(string accessToken, string apiKey, string contactId, int? limit);

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        ResultSet<SendActivity> GetSends(string accessToken, string apiKey, Pagination pag);

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, string contactId, int? limit);

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, Pagination pag);

        /// <summary>
        /// Get a summary of reporting data for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Tracking summary.</returns>
        TrackingSummary GetSummary(string accessToken, string apiKey, string contactId);
    }
}
