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
    public class ContactList : Component
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the number of contacts in the list
        /// </summary>
        [DataMember(Name = "contact_count", EmitDefaultValue = false)]
        public int ContactCount { get; set; }
        /// <summary>
        /// Gets or sets the contact list name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public ContactList() { }
    }
}
