using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Class to wrap a result set of individual activities (ie: OpensActivity, SendActivity).
    /// </summary>
    public class TrackingActivity
    {
        /// <summary>
        /// Gets or sets the list of activities.
        /// </summary>
        public IList<BaseActivity> Results { get; set; }
        /// <summary>
        /// Gets or sets the pagination array returned from a tracking endpoint.
        /// </summary>
        public int Next { get; set; }
    }
}
