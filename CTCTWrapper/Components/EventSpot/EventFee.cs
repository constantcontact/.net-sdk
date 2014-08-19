using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// EventFee class
    /// </summary>
    [DataContract]
    [Serializable]
    public class EventFee : Component
    {
        /// <summary>
        /// Fee for registrations that occur prior to the event's early_fee_date 
        /// </summary>
        [DataMember(Name = "early_fee", EmitDefaultValue = false)]
        public double EarlyFee { get; set; }

        /// <summary>
        /// The fee amount 
        /// </summary>
        [DataMember(Name = "fee", EmitDefaultValue = false)]
        public double Fee { get; set; }

        /// <summary>
        /// String representation Specifies who the fee applies to
        /// </summary>
        [DataMember(Name = "fee_scope", EmitDefaultValue = false)]
        private string FeeScopeString { get; set; }

        /// <summary>
        /// Specifies who the fee applies to
        /// </summary>
        public FeeScope FeeScope
        {
            get { return this.FeeScopeString.ToEnum<FeeScope>(); }
            set { this.FeeScopeString = value.ToString(); }
        }

        /// <summary>
        /// Unique ID for that fee 
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Fee description displayed to event registrants 
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// Fee for registrations that occur after the event's late_fee_date 
        /// </summary>
        [DataMember(Name = "late_fee", EmitDefaultValue = true)]
        public double LateFee { get; set; }

        /// <summary>
        /// Has restricted access
        /// </summary>
        [DataMember(Name = "has_restricted_access", EmitDefaultValue = true)]
        public bool HasRestrictedAccess { get; set; }
    }

    /// <summary>
    /// Specifies who the fee applies to
    /// </summary>
    [Serializable]
    public enum FeeScope
    {
        /// <summary>
        /// Fee applies to Registrants and Guests
        /// </summary>
        BOTH,
        /// <summary>
        /// Fee applies to registrants only
        /// </summary>
        REGISTRANTS,
        /// <summary>
        /// Fee applies to guests only
        /// </summary>
        GUESTS
    }
}
