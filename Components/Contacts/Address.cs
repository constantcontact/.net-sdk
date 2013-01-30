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
        /// Gets or sets the first address line.
        /// </summary>
        [DataMember(Name="line1")]
        public string Line1 { get; set; }
        /// <summary>
        /// Gets or sets the second address line.
        /// </summary>
        [DataMember(Name = "line2")]
        public string Line2 { get; set; }
        /// <summary>
        /// Gets or sets the third address line.
        /// </summary>
        [DataMember(Name = "line3")]
        public string Line3 { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        [DataMember(Name = "address_type")]
        public string AddressType { get; set; }
        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        [DataMember(Name = "state_code")]
        public string StateCode { get; set; }
        /// <summary>
        /// Gets or sets the contry code.
        /// </summary>
        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the subpostal code.
        /// </summary>
        [DataMember(Name = "sub_postal_code")]
        public string SubPostalCode { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public Address() { }
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
        /// <summary>
        /// Unknown.
        /// </summary>
        public const string Unknown = "UNKNOWN";
    }
}
