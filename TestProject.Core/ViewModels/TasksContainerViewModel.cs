using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class TasksContainerViewModel : BaseViewModel<Action>
    {
        #region Variables
        private readonly IMvxNavigationService _navigationService;
        private ILoginService _loginService;
        private IAPIService _aPIService;
        private bool _isNetChecking;
        #endregion

        #region Constructors
        public TasksContainerViewModel(IMvxNavigationService navigationService, ILoginService loginService, IAPIService aPIService)
        {
            _loginService = loginService;
            _navigationService = navigationService;
            _aPIService = aPIService;
            DoneListItemViewModel = Mvx.IoCConstruct<DoneListItemViewModel>();
            NotDoneListItemViewModel = Mvx.IoCConstruct<NotDoneListItemViewModel>();
            AboutViewModel1 = Mvx.IoCConstruct<AboutViewModel>();

            AboutViewModel1.OnLoggedInHandler = new Action(() =>
            {
                LogoutCommand.Execute();
            });

            ShowDoneListItemViewModelCommand = new MvxAsyncCommand<Action>(async (closeHandler) => await _navigationService.Navigate<DoneListItemViewModel, Action>(closeHandler));
            ShowNotDoneListItemViewModelCommand = new MvxAsyncCommand<Action>(async (closeHandler) => await _navigationService.Navigate<NotDoneListItemViewModel, Action>(closeHandler));
            ShowAboutViewModelCommand = new MvxAsyncCommand<Action>(async (closeHandler) => await _navigationService.Navigate<AboutViewModel, Action>(closeHandler));

            NetChecking();

            Connectivity.ConnectivityChanged += delegate { NetChecking(); };
        }
        #endregion

        #region Commands
        public IMvxCommand LogoutCommand
        {
            get
            {
                return new MvxAsyncCommand(LogOut);
            }
        }
        public IMvxAsyncCommand<Action> ShowDoneListItemViewModelCommand { get; private set; }
        public IMvxAsyncCommand<Action> ShowNotDoneListItemViewModelCommand { get; private set; }
        public IMvxAsyncCommand<Action> ShowAboutViewModelCommand { get; private set; }
        #endregion

        #region Properties
        public DoneListItemViewModel DoneListItemViewModel { get; set; }

        public NotDoneListItemViewModel NotDoneListItemViewModel { get; set; }

        public AboutViewModel AboutViewModel1 { get; set; }

        #endregion

        #region Methods
        private async Task LogOut()
        {

            _loginService.Logout();
            await _navigationService.Navigate<LoginViewModel>();
            await _navigationService.Close(this);
        }
        #endregion












    }
}
