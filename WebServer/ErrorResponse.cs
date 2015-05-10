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
            NOTFOUND
        }

        public int StatusCode { private set; get; }

        public byte[] Content { private set; get; }

        public string MimeType { private set; get; }

        private ErrorType error;

        /// <summary>
        /// Returns an error to the user
        /// </summary>
        /// <param name="errorType"></param>
        public ErrorResponse(ErrorType errorType)
        {
            error = errorType;
        }

        public void GetResponse()
        {
            MimeType = "text/html";
            switch (error)
            {
                case ErrorType.NOTFOUND:
                    Content = Encoding.UTF8.GetBytes("404 - Page Not Found");
                    StatusCode = 404;
                    break;
            }
        }
    }
}
