#region Using

using System;
using System.Collections.Generic;
using CTCT.Components.Contacts;
using CTCT.Exceptions;
using CTCT.Services;
using CTCT.Util;
using CTCT.Components.Activities;
using CTCT.Components.EmailCampaigns;
using CTCT.Components;
using CTCT.Components.Tracking;
using System.Configuration;
using System.IO;
using System.Text;
using CTCT.Components.MyLibrary;
using CTCT.Components.EventSpot;
using CTCT.Components.AccountService;

#endregion

namespace CTCT
{
    /// <summary>
    /// Main class meant to be used by users to access Constant Contact API functionality.
    /// <example>
    /// ASPX page:
    /// <code>
    /// <![CDATA[
    ///  <%@Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ///         CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default"%>
    ///  ...
    ///     <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
    ///     <asp:Button ID="btnJoin" runat="server" Text="Join" onclick="btnJoin_Click" />
    ///     <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    ///  ...
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Code behind:
    /// <code>
    /// <![CDATA[
    /// public partial class _Default : System.Web.UI.Page
    /// {
    ///    protected void Page_Load(object sender, EventArgs e)
    ///    {
    ///         ...
    ///    }
    ///
    ///    protected void btnJoin_Click(object sender, EventArgs e)
    ///    {
    ///        try
    ///        {
    ///            Contact contact = new Contact();
    ///            // Don't care about the id value
    ///            contact.Id = 1;
    ///            contact.EmailAddresses.Add(new EmailAddress() {
    ///                 EmailAddr = tbxEmail.Text,
    ///                 ConfirmStatus = ConfirmStatus.NoConfirmationRequired,
    ///                 Status = Status.Active });
    ///            contact.Lists.Add(new ContactList() {
    ///                 Id = 1,
    ///                 Status = Status.Active });
    ///
    ///            ConstantContact cc = new ConstantContact();
    ///            cc.AddContact(contact);
    ///            lblMessage.Text = "You have been added to my mailing list!";
    ///        }
    ///        catch (Exception ex) { lblMessage.Text = ex.ToString(); }
    ///    }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Web.config entries:
    /// <code>
    /// <![CDATA[
    /// ...
    /// <appSettings>
    ///     <add key="APIKey" value="APIKey"/>
    ///     <add key="Password" value="password"/>
    ///     <add key="Username" value="username"/>
    ///     <add key="RedirectURL" value="http://somedomain"/>
    /// </appSettings>
    /// ...
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    public class ConstantContact
    {
        #region Fields

		/// <summary>
        /// Gets or sets the AccessToken
        /// </summary>
        private string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the api_key
        /// </summary>
        private string APIKey { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Contact service.
        /// </summary>
        protected virtual IContactService ContactService { get; set; }

        /// <summary>
        /// Gets or sets the List service.
        /// </summary>
        protected virtual IListService ListService { get; set; }

        /// <summary>
        /// Gets or sets the Activity service.
        /// </summary>
        protected virtual IActivityService ActivityService { get; set; }

        /// <summary>
        /// Gets or sets the Campaign Schedule service.
        /// </summary>
        protected virtual ICampaignScheduleService CampaignScheduleService { get; set; }

        /// <summary>
        /// Gets or sets the Campaign Tracking service.
        /// </summary>
        protected virtual ICampaignTrackingService CampaignTrackingService { get; set; }

        /// <summary>
        /// Gets or sets the Contact Tracking service.
        /// </summary>
        protected virtual IContactTrackingService ContactTrackingService { get; set; }

        /// <summary>
        /// Gets or sets the Email Campaign service.
        /// </summary>
        protected virtual IEmailCampaignService EmailCampaignService { get; set; }

        /// <summary>
        /// Gets or sets the Account service
        /// </summary>
        protected virtual IAccountService AccountService { get; set; }

		/// <summary>
		/// Gets or sets the MyLibrary service
		/// </summary>
		protected virtual IMyLibraryService MyLibraryService { get; set; }

        /// <summary>
        /// Gets of sets the EventSpot service
        /// </summary>
        protected virtual IEventSpotService EventSpotService { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new ConstantContact object using provided apiKey and accessToken parameters
        /// </summary>
        /// <param name="apiKey">APIKey</param>
        /// <param name="accessToken">access token</param>
        public ConstantContact(string apiKey, string accessToken)
        {
            this.InitializeFields();

            this.AccessToken = accessToken;
            this.APIKey = apiKey;
        }

        #endregion

		#region Private methods

        private void InitializeFields()
        {
            this.ContactService = new ContactService();
            this.ListService = new ListService();
            this.ActivityService = new ActivityService();
            this.CampaignScheduleService = new CampaignScheduleService();
            this.CampaignTrackingService = new CampaignTrackingService();
            this.ContactTrackingService = new ContactTrackingService();
            this.EmailCampaignService = new EmailCampaignService();
            this.AccountService = new AccountService();
			this.MyLibraryService = new MyLibraryService();
            this.EventSpotService = new EventSpotService();
        }

        #endregion

        #region Public methods

        #region Contact service

        /// <summary>
        /// Get a set of contacts.
        /// </summary>
        /// <param name="email">Match the exact email address.</param>
        /// <param name="limit">Limit the number of returned values, default 500.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
		/// <param name="status">Match the exact contact status</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(string email, int? limit, DateTime? modifiedSince, ContactStatus? status)
        {
            return ContactService.GetContacts(AccessToken, APIKey, email, limit, modifiedSince, status);
        }

        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(DateTime? modifiedSince, Pagination pag)
        {
            return ContactService.GetContacts(AccessToken, APIKey, modifiedSince, pag);
        }

        /// <summary>
        /// Get a set of contacts.
        /// </summary>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(DateTime? modifiedSince)
        {
            return ContactService.GetContacts(AccessToken, APIKey, modifiedSince, null);
        }

        /// <summary>
        /// Get an individual contact.
        /// </summary>
        /// <param name="contactId">Id of the contact to retrieve</param>
        /// <returns>Returns a contact.</returns>
        public Contact GetContact(string contactId)
        {
			if (string.IsNullOrEmpty(contactId))
			{
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
			}

            return ContactService.GetContact(AccessToken, APIKey, contactId);
        }

        /// <summary>
        /// Add a new contact to an account.
        /// </summary>
        /// <param name="contact">Contact to add.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the newly created contact.</returns>
        public Contact AddContact(Contact contact, bool actionByVisitor)
        {
			if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return ContactService.AddContact(AccessToken, APIKey, contact, actionByVisitor);
        }

        /// <summary>
        /// Sets an individual contact to 'Unsubscribed' status.
        /// </summary>
        /// <param name="contact">Contact object.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContact(Contact contact)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return this.DeleteContact(contact.Id);
        }

        /// <summary>
        /// Sets an individual contact to 'Unsubscribed' status.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContact(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return ContactService.DeleteContact(AccessToken, APIKey, contactId);
        }

		/// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contact">Contact object.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromLists(Contact contact)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return this.DeleteContactFromLists(contact.Id);
        }

