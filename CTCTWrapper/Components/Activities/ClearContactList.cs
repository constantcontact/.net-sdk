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
        [DataMember(Name = "lists", EmitDefaultValue = false)]
        private List<string> _Lists = new List<string>();

        /// <summary>
        /// Gets or sets the list of id's to add.
        /// </summary>
        public IList<string> Lists
        {
            get { return _Lists; }
            set { _Lists = value == null ? null : value.ToList(); }
        }
    }
}
