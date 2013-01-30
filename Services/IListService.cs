using System;
using System.Collections.Generic;
using CTCT.Components.Contacts;
namespace CTCT.Services
{
    /// <summary>
    /// Interface for ListService class.
    /// </summary>
    public interface IListService
    {
        /// <summary>
        /// Get lists within an account.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <returns>Returns a list of contact lists.</returns>
        IList<ContactList> GetLists(string accessToken);

        /// <summary>
        /// Add a new list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="list">Contact list.</param>
        /// <returns>Returns the newly created list.</returns>
        ContactList AddList(string accessToken, ContactList list);

        /// <summary>
        /// Get an individual contact list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="listId">List id.</param>
        /// <returns>Returns a contact list.</returns>
        ContactList GetList(string accessToken, int listId);

        /// <summary>
        /// Get all contacts from an individual list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="listId">List id to retrieve contacts for.</param>
        /// <returns>Returns a list of contacts.</returns>
        IList<Contact> GetContactsFromList(string accessToken, int listId);
    }
}
