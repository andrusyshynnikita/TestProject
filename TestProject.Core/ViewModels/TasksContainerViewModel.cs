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
    public class TasksContainerViewModel : BaseViewModel<object>
    {
        #region Variables
        private ILoginService _loginService;
        private IAPIService _aPIService;
        #endregion

        #region Constructors
        public TasksContainerViewModel(IMvxNavigationService mvxNavigationService, ILoginService loginService, IAPIService aPIService) : base(mvxNavigationService)
        {
            _loginService = loginService;
            _aPIService = aPIService;
            DoneListItemViewModel = Mvx.IoCProvider.IoCConstruct<DoneListItemViewModel>();
            NotDoneListItemViewModel = Mvx.IoCProvider.IoCConstruct<NotDoneListItemViewModel>();
            AboutViewModel1 = Mvx.IoCProvider.IoCConstruct<AboutViewModel>();

            AboutViewModel1.OnLogOutHandler = new Action(() =>
            {
                LogOut();
            });

            ShowDoneListItemViewModelCommand = new MvxAsyncCommand<Action>(async (closeHandler) => await _mvxNavigationService.Navigate<DoneListItemViewModel>());
            ShowNotDoneListItemViewModelCommand = new MvxAsyncCommand<Action>(async (closeHandler) => await _mvxNavigationService.Navigate<NotDoneListItemViewModel>());
            ShowAboutViewModelCommand = new MvxAsyncCommand<Action>(async (closeHandler) => await _mvxNavigationService.Navigate<AboutViewModel, Action>(closeHandler));            
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
            await _mvxNavigationService.Navigate<LoginViewModel>();
            await _mvxNavigationService.Close(this);
        }
        #endregion












    }
}
