using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebServer
{
    [Serializable]
    public class HTTPErrorException : Exception
    {
        public HTTPResponse.HTTPStatus ErrorCode { get; set; } = HTTPResponse.HTTPStatus.SERVERERROR;

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTPErrorException"/> class
        /// </summary>
        public HTTPErrorException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTPErrorException"/> class with an error message to be returned
        /// </summary>
        /// <param name="message">The error message to be returned</param>
        public HTTPErrorException(string message): base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTPErrorException"/> class with an error message to be returned, and an inner exception
        /// </summary>
        /// <param name="message">The error message to be returned</param>
        /// <param name="innerException">The inner exception to be returned</param>
        public HTTPErrorException(string message, Exception innerException): base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTPErrorException"/> class with the HTTP status code to be returned
        /// </summary>
        /// <param name="errorCode">The HTTP status code to be returned</param>
        public HTTPErrorException(HTTPResponse.HTTPStatus errorCode): base("A HTTP error has occured")
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTPErrorException"/> class with the HTTP status code to be returned
        /// </summary>
        /// <param name="errorCode">The HTTP status code to be returned</param>
        public HTTPErrorException(int errorCode) : base("A HTTP error has occured")
        {
            ErrorCode = (HTTPResponse.HTTPStatus)errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTPErrorException"/> class with the HTTP status code and the inner exception to be returned
        /// </summary>
        /// <param name="errorCode">The HTTP status code to be returned</param>
        /// <param name="innerException">The inner exception to be returned</param>
        public HTTPErrorException(HTTPResponse.HTTPStatus errorCode, Exception innerException) : base("A HTTP error has occured", innerException)
        {
            ErrorCode = errorCode;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected HTTPErrorException(SerializationInfo info, StreamingContext context): base(info, context)
        {
            if (info != null)
            {
                ErrorCode = (HTTPResponse.HTTPStatus)info.GetValue("ErrorCode", typeof(HTTPResponse.HTTPStatus));
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info != null)
            {
                info.AddValue("ErrorCode", ErrorCode, typeof(HTTPResponse.HTTPStatus));
            }
        }
    }
}
