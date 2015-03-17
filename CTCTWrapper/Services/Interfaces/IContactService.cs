﻿using System;
using CTCT.Components.Contacts;
using CTCT.Components;
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
        /// <param name="email">Match the exact email address.</param>
        /// <param name="limit">Limit the number of returned values.</param>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
		/// <param name="status">Returns list of contacts with specified status.</param>
        /// <returns>Returns a list of contacts.</returns>
        ResultSet<Contact> GetContacts(string email, int? limit, DateTime? modifiedSince, ContactStatus? status);

        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
        /// <param name="pag">Pagination object.</param>
        /// <returns>Returns a list of contacts.</returns>
        ResultSet<Contact> GetContacts(DateTime? modifiedSince, Pagination pag);

        /// <summary>
        /// Get contact details for a specific contact.
        /// </summary>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns a contact.</returns>
        Contact GetContact(string contactId);

        /// <summary>
        /// Get an array of contacts.
        /// </summary>
        /// <param name="modifiedSince">limit contact to contacts modified since the supplied date</param>
        /// <returns>Returns a list of contacts.</returns>
        ResultSet<Contact> GetContacts(DateTime? modifiedSince);

        /// <summary>
        /// Add a new contact to the Constant Contact account
        /// </summary>
        /// <param name="contact">Contact to add.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the newly created contact.</returns>
        Contact AddContact(Contact contact, bool actionByVisitor);

        /// <summary>
        /// Delete contact details for a specific contact.
        /// </summary>
        /// <param name="contactId">Unique contact id.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        bool DeleteContact(string contactId);

        /// <summary>
        /// Unsubscribe a specific contact.
        /// </summary>
        /// <param name="contact">The Contact</param>
        /// <returns>Returns true if operation succeeded.</returns>
        bool DeleteContact(Contact contact);

        /// <summary>
        /// Delete a contact from all contact lists.
        /// </summary>
        /// <param name="contactId">Contact id to be removed from lists.</param>
        /// <returns>Returns true if operation succeeded.</returns>
        bool DeleteContactFromLists(string contactId);
        
        /// <summary>
        /// Delete a contact from a specific contact list.
        /// </summary>
        /// <param name="contactId">Contact id to be removed</param>
        /// <param name="listId">ContactList to remove the contact from</param>
        /// <returns>Returns true if operation succeeded.</returns>
        bool DeleteContactFromList(string contactId, string listId);
        
        /// <summary>
        /// Update contact details for a specific contact.
        /// </summary>
        /// <param name="contact">Contact to be updated.</param>
        /// <param name="actionByVisitor">Set to true if action by visitor.</param>
        /// <returns>Returns the updated contact.</returns>
        Contact UpdateContact(Contact contact, bool actionByVisitor);
    }
}
