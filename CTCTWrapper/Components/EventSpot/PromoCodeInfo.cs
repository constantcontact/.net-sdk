using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// PromoCodeInfo class
    /// </summary>
    [DataContract]
    [Serializable]
    public class PromoCodeInfo : Component
    {
        /// <summary>
        /// Code name
        /// </summary>
        [DataMember(Name = "code_name", EmitDefaultValue = false)]
        public string CodeName { get; set; }

        /// <summary>
        /// String representation Code type
        /// </summary>
        [DataMember(Name = "code_type", EmitDefaultValue = false)]
        private string CodeTypeString { get; set; }

        /// <summary>
        /// Code type
        /// </summary>
        public CodeType CodeType
        {
            get { return this.CodeTypeString.ToEnum<CodeType>(); }
            set { this.CodeTypeString = value.ToString(); }
        }

        /// <summary>
        /// Redemption count
        /// </summary>
        [DataMember(Name = "redemption_count", EmitDefaultValue = true)]
        public int RedemptionCount { get; set; }

        /// <summary>
        /// Discount percent
        /// </summary>
        [DataMember(Name = "discount_percent", EmitDefaultValue = true)]
        public double DiscountPercent { get; set; }

        /// <summary>
        /// Discount amount 
        /// </summary>
        [DataMember(Name = "discount_amount", EmitDefaultValue = true)]
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Discount types
        /// </summary>
        [DataMember(Name = "discount_type", EmitDefaultValue = false)]
        private string DiscountTypeString { get; set; }

        /// <summary>
        /// Discount types
        /// </summary>
        public DiscountType DiscountType
        {
            get { return this.DiscountTypeString.ToEnum<DiscountType>(); }
            set { this.DiscountTypeString = value.ToString(); }
        }

        /// <summary>
        /// Discount scope
        /// </summary>
        [DataMember(Name = "discount_scope", EmitDefaultValue = false)]
        public string DiscountScope { get; set; }
    }
}
