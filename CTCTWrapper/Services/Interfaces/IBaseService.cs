using System;
using CTCT.Util;

namespace CTCT.Services
{
    /// <summary>
    /// Interface for BaseService class.
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// User service context
        /// </summary>
        IUserServiceContext UserServiceContext { get; }

        /// <summary>
        /// Returns the REST client object.
        /// </summary>
        IRestClient RestClient { get; }
    }
}
