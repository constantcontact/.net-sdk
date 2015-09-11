using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.Activities
{
    /// <summary>
    /// Activity class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Activity : Component
    {
        [DataMember(Name = "warnings")]
        private List<string> _Warnings = new List<string>();

        [DataMember(Name = "errors")]
        private List<string> _Errors = new List<string>();

        /// <summary>
        /// Activity id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Represetns the start date string.
        /// </summary>
        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        private string StartDateString { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate
        {
            get { return this.StartDateString.FromISO8601String(); }
            set { this.StartDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// String representation of finish date.
        /// </summary>
        [DataMember(Name = "finish_date", EmitDefaultValue = false)]
        private string FinishDateString { get; set; }

        /// <summary>
        /// Gets or sets the finish date.
        /// </summary>
        public DateTime? FinishDate
        {
            get { return this.FinishDateString.FromISO8601String(); }
            set { this.FinishDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        [DataMember(Name = "file_name", EmitDefaultValue = false)]
        public string FileName { get; set; }

        /// <summary>
        /// String representation of created date.
        /// </summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        private string CreatedDateString { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate
        {
            get { return this.CreatedDateString.FromISO8601String(); }
            set { this.CreatedDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Gets or sets the error count.
        /// </summary>
        [DataMember(Name = "error_count", EmitDefaultValue = false)]
        public int ErrorCount { get; set; }

        /// <summary>
        /// Gets or sets the contact count.
        /// </summary>
        [DataMember(Name = "contact_count", EmitDefaultValue = false)]
        public int ContactCount { get; set; }

        /// <summary>
        /// Gets or sets the error list.
        /// </summary>
        public IList<string> Errors
        {
            get
            {
                return _Errors;
            }
            set
            {
                _Errors = value == null ? null : value.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the warning list.
        /// </summary>
        public IList<string> Warnings
        {
            get
            {
                return _Warnings;
            }
            set
            {
                _Warnings = value == null ? null : value.ToList();
            }
        }
    }
}
