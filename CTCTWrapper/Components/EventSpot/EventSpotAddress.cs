using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// EventSpotAddress class
    /// </summary>
    [DataContract]
    [Serializable]
    public class EventSpotAddress : Component
    {

        /// <summary>
        /// Standard 2 letter abbreviation for the state or Canadian province of the event location; if state_code is entered, the system overwrites the state property with the resolved state or province name
        /// </summary>
        [DataMember(Name = "state_code", EmitDefaultValue = false)]
        public string StateCode { get; set; }

        /// <summary>
        /// Postal ZIP code for the event 
        /// </summary>
        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// State or Canadian province name of the event location 
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        /// Longitude coordinates of the event location, , not used to determine event Location at this time on map if is_map_displayed set to true 
        /// </summary>
        [DataMember(Name = "longitude", EmitDefaultValue = true)]
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude coordinates of the event location, not used to determine event Location on map if is_map_displayed set to true 
        /// </summary>
        [DataMember(Name = "latitude", EmitDefaultValue = true)]
        public double Latitude { get; set; }

        /// <summary>
        /// Event address line 1 
        /// </summary>
        [DataMember(Name = "line1", EmitDefaultValue = false)]
        public string Line1 { get; set; }

        /// <summary>
        /// Event address line 2
        /// </summary>
        [DataMember(Name = "line2", EmitDefaultValue = false)]
        public string Line2 { get; set; }

        /// <summary>
        /// Event address line 3
        /// </summary>
        [DataMember(Name = "line3", EmitDefaultValue = false)]
        public string Line3 { get; set; }

        /// <summary>
        /// Standard 2 letter ISO 3166-1 code of the country associated with the event address 
        /// </summary>
        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Country of the event location 
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// City of the event location 
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }
        
    }
}
