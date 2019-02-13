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
    public class APIService : IAPIService
    {
        private HttpClient client;
        private ITaskService _taskService;

        public Action OnRefresDonehDataHandler { get; set; }
        public Action OnRefresNotDonehDataHandler { get; set; }

        public APIService(ITaskService taskService)
        {
            client = new HttpClient();
            _taskService = taskService;
        }

        public async Task<bool> RefreshDataAsync()
        {
            try
            {
                var uri = new Uri(string.Format("http://10.10.3.221:58778/api/values/" + TwitterUserId.Id_User));

                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var _tasks = JsonConvert.DeserializeObject<List<TaskInfo>>(content);
                    _taskService.DeleteUserAllTask(TwitterUserId.Id_User);
                    _taskService.InsertAllUserTasks(_tasks);

                    OnRefresDonehDataHandler();
                    OnRefresNotDonehDataHandler();

                }

                return response.IsSuccessStatusCode;

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

        public async Task InsertOrUpdateTaskAsync(TaskInfo item)
        {
            var uri = new Uri(string.Format("http://10.10.3.221:58778/api/values/"));
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                if (item.Id == 0)
                {
                    response = await client.PostAsync(uri, content);
                }

                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    _taskService.InsertTask(item);
                    //OnRefresDonehDataHandler();
                }
            }

            catch (WebException e)
            {
                throw new WebException(e.Message);
            }

        }

        public async Task DeleteTaskAsync(int id)
        {
            var uri = new Uri(string.Format("http://10.10.3.221:58778/api/values/" + id));

            var response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                _taskService.DeleteTask(id);
            }
        }


    }
}

