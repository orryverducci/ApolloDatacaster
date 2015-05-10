using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class ErrorResponse : IWebResponse
    {
        /// <summary>
        /// The errors that can be returned
        /// </summary>
        public enum ErrorType
        {
            BADREQUEST,
            UNAUTHORISED,
            FORBIDDEN,
            NOTFOUND,
            METHODNOTALLOWED,
            REQUESTTIMEOUT,
            CONFLICT,
            SERVERERROR,
            NOTIMPLEMENTED,
            BADGATEWAY,
            UNAVAILABLE,
            GATEWAYTIMEOUT,
            NETWORKAUTHREQUIRED
        }

        public int StatusCode { private set; get; }

        public byte[] Content { private set; get; }

        public string MimeType { private set; get; }

        public bool ReturnError
        {
            get
            {
                return false;
            }

        }

        /// <summary>
        /// Returns an error to the user
        /// </summary>
        /// <param name="errorNumber">The type of error to be returned to the user</param>
        public ErrorResponse(int errorNumber)
        {
            StatusCode = errorNumber;
        }

        public void GetResponse()
        {
            MimeType = "text/html";
            switch (StatusCode)
            {
                case 400:
                    Content = Encoding.UTF8.GetBytes("400 - Bad Request");
                    break;
                case 401:
                    Content = Encoding.UTF8.GetBytes("401 - Unauthorised");
                    break;
                case 403:
                    Content = Encoding.UTF8.GetBytes("403 - Forbidden");
                    break;
                case 404:
                    Content = Encoding.UTF8.GetBytes("404 - Page Not Found");
                    break;
                case 405:
                    Content = Encoding.UTF8.GetBytes("400 - Method Not Allowed");
                    break;
                case 500:
                    Content = Encoding.UTF8.GetBytes("500 - Internal Server Error");
                    break;
                case 501:
                    Content = Encoding.UTF8.GetBytes("501 - Not Implemented");
                    break;
                case 502:
                    Content = Encoding.UTF8.GetBytes("502 - Bad Gateway");
                    break;
                case 503:
                    Content = Encoding.UTF8.GetBytes("500 - Service Unavailable");
                    break;
                case 504:
                    Content = Encoding.UTF8.GetBytes("504 - Gateway Timeout");
                    break;
                default:
                    Content = Encoding.UTF8.GetBytes("An unknown error occurred");
                    break;
            }
        }
    }
}
