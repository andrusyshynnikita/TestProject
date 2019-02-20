using System;
using System.IO;
using Android.Content;
using Android.Media;
using TestProject.Core.Helper;
using TestProject.Core.Interface;
using TestProject.Core.Models;

namespace TestProject.Droid.Services
{
    public class AudioService : IAudioService
    {

        private MediaRecorder _recorder;
        private MediaPlayer _player;

        public Action OnPlaydStatusHandler { get; set; }

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
                if (File.Exists(Constants.INITIAL_AUDIO_FILE_PATH))
                    File.Delete(Constants.INITIAL_AUDIO_FILE_PATH);

                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.Mpeg4);
                _recorder.SetAudioEncoder(AudioEncoder.Aac);
                _recorder.SetOutputFile(Constants.INITIAL_AUDIO_FILE_PATH);
                _recorder.Prepare();
                _recorder.Start();
            }

            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public void StopRecording()
        {
            try
            {
                _recorder.Stop();
                _recorder.Reset();
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

                if (File.Exists(Constants.INITIAL_AUDIO_FILE_PATH))
                {
                    Java.IO.File file1 = new Java.IO.File(Constants.INITIAL_AUDIO_FILE_PATH);
                    Java.IO.FileInputStream fis1 = new Java.IO.FileInputStream(file1);
                    await _player.SetDataSourceAsync(fis1.FD);
                    _player.Prepare();
                    _player.Start();
                    _player.Completion += PlayCompletion;
                }

                if(path!= null)
                {
                    await _player.SetDataSourceAsync(path);
                    _player.Prepare();
                    _player.Start();
                    _player.Completion += PlayCompletion;
                }
            }

            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
                throw;
            }

        }

        private void PlayCompletion(object sender, EventArgs e)
        {
            OnPlaydStatusHandler();
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

        public void DeleteInitialFile()
        {
            File.Delete(Constants.INITIAL_AUDIO_FILE_PATH);
        }
    }
}