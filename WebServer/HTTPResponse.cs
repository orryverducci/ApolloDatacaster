using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public abstract class HTTPResponse
    {
        /// <summary>
        /// The status code of the HTTP response
        /// </summary>
        public int StatusCode { protected set; get; } = 200;

        /// <summary>
        /// The mime type of the HTTP response
        /// </summary>
        public string MimeType { protected set; get; } = "application/octet-stream";

        /// <summary>
        /// Returns the HTTP response
        /// </summary>
        public abstract byte[] GetResponse();
    }
}