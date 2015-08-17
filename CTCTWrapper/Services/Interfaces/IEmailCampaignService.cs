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
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a list of campaigns.</returns>
        ResultSet<EmailCampaign> GetCampaigns(CampaignStatus? status, int? limit, DateTime? modifiedSince);
        
        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="status">Returns list of email campaigns with specified status.</param>
        /// <param name="limit">Specifies the number of results per page in the output, from 1 - 500, default = 500.</param>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <param name="pag">Pagination object returned by a previous call to GetCampaigns</param>
        /// <returns>Returns a list of campaigns.</returns>
        ResultSet<EmailCampaign> GetCampaigns(CampaignStatus? status, int? limit, DateTime? modifiedSince, Pagination pag);

        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="modifiedSince">limit campaigns to campaigns modified since the supplied date</param>
        /// <returns>Returns a list of campaigns.</returns>
        ResultSet<EmailCampaign> GetCampaigns(DateTime? modifiedSince);

        /// <summary>
        /// Get campaign details for a specific campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a campaign.</returns>
        EmailCampaign GetCampaign(string campaignId);

        /// <summary>
        /// Create a new campaign.
        /// </summary>
        /// <param name="campaign">Campign to be created</param>
        /// <returns>Returns a campaign.</returns>
        EmailCampaign AddCampaign(EmailCampaign campaign);

        /// <summary>
        /// Delete an email campaign.
        /// </summary>
        /// <param name="campaignId">Valid campaign id.</param>
        /// <returns>Returns true if successful.</returns>
        bool DeleteCampaign(string campaignId);

        /// <summary>
        /// Update a specific email campaign.
        /// </summary>
        /// <param name="campaign">Campaign to be updated.</param>
        /// <returns>Returns a campaign.</returns>
        EmailCampaign UpdateCampaign(EmailCampaign campaign);

        /// <summary>
        /// Retrieve the text and HTML content to preview an existing email campaign.
        /// </summary>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a EmailCampaignPreview</returns>
        EmailCampaignPreview GetCampaignPreview(string campaignId);
    }
}
