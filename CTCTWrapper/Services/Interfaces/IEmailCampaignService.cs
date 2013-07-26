using System;
using System.Collections.Generic;
using CTCT.Components.EmailCampaigns;
using CTCT.Components;
namespace CTCT.Services
{
    /// <summary>
    /// Interface for CampaignService class.
    /// </summary>
    public interface IEmailCampaignService
    {
        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a list of campaigns.</returns>
        ResultSet<EmailCampaign> GetCampaigns(string accessToken, string apiKey, CampaignStatus? status, int? limit, DateTime? modifiedSince);
        
        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <param name="pag">Pagination object returned by a previous call to GetCampaigns</param>
        /// <returns>Returns a list of campaigns.</returns>
        ResultSet<EmailCampaign> GetCampaigns(string accessToken, string apiKey, CampaignStatus? status, int? limit, DateTime? modifiedSince, Pagination pag);

        /// <summary>
        /// Get campaign details for a specific campaign.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a campaign.</returns>
        EmailCampaign GetCampaign(string accessToken, string apiKey, string campaignId);

        /// <summary>
        /// Create a new campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaign">Campign to be created</param>
        /// <returns>Returns a campaign.</returns>
        EmailCampaign AddCampaign(string accessToken, string apiKey, EmailCampaign campaign);

        /// <summary>
        /// Delete an email campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaignId">Valid campaign id.</param>
        /// <returns>Returns true if successful.</returns>
        bool DeleteCampaign(string accessToken, string apiKey, string campaignId);

        /// <summary>
        /// Update a specific email campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="campaign">Campaign to be updated.</param>
        /// <returns>Returns a campaign.</returns>
        EmailCampaign UpdateCampaign(string accessToken, string apiKey, EmailCampaign campaign);
    }
}
