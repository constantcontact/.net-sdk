using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using CTCT.Exceptions;
using CTCT.Components;

namespace CTCT.Util
{
    /// <summary>
    /// URL response class.
    /// </summary>
    public class RawApiResponse
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
        public IList<RawApiRequestError> Info { get; set; }
        /// <summary>
        /// Returns true if valid data exists.
        /// </summary>
        public bool HasData { get { return !IsError && !String.IsNullOrEmpty(Body); } }
        /// <summary>
        /// Response status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Headers dictionary.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public RawApiResponse()
        {
            IsError = false;
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Returns the list of errors.
        /// </summary>
        /// <returns>Returns formatted error message.</returns>
        public string GetErrorMessage()
        {
            string message = null;
            if (this.Info != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (RawApiRequestError error in this.Info)
                {
                    sb.AppendLine(String.Format("{0}:{1}", error.Key, error.Message));
                }
                message = sb.ToString();
            }
            else
            {
                message = Body;
            }

            return message;
        }

        /// <summary>
        /// Returns the object represented by the JSON string.
        /// </summary>
        /// <typeparam name="T">Object type to return.</typeparam>
        /// <returns>Returns the object from its JSON representation.</returns>
        public T Get<T>() where T : class
        {
            if (IsError)
            {
                throw new Exception(GetErrorMessage());
            }

            T t = default(T);
            if (this.HasData && !String.IsNullOrEmpty(this.Body))
            {
                t = Component.FromJSON<T>(this.Body);
            }

            return t;
        }
    }
}
