using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Util;
using CTCT.Components.Campaigns;
using CTCT.Components;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to the Contacts Collection.
    /// </summary>
    public class CampaignService : BaseService, ICampaignService
    {
        /// <summary>
        /// Get a set of campaigns.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="offset">Denotes the starting number for the result set.</param>
        /// <param name="limit">Denotes the number of results per set of results, limited to 50</param>
        /// <returns>Returns a list of campaigns.</returns>
        public IList<Campaign> GetCampaigns(string accessToken, int? offset, int? limit)
        {
            IList<Campaign> campaigns = new List<Campaign>();
            string url = PaginateUrl(String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.Campaigns), offset, limit);
            CUrlResponse response = RestClient.Get(url, accessToken);
            if (response.HasData)
            {
                campaigns = Component.FromJSON<IList<Campaign>>(response.Body);
            }
            
            return campaigns;
        }

        /// <summary>
        /// Get campaign details for a specific campaign.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns a campaign.</returns>
        public Campaign GetCampaign(string accessToken, string campaignId)
        {
            Campaign campaign = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.CampaignId, campaignId));
            CUrlResponse response = RestClient.Get(url, accessToken);
            if (response.HasData)
            {
                campaign = Component.FromJSON<Campaign>(response.Body);
            }

            return campaign;
        }
    }
}
