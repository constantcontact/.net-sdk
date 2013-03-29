using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.EmailCampaigns;
using CTCT.Util;
using CTCT.Components;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to scheduling Constant Contact Campaigns.
    /// </summary>
    public class CampaignScheduleService : BaseService, ICampaignScheduleService
    {
        /// <summary>
        /// Create a new schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to be created.</param>
        /// <returns>Returns the schedule added.</returns>
        public Schedule AddSchedule(string accessToken, string apiKey, string campaignId, Schedule schedule)
        {
            Schedule sch = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.CampaignSchedules, campaignId));
            string json = schedule.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                sch = Component.FromJSON<Schedule>(response.Body);
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return sch;
        }

        /// <summary>
        /// Get a list of schedules for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <returns>Returns the list of schedules for the specified campaign.</returns>
        public IList<Schedule> GetSchedules(string accessToken, string apiKey, string campaignId)
        {
            IList<Schedule> list = new List<Schedule>();
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.CampaignSchedules, campaignId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                list = Component.FromJSON<IList<Schedule>>(response.Body);
            }

            return list;
        }

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be get a schedule for.</param>
        /// <param name="scheduleId">Schedule id to retrieve.</param>
        /// <returns>Returns the requested schedule object.</returns>
        public Schedule GetSchedule(string accessToken, string apiKey, string campaignId, string scheduleId)
        {
            Schedule schedule = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.CampaignSchedule, campaignId, scheduleId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                schedule = Component.FromJSON<Schedule>(response.Body);
            }

            return schedule;
        }

        /// <summary>
        /// Update a specific schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to retrieve.</param>
        /// <returns>Returns the updated schedule object.</returns>
        public Schedule UpdateSchedule(string accessToken, string apiKey, string campaignId, Schedule schedule)
        {
            Schedule upd = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.CampaignSchedule, campaignId, schedule.Id));
            string json = schedule.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                upd = Component.FromJSON<Schedule>(response.Body);
            }

            return upd;
        }

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="scheduleId">Schedule id to delete.</param>
        /// <returns>Returns true if shcedule was successfully deleted.</returns>
        public bool DeleteSchedule(string accessToken, string apiKey, string campaignId, string scheduleId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.CampaignSchedule, campaignId, scheduleId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Send a test send of a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Id of campaign to send test of.</param>
        /// <param name="testSend">Test send details.</param>
        /// <returns>Returns the sent test object.</returns>
        public TestSend SendTest(string accessToken, string apiKey, string campaignId, TestSend testSend)
        {
            TestSend test = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.CampaignTestSends, campaignId));
            string json = testSend.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                test = Component.FromJSON<TestSend>(response.Body);
            }

            return test;
        }
    }
}
