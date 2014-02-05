using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Guest class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Guest : Component
    {
        /// <summary>
        /// Unique ID assigned to a guest 
        /// </summary>
        [DataMember(Name = "guest_id", EmitDefaultValue = false)]
        public string GuestId { get; set; }

        /// <summary>
        /// Field sections displayed 
        /// </summary>
        [DataMember(Name = "guests_info", EmitDefaultValue = false)]
        public string GuestsInfo { get; set; }
    }
}
