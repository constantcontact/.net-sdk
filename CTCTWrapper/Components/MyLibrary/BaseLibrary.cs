using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
	/// Base class for MyLibrary
	/// </summary>
	[DataContract]
    [Serializable]
	public class BaseLibrary : Component
	{
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        public string CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the modified date
        /// </summary>
        [DataMember(Name = "modified_date", EmitDefaultValue = false)]
        public string ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the library item
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
	}
}
