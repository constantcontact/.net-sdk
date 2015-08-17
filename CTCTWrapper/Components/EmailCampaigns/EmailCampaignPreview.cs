using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Represents a content to preview an existing email campaign
    /// </summary>
    [DataContract]
    public class EmailCampaignPreview : Component
    {
        /// <summary>
        /// The email address the email campaign originated from
        /// </summary>
        [DataMember(Name = "from_email", EmitDefaultValue = false)]
        public string FromEmail { get; set; }

        /// <summary>
        /// The preview of the HTML version of the email campaign
        /// </summary>
        [DataMember(Name = "preview_email_content", EmitDefaultValue = false)]
        public string PreviewEmailContent { get; set; }

        /// <summary>
        /// The preview of the text-only version of the email campaign
        /// </summary>
        [DataMember(Name = "preview_text_content", EmitDefaultValue = false)]
        public string PreviewTextContent { get; set; }

        /// <summary>
        /// The reply-to email address for the email campaign
        /// </summary>
        [DataMember(Name = "reply_to_email", EmitDefaultValue = false)]
        public string ReplyToEmail { get; set; }

        /// <summary>
        /// The subject line content for the message
        /// </summary>
        [DataMember(Name = "subject", EmitDefaultValue = false)]
        public string Subject { get; set; }
    }
}
