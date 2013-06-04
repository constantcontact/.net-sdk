using System;
using CTCT.Components;
using CTCT.Components.Tracking;
using CTCT.Util;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to Constant Contact Campaign Tracking.
    /// </summary>
    public class CampaignTrackingService : BaseService, ICampaignTrackingService
    {
        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        public ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, string campaignId, int? limit)
        {
            return GetBounces(accessToken, apiKey, campaignId, limit, null);
        }

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        public ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, Pagination pag)
        {
            return GetBounces(accessToken, apiKey, null, null, pag);
        }

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        private ResultSet<BounceActivity> GetBounces(string accessToken, string apiKey, string campaignId, int? limit, Pagination pag)
        {
            ResultSet<BounceActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.CampaignTrackingBounces, new object[] { campaignId }, new object[] { "limit", limit }) : pag.GetNextUrl();
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
        /// Get clicks for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince)
        {
            string url = Config.ConstructUrl(Config.Endpoints.CampaignTrackingClicks, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) });
            return GetClicks(accessToken, apiKey, url);
        }

        /// <summary>
        /// Get clicks for a specific link in a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="linkId">Specifies the link in the email campaign to retrieve click data for.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, string campaignId, string linkId, int? limit, DateTime? createdSince)
        {
            string url = Config.ConstructUrl(Config.Endpoints.CampaignTrackingClicksForLink, new object[] { campaignId, linkId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) });
            return GetClicks(accessToken, apiKey, url);
        }

        /// <summary>
        /// Get clicks for the provided Pagination page.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, DateTime? createdSince, Pagination pag)
        {
            return GetClicks(accessToken, apiKey, pag.GetNextUrl());
        }

        /// <summary>
        /// Get ClickActivity at the given url.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="url">The url from which to retrieve an array of @link ClickActivity.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        private ResultSet<ClickActivity> GetClicks(string accessToken, string apiKey, string url)
        {
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            ResultSet<ClickActivity> results = null;
            if (response.HasData)
            {
                results = Component.FromJSON<ResultSet<ClickActivity>>(response.Body);
            }

            return results;
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince)
        {
            return GetForwards(accessToken, apiKey, campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, DateTime? createdSince, Pagination pag)
        {
            return GetForwards(accessToken, apiKey, null, null, createdSince, pag);
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        private ResultSet<ForwardActivity> GetForwards(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            ResultSet<ForwardActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.CampaignTrackingForwards, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince)
        {
            return GetOpens(accessToken, apiKey, campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, DateTime? createdSince, Pagination pag)
        {
            return GetOpens(accessToken, apiKey, null, null, createdSince, pag);
        }

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        private ResultSet<OpenActivity> GetOpens(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            ResultSet<OpenActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.CampaignTrackingOpens, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        public ResultSet<SendActivity> GetSends(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince)
        {
            return GetSends(accessToken, apiKey, campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        public ResultSet<SendActivity> GetSends(string accessToken, string apiKey, DateTime? createdSince, Pagination pag)
        {
            return GetSends(accessToken, apiKey, null, null, createdSince, pag);
        }

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        private ResultSet<SendActivity> GetSends(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            ResultSet<SendActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.CampaignTrackingSends, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince)
        {
            return GetOptOuts(accessToken, apiKey, campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, DateTime? createdSince, Pagination pag)
        {
            return GetOptOuts(accessToken, apiKey, null, null, createdSince, pag);
        }

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        private ResultSet<OptOutActivity> GetOptOuts(string accessToken, string apiKey, string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            ResultSet<OptOutActivity> results = null;
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.CampaignTrackingUnsubscribes, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get a summary of reporting data for a given campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Tracking summary.</returns>
        public TrackingSummary GetSummary(string accessToken, string apiKey, string campaignId)
        {
            TrackingSummary summary = null;
            string url = Config.ConstructUrl(Config.Endpoints.CampaignTrackingSummary, new object[] { campaignId }, null);
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
