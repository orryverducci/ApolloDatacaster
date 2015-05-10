﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public interface IWebResponse
    {
        /// <summary>
        /// The status code of the HTTP response
        /// </summary>
        int StatusCode { get; }

        /// <summary>
        /// The content of the HTTP response
        /// </summary>
        byte[] Content { get; }

        /// <summary>
        /// The mime type of the HTTP response
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// If true, server responds with default error message. Message can be changed by setting StatusCode
        /// </summary>
        bool ReturnError { get; }

        /// <summary>
        /// Generates the HTTP response
        /// </summary>
        void GetResponse();
    }
}