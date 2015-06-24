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
        [DataMember(Name = "email_addresses")]
        private List<string> _EmailAddresses = new List<string>();

        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        private List<Address> _Addresses = new List<Address>();

        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        private List<CustomField> _CustomFields = new List<CustomField>();

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
        /// Birthday day
        /// </summary>
        [DataMember(Name = "birthday_day", EmitDefaultValue = false)]
        public string BirthdayDay { get; set; }

        /// <summary>
        /// Birthday month
        /// </summary>
        [DataMember(Name = "birthday_month", EmitDefaultValue = false)]
        public string BirthdayMonth { get; set; }

        /// <summary>
        /// Anniversary
        /// Accepts the following formats MM/DD/YYYY, M/D/YYYY, YYYY/MM/DD, YYYY/M/D, YYYY-MM-DD, YYYY-M-D,M-D-YYYY, M-DD-YYYY. 
        /// The year must be greater than 1900 and cannot be more than 10 years in the future (with respect to the current year
        /// </summary>
        [DataMember(Name = "anniversary", EmitDefaultValue = false)]
        public string Anniversary { get; set; }

        /// <summary>
        /// Gets or sets the email addresses list.
        /// </summary>
        public IList<string> EmailAddresses 
        {
            get { return _EmailAddresses; }
            set { _EmailAddresses = value == null ? null : value.ToList(); } 
        }

        /// <summary>
        /// Gets or sets addresses list.
        /// </summary>
        public IList<Address> Addresses 
        {
            get { return _Addresses; }
            set { _Addresses = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the custom fields list.
        /// </summary>
        public IList<CustomField> CustomFields 
        {
            get { return _CustomFields; }
            set { _CustomFields = value == null ? null : value.ToList(); }
        }
    }
}
