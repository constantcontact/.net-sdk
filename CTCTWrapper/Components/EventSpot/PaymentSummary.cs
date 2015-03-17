using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Payment summary
    /// </summary>
    [DataContract]
    [Serializable]
    public class PaymentSummary : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentSummary()
        {
            this.Order = new Order();
            this.PromoCode = new PaymentPromoCode();
        }
 
        /// <summary>
        /// Order
        /// </summary>
        [DataMember(Name = "order", EmitDefaultValue = false)]
        public Order Order { get; set; }

        /// <summary>
        /// Payment status
        /// </summary>
        [DataMember(Name = "payment_status", EmitDefaultValue = false)]
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Payment type
        /// </summary>
        [DataMember(Name = "payment_type", EmitDefaultValue = false)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Promo code
        /// </summary>
        [DataMember(Name = "promo_code", EmitDefaultValue = false)]
        public PaymentPromoCode PromoCode { get; set; }
    }
}
