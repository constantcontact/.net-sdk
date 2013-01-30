using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace CTCT.Util
{
    /// <summary>
    /// URL response class.
    /// </summary>
    public class CUrlResponse
    {
        /// <summary>
        /// Requests body.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Returns true if error occur.
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// List of errors.
        /// </summary>
        public IList<CUrlRequestError> Info { get; set; }
        /// <summary>
        /// Returns true if valid data exists.
        /// </summary>
        public bool HasData { get { return !IsError && !String.IsNullOrEmpty(Body); } }
        /// <summary>
        /// Response status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Class constructor.
        /// </summary>
        public CUrlResponse()
        {
            IsError = false;
        }
    }
}
