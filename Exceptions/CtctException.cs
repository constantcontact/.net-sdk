using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCT.Exceptions
{
    /// <summary>
    /// General exception.
    /// </summary>
    public class CtctException : Exception
    {
        private IList<string> _errors;

        /// <summary>
        /// Returns the errors list.
        /// </summary>
        public IList<string> Errors
        {
            get
            {
                if (_errors == null)
                    _errors = new List<string>();
                return _errors;
            }
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public CtctException() { }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="message">Error message.</param>
        public CtctException(string message) : base(message) { }
    }
}
