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
        /// Constructs the query with specified parameters.
        /// </summary>
        /// <param name="prms">An array of parameter name and value combinations.</param>
        /// <returns>Returns the query part of the URL.</returns>
        public static string GetQueryParameters(params object[] prms)
        {
            string query = null;
            if (prms != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < prms.Length; i += 2)
                {
                    if (prms[i + 1] == null)
                        continue;

                    if (sb.Length == 0)
                    {
                        sb.Append("?");
                    }
                    else if (sb.Length > 0)
                    {
                        sb.Append("&");
                    }
                    sb.AppendFormat("{0}={1}", HttpUtility.UrlEncode(prms[i].ToString()), HttpUtility.UrlEncode(prms[i + 1].ToString()));
                }
                query = sb.ToString();
            }

            return query;
        }
    }
}
