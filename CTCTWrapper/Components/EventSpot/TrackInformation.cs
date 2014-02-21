using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EventSpot
{
    /// <summary>
    /// TrackInformation class
    /// </summary>
    [DataContract]
    [Serializable]
    public class TrackInformation : Component
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TrackInformation()
        {
            this.InformationSectionsArray = new List<string>();
            this.InformationSections = new List<InformationSections>();
        }

        /// <summary>
        /// Date on which early fees end, in ISO-8601 format 
        /// </summary>
        [DataMember(Name = "early_fee_date", EmitDefaultValue = false)]
        private string EarlyFeeDateString { get; set; }

        /// <summary>
        /// Date on which early fees end
        /// </summary>
        public DateTime? EarlyFeeDate
        {
            get { return this.EarlyFeeDateString.FromISO8601String(); }
            set { this.EarlyFeeDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Default = Guest(s); How guests are referred to on the registration form; use your own, or one of the following suggestions are 
        /// Associate(s), Camper(s), Child(ren), Colleague(s), Employee(s), Friend(s), Guest(s), Member(s), Participant(s), Partner(s), Player(s), Spouse(s), Student(s), Teammate(s), Volunteer(s) 
        /// </summary>
        [DataMember(Name = "guest_display_label", EmitDefaultValue = false)]
        public string GuestDisplayLabel { get; set; }

        /// <summary>
        /// Number of guests each registrant can bring, 0 - 100, default = 0 
        /// </summary>
        [DataMember(Name = "guest_limit", EmitDefaultValue = true)]
        public int GuestLimit { get; set; }

        /// <summary>
        /// Default = false; Set to true to display the guest count field on the registration form; if true, is_guest_name_required must be set to false (default)
        /// </summary>
        [DataMember(Name = "is_guest_anonymous_enabled", EmitDefaultValue = true)]
        public bool IsGuestAnonymousEnabled { get; set; }

        /// <summary>
        /// Default = false. Set to display guest name fields on registration form; if true, then is_guest_anonymous_enabled must be set false (default)
        /// </summary>
        [DataMember(Name = "is_guest_name_required", EmitDefaultValue = true)]
        public bool IsGuestNameRequired { get; set; }

        /// <summary>
        /// Default = false; Manually closes the event registration when set to true, takes precedence over registration_limit_date and registration_limit_count settings 
        /// </summary>
        [DataMember(Name = "is_registration_closed_manually", EmitDefaultValue = true)]
        public bool IsRegistrationClosedManually { get; set; }

        /// <summary>
        /// Default = false; Set to true provide a link for registrants to retrieve an event ticket after they register
        /// </summary>
        [DataMember(Name = "is_ticketing_link_displayed", EmitDefaultValue = true)]
        public bool IsTicketingLinkDisplayed { get; set; }

        /// <summary>
        /// String representation of the date after which late fees apply, in ISO-8601 format 
        /// </summary>
        [DataMember(Name = "late_fee_date", EmitDefaultValue = false)]
        private string LateFeeDateString { get; set; }

        /// <summary>
        /// Date after which late fees apply
        /// </summary>
        public DateTime? LateFeeDate
        {
            get { return this.LateFeeDateString.FromISO8601String(); }
            set { this.LateFeeDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Specifies the maximum number of registrants for the event 
        /// </summary>
        [DataMember(Name = "registration_limit_count", EmitDefaultValue = true)]
        public int RegistrationLimitCount { get; set; }

        /// <summary>
        /// String representation of the date when event registrations close, in ISO-8601 format 
        /// </summary>
        [DataMember(Name = "registration_limit_date", EmitDefaultValue = false)]
        private string RegistrationLimitDateString { get; set; }

        /// <summary>
        /// Date when event registrations close
        /// </summary>
        public DateTime? RegistrationLimitDate
        {
            get { return this.RegistrationLimitDateString.FromISO8601String(); }
            set { this.RegistrationLimitDateString = value.ToISO8601String(); }
        }

        /// <summary>
        ///  Determines if the Who (CONTACT), When (TIME), or Where (LOCATION) information is shown on the Event page. Default settings are CONTACT, TIME, and LOCATION 
        ///  valid values are: CONTACT - displays the event contact informationTIME - displays the event date and time
        ///  LOCATION - displays the event location
        /// </summary>
        [DataMember(Name = "information_sections", EmitDefaultValue = false)]
        private IList<string> InformationSectionsArray { get; set; }

        /// <summary>
        ///  Determines if the Who (CONTACT), When (TIME), or Where (LOCATION) information is shown on the Event page. Default settings are CONTACT, TIME, and LOCATION 
        ///  valid values are: CONTACT - displays the event contact informationTIME - displays the event date and time
        ///  LOCATION - displays the event location
        /// </summary>
        public IList<InformationSections> InformationSections
        {
            get
            {
                IList<InformationSections> temp = new List<InformationSections>();
                foreach (string s in this.InformationSectionsArray)
                {
                    var value = s.ToEnum<InformationSections>();
                    temp.Add(value);
                }
                return temp;
            }
            set
            {
                IList<string> temp = new List<string>();
                foreach (InformationSections pt in value.Distinct())
                {
                    temp.Add(pt.ToString());
                }
                this.InformationSectionsArray = temp;
            }
        }
    }

    /// <summary>
    /// InformationSection of TrackTnformation
    /// </summary>
    [Serializable]
    public enum InformationSections
    {
        /// <summary>
        /// displays the event contact information
        /// </summary>
        CONTACT,
        /// <summary>
        /// displays the event date and time
        /// </summary>
        TIME,
        /// <summary>
        /// displays the event location
        /// </summary>
        LOCATION
    }
}
