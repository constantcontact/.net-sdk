using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Contacts;
using CTCT.Util;
using System.Web.Script.Serialization;
using CTCT.Components;

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
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Limit.</param>
        /// <returns>Returns a list of contacts.</returns>
        public IList<Contact> GetContacts(string accessToken, int? offset, int? limit)
        {
            IList<Contact> contacts = new List<Contact>();
            // Construct access URL
            string url = PaginateUrl(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Contacts), offset, limit);
            // Get REST response
            CUrlResponse response = RestClient.Get(url, accessToken);
            if (response.HasData)
            {
                // Convert from JSON
                contacts = Component.FromJSON<IList<Contact>>(response.Body);
            }

            return contacts;
        }

        /// <summary>
        /// Get contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns a contact.</returns>
        public Contact GetContact(string accessToken, int contactId)
        {
            Contact contact = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Contact, contactId));
            CUrlResponse response = RestClient.Get(url, accessToken);
            if (response.HasData)
            {
                contact = Component.FromJSON<Contact>(response.Body);
            }

            return contact;
        }

        /// <summary>
        /// Get contacts with a specified email address.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="email">Contact email address to search for.</param>
        /// <returns>Returns a list of contacts.</returns>
        public IList<Contact> GetContactByEmail(string accessToken, string email)
        {
            IList<Contact> contacts = new List<Contact>();
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Contacts, "?email=", email);
            CUrlResponse response = RestClient.Get(url, accessToken);
            if (response.HasData)
            {
                contacts = Component.FromJSON<IList<Contact>>(response.Body);
            }

            return contacts;
        }

        /// <summary>
        /// Add a new contact to the Constant Contact account
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contact">Contact to add.</param>
        /// <returns>Returns the newly created contact.</returns>
        public Contact AddContact(string accessToken, Contact contact)
        {
            Contact newContact = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Contacts);
            string json = contact.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, json);
            if (response.HasData)
            {
                newContact = Component.FromJSON<Contact>(response.Body);
            }

            return newContact;
        }

        /// <summary>
        /// Delete contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContact(string accessToken, int contactId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Contact, contactId));
            CUrlResponse response = RestClient.Delete(url, accessToken);

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contactId">Contact id to be removed from lists.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromLists(string accessToken, int contactId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.ContactLists, contactId));
            CUrlResponse response = RestClient.Delete(url, accessToken);

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a contact from a specific contact list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="contactId">Contact id to be removed</param>
        /// <param name="listId">ContactList to remove the contact from</param>
        /// <returns>Returns true if operation succeeded.</returns>
        public bool DeleteContactFromList(string accessToken, int contactId, int listId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.ContactList, contactId, listId));
            CUrlResponse response = RestClient.Delete(url, accessToken);

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contact">Contact to be updated.</param>
        /// <returns>Returns the updated contact.</returns>
        public Contact UpdateContact(string accessToken, Contact contact)
        {
            Contact updateContact = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Contact, contact.Id));
            string json = contact.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, json);
            if (response.HasData)
            {
                updateContact = Component.FromJSON<Contact>(response.Body);
            }

            return updateContact;
        }
    }
}
