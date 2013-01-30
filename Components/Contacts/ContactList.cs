using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// Represents a single List in Constant Contact.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ContactList : Component
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name="id")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        public ContactList() { }
    }
}
