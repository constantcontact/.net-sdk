using System;
using CTCT.Components.Contacts;
using CTCT.Util;
using CTCT.Components;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to the Contacts Collection.
    /// </summary>
    public class ContactService : BaseService, IContactService
    {
        /// <summary>
        /// Contact service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public ContactService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="email">Match the exact email address.</param>
        /// <param name="limit">Limit the number of returned values.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
		/// <param name="status">Filter results by contact status</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(string email, int? limit, DateTime? modifiedSince, ContactStatus? status)
        {
            return GetContacts(email, limit, modifiedSince, status, null);
        }

        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(DateTime? modifiedSince, Pagination pag)
        {
            return GetContacts(null, null, modifiedSince, null, pag);
        }

        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(DateTime? modifiedSince)
        {
            return GetContacts(null, null, modifiedSince, null, null);
        }

		/// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="email">Match the exact email address.</param>
        /// <param name="limit">Limit the number of returned values.</param>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
		/// <param name="status">Match the exact contact status.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        private ResultSet<Contact> GetContacts(string email, int? limit, DateTime? modifiedSince, ContactStatus? status, Pagination pag)
        {
            // Construct access URL
            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.Contacts, null, new object[] { "email", email, "limit", limit, "modified_since", Extensions.ToISO8601String(modifiedSince), "status", status }) : pag.GetNextUrl();
            // Get REST response
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<Contact>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get contact details for a specific contact.
        /// </summary>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns a contact.</returns>
        public Contact GetContact(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Contact, contactId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var contact = response.Get<Contact>();
                return contact;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Add a new contact to the Constant Contact account
        /// </summary>
        /// <param name="contact">Contact to add.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the newly created contact.</returns>
        public Contact AddContact(Contact contact, bool actionByVisitor)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.Contacts, actionByVisitor ? String.Format("?action_by={0}", ActionBy.ActionByVisitor) : null);
            string json = contact.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var newContact = response.Get<Contact>();
                return newContact;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Unsubscribe a specific contact.
        /// </summary>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContact(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Contact, contactId));
            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Unsubscribe a specific contact.
        /// </summary>
        /// <param name="contact">The Contact</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContact(Contact contact)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }

            return DeleteContact(contact.Id);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contactId">Contact id to be removed from lists.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromLists(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.ContactLists, contactId));
            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contact">The Contact</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromLists(Contact contact)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }

            return DeleteContactFromLists(contact.Id);
        }

        /// <summary>
        /// Delete a contact from a specific contact list.
        /// </summary>
        /// <param name="contactId">Contact id to be removed</param>
        /// <param name="listId">ContactList to remove the contact from</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromList(string contactId, string listId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }
            if (string.IsNullOrEmpty(contactId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ListOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.ContactList, contactId, listId));
            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }   
        }

        /// <summary>
        /// Delete a contact from a specific contact list.
        /// </summary>
        /// <param name="contact">The Contact to be removed</param>
        /// <param name="list">The ContactList to remove the contact from</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromList(Contact contact, ContactList list)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }
            if (list == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ListOrId);
            }

            return DeleteContactFromList(contact.Id, list.Id);
        }

        /// <summary>
        /// Update contact details for a specific contact.
        /// </summary>
        /// <param name="contact">Contact to be updated.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the updated contact.</returns>
        public Contact UpdateContact(Contact contact, bool actionByVisitor)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ContactOrId);
            }

            if (contact.Id == null)
            {
                throw new CtctException(CTCT.Resources.Errors.UpdateId);
            }
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Contact, contact.Id), actionByVisitor ? String.Format("?action_by={0}", ActionBy.ActionByVisitor) : null);
            string json = contact.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var updateContact = response.Get<Contact>();
                return updateContact;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }  
        }
    }
}
