using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
        private HttpClient _client;
        private ITaskService _taskService;
        private MediaFile _mediaFile;
        private WebClient _webClient;
        public Action OnRefresDonehDataHandler { get; set; }
        public Action OnRefresNotDonehDataHandler { get; set; }

        public APIService(ITaskService taskService)
        {
            _client = new HttpClient();
            _webClient = new WebClient();
            _taskService = taskService;
        }

        public async Task<bool> RefreshDataAsync()
        {
            try
            {
                var uri = new Uri(string.Format("http://10.10.3.221:58778/api/tasks/" + TwitterUserId.Id_User));

                var response = await _client.GetAsync(uri);

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
            var uri = new Uri(string.Format("http://10.10.3.221:58778/api/tasks/"));
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                if (item.Id == 0)
                {
                    response = await _client.PostAsync(uri, content);
                }

                else
                {
                    response = await _client.PutAsync(uri, content);
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
            var uri = new Uri(string.Format("http://10.10.3.221:58778/api/tasks/" + id));

            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                _taskService.DeleteTask(id);
            }
        }

        public async void UpLoadAudioFile()
        {

            var path = Path.Combine(System.Environment.
                 GetFolderPath(System.Environment.
                 SpecialFolder.Personal), "0" + TwitterUserId.Id_User + ".3gpp");
           byte [] array= File.ReadAllBytes(path);

            ByteArrayContent baContent = new ByteArrayContent(array);
            var content = new MultipartFormDataContent();
            

            content.Add(baContent,
                "\"file\"",
                $"\"{path}\"");

            var httpClient = new HttpClient();

            var uploadServiceBaseAddress = "http://10.10.3.221:58778/api/Files/Upload";
            //"http://localhost:12214/api/Files/Upload";

            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
        }
    }
}

