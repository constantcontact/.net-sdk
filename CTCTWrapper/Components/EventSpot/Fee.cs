using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Fee class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Fee : Component
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [DataMember(Name = "quantity", EmitDefaultValue = true)]
        public int Quantity { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [DataMember(Name = "amount", EmitDefaultValue = true)]
        public float Amount { get; set; }

        /// <summary>
        /// Fee period type
        /// </summary>
        [DataMember(Name = "fee_period_type", EmitDefaultValue = false)]
        public string FeePeriodType { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        [DataMember(Name = "promo_type", EmitDefaultValue = false)]
        public string PromoType { get; set; }
        
    }
}