        /// <summary>
        /// Delete a contact from all contact lists. Sets them to 'Removed' status.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromLists(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return ContactService.DeleteContactFromLists(AccessToken, APIKey, contactId);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contact">Contact object.</param>
        /// <param name="list">List object.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromList(Contact contact, ContactList list)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }
            if (list == null)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return this.DeleteContactFromList(contact.Id, list.Id);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="listId">List id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromList(string contactId, string listId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ContactService.DeleteContactFromList(AccessToken, APIKey, contactId, listId);
        }

        /// <summary>
        /// Update an individual contact.
        /// </summary>
        /// <param name="contact">Contact to update.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the updated contact.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Contact UpdateContact(Contact contact, bool actionByVisitor)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return ContactService.UpdateContact(AccessToken, APIKey, contact, actionByVisitor);
        }

        #endregion

        #region List service

        /// <summary>
        /// Get lists.
        /// </summary>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns the list of lists where contact belong to.</returns>
        public IList<ContactList> GetLists(DateTime? modifiedSince)
        {
            return ListService.GetLists(AccessToken, APIKey, modifiedSince);
        }

        /// <summary>
        /// Get an individual list.
        /// </summary>
        /// <param name="listId">Id of the list to retrieve</param>
        /// <returns>Returns contact list.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ContactList GetList(string listId)
        {
            if (string.IsNullOrEmpty(listId))
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.GetList(AccessToken, APIKey, listId);
        }

        /// <summary>
        /// Update a Contact List.
        /// </summary>
        /// <param name="list">ContactList to be updated</param>
        /// <returns>Contact list</returns>
        public ContactList UpdateList(ContactList list)
        {
            if (list == null)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.UpdateList(AccessToken, APIKey, list);
        }

        /// <summary>
        /// Add a new list to an account.
        /// </summary>
        /// <param name="list">List to add.</param>
        /// <returns>Returns the newly created list.</returns>
        public ContactList AddList(ContactList list)
        {
			if (list == null)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.AddList(AccessToken, APIKey, list);
        }

        /// <summary>
        /// Delete a Contact List.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <returns>return true if list was deleted successfully, false otherwise</returns>
        public bool DeleteList(string listId)
        {
            if (string.IsNullOrEmpty(listId))
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.DeleteList(AccessToken, APIKey, listId);
        }

        /// <summary>
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="list">Contact list object.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns the list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<Contact> GetContactsFromList(ContactList list, DateTime? modifiedSince)
        {
            return this.GetContactsFromList(list.Id, null, modifiedSince, null);
        }

        /// <summary>
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="list">Contact list object.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns the list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<Contact> GetContactsFromList(ContactList list, int? limit, DateTime? modifiedSince)
        {
            return this.GetContactsFromList(list.Id, limit, modifiedSince, null);
        }

        /// <summary>
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="listId">Contact list id.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<Contact> GetContactsFromList(string listId, DateTime? modifiedSince)
        {
            return this.GetContactsFromList(listId, null, modifiedSince, null);
        }

        /// <summary>
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="listId">Contact list id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<Contact> GetContactsFromList(string listId, int? limit, DateTime? modifiedSince)
        {
            return this.GetContactsFromList(listId, limit, modifiedSince, null);
        }

		/// <summary>
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="listId">Contact list id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
		/// /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
		public ResultSet<Contact> GetContactsFromList(string listId, int? limit, DateTime? modifiedSince, Pagination pag)
		{
			if (string.IsNullOrEmpty(listId))
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.GetContactsFromList(AccessToken, APIKey, listId, limit, modifiedSince, pag);
		}

        #endregion

        #region Activity service

        /// <summary>
        /// Get a list of activities.
        /// </summary>
        /// <returns>Returns the list of activities.</returns>
        public IList<Activity> GetActivities()
        {
            return ActivityService.GetActivities(AccessToken, APIKey);
        }

        /// <summary>
        /// Get an activity.
        /// </summary>
        /// <param name="activityId">The activity identification.</param>
        /// <returns>Returns the activity identified by its id.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Activity GetActivity(string activityId)
        {
            if (string.IsNullOrEmpty(activityId))
            {
                throw new IllegalArgumentException(Config.Errors.ActivityOrId);
            }

            return ActivityService.GetActivity(AccessToken, APIKey, activityId);
        }

        /// <summary>
        /// Create an Add Contacts Activity.
        /// </summary>
        /// <param name="addContacts">AddContacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Activity CreateAddContactsActivity(AddContacts addContacts)
        {
            if (addContacts == null)
            {
                throw new IllegalArgumentException(Config.Errors.ActivityOrId);
            }

            return ActivityService.CreateAddContactsActivity(AccessToken, APIKey, addContacts);
        }

        /// <summary>
        /// Create a Clear Lists Activity.
        /// </summary>
        /// <param name="lists">Array of list id's to be cleared.</param>
        /// <returns>Returns an Activity object.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Activity AddClearListsActivity(IList<string> lists)
        {
            if (lists == null || lists.Count.Equals(0))
            {
                throw new IllegalArgumentException(Config.Errors.ActivityOrId);
            }

            return ActivityService.AddClearListsActivity(AccessToken, APIKey, lists);
        }

        /// <summary>
        /// Create an Export Contacts Activity.
        /// </summary>
        /// <param name="exportContacts">Export contacts object.</param>
        /// <returns>Returns an Activity object.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Activity AddExportContactsActivity(ExportContacts exportContacts)
        {
            if (exportContacts == null)
            {
                throw new IllegalArgumentException(Config.Errors.ActivityOrId);
            }

            return ActivityService.AddExportContactsActivity(AccessToken, APIKey, exportContacts);
        }

        /// <summary>
        /// Create a Remove Contacts From Lists Activity.
        /// </summary>
        /// <param name="emailAddresses">List of email addresses.</param>
        /// <param name="lists">List of id's.</param>
        /// <returns>Returns an Activity object.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Activity AddRemoveContactsFromListsActivity(IList<string> emailAddresses, IList<string> lists)
        {
            if (emailAddresses == null || lists == null)
            {
                throw new IllegalArgumentException(Config.Errors.ActivityOrId);
            }

            return ActivityService.AddRemoveContactsFromListsActivity(AccessToken, APIKey, emailAddresses, lists);
        }

		/// <summary>
		/// Create an Add Contacts Multipart Activity
		/// </summary>
		/// <param name="fileName">The name of the file</param>
		/// <param name="fileContent">The contents of the file</param>
		/// <param name="lists">List of contact list Ids to add the contacts to</param>
		/// <returns>Returns an Activity object.</returns>
		public Activity AddContactsMultipartActivity(string fileName, byte[] fileContent, IList<string> lists)
		{
			if(string.IsNullOrEmpty(fileName))
			{
				throw new IllegalArgumentException(Config.Errors.FileNameNull);
			}

			var extension = Path.GetExtension(fileName).ToLowerInvariant();
			string[] fileTypes = new string[4] { ".txt", ".csv", ".xls", ".xlsx" };

			if (!((IList<string>)fileTypes).Contains(extension))
			{
			    throw new IllegalArgumentException(Config.Errors.FileTypeInvalid);
			}

			if(fileContent == null)
			{
				throw new IllegalArgumentException(Config.Errors.FileNull);
			}

			if(lists == null || lists.Count.Equals(0))
			{
				throw new IllegalArgumentException(Config.Errors.ActivityOrId);
			}

			return ActivityService.AddContactstMultipartActivity(AccessToken, APIKey, fileName, fileContent, lists);
		}

		/// <summary>
		/// Create a Remove Contacts Multipart Activity
		/// </summary>
		/// <param name="fileName">The name of the file</param>
		/// <param name="fileContent">The contents of the file</param>
		/// <param name="lists">List of contact list Ids to add to remove the contacts to</param>
		/// <returns>Returns an Activity object.</returns>
		public Activity RemoveContactsMultipartActivity(string fileName, byte[] fileContent, IList<string> lists)
		{
			if(string.IsNullOrEmpty(fileName))
			{
				throw new IllegalArgumentException(Config.Errors.FileNameNull);
			}

			var extension = Path.GetExtension(fileName).ToLowerInvariant();
			string[] fileTypes = new string[4] { ".txt", ".csv", ".xls", ".xlsx" };

			if (!((IList<string>)fileTypes).Contains(extension))
			{
			    throw new IllegalArgumentException(Config.Errors.FileTypeInvalid);
			}

			if(fileContent == null)
			{
				throw new IllegalArgumentException(Config.Errors.FileNull);
			}

			if(lists == null || lists.Count.Equals(0))
			{
				throw new IllegalArgumentException(Config.Errors.ActivityOrId);
			}

			return ActivityService.RemoveContactsMultipartActivity(AccessToken, APIKey, fileName, fileContent, lists);
		}

        #endregion

        #region CampaignSchedule service

        /// <summary>
        /// Create a new schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to be created.</param>
        /// <returns>Returns the scheduled object.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Schedule AddSchedule(string campaignId, Schedule schedule)
        {
            if (string.IsNullOrEmpty(campaignId) || schedule == null)
            {
                throw new IllegalArgumentException(Config.Errors.ScheduleOrId);
            }

            return CampaignScheduleService.AddSchedule(AccessToken, APIKey, campaignId, schedule);
        }

        /// <summary>
        /// Get a list of schedules for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <returns>Returns the list of schedules for specified campaign.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public IList<Schedule> GetSchedules(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.ScheduleOrId);
            }

            return CampaignScheduleService.GetSchedules(AccessToken, APIKey, campaignId);
        }

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be get a schedule for.</param>
        /// <param name="scheduleId">Schedule id to retrieve.</param>
        /// <returns>Returns the schedule object for the requested campaign.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Schedule GetSchedule(string campaignId, string scheduleId)
        {
            if (string.IsNullOrEmpty(campaignId) || string.IsNullOrEmpty(scheduleId))
            {
                throw new IllegalArgumentException(Config.Errors.ScheduleOrId);
            }

            return CampaignScheduleService.GetSchedule(AccessToken, APIKey, campaignId, scheduleId);
        }

        /// <summary>
        /// Update a specific schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to retrieve.</param>
        /// <returns>Returns the updated schedule object.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Schedule UpdateSchedule(string campaignId, Schedule schedule)
        {
            if (string.IsNullOrEmpty(campaignId) || schedule == null)
            {
                throw new IllegalArgumentException(Config.Errors.ScheduleOrId);
            }

            return CampaignScheduleService.AddSchedule(AccessToken, APIKey, campaignId, schedule);
        }

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="scheduleId">Schedule id to delete.</param>
        /// <returns>Returns true if schedule was successfully deleted.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteSchedule(string campaignId, string scheduleId)
        {
            if (string.IsNullOrEmpty(campaignId) || string.IsNullOrEmpty(scheduleId))
            {
                throw new IllegalArgumentException(Config.Errors.ScheduleOrId);
            }

            return CampaignScheduleService.DeleteSchedule(AccessToken, APIKey, campaignId, scheduleId);
        }

        /// <summary>
        /// Send a test send of a campaign.
        /// </summary>
        /// <param name="campaignId">Id of campaign to send test of.</param>
        /// <param name="testSend">Test send details.</param>
        /// <returns>Returns the sent object.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public TestSend SendTest(string campaignId, TestSend testSend)
        {
            if (string.IsNullOrEmpty(campaignId) || testSend == null)
            {
                throw new IllegalArgumentException(Config.Errors.ScheduleOrId);
            }

            return CampaignScheduleService.SendTest(AccessToken, APIKey, campaignId, testSend);
        }

        #endregion

        #region CampaignTracking service

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        public ResultSet<BounceActivity> GetCampaignTrackingBounces(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetBounces(AccessToken, APIKey, campaignId, limit, createdSince);
        }

        /// <summary>
        /// Get a result set of bounces for a given campaign.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
        public ResultSet<BounceActivity> GetCampaignTrackingBounces(Pagination pag)
        {
            return CampaignTrackingService.GetBounces(AccessToken, APIKey, pag);
        }

        /// <summary>
        /// Get clicks for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetCampaignTrackingClicks(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetClicks(AccessToken, APIKey, campaignId, limit, createdSince);
        }

        /// <summary>
        /// Get clicks for a specific link in a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="linkId">Specifies the link in the email campaign to retrieve click data for.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetCampaignTrackingClicks(string campaignId, string linkId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetClicks(AccessToken, APIKey, campaignId, linkId, limit, createdSince);
        }

		/// <summary>
		/// Get clicks for a specific link in a campaign.
		/// </summary>
		/// <param name="pag">Pagination object.</param>
		/// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetCampaignTrackingClicks(Pagination pag)
        {
            return CampaignTrackingService.GetClicks(AccessToken, APIKey, pag);
        }

        /// <summary>
        /// Get clicks for a given campaign.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
        public ResultSet<ClickActivity> GetClicks(Pagination pag)
        {
            return CampaignTrackingService.GetClicks(AccessToken, APIKey, pag);
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetCampaignTrackingForwards(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetForwards(AccessToken, APIKey, campaignId, limit, createdSince);
        }

        /// <summary>
        /// Get forwards for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetCampaignTrackingForwards(DateTime? createdSince, Pagination pag)
        {
            return CampaignTrackingService.GetForwards(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetCampaignTrackingOpens(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetOpens(AccessToken, APIKey, campaignId, limit, createdSince);
        }

        /// <summary>
        /// Get opens for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetCampaignTrackingOpens(DateTime? createdSince, Pagination pag)
        {
            return CampaignTrackingService.GetOpens(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        public ResultSet<SendActivity> GetCampaignTrackingSends(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetSends(AccessToken, APIKey, campaignId, limit, createdSince);
        }

        /// <summary>
        /// Get sends for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
        public ResultSet<SendActivity> GetCampaignTrackingSends(DateTime? createdSince, Pagination pag)
        {
            return CampaignTrackingService.GetSends(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetCampaignTrackingOptOuts(string campaignId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetOptOuts(AccessToken, APIKey, campaignId, limit, createdSince);
        }

        /// <summary>
        /// Get opt outs for a given campaign.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetCampaignTrackingOptOuts(DateTime? createdSince, Pagination pag)
        {
            return CampaignTrackingService.GetOptOuts(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get a summary of reporting data for a given campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Tracking summary.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public TrackingSummary GetCampaignTrackingSummary(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.CampaignTrackingOrId);
            }

            return CampaignTrackingService.GetSummary(AccessToken, APIKey, campaignId);
        }

        #endregion

        #region ContactTracking service

		/// <summary>
		/// Get all activities for a given contact.
		/// </summary>
        /// <param name="contactId">Contact id.</param>
		/// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
		/// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
		public ResultSet<ContactActivity> GetContactTrackingActivities(string contactId)
		{
			return this.GetContactTrackingActivities(contactId, null, null);
		}

		/// <summary>
		/// Get all activities for a given contact.
		/// </summary>
        /// <param name="contactId">Contact id.</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
		/// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
		/// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
		public ResultSet<ContactActivity> GetContactTrackingActivities(string contactId, int? limit, DateTime? createdSince)
		{
			if (string.IsNullOrEmpty(contactId))
			{
			    throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
			}

		    return ContactTrackingService.GetActivities(AccessToken, APIKey, contactId, limit, createdSince);
		}

		/// <summary>
		/// Get all activities for a given contact.
		/// </summary>
        /// <param name="contactId">Contact id.</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
		/// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>	 
		/// <param name="pag">Pagination object.</param>
		/// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
		/// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
		public ResultSet<ContactActivity> GetContactTrackingActivities(string contactId, int? limit, DateTime? createdSince, Pagination pag)
		{
			if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

			return ContactTrackingService.GetActivities(AccessToken, APIKey, contactId, limit, createdSince, pag);
		}

		/// <summary>
        /// Get activities by email campaign for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>ResultSet containing a results array of @link TrackingSummary.</returns>
		/// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<TrackingSummary> GetContactTrackingEmailCampaignActivities(string contactId)
		{
			if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

			return ContactTrackingService.GetEmailCampaignActivities(AccessToken, APIKey, contactId);
		}

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<BounceActivity> GetContactTrackingBounces(string contactId)
        {
            return this.GetContactTrackingBounces(contactId, null);
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<BounceActivity> GetContactTrackingBounces(string contactId, int? limit)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

            return ContactTrackingService.GetBounces(AccessToken, APIKey, contactId, limit);
        }

        /// <summary>
        /// Get bounces for a given contact.
        /// </summary>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
        public ResultSet<BounceActivity> GetContactTrackingBounces(Pagination pag)
        {
            return ContactTrackingService.GetBounces(AccessToken, APIKey, pag);
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<ClickActivity> GetContactTrackingClicks(string contactId, DateTime? createdSince)
        {
            return this.GetContactTrackingClicks(contactId, null, createdSince);
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<ClickActivity> GetContactTrackingClicks(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

            return ContactTrackingService.GetClicks(AccessToken, APIKey, contactId, limit, createdSince);
        }

        /// <summary>
        /// Get clicks for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
        public ResultSet<ClickActivity> GetContactTrackingClicks(DateTime? createdSince, Pagination pag)
        {
            return ContactTrackingService.GetClicks(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<ForwardActivity> GetContactTrackingForwards(string contactId, DateTime? createdSince)
        {
            return this.GetContactTrackingForwards(contactId, null, createdSince);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<ForwardActivity> GetContactTrackingForwards(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

            return ContactTrackingService.GetForwards(AccessToken, APIKey, contactId, limit, createdSince);
        }

        /// <summary>
        /// Get forwards for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
        public ResultSet<ForwardActivity> GetContactTrackingForwards(DateTime? createdSince, Pagination pag)
        {
            return ContactTrackingService.GetForwards(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<OpenActivity> GetContactTrackingOpens(string contactId, DateTime? createdSince)
        {
            return this.GetContactTrackingOpens(contactId, null, createdSince);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<OpenActivity> GetContactTrackingOpens(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

            return ContactTrackingService.GetOpens(AccessToken, APIKey, contactId, limit, createdSince);
        }

        /// <summary>
        /// Get opens for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
        public ResultSet<OpenActivity> GetContactTrackingOpens(DateTime? createdSince, Pagination pag)
        {
            return ContactTrackingService.GetOpens(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<SendActivity> GetContactTrackingSends(string contactId, DateTime? createdSince)
        {
            return this.GetContactTrackingSends(contactId, null, createdSince);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<SendActivity> GetContactTrackingSends(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

            return ContactTrackingService.GetSends(AccessToken, APIKey, contactId, limit, createdSince);
        }

        /// <summary>
        /// Get sends for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
        public ResultSet<SendActivity> GetContactTrackingSends(DateTime? createdSince, Pagination pag)
        {
            return ContactTrackingService.GetSends(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<OptOutActivity> GetContactTrackingOptOuts(string contactId, DateTime? createdSince)
        {
            return this.GetContactTrackingOptOuts(contactId, null, createdSince);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<OptOutActivity> GetContactTrackingOptOuts(string contactId, int? limit, DateTime? createdSince)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

            return ContactTrackingService.GetOptOuts(AccessToken, APIKey, contactId, limit, createdSince);
        }

        /// <summary>
        /// Get opt outs for a given contact.
        /// </summary>
        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
        public ResultSet<OptOutActivity> GetContactTrackingOptOuts(DateTime? createdSince, Pagination pag)
        {
            return ContactTrackingService.GetOptOuts(AccessToken, APIKey, createdSince, pag);
        }

        /// <summary>
        /// Get a summary of reporting data for a given contact.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Tracking summary.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public TrackingSummary GetContactTrackingSummary(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(Config.Errors.ContactTrackingOrId);
            }

            return ContactTrackingService.GetSummary(AccessToken, APIKey, contactId);
        }

        #endregion

        #region EmailCampaign service

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="pagination">Select the next page of campaigns from a pagination</param>
        /// <returns>Returns a list of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(Pagination pagination)
        {
            return this.GetCampaigns(null, null, null, pagination);
        }

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a list of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(DateTime? modifiedSince)
        {
            return this.GetCampaigns(null, null, modifiedSince, null);
        }

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a list of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(CampaignStatus status, DateTime? modifiedSince)
        {
            return this.GetCampaigns(status, null, modifiedSince, null);
        }

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <param name="pagination">Pagination object supplied by a previous call to GetCampaigns when another page is present</param>
        /// <returns>Returns a list of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(CampaignStatus? status, int? limit, DateTime? modifiedSince, Pagination pagination)
        {
            return EmailCampaignService.GetCampaigns(AccessToken, APIKey, status, limit, modifiedSince, pagination);
        }

        /// <summary>
        /// Get campaign details for a specific campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a campaign.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public EmailCampaign GetCampaign(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.EmailCampaignOrId);
            }

            return EmailCampaignService.GetCampaign(AccessToken, APIKey, campaignId);
        }

        /// <summary>
        /// Create a new campaign.
        /// </summary>
        /// <param name="campaign">Campign to be created</param>
        /// <returns>Returns a campaign.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public EmailCampaign AddCampaign(EmailCampaign campaign)
        {
            if (campaign == null)
            {
                throw new IllegalArgumentException(Config.Errors.EmailCampaignOrId);
            }

            return EmailCampaignService.AddCampaign(AccessToken, APIKey, campaign);
        }

        /// <summary>
        /// Delete an email campaign.
        /// </summary>
        /// <param name="campaignId">Valid campaign id.</param>
        /// <returns>Returns true if successful.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteCampaign(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(Config.Errors.EmailCampaignOrId);
            }

            return EmailCampaignService.DeleteCampaign(AccessToken, APIKey, campaignId);
        }

        /// <summary>
        /// Update a specific email campaign.
        /// </summary>
        /// <param name="campaign">Campaign to be updated.</param>
        /// <returns>Returns a campaign.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public EmailCampaign UpdateCampaign(EmailCampaign campaign)
        {
            if (campaign == null)
            {
                throw new IllegalArgumentException(Config.Errors.EmailCampaignOrId);
            }

            return EmailCampaignService.UpdateCampaign(AccessToken, APIKey, campaign);
        }

        #endregion

        #region Account service

        /// <summary>
        /// Retrieve a list of all the account owner's email addresses
        /// </summary>
        /// <returns>list of all verified account owner's email addresses</returns>
        public IList<VerifiedEmailAddress> GetVerifiedEmailAddress()
        {
            return AccountService.GetVerifiedEmailAddress(AccessToken, APIKey);
        }

        /// <summary>
        /// Get account summary information
        /// </summary>
        /// <returns>An AccountSummaryInformation object</returns>
        public AccountSummaryInformation GetAccountSummaryInformation()
        { 
            return AccountService.GetAccountSummaryInformation(AccessToken, APIKey);
        }

        /// <summary>
        /// Updates account summary information
        /// </summary>
        /// <param name="accountSumaryInfo">An AccountSummaryInformation object</param>
        /// <returns>An AccountSummaryInformation object</returns>
        public AccountSummaryInformation PutAccountSummaryInformation(AccountSummaryInformation accountSumaryInfo)
        {
            return AccountService.PutAccountSummaryInformation(AccessToken, APIKey, accountSumaryInfo);
        }

        #endregion Account service

		#region MyLibrary service

		/// <summary>
		/// Get MyLibrary usage information
		/// </summary>
		/// <returns>Returns a MyLibraryInfo object</returns>
		public MyLibraryInfo GetLibraryInfo()
		{
			return MyLibraryService.GetLibraryInfo(AccessToken, APIKey);
		}

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		public ResultSet<MyLibraryFolder> GetLibraryFolders()
		{
			return this.GetLibraryFolders(null, null, null);
		}

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		public ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy)
		{
			return this.GetLibraryFolders(sortBy, null, null);
		}

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		public ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy, int? limit)
		{
			return this.GetLibraryFolders(sortBy, limit, null);
		}

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		public ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy, int? limit, Pagination pag)
		{
			return MyLibraryService.GetLibraryFolders(this.AccessToken, this.APIKey, sortBy, limit, pag);
		}

		/// <summary>
		/// Add new folder to MyLibrary
		/// </summary>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder AddLibraryFolder(MyLibraryFolder folder)
		{
			if (folder == null)
            {
                throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
            }

			return MyLibraryService.AddLibraryFolder(this.AccessToken, this.APIKey, folder);
		}

		/// <summary>
		/// Get a folder by Id
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder GetLibraryFolder(string folderId)
		{
			if(string.IsNullOrEmpty(folderId))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return MyLibraryService.GetLibraryFolder(this.AccessToken, this.APIKey, folderId);
		}

		/// <summary>
		/// Update name and parent_id for a specific folder
		/// </summary>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder)
		{
			return this.UpdateLibraryFolder(folder, null);
		}

		/// <summary>
		/// Update name and parent_id for a specific folder
		/// </summary>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder, bool? includePayload)
		{
			if (folder == null)
            {
                throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
            }

			return MyLibraryService.UpdateLibraryFolder(this.AccessToken, this.APIKey, folder, includePayload);
		}

		/// <summary>
		/// Delete a specific folder
		/// </summary>
		/// <param name="folder">The folder to be deleted</param>
		 /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFolder(MyLibraryFolder folder)
		{
			if(folder == null)
			{
				 throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return this.DeleteLibraryFolder(folder.Id);
		}

		/// <summary>
		/// Delete a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		 /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFolder(string folderId)
		{
			if(string.IsNullOrEmpty(folderId))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return MyLibraryService.DeleteLibraryFolder(this.AccessToken, this.APIKey, folderId);
		}

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryTrashFiles()
		{
			return this.GetLibraryTrashFiles(null, null, null, null);
		}

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type)
		{
			return this.GetLibraryTrashFiles(type, null, null, null);
		}

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy)
		{
			return this.GetLibraryTrashFiles(type, sortBy, null, null);
		}

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy, int? limit)
		{
			return this.GetLibraryTrashFiles(type, sortBy, limit, null);
		}

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy, int? limit, Pagination pag)
		{
			return MyLibraryService.GetLibraryTrashFiles(this.AccessToken, this.APIKey, type, sortBy, limit, pag);
		}

		/// <summary>
		/// Delete files in Trash folder
		/// </summary>
		 /// <returns>Returns true if files were deleted successfully, false otherwise</returns>
		public bool DeleteLibraryTrashFiles()
		{
			return MyLibraryService.DeleteLibraryTrashFiles(this.AccessToken, this.APIKey);
		}

		/// <summary>
		/// Get files
		/// </summary>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFiles()
		{
			return this.GetLibraryFiles(null, null, null, null);
		}

		/// <summary>
		/// Get files
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type)
		{
			return this.GetLibraryFiles(type, null, null, null);
		}

		/// <summary>
		/// Get files
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="source">Specifies to retrieve files from a particular source</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source)
		{
			return this.GetLibraryFiles(type, source, null, null);
		}

		/// <summary>
		/// Get files
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="source">Specifies to retrieve files from a particular source</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source, int? limit)
		{
			return this.GetLibraryFiles(type, source, limit, null);
		}

		/// <summary>
		/// Get files
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="source">Specifies to retrieve files from a particular source</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source, int? limit, Pagination pag)
		{
			return MyLibraryService.GetLibraryFiles(this.AccessToken, this.APIKey, type, source, limit, pag);
		}

		/// <summary>
		/// Get files from a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder from which to retrieve files</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId)
		{
			return this.GetLibraryFilesByFolder(folderId, null, null);
		}

		/// <summary>
		/// Get files from a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder from which to retrieve files</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId, int? limit)
		{
			return this.GetLibraryFilesByFolder(folderId, limit, null);
		}

		/// <summary>
		/// Get files from a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder from which to retrieve files</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId, int? limit, Pagination pag)
		{
			if(string.IsNullOrEmpty(folderId))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return MyLibraryService.GetLibraryFilesByFolder(this.AccessToken, this.APIKey, folderId, limit, pag);
		}

		/// <summary>
		/// Get file after id
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		public MyLibraryFile GetLibraryFile(string fileId)
		{
			if(string.IsNullOrEmpty(fileId))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return MyLibraryService.GetLibraryFile(this.AccessToken, this.APIKey, fileId);
		}

		/// <summary>
		/// Update a specific file
		/// </summary>
		/// <param name="file">File to be updated</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		public MyLibraryFile UpdateLibraryFile(MyLibraryFile file)
		{
			return this.UpdateLibraryFile(file, null);
		}

		/// <summary>
		/// Update a specific file
		/// </summary>
		/// <param name="file">File to be updated</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		public MyLibraryFile UpdateLibraryFile(MyLibraryFile file, bool? includePayload)
		{
			if(file == null)
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}
						
			return MyLibraryService.UpdateLibraryFile(this.AccessToken, this.APIKey, file, includePayload);
		}

		/// <summary>
		/// Delete a specific file
		/// </summary>
		/// <param name="file">The file to be deleted</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFile(MyLibraryFile file)
		{
			if(file == null)
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return this.DeleteLibraryFile(file.Id);
		}

		/// <summary>
		/// Delete a specific file
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFile(string fileId)
		{
			if(string.IsNullOrEmpty(fileId))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return MyLibraryService.DeleteLibraryFile(this.AccessToken, this.APIKey, fileId);
		}

		/// <summary>
		/// Get status for an upload file
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a list of FileUploadStatus objects</returns>
		public IList<FileUploadStatus> GetLibraryFileUploadStatus(string fileId)
		{
			if(string.IsNullOrEmpty(fileId))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return MyLibraryService.GetLibraryFileUploadStatus(this.AccessToken, this.APIKey, fileId);
		}

		/// <summary>
		/// Move files to a different folder
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="fileIds">List of comma separated file ids</param>
		/// <returns>Returns a list of FileMoveResult objects.</returns>
		public IList<FileMoveResult> MoveLibraryFile(string folderId, IList<string> fileIds)
		{
			if(string.IsNullOrEmpty(folderId))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			if(fileIds == null || fileIds.Count.Equals(0))
			{
				throw new IllegalArgumentException(Config.Errors.MyLibraryOrId);
			}

			return MyLibraryService.MoveLibraryFile(this.AccessToken, this.APIKey, folderId, fileIds);
		}

		/// <summary>
		/// Add files using the multipart content-type
		/// </summary>
		/// <param name="fileName">The file name and extension</param>
		/// <param name="fileType">The file type</param>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="description">The description of the file</param>
		/// <param name="source">The source of the original file</param>
		/// <param name="data">The data contained in the file being uploaded</param>
		/// <returns>Returns the file Id associated with the uploaded file</returns>
		public string AddLibraryFilesMultipart(string fileName, FileType fileType, string folderId, string description, FileSource source, byte[] data)
		{
			if(string.IsNullOrEmpty(fileName))
			{
				throw new IllegalArgumentException(Config.Errors.FileNameNull);
			}

			var extension = Path.GetExtension(fileName).ToLowerInvariant();
			string[] fileTypes = new string[5] { ".jpeg", ".jpg", ".gif", ".png", ".pdf" };

			if (!((IList<string>)fileTypes).Contains(extension))
			{
			    throw new IllegalArgumentException(Config.Errors.FileTypeInvalid);
			}

			if(string.IsNullOrEmpty(folderId) || string.IsNullOrEmpty(description))
			{
				throw new IllegalArgumentException(Config.Errors.FieldNull);
			}

			if(data == null)
			{
				throw new IllegalArgumentException(Config.Errors.FileNull);
			}

			return MyLibraryService.AddLibraryFilesMultipart(this.AccessToken, this.APIKey, fileName, fileType, folderId, description, source, data);
		}

		#endregion

        #region EventSpot service

        /// <summary>
        /// View all existing events
        /// </summary>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 50</param>
        /// <param name="pag">Pagination object</param>
        /// <returns>ResultSet containing a results array of IndividualEvents</returns>
        public ResultSet<IndividualEvent> GetAllEventSpots(int? limit, Pagination pag)
        {
            return EventSpotService.GetAllEventSpots(this.AccessToken, this.APIKey, limit, pag);
        }


         /// <summary>
        /// Retrieve an event specified by the event_id
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>The event</returns>
        public IndividualEvent GetEventSpot(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetEventSpot(this.AccessToken, this.APIKey, eventId);
        }

           /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="eventSpot">The event to publish</param>
        /// <returns>The published event</returns>
        public IndividualEvent PostEventSpot(IndividualEvent eventSpot)
        {
            if (eventSpot == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PostEventSpot(this.AccessToken, this.APIKey, eventSpot);
        }

         /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="eventId">Event id to be updated</param>
        /// <param name="eventSpot">The new values for event</param>
        /// <returns>The updated event</returns>
        public IndividualEvent PutEventSpot( string eventId, IndividualEvent eventSpot)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (eventSpot == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PutEventSpot(this.AccessToken, this.APIKey, eventId, eventSpot);
        }

         /// <summary>
        /// Publish or cancel an event by changing the status of the event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="eventStatus">New status of the event. ACTIVE" and "CANCELLED are allowed</param>
        /// <returns>The updated event</returns>
        public IndividualEvent PatchEventSpotStatus(string eventId, EventStatus eventStatus)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.PatchEventSpotStatus(this.AccessToken, this.APIKey, eventId, eventStatus);
        }




        /// <summary>
        /// Retrieve all existing fees for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of event fees for the specified event</returns>
        public List<EventFee> GetAllEventFees(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetAllEventFees(this.AccessToken, this.APIKey, eventId);
        }

        /// <summary>
        /// Retrieve an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>An EventFee object</returns>
        public EventFee GetEventFee(string eventId, string feeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(feeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetEventFee(this.AccessToken, this.APIKey, eventId, feeId);
        }

         /// <summary>
        /// Update an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <param name="eventFee">The new values of EventFee</param>
        /// <returns>The updated EventFee</returns>
        public EventFee PutEventFee(string eventId, string feeId, EventFee eventFee)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(feeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (eventFee == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PutEventFee(this.AccessToken, this.APIKey, eventId, feeId, eventFee);
        }


        /// <summary>
        ///  Delete an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventFee(string eventId, string feeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(feeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.DeleteEventFee(this.AccessToken, this.APIKey, eventId, feeId);
        }

        /// <summary>
        /// Create an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="eventFee">EventFee object</param>
        /// <returns>The newly created EventFee</returns>
        public EventFee PostEventFee(string eventId, EventFee eventFee)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (eventFee == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PostEventFee(this.AccessToken, this.APIKey, eventId, eventFee);
        }

        /// <summary>
        /// Retrieve all existing promo codes for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of Promocode</returns>
        public List<Promocode> GetAllPromocodes(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetAllPromocodes(this.AccessToken, this.APIKey, eventId);
        }

        /// <summary>
        /// Retrieve an existing promo codes for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>The Promocode object</returns>
        public Promocode GetPromocode(string eventId, string promocodeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(promocodeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetPromocode(this.AccessToken, this.APIKey, eventId, promocodeId);
        }

         /// <summary>
        /// Create a new promo code for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocode">Promocode object to be created</param>
        /// <returns>The newly created Promocode</returns>
        public Promocode PostPromocode(string eventId, Promocode promocode)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (promocode == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PostPromocode(this.AccessToken, this.APIKey, eventId, promocode);
        }

        /// <summary>
        /// Update a promo code for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <param name="promocode">The new Promocode values</param>
        /// <returns>The newly updated Promocode</returns>
        public Promocode PutPromocode(string eventId, string promocodeId, Promocode promocode)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(promocodeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (promocode == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PutPromocode(this.AccessToken, this.APIKey, eventId, promocodeId, promocode);
        }

        /// <summary>
        /// Delete a promo code for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeletePromocode( string eventId, string promocodeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(promocodeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.DeletePromocode(this.AccessToken, this.APIKey, eventId, promocodeId);
        }




        /// <summary>
        /// Retrieve detailed information for a specific event registrant
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="registrantId">Redistrant id</param>
        /// <returns>Registrant details</returns>
        public Registrant GetRegistrant(string eventId, string registrantId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(registrantId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetRegistrant(this.AccessToken, this.APIKey, eventId, registrantId);
        }

         /// <summary>
        /// Retrieve a list of registrants for the specified event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>ResultSet containing a results array of Registrant</returns>
        public ResultSet<Registrant> GetAllRegistrants(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetAllRegistrants(this.AccessToken, this.APIKey, eventId);
        }




        /// <summary>
        /// Retrieve all existing items associated with an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of EventItem</returns>
        public List<EventItem> GetAllEventItems(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetAllEventItems(this.AccessToken, this.APIKey, eventId);
        }

        /// <summary>
        ///  Retrieve specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">Eventitem id</param>
        /// <returns>EventItem object</returns>
        public EventItem GetEventItem(string eventId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetEventItem(this.AccessToken, this.APIKey, eventId, itemId);
        }

        /// <summary>
        ///  Update a specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="eventItem">The newly values for EventItem</param>
        /// <returns>The updated EventItem</returns>
        public EventItem PutEventItem(string eventId, string itemId, EventItem eventItem)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (eventItem == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PutEventItem(this.AccessToken, this.APIKey, eventId, itemId, eventItem);
        }

        
        /// <summary>
        ///  Create a specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="eventItem">EventItem id</param>
        /// <returns>The newly created EventItem</returns>
        public EventItem PostEventItem(string eventId, EventItem eventItem)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (eventItem == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PostEventItem(this.AccessToken, this.APIKey, eventId, eventItem);
        }

        /// <summary>
        /// Delete a specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventItem(string eventId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.DeleteEventItem(this.AccessToken, this.APIKey, eventId, itemId);
        }




        /// <summary>
        /// Create an attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attribute">The Attribute object</param>
        /// <returns>The newly created attribure</returns>
        public CTCT.Components.EventSpot.Attribute PostEventItemAttribute(string eventId, string itemId, CTCT.Components.EventSpot.Attribute attribute)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (attribute == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PostEventItemAttribute(this.AccessToken, this.APIKey, eventId, itemId, attribute);
        }

        /// <summary>
        /// Updates an existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <param name="attribute">Attribute new values</param>
        /// <returns>The newly updated attribute</returns>
        public CTCT.Components.EventSpot.Attribute PutEventItemAttribute( string eventId, string itemId, string attributeId,  CTCT.Components.EventSpot.Attribute attribute)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(attributeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (attribute == null)
            {
                throw new IllegalArgumentException(Config.Errors.ObjectNull);
            }
            return EventSpotService.PutEventItemAttribute(this.AccessToken, this.APIKey, eventId, itemId, attributeId, attribute);
        }

         /// <summary>
        /// Retrieve an existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>Attribute object</returns>
        public CTCT.Components.EventSpot.Attribute GetEventItemAttribute(string eventId, string itemId, string attributeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(attributeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetEventItemAttribute(this.AccessToken, this.APIKey, eventId, itemId, attributeId);
        }

         /// <summary>
        /// Retrieve all existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>A list of Attributes</returns>
        public List<CTCT.Components.EventSpot.Attribute> GetAllEventItemAttributes(string eventId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.GetAllEventItemAttributes(this.AccessToken, this.APIKey, eventId, itemId);
        }

        /// <summary>
        /// Delete an existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventItemAttribute(string eventId, string itemId, string attributeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(attributeId))
            {
                throw new IllegalArgumentException(Config.Errors.InvalidId);
            }
            return EventSpotService.DeleteEventItemAttribute(this.AccessToken, this.APIKey, eventId, itemId, attributeId);
        }

        #endregion

        #endregion
    }
}
