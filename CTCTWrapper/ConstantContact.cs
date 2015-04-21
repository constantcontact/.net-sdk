

//using System;
//using System.Collections.Generic;
//using CTCT.Components.Contacts;
//using CTCT.Exceptions;
//using CTCT.Services;
//using CTCT.Util;
//using CTCT.Components.Activities;
//using CTCT.Components.EmailCampaigns;
//using CTCT.Components;
//using CTCT.Components.Tracking;
//using System.Configuration;
//using System.IO;
//using System.Text;
//using CTCT.Components.MyLibrary;
//using CTCT.Components.EventSpot;
//using CTCT.Components.AccountService;



//namespace CTCT
//{
//    /// <summary>
//    /// Main class meant to be used by users to access Constant Contact API functionality.
//    /// <example>
//    /// ASPX page:
//    /// <code>
//    /// <![CDATA[
//    ///  <%@Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
//    ///         CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default"%>
//    ///  ...
//    ///     <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
//    ///     <asp:Button ID="btnJoin" runat="server" Text="Join" onclick="btnJoin_Click" />
//    ///     <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
//    ///  ...
//    /// ]]>
//    /// </code>
//    /// </example>
//    /// <example>
//    /// Code behind:
//    /// <code>
//    /// <![CDATA[
//    /// public partial class _Default : System.Web.UI.Page
//    /// {
//    ///    protected void Page_Load(object sender, EventArgs e)
//    ///    {
//    ///         ...
//    ///    }
//    ///
//    ///    protected void btnJoin_Click(object sender, EventArgs e)
//    ///    {
//    ///        try
//    ///        {
//    ///            Contact contact = new Contact();
//    ///            // Don't care about the id value
//    ///            contact.Id = 1;
//    ///            contact.EmailAddresses.Add(new EmailAddress() {
//    ///                 EmailAddr = tbxEmail.Text,
//    ///                 ConfirmStatus = ConfirmStatus.NoConfirmationRequired,
//    ///                 Status = Status.Active });
//    ///            contact.Lists.Add(new ContactList() {
//    ///                 Id = 1,
//    ///                 Status = Status.Active });
//    ///
//    ///            ConstantContact cc = new ConstantContact();
//    ///            cc.AddContact(contact);
//    ///            lblMessage.Text = "You have been added to my mailing list!";
//    ///        }
//    ///        catch (Exception ex) { lblMessage.Text = ex.ToString(); }
//    ///    }
//    /// }
//    /// ]]>
//    /// </code>
//    /// </example>
//    /// <example>
//    /// Web.config entries:
//    /// <code>
//    /// <![CDATA[
//    /// ...
//    /// <appSettings>
//    ///     <add key="APIKey" value="APIKey"/>
//    ///     <add key="Password" value="password"/>
//    ///     <add key="Username" value="username"/>
//    ///     <add key="RedirectURL" value="http://somedomain"/>
//    /// </appSettings>
//    /// ...
//    /// ]]>
//    /// </code>
//    /// </example>
//    /// </summary>
//    public class ConstantContact
//    {


//        /// <summary>
//        /// User service context
//        /// </summary>
//        private IUserServiceContext UserServiceContext { get; set; }

//        /// <summary>
//        /// Gets or sets the AccessToken
//        /// </summary>
//        private string AccessToken { get; set; }

//        /// <summary>
//        /// Gets or sets the api_key
//        /// </summary>
//        private string APIKey { get; set; }





//        /// <summary>
//        /// Gets or sets the Contact service.
//        /// </summary>
//        protected virtual IContactService ContactService { get; set; }

//        /// <summary>
//        /// Gets or sets the List service.
//        /// </summary>
//        protected virtual IListService ListService { get; set; }

//        /// <summary>
//        /// Gets or sets the Activity service.
//        /// </summary>
//        protected virtual IActivityService ActivityService { get; set; }

//        /// <summary>
//        /// Gets or sets the Campaign Schedule service.
//        /// </summary>
//        protected virtual ICampaignScheduleService CampaignScheduleService { get; set; }

//        /// <summary>
//        /// Gets or sets the Campaign Tracking service.
//        /// </summary>
//        protected virtual ICampaignTrackingService CampaignTrackingService { get; set; }

//        /// <summary>
//        /// Gets or sets the Contact Tracking service.
//        /// </summary>
//        protected virtual IContactTrackingService ContactTrackingService { get; set; }

//        /// <summary>
//        /// Gets or sets the Email Campaign service.
//        /// </summary>
//        protected virtual IEmailCampaignService EmailCampaignService { get; set; }

//        /// <summary>
//        /// Gets or sets the Account service
//        /// </summary>
//        protected virtual IAccountService AccountService { get; set; }

//        /// <summary>
//        /// Gets or sets the MyLibrary service
//        /// </summary>
//        protected virtual IMyLibraryService MyLibraryService { get; set; }

//        /// <summary>
//        /// Gets of sets the EventSpot service
//        /// </summary>
//        protected virtual IEventSpotService EventSpotService { get; set; }





//        /// <summary>
//        /// Creates a new ConstantContact object using provided apiKey and accessToken parameters
//        /// </summary>
//        /// <param name="apiKey">APIKey</param>
//        /// <param name="accessToken">access token</param>
//        public ConstantContact(string apiKey, string accessToken)
//        {
//            this.InitializeFields();

//            this.AccessToken = accessToken;
//            this.APIKey = apiKey;

//            this.UserServiceContext = new UserServiceContext(accessToken, apiKey);
//        }





//        private void InitializeFields()
//        {
//            this.ContactService = new ContactService(UserServiceContext);
//            this.ListService = new ListService(UserServiceContext);
//            this.ActivityService = new ActivityService(UserServiceContext);
//            this.CampaignScheduleService = new CampaignScheduleService(UserServiceContext);
//            this.CampaignTrackingService = new CampaignTrackingService(UserServiceContext);
//            this.ContactTrackingService = new ContactTrackingService(UserServiceContext);
//            this.EmailCampaignService = new EmailCampaignService(UserServiceContext);
//            this.AccountService = new AccountService(UserServiceContext);
//            this.MyLibraryService = new MyLibraryService(UserServiceContext);
//            this.EventSpotService = new EventSpotService(UserServiceContext);
//        }







//        /// <summary>
//        /// Get a set of contacts.
//        /// </summary>
//        /// <param name="email">Match the exact email address.</param>
//        /// <param name="limit">Limit the number of returned values, default 500.</param>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <param name="status">Match the exact contact status</param>
//        /// <returns>Returns a list of contacts.</returns>
//        public ResultSet<Contact> GetContacts(string email, int? limit, DateTime? modifiedSince, ContactStatus? status)
//        {
//            return ContactService.GetContacts(email, limit, modifiedSince, status);
//        }

