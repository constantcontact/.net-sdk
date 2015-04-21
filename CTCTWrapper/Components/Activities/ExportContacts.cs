using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Activities
{
    /// <summary>
    /// Export contacts class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ExportContacts : Component
    {
        [DataMember(Name = "lists")]
        private List<string> _Lists = new List<string>();

        [DataMember(Name = "column_names")]
        private List<string> _ColumnNames;

        /// <summary>
        /// Gets or sets the field type.
        /// </summary>
        [DataMember(Name = "file_type")]
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets sort by.
        /// </summary>
        [DataMember(Name = "sort_by")]
        public string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the flag for export date added.
        /// </summary>
        [DataMember(Name = "export_date_added")]
        public bool ExportDateAdded { get; set; }

        /// <summary>
        /// Gets or sets the flag for export added by.
        /// </summary>
        [DataMember(Name = "export_added_by")]
        public bool ExportAddedBy { get; set; }

        /// <summary>
        /// Gets or sets list of id's to export.
        /// </summary>
        public IList<string> Lists 
        {
            get { return _Lists; }
            set { _Lists = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the column names.
        /// </summary>
        public IList<string> ColumnNames
        {
            get { return _ColumnNames; }
            set { _ColumnNames = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public ExportContacts()
        {
            this.FileType = "CSV";
            this.SortBy = "EMAIL_ADDRESS";
            this.ExportDateAdded = true;
            this.ExportAddedBy = true;
            this._ColumnNames = new List<string>() { "Email Address", "First Name", "Last Name" };
        }
    }
}
