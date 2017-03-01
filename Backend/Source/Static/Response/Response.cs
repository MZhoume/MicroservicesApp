namespace Static.Response
{
    using System.Collections.Generic;

    /// <summary>
    /// Response from common service
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the results returned from the service
        /// </summary>
        /// <returns> Results </returns>
        public object Payload { get; set; }
    }
}