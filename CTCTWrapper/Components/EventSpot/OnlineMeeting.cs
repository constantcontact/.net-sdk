using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// OnlineMeeting class
    /// </summary>
    [DataContract]
    [Serializable]
    public class OnlineMeeting : Component
    {
        /// <summary>
        /// Online meeting instructions, such as dial in number, password, etc 
        /// </summary>
        [DataMember(Name = "instructions", EmitDefaultValue = false)]
        public string Instructions { get; set; }

        /// <summary>
        /// Meeting ID, if any, for the meeting 
        /// </summary>
        [DataMember(Name = "provider_meeting_id", EmitDefaultValue = false)]
        public string ProviderMeetingId { get; set; }

        /// <summary>
        /// Specify the online meeting provider, such as WebEx 
        /// </summary>
        [DataMember(Name = "provider_type", EmitDefaultValue = false)]
        public string ProviderType { get; set; }

        /// <summary>
        /// URL for online meeting. REQUIRED if is_virtual_event is set to true
        /// </summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; } 
    }
}
