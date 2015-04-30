using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Guest information
    /// </summary>
    [DataContract]
    [Serializable]
    public class GuestInformation : Component
    {
        /// <summary>
        /// Guest Id
        /// </summary>
        [DataMember(Name = "guest_id", EmitDefaultValue = false)]
        public string GuestId { get; set; }

        /// <summary>
        /// Guest section
        /// </summary>
        [DataMember(Name = "guest_section", EmitDefaultValue = false)]
        public Section GuestSection { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GuestInformation()
        {
            this.GuestSection = new Section();
        }
    }
}