//        /// <summary>
//        /// Get an array of contacts.
//        /// </summary>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>Returns a list of contacts.</returns>
//        public ResultSet<Contact> GetContacts(DateTime? modifiedSince, Pagination pag)
//        {
//            return ContactService.GetContacts(modifiedSince, pag);
//        }

//        /// <summary>
//        /// Get a set of contacts.
//        /// </summary>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <returns>Returns a list of contacts.</returns>
//        public ResultSet<Contact> GetContacts(DateTime? modifiedSince)
//        {
//            return ContactService.GetContacts(modifiedSince, null);
//        }

//        /// <summary>
//        /// Get an individual contact.
//        /// </summary>
//        /// <param name="contactId">Id of the contact to retrieve</param>
//        /// <returns>Returns a contact.</returns>
//        public Contact GetContact(string contactId)
//        {
//            return ContactService.GetContact(contactId);
//        }

//        /// <summary>
//        /// Add a new contact to an account.
//        /// </summary>
//        /// <param name="contact">Contact to add.</param>
//        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
//        /// <returns>Returns the newly created contact.</returns>
//        public Contact AddContact(Contact contact, bool actionByVisitor)
//        {
//            return ContactService.AddContact(contact, actionByVisitor);
//        }

//        /// <summary>
//        /// Sets an individual contact to 'Unsubscribed' status.
//        /// </summary>
//        /// <param name="contact">Contact object.</param>
//        /// <returns>Returns true if operation succeeded.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteContact(Contact contact)
//        {
           

//            return this.DeleteContact(contact.Id);
//        }

//        /// <summary>
//        /// Sets an individual contact to 'Unsubscribed' status.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <returns>Returns true if operation succeeded.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteContact(string contactId)
//        {
//            return ContactService.DeleteContact(contactId);
//        }

//        /// <summary>
//        /// Delete a contact from all contact lists.
//        /// </summary>
//        /// <param name="contact">Contact object.</param>
//        /// <returns>Returns true if operation succeeded.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteContactFromLists(Contact contact)
//        {


//            return this.DeleteContactFromLists(contact.Id);
//        }

//        /// <summary>
//        /// Delete a contact from all contact lists. Sets them to 'Removed' status.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <returns>Returns true if operation succeeded.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteContactFromLists(string contactId)
//        {
//            return ContactService.DeleteContactFromLists(contactId);
//        }

//        /// <summary>
//        /// Delete a contact from all contact lists.
//        /// </summary>
//        /// <param name="contact">Contact object.</param>
//        /// <param name="list">List object.</param>
//        /// <returns>Returns true if operation succeeded.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteContactFromList(Contact contact, ContactList list)
//        {


//            return this.DeleteContactFromList(contact.Id, list.Id);
//        }

//        /// <summary>
//        /// Delete a contact from all contact lists.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="listId">List id.</param>
//        /// <returns>Returns true if operation succeeded.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteContactFromList(string contactId, string listId)
//        {
//            return ContactService.DeleteContactFromList(contactId, listId);
//        }

//        /// <summary>
//        /// Update an individual contact.
//        /// </summary>
//        /// <param name="contact">Contact to update.</param>
//        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
//        /// <returns>Returns the updated contact.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Contact UpdateContact(Contact contact, bool actionByVisitor)
//        {
//            return ContactService.UpdateContact(contact, actionByVisitor);
//        }





//        /// <summary>
//        /// Get lists.
//        /// </summary>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <returns>Returns the list of lists where contact belong to.</returns>
//        public IList<ContactList> GetLists(DateTime? modifiedSince)
//        {
//            return ListService.GetLists(modifiedSince);
//        }

//        /// <summary>
//        /// Get an individual list.
//        /// </summary>
//        /// <param name="listId">Id of the list to retrieve</param>
//        /// <returns>Returns contact list.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ContactList GetList(string listId)
//        {
//            return ListService.GetList(listId);
//        }

//        /// <summary>
//        /// Update a Contact List.
//        /// </summary>
//        /// <param name="list">ContactList to be updated</param>
//        /// <returns>Contact list</returns>
//        public ContactList UpdateList(ContactList list)
//        {
//            return ListService.UpdateList(list);
//        }

//        /// <summary>
//        /// Add a new list to an account.
//        /// </summary>
//        /// <param name="list">List to add.</param>
//        /// <returns>Returns the newly created list.</returns>
//        public ContactList AddList(ContactList list)
//        {
//            return ListService.AddList(list);
//        }

//        /// <summary>
//        /// Delete a Contact List.
//        /// </summary>
//        /// <param name="listId">List id.</param>
//        /// <returns>return true if list was deleted successfully, false otherwise</returns>
//        public bool DeleteList(string listId)
//        {
//            return ListService.DeleteList(listId);
//        }

//        /// <summary>
//        /// Get contact that belong to a specific list.
//        /// </summary>
//        /// <param name="list">Contact list object.</param>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <returns>Returns the list of contacts.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<Contact> GetContactsFromList(ContactList list, DateTime? modifiedSince)
//        {
//            return this.GetContactsFromList(list.Id, null, modifiedSince, null);
//        }

//        /// <summary>
//        /// Get contact that belong to a specific list.
//        /// </summary>
//        /// <param name="list">Contact list object.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <returns>Returns the list of contacts.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<Contact> GetContactsFromList(ContactList list, int? limit, DateTime? modifiedSince)
//        {
//            return this.GetContactsFromList(list.Id, limit, modifiedSince, null);
//        }

//        /// <summary>
//        /// Get contact that belong to a specific list.
//        /// </summary>
//        /// <param name="listId">Contact list id.</param>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <returns>Returns a list of contacts.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<Contact> GetContactsFromList(string listId, DateTime? modifiedSince)
//        {
//            return this.GetContactsFromList(listId, null, modifiedSince, null);
//        }

//        /// <summary>
//        /// Get contact that belong to a specific list.
//        /// </summary>
//        /// <param name="listId">Contact list id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// <returns>Returns a list of contacts.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<Contact> GetContactsFromList(string listId, int? limit, DateTime? modifiedSince)
//        {
//            return this.GetContactsFromList(listId, limit, modifiedSince, null);
//        }

