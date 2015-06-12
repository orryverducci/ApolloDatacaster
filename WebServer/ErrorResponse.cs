using System.IO;
using System.Text;
using System.Reflection;

namespace WebServer
{
    class ErrorResponse : HTTPResponse
    {
        /// <summary>
        /// Returns an error to the user
        /// </summary>
        /// <param name="errorNumber">The type of error to be returned to the user</param>
        public ErrorResponse(int errorNumber)
        {
            // Set HTTP status code
            StatusCode = errorNumber;
            // Set HTTP Mime Type to HTML
            MimeType = "text/html";
        }

        public override byte[] GetResponse()
        {
            // Set error message
            string errorMessage;
            string errorDescription;
            switch (StatusCode)
            {
                case 400:
                    errorMessage = "Error 400 - Bad Request";
                    errorDescription = "The request to the server is not valid.";
                    break;
                case 401:
                    errorMessage = "Error 401 - Unauthorised";
                    errorDescription = "The page you are trying to access is available to only authenticated users.";
                    break;
                case 403:
                    errorMessage = "Error 403 - Forbidden";
                    errorDescription = "You are not authorised to access this page.";
                    break;
                case 404:
                    errorMessage = "Error 404 - Page Not Found";
                    errorDescription = "The page you are looking for could not be found. Either the web address you have entered is incorrect, or the page you are trying to access no longer exists.";
                    break;
                case 405:
                    errorMessage = "Error 405 - Method Not Available";
                    errorDescription = "The requested method is not available for this page.";
                    errorMessage = "Error 405 - Method Not Allowed";
                    errorDescription = "The requested method is not allowed for this page.";
                    break;
                case 500:
                    errorMessage = "Error 500 - Internal Server Error";
                    errorDescription = "An error has occured on the server while trying to access this page. Please try again later.";
                    break;
                case 501:
                    errorMessage = "Error 501 - Not Implemented";
                    errorDescription = "The service you are trying to access has not yet been implemented.";
                    break;
                case 502:
                    errorMessage = "Error 502 - Bad Gateway";
                    errorDescription = "This server received an invalid response when trying to retrieve the requested page.";
                    break;
                case 503:
                    errorMessage = "Error 503 - Service Unavailable";
                    errorDescription = "This service is currently unavailable. Please try again later.";
                    break;
                case 504:
                    errorMessage = "Error 504 - Gateway Timeout";
                    errorDescription = "This server did not receive a response within a reasonable timeframe when trying to retrieve the requested page.";
                    break;
                default:
                    errorMessage = "Unknown Error";
                    errorDescription = "An unknown error has occured.";
                    break;
            }
            // Retrieve error page
            string errorPageHTML;
            Stream errorPageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebServer.error.html");
            StreamReader errorStreamReader = new StreamReader(errorPageStream);
            errorPageHTML = errorStreamReader.ReadToEnd();
            // Replace placeholders
            errorPageHTML = errorPageHTML.Replace("[ERRORMESSAGE]", errorMessage);
            errorPageHTML = errorPageHTML.Replace("[ERRORDESCRIPTION]", errorDescription);
            int exceptionOpenLocation = errorPageHTML.IndexOf("[EXCEPTION]");
            int exceptionCloseLocation = errorPageHTML.IndexOf("[/EXCEPTION]");
            errorPageHTML = errorPageHTML.Remove(exceptionOpenLocation, exceptionCloseLocation - exceptionOpenLocation + 12);
            // Return error page
            return Encoding.UTF8.GetBytes(errorPageHTML);
        }
    }
}
