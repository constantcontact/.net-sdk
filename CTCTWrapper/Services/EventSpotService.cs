using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Util;
using CTCT.Components.EventSpot;
using System.Runtime.Serialization;
using CTCT.Components;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions for EventSpot
    /// </summary>
    public class EventSpotService : BaseService, IEventSpotService
    {
        #region EventSpot

        /// <summary>
        /// View all existing events
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 50</param>
        /// <param name="pag">Pagination object</param>
        /// <returns>ResultSet containing a results array of IndividualEvents</returns>
        public ResultSet<IndividualEvent> GetAllEventSpots(string accessToken, string apiKey, int? limit, Pagination pag)
        {
            string url = (pag == null) ? String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventSpots, GetQueryParameters(new object[] { "limit", limit })) : pag.GetNextUrl();

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<ResultSet<IndividualEvent>>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new ResultSet<IndividualEvent>();
        }

        /// <summary>
        /// Retrieve an event specified by the event_id
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>The event</returns>
        public IndividualEvent GetEventSpot(string accessToken, string apiKey, string eventId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventSpots , "/", eventId);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<IndividualEvent>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new IndividualEvent();
        }

        /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventSpot">The event to publish</param>
        /// <returns>The published event</returns>
        public IndividualEvent PostEventSpot(string accessToken, string apiKey, IndividualEvent eventSpot)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventSpots);
            string json = eventSpot.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<IndividualEvent>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new IndividualEvent();
        }

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id to be updated</param>
        /// <param name="eventSpot">The new values for event</param>
        /// <returns>The updated event</returns>
        public IndividualEvent PutEventSpot(string accessToken, string apiKey, string eventId, IndividualEvent eventSpot)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventSpots, "/", eventId);
            string json = eventSpot.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<IndividualEvent>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new IndividualEvent();
        }

        /// <summary>
        /// Publish or cancel an event by changing the status of the event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="eventStatus">New status of the event. ACTIVE" and "CANCELLED are allowed</param>
        /// <returns>The updated event</returns>
        public IndividualEvent PatchEventSpotStatus(string accessToken, string apiKey, string eventId, EventStatus eventStatus)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventSpots, "/", eventId);

            var patchRequests = new List<PatchRequest>();
            var patchRequest = new PatchRequest("REPLACE", "#/status", eventStatus.ToString());
            patchRequests.Add(patchRequest);

            string json = patchRequests.ToJSON();

            CUrlResponse response = RestClient.Patch(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<IndividualEvent>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new IndividualEvent();
        }

        #endregion

        #region EventFees

        /// <summary>
        /// Retrieve all existing fees for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of event fees for the specified event</returns>
        public List<EventFee> GetAllEventFees(string accessToken, string apiKey, string eventId)
        {
            string url =  String.Format( String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventFees), eventId, null);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<List<EventFee>>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new List<EventFee>();
        }

        /// <summary>
        /// Retrieve an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>An EventFee object</returns>
        public EventFee GetEventFee(string accessToken, string apiKey, string eventId, string feeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventFees), eventId, feeId);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<EventFee>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new EventFee();
        }

        /// <summary>
        /// Update an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <param name="eventFee">The new values of EventFee</param>
        /// <returns>The updated EventFee</returns>
        public EventFee PutEventFee(string accessToken, string apiKey, string eventId, string feeId, EventFee eventFee)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventFees), eventId, feeId);

            string json = eventFee.ToJSON();

            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<EventFee>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new EventFee();
        }

        /// <summary>
        ///  Delete an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventFee(string accessToken, string apiKey, string eventId, string feeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventFees), eventId, feeId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        /// <summary>
        /// Create an individual event fee
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="eventFee">EventFee object</param>
        /// <returns>The newly created EventFee</returns>
        public EventFee PostEventFee(string accessToken, string apiKey, string eventId, EventFee eventFee)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventFees), eventId, null);

            string json = eventFee.ToJSON();

            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<EventFee>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new EventFee();
        }

        #endregion

        #region Promocodes

        /// <summary>
        /// Retrieve all existing promo codes for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of Promocode</returns>
        public List<Promocode> GetAllPromocodes(string accessToken, string apiKey, string eventId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventPromocode), eventId, null);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<List<Promocode>>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new List<Promocode>();
        }

        /// <summary>
        /// Retrieve an existing promo codes for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>The Promocode object</returns>
        public Promocode GetPromocode(string accessToken, string apiKey, string eventId, string promocodeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventPromocode), eventId, promocodeId);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<Promocode>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new Promocode();
        }

        /// <summary>
        /// Create a new promo code for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocode">Promocode object to be created</param>
        /// <returns>The newly created Promocode</returns>
        public Promocode PostPromocode(string accessToken, string apiKey, string eventId, Promocode promocode)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventPromocode), eventId, null);

            string json = promocode.ToJSON();

            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<Promocode>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new Promocode();
        }

        /// <summary>
        /// Update a promo code for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <param name="promocode">The new Promocode values</param>
        /// <returns>The newly updated Promocode</returns>
        public Promocode PutPromocode(string accessToken, string apiKey, string eventId, string promocodeId, Promocode promocode)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventPromocode), eventId, promocodeId);

            string json = promocode.ToJSON();

            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<Promocode>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new Promocode();
        }

        /// <summary>
        /// Delete a promo code for an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeletePromocode(string accessToken, string apiKey, string eventId, string promocodeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventPromocode), eventId, promocodeId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        #endregion

        #region Registrants

        /// <summary>
        /// Retrieve detailed information for a specific event registrant
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="registrantId">Redistrant id</param>
        /// <returns>Registrant details</returns>
        public Registrant GetRegistrant(string accessToken, string apiKey, string eventId, string registrantId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventRegistrant), eventId, registrantId);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<Registrant>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new Registrant();
        }

        /// <summary>
        /// Retrieve a list of registrants for the specified event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>ResultSet containing a results array of Registrant</returns>
        public ResultSet<Registrant> GetAllRegistrants(string accessToken, string apiKey, string eventId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventRegistrant), eventId, null);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<ResultSet<Registrant>>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new ResultSet<Registrant>();
        }

        #endregion

        #region EventItems

        /// <summary>
        /// Retrieve all existing items associated with an event
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of EventItem</returns>
        public List<EventItem> GetAllEventItems(string accessToken, string apiKey, string eventId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventItem), eventId, null);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<List<EventItem>>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new List<EventItem>();
        }

        /// <summary>
        ///  Retrieve specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">Eventitem id</param>
        /// <returns>EventItem object</returns>
        public EventItem GetEventItem(string accessToken, string apiKey, string eventId, string itemId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventItem), eventId, itemId);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<EventItem>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new EventItem();
        }

        /// <summary>
        ///  Update a specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="eventItem">The newly values for EventItem</param>
        /// <returns>The updated EventItem</returns>
        public EventItem PutEventItem(string accessToken, string apiKey, string eventId, string itemId, EventItem eventItem)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventItem), eventId, itemId);
            string json = eventItem.ToJSON();

            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<EventItem>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new EventItem();
        }

        /// <summary>
        ///  Create a specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="eventItem">EventItem id</param>
        /// <returns>The newly created EventItem</returns>
        public EventItem PostEventItem(string accessToken, string apiKey, string eventId, EventItem eventItem)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventItem), eventId, null);
            string json = eventItem.ToJSON();

            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<EventItem>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new EventItem();
        }

        /// <summary>
        /// Delete a specific event item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventItem(string accessToken, string apiKey, string eventId, string itemId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventItem), eventId, itemId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        #endregion

        #region Attribute

        /// <summary>
        /// Create an attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attribute">The Attribute object</param>
        /// <returns>The newly created attribure</returns>
        public CTCT.Components.EventSpot.Attribute PostEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, CTCT.Components.EventSpot.Attribute attribute)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, null);
            string json = attribute.ToJSON();

            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<CTCT.Components.EventSpot.Attribute>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new CTCT.Components.EventSpot.Attribute();
        }

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
        public CTCT.Components.EventSpot.Attribute PutEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId,  CTCT.Components.EventSpot.Attribute attribute)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, attributeId);
            string json = attribute.ToJSON();

            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                return response.Get<CTCT.Components.EventSpot.Attribute>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new CTCT.Components.EventSpot.Attribute();
        }

        /// <summary>
        /// Retrieve an existing attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>Attribute object</returns>
        public CTCT.Components.EventSpot.Attribute GetEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, attributeId);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<CTCT.Components.EventSpot.Attribute>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new CTCT.Components.EventSpot.Attribute();
        }

        /// <summary>
        /// Retrieve all existing attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>A list of Attributes</returns>
        public List<CTCT.Components.EventSpot.Attribute> GetAllEventItemAttributes(string accessToken, string apiKey, string eventId, string itemId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, null);

            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Get<List<CTCT.Components.EventSpot.Attribute>>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return new List<CTCT.Components.EventSpot.Attribute>();
        }

        /// <summary>
        /// Delete an existing attributes for an item
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, attributeId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        #endregion
    }
}
