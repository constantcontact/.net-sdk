using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Components.Contacts;

namespace CTCT.Components.Activities
{
    /// <summary>
    /// Add contacts import data class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class AddContactsImportData : Component
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        [DataMember(Name = "middle_name", EmitDefaultValue = false)]
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the job title.
        /// </summary>
        [DataMember(Name = "job_title", EmitDefaultValue = false)]
        public string JobTitle { get; set; }
        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [DataMember(Name = "company_name", EmitDefaultValue = false)]
        public string CompanyName { get; set; }
        /// <summary>
        /// Gets or sets the work phone.
        /// </summary>
        [DataMember(Name = "work_phone", EmitDefaultValue = false)]
        public string WorkPhone { get; set; }
        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        [DataMember(Name = "home_phone", EmitDefaultValue = false)]
        public string HomePhone { get; set; }
        /// <summary>
        /// Gets or sets the email addresses list.
        /// </summary>
        [DataMember(Name = "email_addresses")]
        public IList<string> EmailAddresses { get; set; }
        /// <summary>
        /// Gets or sets addresses list.
        /// </summary>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public IList<Address> Addresses { get; set; }
        /// <summary>
        /// Gets or sets the custom fields list.
        /// </summary>
        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public IList<CustomField> CustomFields { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public AddContactsImportData()
        {
            this.EmailAddresses = new List<string>();
            this.Addresses = new List<Address>();
            this.CustomFields = new List<CustomField>();
        }
    }
}
