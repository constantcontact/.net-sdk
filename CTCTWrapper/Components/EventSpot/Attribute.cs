using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// EventSpot Item Attribute
    /// </summary>
    [DataContract]
    [Serializable]
    public class Attribute : Component
    {
        /// <summary>
        /// The attribute's unique ID 
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Name of attribute being sold 
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Number of item attributes that are still available for sale 
        /// </summary>
        [DataMember(Name = "quantity_available", EmitDefaultValue = true)]
        public int QuantityAvailable { get; set; }

        /// <summary>
        /// Number of attributes offered for sale 
        /// </summary>
        [DataMember(Name = "quantity_total", EmitDefaultValue = true)]
        public int QuantityTotal { get; set; }
    }
}