//        /// <summary>
//        /// Get contact that belong to a specific list.
//        /// </summary>
//        /// <param name="listId">Contact list id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
//        /// /// <param name="pag">Pagination object.</param>
//        /// <returns>Returns a list of contacts.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<Contact> GetContactsFromList(string listId, int? limit, DateTime? modifiedSince, Pagination pag)
//        {
//            return ListService.GetContactsFromList(listId, limit, modifiedSince, pag);
//        }





//        /// <summary>
//        /// Get a list of activities.
//        /// </summary>
//        /// <returns>Returns the list of activities.</returns>
//        public IList<Activity> GetActivities()
//        {
//            return ActivityService.GetActivities();
//        }

//        /// <summary>
//        /// Get an activity.
//        /// </summary>
//        /// <param name="activityId">The activity identification.</param>
//        /// <returns>Returns the activity identified by its id.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Activity GetActivity(string activityId)
//        {
//            return ActivityService.GetActivity(activityId);
//        }

//        /// <summary>
//        /// Create an Add Contacts Activity.
//        /// </summary>
//        /// <param name="addContacts">AddContacts object.</param>
//        /// <returns>Returns an Activity object.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Activity CreateAddContactsActivity(AddContacts addContacts)
//        {
//            return ActivityService.CreateAddContactsActivity(addContacts);
//        }

//        /// <summary>
//        /// Create a Clear Lists Activity.
//        /// </summary>
//        /// <param name="lists">Array of list id's to be cleared.</param>
//        /// <returns>Returns an Activity object.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Activity AddClearListsActivity(IList<string> lists)
//        {
//            return ActivityService.AddClearListsActivity(lists);
//        }

//        /// <summary>
//        /// Create an Export Contacts Activity.
//        /// </summary>
//        /// <param name="exportContacts">Export contacts object.</param>
//        /// <returns>Returns an Activity object.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Activity AddExportContactsActivity(ExportContacts exportContacts)
//        {
//            return ActivityService.AddExportContactsActivity(exportContacts);
//        }

//        /// <summary>
//        /// Create a Remove Contacts From Lists Activity.
//        /// </summary>
//        /// <param name="emailAddresses">List of email addresses.</param>
//        /// <param name="lists">List of id's.</param>
//        /// <returns>Returns an Activity object.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Activity AddRemoveContactsFromListsActivity(IList<string> emailAddresses, IList<string> lists)
//        {
//            return ActivityService.AddRemoveContactsFromListsActivity(emailAddresses, lists);
//        }

//        /// <summary>
//        /// Create an Add Contacts Multipart Activity
//        /// </summary>
//        /// <param name="fileName">The name of the file</param>
//        /// <param name="fileContent">The contents of the file</param>
//        /// <param name="lists">List of contact list Ids to add the contacts to</param>
//        /// <returns>Returns an Activity object.</returns>
//        public Activity AddContactsMultipartActivity(string fileName, byte[] fileContent, IList<string> lists)
//        {
//            return ActivityService.AddContactstMultipartActivity(fileName, fileContent, lists);
//        }

//        /// <summary>
//        /// Create a Remove Contacts Multipart Activity
//        /// </summary>
//        /// <param name="fileName">The name of the file</param>
//        /// <param name="fileContent">The contents of the file</param>
//        /// <param name="lists">List of contact list Ids to add to remove the contacts to</param>
//        /// <returns>Returns an Activity object.</returns>
//        public Activity RemoveContactsMultipartActivity(string fileName, byte[] fileContent, IList<string> lists)
//        {
//            return ActivityService.RemoveContactsMultipartActivity(fileName, fileContent, lists);
//        }





//        /// <summary>
//        /// Create a new schedule for a campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id to be scheduled.</param>
//        /// <param name="schedule">Schedule to be created.</param>
//        /// <returns>Returns the scheduled object.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Schedule AddSchedule(string campaignId, Schedule schedule)
//        {
//            return CampaignScheduleService.AddSchedule(campaignId, schedule);
//        }

//        /// <summary>
//        /// Get a list of schedules for a campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id to be scheduled.</param>
//        /// <returns>Returns the list of schedules for specified campaign.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public IList<Schedule> GetSchedules(string campaignId)
//        {
//            return CampaignScheduleService.GetSchedules(campaignId);
//        }

//        /// <summary>
//        /// Get a specific schedule for a campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id to be get a schedule for.</param>
//        /// <param name="scheduleId">Schedule id to retrieve.</param>
//        /// <returns>Returns the schedule object for the requested campaign.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Schedule GetSchedule(string campaignId, string scheduleId)
//        {
//            return CampaignScheduleService.GetSchedule(campaignId, scheduleId);
//        }

//        /// <summary>
//        /// Update a specific schedule for a campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id to be scheduled.</param>
//        /// <param name="schedule">Schedule to retrieve.</param>
//        /// <returns>Returns the updated schedule object.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public Schedule UpdateSchedule(string campaignId, Schedule schedule)
//        {
//            return CampaignScheduleService.AddSchedule(campaignId, schedule);
//        }

//        /// <summary>
//        /// Get a specific schedule for a campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="scheduleId">Schedule id to delete.</param>
//        /// <returns>Returns true if schedule was successfully deleted.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteSchedule(string campaignId, string scheduleId)
//        {
//            return CampaignScheduleService.DeleteSchedule(campaignId, scheduleId);
//        }

//        /// <summary>
//        /// Send a test send of a campaign.
//        /// </summary>
//        /// <param name="campaignId">Id of campaign to send test of.</param>
//        /// <param name="testSend">Test send details.</param>
//        /// <returns>Returns the sent object.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public TestSend SendTest(string campaignId, TestSend testSend)
//        {
//            return CampaignScheduleService.SendTest(campaignId, testSend);
//        }





//        /// <summary>
//        /// Get a result set of bounces for a given campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
//        public ResultSet<BounceActivity> GetCampaignTrackingBounces(string campaignId, int? limit, DateTime? createdSince)
//        {
//            return CampaignTrackingService.GetBounces(campaignId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get a result set of bounces for a given campaign.
//        /// </summary>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link BounceActivity.</returns>
//        public ResultSet<BounceActivity> GetCampaignTrackingBounces(Pagination pag)
//        {
//            return CampaignTrackingService.GetBounces(pag);
//        }

