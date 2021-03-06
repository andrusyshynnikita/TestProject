﻿using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using TestProject.Core.Models;
using System;
using TestProject.Core.Helper;
using Xamarin.Essentials;
using TestProject.Core.services;

namespace TestProject.Core.ViewModels
{
    public class ItemViewModel : BaseViewModel<TaskInfo>
    {
        #region Variables
        private readonly ITaskService _taskService;
        private readonly IAudioService _audioService;
        private readonly IAPIService _apiService;
        private int _id;
        private string _title;
        private string _description;
        private bool _status;
        private string _audio_File_Path;
        private bool _titleEnableStatus;
        private bool _saveTaskEnable;
        private bool _deleteTaskEnable;
        private bool _recordcheck;
        private bool _playdcheck;
        private bool _playRecordEnable;
        #endregion

        #region Constructors
        public ItemViewModel(IMvxNavigationService mvxNavigationService, ITaskService taskService, IAudioService audioService, IAPIService apiService) : base(mvxNavigationService)
        {
            _taskService = taskService;
            _audioService = audioService;
            _apiService = apiService;
            PermissionToPlay = false;
            IsREcordChecking = true;
            IsPlayChecking = true;

            _audioService.OnPlaydStatusHandler = new Action(() =>
            {
                IsPlayChecking = true;
            });
        }
        #endregion

        #region LifeCycle
        public override void Prepare()
        {
            base.Prepare();
        }

        public override void Prepare(TaskInfo _taskInfo)
        {
            Id = _taskInfo.Id;
            Title = _taskInfo.Title;
            Description = _taskInfo.Description;
            Status = _taskInfo.Status;
            AudioFileName = _taskInfo.AudioFileName;


            if (AudioFileName != null)
            {
                _apiService.DownloadAudioFile(Id, AudioFileName);
                PermissionToPlay = true;
            }
            else
            {
                PermissionToPlay = false;
            }

        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }
        #endregion

        #region Commands
        public IMvxAsyncCommand CloseCommand
        {
            get { return new MvxAsyncCommand(CloseTask); }
        }

        public IMvxCommand SaveCommand
        {
            get { return new MvxCommand(SaveTask); }

        }

        public IMvxCommand DeleteCommand
        {
            get { return new MvxCommand(DeleteTask); }
        }

        public IMvxCommand StartRecordingCommand
        {
            get { return new MvxCommand(StartRecording); }
        }

        public IMvxCommand PlayRecordingCommand
        {
            get { return new MvxCommand(PlayRecording); }
        }
        #endregion

        #region Properties
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
                RaisePropertyChanged(() => IsDeletingTaskEnable);
            }
        }

        public string Title
        {
            get => _title;

            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(() => IsSavingTaskEnable);
            }
        }

        public string Description
        {
            get => _description;

            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public bool Status
        {
            get => _status;

            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public string AudioFileName { get; set; }

        public bool IsTitleEnable
        {
            get
            {
                if (Id == 0)
                {
                    _titleEnableStatus = true;
                }
                else
                {
                    _titleEnableStatus = false;
                }
                return _titleEnableStatus;
            }

        }

        public bool IsSavingTaskEnable
        {
            get
            {
                if (!string.IsNullOrEmpty(Title.Trim()))
                {
                    _saveTaskEnable = true;
                }
                else
                {
                    _saveTaskEnable = false;
                }
                return _saveTaskEnable;
            }
        }

        public bool IsDeletingTaskEnable
        {
            get
            {
                if (_taskService.CurrentTask(Id) == null)
                {
                    _deleteTaskEnable = false;
                }
                else
                {
                    _deleteTaskEnable = true;
                }
                return _deleteTaskEnable;
            }
        }

        public bool IsREcordChecking
        {
            get => _recordcheck;

            set
            {
                _recordcheck = value;
                RaisePropertyChanged(() => IsREcordChecking);
            }
        }

        public bool IsPlayChecking
        {
            get => _playdcheck;

            set
            {
                _playdcheck = value;
                RaisePropertyChanged(() => IsPlayChecking);
            }
        }

        private bool _isSavingProcessing;
        public bool IsSavingProcessing
        {
            get => _isSavingProcessing;

            set => SetProperty(ref _isSavingProcessing, value);
        }
        public bool PermissionToPlay
        {
            get
            {
                return _playRecordEnable;
            }

            set
            {
                _playRecordEnable = value;
                RaisePropertyChanged(() => PermissionToPlay);
            }
        }
        #endregion

        #region Methods
        private void PlayRecording()
        {
            if (AudioFileName != null)
            {
                _audio_File_Path = Constants.AUDIO_FILE_PATH(AudioFileName);
            }

            if (IsPlayChecking == true)
            {
                IsPlayChecking = false;
                _audioService.PlayRecording(_audio_File_Path);

            }

            else
            {
                _audioService.StopPlayRecording();
                IsPlayChecking = true;
            }
        }

        private void StartRecording()
        {
            if (IsREcordChecking == true)
            {
                _audioService.StartRecording(Id);
                IsREcordChecking = false;
            }
            else
            {
                _audioService.StopRecording();
                PermissionToPlay = true;
                IsREcordChecking = true;
            }
        }

        private async Task CloseTask()
        {
            _audioService.DeleteInitialFile();
            await _mvxNavigationService.Close(this);
        }

        private async void SaveTask()
        {
            //var TEST = new ChatService();
            //var chatmes = new ChatMessage()
            //{
            //    Message = "heelo",
            //    Name = "test"

            //};
            //     await  TEST.JoinGroup("test");
            //  await  TEST.Send(chatmes, "heelo");

            TaskInfo taskInfo = new TaskInfo(Id, UserAccount.GetUserId(), Title.Trim(), Description, Status, AudioFileName);
            IsSavingProcessing = true;
            await _apiService.InsertOrUpdateTaskAsync(taskInfo);
            await _mvxNavigationService.Close(this);
            IsSavingProcessing = false;
        }

        private async void DeleteTask()
        {
            IsBusy = true;
            await _apiService.DeleteTaskAsync(new TaskInfo(Id, UserAccount.GetUserId(), Title, Description, Status, AudioFileName));
            IsBusy = false;
            await _mvxNavigationService.Close(this);
        }
        #endregion
    }
}
