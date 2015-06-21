using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebServer
{
    [Serializable]
    public class HTTPErrorException : Exception
    {
        public HTTPResponse.HTTPStatus ErrorCode { get; set; } = HTTPResponse.HTTPStatus.SERVERERROR;

        public HTTPErrorException() { }

        public HTTPErrorException(string message): base(message) { }

        public HTTPErrorException(string message, Exception innerException): base(message, innerException) { }

        public HTTPErrorException(HTTPResponse.HTTPStatus errorCode): base("A HTTP error has occured")
        {
            ErrorCode = errorCode;
        }

        public HTTPErrorException(int errorCode) : base("A HTTP error has occured")
        {
            ErrorCode = (HTTPResponse.HTTPStatus)errorCode;
        }

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
