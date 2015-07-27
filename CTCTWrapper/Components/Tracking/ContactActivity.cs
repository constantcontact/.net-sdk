using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

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
        [DataMember(Name = "open_date", EmitDefaultValue = false)]
        private string OpenDateString { get; set; }

        public DateTime? OpenDate
        {
            get { return this.OpenDateString.FromISO8601String(); }
            set { this.OpenDateString = value.ToISO8601String(); }
        }

		/// <summary>
        /// Gets or sets the unsubscribe date
        /// </summary>
		[DataMember(Name = "unsubscribe_date", EmitDefaultValue = false)]
        private string UnsubscribeDateString { get; set; }

        public DateTime? UnsubscribeDate
        {
            get { return this.UnsubscribeDateString.FromISO8601String(); }
            set { this.UnsubscribeDateString = value.ToISO8601String(); }
        }

		/// <summary>
        /// Gets or sets the send date
        /// </summary>
        [DataMember(Name = "send_date", EmitDefaultValue = false)]
        private string SendDateString { get; set; }

        public DateTime? SendDate
        {
            get { return this.SendDateString.FromISO8601String(); }
            set { this.SendDateString = value.ToISO8601String(); }
        }

		/// <summary>
        /// Gets or sets the forward date
        /// </summary>
        [DataMember(Name = "forward_date", EmitDefaultValue = false)]
        private string ForwardDateString { get; set; }

        public DateTime? ForwardDate
        {
            get { return this.ForwardDateString.FromISO8601String(); }
            set { this.ForwardDateString = value.ToISO8601String(); }
        }

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
        [DataMember(Name = "bounce_date", EmitDefaultValue = false)]
        private string BounceDateString { get; set; }

        public DateTime? BounceDate
        {
            get { return this.BounceDateString.FromISO8601String(); }
            set { this.BounceDateString = value.ToISO8601String(); }
        }

		/// <summary>
        /// Gets or sets the click date
        /// </summary>
        [DataMember(Name = "click_date", EmitDefaultValue = false)]
        private string ClickDateString { get; set; }

        public DateTime? ClickDate
        {
            get { return this.ClickDateString.FromISO8601String(); }
            set { this.ClickDateString = value.ToISO8601String(); }
        }
	}
}
