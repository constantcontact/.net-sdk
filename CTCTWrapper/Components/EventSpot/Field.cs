using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Field class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Field : Component
    {
        /// <summary>
        /// String representation of the type value
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        private string TypeString { get; set; }

        /// <summary>
        /// Type of the value
        /// </summary>
        public FieldType FieldType
        {
            get { return this.TypeString.ToEnum<FieldType>(); }
            set { this.TypeString = value.ToString(); }
        }

        /// <summary>
        /// Name of the field 
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Field label displayed to viewers 
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// The value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; } 
    }
}
