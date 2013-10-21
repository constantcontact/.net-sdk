using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Activities
{
    /// <summary>
    /// Represents an ClearContactList activity class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ClearContactList: Component
    {
        /// <summary>
        /// Gets or sets the list of id's to add.
        /// </summary>
        [DataMember(Name = "lists", EmitDefaultValue = false)]
        public IList<string> Lists { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
        public ClearContactList() { 
        }
    }
}
