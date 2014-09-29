using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// Represents a single Contact in Constant Contact.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Contact : Component
    {
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
        [DataMember(Name = "email_addresses")]
        public IList<EmailAddress> EmailAddresses { get; set; }
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
        [DataMember(Name = "addresses")]
        public IList<Address> Addresses { get; set; }
        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        [DataMember(Name = "notes")]
        public IList<Note> Notes { get; set; }
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
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        public string DateCreated { get; set; }
        /// <summary>
        /// Gets or sets the date and time contact's information was last modified
        /// </summary>
        [DataMember(Name = "modified_date", EmitDefaultValue = false)]
        public string DateModified { get; set; }
        /// <summary>
        /// Gets or sets the list of custom fields.
        /// </summary>
        [DataMember(Name = "custom_fields")]
        public IList<CustomField> CustomFields { get; set; }
        /// <summary>
        /// Gets or sets the lists.
        /// </summary>
        [DataMember(Name = "lists")]
        public List<ContactList> Lists { get; set; }
        /// <summary>
        /// Gets or sets the source details.
        /// </summary>
        [DataMember(Name = "source_details", EmitDefaultValue = false)]
        public string SourceDetails { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public Contact()
        {
            this.EmailAddresses = new List<EmailAddress>();
            this.Addresses = new List<Address>();
            this.CustomFields = new List<CustomField>();
            this.Lists = new List<ContactList>();
            this.Notes = new List<Note>();
        }
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
