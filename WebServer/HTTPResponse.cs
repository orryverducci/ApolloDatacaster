using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

namespace WebServer
{
    public abstract class HTTPResponse
    {
		/// <summary>
		/// HTTP Response Status Codes
		/// </summary>
		public enum HTTPStatus
		{
            /// <summary>
            /// The request was successful
            /// </summary>
			OK = 200,
            /// <summary>
            /// A new resource is being created
            /// </summary>
			CREATED = 201,
            /// <summary>
            /// The request was accepted, and is currently being processed
            /// </summary>
			ACCEPTED = 202,
            /// <summary>
            /// The request was accepted, and the server is not returning content
            /// </summary>
			NOCONTENT = 204,
            /// <summary>
            /// The request was accepted, the server is not returning content, and the client should reset its view
            /// </summary>
			RESETCONTENT = 205,
            /// <summary>
            /// The server is returning only a part of the resource
            /// </summary>
			PARTIALCONTENT = 206,
            /// <summary>
            /// The resource has moved permanently, and the client should redirect
            /// </summary>
			MOVEDPERMANENTLY = 301,
            /// <summary>
            /// The resource has not been modified since it was last accessed
            /// </summary>
			NOTMODIFIED = 304,
            /// <summary>
            /// The resource has moved temporarily, and the client should redirect
            /// </summary>
			TEMPORARYREDIRECT = 307,
            /// <summary>
            /// The server can not process the request due to a client error
            /// </summary>
			BADREQUEST = 400,
            /// <summary>
            /// Access to the resource is only available to authenticated users
            /// </summary>
			UNAUTHORISED = 401,
            /// <summary>
            /// Access to the resource is not permitted
            /// </summary>
			FORBIDDEN = 403,
            /// <summary>
            /// The resource could not be found
            /// </summary>
			NOTFOUND = 404,
            /// <summary>
            /// The requested HTTP method (e.g. GET, PUT, etc) is not supported by the resource
            /// </summary>
			METHODNOTALLOWED = 405,
            /// <summary>
            /// The server timed out while waiting for a request from the client
            /// </summary>
			REQUESTTIMEOUT = 408,
            /// <summary>
            /// An unexpected error has occured on the server
            /// </summary>
			SERVERERROR = 500,
            /// <summary>
            /// The resource has not yet been implemented
            /// </summary>
			NOTIMPLEMENTED = 501,
            /// <summary>
            /// The server has received an invalid response from an upstream server
            /// </summary>
			BADGATEWAY = 502,
            /// <summary>
            /// The service is currently unavailable
            /// </summary>
			UNAVAILABLE = 503,
            /// <summary>
            /// The server timed out while waiting for a response from an upstream server
            /// </summary>
			GATEWAYTIMEOUT = 504
		}

        /// <summary>
        /// The status code of the HTTP response
        /// </summary>
        private HTTPStatus statusCode = HTTPStatus.OK;

        /// <summary>
        /// The status code of the HTTP response
        /// </summary>
		public HTTPStatus StatusCode
        {
            protected set
            {
                if ((int)value >= 100 && (int)value < 1000)
                {
                    statusCode = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The HTTP response status code must be a 3 digit number");
                }
            }
            get
            {
                return statusCode;
            }
        }

        /// <summary>
        /// The mime type of the HTTP response
        /// </summary>
        public string MimeType { protected set; get; } = "application/octet-stream";

        /// <summary>
        /// Information about the received request
        /// </summary>
        public RequestInfo RequestInformation { set; get; }

        /// <summary>
        /// The IP address the request originated from
        /// </summary>
        public IPAddress SourceIP
        {
            get
            {
                return RequestInformation.SourceIP;
            }
        }

        /// <summary>
        /// Returns the HTTP response
        /// </summary>
        public abstract byte[] GetResponse();
    }
}