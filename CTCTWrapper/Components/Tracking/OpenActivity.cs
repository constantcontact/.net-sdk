using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Tracking
{
    /// <summary>
    /// Represents an Open Activity class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class OpenActivity : BaseActivity
    {
        /// <summary>
        /// Gets or sets the open date.
        /// </summary>
        [DataMember(Name="open_date")]
        public string OpenDate { get; set; }
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OpenActivity() { }
    }
}
