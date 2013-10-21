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
        /// Get an array of contacts.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="email">Match the exact email address.</param>
        /// <param name="limit">Limit the number of returned values.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
		/// <param name="status">Filter results by contact status</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(string accessToken, string apiKey, string email, int? limit, DateTime? modifiedSince, ContactStatus? status)
        {
            return GetContacts(accessToken, apiKey, email, limit, modifiedSince, status, null);
        }

        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContacts(string accessToken, string apiKey, DateTime? modifiedSince, Pagination pag)
        {
            return GetContacts(accessToken, apiKey, null, null, modifiedSince, null, pag);
        }

		/// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="email">Match the exact email address.</param>
        /// <param name="limit">Limit the number of returned values.</param>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
		/// <param name="status">Match the exact contact status.</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        private ResultSet<Contact> GetContacts(string accessToken, string apiKey, string email, int? limit, DateTime? modifiedSince, ContactStatus? status, Pagination pag)
        {
            ResultSet<Contact> results = null;
            // Construct access URL
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.Contacts, null, new object[] { "email", email, "limit", limit, "modified_since", Extensions.ToISO8601String(modifiedSince), "status", status }) : pag.GetNextUrl();
            // Get REST response
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                // Convert from JSON
                results = response.Get<ResultSet<Contact>>();
            }

            return results;
        }

        /// <summary>
        /// Get contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns a contact.</returns>
        public Contact GetContact(string accessToken, string apiKey, string contactId)
        {
            Contact contact = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Contact, contactId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                contact = Component.FromJSON<Contact>(response.Body);
            }

            return contact;
        }

        /// <summary>
        /// Add a new contact to the Constant Contact account
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contact">Contact to add.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the newly created contact.</returns>
        public Contact AddContact(string accessToken, string apiKey, Contact contact, bool actionByVisitor)
        {
            Contact newContact = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Contacts, actionByVisitor ? String.Format("?action_by={0}", ActionBy.ActionByVisitor) : null);
            string json = contact.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                newContact = Component.FromJSON<Contact>(response.Body);
            }
            else
                if (response.IsError)
                {
                    throw new CtctException(response.GetErrorMessage());
                }

            return newContact;
        }

        /// <summary>
        /// Unsubscribe a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContact(string accessToken, string apiKey, string contactId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Contact, contactId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id to be removed from lists.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromLists(string accessToken, string apiKey, string contactId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.ContactLists, contactId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a contact from a specific contact list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contactId">Contact id to be removed</param>
        /// <param name="listId">ContactList to remove the contact from</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromList(string accessToken, string apiKey, string contactId, string listId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.ContactList, contactId, listId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="contact">Contact to be updated.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the updated contact.</returns>
        public Contact UpdateContact(string accessToken, string apiKey, Contact contact, bool actionByVisitor)
        {
            Contact updateContact = null;
            if (contact.Id == null)
            {
                throw new CtctException(Config.Errors.UpdateId);
            }
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Contact, contact.Id), actionByVisitor ? String.Format("?action_by={0}", ActionBy.ActionByVisitor) : null);
            string json = contact.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                updateContact = Component.FromJSON<Contact>(response.Body);
            }
            else
                if (response.IsError) {
                    throw new CtctException(response.GetErrorMessage());
                }

            return updateContact;
        }
    }
}
