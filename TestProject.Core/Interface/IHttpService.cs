using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Helper;
using TestProject.Core.Models;

namespace TestProject.Core.Interface
{
    public interface IHttpService
    {
        Task<RequestResponse> SendHTTPRequest(string urlString, string method = HTTP.Get, Dictionary<string, string> headers = null, object body = null, string bodyType = "application/json");
    }
}
