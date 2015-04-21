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
        /// Campaign schedule service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public CampaignScheduleService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Create a new schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to be created.</param>
        /// <returns>Returns the schedule added.</returns>
        public Schedule AddSchedule(string campaignId, Schedule schedule)
        {
            if (string.IsNullOrEmpty(campaignId) || schedule == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ScheduleOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.CampaignSchedules, campaignId));
            string json = schedule.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var sch = response.Get<Schedule>();
                return sch;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get a list of schedules for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <returns>Returns the list of schedules for the specified campaign.</returns>
        public IList<Schedule> GetSchedules(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ScheduleOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.CampaignSchedules, campaignId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var scheduleList = response.Get<IList<Schedule>>();
                return scheduleList;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be get a schedule for.</param>
        /// <param name="scheduleId">Schedule id to retrieve.</param>
        /// <returns>Returns the requested schedule object.</returns>
        public Schedule GetSchedule(string campaignId, string scheduleId)
        {
            if (string.IsNullOrEmpty(campaignId) || string.IsNullOrEmpty(scheduleId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ScheduleOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.CampaignSchedule, campaignId, scheduleId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var schedule = response.Get<Schedule>();
                return schedule;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Update a specific schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to retrieve.</param>
        /// <returns>Returns the updated schedule object.</returns>
        public Schedule UpdateSchedule(string campaignId, Schedule schedule)
        {
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.CampaignSchedule, campaignId, schedule.Id));
            string json = schedule.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var upd = response.Get<Schedule>();
                return upd;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="scheduleId">Schedule id to delete.</param>
        /// <returns>Returns true if shcedule was successfully deleted.</returns>
        public bool DeleteSchedule(string campaignId, string scheduleId)
        {
            if (string.IsNullOrEmpty(campaignId) || string.IsNullOrEmpty(scheduleId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ScheduleOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.CampaignSchedule, campaignId, scheduleId));
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
        /// Send a test send of a campaign.
        /// </summary>
        /// <param name="campaignId">Id of campaign to send test of.</param>
        /// <param name="testSend">Test send details.</param>
        /// <returns>Returns the sent test object.</returns>
        public TestSend SendTest(string campaignId, TestSend testSend)
        {
            if (string.IsNullOrEmpty(campaignId) || testSend == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ScheduleOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.CampaignTestSends, campaignId));
            string json = testSend.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var test = response.Get<TestSend>();
                return test;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }
    }
}
