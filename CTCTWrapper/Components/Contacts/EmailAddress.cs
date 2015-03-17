using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// Represents a single EmailAddress of a Contact.
    /// </summary>
    [DataContract]
    [Serializable]
    public class EmailAddress : Component
    {
        /// <summary>
        /// Email address id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the OPT out date.
        /// </summary>
        [DataMember(Name = "opt_out_date", EmitDefaultValue = false)]
        public string OptOutDate { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the OPT source.
        /// </summary>
        [DataMember(Name = "opt_in_source", EmitDefaultValue = false)]
        public string OptInSource { get; set; }
        /// <summary>
        /// Gets or sets the OPT date.
        /// </summary>
        [DataMember(Name = "opt_in_date", EmitDefaultValue = false)]
        public string OptInDate { get; set; }
        /// <summary>
        /// Gets or sets the OPT source.
        /// </summary>
        [DataMember(Name = "opt_out_source", EmitDefaultValue = false)]
        public string OptOutSource { get; set; }
        /// <summary>
        /// Gets or sets the confirmation status.
        /// </summary>
        [DataMember(Name = "confirm_status", EmitDefaultValue = false)]
        public string ConfirmStatus { get; set; }
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DataMember(Name = "email_address")]
        public string EmailAddr { get; set; } 

        /// <summary>
        /// Class constructor.
        /// </summary>
        public EmailAddress() 
        {
        
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="emailAddress">Email address.</param>
        public EmailAddress(string emailAddress)
        {
            this.EmailAddr = emailAddress;
        }
    }

    /// <summary>
    /// Confirmation status structure.
    /// </summary>
    public struct ConfirmStatus
    {
        /// <summary>
        /// Confirmed.
        /// </summary>
        public const string Confirmed = "CONFIRMED";
        /// <summary>
        /// NoConfirmationRequired.
        /// </summary>
        public const string NoConfirmationRequired = "NO_CONFIRMATION_REQUIRED";
        /// <summary>
        /// Unconfirmed.
        /// </summary>
        public const string Unconfirmed = "UNCONFIRMED"; 
    }

    /// <summary>
    /// Option source structure.
    /// </summary>
    public struct OptInSource
    {
        /// <summary>
        /// ActionByVisitor.
        /// </summary>
        public const string ActionByVisitor = "ACTION_BY_VISITOR";
        /// <summary>
        /// ActionByOwner.
        /// </summary>
        public const string ActionByOwner = "ACTION_BY_OWNER"; 
    }
}
