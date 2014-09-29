using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// IndividualEvent class
    /// </summary>
    [DataContract]
    [Serializable]
    public class IndividualEvent : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IndividualEvent()
        {
            this.Address = new EventSpotAddress();
            this.PaymentAddress = new EventSpotAddress();
            this.PaymentOptions = new List<PaymentTypes>();
            this.PaymentOptionsArray = new List<string>();
            this.TrackInformation = new TrackInformation();
            this.OnlineMeeting = new OnlineMeeting();
            this.NotificationOptions = new List<NotificationOptions>();
            this.Contact = new EventSpotContact();
        }

        /// <summary>
        /// String representation of date event was published or announced, in ISO-8601 format
        /// </summary>
        [DataMember(Name = "active_date", EmitDefaultValue = false)]
        private string ActiveDateString { get; set; }

        /// <summary>
        /// Date event was published or announced
        /// </summary>
        public DateTime? ActiveDate
        {
            get { return this.ActiveDateString.FromISO8601String(); }
            set { this.ActiveDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Set to true allows registrants to view others who have registered for the event, Default is false 
        /// </summary>
        [DataMember(Name = "are_registrants_public", EmitDefaultValue = true)]
        public bool AreRegistrantsPublic { get; set; }

        /// <summary>
        /// Date the event was cancelled in ISO-8601 format
        /// </summary>
        [DataMember(Name = "cancelled_date", EmitDefaultValue = false)]
        private string CancelledDateString { get; set; }

        /// <summary>
        /// Date the event was cancelled
        /// </summary>
        public DateTime? CancelledDate
        {
            get { return this.CancelledDateString.FromISO8601String(); }
            set { this.CancelledDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// String representation Date the event was created in ISO-8601 format
        /// </summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        private string CreatedDateString { get; set; }

        /// <summary>
        /// Date the event was created
        /// </summary>
        public DateTime? CreatedDate
        {
            get { return this.CreatedDateString.FromISO8601String(); }
            set { this.CreatedDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Currency that the account will be paid in; although this is not a required field, it has a default value of USD.
        /// Valid values are: USD, CAD, AUD, CHF, CZK, DKK, EUR, GBP, HKD, HUF, ILS, JPY, MXN, NOK, NZD, PHP, PLN, SEK, SGD, THB, TWD 
        /// </summary>
        [DataMember(Name = "currency_type", EmitDefaultValue = false)]
        public string CurrencyType { get; set; }

        /// <summary>
        /// String representation Date the event was deleted in ISO-8601 format 
        /// </summary>
        [DataMember(Name = "deleted_date", EmitDefaultValue = false)]
        private string DeletedDateString { get; set; }

        /// <summary>
        /// Date the event was deleted
        /// </summary>
        public DateTime? DeletedDate
        {
            get { return this.DeletedDateString.FromISO8601String(); }
            set { this.DeletedDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Provide a brief description of the event that will be visible on the event registration form and landing page 
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// String representation of the event end date, in ISO-8601 format
        /// </summary>
        [DataMember(Name = "end_date", EmitDefaultValue = false)]
        private string EndDateString { get; set; }

        /// <summary>
        /// The event end date
        /// </summary>
        public DateTime? EndDate
        {
            get { return this.EndDateString.FromISO8601String(); }
            set { this.EndDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Enter the Google analytics key if being used to track the event registration homepage 
        /// </summary>
        [DataMember(Name = "google_analytics_key", EmitDefaultValue = false)]
        public string GoogleAnalyticsKey { get; set; }

        /// <summary>
        /// Google merchant id to which payments are made; Google Checkout is not supported for new events, only valid on events created prior to October 2013
        /// </summary>
        [DataMember(Name = "google_merchant_id", EmitDefaultValue = false)]
        public string GoogleMerchantId { get; set; }

        /// <summary>
        /// Unique ID of the event 
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Set to true to display the event on the account's calendar; Default = true 
        /// </summary>
        [DataMember(Name = "is_calendar_displayed", EmitDefaultValue = true)]
        public bool IsCalendarDisplayed { get; set; }

        /// <summary>
        /// Set to true to enable registrant check-in, and indicate that the registrant attended the event; Default is false 
        /// </summary>
        [DataMember(Name = "is_checkin_available", EmitDefaultValue = true)]
        public bool IsCheckinAvailable { get; set; }

        /// <summary>
        /// Indicates if the event home/landing page is displayed for the event; set to true only if a landing page has been created for the event; Default is false 
        /// </summary>
        [DataMember(Name = "is_home_page_displayed", EmitDefaultValue = true)]
        public bool IsHomePageDisplayed { get; set; }

        /// <summary>
        /// Set to true to publish the event in external event directories such as SocialVents and EventsInAmerica; Default is false 
        /// </summary>
        [DataMember(Name = "is_listed_in_external_directory", EmitDefaultValue = true)]
        public bool IsListedInExternalDirectory { get; set; }

        /// <summary>
        /// For future usage, Default = true 
        /// </summary>
        [DataMember(Name = "is_map_displayed", EmitDefaultValue = true)]
        public bool IsMapDisplayed { get; set; }

        /// <summary>
        /// Set to true if this is an online event; Default is false 
        /// </summary>
        [DataMember(Name = "is_virtual_event", EmitDefaultValue = true)]
        public bool IsVirtualEvent { get; set; }

        /// <summary>
        /// Name of the venue or Location at which the event is being held 
        /// </summary>
        [DataMember(Name = "location", EmitDefaultValue = false)]
        public string Location { get; set; }

        /// <summary>
        /// Specify keywords to improve search engine optimization (SEO) for the event; use commas to separate multiple keywords 
        /// </summary>
        [DataMember(Name = "meta_data_tags", EmitDefaultValue = false)]
        public string MetaDataTags { get; set; }

        /// <summary>
        /// The event filename - not visible to registrants 
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Name to which registrants paying by check must make checks payable to; REQUIRED if 'CHECK' is selected as a payment option 
        /// </summary>
        [DataMember(Name = "payable_to", EmitDefaultValue = false)]
        public string PayableTo { get; set; }

        /// <summary>
        /// Email address linked to PayPal account to which payments will be made. REQUIRED if 'PAYPAL' is selected as a payment option 
        /// </summary>
        [DataMember(Name = "paypal_account_email", EmitDefaultValue = false)]
        public string PaypalAccountEmail { get; set; }

        /// <summary>
        /// The URL for the event registration form 
        /// </summary>
        [DataMember(Name = "registration_url", EmitDefaultValue = false)]
        public string RegistrationUrl { get; set; }

        /// <summary>
        /// String representation of the event start date, in ISO-8601 format
        /// </summary>
        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        private string StartDateString { get; set; }

        /// <summary>
        /// The event start date
        /// </summary>
        public DateTime? StartDate
        {
            get { return this.StartDateString.FromISO8601String(); }
            set { this.StartDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// String representation of the event status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        private string StatusString { get; set; }

        /// <summary>
        /// The event status
        /// </summary>
        public EventStatus Status
        {
            get { return this.StatusString.ToEnum<EventStatus>(); }
            set { this.StatusString = value.ToString(); }
        }

        /// <summary>
        /// The background and color theme for the event invitation, home page, and Registration form; default is Default 
        /// </summary>
        [DataMember(Name = "theme_name", EmitDefaultValue = false)]
        public string ThemeName { get; set; }

        /// <summary>
        /// Specify additional text to help describe the event time zone
        /// </summary>
        [DataMember(Name = "time_zone_description", EmitDefaultValue = false)]
        public string TimeZoneDescription { get; set; }

        /// <summary>
        /// Time zone in which the event occurs
        /// </summary>
        [DataMember(Name = "time_zone_id", EmitDefaultValue = false)]
        public string TimeZoneId { get; set; }

        /// <summary>
        /// The event title, visible to registrants 
        /// </summary>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// Number of event registrants 
        /// </summary>
        [DataMember(Name = "total_registered_count", EmitDefaultValue = true)]
        public int TotalRegisteredCount { get; set; }

        /// <summary>
        /// The event's Twitter hashtag 
        /// </summary>
        [DataMember(Name = "twitter_hash_tag", EmitDefaultValue = false)]
        public string TwitterHashTag { get; set; }

        /// <summary>
        /// String representation of the event type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        private string TypeString { get; set; }

        /// <summary>
        /// The event type
        /// </summary>
        public EventType Type
        {
            get { return this.TypeString.ToEnum<EventType>(); }
            set { this.TypeString = value.ToString(); }
        }

        /// <summary>
        /// String representation of the date the event was updated in ISO-8601 format
        /// </summary>
        [DataMember(Name = "updated_date", EmitDefaultValue = false)]
        private string UpdatedDateString { get; set; }

        /// <summary>
        /// Date the event was updated
        /// </summary>
        public DateTime? UpdatedDate
        {
            get { return this.UpdatedDateString.FromISO8601String(); }
            set { this.UpdatedDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// URI that points to the detailed description of that event
        /// </summary>
        [DataMember(Name = "event_detail_url", EmitDefaultValue = false)]
        public string EventDetailUrl { get; set; }

        /// <summary>
        /// Address specifying the event location, used to determine event location on map if is_map_displayed set to true
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public EventSpotAddress Address { get; set; }

        /// <summary>
        /// The event host's contact information 
        /// </summary>
        [DataMember(Name = "contact", EmitDefaultValue = false)]
        public EventSpotContact Contact { get; set; }

        /// <summary>
        /// Define whether or not event notifications are sent to the contact email_address, and which notifications. 
        /// </summary>
        [DataMember(Name = "notification_options", EmitDefaultValue = false)]
        public IList<NotificationOptions> NotificationOptions { get; set; }

        /// <summary>
        /// Online meeting details, REQUIRED if is_virtual_event is set to true 
        /// </summary>
        [DataMember(Name = "online_meeting", EmitDefaultValue = false)]
        public OnlineMeeting OnlineMeeting { get; set; }

        /// <summary>
        /// Address to which checks will be sent. REQUIRED if CHECK is selected as a payment option 
        /// </summary>
        [DataMember(Name = "payment_address", EmitDefaultValue = false)]
        public EventSpotAddress PaymentAddress { get; set; }

        /// <summary>
        /// Use these settings to define the information displayed on the Event registration page 
        /// </summary>
        [DataMember(Name = "track_information", EmitDefaultValue = false)]
        public TrackInformation TrackInformation { get; set; }

        /// <summary>
        /// Specifies the payment options available to registrants 
        /// </summary>
        [DataMember(Name = "payment_options", EmitDefaultValue = false)]
        private IList<string> PaymentOptionsArray { get; set; }

        /// <summary>
        /// Specifies the payment options available to registrants 
        /// </summary>
        public IList<PaymentTypes> PaymentOptions 
        {
            get
            {
                IList<PaymentTypes> temp = new List<PaymentTypes>();
                foreach (string s in this.PaymentOptionsArray)
                {
                    var value = s.ToEnum<PaymentTypes>();
                    temp.Add(value);
                }
                return temp;
            }
            set
            {
                IList<string> temp = new List<string>();
                foreach (PaymentTypes pt in value)
                {
                    temp.Add(pt.ToString());
                }
                this.PaymentOptionsArray = temp;
            }
        }
    }



    /// <summary>
    /// Event status
    /// </summary>
    [Serializable]
    public enum EventStatus
    {
        /// <summary>
        /// Draft.
        /// </summary>
        DRAFT,
        /// <summary>
        /// Active.
        /// </summary>
        ACTIVE,
        /// <summary>
        /// Complete.
        /// </summary>
        COMPLETE,
        /// <summary>
        /// Cancelled.
        /// </summary>
        CANCELLED,
        /// <summary>
        /// Deleted
        /// </summary>
        DELETED
    }

#pragma warning disable 1591
    /// <summary>
    /// Event type
    /// </summary>
    [Serializable]
    public enum EventType
    {
        AUCTION,
        BIRTHDAY,
        BUSINESS_FINANCE_SALES,
        CLASSES_WORKSHOPS,
        COMPETITION_SPORTS,
        CONFERENCES_SEMINARS_FORUM,
        CONVENTIONS_TRADESHOWS_EXPOS,
        FESTIVALS_FAIRS,
        FOOD_WINE,
        FUNDRAISERS_CHARITIES,
        HOLIDAY,
        INCENTIVE_REWARD_RECOGNITION,
        MOVIES_FILM,
        MUSIC_CONCERTS,
        NETWORKING_CLUBS,
        PERFORMING_ARTS,
        OUTDOORS_RECREATION,
        RELIGION_SPIRITUALITY,
        SCHOOLS_REUNIONS_ALUMNI,
        PARTIES_SOCIAL_EVENTS_MIXERS,
        TRAVEL,
        WEBINAR_TELESEMINAR_TELECLASS,
        WEDDINGS,
        OTHER
    }

    /// <summary>
    /// Payment Types
    /// </summary>
    public enum PaymentTypes
    {
        [Obsolete("This type is not supported for new events starting November 2013.", false)]
        ONLINE_CREDIT_CARD_PROCESSOR,
        PAYPAL,
        GOOGLE_CHECKOUT,
        CHECK,
        DOOR
    }
#pragma warning restore 1591
}
