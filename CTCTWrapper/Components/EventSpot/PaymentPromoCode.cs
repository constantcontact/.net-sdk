using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// PaymentPromoCode class
    /// </summary>
    [DataContract]
    [Serializable]
    public class PaymentPromoCode : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentPromoCode()
        {
            this.PromoCodeInfo = new PromoCodeInfo();
        }

        /// <summary>
        /// Total discount
        /// </summary>
        [DataMember(Name = "total_discount", EmitDefaultValue = true)]
        public double TotalDiscount { get; set; }

        /// <summary>
        /// Promo code info
        /// </summary>
        [DataMember(Name = "promo_code_info", EmitDefaultValue = false)]
        public PromoCodeInfo PromoCodeInfo { get; set; }

    }
}
