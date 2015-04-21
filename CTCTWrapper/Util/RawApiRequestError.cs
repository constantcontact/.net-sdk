using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Components;

namespace CTCT.Util
{
    /// <summary>
    /// Class for holding the URL request error.
    /// </summary>
    [DataContract]
    [Serializable]
    public class RawApiRequestError : Component
    {
        /// <summary>
        /// Gets or sets the error key.
        /// </summary>
        [DataMember(Name = "error_key")]
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [DataMember(Name = "error_message")]
        public string Message { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public RawApiRequestError() { }
    }
}
