using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCT.Util
{
    /// <summary>
    /// Configuration structure.
    /// </summary>
    public struct Config
    {
        /// <summary>
        /// REST endpoints.
        /// </summary>
        public struct Endpoints
        {
            /// <summary>
            /// API access URL.
            /// </summary>
            public const string BaseUrl = "https://api.constantcontact.com/v2/";
            /// <summary>
            /// Access a contact.
            /// </summary>
            public const string Contact = "contacts/{0}";
            /// <summary>
            /// Get all contacts.
            /// </summary>
            public const string Contacts = "contacts";
            /// <summary>
            /// Get all lists.
            /// </summary>
            public const string Lists = "lists";
            /// <summary>
            /// Access a specified list.
            /// </summary>
            public const string List = "lists/{0}";
            /// <summary>
            /// Get the list of contacts from a list.
            /// </summary>
            public const string ListContacts = "lists/{0}/contacts";
            /// <summary>
            /// Get contact lists.
            /// </summary>
            public const string ContactLists = "contacts/{0}/lists";
            /// <summary>
            /// Get a list from contact lists.
            /// </summary>
            public const string ContactList = "contacts/{0}/lists/{1}";
            /// <summary>
            /// Get campaigns.
            /// </summary>
            public const string Campaigns = "campaigns";
            /// <summary>
            /// Access a campaign
            /// </summary>
            public const string CampaignId = "campaigns/{0}";
        }

        /// <summary>
        /// OAuth2 Authorization related configuration options.
        /// </summary>
        public struct Auth
        {
            /// <summary>
            /// Authentication base URL.
            /// </summary>
            public const string BaseUrl = "https://oauth2.constantcontact.com/oauth2/";
            /// <summary>
            /// Query code.
            /// </summary>
            public const string ResponseTypeCode = "code";
            /// <summary>
            /// Query token.
            /// </summary>
            public const string ResponseTypeToken = "token";
            /// <summary>
            /// Query authorization type.
            /// </summary>
            public const string AuthorizationCodeGrantType = "authorization_code";
            /// <summary>
            /// Authorization endpoint.
            /// </summary>
            public const string AuthorizationEndpoint = "oauth/siteowner/authorize";
            /// <summary>
            /// Token endpoint.
            /// </summary>
            public const string TokenEndpoint = "oauth/token";
            /// <summary>
            /// Request host.
            /// </summary>
            public const string Host = "oauth2.constantcontact.com";
        }

        /// <summary>
        /// Login related configuration options.
        /// </summary>
        public struct Login
        {
            /// <summary>
            /// Login base URL.
            /// </summary>
            public const string BaseUrl = "https://login.constantcontact.com/login/";
            /// <summary>
            /// Login endpoint.
            /// </summary>
            public const string LoginEndpoint = "oauth/login";
            /// <summary>
            /// Request host.
            /// </summary>
            public const string Host = "login.constantcontact.com";
        }

        /// <summary>
        /// Errors to be returned for various exceptions.
        /// </summary>
        public struct Errors
        {
            /// <summary>
            /// Contact or id error.
            /// </summary>
            public const string ContactOrId = "Only an interger or Contact are allowed for this method.";
            /// <summary>
            /// List or id error.
            /// </summary>
            public const string ListOrId = "Only an interger or ContactList are allowed for this method.";
        }

        /// <summary>
        /// Accept header value.
        /// </summary>
        public const string HeaderAccept = "text/html, application/xhtml+xml, */*";
        /// <summary>
        /// ContentType header value.
        /// </summary>
        public const string HeaderContentType = "application/x-www-form-urlencoded";
        /// <summary>
        /// UserAgent header value.
        /// </summary>
        public const string HeaderUserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
    }
}
