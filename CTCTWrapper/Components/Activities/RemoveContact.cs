using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Activities
{
    /// <summary>
    /// Represents an AddContact activity class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class RemoveContact: Component
    {
        /// <summary>
        /// Gets or sets the list of imported data.
        /// </summary>
        [DataMember(Name = "import_data", EmitDefaultValue = false)]
        public IList<ImportEmailAddress> ImportData { get; set; }
        /// <summary>
        /// Gets or sets the list of id's to add.
        /// </summary>
        [DataMember(Name = "lists", EmitDefaultValue = false)]
        public IList<string> Lists { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RemoveContact() {
        }
    }

    /// <summary>
    /// Represents an ImportEmailAddress class
    /// </summary>
    [DataContract]
    [Serializable]
    public class ImportEmailAddress
    {
        /// <summary>
        /// Gets or sets the list of email addresses
        /// </summary>
        [DataMember(Name = "email_addresses", EmitDefaultValue = false)]
        public IList<string> EmailAddresses { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ImportEmailAddress()
        { }
    }
}
