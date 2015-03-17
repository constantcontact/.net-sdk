using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
    /// Represents a MyLibrary Info data class
    /// </summary>
    [DataContract]
    [Serializable]
	public class MyLibraryInfo : Component
	{
        /// <summary>
        /// Gets or sets the image root
        /// </summary>
        [DataMember(Name = "image_root", EmitDefaultValue = false)]
        public string ImageRoot { get; set; }
        /// <summary>
        /// Gets or sets the maximum number of free MyLibrary files
        /// If value = 0, refer to max_premium_space_limit for capacity
        /// </summary>
        [DataMember(Name = "max_free_file_num", EmitDefaultValue = false)]
        public int MaxFreeFileNum { get; set; }
        /// <summary>
        /// Gets or sets the total amount of MyLibrary Plus storage space (in bytes)
        /// If value = 0, refer to max_free_file_num for capacity
        /// If value = -1, the account has unlimited storage
        /// </summary>
        [DataMember(Name = "max_premium_space_limit", EmitDefaultValue = false)]
        public int MaxPremiumSpaceLimit { get; set; }
        /// <summary>
        /// Gets or sets the maximum file size (in bytes) that can be uploaded
        /// </summary>
        [DataMember(Name = "max_upload_size_limit", EmitDefaultValue = false)]
        public int MaxUploadSizeLimit { get; set; }
        /// <summary>
        /// Gets or sets the usage data
        /// </summary>
        [DataMember(Name = "usage_summary", EmitDefaultValue = false)]
        public UsageSummary UsageSummary { get; set; }
 
        /// <summary>
        /// Class constructor
        /// </summary>
        public MyLibraryInfo()
        {
            this.UsageSummary = new UsageSummary();
        }
	}
}
