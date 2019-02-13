using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;

namespace TestProject.Core.Interface
{
   public interface IAPIService
    {
        Task<bool> RefreshDataAsync();
        Task InsertOrUpdateTaskAsync(TaskInfo item);
        Action OnRefresDonehDataHandler { get; set; }
        Action OnRefresNotDonehDataHandler { get; set; }
        Task DeleteTaskAsync(int id);

    }
}
