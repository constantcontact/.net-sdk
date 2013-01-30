using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Util;
using System.Collections.Specialized;
using System.Web;

namespace CTCT.Services
{
    /// <summary>
    /// Super class for all services.
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// Get the rest client being used by the service.
        /// </summary>
        public virtual IRestClient RestClient { get; set; }
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        public BaseService()
        {
            this.RestClient = new RestClient();
        }

        /// <summary>
        /// Constructor with the option to to supply an alternative rest client to be used.
        /// </summary>
        /// <param name="restClient">RestClientInterface implementation to be used in the service.</param>
        public BaseService(RestClient restClient)
        {
            this.RestClient = restClient;
        }

        /// <summary>
        /// Helper function to build a url depending on the offset and limit.
        /// </summary>
        /// <param name="url">Url.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Limit.</param>
        /// <returns>Returns an URL string.</returns>
        protected static string PaginateUrl(string url, int? offset, int? limit)
        {
            string paginateUrl = url;
            StringBuilder sb = new StringBuilder();

            if (offset.HasValue)
            {
                sb.AppendFormat("offset={0}", offset);
            }

            if (limit.HasValue)
            {
                sb.AppendFormat("{0}limit={1}", (sb.Length > 0) ? "&" : String.Empty, limit);
            }

            if (sb.Length > 0)
            {
                paginateUrl = String.Concat(url, "?", sb.ToString());
            }

            return paginateUrl;
        }
    }
}
