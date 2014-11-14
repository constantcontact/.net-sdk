using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Represents a campaign Schedule in Constant Contact class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Schedule : Component
    {
        /// <summary>
        /// Unique id of the schedule.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        /// <summary>
        /// The scheduled start date/time in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "scheduled_date", EmitDefaultValue = false)]
        private string ScheduledDateString { get; set; }
        /// <summary>
        /// Gets or sets the scheduled date.
        /// </summary>
        public DateTime? ScheduledDate
        {
            get { return this.ScheduledDateString.FromISO8601String(); }
            set { this.ScheduledDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public Schedule() { }
    }
}
