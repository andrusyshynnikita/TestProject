﻿using System;
using System.IO;
using AudioToolbox;
using AVFoundation;
using Foundation;
using TestProject.Core.Interface;
using TestProject.Core.Models;

namespace TestProject.IOS.Services
{
    public class AudioService : IAudioService
    {
        private AVAudioRecorder _audioRecorder=null;
        private AVAudioPlayer _audioPlayer;
        private NSUrl _url;
        private NSError _error;
        string _initialpath = Path.Combine(System.Environment.
               GetFolderPath(System.Environment.
               SpecialFolder.Personal), "0" + TwitterUserId.Id_User + ".3gpp");

        public Action OnRecordHandler { get; set; }
        public Action OnPlaydHandler { get; set; }

        public AudioService()
        {
        }

        public bool CheckAudioFile(int id)
        {
            var path = Path.Combine(System.Environment.
                GetFolderPath(System.Environment.
                SpecialFolder.Personal), id.ToString() + TwitterUserId.Id_User + ".3gpp");
            var result = File.Exists(path);

            return result;
        }

        public void DeleteNullFile()
        {
            File.Delete(_initialpath);
        }

        public void PlayRecording(int id)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.Stop();
                _audioPlayer.Dispose();
            }

            if (File.Exists(_initialpath))
            {
                _url = NSUrl.FromFilename(_initialpath);
                _audioPlayer = AVAudioPlayer.FromUrl(_url, out _error);
                _audioPlayer.Play();
                _audioPlayer.FinishedPlaying += PlayCompletion;

            }

            else
            {
                var path = Path.Combine(System.Environment.
                GetFolderPath(System.Environment.
                SpecialFolder.Personal), id.ToString() + TwitterUserId.Id_User + ".3gpp");
                _url = NSUrl.FromFilename(path);
                _audioPlayer = AVAudioPlayer.FromUrl(_url, out _error);
                _audioPlayer.Play();
                _audioPlayer.FinishedPlaying += PlayCompletion;
            }
        }

        private void PlayCompletion(object sender, AVStatusEventArgs e)
        {
            _audioPlayer = null;
            OnPlaydHandler();
        }

        public void StopPlayRecording()
        {
            if ((_audioPlayer != null))
            {
                if (_audioPlayer.Playing)
                {
                    _audioPlayer.Stop();
                }

                _audioPlayer = null;
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

        public void StartRecording(int id)
        {
            if (PrepareAudioRecording())
            {
                _audioRecorder.Record();
            }
        }

        public void StopRecording()
        {
            _audioRecorder.Stop();
            var hdf1 = File.ReadAllBytes(_initialpath);
            OnRecordHandler();
        }

        bool PrepareAudioRecording()
        {

            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return false;
            }
            err = audioSession.SetActive(true);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return false;
            }

            NSObject[] values = new NSObject[]
            {
                NSNumber.FromFloat(44100.0f),
                NSNumber.FromInt32((int)AudioToolbox.AudioFormatType.MPEG4AAC),
                NSNumber.FromInt32(1),
                NSNumber.FromInt32((int)AVAudioQuality.High)
            };

            NSObject[] keys = new NSObject[]
            {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVEncoderAudioQualityKey
            };
          
           var settings = NSDictionary.FromObjectsAndKeys(values, keys);

            NSError error;
           var _url = NSUrl.FromFilename(_initialpath);
            _audioRecorder = AVAudioRecorder.Create(_url, new AudioSettings(settings), out error);
            if ((_audioRecorder == null) || (error != null))
            {
                Console.WriteLine(error);
                return false;
            }
           
            if (!_audioRecorder.PrepareToRecord())
            {
                _audioRecorder.Dispose();
                _audioRecorder = null;
                return false;
            }

            _audioRecorder.FinishedRecording += delegate (object sender, AVStatusEventArgs e) {
                _audioRecorder.Dispose();
                _audioRecorder = null;
                Console.WriteLine("Done Recording (status: {0})", e.Status);
            };

            return true;
        }

    }

}