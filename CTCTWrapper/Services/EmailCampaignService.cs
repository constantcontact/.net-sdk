using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Util;
using CTCT.Components.EmailCampaigns;
using CTCT.Components;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to the Contacts Collection.
    /// </summary>
    public class EmailCampaignService : BaseService, IEmailCampaignService
    {

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a ResultSet of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(string accessToken, string apiKey, CampaignStatus? status, int? limit, DateTime? modifiedSince)
        {
            return this.GetCampaigns(accessToken, apiKey, status, limit, modifiedSince, null);
        }
        
        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <param name="pag">Pagination object returned by a previous call to GetCampaigns</param>
        /// <returns>Returns a ResultSet of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(string accessToken, string apiKey, CampaignStatus? status, int? limit, DateTime? modifiedSince, Pagination pag)
        {
            ResultSet<EmailCampaign> results = null;
            string url = (pag == null) ? String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Campaigns, GetQueryParameters(new object[] { "status", status, "limit", limit, "modified_since", Extensions.ToISO8601String(modifiedSince) })) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = response.Get<ResultSet<EmailCampaign>>();
            }

            return results;
        }

        /// <summary>
        /// Get campaign details for a specific campaign.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign GetCampaign(string accessToken, string apiKey, string campaignId)
        {
            EmailCampaign campaign = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Campaign, campaignId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                campaign = response.Get<EmailCampaign>();
            }

            return campaign;
        }

        /// <summary>
        /// Create a new campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaign">Campign to be created</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign AddCampaign(string accessToken, string apiKey, EmailCampaign campaign)
        {
            EmailCampaign emailcampaign = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Campaigns);
            string json = campaign.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);
            if (response.HasData)
            {
                emailcampaign = response.Get<EmailCampaign>();
            }
            else if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return emailcampaign;
        }

        /// <summary>
        /// Delete an email campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Valid campaign id.</param>
        /// <returns>Returns true if successful.</returns>
        public bool DeleteCampaign(string accessToken, string apiKey, string campaignId)
        {
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Campaign, campaignId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update a specific email campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaign">Campaign to be updated.</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign UpdateCampaign(string accessToken, string apiKey, EmailCampaign campaign)
        {
            EmailCampaign emailCampaign = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.Campaign, campaign.Id));
            // Invalidate data that are not supported by the update API procedure
            campaign.CreatedDate = null;
            campaign.TrackingSummary = null;
            campaign.ModifiedDate = null;
            campaign.IsVisibleInUI = null;
            // Convert to JSON string
            string json = campaign.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                emailCampaign = response.Get<EmailCampaign>();
            }

            return emailCampaign;
        }
    }
}
