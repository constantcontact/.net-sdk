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
        [DataMember(Name = "import_data", EmitDefaultValue = false)]
        private List<ImportEmailAddress> _ImportData = new List<ImportEmailAddress>();

        [DataMember(Name = "lists", EmitDefaultValue = false)]
        private List<string> _Lists = new List<string>();

        /// <summary>
        /// Gets or sets the list of imported data.
        /// </summary>
        public IList<ImportEmailAddress> ImportData
        {
            get { return _ImportData; }
            set { _ImportData = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the list of id's to add.
        /// </summary>
        public IList<string> Lists 
        {
            get { return _Lists; }
            set { _Lists = value == null ? null : value.ToList(); }
        }
    }

    /// <summary>
    /// Represents an ImportEmailAddress class
    /// </summary>
    [DataContract]
    [Serializable]
    public class ImportEmailAddress
    {
        [DataMember(Name = "email_addresses", EmitDefaultValue = false)]
        private List<string> _EmailAddresses = new List<string>();

        /// <summary>
        /// Gets or sets the list of email addresses
        /// </summary>
        public IList<string> EmailAddresses
        {
            get { return _EmailAddresses; }
            set { _EmailAddresses = value == null ? null : value.ToList(); }
        }
    }
}
