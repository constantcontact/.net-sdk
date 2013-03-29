using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCT.Exceptions
{
    /// <summary>
    /// Exception thrown if there is an illegal argument passed to a particular method.
    /// </summary>
    public class IllegalArgumentException : Exception
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public IllegalArgumentException() { }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="message">Error message.</param>
        public IllegalArgumentException(string message) : base(message) { }
    }
}
