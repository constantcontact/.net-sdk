using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Represents a click through detail class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class MessageFooter : Component
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the organization name.
        /// </summary>
        [DataMember(Name = "organization_name", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }
        /// <summary>
        /// Gets or sets the addrese line 1.
        /// </summary>
        [DataMember(Name = "address_line_1", EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }
        /// <summary>
        /// Gets or sets the addrese line 2.
        /// </summary>
        [DataMember(Name = "address_line_2", EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }
        /// <summary>
        /// Gets or sets the addrese line 3.
        /// </summary>
        [DataMember(Name = "address_line_3", EmitDefaultValue = false)]
        public string AddressLine3 { get; set; }
        /// <summary>
        /// Gets or sets the international state.
        /// </summary>
        [DataMember(Name = "international_state", EmitDefaultValue = false)]
        public string InternationalState { get; set; }
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the include forward email flag.
        /// </summary>
        [DataMember(Name = "include_forward_email", EmitDefaultValue = false)]
        public bool IncludeForwardEmail { get; set; }
        /// <summary>
        /// Gets or sets the forward email link text.
        /// </summary>
        [DataMember(Name = "forward_email_link_text", EmitDefaultValue = false)]
        public string ForwardEmailLinkText { get; set; }
        /// <summary>
        /// Gets or sets the include subscribe link flag.
        /// </summary>
        [DataMember(Name = "include_subscribe_link", EmitDefaultValue = false)]
        public bool IncludeSubscribeLink { get; set; }
        /// <summary>
        /// Gets or sets the subscribe link text.
        /// </summary>
        [DataMember(Name = "subscribe_link_text", EmitDefaultValue = false)]
        public string SubscribeLinkText { get; set; } 
    }
}
