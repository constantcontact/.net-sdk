using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Components;

namespace CTCT.Util
{
    /// <summary>
    /// Used PATCH method
    /// </summary>
    [DataContract]
    [Serializable]
    public class PatchRequest : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operation to perform </param>
        /// <param name="path">Where in the object to perform it</param>
        /// <param name="value">The new value to write</param>
        public PatchRequest(string op, string path, object value)
        {
            this.Op = op;
            this.Path = path;
            this.Value = value;
        }

        /// <summary>
        /// The operation to perform 
        /// </summary>
        [DataMember(Name = "op", EmitDefaultValue = false)]
        public string Op { get; private set; }

        /// <summary>
        /// Where in the object to perform it
        /// </summary>
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; private set; }

        /// <summary>
        /// The new value to write
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public object Value { get; private set; }

    }
}
