using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// The HTTP Method requested
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The content type of the sent data, expressed as an Internet media type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The data sent with the request
        /// </summary>
        public Stream SentData { get; set; }
        
        /// <summary>
        /// Dictionary of queries requested as GET queries in the URL
        /// </summary>
        public Dictionary<string, string> GetQueries { get; set; }

        /// <summary>
        /// Dictionary of data as sent as HTTP POST data
        /// </summary>
        public Dictionary<string, string> PostData { get; set; }
    }
}