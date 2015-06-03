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
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        private List<Field> _Fields = new List<Field>();

        /// <summary>
        /// Field label displayed to viewers 
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        ///  An array of the fields displayed in a section: field_type, name, label, value, values
        /// </summary>
        public IList<Field> Fields 
        {
            get { return _Fields; }
            set { _Fields = value == null ? null : value.ToList(); }
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
