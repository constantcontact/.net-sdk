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
        /// Email campaign service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public EmailCampaignService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a list of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(DateTime? modifiedSince)
        {
            return GetCampaigns(null, null, modifiedSince, null);
        }

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a ResultSet of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(CampaignStatus? status, int? limit, DateTime? modifiedSince)
        {
            return this.GetCampaigns(status, limit, modifiedSince, null);
        }

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <param name="pag">Pagination object returned by a previous call to GetCampaigns</param>
        /// <returns>Returns a ResultSet of campaigns.</returns>
        public ResultSet<EmailCampaign> GetCampaigns(CampaignStatus? status, int? limit, DateTime? modifiedSince, Pagination pag)
        {
            string url = (pag == null) ? String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.Campaigns, GetQueryParameters(new object[] { "status", status, "limit", limit, "modified_since", Extensions.ToISO8601String(modifiedSince) })) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<EmailCampaign>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
        }

        /// <summary>
        /// Get campaign details for a specific campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign GetCampaign(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.EmailCampaignOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Campaign, campaignId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var campaign = response.Get<EmailCampaign>();
                return campaign;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
        }

        /// <summary>
        /// Create a new campaign.
        /// </summary>
        /// <param name="campaign">Campign to be created</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign AddCampaign(EmailCampaign campaign)
        {
            if (campaign == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.EmailCampaignOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.Campaigns);
            string json = campaign.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var emailcampaign = response.Get<EmailCampaign>();
                return emailcampaign;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
        }

        /// <summary>
        /// Delete an email campaign.
        /// </summary>
        /// <param name="campaignId">Valid campaign id.</param>
        /// <returns>Returns true if successful.</returns>
        public bool DeleteCampaign(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.EmailCampaignOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Campaign, campaignId));
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
        /// Update a specific email campaign.
        /// </summary>
        /// <param name="campaign">Campaign to be updated.</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign UpdateCampaign(EmailCampaign campaign)
        {
            if (campaign == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.EmailCampaignOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.Campaign, campaign.Id));
            // Invalidate data that are not supported by the update API procedure
            campaign.CreatedDate = null;
            campaign.TrackingSummary = null;
            campaign.ModifiedDate = null;
            campaign.IsVisibleInUI = null;
            // Convert to JSON string
            string json = campaign.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var emailCampaign = response.Get<EmailCampaign>();
                return emailCampaign;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieve the text and HTML content to preview an existing email campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a EmailCampaignPreview</returns>
        public EmailCampaignPreview GetCampaignPreview(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.EmailCampaignOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.EmailCampaignPreview, campaignId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var campaignPreview = response.Get<EmailCampaignPreview>();
                return campaignPreview;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
        }
    }
}
