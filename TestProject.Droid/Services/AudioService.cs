using System;
using System.IO;
using Android.Content;
using Android.Media;
using TestProject.Core.Interface;
using TestProject.Core.Models;

namespace TestProject.Droid.Services
{
    public class AudioService : IAudioService
    {

        private MediaRecorder _recorder;
        private MediaPlayer _player;
        private string path;
        private string _initialpath = Path.Combine(System.Environment.
               GetFolderPath(System.Environment.
               SpecialFolder.Personal), "0" + TwitterUserId.Id_User + ".mpeg4");



        public AudioService()
        {
            _recorder = new MediaRecorder();
            _player = new MediaPlayer();

            _player.Completion += (sender, e) =>
            {
                _player.Reset();
            };
        }

        public void StartRecording(int id)
        {
            try
            {
                if (File.Exists(_initialpath))
                    File.Delete(_initialpath);

                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.Mpeg4);
                _recorder.SetAudioEncoder(AudioEncoder.Aac);
                _recorder.SetOutputFile(_initialpath);
                _recorder.Prepare();
                _recorder.Start();
            }

            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void StopRecording()
        {
            try
            {
                _recorder.Stop();
                _recorder.Reset();
                OnRecordHandler();
            }

            catch (Exception e)
            {
                var str = e.Message;
            }
        }

        public async void PlayRecording(string path)
        {
            try
            {
                if (_player == null)
                {
                    _player = new MediaPlayer();
                }

                else
                {
                    _player.Reset();
                }

                if (File.Exists(_initialpath))
                {
                    Java.IO.File file1 = new Java.IO.File(_initialpath);
                    Java.IO.FileInputStream fis1 = new Java.IO.FileInputStream(file1);
                    await _player.SetDataSourceAsync(fis1.FD);
                    _player.Prepare();
                    _player.Start();
                    _player.Completion += PlayCompletion;
                }

                if(path!= null)
                {
                    //Java.IO.File file2 = new Java.IO.File(path);
                    //Java.IO.FileInputStream fis2 = new Java.IO.FileInputStream(file2);
                    //if(File.Exists(file2.ToString()))
                    await _player.SetDataSourceAsync(path);
                    _player.Prepare();
                    _player.Start();
                    _player.Completion += PlayCompletion;
                }

            }

            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }

        }

        private void PlayCompletion(object sender, EventArgs e)
        {
            OnPlaydHandler();
        }

        public void StopPlayRecording()
        {
            if ((_player != null))
            {
                if (_player.IsPlaying)
                {
                    _player.Stop();
                }
                _player.Release();
                _player = null;
            }
        }

        public void RenameFile(int id)
        {
            var path = Path.Combine(System.Environment.
                 GetFolderPath(System.Environment.
                 SpecialFolder.Personal), id.ToString() + TwitterUserId.Id_User + ".3gpp");
            if (File.Exists(_initialpath))
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    File.Move(_initialpath, path);
                    File.Delete(_initialpath);
                }
                else
                {
                    File.Move(_initialpath, path);
                    File.Delete(_initialpath);
                }
            }
        }

        public void DeleteNullFile()
        {

            File.Delete(_initialpath);
        }

        public bool CheckAudioFile(int id)
        {
            path = Path.Combine(System.Environment.
                GetFolderPath(System.Environment.
                SpecialFolder.Personal), id.ToString() + TwitterUserId.Id_User + ".3gpp");
            var result = File.Exists(path);

            return result;

        }

        public Action OnPlaydHandler
        {
            get; set;
        }

        public Action OnRecordHandler
        {
            get; set;
        }
    }
}