using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// SaleItem class
    /// </summary>
    [DataContract]
    [Serializable]
    public class SaleItem : Component
    {
        /// <summary>
        /// Amount paid
        /// </summary>
        [DataMember(Name = "amount", EmitDefaultValue = true)]
        public double Amount { get; set; }

        /// <summary>
        /// Fee ID 
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        [DataMember(Name = "promo_type", EmitDefaultValue = false)]
        public string PromoType { get; set; }

        /// <summary>
        /// Name of registrant or guest 
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Number of amount required
        /// </summary>
        [DataMember(Name = "quantity", EmitDefaultValue = true)]
        public int Quantity { get; set; }

        /// <summary>
        /// Type of fees 
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Fee period type
        /// </summary>
        [DataMember(Name = "fee_period_type", EmitDefaultValue = false)]
        public string FeePeriodType { get; set; }
    }
}
