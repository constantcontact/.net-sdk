using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CTCT.Components.Tracking;
using CTCT.Components.Contacts;
using CTCT.Util;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Represents a single Campaign in Constant Contact.
    /// </summary>
    [DataContract]
    public class EmailCampaign : Component
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [DataMember(Name = "subject", EmitDefaultValue = false)]
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets the name from.
        /// </summary>
        [DataMember(Name = "from_name", EmitDefaultValue = false)]
        public string FromName { get; set; }
        /// <summary>
        /// Gets or sets the email from.
        /// </summary>
        [DataMember(Name = "from_email", EmitDefaultValue = false)]
        public string FromEmail { get; set; }
        /// <summary>
        /// Gets or sets the reply email address.
        /// </summary>
        [DataMember(Name = "reply_to_email", EmitDefaultValue = false)]
        public string ReplyToEmail { get; set; }
        /// <summary>
        /// Campaign type, string representation.
        /// </summary>
        [DataMember(Name = "template_type", EmitDefaultValue = false)]
        private string TemplateTypeString { get; set; }
        /// <summary>
        /// Gets or sets the campaign type.
        /// </summary>
        public TemplateType TemplateType
        {
            get { return this.TemplateTypeString.ToEnum<TemplateType>(); }
            set { this.TemplateTypeString = value.ToString(); }
        }
        /// <summary>
        /// Created date string representation.
        /// </summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        private string CreatedDateString { get; set; }
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime? CreatedDate
        {
            get { return this.CreatedDateString.FromISO8601String(); }
            set { this.CreatedDateString = value.ToISO8601String(); }
        }
        /// <summary>
        /// String representation of modification date.
        /// </summary>
        [DataMember(Name = "modified_date", EmitDefaultValue = false)]
        private string ModifiedDateString { get; set; }
        /// <summary>
        /// Gets or sets the modification date.
        /// </summary>
        public DateTime? ModifiedDate
        {
            get { return this.ModifiedDateString.FromISO8601String(); }
            set { this.ModifiedDateString = value.ToISO8601String(); }
        }
        /// <summary>
        /// String representation of last run date.
        /// </summary>
        [DataMember(Name = "last_run_date", EmitDefaultValue = false)]
        private string LastRunDateString { get; set; }
        /// <summary>
        /// Gets or sets the last run date.
        /// </summary>
        public DateTime? LastRunDate
        {
            get { return this.LastRunDateString.FromISO8601String(); }
            set { this.LastRunDateString = value.ToISO8601String(); }
        }
        /// <summary>
        /// String representation of next run date.
        /// </summary>
        [DataMember(Name = "next_run_date", EmitDefaultValue = false)]
        private string NextRunDateString { get; set; }
        /// <summary>
        /// Gets or sets the next run date.
        /// </summary>
        public DateTime? NextRunDateDate
        {
            get { return NextRunDateString.FromISO8601String(); }
            set { NextRunDateString = value.ToISO8601String(); }
        }
        /// <summary>
        /// Campaign status, string representation.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        private string StatusString { get; set; }
        /// <summary>
        /// Gets or sets the campaign status.
        /// </summary>
        public CampaignStatus Status
        {
            get { return this.StatusString.ToEnum<CampaignStatus>(); }
            set { this.StatusString = value.ToString(); }
        }
        /// <summary>
        /// Gets or sets the permission for web page.
        /// </summary>
        [DataMember(Name = "is_view_as_webpage_enabled", EmitDefaultValue = false)]
        public bool IsViewAsWebPageEnabled { get; set; }
        /// <summary>
        /// Gets or sets the permission reminder text.
        /// </summary>
        [DataMember(Name = "permission_reminder_text", EmitDefaultValue = false)]
        public string PermissionReminderText { get; set; }
        /// <summary>
        /// Gets or sets the view page text.
        /// </summary>
        [DataMember(Name = "view_as_web_page_text", EmitDefaultValue = false)]
        public string ViewAsWebPageText { get; set; }
        /// <summary>
        /// Gets or sets the view page link.
        /// </summary>
        [DataMember(Name = "view_as_web_page_link_text", EmitDefaultValue = false)]
        public string ViewAsWebPageLinkText { get; set; }
        /// <summary>
        /// Gets or sets the greeting.
        /// </summary>
        [DataMember(Name = "greeting_salutations", EmitDefaultValue = false)]
        public string GreetingSalutations { get; set; }
        /// <summary>
        /// Greeting name, string representation.
        /// </summary>
        [DataMember(Name = "greeting_name", EmitDefaultValue = false)]
        private string GreetingNameString { get; set; }
        /// <summary>
        /// Gets or sets the greeting name.
        /// </summary>
        public GreetingName GreetingName
        {
            get { return this.GreetingNameString.ToEnum<GreetingName>(); }
            set { this.GreetingNameString = value.ToString(); }
        }
        /// <summary>
        /// Gets or sets o non-expiring link to use for sharing a sent email campaign using social channels
        /// </summary>
        [DataMember(Name = "permalink_url", EmitDefaultValue = false)]
        public string PermanentLink { get; set; }
        /// <summary>
        /// Gets or sets the greeting string.
        /// </summary>
        [DataMember(Name = "greeting_string", EmitDefaultValue = false)]
        public string GreetingString { get; set; }
        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        [DataMember(Name = "email_content", EmitDefaultValue = false)]
        public string EmailContent { get; set; }
        /// <summary>
        /// String representation of email content format.
        /// </summary>
        [DataMember(Name = "email_content_format", EmitDefaultValue = false)]
        private string EmailContentFormatString { get; set; }
        /// <summary>
        /// Gets or sets the email content format.
        /// </summary>
        public CampaignEmailFormat EmailContentFormat
        {
            get { return this.EmailContentFormatString.ToEnum<CampaignEmailFormat>(); }
            set { this.EmailContentFormatString = value.ToString(); }
        }
        /// <summary>
        /// Gets or sets the text content.
        /// </summary>
        [DataMember(Name = "text_content", EmitDefaultValue = false)]
        public string TextContent { get; set; }
        /// <summary>
        /// Gets or sets the syle sheet.
        /// </summary>
        [DataMember(Name = "style_sheet", EmitDefaultValue = false)]
        public string StyleSheet { get; set; }
        /// <summary>
        /// Gets or sets the tracking summary.
        /// </summary>
        [DataMember(Name = "tracking_summary", EmitDefaultValue = false)]
        public TrackingSummary TrackingSummary { get; set; }
        /// <summary>
        /// Click through details for each link in this email campaign.
        /// </summary>
        [DataMember(Name = "click_through_details", EmitDefaultValue=false)]
        public IList<ClickThroughDetails> ClickThroughDetails { get; set; }
        /// <summary>
        /// Gets or sets the message footer.
        /// </summary>
        [DataMember(Name = "message_footer")]
        public MessageFooter MessageFooter { get; set; }
        /// <summary>
        /// Gets or sets the lists where the campaign is registered.
        /// </summary>
        [DataMember(Name = "sent_to_contact_lists", EmitDefaultValue = false)]
        public IList<SentContactList> Lists { get; set; }
        /// <summary>
        /// Gets or sets the flag for UI visibility.
        /// </summary>
        [DataMember(Name = "is_visible_in_ui", EmitDefaultValue = false)]
        public bool? IsVisibleInUI { get; set; }
        /// <summary>
        /// Gets or sets the archive status.
        /// </summary>
        [DataMember(Name = "archive_status", EmitDefaultValue = false)]
        public string ArchiveStatus { get; set; }
        /// <summary>
        /// Gets or sets the archive URL.
        /// </summary>
        [DataMember(Name = "archive_url", EmitDefaultValue = false)]
        public string ArchiveURL { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public EmailCampaign() { }
    }

    /// <summary>
    /// Greeting name.
    /// </summary>
    public enum GreetingName
    {
        /// <summary>
        /// None.
        /// </summary>
        NONE,
        /// <summary>
        /// First name.
        /// </summary>
        FIRST_NAME,
        /// <summary>
        /// Last name.
        /// </summary>
        LAST_NAME,
        /// <summary>
        /// First and last name.
        /// </summary>
        FIRST_AND_LAST_NAME
    }

    /// <summary>
    /// Campaign type.
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// Custom type.
        /// </summary>
        CUSTOM,
        /// <summary>
        /// Stock type.
        /// </summary>
        STOCK
    }

    /// <summary>
    /// Campaign status.
    /// </summary>
    public enum CampaignStatus
    {
        /// <summary>
        /// Draft.
        /// </summary>
        DRAFT,
        /// <summary>
        /// Running.
        /// </summary>
        RUNNING,
        /// <summary>
        /// Sent.
        /// </summary>
        SENT,
        /// <summary>
        /// Scheduled.
        /// </summary>
        SCHEDULED
    }

    /// <summary>
    /// Campaign email format.
    /// </summary>
    public enum CampaignEmailFormat
    {
        /// <summary>
        /// HTML.
        /// </summary>
        HTML,
        /// <summary>
        /// XHTML.
        /// </summary>
        XHTML
    }
}
