using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Section class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Section : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Section()
        {
            this.Fields = new List<Field>();
        }

        /// <summary>
        /// Field label displayed to viewers 
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        ///  An array of the fields displayed in a section: field_type, name, label, value, values
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public IList<Field> Fields { get; set; }

        /// <summary>
        /// String representation of field type
        /// </summary>
        [DataMember(Name = "field_type", EmitDefaultValue = false)]
        private string FieldTypeString { get; set; }

        /// <summary>
        /// Type of the value
        /// </summary>
        public FieldType FieldType
        {
            get { return this.FieldTypeString.ToEnum<FieldType>(); }
            set { this.FieldTypeString = value.ToString(); }
        }
    }

    /// <summary>
    /// Field type
    /// </summary>
    [Serializable]
    public enum FieldType
    {
        /// <summary>
        /// single value
        /// </summary>
        single_value,
        /// <summary>
        /// multiple value
        /// </summary>
        multiple_values
    }
}