//        /// <summary>
//        /// Get clicks for a given campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
//        public ResultSet<ClickActivity> GetCampaignTrackingClicks(string campaignId, int? limit, DateTime? createdSince)
//        {
//            return CampaignTrackingService.GetClicks(campaignId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get clicks for a specific link in a campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="linkId">Specifies the link in the email campaign to retrieve click data for.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
//        public ResultSet<ClickActivity> GetCampaignTrackingClicks(string campaignId, string linkId, int? limit, DateTime? createdSince)
//        {
//            return CampaignTrackingService.GetClicks(campaignId, linkId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get clicks for a specific link in a campaign.
//        /// </summary>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
//        public ResultSet<ClickActivity> GetCampaignTrackingClicks(Pagination pag)
//        {
//            return CampaignTrackingService.GetClicks(pag);
//        }

//        /// <summary>
//        /// Get clicks for a given campaign.
//        /// </summary>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link ClickActivity.</returns>
//        public ResultSet<ClickActivity> GetClicks(Pagination pag)
//        {
//            return CampaignTrackingService.GetClicks(pag);
//        }

//        /// <summary>
//        /// Get forwards for a given campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
//        public ResultSet<ForwardActivity> GetCampaignTrackingForwards(string campaignId, int? limit, DateTime? createdSince)
//        {
//            return CampaignTrackingService.GetForwards(campaignId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get forwards for a given campaign.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
//        public ResultSet<ForwardActivity> GetCampaignTrackingForwards(DateTime? createdSince, Pagination pag)
//        {
//            return CampaignTrackingService.GetForwards(createdSince, pag);
//        }

//        /// <summary>
//        /// Get opens for a given campaign.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
//        public ResultSet<OpenActivity> GetCampaignTrackingOpens(string campaignId, int? limit, DateTime? createdSince)
//        {
//            return CampaignTrackingService.GetOpens(campaignId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get opens for a given campaign.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
//        public ResultSet<OpenActivity> GetCampaignTrackingOpens(DateTime? createdSince, Pagination pag)
//        {
//            return CampaignTrackingService.GetOpens(createdSince, pag);
//        }

//        /// <summary>
//        /// Get sends for a given campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
//        public ResultSet<SendActivity> GetCampaignTrackingSends(string campaignId, int? limit, DateTime? createdSince)
//        {
//            return CampaignTrackingService.GetSends(campaignId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get sends for a given campaign.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link SendActivity</returns>
//        public ResultSet<SendActivity> GetCampaignTrackingSends(DateTime? createdSince, Pagination pag)
//        {
//            return CampaignTrackingService.GetSends(createdSince, pag);
//        }

//        /// <summary>
//        /// Get opt outs for a given campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
//        public ResultSet<OptOutActivity> GetCampaignTrackingOptOuts(string campaignId, int? limit, DateTime? createdSince)
//        {
//            return CampaignTrackingService.GetOptOuts(campaignId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get opt outs for a given campaign.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
//        public ResultSet<OptOutActivity> GetCampaignTrackingOptOuts(DateTime? createdSince, Pagination pag)
//        {
//            return CampaignTrackingService.GetOptOuts(createdSince, pag);
//        }

//        /// <summary>
//        /// Get a summary of reporting data for a given campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <returns>Tracking summary.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public TrackingSummary GetCampaignTrackingSummary(string campaignId)
//        {
//            return CampaignTrackingService.GetSummary(campaignId);
//        }





//        /// <summary>
//        /// Get all activities for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<ContactActivity> GetContactTrackingActivities(string contactId)
//        {
//            return this.GetContactTrackingActivities(contactId, null, null);
//        }

//        /// <summary>
//        /// Get all activities for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<ContactActivity> GetContactTrackingActivities(string contactId, int? limit, DateTime? createdSince)
//        {
//            return ContactTrackingService.GetActivities(contactId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get all activities for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">Filter for activities created since the supplied date in the collection</param>	 
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link ContactActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<ContactActivity> GetContactTrackingActivities(string contactId, int? limit, DateTime? createdSince, Pagination pag)
//        {
//            return ContactTrackingService.GetActivities(contactId, limit, createdSince, pag);
//        }

//        /// <summary>
//        /// Get activities by email campaign for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <returns>ResultSet containing a results array of @link TrackingSummary.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<TrackingSummary> GetContactTrackingEmailCampaignActivities(string contactId)
//        {
//            return ContactTrackingService.GetEmailCampaignActivities(contactId);
//        }

//        /// <summary>
//        /// Get bounces for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<BounceActivity> GetContactTrackingBounces(string contactId)
//        {
//            return this.GetContactTrackingBounces(contactId, null);
//        }

//        /// <summary>
//        /// Get bounces for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<BounceActivity> GetContactTrackingBounces(string contactId, int? limit)
//        {
//            return ContactTrackingService.GetBounces(contactId, limit);
//        }

//        /// <summary>
//        /// Get bounces for a given contact.
//        /// </summary>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link BounceActivity</returns>
//        public ResultSet<BounceActivity> GetContactTrackingBounces(Pagination pag)
//        {
//            return ContactTrackingService.GetBounces(pag);
//        }

//        /// <summary>
//        /// Get clicks for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<ClickActivity> GetContactTrackingClicks(string contactId, DateTime? createdSince)
//        {
//            return this.GetContactTrackingClicks(contactId, null, createdSince);
//        }

//        /// <summary>
//        /// Get clicks for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<ClickActivity> GetContactTrackingClicks(string contactId, int? limit, DateTime? createdSince)
//        {
//            return ContactTrackingService.GetClicks(contactId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get clicks for a given contact.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link ClickActivity</returns>
//        public ResultSet<ClickActivity> GetContactTrackingClicks(DateTime? createdSince, Pagination pag)
//        {
//            return ContactTrackingService.GetClicks(createdSince, pag);
//        }

//        /// <summary>
//        /// Get forwards for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<ForwardActivity> GetContactTrackingForwards(string contactId, DateTime? createdSince)
//        {
//            return this.GetContactTrackingForwards(contactId, null, createdSince);
//        }

//        /// <summary>
//        /// Get forwards for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<ForwardActivity> GetContactTrackingForwards(string contactId, int? limit, DateTime? createdSince)
//        {
//            return ContactTrackingService.GetForwards(contactId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get forwards for a given contact.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link ForwardActivity.</returns>
//        public ResultSet<ForwardActivity> GetContactTrackingForwards(DateTime? createdSince, Pagination pag)
//        {
//            return ContactTrackingService.GetForwards(createdSince, pag);
//        }

//        /// <summary>
//        /// Get opens for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<OpenActivity> GetContactTrackingOpens(string contactId, DateTime? createdSince)
//        {
//            return this.GetContactTrackingOpens(contactId, null, createdSince);
//        }

