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
        /// <returns>list of all verified account owner's email addresses</returns>
        IList<VerifiedEmailAddress> GetVerifiedEmailAddress();

        /// <summary>
        /// Get account summary information
        /// </summary>
        /// <returns>An AccountSummaryInformation object</returns>
        AccountSummaryInformation GetAccountSummaryInformation();

        /// <summary>
        /// Updates account summary information
        /// </summary>
        /// <param name="accountSumaryInfo">An AccountSummaryInformation object</param>
        /// <returns>An AccountSummaryInformation object</returns>
        AccountSummaryInformation PutAccountSummaryInformation( AccountSummaryInformation accountSumaryInfo);
    }
}
