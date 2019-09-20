using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TestProject.Core.Models
{
    public class RequestResponse
    {
        public bool IsNetworkError { get; set; } // If there was a network error this is true

        public HttpStatusCode StatusCode { get; set; } // HTTP status code (unless there is none, because of a network error)

        public string ResponseString { get; set; }     // Response string
    }
}
