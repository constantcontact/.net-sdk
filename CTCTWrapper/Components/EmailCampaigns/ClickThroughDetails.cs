using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Represents a click through detail class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ClickThroughDetails : Component
    {
        /// <summary>
        /// Gets or sets the actual url that was clicked on.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the url unique identifier.
        /// </summary>
        [DataMember(Name = "url_uid")]
        public string UrlUid { get; set; }

        /// <summary>
        /// Gets or sets the number of times the url was clicked on.
        /// </summary>
        [DataMember(Name = "click_count")]
        public int ClickCount { get; set; } 
    }
}
