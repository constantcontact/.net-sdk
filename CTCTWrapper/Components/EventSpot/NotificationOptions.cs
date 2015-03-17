using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// NotificationOptions class
    /// </summary>
    [DataContract]
    [Serializable]
    public class NotificationOptions : Component
    {
        /// <summary>
        /// Set to true to send event notifications to the contact email_address, false for no notifications; Default is false 
        /// </summary>
        [DataMember(Name = "is_opted_in", EmitDefaultValue = true)]
        public bool IsOptedIn { get; set; }

        /// <summary>
        /// String representation of the type of notifications sent to the contact email_address, valid values: SO_REGISTRATION_NOTIFICATION - send notice for each registration (Default) 
        /// </summary>
        [DataMember(Name = "notification_type", EmitDefaultValue = false)]
        private string NotificationTypeString { get; set; }

        /// <summary>
        /// Specifies the type of notifications sent to the contact email_address
        /// </summary>
        public NotificationType NotificationType
        {
            get { return this.NotificationTypeString.ToEnum<NotificationType>(); }
            set { this.NotificationTypeString = value.ToString(); }
        } 
    }

    /// <summary>
    /// Specifies the type of notifications sent to the contact email_address
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// So registrant notification
        /// </summary>
        SO_REGISTRATION_NOTIFICATION
    }
}
