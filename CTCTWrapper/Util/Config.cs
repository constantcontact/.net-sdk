using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using CTCT.Services;

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
            /// Access an activity.
            /// </summary>
            public const string Activity = "activities/{0}";
            /// <summary>
            /// List activities.
            /// </summary>
            public const string Activities = "activities";
            /// <summary>
            /// Export contacts linked to an activity.
            /// </summary>
            public const string ExportContactsActivity = "activities/exportcontacts";
            /// <summary>
            /// Clear the list of activities.
            /// </summary>
            public const string ClearListsActivity = "activities/clearlists";
            /// <summary>
            /// Remove from list.
            /// </summary>
            public const string RemoveFromListsActivity = "activities/removefromlists";
            /// <summary>
            /// Add contacts to activities.
            /// </summary>
            public const string AddContactsActivity = "activities/addcontacts";
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
            public const string Campaigns = "emailmarketing/campaigns";
            /// <summary>
            /// Access a campaign
            /// </summary>
            public const string Campaign = "emailmarketing/campaigns/{0}";
            /// <summary>
            /// Campaign schedules.
            /// </summary>
            public const string CampaignSchedules = "emailmarketing/campaigns/{0}/schedules";
            /// <summary>
            /// Campaign schedule.
            /// </summary>
            public const string CampaignSchedule = "emailmarketing/campaigns/{0}/schedules/{1}";
            /// <summary>
            /// Campaign test sends.
            /// </summary>
            public const string CampaignTestSends = "emailmarketing/campaigns/{0}/tests";
            /// <summary>
            /// Campaign tracking summary.
            /// </summary>
            public const string CampaignTrackingSummary = "emailmarketing/campaigns/{0}/tracking/reports/summary";
            /// <summary>
            /// Campaign tracking bounces.
            /// </summary>
            public const string CampaignTrackingBounces = "emailmarketing/campaigns/{0}/tracking/bounces";
            /// <summary>
            /// Campaign tracking clicks.
            /// </summary>
            public const string CampaignTrackingClicks = "emailmarketing/campaigns/{0}/tracking/clicks";
            /// <summary>
            /// Campaign tracking clicks for a specific link.
            /// </summary>
            public const string CampaignTrackingClicksForLink = "emailmarketing/campaigns/{0}/tracking/clicks/{1}";
            /// <summary>
            /// Campaign tracking forwards.
            /// </summary>
            public const string CampaignTrackingForwards = "emailmarketing/campaigns/{0}/tracking/forwards";
            /// <summary>
            /// Campaign tracking opens.
            /// </summary>
            public const string CampaignTrackingOpens = "emailmarketing/campaigns/{0}/tracking/opens";
            /// <summary>
            /// Campaign tracking sends.
            /// </summary>
            public const string CampaignTrackingSends = "emailmarketing/campaigns/{0}/tracking/sends";
            /// <summary>
            /// Campaign tracking unsubscribes.
            /// </summary>
            public const string CampaignTrackingUnsubscribes = "emailmarketing/campaigns/{0}/tracking/unsubscribes";
            /// <summary>
            /// Campaign tracking link.
            /// </summary>
            public const string CampaignTrackingLink = "emailmarketing/campaigns/{0}/tracking/clicks/{1}";
            /// <summary>
            /// Contact tracking activities.
            /// </summary>
            public const string ContactTrackingActivities = "contacts/{0}/tracking";
            /// <summary>
            /// Contact tracking activities by email campaign.
            /// </summary>
            public const string ContactTrackingEmailCampaignActivities = "contacts/{0}/tracking/reports/summaryByCampaign";
            /// <summary>
            /// Contact tracking summary.
            /// </summary>
            public const string ContactTrackingSummary = "contacts/{0}/tracking/reports/summary";
            /// <summary>
            /// Contact tracking bounces.
            /// </summary>
            public const string ContactTrackingBounces = "contacts/{0}/tracking/bounces";
            /// <summary>
            /// Contact tracking clicks.
            /// </summary>
            public const string ContactTrackingClicks = "contacts/{0}/tracking/clicks";
            /// <summary>
            /// Contact tracking forwards.
            /// </summary>
            public const string ContactTrackingForwards = "contacts/{0}/tracking/forwards";
            /// <summary>
            /// Contact tracking opens.
            /// </summary>
            public const string ContactTrackingOpens = "contacts/{0}/tracking/opens";
            /// <summary>
            /// Contact tracking sends.
            /// </summary>
            public const string ContactTrackingSends = "contacts/{0}/tracking/sends";
            /// <summary>
            /// Contact tracking unsubscribes.
            /// </summary>
            public const string ContactTrackingUnsubscribes = "contacts/{0}/tracking/unsubscribes";
            /// <summary>
            /// Contact tracking link.
            /// </summary>
            public const string ContactTrackingLink = "contacts/{0}/tracking/clicks/{1}";
            /// <summary>
            /// Account verified email addresses link
            /// </summary>
            public const string AccountVerifiedEmailAddressess = "account/verifiedemailaddresses";
            /// <summary>
            /// Account Summary Information
            /// </summary>
            public const string AccountSummaryInformation = "account/info";
            /// <summary>
            /// MyLibrary information
            /// </summary>
            public const string MyLibraryInfo = "library/info";
            /// <summary>
            /// MyLibrary folders
            /// </summary>
            public const string MyLibraryFolders = "library/folders";
            /// <summary>
            ///  Access a specified folder
            /// </summary>
            public const string MyLibraryFolder = "library/folders/{0}";
            /// <summary>
            /// Files in Trash folder
            /// </summary>
            public const string MyLibraryTrash = "library/folders/trash/files";
            /// <summary>
            /// MyLibrray files
            /// </summary>
            public const string MyLibraryFiles = "library/files";
            /// <summary>
            /// MyLibrary file
            /// </summary>
            public const string MyLibraryFile = "library/files/{0}";
            /// <summary>
            /// MyLibrary files for a specific folder
            /// </summary>
            public const string MyLibraryFolderFiles = "library/folders/{0}/files";
            /// <summary>
            /// MyLibrary file upload status
            /// </summary>
            public const string MyLibraryFileUploadStatus = "library/files/uploadstatus/{0}";
            /// <summary>
            /// EventSpot Events Collection Endpoint
            /// </summary>
            public const string EventSpots = "eventspot/events";
            /// <summary>
            /// Individual Event Fees Endpoint
            /// </summary>
            public const string EventFees = "eventspot/events/{0}/fees/{1}";
            /// <summary>
            /// Individual Promocode Endpoint
            /// </summary>
            public const string EventPromocode = "eventspot/events/{0}/promocodes/{1}";
            /// <summary>
            /// Individual Event Registrant Endpoint
            /// </summary>
            public const string EventRegistrant = "eventspot/events/{0}/registrants/{1}";
            /// <summary>
            /// Event Item Endpoint
            /// </summary>
            public const string EventItem = "eventspot/events/{0}/items/{1}";
            /// <summary>
            /// Item Attribute Endpoint
            /// </summary>
            public const string ItemAttribute = "eventspot/events/{0}/items/{1}/attributes/{2}";

        }

        /// <summary>
        /// Column names used with bulk activities.
        /// </summary>
        public struct ActivitiesColumns
        {
            /// <summary>
            /// Email address.
            /// </summary>
            public const string Email = "EMAIL";
            /// <summary>
            /// First name.
            /// </summary>
            public const string FirstName = "FIRST NAME";
            /// <summary>
            /// Middle name.
            /// </summary>
            public const string MiddleName = "MIDDLE NAME";
            /// <summary>
            /// Last name.
            /// </summary>
            public const string LastName = "LAST NAME";
            /// <summary>
            /// Job title.
            /// </summary>
            public const string JobTitle = "JOB TITLE";
            /// <summary>
            /// Company name.
            /// </summary>
            public const string CompanyName = "COMPANY NAME";
            /// <summary>
            /// Work phone.
            /// </summary>
            public const string WorkPhone = "WORK PHONE";
            /// <summary>
            /// Home phone.
            /// </summary>
            public const string HomePhone = "HOME PHONE";
            /// <summary>
            /// Address line 1.
            /// </summary>
            public const string Address1 = "ADDRESS LINE 1";
            /// <summary>
            /// Address line 2.
            /// </summary>
            public const string Address2 = "ADDRESS LINE 2";
            /// <summary>
            /// Address line 3.
            /// </summary>
            public const string Address3 = "ADDRESS LINE 3";
            /// <summary>
            /// City.
            /// </summary>
            public const string City = "CITY";
            /// <summary>
            /// State.
            /// </summary>
            public const string State = "STATE";
            /// <summary>
            /// US state or province.
            /// </summary>
            public const string StateProvince = "US STATE/CA PROVINCE";
            /// <summary>
            /// Country.
            /// </summary>
            public const string Country = "COUNTRY";
            /// <summary>
            /// Postal code.
            /// </summary>
            public const string PostalCode = "ZIP/POSTAL CODE";
            /// <summary>
            /// Sub-postal code.
            /// </summary>
            public const string SubPostalCode = "SUB ZIP/POSTAL CODE";
            /// <summary>
            /// Custom field 1.
            /// </summary>
            public const string CustomField1 = "CUSTOM FIELD 1";
            /// <summary>
            /// Custom field 2.
            /// </summary>
            public const string CustomField2 = "CUSTOM FIELD 2";
            /// <summary>
            /// Custom field 3.
            /// </summary>
            public const string CustomField3 = "CUSTOM FIELD 3";
            /// <summary>
            /// Custom field 4.
            /// </summary>
            public const string CustomField4 = "CUSTOM FIELD 4";
            /// <summary>
            /// Custom field 5.
            /// </summary>
            public const string CustomField5 = "CUSTOM FIELD 5";
            /// <summary>
            /// Custom field 6.
            /// </summary>
            public const string CustomField6 = "CUSTOM FIELD 6";
            /// <summary>
            /// Custom field 7.
            /// </summary>
            public const string CustomField7 = "CUSTOM FIELD 7";
            /// <summary>
            /// Custom field 8.
            /// </summary>
            public const string CustomField8 = "CUSTOM FIELD 8";
            /// <summary>
            /// Custom field 9.
            /// </summary>
            public const string CustomField9 = "CUSTOM FIELD 9";
            /// <summary>
            /// Custom field 10.
            /// </summary>
            public const string CustomField10 = "CUSTOM FIELD 10";
            /// <summary>
            /// Custom field 11.
            /// </summary>
            public const string CustomField11 = "CUSTOM FIELD 11";
            /// <summary>
            /// Custom field 12.
            /// </summary>
            public const string CustomField12 = "CUSTOM FIELD 12";
            /// <summary>
            /// Custom field 13.
            /// </summary>
            public const string CustomField13 = "CUSTOM FIELD 13";
            /// <summary>
            /// Custom field 14.
            /// </summary>
            public const string CustomField14 = "CUSTOM FIELD 14";
            /// <summary>
            /// Custom field 15.
            /// </summary>
            public const string CustomField15 = "CUSTOM FIELD 15";
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
            /// <summary>
            /// Activity or id error.
            /// </summary>
            public const string ActivityOrId = "Only an interger or Activity are allowed for this method.";
            /// <summary>
            /// Schedule or id error.
            /// </summary>
            public const string ScheduleOrId = "Only an interger or Schedule are allowed for this method.";
            /// <summary>
            /// CampaignTracking or id error.
            /// </summary>
            public const string CampaignTrackingOrId = "Only an interger or CampaignTracking are allowed for this method.";
            /// <summary>
            /// ContactTracking or id error.
            /// </summary>
            public const string ContactTrackingOrId = "Only an interger or ContactTracking are allowed for this method.";
            /// <summary>
            /// EmailCampaign or id error.
            /// </summary>
            public const string EmailCampaignOrId = "Only an interger or EmailCampaign are allowed for this method.";
            /// <summary>
            /// Update contact without ID error.
            /// </summary>
            public const string UpdateId = "You must provide a contact ID in order to update a contact.";
            /// <summary>
            /// FileName null error.
            /// </summary>
            public const string FileNameNull = "You must provide a FileName parameter for this method.";
            /// <summary>
            /// File null error.
            /// </summary>
            public const string FileNull = "You must provide a File parameter for this method.";
            /// <summary>
            /// File type invalid error.
            /// </summary>
            public const string FileTypeInvalid = "File type is invalid.";
            /// <summary>
            /// MyLibrary item or id error
            /// </summary>
            public const string MyLibraryOrId = "Only an interger or a MyLibrary item is allowed for this method.";
            /// <summary>
            /// File Ids null error
            /// </summary>
            public const string FileIdNull = "File Id parameter must not be null";
            /// <summary>
            /// Field null error
            /// </summary>
            public const string FieldNull = "Field must not be null";
            /// <summary>
            /// EventSpot id error
            /// </summary>
            public const string InvalidId = "Only an valid id is allowed for this method.";
            /// <summary>
            /// Objects null
            /// </summary>
            public const string ObjectNull = "Object provided is not valid.";

            /// <summary>
            /// Invalid Webhook error
            /// </summary>
            public const string InvalidWebhook = "Invalid Webhook. The x-ctct-hmac-sha256 does not correspond to message encryption.";

            /// <summary>
            /// Client Secret null error
            /// </summary>
            public const string NoClientSecret = "You must provide the client secret (via constructor or setClientSecret() ) corresponding to your API   from Constant Contact";
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

        /// <summary>
        /// Creates the URL for API access.
        /// </summary>
        /// <param name="urlPart">URL part.</param>
        /// <param name="prms">Additional parameters for URL formatting.</param>
        /// <param name="queryList">Query parameters to add to the URL.</param>
        /// <returns>Returns the URL with all specified query parameters.</returns>
        public static string ConstructUrl(string urlPart, object[] prms, object[] queryList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Endpoints.BaseUrl);
            if (prms == null)
            {
                sb.Append(urlPart);
            }
            else
            {
                sb.AppendFormat(urlPart, prms);
            }
            if (queryList != null)
            {
                sb.Append(BaseService.GetQueryParameters(queryList));
            }

            return sb.ToString();
        }
    }
}
