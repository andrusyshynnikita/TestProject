using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Interface
{
   public interface IAudioService
    {
        void StartRecording(int id);
        void StopRecording();
        void PlayRecording(string path); //android use return Task
        void StopPlayRecording();
        void RenameFile(int newName);
        bool CheckAudioFile(int id);
        void DeleteNullFile();
        Action OnRecordHandler { get; set; }
        Action OnPlaydHandler { get; set; }
    }
}
