using System;
using CTCT.Components;
using CTCT.Components.Tracking;
using System.Collections.Generic;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for ContactTrackingService class.
    /// </summary>
    public interface IContactTrackingService
    {
		/// <summary>
		/// Get all activities for a given contact.
		/// </summary>
        /// <param name="contactId">Contact id.</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
		/// <returns>ResultSet containing a results array of @link ContactActivity</returns>
		ResultSet<ContactActivity> GetActivities(string contactId, int? limit, DateTime? createdSince);

		/// <summary>
		/// Get all activities for a given contact.
		/// </summary>
        /// <param name="contactId">Contact id.</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>	 
		/// <param name="pag">Pagination object.</param>
		/// <returns>ResultSet containing a results array of @link ContactActivity</returns>
		ResultSet<ContactActivity> GetActivities(string contactId, int? limit, DateTime? createdSince, Pagination pag);

		/// <summary>
        /// Get activities by email campaign for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>ResultSet containing a results array of @link TrackingSummary</returns>
        IList<TrackingSummary> GetEmailCampaignActivities(string contactId);

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        ResultSet<BounceActivity> GetBounces(string contactId, int? limit);

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        ResultSet<BounceActivity> GetBounces(Pagination pag);

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        ResultSet<ClickActivity> GetClicks(string contactId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        ResultSet<ClickActivity> GetClicks(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        ResultSet<ForwardActivity> GetForwards(string contactId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        ResultSet<ForwardActivity> GetForwards(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        ResultSet<OpenActivity> GetOpens(string contactId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        ResultSet<OpenActivity> GetOpens(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        ResultSet<SendActivity> GetSends(string contactId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        ResultSet<SendActivity> GetSends(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        ResultSet<OptOutActivity> GetOptOuts(string contactId, int? limit, DateTime? createdSince);

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        ResultSet<OptOutActivity> GetOptOuts(DateTime? createdSince, Pagination pag);

        /// <summary>
        /// Get a summary of reporting data for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Tracking summary.</returns>
        TrackingSummary GetSummary(string contactId);

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        /// <exception cref="CTCT.Exceptions.IllegalArgumentException">IllegalArgumentException</exception>
        ResultSet<ClickActivity> GetContactTrackingClicks(string contactId, DateTime? createdSince);

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        /// <exception cref="CTCT.Exceptions.IllegalArgumentException">IllegalArgumentException</exception>
        ResultSet<BounceActivity> GetContactTrackingBounces(string contactId);

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        /// <exception cref="CTCT.Exceptions.IllegalArgumentException">IllegalArgumentException</exception>
        ResultSet<ForwardActivity> GetContactTrackingForwards(string contactId, DateTime? createdSince);

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        /// <exception cref="CTCT.Exceptions.IllegalArgumentException">IllegalArgumentException</exception>
        ResultSet<OpenActivity> GetContactTrackingOpens(string contactId, DateTime? createdSince);

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        /// <exception cref="CTCT.Exceptions.IllegalArgumentException">IllegalArgumentException</exception>
        ResultSet<SendActivity> GetContactTrackingSends(string contactId, DateTime? createdSince);

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        /// <exception cref="CTCT.Exceptions.IllegalArgumentException">IllegalArgumentException</exception>
        ResultSet<OptOutActivity> GetContactTrackingOptOuts(string contactId, DateTime? createdSince);

        /// <summary>
        /// Get all activities for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
        /// <exception cref="CTCT.Exceptions.IllegalArgumentException">IllegalArgumentException</exception>
        ResultSet<ContactActivity> GetContactTrackingActivities(string contactId, int? limit, DateTime? createdSince);
    }
}
