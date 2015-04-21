using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.AccountService
{
    /// <summary>
    /// Organization addresses
    /// </summary>
    [DataContract]
    [Serializable]
    public class OrganizationAddresses : Component
    {
        /// <summary>
        /// REQUIRED if including organization_addresses; The city the organization is located in 
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// REQUIRED if including organization_addresses; Standard 2 letter ISO 3166-1 code for the organization_addresses 
        /// </summary>
        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string CountryCode { get; set; }

        /// <summary>
        /// REQUIRED if including organization_addresses; Line 1 of the organization's street address 
        /// </summary>
        [DataMember(Name = "line1", EmitDefaultValue = false)]
        public string Line1 { get; set; }

        /// <summary>
        /// Line 2 of the organization's street address
        /// </summary>
        [DataMember(Name = "line2", EmitDefaultValue = false)]
        public string Line2 { get; set; }

        /// <summary>
        /// Line 3 of the organization's street address 
        /// </summary>
        [DataMember(Name = "line3", EmitDefaultValue = false)]
        public string Line3 { get; set; }

        /// <summary>
        /// Postal (zip) code of the organization's street address 
        /// </summary>
        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Name of the state or province for the organization_addresses; 
        /// For country = CA or US, this field is overwritten by the state or province name derived from the state_code, if entered. 
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        /// Use ONLY for the standard 2 letter abbreviation for the US state or Canadian province for organization_addresses;
        /// NOTE: A data validation error occurs if state_code is populated and country_code does not = US or CA. 
        /// </summary>
        [DataMember(Name = "state_code", EmitDefaultValue = false)]
        public string StateCode { get; set; }   
    }
}
