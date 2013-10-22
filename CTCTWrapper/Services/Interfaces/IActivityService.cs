using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Activities;
using CTCT.Util;
using System.IO;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for ActivityService class.
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// Get a list of activities.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>Returns the list of activities.</returns>
        IList<Activity> GetActivities(string accessToken, string apiKey);

        /// <summary>
        /// Get an activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="activityId">The activity identification.</param>
        /// <returns>Returns the activity identified by its id.</returns>
        Activity GetActivity(string accessToken, string apiKey, string activityId);

        /// <summary>
        /// Create an Add Contacts Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="addContacts">AddContacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        Activity CreateAddContactsActivity(string accessToken, string apiKey, AddContacts addContacts);

        /// <summary>
        /// Create a Clear Lists Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="lists">Array of list id's to be cleared.</param>
        /// <returns>Returns an Activity object.</returns>
        Activity AddClearListsActivity(string accessToken, string apiKey, IList<string> lists);

        /// <summary>
        /// Create an Export Contacts Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="exportContacts">Export contacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        Activity AddExportContactsActivity(string accessToken, string apiKey, ExportContacts exportContacts);

        /// <summary>
        /// Create a Remove Contacts From Lists Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="emailAddresses">List of email addresses.</param>
        /// <param name="lists">List of id's.</param>
        /// <returns>Returns an Activity object.</returns>
        Activity AddRemoveContactsFromListsActivity(string accessToken, string apiKey, IList<string> emailAddresses, IList<string> lists);

		/// <summary>
		/// Create an Add Contacts Multipart Activity.
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileName">The file name to be imported</param>
		/// <param name="fileContent">The file content to be imported</param>
		/// <param name="lists">Array of list's id</param>
		/// <returns>Returns an Activity object.</returns>
		Activity AddContactstMultipartActivity(string accessToken, string apiKey, string fileName, byte[] fileContent, IList<string> lists);

		/// <summary>
		/// Create a Remove Contacts Multipart Activity.
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileName">The file name to be imported</param>
		/// <param name="fileContent">The file content to be imported</param>
		/// <param name="lists">Array of list's id</param>
		/// <returns>Returns an Activity object.</returns>
		Activity RemoveContactsMultipartActivity(string accessToken, string apiKey, string fileName, byte[] fileContent, IList<string> lists);
    }
}
