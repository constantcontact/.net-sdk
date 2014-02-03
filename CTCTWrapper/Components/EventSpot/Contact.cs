using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.EventSpot
{
    [DataContract]
    [Serializable]
    public class Contact : Component
    {
        /// <summary>
        /// Event contact's email-address
        /// </summary>
        [DataMember(Name = "email_address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Name of the person conducting or managing the event 
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Event contact's organization name 
        /// </summary>
        [DataMember(Name = "organization_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Event contact's phone number 
        /// </summary>
        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }
    }
}
