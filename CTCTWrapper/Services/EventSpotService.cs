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
    public class EventSpotService : BaseService
    {

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
            string json = "[{\"op\":\"REPLACE\",\"path\":\"#/status\",\"value\":\"" + eventStatus.ToString() + "\"}]";

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


        //-----------------------------------------------------------------

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
        //-----------------------------------------------------------------


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

        //-----------------------------------------------------------------




        //-----------------------------------------------------------------





        public string GetJson(string url, string accessToken, string apiKey)
        {
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                return response.Body;
            }
            return "";
        }
    }
}
