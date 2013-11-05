using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
	/// Represents a single File Move Result
	/// </summary>
	[DataContract]
    [Serializable]
	public class FileMoveResult : Component
	{
		/// <summary>
		/// Gets or sets the id
		/// </summary>
		[DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
		/// <summary>
		/// Gets or sets the uri
		/// </summary>
		[DataMember(Name = "uri", EmitDefaultValue = false)]
        public string Uri { get; set; }
	}
}
