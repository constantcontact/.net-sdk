using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Contacts;
using CTCT.Components.AccountService;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for account service
    /// </summary>
    public interface  IAccountService
    {
        /// <summary>
        /// Retrieve a list of all the account owner's email addresses
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>list of all verified account owner's email addresses</returns>
        IList<VerifiedEmailAddress> GetVerifiedEmailAddress(string accessToken, string apiKey);

        /// <summary>
        /// Get account summary information
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>An AccountSummaryInformation object</returns>
        AccountSummaryInformation GetAccountSummaryInformation(string accessToken, string apiKey);

        /// <summary>
        /// Updates account summary information
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="accountSumaryInfo">An AccountSummaryInformation object</param>
        /// <returns>An AccountSummaryInformation object</returns>
        AccountSummaryInformation PutAccountSummaryInformation(string accessToken, string apiKey, AccountSummaryInformation accountSumaryInfo);
    }
}
