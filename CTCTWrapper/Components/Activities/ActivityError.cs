using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Activities
{
    /// <summary>
    /// Activity error class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ActivityError : Component
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        [DataMember(Name = "line_number")]
        public string LineNumber { get; set; }
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DataMember(Name = "email_address")]
        public string EmailAddress { get; set; } 
    }
}
