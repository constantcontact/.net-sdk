using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.AccountService
{
    /// <summary>
    /// Summary account-related information 
    /// </summary>
    [DataContract]
    [Serializable]
    public class AccountSummaryInformation : Component
    {
        [DataMember(Name = "organization_addresses", EmitDefaultValue = false)]
        private List<OrganizationAddresses> _OrganizationAddresses = new List<OrganizationAddresses>();

        /// <summary>
        /// URL to the logo associated with the account if the account owner has provided one. 
        /// No value is returned if user has not added a logo. 
        /// </summary>
        [DataMember(Name = "company_logo", EmitDefaultValue = false)]
        public string CompanyLogo { get; set; }

        /// <summary>
        /// Standard 2 letter ISO 3166-1 code of the country associated with the account owner 
        /// </summary>
        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Email address associated with the account owner 
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The account owner's first name 
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// The account owner's last name 
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Name of the organization associated with the account 
        /// </summary>
        [DataMember(Name = "organization_name", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Phone number associated with the account owner 
        /// </summary>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        /// 2 letter code for USA state or Canadian province ONLY, available only if country_code = US or CA associated with the account owner 
        /// </summary>
        [DataMember(Name = "state_code", EmitDefaultValue = false)]
        public string StateCode { get; set; }

        /// <summary>
        /// The time zone associated with the account 
        /// </summary>
        [DataMember(Name = "time_zone", EmitDefaultValue = false)]
        public string TimeZone { get; set; }

        /// <summary>
        /// The URL of the Web site associated with the account 
        /// </summary>
        [DataMember(Name = "website", EmitDefaultValue = false)]
        public string Website { get; set; }

        /// <summary>
        /// An array of organization street addresses; currently, only a single address is supported. 
        /// This is not a required attribute, but if you include organization_addresses in a put, it must include the country_code, city, and line1 fields at minimum. 
        /// </summary>
        public IList<OrganizationAddresses> OrganizationAddresses
        {
            get { return _OrganizationAddresses; }
            set { _OrganizationAddresses = value == null ? null : value.ToList(); }
        }
    }
}
