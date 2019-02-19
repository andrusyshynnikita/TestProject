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
        void PlayRecording(string path); 
        void StopPlayRecording();
        void DeleteInitialFile();
        Action OnPlaydStatusHandler { get; set; }
    }
}
