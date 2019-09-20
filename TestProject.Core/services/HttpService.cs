using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Helper;
using TestProject.Core.Interface;
using TestProject.Core.Models;

namespace TestProject.Core.services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        public HttpService()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(20) };
        }

        public async Task<RequestResponse> SendHTTPRequest(string urlString, string method = HTTP.Get, Dictionary<string, string> headers = null, object body = null, string bodyType = "application/json")
        {
            var response = new RequestResponse { };

            using (var request = new HttpRequestMessage(new HttpMethod(method), new Uri(urlString)))
            {
                // Add headers
                if (headers != null)
                {

                    foreach (var key in headers.Keys)
                    {
                        string value = string.Empty;
                        headers.TryGetValue(key, out value);
                        request.Headers.Add(key, value);
                    }
                }
                if (body != null)
                {
                    if (body is HttpContent httpBody)
                    {
                        request.Content = httpBody;
                    }
                    if (body is string stringBody)
                    {
                        request.Content = new StringContent(stringBody, System.Text.Encoding.UTF8, bodyType);
                    }
                    if (body is Dictionary<string, string> formBody)
                    {
                        request.Content = new FormUrlEncodedContent(formBody);
                    }
                }
                try
                {
                    var httpResponse = await _httpClient.SendAsync(request); // throws if the network is unavilable or it can't connect to the server

                    response.StatusCode = httpResponse.StatusCode;

                    response.ResponseString = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                catch (System.Net.WebException exe) // throws if the network is unavailable
                {
                    response.IsNetworkError = true;
                }
                catch (TaskCanceledException exe)
                {
                    response.IsNetworkError = true;
                }
                catch (HttpRequestException exe)
                {
                    response.IsNetworkError = true;
                }
                catch (Exception exe)
                {
                    response.IsNetworkError = true;
                }
            }

            return response;
        }
    }
}
