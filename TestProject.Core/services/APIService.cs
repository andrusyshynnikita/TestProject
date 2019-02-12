using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using TestProject.Core.Models;

namespace TestProject.Core.services
{
  public  class APIService: IAPIService
    {
        private HttpClient client;
        private ITaskService _taskService;

        public APIService(ITaskService taskService)
        {
            client = new HttpClient();
        }

        public async void RefreshDataAsync()
        {

            try
            {
                var uri = new Uri(string.Format("http://10.10.3.221:58778/api/values"));
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var _tasks = JsonConvert.DeserializeObject<List<TaskInfo>>(content);


                };
            }

            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                throw new TaskCanceledException(ex.Message);
            }

        }
    }
}

