using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Campaigns
{
    /// <summary>
    /// Represents a single Campaign in Constant Contact.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Campaign : Component
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name="id")]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets the name from.
        /// </summary>
        [DataMember(Name = "from_name")]
        public string FromName { get; set; }
        /// <summary>
        /// Gets or sets the email from.
        /// </summary>
        [DataMember(Name = "from_email")]
        public string FromEmail { get; set; }
        /// <summary>
        /// Gets or sets the reply email address.
        /// </summary>
        [DataMember(Name = "reply_to_email")]
        public string ReplyToEmail { get; set; }
        /// <summary>
        /// Gets or sets the campaign type.
        /// </summary>
        [DataMember(Name = "campaign_type")]
        public string CampaignType { get; set; }
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [DataMember(Name = "created_date")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the last send date.
        /// </summary>
        [DataMember(Name = "last_send_date")]
        public DateTime LastSendDate { get; set; }
        /// <summary>
        /// Gets or sets the last edit date.
        /// </summary>
        [DataMember(Name = "last_edit_date")]
        public DateTime LastEditDate { get; set; }
        /// <summary>
        /// Gets or sets the last run date.
        /// </summary>
        [DataMember(Name = "last_run_date")]
        public DateTime LastRunDate { get; set; }
        /// <summary>
        /// Gets or sets the next run date.
        /// </summary>
        [DataMember(Name = "next_run_date")]
        public DateTime NextRunDate { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the share page URL.
        /// </summary>
        [DataMember(Name = "share_page_url")]
        public string SharePageUrl { get; set; }
        /// <summary>
        /// Gets or sets the permission reminder flag.
        /// </summary>
        [DataMember(Name = "permission_reminder")]
        public bool IsPermissionReminderEnabled { get; set; }
        /// <summary>
        /// Gets or sets the permission reminder text.
        /// </summary>
        [DataMember(Name = "permission_reminder_text")]
        public string PermissionReminderText { get; set; }
        /// <summary>
        /// Gets or sets the view as web page flag.
        /// </summary>
        [DataMember(Name = "view_as_web_page")]
        public bool IsViewAsWebpageEnabled { get; set; }
        /// <summary>
        /// Gets or sets the view page text.
        /// </summary>
        [DataMember(Name = "view_as_web_page_text")]
        public bool ViewAsWebPageText { get; set; }
        /// <summary>
        /// Gets or sets the view page link.
        /// </summary>
        [DataMember(Name = "view_as_web_page_link")]
        public bool ViewAsWebPageLinkText { get; set; }
        /// <summary>
        /// Gets or sets the greeting.
        /// </summary>
        [DataMember(Name = "greetings_salutations")]
        public string GreetingSalutations { get; set; }
        /// <summary>
        /// Gets or sets the greeting name.
        /// </summary>
        [DataMember(Name = "greeting_name")]
        public string GreetingName { get; set; }
        /// <summary>
        /// Gets or sets the greeting string.
        /// </summary>
        [DataMember(Name = "greeting_string")]
        public string GreetingString { get; set; }
        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        [DataMember(Name = "email_content")]
        public string EmailContent { get; set; }
        /// <summary>
        /// Gets or sets the text content.
        /// </summary>
        [DataMember(Name = "text_content")]
        public string TextContent { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public Campaign() { }
    }
}
