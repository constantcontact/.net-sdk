using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Util;
using System.Runtime.Serialization;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Promocode class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Promocode : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Promocode()
        {
            this.FeeIds = new List<string>();
        }

        /// <summary>
        /// Name of the promotional code visible to registrants, between 4 - 12 characters, cannot contain spaces or special character (_ is OK); each code_name must be unique 
        /// </summary>
        [DataMember(Name = "code_name", EmitDefaultValue = false)]
        public string CodeName { get; set; }

        /// <summary>
        ///  Type of promocode:
        ///  ACCESS - applies to a specific fee with has_restricted_access = true, fee_list must include only a single fee_id. See Event Fees
        ///  DISCOUNT - when set to DISCOUNT, you must specify either a discount_percent or a discount_amount
        /// </summary>
        [DataMember(Name = "code_type", EmitDefaultValue = false)]
        private string CodeTypeString { get; set; }

        /// <summary>
        ///  Type of promocode:
        ///  ACCESS - applies to a specific fee with has_restricted_access = true, fee_list must include only a single fee_id. See Event Fees
        ///  DISCOUNT - when set to DISCOUNT, you must specify either a discount_percent or a discount_amount
        /// </summary>
        public CodeType CodeType
        {
            get { return this.CodeTypeString.ToEnum<CodeType>(); }
            set { this.CodeTypeString = value.ToString(); }
        }

        /// <summary>
        /// Specifies a fixed discount amount, minimum of 0.01, is required when code_type = DISCOUNT, but not using discount_percent
        /// </summary>
        [DataMember(Name = "discount_amount", EmitDefaultValue = false)]
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Specifies a discount percentage, from 1% - 100%, is required when code_type = DISCOUNT, but not using discount_amount
        /// </summary>
        [DataMember(Name = "discount_percent", EmitDefaultValue = false)]
        public int DiscountPercent { get; set; }

        /// <summary>
        ///  Required when code_type = DISCOUNT;
        ///  FEE_LIST - discount is applied only to those fees listed in the fee_ids array ORDER_TOTAL - discount is applied to the order total
        /// </summary>
        [DataMember(Name = "discount_scope", EmitDefaultValue = false)]
        private string DiscountScopeString { get; set; }

        /// <summary>
        ///  Required when code_type = DISCOUNT;
        ///  FEE_LIST - discount is applied only to those fees listed in the fee_ids array ORDER_TOTAL - discount is applied to the order total
        /// </summary>
        public DiscountScope DiscountScope
        {
            get { return this.DiscountScopeString.ToEnum<DiscountScope>(); }
            set { this.DiscountScopeString = value.ToString(); }
        }

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
        /// Identifies the fees to which the promocode applies; 
        /// </summary>
        [DataMember(Name = "fee_ids", EmitDefaultValue = false)]
        public IList<string> FeeIds { get; set; }

        /// <summary>
        /// Unique ID for the event promotional code 
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// When set to true, promocode cannot be redeemed; when false, promocode can be redeemed; default = false
        /// </summary>
        [DataMember(Name = "is_paused", EmitDefaultValue = true)]
        public bool IsPaused { get; set; }

        /// <summary>
        /// Number of promocodes available for redemption; -1 = unlimited 
        /// </summary>
        [DataMember(Name = "quantity_available", EmitDefaultValue = true)]
        public int QuantityAvailable { get; set; }

        /// <summary>
        /// Total number of promocodes available for redemption; -1 = unlimited
        /// </summary>
        [DataMember(Name = "quantity_total", EmitDefaultValue = true)]
        public int QuantityTotal { get; set; }

        /// <summary>
        /// Number of promocodes that have been redeemed; starts at 0
        /// </summary>
        [DataMember(Name = "quantity_used", EmitDefaultValue = true)]
        public int QuantityUsed { get; set; }

        /// <summary>
        /// Status of the promocode
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        private string StatusString { get; set; }

        /// <summary>
        /// Status of the promocode
        /// </summary>
        public PromocodeStatus Status
        {
            get { return this.StatusString.ToEnum<PromocodeStatus>(); }
            set { this.StatusString = value.ToString(); }
        }
    }

    /// <summary>
    /// Type of promocode
    /// </summary>
    [Serializable]
    public enum CodeType
    {
        /// <summary>
        /// applies to a specific fee with has_restricted_access = true, fee_list must include only a single fee_id. See Event Fees
        /// </summary>
        ACCESS,
        /// <summary>
        /// when set to DISCOUNT, you must specify either a discount_percent or a discount_amount
        /// </summary>
        DISCOUNT
    }

    /// <summary>
    /// Discount Scope
    /// </summary>
    [Serializable]
    public enum DiscountScope
    {
        /// <summary>
        ///  discount is applied only to those fees listed in the fee_ids array
        /// </summary>
        FEE_LIST,
        /// <summary>
        /// discount is applied to the order total
        /// </summary>
        ORDER_TOTAL
    }

    /// <summary>
    /// Discount Type
    /// </summary>
    [Serializable]
    public enum DiscountType
    {
        /// <summary>
        /// discount is a percentage specified by discount_percent
        /// </summary>
        PERCENT,
        /// <summary>
        /// discount is a fixed amount, specified by discount_amount
        /// </summary>
        AMOUNT 
    }

    /// <summary>
    /// Promocode Status
    /// </summary>
    [Serializable]
    public enum PromocodeStatus
    {
        /// <summary>
        /// promocode is available to be redeemed
        /// </summary>
        LIVE,
        /// <summary>
        /// promocode is not available for redemption
        /// </summary>
        PAUSED,
        /// <summary>
        /// no more promocodes remain, 
        /// </summary>
        DEPLETED 
    }
}
