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
        [DataMember(Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the note content.
        /// </summary>
        [DataMember(Name = "note")]
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets the datetime when note was created.
        /// </summary>
        [DataMember(Name = "created_date")]
        public string CreatedDate { get; set; }
    }
}
