using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Registrant class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Registrant : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Registrant()
        {
            this.Sections = new List<Section>();
            this.PaymentSummary = new PaymentSummary();
            this.GuestSections = new List<Guest>();
        }

        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Sections
        /// </summary>
        [DataMember(Name = "sections", EmitDefaultValue = false)]
        public IList<Section> Sections { get; set; }

        /// <summary>
        /// Ticket id
        /// </summary>
        [DataMember(Name = "ticket_id", EmitDefaultValue = false)]
        public string TicketId { get; set; }

        /// <summary>
        /// String representation of date the registrant registered for the event
        /// </summary>
        [DataMember(Name = "registration_date", EmitDefaultValue = false)]
        private string RegistrationDateString { get; set; }

        /// <summary>
        /// Date the registrant registered for the event
        /// </summary>
        public DateTime? RegistrationDate
        {
            get { return this.RegistrationDateString.FromISO8601String(); }
            set { this.RegistrationDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Payment summary
        /// </summary>
        [DataMember(Name = "payment_summary", EmitDefaultValue = false)]
        public PaymentSummary PaymentSummary { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Guest count
        /// </summary>
        [DataMember(Name = "guest_count", EmitDefaultValue = true)]
        public int GuestCount { get; set; }

        /// <summary>
        /// Payment status
        /// </summary>
        [DataMember(Name = "payment_status", EmitDefaultValue = false)]
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Registration status
        /// </summary>
        [DataMember(Name = "registration_status", EmitDefaultValue = false)]
        public string RegistrationStatus { get; set; }

        /// <summary>
        /// String representation of update date
        /// </summary>
        [DataMember(Name = "updated_date", EmitDefaultValue = false)]
        private string UpdatedDateString { get; set; }

        /// <summary>
        /// Update date
        /// </summary>
        public DateTime? UpdatedDate
        {
            get { return this.UpdatedDateString.FromISO8601String(); }
            set { this.UpdatedDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// An array of guest properties 
        /// </summary>
        [DataMember(Name = "guest_sections", EmitDefaultValue = false)]
        public IList<Guest> GuestSections { get; set; }
    }
}
