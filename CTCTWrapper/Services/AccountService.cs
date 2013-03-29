using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Contacts;
using CTCT.Components;
using CTCT.Util;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to getting list of verified email addresses
    /// </summary>
    public class AccountService: BaseService, IAccountService
    {
        /// <summary>
        /// Retrieve a list of all the account owner's email addresses
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>list of all verified account owner's email addresses</returns>
        public IList<VerifiedEmailAddress> GetVerifiedEmailAddress(string accessToken, string apiKey)
        {
            IList<VerifiedEmailAddress> emails = new List<VerifiedEmailAddress>();

            // Construct access URL
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.AccountVerifiedEmailAddressess);

            // Get REST response
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);
            if (response.HasData)
            {
                IList<VerifiedEmailAddress> result = response.Get<IList<VerifiedEmailAddress>>();
                return result;
            }
            else
                if (response.IsError)
                {
                    throw new CtctException(response.GetErrorMessage());
                }

            return emails;
        }
    }
}
