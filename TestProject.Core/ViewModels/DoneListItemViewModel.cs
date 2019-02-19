using TestProject.Core.Models;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Core.Interface;
using System.Threading.Tasks;
using System;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class DoneListItemViewModel : MvxViewModel<Action>
    {
        private readonly IMvxNavigationService _navigationService;
        private MvxObservableCollection<TaskInfo> _taskCollection;
        private ITaskService _taskService;
        private MvxCommand _refreshCommand;
        private bool _isRefreshing;
        private ILoginService _loginService;
        private IAPIService _apiService;
        private bool _isNetChecking;


        public DoneListItemViewModel(IMvxNavigationService mvxNavigationService, ITaskService taskService, ILoginService loginService, IAPIService aPIService)
        {
            _apiService = aPIService;
            _navigationService = mvxNavigationService;
            _loginService = loginService;
            _taskService = taskService;

            ShowSecondPageCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ItemViewModel>());
            TaskViewCommand = new MvxAsyncCommand<TaskInfo>(NavigateMethod);

            _apiService.OnRefresDonehDataHandler = new Action(() =>
             {
                 var items = _taskService.GetAllDoneUserTasks(TwitterUserId.Id_User);
                 TaskCollection = new MvxObservableCollection<TaskInfo>(items);
                 IsRefreshing = false;
             });

             NetCheck();

            Connectivity.ConnectivityChanged += delegate { NetCheck(); };

        }

        public IMvxCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(DoRefresh);

        private void DoRefresh()
        {
            IsRefreshing = true;
            _apiService.RefreshDataAsync();
        }

        public IMvxCommand ShowSecondPageCommand { get; set; }

        public IMvxCommand<TaskInfo> TaskViewCommand { get; set; }

        public IMvxCommand LogoutCommand
        {
            get
            {
                return new MvxAsyncCommand(LogOut);
            }
        }

        private async Task NavigateMethod(TaskInfo taskInfo)
        {
            try
            {
                var result = await _navigationService.Navigate<ItemViewModel, TaskInfo>(taskInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

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

        public override void ViewAppearing()
        {

            if (IsNetChecking)
            {
                _apiService.RefreshDataAsync();
            }

            if(!IsNetChecking)
            {
                var items = _taskService.GetAllDoneUserTasks(TwitterUserId.Id_User);
                TaskCollection = new MvxObservableCollection<TaskInfo>(items);
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

        private async Task LogOut()
        {
            _loginService.Logout();
            await _navigationService.Navigate<LoginViewModel>();
            await _navigationService.Close(this);
        }

        public override void Prepare(Action parameter)
        {
        }

        public bool IsNetChecking
        {
            get
            {
                return _isNetChecking;
            }
            set
            {
                _isNetChecking = value;
                RaisePropertyChanged(() => IsNetChecking);
            }
        }

        private void NetCheck()
        {
            var currentNetWork = Connectivity.NetworkAccess;

            if (currentNetWork == NetworkAccess.Internet)
            {
                IsNetChecking= true;
            }

            if(currentNetWork != NetworkAccess.Internet)
            {
                IsNetChecking = false;
            }
        }
    }

}

