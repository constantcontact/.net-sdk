using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Represents a Click Activity class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ClickActivity : BaseActivity
    {
        /// <summary>
        /// Gets or sets the link identification.
        /// </summary>
        [DataMember(Name="link_id")]
        public string LinkId { get; set; }
        /// <summary>
        /// Gets or sets the click date.
        /// </summary>
        [DataMember(Name="click_date")]
        public string ClickDate { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public ClickActivity() { }
    }
}
