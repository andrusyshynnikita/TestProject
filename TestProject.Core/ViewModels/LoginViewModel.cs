using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel<Action>
    {
        #region Variables
        private IMvxNavigationService _mvxNavigationService;
        private ILoginService _loginService;
        #endregion

        #region Constructors
        public LoginViewModel(IMvxNavigationService mvxNavigationService, ILoginService loginService)
        {
            _loginService = loginService;

            _loginService.OnLoggedInHandler = new Action(() =>
            {
                ShowViewPager.Execute();
            });

            _mvxNavigationService = mvxNavigationService;

            NetChecking();

            Connectivity.ConnectivityChanged += delegate { NetChecking(); };
        }
        #endregion

        #region LifeCycle
        public override Task Initialize()
        {
            return base.Initialize();
        }
        #endregion

        #region Commands
        public IMvxCommand LoginCommand => new MvxCommand(_loginService.LoginTwitter);

        public IMvxAsyncCommand ShowViewPager
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await _mvxNavigationService.Navigate<TasksContainerViewModel>();
                    await _mvxNavigationService.Close(this);
                });
            }
        }
        #endregion

        #region Properties
        public OAuth1Authenticator Authenticator
        {
            get
            {
                return _loginService.Authenticator();
            }
        }
        #endregion













    }
}
