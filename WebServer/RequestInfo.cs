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

        /// <summary>
        /// The requested domain name
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Array of the seperate segments of the requested URL path
        /// </summary>
        public string[] Path { get; set; }
    }
}