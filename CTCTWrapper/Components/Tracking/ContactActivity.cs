using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
	/// <summary>
    /// Represents a Contact Activity class.
    /// </summary>
    [Serializable]
    [DataContract]
	public class ContactActivity : BaseActivity
	{
		/// <summary>
        /// Gets or sets the open date.
        /// </summary>
        [DataMember(Name = "open_date")]
        public string OpenDate { get; set; }
		/// <summary>
        /// Gets or sets the unsubscribe date
        /// </summary>
		[DataMember(Name = "unsubscribe_date")]
        public string UnsubscribeDate { get; set; }
		/// <summary>
        /// Gets or sets the send date
        /// </summary>
		[DataMember(Name = "send_date")]
        public string SendDate { get; set; }
		/// <summary>
        /// Gets or sets the forward date
        /// </summary>
		[DataMember(Name = "forward_date ")]
        public string ForwardDate { get; set; }
		/// <summary>
        /// Gets or sets the opens
        /// </summary>
		[DataMember(Name = "opens")]
        public int Opens { get; set; }
		/// <summary>
        /// Gets or sets the link uri
        /// </summary>
		[DataMember(Name = "link_uri ")]
        public string LinkUri { get; set; }
		/// <summary>
        /// Gets or sets the link id
        /// </summary>
		[DataMember(Name = "link_id")]
        public string LinkId { get; set; }
		/// <summary>
        /// Gets or sets the bounces
        /// </summary>
		[DataMember(Name = "bounces")]
        public int Bounces { get; set; }
		/// <summary>
        /// Gets or sets the unsubscribe reason
        /// </summary>
		[DataMember(Name = "unsubscribe_reason")]
        public string UnsubscribeReason { get; set; }
		/// <summary>
        /// Gets or sets the forwards
        /// </summary>
		[DataMember(Name = "forwards")]
        public int Forwards { get; set; }
		/// <summary>
        /// Gets or sets the bounce description
        /// </summary>
		[DataMember(Name = "bounce_description")]
        public string BounceDescription { get; set; }
		/// <summary>
        /// Gets or sets the unsubscribe source
        /// </summary>
		[DataMember(Name = "unsubscribe_source")]
        public string UnsubscribeSource { get; set; }
		/// <summary>
        /// Gets or sets the bounce message
        /// </summary>
		[DataMember(Name = "bounce_message")]
        public string BounceMessage { get; set; }
		/// <summary>
        /// Gets or sets the bounce code
        /// </summary>
		[DataMember(Name = "bounce_code")]
        public string BounceCode { get; set; }
		/// <summary>
        /// Gets or sets the clicks
        /// </summary>
		[DataMember(Name = "clicks")]
        public int Clicks { get; set; }
		/// <summary>
        /// Gets or sets the bounce date
        /// </summary>
		[DataMember(Name = "bounce_date")]
        public string BounceDate { get; set; }
		/// <summary>
        /// Gets or sets the click date
        /// </summary>
		[DataMember(Name = "click_date")]
        public string ClickDate { get; set; }
	}
}
