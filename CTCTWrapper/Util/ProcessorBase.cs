using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCT.Util
{
    /// <summary>
    /// Processor Constants in Constant Contact.
    /// </summary>
    public abstract class ProcessorBase
    {
        /// <summary>
        /// Content-Type header.
        /// </summary>
        public const string CONTENT_TYPE_HEADER = "Content-Type";

        /// <summary>
        /// Content-Length header.
        /// </summary>
        public const string CONTENT_LENGTH_HEADER = "Content-Length";

        /// <summary>
        /// Accept header.
        /// </summary>
        public const string ACCEPT_HEADER = "Accept";

        /// <summary>
        /// Authorization header.
        /// </summary>
        public const string AUTHORIZATION_HEADER = "Authorization";

        /// <summary>
        /// Constant Contact request source header
        /// </summary>
        public const string X_CTCT_REQUEST_SOURCE_HEADER = "x-ctct-request-source";

        /// <summary>
        /// constant for "application/json" content type
        /// </summary>
        public const string JSON_CONTENT_TYPE = "application/json";

        /// <summary>
        /// constant for "multipart/form-data" content type
        /// </summary>
        public const string MULTIPART_CONTENT_TYPE = "multipart/form-data";

        /// <summary>
        /// The boundary we will use in our multipart requests
        /// </summary>
        public const string MULTIPART_BOUNDARY = "ihLgaHFfpPMsYLTKiwf4";

        /// <summary>
        /// constant for boundary content type. Used with multipart after a semicolon
        /// </summary>
        public const string BOUNDARY_CONTENT_TYPE = "boundary=" + MULTIPART_BOUNDARY;

        /// <summary>
        /// A single Space
        /// </summary>
        public const string SPACE = " ";

        /// <summary>
        /// Location header.
        /// </summary>
        public const string LOCATION_HEADER = "Location";

        /// <summary>
        /// last_redirect_url parameter.
        /// </summary>
        public const string LAST_REDIRECT_URL = "last_redirect_url";

        /// <summary>
        /// Cookie header.
        /// </summary>
        public const string COOKIE_HEADER = "Cookie";

        /// <summary>
        /// Cookie2 header.
        /// </summary>
        public const string COOKIE2_HEADER = "Cookie2";

        /// <summary>
        /// $Version=1 value.
        /// </summary>
        public const string VERSION1_VALUE = "$Version=1";

        /// <summary>
        /// Host header.
        /// </summary>
        public const string HOST_HEADER = "Host";

        /// <summary>
        /// User-Agent header.
        /// </summary>
        public const string USER_AGENT_HEADER = "User-Agent";
    }

    /// <summary>
    /// The allowed HTTP methods for the HTTP and HTTPS Processors.
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// Get
        /// </summary>
        GET,

        /// <summary>
        /// Post
        /// </summary>
        POST,

        /// <summary>
        /// Delete
        /// </summary>
        DELETE,

        /// <summary>
        /// Put
        /// </summary>
        PUT,

        /// <summary>
        /// Patch
        /// </summary>
        PATCH
    }

    /// <summary>
    /// Content type interface
    /// </summary>
    public interface IContentType
    {
        /// <summary>
        /// The value
        /// </summary>
        string Value { get; }
    }

    /// <summary>
    /// Json content type
    /// </summary>
    public class JsonContentType : IContentType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JsonContentType()
        {
            this.Value = ProcessorBase.JSON_CONTENT_TYPE;
        }

        /// <summary>
        /// The value
        /// </summary>
        public string Value { get; private set; }
    }

    /// <summary>
    /// Form data content type
    /// </summary>
    public class FormDataContentType : IContentType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FormDataContentType()
        {
            this.Value = ProcessorBase.MULTIPART_CONTENT_TYPE + ";" + ProcessorBase.BOUNDARY_CONTENT_TYPE; ;
        }

        /// <summary>
        /// The value
        /// </summary>
        public string Value { get; private set; }
    }
}
