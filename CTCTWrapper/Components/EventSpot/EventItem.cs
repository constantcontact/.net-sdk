using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// EventItem class
    /// </summary>
    [DataContract]
    [Serializable]
    public class EventItem : Component
    {
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <summary>
        /// An array of item attributes and options 
        /// </summary>
        public IList<Attribute> Attributes 
        {
            get { return _Attributes; }
            private set { _Attributes = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Number of items available for sale, displayed on the registration page if show_quantity_available = true. 
        /// </summary>
        [DataMember(Name = "default_quantity_available", EmitDefaultValue = true)]
        public int DefaultQuantityAvailable { get; set; }

        /// <summary>
        /// The total quantity of items offered for sale, minimum = 0, cannot leave blank. 
        /// If the item has attributes, the summation of the quantity_total for all attributes automatically overwrites the value you enter here. 
        /// </summary>
        [DataMember(Name = "default_quantity_total", EmitDefaultValue = true)]
        public int DefaultQuantityTotal { get; set; }

        /// <summary>
        /// The item description that is shown on the registration page
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// The items unique ID 
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Item name that is shown on the registration page 
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The maximum quantity of this item that a registrant, as well as any of their guests, can purchase, minimum = 0, cannot leave blank; value cannot be greater than the value of default_quantity_available 
        /// </summary>
        [DataMember(Name = "per_registrant_limit", EmitDefaultValue = true)]
        public int PerRegistrantLimit { get; set; }

        /// <summary>
        /// The item cost, minimum = 0.00 
        /// </summary>
        [DataMember(Name = "price", EmitDefaultValue = true)]
        public double Price { get; set; }

        /// <summary>
        /// If true, displays the remaining quantity of this item for purchase on the registration page 
        /// </summary>
        [DataMember(Name = "show_quantity_available", EmitDefaultValue = true)]
        public bool ShowQuantityAvailable { get; set; }
    }
}
