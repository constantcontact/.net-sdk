using System;
using CTCT.Components;
using CTCT.Components.Tracking;
using CTCT.Util;
using CTCT.Exceptions;
using System.Collections.Generic;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to Contact Tracking.
    /// </summary>
    public class ContactTrackingService : BaseService, IContactTrackingService
    {
        /// <summary>
        /// Contact tracking service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public ContactTrackingService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Get all activities for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ContactActivity</returns>
        public ResultSet<ContactActivity> GetActivities(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            return GetActivities(contactId, limit, createdSince, null);
        }

        /// <summary>
        /// Get all activities for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>	 
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ContactActivity</returns>
        public ResultSet<ContactActivity> GetActivities(string contactId, int? limit, DateTime? createdSince, Pagination pag)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ContactTrackingActivities, new object[] { contactId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<ContactActivity>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }    
        }

        /// <summary>
        /// Get activities by email campaign for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>ResultSet containing a results array of @link TrackingSummary</returns>
        public IList<TrackingSummary> GetEmailCampaignActivities(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            string url = ConstructUrl(Settings.Endpoints.Default.ContactTrackingEmailCampaignActivities, new object[] { contactId }, null);
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<IList<TrackingSummary>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        public ResultSet<BounceActivity> GetBounces(string contactId, int? limit)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            return GetBounces(contactId, limit, null);
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        public ResultSet<BounceActivity> GetBounces(Pagination pag)
        {
            return GetBounces(null, null, pag);
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        private ResultSet<BounceActivity> GetBounces(string contactId, int? limit, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ContactTrackingBounces, new object[] { contactId }, new object[] { "limit", limit }) : pag.GetNextUrl();
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
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        public ResultSet<ClickActivity> GetClicks(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            return GetClicks(contactId, limit, createdSince, null);
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        public ResultSet<ClickActivity> GetClicks(DateTime? createdSince, Pagination pag)
        {
            return GetClicks(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        private ResultSet<ClickActivity> GetClicks(string contactId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ContactTrackingClicks, new object[] { contactId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            return GetForwards(contactId, limit, createdSince, null);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetForwards(DateTime? createdSince, Pagination pag)
        {
            return GetForwards(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        private ResultSet<ForwardActivity> GetForwards(string contactId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ContactTrackingForwards, new object[] { contactId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            return GetOpens(contactId, limit, createdSince, null);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetOpens(DateTime? createdSince, Pagination pag)
        {
            return GetOpens(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        private ResultSet<OpenActivity> GetOpens(string contactId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ContactTrackingOpens, new object[] { contactId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        public ResultSet<SendActivity> GetSends(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            return GetSends(contactId, limit, createdSince, null);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        public ResultSet<SendActivity> GetSends(DateTime? createdSince, Pagination pag)
        {
            return GetSends(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        private ResultSet<SendActivity> GetSends(string contactId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ContactTrackingSends, new object[] { contactId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            return GetOptOuts(contactId, limit, createdSince, null);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetOptOuts(DateTime? createdSince, Pagination pag)
        {
            return GetOptOuts(null, null, createdSince, pag);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        private ResultSet<OptOutActivity> GetOptOuts(string contactId, int? limit, DateTime? createdSince, Pagination pag)
        {
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ContactTrackingUnsubscribes, new object[] { contactId }, new object[] { "limit", limit, "created_since", Extensions.ToISO8601String(createdSince) }) : pag.GetNextUrl();
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
        /// Get a summary of reporting data for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Tracking summary.</returns>
        public TrackingSummary GetSummary(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactTrackingOrId);
            }

            string url = ConstructUrl(Settings.Endpoints.Default.ContactTrackingSummary, new object[] { contactId }, null);
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

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<ClickActivity> GetContactTrackingClicks(string contactId, DateTime? createdSince)
        {
            return GetClicks(contactId, null, createdSince);
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<BounceActivity> GetContactTrackingBounces(string contactId)
        {
            return GetBounces(contactId, null);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<ForwardActivity> GetContactTrackingForwards(string contactId, DateTime? createdSince)
        {
            return GetForwards(contactId, null, createdSince);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<OpenActivity> GetContactTrackingOpens(string contactId, DateTime? createdSince)
        {
            return GetOpens(contactId, null, createdSince);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<SendActivity> GetContactTrackingSends(string contactId, DateTime? createdSince)
        {
            return GetSends(contactId, null, createdSince);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<OptOutActivity> GetContactTrackingOptOuts(string contactId, DateTime? createdSince)
        {
            return GetOptOuts(contactId, null, createdSince);
        }

        /// <summary>
        /// Get all activities for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<ContactActivity> GetContactTrackingActivities(string contactId, int? limit, DateTime? createdSince)
        {
            return GetActivities(contactId, limit, createdSince);
        }
    }
}
