using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Contacts;
using CTCT.Util;
using CTCT.Components;

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
        /// <returns>Returns a list of contact lists.</returns>
        public IList<ContactList> GetLists(string accessToken)
        {
            IList<ContactList> lists = new List<ContactList>();
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Lists);
            CUrlResponse response = RestClient.Get(url, accessToken);
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
        /// <param name="list">Contact list.</param>
        /// <returns>Returns the newly created list.</returns>
        public ContactList AddList(string accessToken, ContactList list)
        {
            ContactList newList = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Lists);
            string json = list.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, json);
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
        /// <param name="listId">List id.</param>
        /// <returns>Returns a contact list.</returns>
        public ContactList GetList(string accessToken, int listId)
        {
            ContactList list = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.List, listId));
            CUrlResponse response = RestClient.Get(url, accessToken);
            if (response.HasData)
            {
                list = Component.FromJSON<ContactList>(response.Body);
            }
            
            return list;
        }


        /// <summary>
        /// Get all contacts from an individual list.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="listId">List id to retrieve contacts for.</param>
        /// <returns>Returns a list of contacts.</returns>
        public IList<Contact> GetContactsFromList(string accessToken, int listId)
        {
            IList<Contact> contacts = new List<Contact>();
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.ListContacts, listId));
            CUrlResponse response = RestClient.Get(url, accessToken);
            if (response.HasData)
            {
                contacts = Component.FromJSON<IList<Contact>>(response.Body);
            }

            return contacts;
        }
    }
}
