using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// Order class
    /// </summary>
    [DataContract]
    [Serializable]
    public class Order : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Order()
        {
            this.Fees = new List<Fee>();
        }

        /// <summary>
        /// Total
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = true)]
        public double Total { get; set; }

        /// <summary>
        /// Fees list
        /// </summary>
        [DataMember(Name = "fees", EmitDefaultValue = false)]
        public IList<Fee> Fees { get; set; }

        /// <summary>
        /// Order id
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public string OrderId { get; set; }

        /// <summary>
        /// Currency type
        /// </summary>
        [DataMember(Name = "currency_type", EmitDefaultValue = false)]
        public string CurrencyType { get; set; }

        /// <summary>
        /// String representation Order date
        /// </summary>
        [DataMember(Name = "order_date", EmitDefaultValue = false)]
        private string OrderDateString { get; set; }

        /// <summary>
        /// Order date
        /// </summary>
        public DateTime? OrderDate
        {
            get { return this.OrderDateString.FromISO8601String(); }
            set { this.OrderDateString = value.ToISO8601String(); }
        }
    }
}