//        /// <summary>
//        /// Get opens for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<OpenActivity> GetContactTrackingOpens(string contactId, int? limit, DateTime? createdSince)
//        {
//            return ContactTrackingService.GetOpens(contactId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get opens for a given contact.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link OpenActivity.</returns>
//        public ResultSet<OpenActivity> GetContactTrackingOpens(DateTime? createdSince, Pagination pag)
//        {
//            return ContactTrackingService.GetOpens(createdSince, pag);
//        }

//        /// <summary>
//        /// Get sends for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<SendActivity> GetContactTrackingSends(string contactId, DateTime? createdSince)
//        {
//            return this.GetContactTrackingSends(contactId, null, createdSince);
//        }

//        /// <summary>
//        /// Get sends for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<SendActivity> GetContactTrackingSends(string contactId, int? limit, DateTime? createdSince)
//        {
//            return ContactTrackingService.GetSends(contactId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get sends for a given contact.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link SendActivity.</returns>
//        public ResultSet<SendActivity> GetContactTrackingSends(DateTime? createdSince, Pagination pag)
//        {
//            return ContactTrackingService.GetSends(createdSince, pag);
//        }

//        /// <summary>
//        /// Get opt outs for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<OptOutActivity> GetContactTrackingOptOuts(string contactId, DateTime? createdSince)
//        {
//            return this.GetContactTrackingOptOuts(contactId, null, createdSince);
//        }

//        /// <summary>
//        /// Get opt outs for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public ResultSet<OptOutActivity> GetContactTrackingOptOuts(string contactId, int? limit, DateTime? createdSince)
//        {
//            return ContactTrackingService.GetOptOuts(contactId, limit, createdSince);
//        }

//        /// <summary>
//        /// Get opt outs for a given contact.
//        /// </summary>
//        /// <param name="createdSince">filter for activities created since the supplied date in the collection</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>ResultSet containing a results array of @link OptOutActivity.</returns>
//        public ResultSet<OptOutActivity> GetContactTrackingOptOuts(DateTime? createdSince, Pagination pag)
//        {
//            return ContactTrackingService.GetOptOuts(createdSince, pag);
//        }

//        /// <summary>
//        /// Get a summary of reporting data for a given contact.
//        /// </summary>
//        /// <param name="contactId">Contact id.</param>
//        /// <returns>Tracking summary.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public TrackingSummary GetContactTrackingSummary(string contactId)
//        {
//            return ContactTrackingService.GetSummary(contactId);
//        }





//        /// <summary>
//        /// Get a set of campaigns.
//        /// </summary>
//        /// <param name="pagination">Select the next page of campaigns from a pagination</param>
//        /// <returns>Returns a list of campaigns.</returns>
//        public ResultSet<EmailCampaign> GetCampaigns(Pagination pagination)
//        {
//            return this.GetCampaigns(null, null, null, pagination);
//        }

//        /// <summary>
//        /// Get a set of campaigns.
//        /// </summary>
//        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
//        /// <returns>Returns a list of campaigns.</returns>
//        public ResultSet<EmailCampaign> GetCampaigns(DateTime? modifiedSince)
//        {
//            return this.GetCampaigns(null, null, modifiedSince, null);
//        }

//        /// <summary>
//        /// Get a set of campaigns.
//        /// </summary>
//        /// <param name="status">Returns list of email campaigns with specified status.</param>
//        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
//        /// <returns>Returns a list of campaigns.</returns>
//        public ResultSet<EmailCampaign> GetCampaigns(CampaignStatus status, DateTime? modifiedSince)
//        {
//            return this.GetCampaigns(status, null, modifiedSince, null);
//        }

//        /// <summary>
//        /// Get a set of campaigns.
//        /// </summary>
//        /// <param name="status">Returns list of email campaigns with specified status.</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
//        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
//        /// <param name="pagination">Pagination object supplied by a previous call to GetCampaigns when another page is present</param>
//        /// <returns>Returns a list of campaigns.</returns>
//        public ResultSet<EmailCampaign> GetCampaigns(CampaignStatus? status, int? limit, DateTime? modifiedSince, Pagination pagination)
//        {
//            return EmailCampaignService.GetCampaigns(status, limit, modifiedSince, pagination);
//        }

//        /// <summary>
//        /// Get campaign details for a specific campaign.
//        /// </summary>
//        /// <param name="campaignId">Campaign id.</param>
//        /// <returns>Returns a campaign.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public EmailCampaign GetCampaign(string campaignId)
//        {
//            return EmailCampaignService.GetCampaign(campaignId);
//        }

//        /// <summary>
//        /// Create a new campaign.
//        /// </summary>
//        /// <param name="campaign">Campign to be created</param>
//        /// <returns>Returns a campaign.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public EmailCampaign AddCampaign(EmailCampaign campaign)
//        {
//            return EmailCampaignService.AddCampaign(campaign);
//        }

//        /// <summary>
//        /// Delete an email campaign.
//        /// </summary>
//        /// <param name="campaignId">Valid campaign id.</param>
//        /// <returns>Returns true if successful.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public bool DeleteCampaign(string campaignId)
//        {
//            return EmailCampaignService.DeleteCampaign(campaignId);
//        }

//        /// <summary>
//        /// Update a specific email campaign.
//        /// </summary>
//        /// <param name="campaign">Campaign to be updated.</param>
//        /// <returns>Returns a campaign.</returns>
//        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
//        public EmailCampaign UpdateCampaign(EmailCampaign campaign)
//        {
//            return EmailCampaignService.UpdateCampaign(campaign);
//        }





//        /// <summary>
//        /// Retrieve a list of all the account owner's email addresses
//        /// </summary>
//        /// <returns>list of all verified account owner's email addresses</returns>
//        public IList<VerifiedEmailAddress> GetVerifiedEmailAddress()
//        {
//            return AccountService.GetVerifiedEmailAddress();
//        }

//        /// <summary>
//        /// Get account summary information
//        /// </summary>
//        /// <returns>An AccountSummaryInformation object</returns>
//        public AccountSummaryInformation GetAccountSummaryInformation()
//        { 
//            return AccountService.GetAccountSummaryInformation();
//        }

//        /// <summary>
//        /// Updates account summary information
//        /// </summary>
//        /// <param name="accountSumaryInfo">An AccountSummaryInformation object</param>
//        /// <returns>An AccountSummaryInformation object</returns>
//        public AccountSummaryInformation PutAccountSummaryInformation(AccountSummaryInformation accountSumaryInfo)
//        {
//            return AccountService.PutAccountSummaryInformation(accountSumaryInfo);
//        }





