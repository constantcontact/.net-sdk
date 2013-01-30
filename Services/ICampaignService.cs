using System;
using System.Collections.Generic;
using CTCT.Components.Campaigns;
namespace CTCT.Services
{
    /// <summary>
    /// Interface for CampaignService class.
    /// </summary>
    public interface ICampaignService
    {
        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="offset">Denotes the starting number for the result set.</param>
        /// <param name="limit">Denotes the number of results per set of results, limited to 50</param>
        /// <returns>Returns a list of campaigns.</returns>
        IList<Campaign> GetCampaigns(string accessToken, int? offset, int? limit);
        
        /// <summary>
        /// Get campaign details for a specific campaign.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a campaign.</returns>
        Campaign GetCampaign(string accessToken, string campaignId);
    }
}
