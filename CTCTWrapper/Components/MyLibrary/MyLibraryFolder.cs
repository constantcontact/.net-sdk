using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
	/// Represents a single MyLibrary Folder
	/// </summary>
	[DataContract]
    [Serializable]
	public class MyLibraryFolder : BaseLibrary
	{
		/// <summary>
		/// Gets or sets the number of files in the library folder
		/// </summary>
		[DataMember(Name = "item_count", EmitDefaultValue = false)]
        public int ItemCount { get; set; }
		/// <summary>
		/// Gets or sets the level
		/// </summary>
		[DataMember(Name = "level", EmitDefaultValue = false)]
        public int Level { get; set; }
		/// <summary>
		/// Gets or sets the parent folder id
		/// </summary>
		[DataMember(Name = "parent_id", EmitDefaultValue = false)]
        public string ParentId { get; set; }
		/// <summary>
		/// Gets or sets the list of child or grandchild folders
		/// </summary>
		[DataMember(Name = "children", EmitDefaultValue = false)]
        public IList<MyLibraryFolder> Children { get; set; }

		/// <summary>
		/// Class constructor
		/// </summary>
		public MyLibraryFolder()
		{
			this.Children = new List<MyLibraryFolder>();
		}
	}
}
