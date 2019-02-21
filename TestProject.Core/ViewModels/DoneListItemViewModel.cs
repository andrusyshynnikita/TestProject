using TestProject.Core.Models;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Core.Interface;
using System.Threading.Tasks;
using System;
using Xamarin.Essentials;
using System.Collections.Generic;
using TestProject.Core.Helper;

namespace TestProject.Core.ViewModels
{
    public class DoneListItemViewModel : BaseViewModel<object>
    {
        #region Variables
        private MvxObservableCollection<TaskInfo> _taskCollection;
        private ITaskService _taskService;
        private MvxCommand _refreshCommand;
        private bool _isRefreshing;
        private ILoginService _loginService;
        private IAPIService _apiService;
        #endregion

        #region Constructors
        public DoneListItemViewModel(IMvxNavigationService mvxNavigationService, ITaskService taskService, ILoginService loginService, IAPIService aPIService): base(mvxNavigationService)
        {
            _apiService = aPIService;
            _loginService = loginService;
            _taskService = taskService;

            ShowSecondPageCommand = new MvxAsyncCommand(async () => await _mvxNavigationService.Navigate<ItemViewModel>());
            TaskViewCommand = new MvxAsyncCommand<TaskInfo>(TransferTaskInfo);
        }
        #endregion

        #region LifeCycle
        public override void ViewAppearing()
        {
            RefreshCurrentTasksData();
        }
        #endregion

        #region Commands
        public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(RefreshCurrentTasksData);

        public IMvxCommand ShowSecondPageCommand { get; set; }

        public IMvxCommand<TaskInfo> TaskViewCommand { get; set; }

        public IMvxCommand LogoutCommand
        {
            get
            {
                return new MvxAsyncCommand(LogOut);
            }
        }
        #endregion

        #region Properties
        public MvxObservableCollection<TaskInfo> TaskCollection
        {
            get
            {
                return _taskCollection;
            }
            set
            {
                _taskCollection = value;
                RaisePropertyChanged(() => TaskCollection);
            }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }
        #endregion

        #region Methods
        private async Task TransferTaskInfo(TaskInfo taskInfo)
        {
            try
            {
                var result = await _mvxNavigationService.Navigate<ItemViewModel, TaskInfo>(taskInfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void RefreshCurrentTasksData()
        {
            if (base.IsNetChecking == true)
            {
                IsRefreshing = true;
               await _apiService.RefreshDataAsync();
                UploadTasksData();
            }

            if (base.IsNetChecking == false)
            {
                IsRefreshing = true;
                UploadTasksData();
            }
        }

        private async Task LogOut()
        {
            _loginService.Logout();
            await _mvxNavigationService.Navigate<LoginViewModel>();
            await _mvxNavigationService.Close(this);
        }

        private void UploadTasksData()
        {
            var items = _taskService.GetAllDoneUserTasks(UserAccount.GetUserId());
            TaskCollection = new MvxObservableCollection<TaskInfo>(items);
            IsRefreshing = false;
        }
        #endregion
















    }

}

