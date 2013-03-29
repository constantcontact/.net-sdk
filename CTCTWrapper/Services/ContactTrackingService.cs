using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components;
using CTCT.Components.Tracking;
using CTCT.Util;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to Contact Tracking.
    /// </summary>
    public class ContactTrackingService : BaseService, IContactTrackingService
    {
        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        public ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, string contactId, int? limit)
        {
            return GetBounces(accessToken, apiKey, contactId, limit, null);
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        public ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, Pagination pag)
        {
            return GetBounces(accessToken, apiKey, null, null, pag);
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        private ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, string contactId, int? limit, Pagination pag)
        {
            ResultSet<BounceActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.ContactTrackingBounces, new object[] { contactId }, new object[] { "limit", limit }) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = Component.FromJSON<ResultSet<BounceActivity>>(response.Body);
            }

            return results;
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        public ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, string contactId, int? limit)
        {
            return GetClicks(accessToken, apiKey, contactId, limit, null);
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        public ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, Pagination pag)
        {
            return GetClicks(accessToken, apiKey, null, null, pag);
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        private ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, string contactId, int? limit, Pagination pag)
        {
            ResultSet<ClickActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.ContactTrackingClicks, new object[] { contactId }, new object[] { "limit", limit }) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = Component.FromJSON<ResultSet<ClickActivity>>(response.Body);
            }

            return results;
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, string contactId, int? limit)
        {
            return GetForwards(accessToken, apiKey, contactId, limit, null);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, Pagination pag)
        {
            return GetForwards(accessToken, apiKey, null, null, pag);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        private ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, string contactId, int? limit, Pagination pag)
        {
            ResultSet<ForwardActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.ContactTrackingForwards, new object[] { contactId }, new object[] { "limit", limit }) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = Component.FromJSON<ResultSet<ForwardActivity>>(response.Body);
            }

            return results;
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, string contactId, int? limit)
        {
            return GetOpens(accessToken, apiKey, contactId, limit, null);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, Pagination pag)
        {
            return GetOpens(accessToken, apiKey, null, null, pag);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        private ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, string contactId, int? limit, Pagination pag)
        {
            ResultSet<OpenActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.ContactTrackingOpens, new object[] { contactId }, new object[] { "limit", limit }) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = Component.FromJSON<ResultSet<OpenActivity>>(response.Body);
            }

            return results;
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        public ResultSet<SendActivity> GetSends(string accessToken, string apiKey, string contactId, int? limit)
        {
            return GetSends(accessToken, apiKey, contactId, limit, null);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        public ResultSet<SendActivity> GetSends(string accessToken, string apiKey, Pagination pag)
        {
            return GetSends(accessToken, apiKey, null, null, pag);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        private ResultSet<SendActivity> GetSends(string accessToken, string apiKey, string contactId, int? limit, Pagination pag)
        {
            ResultSet<SendActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.ContactTrackingSends, new object[] { contactId }, new object[] { "limit", limit }) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = Component.FromJSON<ResultSet<SendActivity>>(response.Body);
            }

            return results;
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, string contactId, int? limit)
        {
            return GetOptOuts(accessToken, apiKey, contactId, limit, null);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, Pagination pag)
        {
            return GetOptOuts(accessToken, apiKey, null, null, pag);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        private ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, string contactId, int? limit, Pagination pag)
        {
            ResultSet<OptOutActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.ContactTrackingUnsubscribes, new object[] { contactId }, new object[] { "limit", limit }) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = Component.FromJSON<ResultSet<OptOutActivity>>(response.Body);
            }

            return results;
        }

        /// <summary>
        /// Get a summary of reporting data for a given contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Tracking summary.</returns>
        public TrackingSummary GetSummary(string accessToken, string apiKey, string contactId)
        {
            TrackingSummary summary = null;
            string url = Config.ConstructUrl(Config.Endpoints.ContactTrackingSummary, new object[] { contactId }, null);
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                summary = Component.FromJSON<TrackingSummary>(response.Body);
            }

            return summary;
        }
    }
}
