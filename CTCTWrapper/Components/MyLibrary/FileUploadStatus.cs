using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
    /// Represents a single Upload Status 
    /// </summary>
    [DataContract]
    [Serializable]
	public class FileUploadStatus : Component
	{
		/// <summary>
		/// Gets or setd the file id
		/// </summary>
		[DataMember(Name = "file_id", EmitDefaultValue = false)]
        public string FileId { get; set; }
		/// <summary>
		/// Gets or sets the description
		/// </summary>
		[DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }
		/// <summary>
        /// Status, string representation.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        private string StatusString { get; set; }
        /// <summary>
        /// Gets or sets the file status
        /// </summary>
        public FileStatus Status
        {
            get { return this.StatusString.ToEnum<FileStatus>(); }
            set { this.StatusString = value.ToString(); }
        }
	}
}
