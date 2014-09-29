using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Contacts;
using CTCT.Util;
using CTCT.Components;
using System.Collections.Specialized;
using System.Collections;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to the Lists Collection
    /// </summary>
    public class ListService : BaseService, IListService
    {
        /// <summary>
        /// Get lists within an account.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contact lists.</returns>
        public IList<ContactList> GetLists(string accessToken, string apiKey, DateTime? modifiedSince)
        {
            IList<ContactList> lists = new List<ContactList>();
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Lists, GetQueryParameters(new object[] { "modified_since", Extensions.ToISO8601String(modifiedSince) }));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                lists = Component.FromJSON<IList<ContactList>>(response.Body);
            }
            
            return lists;
        }

        /// <summary>
        /// Add a new list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="list">Contact list.</param>
        /// <returns>Returns the newly created list.</returns>
        public ContactList AddList(string accessToken, string apiKey, ContactList list)
        {
            ContactList newList = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Lists);
            string json = list.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                newList = Component.FromJSON<ContactList>(response.Body);
            }

            return newList;
        }

        /// <summary>
        /// Get an individual contact list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="listId">List id.</param>
        /// <returns>Returns a contact list.</returns>
        public ContactList GetList(string accessToken, string apiKey, string listId)
        {
            ContactList list = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.List, listId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                list = Component.FromJSON<ContactList>(response.Body);
            }
            
            return list;
        }

        /// <summary>
        /// Update a Contact List.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="list">ContactList to be updated</param>
        /// <returns>Contact list</returns>
        public ContactList UpdateList(string accessToken, string apiKey, ContactList list)
        {
            ContactList updList = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.List, list.Id));
            string json = list.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                updList = Component.FromJSON<ContactList>(response.Body);
            }

            return updList;
        }

        /// <summary>
        /// Delete a Contact List.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="listId">List id.</param>
        /// <returns>return true if list was deleted successfully, false otherwise</returns>
        public bool DeleteList(string accessToken, string apiKey, string listId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.List, listId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

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
        public ResultSet<Contact> GetContactsFromList(string accessToken, string apiKey, string listId, int? limit, DateTime? modifiedSince, Pagination pag)
        {
            var contacts = new ResultSet<Contact>();
            string url = (pag == null) ? Config.ConstructUrl(Config.Endpoints.ListContacts, new object[] { listId }, new object[] { "limit", limit, "modified_since", Extensions.ToISO8601String(modifiedSince) }) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                contacts = response.Get<ResultSet<Contact>>();
            }

            return contacts;
        }
    }
}
