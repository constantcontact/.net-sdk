#region Using

using System;
using System.Collections.Generic;
using CTCT.Auth;
using CTCT.Components.Contacts;
using CTCT.Exceptions;
using CTCT.Services;
using CTCT.Util;

#endregion

namespace CTCT
{
    /// <summary>
    /// Main class meant to be used by users to access Constant Contact API functionality.
    /// <example>
    /// ASPX page:
    /// <code>
    /// <![CDATA[
    ///  <%@Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ///         CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default"%>
    ///  ...
    ///     <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
    ///     <asp:Button ID="btnJoin" runat="server" Text="Join" onclick="btnJoin_Click" />
    ///     <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    ///  ...
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Code behind:
    /// <code>
    /// <![CDATA[
    /// public partial class _Default : System.Web.UI.Page
    /// {
    ///    protected void Page_Load(object sender, EventArgs e)
    ///    {
    ///         ...
    ///    }
    ///
    ///    protected void btnJoin_Click(object sender, EventArgs e)
    ///    {
    ///        try
    ///        {
    ///            Contact contact = new Contact();
    ///            // Don't care about the id value
    ///            contact.Id = 1;
    ///            contact.EmailAddresses.Add(new EmailAddress() {
    ///                 EmailAddr = tbxEmail.Text,
    ///                 ConfirmStatus = ConfirmStatus.NoConfirmationRequired,
    ///                 Status = Status.Active });
    ///            contact.Lists.Add(new ContactList() {
    ///                 Id = 1,
    ///                 Status = Status.Active });
    ///
    ///            ConstantContact cc = new ConstantContact();
    ///            cc.AddContact(contact);
    ///            lblMessage.Text = "You have been added to my mailing list!";
    ///        }
    ///        catch (Exception ex) { lblMessage.Text = ex.ToString(); }
    ///    }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Web.config entries:
    /// <code>
    /// <![CDATA[
    /// ...
    /// <appSettings>
    ///     <add key="APIKey" value="APIKey"/>
    ///     <add key="ConsumerSecret" value="ConsumerSecret"/>
    ///     <add key="Password" value="password"/>
    ///     <add key="Username" value="username"/>
    ///     <add key="RedirectURL" value="http://somedomain"/>
    /// </appSettings>
    /// ...
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    public class ConstantContact
    {
        #region Fields

        /// <summary>
        /// Access token.
        /// </summary>
        private string _accessToken;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the authorization class.
        /// </summary>
        protected virtual ICtctOAuth2 OAuth { get; set; }
        /// <summary>
        /// Gets or sets the Contact service.
        /// </summary>
        protected virtual IContactService ContactService { get; set; }
        /// <summary>
        /// Gets or sets the List service.
        /// </summary>
        protected virtual IListService ListService { get; set; }
        
        /// <summary>
        /// Returns the access token.
        /// </summary>
        private string AccessToken
        {
            get
            {
                if (String.IsNullOrEmpty(_accessToken))
                {
                    _accessToken = this.OAuth.GetAccessToken();
                }
                return _accessToken;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Class constructor.
        /// </summary>
        public ConstantContact()
        {
            this.OAuth = new CtctOAuth2();
            this.ContactService = new ContactService();
            this.ListService = new ListService();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get a set of contacts.
        /// </summary>
        /// <param name="offset">Denotes the starting number for the result set.</param>
        /// <param name="limit">Denotes the number of results per set, limited to 50.</param>
        /// <returns>Returns a list of contacts.</returns>
        public IList<Contact> GetContacts(int? offset, int? limit)
        {
            return ContactService.GetContacts(this.AccessToken, offset, limit);
        }

        /// <summary>
        /// Get a set of contacts.
        /// </summary>
        /// <returns>Returns a list of contacts.</returns>
        public IList<Contact> GetContacts()
        {
            return ContactService.GetContacts(this.AccessToken, null, null);
        }

        /// <summary>
        /// Get an individual contact.
        /// </summary>
        /// <param name="contactId">Id of the contact to retrieve</param>
        /// <returns>Returns a contact.</returns>
        public Contact GetContact(int contactId)
        {
            return ContactService.GetContact(this.AccessToken, contactId);
        }

        /// <summary>
        /// Get contacts with a specified email address.
        /// </summary>
        /// <param name="email">Contact email address to search for.</param>
        /// <returns>Returns a list of contacts.</returns>
        public IList<Contact> GetContactByEmail(string email)
        {
            return ContactService.GetContactByEmail(this.AccessToken, email);
        }

        /// <summary>
        /// Add a new contact to an account.
        /// </summary>
        /// <param name="contact">Contact to add.</param>
        /// <returns>Returns the newly created contact.</returns>
        public Contact AddContact(Contact contact)
        {
            return ContactService.AddContact(this.AccessToken, contact);
        }

        /// <summary>
        /// Sets an individual contact to 'REMOVED' status.
        /// </summary>
        /// <param name="contact">Contact object.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContact(Contact contact)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }
            return DeleteContact(contact.Id);
        }

        /// <summary>
        /// Sets an individual contact to 'REMOVED' status.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContact(int contactId)
        {
            if (contactId < 1)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return ContactService.DeleteContact(this.AccessToken, contactId);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromLists(int contactId)
        {
            if (contactId < 1)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }
            return ContactService.DeleteContactFromLists(this.AccessToken, contactId);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contact">Contact object.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromLists(Contact contact)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return ContactService.DeleteContactFromLists(this.AccessToken, contact.Id);
        }


        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contact">Contact object.</param>
        /// <param name="list">List object.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromList(Contact contact, ContactList list)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }
            if (list == null)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ContactService.DeleteContactFromList(this.AccessToken, contact.Id, list.Id);
        }

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <param name="listId">List id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public bool DeleteContactFromList(int contactId, int listId)
        {
            if (contactId < 1)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }
            if (listId < 1)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ContactService.DeleteContactFromList(this.AccessToken, contactId, listId);
        }

        /// <summary>
        /// Update an individual contact.
        /// </summary>
        /// <param name="contact">Contact to update.</param>
        /// <returns>Returns the updated contact.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public Contact UpdateContact(Contact contact)
        {
            if (contact == null)
            {
                throw new IllegalArgumentException(Config.Errors.ContactOrId);
            }

            return ContactService.UpdateContact(this.AccessToken, contact);
        }

        /// <summary>
        /// Get lists.
        /// </summary>
        /// <returns>Returns the list of lists where contact belong to.</returns>
        public IList<ContactList> GetLists()
        {
            return ListService.GetLists(this.AccessToken);
        }

        /// <summary>
        /// Get an individual list.
        /// </summary>
        /// <param name="listId">Id of the list to retrieve</param>
        /// <returns>Returns contact list.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public ContactList GetList(int listId)
        {
            if (listId < 1)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.GetList(this.AccessToken, listId);
        }

        /// <summary>
        /// Add a new list to an account.
        /// </summary>
        /// <param name="list">List to add.</param>
        /// <returns>Returns the newly created list.</returns>
        public ContactList AddList(ContactList list)
        {
            return ListService.AddList(this.AccessToken, list);
        }

        /// <summary>
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="list">Contact list object.</param>
        /// <returns>Returns the list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public IList<Contact> GetContactsFromList(ContactList list)
        {
            if (list == null)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.GetContactsFromList(this.AccessToken, list.Id);
        }

        /// <summary>
        /// Get contact that belong to a specific list.
        /// </summary>
        /// <param name="listId">Contact list id.</param>
        /// <returns>Returns a list of contacts.</returns>
        /// <exception cref="IllegalArgumentException">IllegalArgumentException</exception>
        public IList<Contact> GetContactsFromList(int listId)
        {
            if (listId < 1)
            {
                throw new IllegalArgumentException(Config.Errors.ListOrId);
            }

            return ListService.GetContactsFromList(this.AccessToken, listId);
        }

        #endregion
    }
}
