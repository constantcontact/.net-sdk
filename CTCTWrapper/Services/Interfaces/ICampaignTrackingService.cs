﻿using System;
using CTCT.Components;
using CTCT.Components.Tracking;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for CampaignTrackingService class.
    /// </summary>
    public interface ICampaignTrackingService
    {
        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for bounces created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        ResultSet<BounceActivity> GetBounces(string campaignId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        ResultSet<BounceActivity> GetBounces(Pagination pag);

        /// <summary>
        /// Get clicks for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="linkId">Specifies the link in the email campaign to retrieve click data for.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        ResultSet<ClickActivity> GetClicks(string campaignId, string linkId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get clicks for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        ResultSet<ClickActivity> GetClicks(string campaignId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get clicks for a given campaign.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        ResultSet<ClickActivity> GetClicks(Pagination pag);

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        ResultSet<ForwardActivity> GetForwards(string campaignId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        ResultSet<ForwardActivity> GetForwards(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        ResultSet<OpenActivity> GetOpens(string campaignId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        ResultSet<OpenActivity> GetOpens(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        ResultSet<SendActivity> GetSends(string campaignId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        ResultSet<SendActivity> GetSends(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        ResultSet<OptOutActivity> GetOptOuts(string campaignId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        ResultSet<OptOutActivity> GetOptOuts(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get a summary of reporting data for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Tracking summary.</returns>
        TrackingSummary GetSummary(string campaignId);
    }
}
