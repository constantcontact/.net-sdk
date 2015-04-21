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
        /// Campaign tracking service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public CampaignTrackingService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for bounces created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        public ResultSet<BounceActivity> GetBounces(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            return GetBounces(campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        public ResultSet<BounceActivity> GetBounces(Pagination pag)
        {
            return GetBounces(null, null, null, pag);
        }

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for bounces created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        private ResultSet<BounceActivity> GetBounces(string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.CampaignTrackingBounces, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<BounceActivity>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get clicks for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetClicks(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            string url = ConstructUrl(Settings.Endpoints.Default.CampaignTrackingClicks, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) });
            return GetClicks(url);
        }

        /// <summary>
        /// Get clicks for a specific link in a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="linkId">Specifies the link in the email campaign to retrieve click data for.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetClicks(string campaignId, string linkId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            string url = ConstructUrl(Settings.Endpoints.Default.CampaignTrackingClicksForLink, new object[] { campaignId, linkId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) });
            return GetClicks(url);
        }

        /// <summary>
        /// Get clicks for the provided Pagination page.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetClicks(Pagination pag)
        {
            return GetClicks(pag.GetNextUrl());
        }

        /// <summary>
        /// Get ClickActivity at the given url.
        /// </summary>
        /// <param name="url">The url from which to retrieve an array of @link ClickActivity.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        private ResultSet<ClickActivity> GetClicks(string url)
        {
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<ClickActivity>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            return GetForwards(campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(DateTime? createdSince, Pagination pag)
        {
            return GetForwards(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        private ResultSet<ForwardActivity> GetForwards(string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.CampaignTrackingForwards, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<ForwardActivity>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            return GetOpens(campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(DateTime? createdSince, Pagination pag)
        {
            return GetOpens(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        private ResultSet<OpenActivity> GetOpens(string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.CampaignTrackingOpens, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<OpenActivity>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        public ResultSet<SendActivity> GetSends(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            return GetSends(campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        public ResultSet<SendActivity> GetSends(DateTime? createdSince, Pagination pag)
        {
            return GetSends(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        private ResultSet<SendActivity> GetSends(string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.CampaignTrackingSends, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<SendActivity>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            return GetOptOuts(campaignId, limit, createdSince, null);
        }

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(DateTime? createdSince, Pagination pag)
        {
            return GetOptOuts(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        private ResultSet<OptOutActivity> GetOptOuts(string campaignId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.CampaignTrackingUnsubscribes, new object[] { campaignId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<OptOutActivity>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get a summary of reporting data for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Tracking summary.</returns>
        public TrackingSummary GetSummary(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.CampaignTrackingOrId);
            }

            string url = ConstructUrl(Settings.Endpoints.Default.CampaignTrackingSummary, new object[] { campaignId }, null);
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var summary = response.Get<TrackingSummary>();
                return summary;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }
    }
}
