using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using TestProject.Core.Models;
using System;
using TestProject.Core.Helper;

namespace TestProject.Core.ViewModels
{
    public class ItemViewModel : MvxViewModel<TaskInfo>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _taskService;
        private readonly IAudioService _audioService;
        private readonly IAPIService _apiService;
        private int _id;
        private string _title;
        private string _description;
        private bool _status;
        private bool _titleEnableStatus;
        private bool _saveTaskEnable;
        private bool _deleteTaskEnable;
        private bool _recordcheck;
        private bool _playdcheck;
        private bool _playRecordEnable;
        private string _audio_File_Path;

        public override async Task Initialize()
        {
            await base.Initialize();

        }

        public ItemViewModel(IMvxNavigationService mvxNavigationService, ITaskService taskService, IAudioService audioService, IAPIService apiService)
        {
            _taskService = taskService;
            _navigationService = mvxNavigationService;
            _audioService = audioService;
            _apiService = apiService;
            IsPlayRecordingEnable = false;
            IsREcordChecking = true;
            IsPlayChecking = true;

            _audioService.OnPlaydHandler = new Action(() =>
            {
                IsPlayChecking = true;
            });

            _audioService.OnRecordHandler = new Action(() =>
            {
                IsPlayRecordingEnable = true;
            });
        }

        public IMvxAsyncCommand CloseCommand
        {
            get { return new MvxAsyncCommand(CloseTask); }
        }

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
                IsREcordChecking = true;
            }
        }

        private async Task CloseTask()
        {
            _audioService.DeleteNullFile();
            await _navigationService.Close(this);
        }

        private async void SaveTask()
        {
            TaskInfo taskInfo = new TaskInfo(Id, TwitterUserId.Id_User, Title, Description, Status, AudioFileName);
            if (Title != null)
            {
                await _apiService.InsertOrUpdateTaskAsync(taskInfo);
            }

            await _navigationService.Close(this);
        }

        private async void DeleteTask()
        {

            await _apiService.DeleteTaskAsync(new TaskInfo(Id, TwitterUserId.Id_User, Title, Description, Status, AudioFileName));

            await _navigationService.Close(this);
        }

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
            AudioFileName = _taskInfo.AudioFilePath;


            if (AudioFileName != null)
            {
                _apiService.DownloadAudioFile(Id, AudioFileName);
                IsPlayRecordingEnable = true;
            }
            else
            {
                IsPlayRecordingEnable = false;
            }

        }

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
                if (!string.IsNullOrEmpty(Title))
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

        public bool IsPlayRecordingEnable
        {
            get
            {
                return _playRecordEnable;
            }

            set
            {
                _playRecordEnable = value;
                RaisePropertyChanged(() => IsPlayRecordingEnable);
            }
        }

    }
}
