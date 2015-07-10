using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// Represents a single Address of a Contact.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Address : Component
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the first address line.
        /// </summary>
        [DataMember(Name = "line1", EmitDefaultValue = false)]
        public string Line1 { get; set; }
        /// <summary>
        /// Gets or sets the second address line.
        /// </summary>
        [DataMember(Name = "line2", EmitDefaultValue = false)]
        public string Line2 { get; set; }
        /// <summary>
        /// Gets or sets the third address line.
        /// </summary>
        [DataMember(Name = "line3", EmitDefaultValue = false)]
        public string Line3 { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        [DataMember(Name = "address_type", EmitDefaultValue = false)]
        public string AddressType { get; set; }
        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        [DataMember(Name = "state_code", EmitDefaultValue = false)]
        public string StateCode { get; set; }
        /// <summary>
        /// Gets or sets the state name.
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string StateName { get; set; }
        /// <summary>
        /// Gets or sets the contry code.
        /// </summary>
        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the subpostal code.
        /// </summary>
        [DataMember(Name = "sub_postal_code", EmitDefaultValue = false)]
        public string SubPostalCode { get; set; } 
    }

    /// <summary>
    /// Address type structure.
    /// </summary>
    public struct AddressType
    {
        /// <summary>
        /// Personal.
        /// </summary>
        public const string Personal = "PERSONAL";
        /// <summary>
        /// Business.
        /// </summary>
        public const string Business = "BUSINESS";
    }
}
