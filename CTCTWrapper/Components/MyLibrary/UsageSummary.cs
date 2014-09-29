using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
    /// Represents a Usage Summary in MyLibrary Info class
    /// </summary>
    [DataContract]
    [Serializable]
	public class UsageSummary : Component
	{
		/// <summary>
        /// Gets or sets the total amount of storage space currently being consumed by document files (in bytes)
        /// </summary>
        [DataMember(Name = "document_bytes_used", EmitDefaultValue = false)]
        public int DocumentBytesUsed { get; set; }
		/// <summary>
		/// Gets or sets the total number of documents stored
		/// </summary>
		[DataMember(Name = "document_count", EmitDefaultValue = false)]
        public int DocumentCount { get; set; }
		/// <summary>
		/// Gets or sets the total number of files stored
		/// </summary>
		[DataMember(Name = "file_count", EmitDefaultValue = false)]
        public int FileCount { get; set; }
		/// <summary>
		/// Gets or sets the total number of folders
		/// </summary>
		[DataMember(Name = "folder_count", EmitDefaultValue = false)]
        public int FolderCount { get; set; }
		/// <summary>
		/// Gets or sets the total number of free files
		/// </summary>
		[DataMember(Name = "free_files_remaining", EmitDefaultValue = false)]
        public int FreeFilesRemaining { get; set; }
		/// <summary>
		/// Gets or sets the total amount of storage space being consumed by image files (in bytes)
		/// </summary>
		[DataMember(Name = "image_bytes_used", EmitDefaultValue = false)]
        public int ImageBytesUsed { get; set; }
		/// <summary>
		/// Gets or sets the total number of images stored
		/// </summary>
		[DataMember(Name = "image_count", EmitDefaultValue = false)]
        public int ImageCount { get; set; }
		/// <summary>
		/// Gets or sets the total amount of storage space available (in bytes)
		/// </summary>
		[DataMember(Name = "total_bytes_remaining", EmitDefaultValue = false)]
        public int TotalBytesRemaining { get; set; }
		/// <summary>
		/// Gets or sets the total amount of storage space being used (in bytes)
		/// </summary>
		[DataMember(Name = "total_bytes_used", EmitDefaultValue = false)]
        public int TotalBytesUsed { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public UsageSummary() { }
	}
}
