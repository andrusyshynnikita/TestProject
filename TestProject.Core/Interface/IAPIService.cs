using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;

namespace TestProject.Core.Interface
{
   public interface IAPIService
    {
        Task RefreshDataAsync();
        Task InsertOrUpdateTaskAsync(TaskInfo item);
        Task DeleteTaskAsync(TaskInfo item);
        Task DownloadAudioFile(int id, string path);
    }
}
