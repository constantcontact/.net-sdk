using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Represents a Sent Activity class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class SendActivity : BaseActivity
    {
        /// <summary>
        /// Gets or sets the send date.
        /// </summary>
        [DataMember(Name="send_date")]
        public string SendDate { get; set; }
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        public SendActivity() { }
    }
}
