using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;
using CTCT.Components.Activities;
using System.ComponentModel;
using System.Reflection;

namespace CTCT.Components.BulkStatus
{
    /// <summary>
    /// Represents a single Bulk Status Report in Constant Contact.
    /// </summary>
    [DataContract]
    [Serializable]
    public class StatusReport : Component
    {
        [DataMember(Name = "warnings", EmitDefaultValue = false)]
        private List<string> _Warnings = new List<string>();

        [DataMember(Name = "errors", EmitDefaultValue = false)]
        private List<string> _Errors = new List<string>();

        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        private string CreatedDateString { get; set; }

        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        private string StartDateString { get; set; }

        [DataMember(Name = "finish_date", EmitDefaultValue = false)]
        private string FinishDateString { get; set; }

        /// <summary>
        /// Gets or sets the number of contacts included the activity. 
        /// </summary>
        [DataMember(Name = "contact_count", EmitDefaultValue = false)]
        public int ContactCount { get; set; }

        /// <summary>
        /// Gets or sets time and date that created the activity after importing the file, in ISO 8601 format 
        /// </summary>
        public DateTime? CreatedDate
        {
            get
            {
                return CreatedDateString.FromISO8601String();
            }
            private set
            {
                if (value != null)
                {
                    CreatedDateString = value.ToISO8601String();
                }
                else
                {
                    CreatedDateString = String.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of errors encountered during the activity 
        /// </summary>
        [DataMember(Name = "error_count", EmitDefaultValue = false)]
        public int ErrorCount { get; set; }

        /// <summary>
        /// Gets or sets the URI pointing to the exported file. Make a GET call to the URI to retrieve the file. 
        /// </summary>
        [DataMember(Name = "file_name", EmitDefaultValue = false)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets time and date that activity was completed, in ISO 8601 format 
        /// </summary>
        public DateTime? FinishDate
        {
            get
            {
                return FinishDateString.FromISO8601String();
            }
            private set
            {
                if (value != null)
                {
                    FinishDateString = value.ToISO8601String();
                }
                else
                {
                    FinishDateString = String.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the unique ID for the activity.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        ///  Gets or sets time and date that the API started processing the activity, in ISO 8601 format 
        /// </summary>
        public DateTime? StartDate
        {
            get
            {
                return StartDateString.FromISO8601String();
            }
            private set
            {
                if (value != null)
                {
                    StartDateString = value.ToISO8601String();
                }
                else
                {
                    StartDateString = String.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the status of the activity
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string StatusString { get; set; }

        /// <summary>
        /// Gets or sets the status of the activity
        /// </summary>
        public BulkActivityStatus Status
        {
            get { return this.StatusString.ToEnum<BulkActivityStatus>(); }
            set { this.StatusString = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the type of activity
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string TypeString { get; set; }

        /// <summary>
        /// Gets or sets the type of activity
        /// </summary>
        public BulkActivityType Type
        {
            get { return this.TypeString.ToEnum<BulkActivityType>(); }
            set { this.TypeString = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the list of imported data.
        /// </summary>
        public IList<string> Warnings
        {
            get { return _Warnings; }
            set { _Warnings = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets a lists errors that occurred (up to the first 100) when the activity ran 
        /// </summary>
        public IList<string> Errors
        {
            get { return _Errors; }
            set { _Errors = value == null ? null : value.ToList(); }
        }
    }

    /// <summary>
    /// ActionBy structure.
    /// </summary>
    public enum BulkActivityType
    {
        /// <summary>
        /// All
        /// </summary>
        ALL,
        /// <summary>
        /// AddContacts.
        /// </summary>
        ADD_CONTACTS,
        /// <summary>
        /// ClearContactsFromLists.
        /// </summary>
        CLEAR_CONTACTS_FROM_LISTS,
        /// <summary>
        /// ExportContacts.
        /// </summary>
        EXPORT_CONTACTS,
        /// <summary>
        /// RemoveContactsFromLists.
        /// </summary>
        REMOVE_CONTACTS_FROM_LISTS
    }

    /// <summary>
    /// Activity status structure.
    /// </summary>
    public enum BulkActivityStatus
    {
        /// <summary>
        /// All
        /// </summary>
        ALL,
        /// <summary>
        /// Unconfirmed.
        /// </summary>
        UNCONFIRMED,
        /// <summary>
        /// Pending.
        /// </summary>
        PENDING,
        /// <summary>
        /// Queued.
        /// </summary>
        QUEUED,
        /// <summary>
        /// Running.
        /// </summary>
        RUNNING,
        /// <summary>
        /// Complete.
        /// </summary>
        COMPLETE,
        /// <summary>
        /// Error.
        /// </summary>
        ERROR
    }
}
