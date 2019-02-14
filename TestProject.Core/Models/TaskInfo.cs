using System;
using System.Collections.Generic;
using System.Text;
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
       // public byte[] AudioFile { get; set; }

        public TaskInfo(int id,string user_id, string taskName, string taskDescription, bool taskStatus/*, byte[] audioFile*/)
        {
            Id = id;
            User_Id = user_id;
            Title = taskName;
            Description = taskDescription;
            Status = taskStatus;
            //AudioFile = audioFile;
        }

        public TaskInfo()
        {

        }
    }
}
