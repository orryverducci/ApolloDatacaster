using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class TestResponse : IWebResponse
    {
        public int StatusCode { private set; get; }

        public byte[] Content { private set; get; }

        public string MimeType { private set; get; }

        public bool ReturnError {
            get
            {
                return false;
            }
        }

        public void GetResponse()
        {
            Content = Encoding.UTF8.GetBytes("** TEST **");
            MimeType = "text/html";
            StatusCode = 200;
        }
    }
}
