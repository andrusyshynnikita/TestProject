using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Configuration.Interfaces;
using TestProject.Core.Helper;
using TestProject.Core.Interface;
using TestProject.Core.Models;

namespace TestProject.Core.services
{
    public class APIService : IAPIService
    {
        private readonly ITaskService _taskService;
        private readonly IHttpService _httpService;
        private readonly IAPIConfiguration _configuration;
        private readonly string _controllerPath;

        public APIService(ITaskService taskService, IHttpService httpService, IAPIConfiguration configuration)
        {
            _taskService = taskService;
            _httpService = httpService;
            _configuration = configuration;
            _controllerPath = "/api/tasks";
        }

        public async Task RefreshDataAsync()
        {
            string urlString = $"{_configuration.CloudHostUrl}{_controllerPath}/GetTasks/{UserAccount.GetUserId()}";

            try
            {
                var response = await _httpService.SendHTTPRequest(urlString);

                if (!response.IsNetworkError && response.StatusCode == HttpStatusCode.OK)
                {
                    var tasks = JsonConvert.DeserializeObject<List<TaskInfo>>(response.ResponseString);

                    _taskService.DeleteUserAllTask(UserAccount.GetUserId());
                    _taskService.InsertAllUserTasks(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task InsertOrUpdateTaskAsync(TaskInfo item)
        {

            bool initial_File = File.Exists(Constants.INITIAL_AUDIO_FILE_PATH);

            if (initial_File == true)
            {
                item.AudioFileContent = File.ReadAllBytes(Constants.INITIAL_AUDIO_FILE_PATH);

                File.Delete(Constants.INITIAL_AUDIO_FILE_PATH);
            }

            if (item.AudioFileName == null && initial_File == true)
            {
                var fileName = Guid.NewGuid() + ".m4a";

                item.AudioFileName = fileName;
            }

            string urlString = $"{_configuration.CloudHostUrl}{_controllerPath}/PostTask";

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            var body = JsonConvert.SerializeObject(item, Formatting.Indented, settings);

            RequestResponse response = await _httpService.SendHTTPRequest(urlString, HTTP.Post, null, body);

            if (!response.IsNetworkError && response.StatusCode == HttpStatusCode.OK)
            {
                _taskService.InsertTask(item);
            }
        }

        public async Task DeleteTaskAsync(TaskInfo item)
        {
            if (File.Exists(Constants.INITIAL_AUDIO_FILE_PATH))
            {
                File.Delete(Constants.INITIAL_AUDIO_FILE_PATH);
            }
            if (!string.IsNullOrEmpty(item?.AudioFileName) && File.Exists(Constants.AUDIO_FILE_PATH(item?.AudioFileName)))
            {
                File.Delete(Constants.AUDIO_FILE_PATH(item?.AudioFileName));
            }

            string urlString = $"{_configuration.CloudHostUrl}{_controllerPath}/DeleteTasks/{item.Id}";

            RequestResponse response = await _httpService.SendHTTPRequest(urlString, HTTP.Delete);

            if (!response.IsNetworkError && response.StatusCode == HttpStatusCode.OK)
            {
                _taskService.DeleteTask(item.Id);
            }
        }

        public async Task DownloadAudioFile(int id, string path)
        {
            string urlString = $"{_configuration.CloudHostUrl}{_controllerPath}/GetAudioFile/{id}";

            var response = await _httpService.SendHTTPRequest(urlString);

            if (!response.IsNetworkError && response.StatusCode == HttpStatusCode.OK)
            {
                var tasks = JsonConvert.DeserializeObject<TaskInfo>(response.ResponseString);

                await StorageHelper.WriteByteToFileAsync(Constants.AUDIO_FILE_PATH(path), tasks.AudioFileContent);
            }
        }

    }
}

