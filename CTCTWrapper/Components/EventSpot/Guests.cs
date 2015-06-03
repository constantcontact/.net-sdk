using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Guests class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Guests : Component
    {
        [DataMember(Name = "guest_info", EmitDefaultValue = false)]
        private List<GuestInformation> _GuestInformation = new List<GuestInformation>();

        /// <summary>
        /// GuestCount
        /// </summary>
        [DataMember(Name = "guest_count", EmitDefaultValue = false)]
        public int GuestCount { get; set; }

        /// <summary>
        /// Guest information list
        /// </summary>
        public IList<GuestInformation> GuestInformation
        {
            get { return _GuestInformation; }
            set { _GuestInformation = value == null ? null : value.ToList(); }
        }
    }
}
