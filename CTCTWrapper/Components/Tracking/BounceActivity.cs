using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Represents a single Bounce Activity class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class BounceActivity : BaseActivity
    {
        /// <summary>
        /// Gets or sets the bounce code.
        /// </summary>
        [DataMember(Name="bounce_code")]
        public string BounceCode { get; set; }
        /// <summary>
        /// Gets or sets the bounce description.
        /// </summary>
        [DataMember(Name="bounce_description")]
        public string BounceDescription { get; set; }
        /// <summary>
        /// Gets or sets the bounce message.
        /// </summary>
        [DataMember(Name="bounce_message")]
        public string BounceMessage { get; set; }
        /// <summary>
        /// Gets or sets the bounce date.
        /// </summary>
        [DataMember(Name="bounce_date")]
        public string BounceDate { get; set; }
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        public BounceActivity() { }
    }
}
