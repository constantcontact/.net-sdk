using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using CTCT.Webohooks.Helper;
using CTCT.Components;
using System.Runtime.Serialization;

namespace CTCT.Webohooks.Model
{
    /// <summary>
    /// Represents a notification object sent to client through Webhooks Notifications.
    /// </summary>
    [DataContract]
    [Serializable]
    public class BillingChangeNotification : Component
    {
        #region Propertiese

        /// <summary>
        /// Notification url
        /// </summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }

        /// <summary>
        /// Event type string
        /// </summary>
        [DataMember(Name = "event_type", EmitDefaultValue = false)]
        private string EventTypeString { get; set; }

        /// <summary>
        /// Event type
        /// </summary>
        public BillingChangeNotificationType EventType
        {
            get
            {
                return (BillingChangeNotificationType)StringEnum.Parse(typeof(BillingChangeNotificationType), EventTypeString, true);
            }
            set
            {
                EventTypeString = StringEnum.GetStringValue(value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BillingChangeNotification [url=");
            builder.Append(Url);
            builder.Append(", event_type=");
            builder.Append(EventType);
            builder.Append("]");

            return builder.ToString();
        }

        #endregion
    }

    /// <summary>
    /// Event types for billing change notification.
    /// </summary>
    public enum BillingChangeNotificationType
    {
        [StringValue("tier.increase")]
        TierIncrease,

        [StringValue("tier.decrease")]
        TierDecrease
    }
}
