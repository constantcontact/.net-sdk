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
        [DataMember(Name="id")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the fax number.
        /// </summary>
        [DataMember(Name = "fax")]
        public string Fax { get; set; }
        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        [DataMember(Name = "addresses")]
        public IList<Address> Addresses { get; private set; }
        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        [DataMember(Name = "notes")]
        public IList<Note> Notes { get; set; }
        /// <summary>
        /// Gets or sets the confirmation flag.
        /// </summary>
        [DataMember(Name = "confirmed")]
        public bool Confirmed { get; set; }
        /// <summary>
        /// Gets or sets the lists.
        /// </summary>
        [DataMember(Name = "lists")]
        public IList<ContactList> Lists { get; private set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }
        /// <summary>
        /// Gets or sets the email addresses.
        /// </summary>
        [DataMember(Name = "email_addresses")]
        public IList<EmailAddress> EmailAddresses { get; private set; }
        /// <summary>
        /// Gets or sets the prefix name.
        /// </summary>
        [DataMember(Name = "prefix_name")]
        public string PrefixName { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        [DataMember(Name = "middle_name")]
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets job title.
        /// </summary>
        [DataMember(Name = "job_title")]
        public string JobTitle { get; set; }
        /// <summary>
        /// Gets or sets the department name.
        /// </summary>
        [DataMember(Name = "department_name")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [DataMember(Name = "company_name")]
        public string CompanyName { get; set; }
        /// <summary>
        /// Gets or sets the list of custom fields.
        /// </summary>
        [DataMember(Name = "custom_fields")]
        public IList<CustomField> CustomFields { get; private set; }
        /// <summary>
        /// Gets or sets the source details.
        /// </summary>
        [DataMember(Name = "source_details")]
        public string SourceDetails { get; set; }
        /// <summary>
        /// Gets or sets action by.
        /// </summary>
        [DataMember(Name = "action_by")]
        public string ActionBy { get; set; }
        /// <summary>
        /// Gets or sets the work phone.
        /// </summary>
        [DataMember(Name = "work_phone")]
        public string WorkPhone { get; set; }
        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        [DataMember(Name = "home_phone")]
        public string HomePhone { get; set; }
        
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
        /// Output.
        /// </summary>
        public const string Output = "OPTOUT";
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
