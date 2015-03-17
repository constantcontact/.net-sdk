using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
    /// Represents a single Thumbnail in MyLibrary File class
    /// </summary>
    [DataContract]
    [Serializable]
	public class Thumbnail : Component
	{
        /// <summary>
        /// Gets or sets the URL referencing the thumbnail of the file
        /// </summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the height in pixels
        /// </summary>
        [DataMember(Name = "height", EmitDefaultValue = false)]
        public int Height { get; set; }
        /// <summary>
        /// Gets or sets the width in pixels
        /// </summary>
        [DataMember(Name = "width", EmitDefaultValue = false)]
        public int Width { get; set; }
	}
}
