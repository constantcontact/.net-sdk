using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Represents a single OptOut Activity class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptOutActivity : BaseActivity
    {
        /// <summary>
        /// Gets or sets the unsubscribe date.
        /// </summary>
        [DataMember(Name="unsubscribe_date")]
        public string UnsubscribeDate { get; set; }
        /// <summary>
        /// Gets or sets the unsubscribe source.
        /// </summary>
        [DataMember(Name="unsubscribe_source")]
        public string UnsubscribeSource { get; set; }
        /// <summary>
        /// Gets or sets the unsubscribe reason.
        /// </summary>
        [DataMember(Name="unsubscribe_reason")]
        public string UnsubscribeReason { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public OptOutActivity() { }
    }
}
