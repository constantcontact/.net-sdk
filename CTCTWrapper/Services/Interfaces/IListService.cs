using System;
using System.Collections.Generic;
using CTCT.Components.Contacts;
using CTCT.Components;
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
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contact lists.</returns>
        IList<ContactList> GetLists(string accessToken, string apiKey, DateTime? modifiedSince);

        /// <summary>
        /// Add a new list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="list">Contact list.</param>
        /// <returns>Returns the newly created list.</returns>
        ContactList AddList(string accessToken, string apiKey, ContactList list);

        /// <summary>
        /// Get an individual contact list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="listId">List id.</param>
        /// <returns>Returns a contact list.</returns>
        ContactList GetList(string accessToken, string apiKey, string listId);

        /// <summary>
        /// Update a Contact List.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="list">ContactList to be updated</param>
        /// <returns>Contact list</returns>
        ContactList UpdateList(string accessToken, string apiKey, ContactList list);

        /// <summary>
        /// Delete a Contact List.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="listId">List id.</param>
        /// <returns>return true if list was deleted successfully, false otherwise</returns>
        bool DeleteList(string accessToken, string apiKey, string listId);

		 /// <summary>
        /// Get all contacts from an individual list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="listId">List id to retrieve contacts for.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
		/// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
		ResultSet<Contact> GetContactsFromList(string accessToken, string apiKey, string listId, int? limit, DateTime? modifiedSince, Pagination pag);
    }
}
