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
        /// List service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public ListService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Get lists within an account.
        /// </summary>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contact lists.</returns>
        public IList<ContactList> GetLists(DateTime? modifiedSince)
        {
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.Lists, GetQueryParameters(new object[] { "modified_since", Extensions.ToISO8601String(modifiedSince) }));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var lists = response.Get<IList<ContactList>>();
                return lists;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Add a new list.
        /// </summary>
        /// <param name="list">Contact list.</param>
        /// <returns>Returns the newly created list.</returns>
        public ContactList AddList(ContactList list)
        {
            if (list == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ListOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.Lists);
            string json = list.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var newList = response.Get<ContactList>();
                return newList;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get an individual contact list.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <returns>Returns a contact list.</returns>
        public ContactList GetList(string listId)
        {
            if (string.IsNullOrEmpty(listId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ListOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.List, listId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var list = response.Get<ContactList>();
                return list;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
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
                throw new IllegalArgumentException(CTCT.Resources.Errors.ListOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.List, list.Id));
            string json = list.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var updList = response.Get<ContactList>();
                return updList;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
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
                throw new IllegalArgumentException(CTCT.Resources.Errors.ListOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.List, listId));
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
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="list">Contact list object.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <returns>Returns the list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ResultSet<Contact> GetContactsFromList(ContactList list, DateTime? modifiedSince)
        {
            return GetContactsFromList(list.Id, null, modifiedSince, null);
        }

        /// <summary>
        /// Get all contacts from an individual list.
        /// </summary>
        /// <param name="listId">List id to retrieve contacts for.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit contacts retrieved to contacts modified since the supplied date</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        public ResultSet<Contact> GetContactsFromList(string listId, int? limit, DateTime? modifiedSince, Pagination pag)
        {
            if (string.IsNullOrEmpty(listId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ListOrId);
            }

            string url = (pag == null) ? ConstructUrl(Settings.Endpoints.Default.ListContacts, new object[] { listId }, new object[] { "limit", limit, "modified_since", Extensions.ToISO8601String(modifiedSince) }) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var contacts = response.Get<ResultSet<Contact>>();
                return contacts;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
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
            return GetContactsFromList(listId, null, modifiedSince, null);
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
            return GetContactsFromList(listId, limit, modifiedSince, null);
        }
    }
}
