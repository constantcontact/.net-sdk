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
    public class EventSpotService : BaseService
    {
        #region Events

        public ResultSet<IndividualEvent> GetEventsCollection(string accessToken, string apiKey, int? limit, Pagination pag)
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


        public IndividualEvent PatchEventStatus(string accessToken, string apiKey, string eventId, EventStatus eventStatus)
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


        public void DeleteEventFee(string accessToken, string apiKey, string eventId, string feeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventFees), eventId, feeId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
        }

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

        public void DeletePromocode(string accessToken, string apiKey, string eventId, string promocodeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventPromocode), eventId, promocodeId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
        }

        #endregion

        #region Registrants

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

        public void DeleteEventItem(string accessToken, string apiKey, string eventId, string itemId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.EventItem), eventId, itemId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
        }

        #endregion

        #region Attribute

        public CTCT.Components.EventSpot.Attribute PostEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, CTCT.Components.EventSpot.Attribute Attribute)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, null);
            string json = Attribute.ToJSON();

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

        public CTCT.Components.EventSpot.Attribute PutEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId,  CTCT.Components.EventSpot.Attribute Attribute)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, attributeId);
            string json = Attribute.ToJSON();

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

        public void DeleteEventItemAttribute(string accessToken, string apiKey, string eventId, string itemId, string attributeId)
        {
            string url = String.Format(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.ItemAttribute), eventId, itemId, attributeId);

            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);
            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }
        }

        #endregion
    }
}
