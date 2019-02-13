using System.Collections.Generic;
using TestProject.Core.Models;

namespace TestProject.Core.Interface
{
    public interface ITaskService
    {
        List<TaskInfo> GetAllDoneUserTasks(string twitterUserId);
        List<TaskInfo> GetAllNotDoneUserTasks(string twitterUserId);
        void DeleteTask(int id);
        void InsertTask(TaskInfo taskInfo);
        void DeleteUserAllTask(string user_id);
        TaskInfo CurrentTask(int id);
        void InsertAllUserTasks(List<TaskInfo> usertasks);

    }
}