//        /// <summary>
//        /// Get MyLibrary usage information
//        /// </summary>
//        /// <returns>Returns a MyLibraryInfo object</returns>
//        public MyLibraryInfo GetLibraryInfo()
//        {
//            return MyLibraryService.GetLibraryInfo();
//        }

//        /// <summary>
//        /// Get all existing MyLibrary folders
//        /// </summary>
//        /// <returns>Returns a collection of MyLibraryFolder objects.</returns>
//        public ResultSet<MyLibraryFolder> GetLibraryFolders()
//        {
//            return this.GetLibraryFolders(null, null, null);
//        }

//        /// <summary>
//        /// Get all existing MyLibrary folders
//        /// </summary>
//        /// <param name="sortBy">Specifies how the list of folders is sorted</param>
//        /// <returns>Returns a collection of MyLibraryFolder objects.</returns>
//        public ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy)
//        {
//            return this.GetLibraryFolders(sortBy, null, null);
//        }

//        /// <summary>
//        /// Get all existing MyLibrary folders
//        /// </summary>
//        /// <param name="sortBy">Specifies how the list of folders is sorted</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <returns>Returns a collection of MyLibraryFolder objects.</returns>
//        public ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy, int? limit)
//        {
//            return this.GetLibraryFolders(sortBy, limit, null);
//        }

//        /// <summary>
//        /// Get all existing MyLibrary folders
//        /// </summary>
//        /// <param name="sortBy">Specifies how the list of folders is sorted</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>Returns a collection of MyLibraryFolder objects.</returns>
//        public ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy, int? limit, Pagination pag)
//        {
//            return MyLibraryService.GetLibraryFolders(sortBy, limit, pag);
//        }

//        /// <summary>
//        /// Add new folder to MyLibrary
//        /// </summary>
//        /// <param name="folder">Folder to be added (with name and parent id)</param>
//        /// <returns>Returns a MyLibraryFolder object.</returns>
//        public MyLibraryFolder AddLibraryFolder(MyLibraryFolder folder)
//        {
//            return MyLibraryService.AddLibraryFolder(folder);
//        }

//        /// <summary>
//        /// Get a folder by Id
//        /// </summary>
//        /// <param name="folderId">The id of the folder</param>
//        /// <returns>Returns a MyLibraryFolder object.</returns>
//        public MyLibraryFolder GetLibraryFolder(string folderId)
//        {
//            return MyLibraryService.GetLibraryFolder(folderId);
//        }

//        /// <summary>
//        /// Update name and parent_id for a specific folder
//        /// </summary>
//        /// <param name="folder">Folder to be added (with name and parent id)</param>
//        /// <returns>Returns a MyLibraryFolder object.</returns>
//        public MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder)
//        {
//            return this.UpdateLibraryFolder(folder, null);
//        }

//        /// <summary>
//        /// Update name and parent_id for a specific folder
//        /// </summary>
//        /// <param name="folder">Folder to be added (with name and parent id)</param>
//        /// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
//        /// <returns>Returns a MyLibraryFolder object.</returns>
//        public MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder, bool? includePayload)
//        {
//            return MyLibraryService.UpdateLibraryFolder(folder, includePayload);
//        }

//        /// <summary>
//        /// Delete a specific folder
//        /// </summary>
//        /// <param name="folder">The folder to be deleted</param>
//         /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
//        public bool DeleteLibraryFolder(MyLibraryFolder folder)
//        {
			

//            return DeleteLibraryFolder(folder);
//        }

//        /// <summary>
//        /// Delete a specific folder
//        /// </summary>
//        /// <param name="folderId">The id of the folder</param>
//         /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
//        public bool DeleteLibraryFolder(string folderId)
//        {
//            return MyLibraryService.DeleteLibraryFolder(folderId);
//        }

//        /// <summary>
//        /// Get files from Trash folder
//        /// </summary>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryTrashFiles()
//        {
//            return this.GetLibraryTrashFiles(null, null, null, null);
//        }

//        /// <summary>
//        /// Get files from Trash folder
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type)
//        {
//            return this.GetLibraryTrashFiles(type, null, null, null);
//        }

//        /// <summary>
//        /// Get files from Trash folder
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <param name="sortBy">Specifies how the list of folders is sorted</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy)
//        {
//            return this.GetLibraryTrashFiles(type, sortBy, null, null);
//        }

//        /// <summary>
//        /// Get files from Trash folder
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <param name="sortBy">Specifies how the list of folders is sorted</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy, int? limit)
//        {
//            return this.GetLibraryTrashFiles(type, sortBy, limit, null);
//        }

//        /// <summary>
//        /// Get files from Trash folder
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <param name="sortBy">Specifies how the list of folders is sorted</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy, int? limit, Pagination pag)
//        {
//            return MyLibraryService.GetLibraryTrashFiles(type, sortBy, limit, pag);
//        }

//        /// <summary>
//        /// Delete files in Trash folder
//        /// </summary>
//         /// <returns>Returns true if files were deleted successfully, false otherwise</returns>
//        public bool DeleteLibraryTrashFiles()
//        {
//            return MyLibraryService.DeleteLibraryTrashFiles();
//        }

//        /// <summary>
//        /// Get files
//        /// </summary>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFiles()
//        {
//            return this.GetLibraryFiles(null, null, null, null);
//        }

//        /// <summary>
//        /// Get files
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type)
//        {
//            return this.GetLibraryFiles(type, null, null, null);
//        }

//        /// <summary>
//        /// Get files
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <param name="source">Specifies to retrieve files from a particular source</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source)
//        {
//            return this.GetLibraryFiles(type, source, null, null);
//        }

//        /// <summary>
//        /// Get files
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <param name="source">Specifies to retrieve files from a particular source</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source, int? limit)
//        {
//            return this.GetLibraryFiles(type, source, limit, null);
//        }

//        /// <summary>
//        /// Get files
//        /// </summary>
//        /// <param name="type">The type of the files to retrieve</param>
//        /// <param name="source">Specifies to retrieve files from a particular source</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source, int? limit, Pagination pag)
//        {
//            return MyLibraryService.GetLibraryFiles(type, source, limit, pag);
//        }

//        /// <summary>
//        /// Get files from a specific folder
//        /// </summary>
//        /// <param name="folderId">The id of the folder from which to retrieve files</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId)
//        {
//            return this.GetLibraryFilesByFolder(folderId, null, null);
//        }

//        /// <summary>
//        /// Get files from a specific folder
//        /// </summary>
//        /// <param name="folderId">The id of the folder from which to retrieve files</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId, int? limit)
//        {
//            return this.GetLibraryFilesByFolder(folderId, limit, null);
//        }

