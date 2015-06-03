using System;
using System.Collections.Generic;
using CTCT.Util;
using CTCT.Components.BulkStatus;
using CTCT.Exceptions;
using System.Web;

namespace CTCT.Services
{
    /// <summary>
    /// Bulk status service
    /// </summary>
    public class BulkStatusService : BaseService, IBulkStatusService
    {
        /// <summary>
        /// Bulk status report service constructor
        /// </summary>
        /// <param name="userServiceContext"></param>
        public BulkStatusService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Get all bulk activities status.
        /// </summary>
        /// <returns>A list of StatusReport</returns>
        /// <exception cref="CtctException">CtctException.</exception>
        public IList<StatusReport> GetBulkActivitiesStatus()
        {
            return GetBulkActivitiesStatus(BulkActivityType.ALL, BulkActivityStatus.ALL);
        }

        /// <summary>
        /// Get all bulk activities status filtered by status and/or type.
        /// </summary>
        /// <param name="type">Bulk activity type</param>
        /// <param name="status">Bulk activity status</param>
        /// <returns>A list of StatusReport</returns>
        /// <exception cref="CtctException">CtctException.</exception>
        public IList<StatusReport> GetBulkActivitiesStatus(BulkActivityType type, BulkActivityStatus status)
        {
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.Activities);
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (type != BulkActivityType.ALL)
            {
                query["type"] = Enum.GetName(typeof(BulkActivityType), type);
            }
            if (status != BulkActivityStatus.ALL)
            {
                query["status"] = Enum.GetName(typeof(BulkActivityStatus), status);
            }
            uriBuilder.Query = query.ToString();
            url = uriBuilder.ToString();

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var bulkStatusReport = response.Get<List<StatusReport>>();
                return bulkStatusReport;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }


        /// <summary>
        /// Gets the status report for an activity by ID
        /// </summary>
        /// <param name="activityId">The activity ID</param>
        /// <returns>The StatusReport</returns>
        public StatusReport GetBulkActivityStatusById(string activityId)
        {
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Activity, activityId));

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var bulkStatusReport = response.Get<StatusReport>();
                return bulkStatusReport;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }
    }
}
