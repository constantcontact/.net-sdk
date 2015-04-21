using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// Custom field class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class CustomField : Component
    {
        /// <summary>
        /// Name of the custom field. Only accepted names.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
        /// <summary>
        /// Value of the custom field.
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; } 
    }

    /// <summary>
    /// Custom fields names structure.
    /// </summary>
    public struct CustomFieldName
    {
        /// <summary>
        /// Custom field 1.
        /// </summary>
        public const string CustomField1 = "CustomField1";
        /// <summary>
        /// Custom field 2.
        /// </summary>
        public const string CustomField2 = "CustomField2";
        /// <summary>
        /// Custom field 3.
        /// </summary>
        public const string CustomField3 = "CustomField3";
        /// <summary>
        /// Custom field 4.
        /// </summary>
        public const string CustomField4 = "CustomField4";
        /// <summary>
        /// Custom field 5.
        /// </summary>
        public const string CustomField5 = "CustomField5";
        /// <summary>
        /// Custom field 6.
        /// </summary>
        public const string CustomField6 = "CustomField6";
        /// <summary>
        /// Custom field 7.
        /// </summary>
        public const string CustomField7 = "CustomField7";
        /// <summary>
        /// Custom field 8.
        /// </summary>
        public const string CustomField8 = "CustomField8";
        /// <summary>
        /// Custom field 9.
        /// </summary>
        public const string CustomField9 = "CustomField9";
        /// <summary>
        /// Custom field 10.
        /// </summary>
        public const string CustomField10 = "CustomField10";
        /// <summary>
        /// Custom field 11.
        /// </summary>
        public const string CustomField11 = "CustomField11";
        /// <summary>
        /// Custom field 12.
        /// </summary>
        public const string CustomField12 = "CustomField12";
        /// <summary>
        /// Custom field 13.
        /// </summary>
        public const string CustomField13 = "CustomField13";
        /// <summary>
        /// Custom field 14.
        /// </summary>
        public const string CustomField14 = "CustomField14";
        /// <summary>
        /// Custom field 15.
        /// </summary>
        public const string CustomField15 = "CustomField15"; 
    }
}