//        /// <summary>
//        /// Get files from a specific folder
//        /// </summary>
//        /// <param name="folderId">The id of the folder from which to retrieve files</param>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
//        /// <param name="pag">Pagination object.</param>
//        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
//        public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId, int? limit, Pagination pag)
//        {
//            return MyLibraryService.GetLibraryFilesByFolder(folderId, limit, pag);
//        }

//        /// <summary>
//        /// Get file after id
//        /// </summary>
//        /// <param name="fileId">The id of the file</param>
//        /// <returns>Returns a MyLibraryFile object.</returns>
//        public MyLibraryFile GetLibraryFile(string fileId)
//        {
//            return MyLibraryService.GetLibraryFile(fileId);
//        }

//        /// <summary>
//        /// Update a specific file
//        /// </summary>
//        /// <param name="file">File to be updated</param>
//        /// <returns>Returns a MyLibraryFile object.</returns>
//        public MyLibraryFile UpdateLibraryFile(MyLibraryFile file)
//        {
//            return this.UpdateLibraryFile(file, null);
//        }

//        /// <summary>
//        /// Update a specific file
//        /// </summary>
//        /// <param name="file">File to be updated</param>
//        /// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
//        /// <returns>Returns a MyLibraryFile object.</returns>
//        public MyLibraryFile UpdateLibraryFile(MyLibraryFile file, bool? includePayload)
//        {		
//            return MyLibraryService.UpdateLibraryFile(file, includePayload);
//        }

//        /// <summary>
//        /// Delete a specific file
//        /// </summary>
//        /// <param name="file">The file to be deleted</param>
//        /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
//        public bool DeleteLibraryFile(MyLibraryFile file)
//        {
			

//            return this.DeleteLibraryFile(file.Id);
//        }

//        /// <summary>
//        /// Delete a specific file
//        /// </summary>
//        /// <param name="fileId">The id of the file</param>
//        /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
//        public bool DeleteLibraryFile(string fileId)
//        {
//            return MyLibraryService.DeleteLibraryFile(fileId);
//        }

//        /// <summary>
//        /// Get status for an upload file
//        /// </summary>
//        /// <param name="fileId">The id of the file</param>
//        /// <returns>Returns a list of FileUploadStatus objects</returns>
//        public IList<FileUploadStatus> GetLibraryFileUploadStatus(string fileId)
//        {
//            return MyLibraryService.GetLibraryFileUploadStatus(fileId);
//        }

//        /// <summary>
//        /// Move files to a different folder
//        /// </summary>
//        /// <param name="folderId">The id of the folder</param>
//        /// <param name="fileIds">List of comma separated file ids</param>
//        /// <returns>Returns a list of FileMoveResult objects.</returns>
//        public IList<FileMoveResult> MoveLibraryFile(string folderId, IList<string> fileIds)
//        {
//            return MyLibraryService.MoveLibraryFile(folderId, fileIds);
//        }

//        /// <summary>
//        /// Add files using the multipart content-type
//        /// </summary>
//        /// <param name="fileName">The file name and extension</param>
//        /// <param name="fileType">The file type</param>
//        /// <param name="folderId">The id of the folder</param>
//        /// <param name="description">The description of the file</param>
//        /// <param name="source">The source of the original file</param>
//        /// <param name="data">The data contained in the file being uploaded</param>
//        /// <returns>Returns the file Id associated with the uploaded file</returns>
//        public string AddLibraryFilesMultipart(string fileName, FileType fileType, string folderId, string description, FileSource source, byte[] data)
//        {
//            return MyLibraryService.AddLibraryFilesMultipart(fileName, fileType, folderId, description, source, data);
//        }





//        /// <summary>
//        /// View all existing events
//        /// </summary>
//        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 50</param>
//        /// <param name="pag">Pagination object</param>
//        /// <returns>ResultSet containing a results array of IndividualEvents</returns>
//        public ResultSet<IndividualEvent> GetAllEventSpots(int? limit, Pagination pag)
//        {
//            return EventSpotService.GetAllEventSpots(limit, pag);
//        }


//         /// <summary>
//        /// Retrieve an event specified by the event_id
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <returns>The event</returns>
//        public IndividualEvent GetEventSpot(string eventId)
//        {
//            return EventSpotService.GetEventSpot(eventId);
//        }

//           /// <summary>
//        /// Publish an event
//        /// </summary>
//        /// <param name="eventSpot">The event to publish</param>
//        /// <returns>The published event</returns>
//        public IndividualEvent PostEventSpot(IndividualEvent eventSpot)
//        {
//            return EventSpotService.PostEventSpot(eventSpot);
//        }

//         /// <summary>
//        /// Update an event
//        /// </summary>
//        /// <param name="eventId">Event id to be updated</param>
//        /// <param name="eventSpot">The new values for event</param>
//        /// <returns>The updated event</returns>
//        public IndividualEvent PutEventSpot(string eventId, IndividualEvent eventSpot)
//        {
//            return EventSpotService.PutEventSpot(eventId, eventSpot);
//        }

//         /// <summary>
//        /// Publish or cancel an event by changing the status of the event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="eventStatus">New status of the event. ACTIVE" and "CANCELLED are allowed</param>
//        /// <returns>The updated event</returns>
//        public IndividualEvent PatchEventSpotStatus(string eventId, EventStatus eventStatus)
//        {
//            return EventSpotService.PatchEventSpotStatus(eventId, eventStatus);
//        }




//        /// <summary>
//        /// Retrieve all existing fees for an event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <returns>A list of event fees for the specified event</returns>
//        public List<EventFee> GetAllEventFees(string eventId)
//        {
//            return EventSpotService.GetAllEventFees(eventId);
//        }

//        /// <summary>
//        /// Retrieve an individual event fee
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="feeId">EventFee id</param>
//        /// <returns>An EventFee object</returns>
//        public EventFee GetEventFee(string eventId, string feeId)
//        {
//            return EventSpotService.GetEventFee(eventId, feeId);
//        }

//         /// <summary>
//        /// Update an individual event fee
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="feeId">EventFee id</param>
//        /// <param name="eventFee">The new values of EventFee</param>
//        /// <returns>The updated EventFee</returns>
//        public EventFee PutEventFee(string eventId, string feeId, EventFee eventFee)
//        {
//            return EventSpotService.PutEventFee(eventId, feeId, eventFee);
//        }


