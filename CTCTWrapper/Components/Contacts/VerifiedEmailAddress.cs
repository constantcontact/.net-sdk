using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// VerifiedEmailAddress class
    /// </summary>
    [Serializable]
    [DataContract]
    public class VerifiedEmailAddress : Component
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DataMember(Name = "email_address", EmitDefaultValue = false)]
        public string EmailAddr { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}
