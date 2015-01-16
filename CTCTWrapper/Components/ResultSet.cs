using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.Tracking;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components
{
    /// <summary>
    /// Container for a get on a collection, such as Contacts, Campaigns, or TrackingData.
    /// </summary>
    /// <typeparam name="T">An object derived from Component class.</typeparam>
    [DataContract]
    [Serializable]
    public class ResultSet<T> where T : Component
    {
        #region Fields

        [DataMember(Name = "results")]
        private List<T> _Results;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the array of result objects returned.
        /// </summary>
        public IList<T> Results 
        {
            get { return _Results; }
            set { _Results = value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the next link.
        /// </summary>
        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }

        #endregion
    }

    /// <summary>
    /// Meta class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Meta
    {
        #region Properties

        /// <summary>
        /// Gets or sets the pagination link.
        /// </summary>
        [DataMember(Name="pagination")]
        public Pagination Pagination { get; set; }

        #endregion
    }

    /// <summary>
    /// Pagination class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Pagination
    {
        #region Properties

        /// <summary>
        /// Gets or sets the next link.
        /// </summary>
        [DataMember(Name="next_link")]
        public string Next { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Format the URL for the next page call.
        /// </summary>
        /// <returns>Returns the URL for the next page call.</returns>
        public string GetNextUrl()
        {
            return String.Concat(Config.Endpoints.BaseUrl, this.Next.Replace("/v2/", String.Empty));
        }

        #endregion
    }
}
