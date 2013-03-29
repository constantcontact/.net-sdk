using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Base class for activities.
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class BaseActivity : Component
    {
        /// <summary>
        /// Gets or sets the activity type.
        /// </summary>
        [DataMember(Name = "activity_type")]
        public string ActivityType { get; set; }
        /// <summary>
        /// Gets or sets the campaign id.
        /// </summary>
        [DataMember(Name = "campaign_id")]
        public string CampaignId { get; set; }
        /// <summary>
        /// Gets or sets the contact id.
        /// </summary>
        [DataMember(Name = "contact_id")]
        public int ContactId { get; set; }
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DataMember(Name = "email_address")]
        public string EmailAddress { get; set; }
    }

    /// <summary>
    /// Activity type enumeration.
    /// </summary>
    [Serializable]
    public enum ActivityType
    {
        /// <summary>
        /// Bounce activity.
        /// </summary>
        Bounce,
        /// <summary>
        /// Click activity.
        /// </summary>
        Click,
        /// <summary>
        /// Forward activity.
        /// </summary>
        Forward,
        /// <summary>
        /// Open activity.
        /// </summary>
        Open,
        /// <summary>
        /// OptOut activity.
        /// </summary>
        OptOut,
        /// <summary>
        /// Send activity.
        /// </summary>
        Send,
        /// <summary>
        /// Email open
        /// </summary>
        EMAIL_OPEN
    }
}
