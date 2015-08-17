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
        /// <summary>
        /// EventSspot service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public EventSpotService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// View all existing events
        /// </summary>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 50</param>
        /// <param name="pag">Pagination object</param>
        /// <returns>ResultSet containing a results array of IndividualEvents</returns>
        public ResultSet<IndividualEvent> GetAllEventSpots(int? limit, Pagination pag)
        {
            string url = (pag == null) ? String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventSpots, GetQueryParameters(new object[] { "limit", limit })) : pag.GetNextUrl();

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var individualEventSet = response.Get<ResultSet<IndividualEvent>>();
                return individualEventSet;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve an event specified by the event_id
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>The event</returns>
        public IndividualEvent GetEventSpot(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventSpots, "/", eventId);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var individualEvent = response.Get<IndividualEvent>();
                return individualEvent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="eventSpot">The event to publish</param>
        /// <returns>The published event</returns>
        public IndividualEvent PostEventSpot(IndividualEvent eventSpot)
        {
            if (eventSpot == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventSpots);
            string json = eventSpot.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var individualEvent = response.Get<IndividualEvent>();
                return individualEvent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="eventId">Event id to be updated</param>
        /// <param name="eventSpot">The new values for event</param>
        /// <returns>The updated event</returns>
        public IndividualEvent PutEventSpot(string eventId, IndividualEvent eventSpot)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (eventSpot == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventSpots, "/", eventId);
            string json = eventSpot.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var individualEvent = response.Get<IndividualEvent>();
                return individualEvent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Publish or cancel an event by changing the status of the event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="eventStatus">New status of the event. ACTIVE" and "CANCELLED are allowed</param>
        /// <returns>The updated event</returns>
        public IndividualEvent PatchEventSpotStatus(string eventId, EventStatus eventStatus)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventSpots, "/", eventId);

            var patchRequests = new List<PatchRequest>();
            var patchRequest = new PatchRequest("REPLACE", "#/status", eventStatus.ToString());
            patchRequests.Add(patchRequest);

            string json = patchRequests.ToJSON();

            RawApiResponse response = RestClient.Patch(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var individualEvent = response.Get<IndividualEvent>();
                return individualEvent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve all existing fees for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of event fees for the specified event</returns>
        public List<EventFee> GetAllEventFees(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventFees), eventId, null);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var eventFeesList = response.Get<List<EventFee>>();
                return eventFeesList;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>An EventFee object</returns>
        public EventFee GetEventFee(string eventId, string feeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(feeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventFees), eventId, feeId);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var eventFee = response.Get<EventFee>();
                return eventFee;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Update an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <param name="eventFee">The new values of EventFee</param>
        /// <returns>The updated EventFee</returns>
        public EventFee PutEventFee(string eventId, string feeId, EventFee eventFee)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(feeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (eventFee == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventFees), eventId, feeId);

            string json = eventFee.ToJSON();

            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var evFee = response.Get<EventFee>();
                return evFee;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Delete an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="feeId">EventFee id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventFee(string eventId, string feeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(feeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventFees), eventId, feeId);

            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create an individual event fee
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="eventFee">EventFee object</param>
        /// <returns>The newly created EventFee</returns>
        public EventFee PostEventFee(string eventId, EventFee eventFee)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (eventFee == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventFees), eventId, null);

            string json = eventFee.ToJSON();

            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var evFee = response.Get<EventFee>();
                return evFee;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve all existing promo codes for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of Promocode</returns>
        public List<Promocode> GetAllPromocodes(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventPromocode), eventId, null);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var promocodeList = response.Get<List<Promocode>>();
                return promocodeList;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve an existing promo codes for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>The Promocode object</returns>
        public Promocode GetPromocode(string eventId, string promocodeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(promocodeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventPromocode), eventId, promocodeId);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var promocode = response.Get<Promocode>();
                return promocode;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create a new promo code for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocode">Promocode object to be created</param>
        /// <returns>The newly created Promocode</returns>
        public Promocode PostPromocode(string eventId, Promocode promocode)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (promocode == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventPromocode), eventId, null);

            string json = promocode.ToJSON();

            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var promoc = response.Get<Promocode>();
                return promoc;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Update a promo code for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <param name="promocode">The new Promocode values</param>
        /// <returns>The newly updated Promocode</returns>
        public Promocode PutPromocode(string eventId, string promocodeId, Promocode promocode)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(promocodeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (promocode == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventPromocode), eventId, promocodeId);

            string json = promocode.ToJSON();

            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var promoc = response.Get<Promocode>();
                return promoc;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete a promo code for an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="promocodeId">Promocode id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeletePromocode(string eventId, string promocodeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(promocodeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventPromocode), eventId, promocodeId);

            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve detailed information for a specific event registrant
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="registrantId">Redistrant id</param>
        /// <returns>Registrant details</returns>
        public Registrant GetRegistrant(string eventId, string registrantId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(registrantId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventRegistrant), eventId, registrantId);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var registrant = response.Get<Registrant>();
                return registrant;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve a list of registrants for the specified event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 50</param>
        /// <param name="pag">Pagination object</param>
        /// <returns>ResultSet containing a results array of Registrant</returns>
        public ResultSet<Registrant> GetAllRegistrants(string eventId, int? limit, Pagination pag)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = (pag == null) ? String.Concat(String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventRegistrant), eventId, null), GetQueryParameters(new object[] { "limit", limit })) : pag.GetNextUrl();

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var registrantSet = response.Get<ResultSet<Registrant>>();
                return registrantSet;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve all existing items associated with an event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>A list of EventItem</returns>
        public List<EventItem> GetAllEventItems(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventItem), eventId, null);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var eventItemList = response.Get<List<EventItem>>();
                return eventItemList;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Retrieve specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">Eventitem id</param>
        /// <returns>EventItem object</returns>
        public EventItem GetEventItem(string eventId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventItem), eventId, itemId);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var eventItem = response.Get<EventItem>();
                return eventItem;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Update a specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="eventItem">The newly values for EventItem</param>
        /// <returns>The updated EventItem</returns>
        public EventItem PutEventItem(string eventId, string itemId, EventItem eventItem)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (eventItem == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventItem), eventId, itemId);
            string json = eventItem.ToJSON();

            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var eventIt = response.Get<EventItem>();
                return eventIt;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Create a specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="eventItem">EventItem id</param>
        /// <returns>The newly created EventItem</returns>
        public EventItem PostEventItem(string eventId, EventItem eventItem)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (eventItem == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventItem), eventId, null);
            string json = eventItem.ToJSON();

            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var eventIt = response.Get<EventItem>();
                return eventIt;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete a specific event item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventItem(string eventId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.EventItem), eventId, itemId);

            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create an attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attribute">The Attribute object</param>
        /// <returns>The newly created attribure</returns>
        public CTCT.Components.EventSpot.Attribute PostEventItemAttribute(string eventId, string itemId, CTCT.Components.EventSpot.Attribute attribute)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (attribute == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.ItemAttribute), eventId, itemId, null);
            string json = attribute.ToJSON();

            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var attrib = response.Get<CTCT.Components.EventSpot.Attribute>();
                return attrib;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates an existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <param name="attribute">Attribute new values</param>
        /// <returns>The newly updated attribute</returns>
        public CTCT.Components.EventSpot.Attribute PutEventItemAttribute(string eventId, string itemId, string attributeId, CTCT.Components.EventSpot.Attribute attribute)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(attributeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (attribute == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.ItemAttribute), eventId, itemId, attributeId);
            string json = attribute.ToJSON();

            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var attrib = response.Get<CTCT.Components.EventSpot.Attribute>();
                return attrib;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve an existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>Attribute object</returns>
        public CTCT.Components.EventSpot.Attribute GetEventItemAttribute(string eventId, string itemId, string attributeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(attributeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.ItemAttribute), eventId, itemId, attributeId);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var attrib =  response.Get<CTCT.Components.EventSpot.Attribute>();
                return attrib;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve all existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <returns>A list of Attributes</returns>
        public List<CTCT.Components.EventSpot.Attribute> GetAllEventItemAttributes(string eventId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.ItemAttribute), eventId, itemId, null);

            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var attrib =  response.Get<List<CTCT.Components.EventSpot.Attribute>>();
                return attrib;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete an existing attributes for an item
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="itemId">EventItem id</param>
        /// <param name="attributeId">Attribute id</param>
        /// <returns>True if successfuly deleted</returns>
        public bool DeleteEventItemAttribute(string eventId, string itemId, string attributeId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }
            if (string.IsNullOrWhiteSpace(attributeId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.InvalidId);
            }

            string url = String.Format(String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.ItemAttribute), eventId, itemId, attributeId);

            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }
    }
}
