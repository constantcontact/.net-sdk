using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.EmailCampaigns;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for CampaignScheduleService class.
    /// </summary>
    public interface ICampaignScheduleService
    {
        /// <summary>
        /// Create a new schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to be created.</param>
        /// <returns>Returns the schedule added.</returns>
        Schedule AddSchedule(string accessToken, string apiKey, string campaignId, Schedule schedule);

        /// <summary>
        /// Get a list of schedules for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <returns>Returns the list of schedules for the specified campaign.</returns>
        IList<Schedule> GetSchedules(string accessToken, string apiKey, string campaignId);

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be get a schedule for.</param>
        /// <param name="scheduleId">Schedule id to retrieve.</param>
        /// <returns>Returns the requested schedule object.</returns>
        Schedule GetSchedule(string accessToken, string apiKey, string campaignId, string scheduleId);

        /// <summary>
        /// Update a specific schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id to be scheduled.</param>
        /// <param name="schedule">Schedule to retrieve.</param>
        /// <returns>Returns the updated schedule object.</returns>
        Schedule UpdateSchedule(string accessToken, string apiKey, string campaignId, Schedule schedule);

        /// <summary>
        /// Get a specific schedule for a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="scheduleId">Schedule id to delete.</param>
        /// <returns>Returns true if shcedule was successfully deleted.</returns>
        bool DeleteSchedule(string accessToken, string apiKey, string campaignId, string scheduleId);

        /// <summary>
        /// Send a test send of a campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Id of campaign to send test of.</param>
        /// <param name="testSend">Test send details.</param>
        /// <returns>Returns the sent test object.</returns>
        TestSend SendTest(string accessToken, string apiKey, string campaignId, TestSend testSend);
    }
}
