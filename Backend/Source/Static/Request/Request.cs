namespace Static.Request
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Request for common Service
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Gets or sets the authorization token
        /// </summary>
        /// <returns> The JWT auth token </returns>
        public string AuthToken { get; set; }

        /// <summary>
        /// Gets or sets the target db the request specifies
        /// </summary>
        /// <returns> The request target </returns>
        [JsonConverter(typeof(StringEnumConverter))]
        public TargetDb TargetDb { get; set; }

        /// <summary>
        /// Gets or sets the operation the request specifies
        /// </summary>
        /// <returns> The request operation </returns>
        [JsonConverter(typeof(StringEnumConverter))]
        public Operation Operation { get; set; }

        /// <summary>
        /// Gets or sets the payload
        /// </summary>
        /// <returns> The payload </returns>
        public IEnumerable<object> Payload { get; set; }

        /// <summary>
        /// Gets or sets the paging info
        /// </summary>
        /// <returns> The Paging Info </returns>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// Gets or sets the search term that the request specifies
        /// </summary>
        /// <returns> The search term </returns>
        public IEnumerable<SearchTerm> SearchTerm { get; set; }
    }
}