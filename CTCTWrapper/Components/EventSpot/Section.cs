using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    [DataContract]
    [Serializable]
    public class Section : Component
    {
        /// <summary>
        /// Type of the value
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

        /// <summary>
        /// An array of field values if type = multiple_values 
        /// </summary>
        [DataMember(Name = "values", EmitDefaultValue = false)]
        public IList<string> Values { get; set; }

        /// <summary>
        /// Name of the field 
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Field value if type = single_value 
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        /// <summary>
        /// Field label displayed to viewers 
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        ///  An array of the fields displayed in a section: field_type, name, label, value, values
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public IList<Section> Fields { get; set; }

    }

    [Serializable]
    public enum FieldType
    {
        single_value,
        multiple_values
    }
}
