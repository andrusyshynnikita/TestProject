using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Helper;
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
        private byte[] _byteArrayAudio;

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
           

            var content = new MultipartFormDataContent();

            var bl = File.Exists(Constants.INITIAL_AUDIO_FILE_PATH);

            if (bl == true)
            {
                var fileName =  Guid.NewGuid() + ".3gpp";

                item.AudioFilePath = fileName;

                _byteArrayAudio = File.ReadAllBytes(Constants.INITIAL_AUDIO_FILE_PATH);
                ByteArrayContent baContent = new ByteArrayContent(_byteArrayAudio);

                content.Add(baContent,
                "\"file\"",
                $"\"{fileName}\"");
            }

            var uri = new Uri(string.Format("http://10.10.3.221:58778/api/Files/Upload"));

            var json = JsonConvert.SerializeObject(item);

            content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "\"TaskModel\"");

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
                }
            }

            catch (WebException e)
            {
                throw new WebException(e.Message);
            }

        }

        public async Task DeleteTaskAsync(TaskInfo item)
        {
            if (File.Exists(Constants.INITIAL_AUDIO_FILE_PATH) == true)
            {
                File.Delete(Constants.INITIAL_AUDIO_FILE_PATH);
            }
            var json = JsonConvert.SerializeObject(item);
            var uri = new Uri(string.Format("http://10.10.3.221:58778/api/tasks/" + item.Id));
            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                _taskService.DeleteTask(item.Id);

            }
        }

        public async Task DownloadAudioFile(int id, string path)
        {
            var uri = new Uri("http://10.10.3.221:58778/api/DownloadFile/" + id);

            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes(Constants.AUDIO_FILE_PATH(path), content);

            }
        }

    }
}

