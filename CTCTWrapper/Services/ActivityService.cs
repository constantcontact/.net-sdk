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
        /// Activity service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public ActivityService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Get a list of activities.
        /// </summary>
        /// <returns>Returns the list of activities.</returns>
        public IList<Activity> GetActivities()
        {
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.Activities);
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var activities = response.Get<IList<Activity>>();
                return activities;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get an activity.
        /// </summary>
        /// <param name="activityId">The activity identification.</param>
        /// <returns>Returns the activity identified by its id.</returns>
        public Activity GetActivity(string activityId)
        {
            if (string.IsNullOrEmpty(activityId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ActivityOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Activity, activityId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var activity = response.Get<Activity>();
                return activity;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create an Add Contacts Activity.
        /// </summary>
        /// <param name="addContacts">AddContacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity CreateAddContactsActivity(AddContacts addContacts)
        {
            if (addContacts == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ActivityOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.AddContactsActivity);
            string json = addContacts.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var activity = response.Get<Activity>();
                return activity;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create a Clear Lists Activity.
        /// </summary>
        /// <param name="lists">Array of list id's to be cleared.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity AddClearListsActivity(IList<string> lists)
        {
            if (lists == null || lists.Count.Equals(0))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ActivityOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.ClearListsActivity);

            ClearContactList clearContact = new ClearContactList() { Lists = lists };
            string json = clearContact.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var activity = response.Get<Activity>();
                return activity;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create an Export Contacts Activity.
        /// </summary>
        /// <param name="exportContacts">Export contacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity AddExportContactsActivity(ExportContacts exportContacts)
        {
            if (exportContacts == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ActivityOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.ExportContactsActivity);
            string json = exportContacts.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var activity = response.Get<Activity>();
                return activity;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create a Remove Contacts From Lists Activity.
        /// </summary>
        /// <param name="emailAddresses">List of email addresses.</param>
        /// <param name="lists">List of id's.</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity AddRemoveContactsFromListsActivity(IList<string> emailAddresses, IList<string> lists)
        {
            if (emailAddresses == null || lists == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ActivityOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.RemoveFromListsActivity);

            IList<ImportEmailAddress> emails = new List<ImportEmailAddress>();
            foreach (string email in emailAddresses)
            {
                emails.Add(new ImportEmailAddress() { EmailAddresses = new List<String>() { email } });
            }

            RemoveContact removeContact = new RemoveContact()
            {
                ImportData = emails,
                Lists = lists
            };

            string json = removeContact.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var activity = response.Get<Activity>();
                return activity;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Create an Add Contacts Multipart Activity
        /// </summary>
        /// <param name="fileName">The file name to be imported</param>
        /// <param name="fileContent">The file content to be imported</param>
        /// <param name="lists">Array of list's id</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity AddContactstMultipartActivity(string fileName, byte[] fileContent, IList<string> lists)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileNameNull);
            }

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            string[] fileTypes = new string[4] { ".txt", ".csv", ".xls", ".xlsx" };

            if (!((IList<string>)fileTypes).Contains(extension))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileTypeInvalid);
            }

            if (fileContent == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileNull);
            }

            if (lists == null || lists.Count.Equals(0))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ActivityOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.AddContactsActivity);
            byte[] data = MultipartBuilder.CreateMultipartContent(fileName, fileContent, lists);
            RawApiResponse response = RestClient.PostMultipart(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, data);
            try
            {
                var activity = response.Get<Activity>();
                return activity;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Create a Remove Contacts Multipart Activity
        /// </summary>
        /// <param name="fileName">The file name to be imported</param>
        /// <param name="fileContent">The file content to be imported</param>
        /// <param name="lists">Array of list's id</param>
        /// <returns>Returns an Activity object.</returns>
        public Activity RemoveContactsMultipartActivity(string fileName, byte[] fileContent, IList<string> lists)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileNameNull);
            }

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            string[] fileTypes = new string[4] { ".txt", ".csv", ".xls", ".xlsx" };

            if (!((IList<string>)fileTypes).Contains(extension))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileTypeInvalid);
            }

            if (fileContent == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileNull);
            }

            if (lists == null || lists.Count.Equals(0))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ActivityOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.RemoveFromListsActivity);
            byte[] data = MultipartBuilder.CreateMultipartContent(fileName, fileContent, lists);
            RawApiResponse response = RestClient.PostMultipart(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, data);
            try
            {
                var activity = response.Get<Activity>();
                return activity;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }
    }
}
