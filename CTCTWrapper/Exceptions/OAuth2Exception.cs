using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCT.Exceptions
{
    /// <summary>
    /// Exception thrown if an error occured during OAuth2 authentication process.
    /// </summary>
    public class OAuth2Exception : Exception
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OAuth2Exception() { }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="message">Error message.</param>
        public OAuth2Exception(string message) : base(message) { }
    }
}
