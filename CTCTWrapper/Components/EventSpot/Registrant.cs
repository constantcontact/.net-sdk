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
        #region Fields

        [DataMember(Name = "sections", EmitDefaultValue = false)]
        private List<Section> _Sections = new List<Section>();

        [DataMember(Name = "guest_sections", EmitDefaultValue = false)]
        private List<Guest> _GuestSections = new List<Guest>();

        #endregion

        #region Properties

        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Sections
        /// </summary>
        public IList<Section> Sections 
        {
            get { return _Sections; }
            set { _Sections = value.ToList(); }
        }

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
        public IList<Guest> GuestSections 
        {
            get { return _GuestSections; }
            set { _GuestSections = value.ToList(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Registrant()
        {
            this.PaymentSummary = new PaymentSummary();
        }

         #endregion
    }
}
