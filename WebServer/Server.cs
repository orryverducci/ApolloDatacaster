using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer
{
    public class Server : IDisposable
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the server returned in the server headers
        /// </summary>
        public string ServerName { get; set; }
        #endregion

        #region Private Fields
        /// <summary>
        /// The HttpListener for the server
        /// </summary>
        private HttpListener listener = new HttpListener();

        /// <summary>
        /// The thread for the http response listener
        /// </summary>
        private Thread listenerThread;

        /// <summary>
        /// A response to a HTTP request
        /// </summary>
        private class WebResponse
        {
            /// <summary>
            /// The web response
            /// </summary>
            public HTTPResponse Response { get; set; }
            /// <summary>
            /// The path to the response
            /// </summary>
            public string Path { get; set; }
        }

        /// <summary>
        /// List of the registered responses to HTTP requests
        /// </summary>
        private List<WebResponse> registeredResponses = new List<WebResponse>();
        #endregion

        #region Constructor and dispose
        /// <summary>
        /// Starts the web server on the default HTTP port
        /// </summary>
        public Server() : this(80) { }

        /// <summary>
        /// Starts the web server
        /// </summary>
        /// <param name="port">The port to listen on</param>
        public Server(int port)
        {
            // Setup server
            listener.Prefixes.Add("http://*:" + port.ToString() + "/");
            // Start server
            try
            {
                listener.Start();
                // Listen for requests on a new thread
                listenerThread = new Thread(new ThreadStart(RequestListener));
                listenerThread.Start();
            }
            catch (HttpListenerException e)
            {
                switch (e.ErrorCode)
                {
                    case 5:
                        throw new InvalidOperationException("Administrative rights are required to start the server", e);
                    case 20:
                    case 183:
                        throw new InvalidOperationException("The requested port is already in use by another application", e);
                    default:
                        throw new InvalidOperationException(e.Message, e);
                } 
            }
        }

        /// <summary>
        /// Stops the web server
        /// </summary>
        public void Dispose()
        {
            listenerThread.Abort();
            listener.Stop();
        }
        #endregion

        #region Response Registration
        /// <summary>
        /// Registers the home page response for the web server
        /// </summary>
        /// <param name="response">The response to serve</param>
        public void RegisterResponse(HTTPResponse response)
        {
            RegisterResponse("", response);
        }

        /// <summary>
        /// Register a response for the web server
        /// </summary>
        /// <param name="path">The path of the response</param>
        /// <param name="response">The response to serve</param>
        public void RegisterResponse(string path, HTTPResponse response)
        {
            // 
            // Check if exists or for valid url characters
            // 
        }

        /// <summary>
        /// Unregister the homepage response for the server
        /// </summary>
        public void UnregisterResponse()
        {
            UnregisterResponse("");
        }

        /// <summary>
        /// Unregister a response for the web server
        /// </summary>
        /// <param name="path"></param>
        public void UnregisterResponse(string path)
        {

        }
        #endregion

        #region Request Handlers
        /// <summary>
        /// Listens for requests from the HTTP service
        /// </summary>
        private void RequestListener()
        {
            try
            {
                while (listener.IsListening) // As long as the server is active and listening for connections
                {
                    // Process request, when one is received, on a new thread
                    try
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(RequestHandler), listener.GetContext());
                    }
                    catch (HttpListenerException) { } // Suppress any exceptions
                }
            }
            catch (Exception) { } // Suppress any exceptions
        }

        /// <summary>
        /// Handles requests from clients
        /// </summary>
        /// <param name="context">The HTTPListenerContext for the request</param>
        private void RequestHandler(object context)
        {
            // Get the listener context
            HttpListenerContext listenerContext = (HttpListenerContext)context;
            // Serve requested content
            try
            {
                // Determine requested response
                string path;
                if (listenerContext.Request.Url.Segments.Length > 1 && listenerContext.Request.Url.Segments[1].EndsWith("/"))
                {
                    path = listenerContext.Request.Url.Segments[1].Substring(0, listenerContext.Request.Url.Segments[1].Length - 1);
                }
                else if (listenerContext.Request.Url.Segments.Length > 1)
                {
                    path = listenerContext.Request.Url.Segments[1];
                }
                else
                {
                    path = String.Empty;
                }
                // Select response
                HTTPResponse response;
                WebResponse matchingResponse = registeredResponses.Find(item => item.Path == path);
                if (matchingResponse != null)
                {
                    response = matchingResponse.Response;
                }
                else
                {
					response = new ErrorResponse(HTTPResponse.HTTPStatus.NOTFOUND);
                }
                // Generate response
                byte[] responseContent;
                response.ListenerRequest = listenerContext.Request;
                try {
                    responseContent = response.GetResponse();
                }
                catch (Exception e)
                {
                    if (e is HTTPErrorException)
                    {
                        response = new ErrorResponse(((HTTPErrorException)e).ErrorCode, e);
                    }
                    else
                    {
						response = new ErrorResponse(HTTPResponse.HTTPStatus.SERVERERROR, e);
                    }
                    response.ListenerRequest = listenerContext.Request;
                    responseContent = response.GetResponse();
                }
                // Send response
                listenerContext.Response.ContentType = response.MimeType;
                listenerContext.Response.StatusCode = (int)response.StatusCode;
                listenerContext.Response.ContentLength64 = responseContent.Length;
                if (ServerName != String.Empty)
                {
                    listenerContext.Response.Headers.Add(HttpResponseHeader.Server, ServerName);
                }
                listenerContext.Response.OutputStream.Write(responseContent, 0, responseContent.Length);
            }
            finally // Close the stream after processing
            {
                listenerContext.Response.OutputStream.Close();
            }
        }
        #endregion
    }
}