//        /// <summary>
//        ///  Delete an individual event fee
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="feeId">EventFee id</param>
//        /// <returns>True if successfuly deleted</returns>
//        public bool DeleteEventFee(string eventId, string feeId)
//        {
//            return EventSpotService.DeleteEventFee(eventId, feeId);
//        }

//        /// <summary>
//        /// Create an individual event fee
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="eventFee">EventFee object</param>
//        /// <returns>The newly created EventFee</returns>
//        public EventFee PostEventFee(string eventId, EventFee eventFee)
//        {
//            return EventSpotService.PostEventFee(eventId, eventFee);
//        }

//        /// <summary>
//        /// Retrieve all existing promo codes for an event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <returns>A list of Promocode</returns>
//        public List<Promocode> GetAllPromocodes(string eventId)
//        {
//            return EventSpotService.GetAllPromocodes(eventId);
//        }

//        /// <summary>
//        /// Retrieve an existing promo codes for an event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="promocodeId">Promocode id</param>
//        /// <returns>The Promocode object</returns>
//        public Promocode GetPromocode(string eventId, string promocodeId)
//        {
//            return EventSpotService.GetPromocode(eventId, promocodeId);
//        }

//         /// <summary>
//        /// Create a new promo code for an event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="promocode">Promocode object to be created</param>
//        /// <returns>The newly created Promocode</returns>
//        public Promocode PostPromocode(string eventId, Promocode promocode)
//        {
//            return EventSpotService.PostPromocode(eventId, promocode);
//        }

//        /// <summary>
//        /// Update a promo code for an event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="promocodeId">Promocode id</param>
//        /// <param name="promocode">The new Promocode values</param>
//        /// <returns>The newly updated Promocode</returns>
//        public Promocode PutPromocode(string eventId, string promocodeId, Promocode promocode)
//        {
//            return EventSpotService.PutPromocode(eventId, promocodeId, promocode);
//        }

//        /// <summary>
//        /// Delete a promo code for an event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="promocodeId">Promocode id</param>
//        /// <returns>True if successfuly deleted</returns>
//        public bool DeletePromocode( string eventId, string promocodeId)
//        {
//            return EventSpotService.DeletePromocode(eventId, promocodeId);
//        }




//        /// <summary>
//        /// Retrieve detailed information for a specific event registrant
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="registrantId">Redistrant id</param>
//        /// <returns>Registrant details</returns>
//        public Registrant GetRegistrant(string eventId, string registrantId)
//        {
//            return EventSpotService.GetRegistrant(eventId, registrantId);
//        }

//         /// <summary>
//        /// Retrieve a list of registrants for the specified event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <returns>ResultSet containing a results array of Registrant</returns>
//        public ResultSet<Registrant> GetAllRegistrants(string eventId)
//        {
//            return EventSpotService.GetAllRegistrants(eventId);
//        }




//        /// <summary>
//        /// Retrieve all existing items associated with an event
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <returns>A list of EventItem</returns>
//        public List<EventItem> GetAllEventItems(string eventId)
//        {
//            return EventSpotService.GetAllEventItems(eventId);
//        }

//        /// <summary>
//        ///  Retrieve specific event item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">Eventitem id</param>
//        /// <returns>EventItem object</returns>
//        public EventItem GetEventItem(string eventId, string itemId)
//        {
//            return EventSpotService.GetEventItem(eventId, itemId);
//        }

//        /// <summary>
//        ///  Update a specific event item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">EventItem id</param>
//        /// <param name="eventItem">The newly values for EventItem</param>
//        /// <returns>The updated EventItem</returns>
//        public EventItem PutEventItem(string eventId, string itemId, EventItem eventItem)
//        {
//            return EventSpotService.PutEventItem(eventId, itemId, eventItem);
//        }

        
//        /// <summary>
//        ///  Create a specific event item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="eventItem">EventItem id</param>
//        /// <returns>The newly created EventItem</returns>
//        public EventItem PostEventItem(string eventId, EventItem eventItem)
//        {
//            return EventSpotService.PostEventItem(eventId, eventItem);
//        }

//        /// <summary>
//        /// Delete a specific event item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">EventItem id</param>
//        /// <returns>True if successfuly deleted</returns>
//        public bool DeleteEventItem(string eventId, string itemId)
//        {
//            return EventSpotService.DeleteEventItem(eventId, itemId);
//        }




//        /// <summary>
//        /// Create an attributes for an item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">EventItem id</param>
//        /// <param name="attribute">The Attribute object</param>
//        /// <returns>The newly created attribure</returns>
//        public CTCT.Components.EventSpot.Attribute PostEventItemAttribute(string eventId, string itemId, CTCT.Components.EventSpot.Attribute attribute)
//        {
//            return EventSpotService.PostEventItemAttribute(eventId, itemId, attribute);
//        }

//        /// <summary>
//        /// Updates an existing attributes for an item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">EventItem id</param>
//        /// <param name="attributeId">Attribute id</param>
//        /// <param name="attribute">Attribute new values</param>
//        /// <returns>The newly updated attribute</returns>
//        public CTCT.Components.EventSpot.Attribute PutEventItemAttribute( string eventId, string itemId, string attributeId,  CTCT.Components.EventSpot.Attribute attribute)
//        {
//            return EventSpotService.PutEventItemAttribute(eventId, itemId, attributeId, attribute);
//        }

//         /// <summary>
//        /// Retrieve an existing attributes for an item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">EventItem id</param>
//        /// <param name="attributeId">Attribute id</param>
//        /// <returns>Attribute object</returns>
//        public CTCT.Components.EventSpot.Attribute GetEventItemAttribute(string eventId, string itemId, string attributeId)
//        {
//            return EventSpotService.GetEventItemAttribute(eventId, itemId, attributeId);
//        }

//         /// <summary>
//        /// Retrieve all existing attributes for an item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">EventItem id</param>
//        /// <returns>A list of Attributes</returns>
//        public List<CTCT.Components.EventSpot.Attribute> GetAllEventItemAttributes(string eventId, string itemId)
//        {
//            return EventSpotService.GetAllEventItemAttributes(eventId, itemId);
//        }

//        /// <summary>
//        /// Delete an existing attributes for an item
//        /// </summary>
//        /// <param name="eventId">Event id</param>
//        /// <param name="itemId">EventItem id</param>
//        /// <param name="attributeId">Attribute id</param>
//        /// <returns>True if successfuly deleted</returns>
//        public bool DeleteEventItemAttribute(string eventId, string itemId, string attributeId)
//        {
//            return EventSpotService.DeleteEventItemAttribute(eventId, itemId, attributeId);
//        }




//    }
//}
