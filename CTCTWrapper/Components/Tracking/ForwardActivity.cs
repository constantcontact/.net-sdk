using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Represents a Forward Activity class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ForwardActivity : BaseActivity
    {
        /// <summary>
        /// Gets or sets the forward date.
        /// </summary>
        [DataMember(Name="forward_date")]
        public string ForwardDate { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public ForwardActivity() { }
    }
}
