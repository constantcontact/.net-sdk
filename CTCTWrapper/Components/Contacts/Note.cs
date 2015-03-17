using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{
    /// <summary>
    /// Note class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Note : Component
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the note content.
        /// </summary>
        [DataMember(Name = "note", EmitDefaultValue = false)]
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets the datetime when note was created.
        /// </summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        public string CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the datetime when note was modified.
        /// </summary>
        [DataMember(Name = "modified_date", EmitDefaultValue = false)]
        public string ModifiedDate { get; set; } 
    }
}
