using System.Net;

namespace WebServer
{
    /// <summary>
    /// Contains information about the received request
    /// </summary>
    public class RequestInfo
    {
        /// <summary>
        /// The IP address the request originated from
        /// </summary>
        public IPAddress SourceIP { get; set; }
    }
}