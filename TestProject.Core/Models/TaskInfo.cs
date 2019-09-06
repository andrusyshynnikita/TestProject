using SQLite;

namespace TestProject.Core.Models
{
    public class TaskInfo

    {
        [PrimaryKey]
        public int Id { get; set; }
        public string User_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string AudioFileName { get; set; }
        public byte[] AudioFileContent { get; set; }

        public TaskInfo(int id, string user_id, string taskName, string taskDescription, bool taskStatus, string audioFilePath)
        {
            Id = id;
            User_Id = user_id;
            Title = taskName;
            Description = taskDescription;
            Status = taskStatus;
            AudioFileName = audioFilePath;
        }

        public TaskInfo()
        {

        }
    }
}
