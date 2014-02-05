using System;
using System.Collections.Generic;
using CTCT.Components;
using CTCT.Components.EventSpot;
namespace CTCT.Services
{
    /// <summary>
    /// Interface for EventSpot class
    /// </summary>
    public interface IEventSpotService
    {
        /// <summary>
        /// View all existing events
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 50</param>
        /// <param name="pag">Pagination object</param>
        /// <returns>ResultSet containing a results array of IndividualEvents</returns>
        ResultSet<IndividualEvent> GetAllEventSpots(string accessToken, string apiKey, int? limit, Pagination pag);

        /// <summary>
        /// Retrieve an event specified by the event_id
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>The event</returns>
        IndividualEvent GetEventSpot(string accessToken, string apiKey, string eventId);

        /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventSpot">The event to publish</param>
        /// <returns>The published event</returns>
        IndividualEvent PostEventSpot(string accessToken, string apiKey, IndividualEvent eventSpot);

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id to be updated</param>
        /// <param name="eventSpot">The new values for event</param>
        /// <returns>The updated event</returns>
        IndividualEvent PutEventSpot(string accessToken, string apiKey, string eventId, IndividualEvent eventSpot);

        /// <summary>
        /// Publish or cancel an event by changing the status of the event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="eventStatus">New status of the event. ACTIVE" and "CANCELLED are allowed</param>
        /// <returns>The updated event</returns>
        IndividualEvent PatchEventSpotStatus(string accessToken, string apiKey, string eventId, EventStatus eventStatus);




        /// <summary>
        /// Retrieve all existing fees for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of event fees for the specified event</returns>
        List<EventFee> GetAllEventFees(string accessToken, string apiKey, string eventId);

        /// <summary>
        /// Retrieve an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>An EventFee object</returns>
        EventFee GetEventFee(string accessToken, string apiKey, string eventId, string feeId);

        /// <summary>
        /// Update an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <param name="eventFee">The new values of EventFee</param>
        /// <returns>The updated EventFee</returns>
        EventFee PutEventFee(string accessToken, string apiKey, string eventId, string feeId, EventFee eventFee);

        /// <summary>
        ///  Delete an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>True if successfuly deleted</returns>
        bool DeleteEventFee(string accessToken, string apiKey, string eventId, string feeId);

        /// <summary>
        /// Create an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="eventFee">EventFee object</param>
        /// <returns>The newly created EventFee</returns>
        EventFee PostEventFee(string accessToken, string apiKey, string eventId, EventFee eventFee);






        /// <summary>
        /// Retrieve all existing promo codes for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of Promocode</returns>
        List<Promocode> GetAllPromocodes(string accessToken, string apiKey, string eventId);

        /// <summary>
        /// Retrieve an existing promo codes for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>The Promocode object</returns>
        Promocode GetPromocode(string accessToken, string apiKey, string eventId, string promocodeId);

        /// <summary>
        /// Create a new promo code for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocode">Promocode object to be created</param>
        /// <returns>The newly created Promocode</returns>
        Promocode PostPromocode(string accessToken, string apiKey, string eventId, Promocode promocode);

        /// <summary>
        /// Update a promo code for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <param name="promocode">The new Promocode values</param>
        /// <returns>The newly updated Promocode</returns>
        Promocode PutPromocode(string accessToken, string apiKey, string eventId, string promocodeId, Promocode promocode);

        /// <summary>
        /// Delete a promo code for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>True if successfuly deleted</returns>
        bool DeletePromocode(string accessToken, string apiKey, string eventId, string promocodeId);






        /// <summary>
        /// Retrieve detailed information for a specific event registrant
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="registrantId">Redistrant id</param>
        /// <returns>Registrant details</returns>
        Registrant GetRegistrant(string accessToken, string apiKey, string eventId, string registrantId);

        /// <summary>
        /// Retrieve a list of registrants for the specified event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>ResultSet containing a results array of Registrant</returns>
        ResultSet<Registrant> GetAllRegistrants(string accessToken, string apiKey, string eventId);







        /// <summary>
        /// Retrieve all existing items associated with an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of EventItem</returns>
        List<EventItem> GetAllEventItems(string accessToken, string apiKey, string eventId);

        /// <summary>
        ///  Retrieve specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">Eventitem id</param>
        /// <returns>EventItem object</returns>
        EventItem GetEventItem(string accessToken, string apiKey, string eventId, string itemId);


        /// <summary>
        ///  Update a specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="eventItem">The newly values for EventItem</param>
        /// <returns>The updated EventItem</returns>
        EventItem PutEventItem(string accessToken, string apiKey, string eventId, string itemId, EventItem eventItem);

        /// <summary>
        ///  Create a specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="eventItem">EventItem id</param>
        /// <returns>The newly created EventItem</returns>
        EventItem PostEventItem(string accessToken, string apiKey, string eventId, EventItem eventItem);

        /// <summary>
        /// Delete a specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>True if successfuly deleted</returns>
        bool DeleteEventItem(string accessToken, string apiKey, string eventId, string itemId);







        /// <summary>
        /// Create an attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attribute">The Attribute object</param>
        /// <returns>The newly created attribure</returns>
        CTCT.Components.EventSpot.Attribute PostEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, CTCT.Components.EventSpot.Attribute attribute);

        /// <summary>
        /// Updates an existing attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <param name="attribute">Attribute new values</param>
        /// <returns>The newly updated attribute</returns>
        CTCT.Components.EventSpot.Attribute PutEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId, CTCT.Components.EventSpot.Attribute attribute);

        /// <summary>
        /// Retrieve an existing attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>Attribute object</returns>
        CTCT.Components.EventSpot.Attribute GetEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId);

        /// <summary>
        /// Retrieve all existing attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>A list of Attributes</returns>
        List<CTCT.Components.EventSpot.Attribute> GetAllEventItemAttributes(string accessToken, string apiKey, string eventId, string itemId);

        /// <summary>
        /// Delete an existing attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>True if successfuly deleted</returns>
        bool DeleteEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId);  
    }
}
