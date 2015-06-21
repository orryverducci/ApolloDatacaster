using System;
using System.Runtime.Serialization;

namespace WebServer
{
    [Serializable]
    public class HTTPException : Exception
    {
        public HTTPResponse.HTTPStatus ErrorCode { get; set; }

        public HTTPException() { }

        public HTTPException(string message): base(message) { }

        public HTTPException(string message, Exception innerException): base(message, innerException) { }

        protected HTTPException(SerializationInfo info, StreamingContext context): base(info, context)
        {
            if (info != null)
            {
                ErrorCode = (HTTPResponse.HTTPStatus)info.GetInt32("ErrorCode");
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info != null)
            {
                info.AddValue("ErrorCode", (int)ErrorCode);
            }
        }
    }
}
