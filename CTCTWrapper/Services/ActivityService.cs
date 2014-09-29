using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Activities;
using CTCT.Util;
using CTCT.Components;
using CTCT.Exceptions;
using System.IO;

namespace CTCT.Services
{
    /// <summary> 
    /// Performs all actions pertaining to scheduling Constant Contact Activities.
    /// </summary>
    public class ActivityService : BaseService, IActivityService
    {
        /// <summary>
        /// Get a list of activities.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>Returns the list of activities.</returns>
        public IList<Activity> GetActivities(string accessToken, string apiKey)
        {
            IList<Activity> activities = new List<Activity>();
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Activities);
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                activities = response.Get<IList<Activity>>();
            }

            return activities;
        }

        /// <summary>
        /// Get an activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="activityId">The activity identification.</param>
        /// <returns>Returns the activity identified by its id.</returns>
        public Activity GetActivity(string accessToken, string apiKey, string activityId)
        {
            Activity activity = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Activity, activityId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                activity = response.Get<Activity>();
            }

            return activity;
        }

        /// <summary>
        /// Create an Add Contacts Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="addContacts">AddContacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity CreateAddContactsActivity(string accessToken, string apiKey, AddContacts addContacts)
        {
            Activity activity = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.AddContactsActivity);
            string json = addContacts.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if(response.HasData)
            {
                activity = response.Get<Activity>();
            }

            return activity;
        }

        /// <summary>
        /// Create a Clear Lists Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="lists">Array of list id's to be cleared.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity AddClearListsActivity(string accessToken, string apiKey, IList<string> lists)
        {
            Activity activity = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ClearListsActivity);

            ClearContactList clearContact = new ClearContactList() { Lists = lists };
            string json = clearContact.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                activity = response.Get<Activity>();
            }

            return activity;
        }

        /// <summary>
        /// Create an Export Contacts Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="exportContacts">Export contacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity AddExportContactsActivity(string accessToken, string apiKey, ExportContacts exportContacts)
        {
            Activity activity = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ExportContactsActivity);
            string json = exportContacts.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                activity = response.Get<Activity>();
            }

            return activity;
        }

        /// <summary>
        /// Create a Remove Contacts From Lists Activity.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="emailAddresses">List of email addresses.</param>
        /// <param name="lists">List of id's.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity AddRemoveContactsFromListsActivity(string accessToken, string apiKey, IList<string> emailAddresses, IList<string> lists)
        {
            Activity activity = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.RemoveFromListsActivity);
            RemoveContact removeContact = new RemoveContact()
            {
                ImportData = new List<ImportEmailAddress>() { new ImportEmailAddress() { EmailAddresses = emailAddresses } }, 
                Lists = lists
            };

            string json = removeContact.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                activity = response.Get<Activity>();
            }

            return activity;
        }

		/// <summary>
		///  Create an Add Contacts Multipart Activity
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>>
		/// <param name="fileName">The file name to be imported</param>
		/// <param name="fileContent">The file content to be imported</param>
		/// <param name="lists">Array of list's id</param>
		/// <returns>Returns an Activity object.</returns>
		public Activity AddContactstMultipartActivity(string accessToken, string apiKey, string fileName, byte[] fileContent, IList<string> lists)
		{
			Activity activity = null;
			string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.AddContactsActivity);
			byte[] data = MultipartBuilder.CreateMultipartContent(fileName, fileContent, lists);
			CUrlResponse response = RestClient.PostMultipart(url, accessToken, apiKey, data);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if(response.HasData)
            {
                activity = response.Get<Activity>();
            }

            return activity;
		}

		/// <summary>
		///  Create a Remove Contacts Multipart Activity
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>>
		/// <param name="fileName">The file name to be imported</param>
		/// <param name="fileContent">The file content to be imported</param>
		/// <param name="lists">Array of list's id</param>
		/// <returns>Returns an Activity object.</returns>
		public Activity RemoveContactsMultipartActivity(string accessToken, string apiKey, string fileName, byte[] fileContent, IList<string> lists)
		{
			Activity activity = null;
			string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.RemoveFromListsActivity);
			byte[] data = MultipartBuilder.CreateMultipartContent(fileName, fileContent, lists);
			CUrlResponse response = RestClient.PostMultipart(url, accessToken, apiKey, data);

			if (response.IsError)
			{
			    throw new CtctException(response.GetErrorMessage());
			}

			if(response.HasData)
			{
			    activity = response.Get<Activity>();
			}

			return activity;
		}
    }
}
