using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// Represents a single Contact in Constant Contact.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Contact : Component
    {
        [DataMember(Name = "email_addresses")]
        private List<EmailAddress> _EmailAddresses = new List<EmailAddress>();

        [DataMember(Name = "addresses")]
        private List<Address> _Addresses = new List<Address>();

        [DataMember(Name = "notes")]
        private List<Note> _Notes = new List<Note>();

        [DataMember(Name = "custom_fields")]
        private List<CustomField> _CustomFields = new List<CustomField>();

        [DataMember(Name = "lists")]
        private List<ContactList> _Lists = new List<ContactList>();

        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        private string DateCreatedString { get; set; }

        [DataMember(Name = "modified_date", EmitDefaultValue = false)]
        private string DateModifiedString { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        [DataMember(Name = "middle_name", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the confirmation flag.
        /// </summary>
        [DataMember(Name = "confirmed", EmitDefaultValue = false)]
        public bool Confirmed { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [DataMember(Name = "source", EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the email addresses.
        /// </summary>
        public IList<EmailAddress> EmailAddresses 
        {
            get { return _EmailAddresses; }
            set { _EmailAddresses = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the prefix name.
        /// </summary>
        [DataMember(Name = "prefix_name", EmitDefaultValue = false)]
        public string PrefixName { get; set; }

        /// <summary>
        /// Gets or sets job title.
        /// </summary>
        [DataMember(Name = "job_title", EmitDefaultValue = false)]
        public string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        public IList<Address> Addresses 
        {
            get { return _Addresses; }
            set { _Addresses = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public IList<Note> Notes 
        {
            get { return _Notes; }
            set { _Notes = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [DataMember(Name = "company_name", EmitDefaultValue = false)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        [DataMember(Name = "home_phone", EmitDefaultValue = false)]
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the work phone.
        /// </summary>
        [DataMember(Name = "work_phone", EmitDefaultValue = false)]
        public string WorkPhone { get; set; }

        /// <summary>
        /// Gets or sets the cell phone.
        /// </summary>
        [DataMember(Name = "cell_phone", EmitDefaultValue = false)]
        public string CellPhone { get; set; }

        /// <summary>
        /// Gets or sets the fax number.
        /// </summary>
        [DataMember(Name = "fax", EmitDefaultValue = false)]
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the date and time the contact was added
        /// </summary>
        public DateTime? DateCreated
        {
            get { return this.DateCreatedString.FromISO8601String(); }
            set { this.DateCreatedString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Gets or sets the date and time contact's information was last modified
        /// </summary>
        public DateTime? DateModified
        {
            get { return this.DateModifiedString.FromISO8601String(); }
            set { this.DateModifiedString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Gets or sets the list of custom fields.
        /// </summary>
        public IList<CustomField> CustomFields
        {
            get { return _CustomFields; }
            set { _CustomFields = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the lists.
        /// </summary>
        public List<ContactList> Lists 
        {
            get { return _Lists; }
            set { _Lists = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the source details.
        /// </summary>
        [DataMember(Name = "source_details", EmitDefaultValue = false)]
        public string SourceDetails { get; set; }
    }

	/// <summary>
	/// Contact status enumeration
	/// </summary>
	public enum ContactStatus
	{
		 /// <summary>
        /// Active.
        /// </summary>
        ACTIVE,
        /// <summary>
        /// Unconfirmed.
        /// </summary>
        UNCONFIRMED,
        /// <summary>
        /// OptOut.
        /// </summary>
        OPTOUT,
        /// <summary>
        /// Removed.
        /// </summary>
        REMOVED
	}

    /// <summary>
    /// ActionBy structure.
    /// </summary>
    public struct ActionBy
    {
        /// <summary>
        /// ActionByVisitor.
        /// </summary>
        public const string ActionByVisitor = "ACTION_BY_VISITOR";
        /// <summary>
        /// ActionByOwner.
        /// </summary>
        public const string ActionByOwner = "ACTION_BY_OWNER";
    }

    /// <summary>
    /// Status structure.
    /// </summary>
    public struct Status
    {
        /// <summary>
        /// Active.
        /// </summary>
        public const string Active = "ACTIVE";
        /// <summary>
        /// Unconfirmed.
        /// </summary>
        public const string Unconfirmed = "UNCONFIRMED";
        /// <summary>
        /// OptOut.
        /// </summary>
        public const string OptOut = "OPTOUT";
        /// <summary>
        /// Removed.
        /// </summary>
        public const string Removed = "REMOVED";
        /// <summary>
        /// NonSubscriber.
        /// </summary>
        public const string NonSubscriber = "NON_SUBSCRIBER";
        /// <summary>
        /// Visitor.
        /// </summary>
        public const string Visitor = "VISITOR";
    }
}
