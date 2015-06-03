using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.BulkStatus;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for bulk status service
    /// </summary>
    public interface IBulkStatusService
    {
        /// <summary>
        /// Get all bulk activities status.
        /// </summary>
        /// <returns>A list of StatusReport</returns>
        /// <exception cref="CtctException">CtctException.</exception>
        IList<StatusReport> GetBulkActivitiesStatus();

        /// <summary>
        /// Get all bulk activities status filtered by status and/or type.
        /// </summary>
        /// <param name="type">Bulk activity type</param>
        /// <param name="status">Bulk activity status</param>
        /// <returns>A list of StatusReport</returns>
        /// <exception cref="CtctException">CtctException.</exception>
        IList<StatusReport> GetBulkActivitiesStatus(BulkActivityType type, BulkActivityStatus status);

        /// <summary>
        /// Gets the status report for an activity by ID
        /// </summary>
        /// <param name="activityId">The activity ID</param>
        /// <returns>The StatusReport</returns>
        /// <exception cref="CtctException">CtctException.</exception>
        StatusReport GetBulkActivityStatusById(string activityId);
    }
}