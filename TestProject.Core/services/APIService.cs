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
        private byte[] _byteArrayAudio;
        private ByteArrayContent _baContent;
        private List<TaskInfo> _tasks;

        public APIService(ITaskService taskService)
        {
            _client = new HttpClient();
            _taskService = taskService;
        }

        public async Task RefreshDataAsync()
        {
            Uri uri = new Uri(string.Format("http://10.10.3.221:58778/api/tasks/" + UserAccount.GetUserId()));

            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _tasks = JsonConvert.DeserializeObject<List<TaskInfo>>(content);

                _taskService.DeleteUserAllTask(UserAccount.GetUserId());
                _taskService.InsertAllUserTasks(_tasks);

            }
        }

        public async Task InsertOrUpdateTaskAsync(TaskInfo item)
        {
            var content = new MultipartFormDataContent();

            bool initial_File = File.Exists(Constants.INITIAL_AUDIO_FILE_PATH);

            if (initial_File == true)
            {
                _byteArrayAudio = File.ReadAllBytes(Constants.INITIAL_AUDIO_FILE_PATH);

                _baContent = new ByteArrayContent(_byteArrayAudio);

                File.Delete(Constants.INITIAL_AUDIO_FILE_PATH);
            }

            if (item.AudioFilePath == null && initial_File == true)
            {
                var fileName = Guid.NewGuid() + ".m4a";

                item.AudioFilePath = fileName;

                content.Add(_baContent,
            "\"file\"",
            $"\"{fileName}\"");
            }

            if (item.AudioFilePath != null && initial_File == true)
            {
                content.Add(_baContent,
            "\"file\"",
            $"\"{item.AudioFilePath}\"");
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
            if (File.Exists(Constants.INITIAL_AUDIO_FILE_PATH))
            {
                File.Delete(Constants.INITIAL_AUDIO_FILE_PATH);
            }

            var json = JsonConvert.SerializeObject(item);

            Uri uri = new Uri(string.Format("http://10.10.3.221:58778/api/tasks/" + item.Id));

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

