using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Contacts;
using CTCT.Components;
using CTCT.Util;
using CTCT.Exceptions;
using CTCT.Components.AccountService;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to getting list of verified email addresses
    /// </summary>
    public class AccountService : BaseService, IAccountService
    {
        /// <summary>
        /// Account service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public AccountService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

        /// <summary>
        /// Retrieve a list of all the account owner's email addresses
        /// </summary>
        /// <returns>list of all verified account owner's email addresses</returns>
        public IList<VerifiedEmailAddress> GetVerifiedEmailAddress()
        {
            IList<VerifiedEmailAddress> emails = new List<VerifiedEmailAddress>();

            // Construct access URL
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.AccountVerifiedEmailAddressess);

            // Get REST response
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var result = response.Get<IList<VerifiedEmailAddress>>();
                return result;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get account summary information
        /// </summary>
        /// <returns>An AccountSummaryInformation object</returns>
        public AccountSummaryInformation GetAccountSummaryInformation()
        {
            // Construct access URL
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.AccountSummaryInformation);

            // Get REST response
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var result = response.Get<AccountSummaryInformation>();
                return result;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates account summary information
        /// </summary>
        /// <param name="accountSumaryInfo">An AccountSummaryInformation object</param>
        /// <returns>An AccountSummaryInformation object</returns>
        public AccountSummaryInformation PutAccountSummaryInformation(AccountSummaryInformation accountSumaryInfo)
        {
            if (accountSumaryInfo == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.ObjectNull);
            }

            // Construct access URL
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.AccountSummaryInformation);

            string json = accountSumaryInfo.ToJSON();
            // Get REST response
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var result = response.Get<AccountSummaryInformation>();
                return result;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
        }
    }
}
