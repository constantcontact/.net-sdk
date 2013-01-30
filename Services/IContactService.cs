using System;
using CTCT.Components.Contacts;
using System.Collections.Generic;
namespace CTCT.Services
{
    /// <summary>
    /// Interface for ContactService class.
    /// </summary>
    public interface IContactService : IBaseService
    {
        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Limit.</param>
        /// <returns>Returns a list of contacts.</returns>
        IList<Contact> GetContacts(string accessToken, int? offset, int? limit);

        /// <summary>
        /// Get contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns a contact.</returns>
        Contact GetContact(string accessToken, int contactId);

        /// <summary>
        /// Get contacts with a specified email address.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="email">Contact email address to search for.</param>
        /// <returns>Returns a list of contacts.</returns>
        IList<Contact> GetContactByEmail(string accessToken, string email);

        /// <summary>
        /// Add a new contact to the Constant Contact account
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contact">Contact to add.</param>
        /// <returns>Returns the newly created contact.</returns>
        Contact AddContact(string accessToken, Contact contact);

        /// <summary>
        /// Delete contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        bool DeleteContact(string accessToken, int contactId);

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contactId">Contact id to be removed from lists.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        bool DeleteContactFromLists(string accessToken, int contactId);
        
            /// <summary>
        /// Delete a contact from a specific contact list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="contactId">Contact id to be removed</param>
        /// <param name="listId">ContactList to remove the contact from</param>
        /// <returns>Returns true if operation succeeded.</returns>
        bool DeleteContactFromList(string accessToken, int contactId, int listId);
        
        /// <summary>
        /// Update contact details for a specific contact.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="contact">Contact to be updated.</param>
        /// <returns>Returns the updated contact.</returns>
        Contact UpdateContact(string accessToken, Contact contact);
    }
}
