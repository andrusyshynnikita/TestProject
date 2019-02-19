using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using TestProject.Core.Models;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private IMvxNavigationService _mvxNavigationService;
        private ILoginService _loginService;
        private Account _userDate;
        private TwitterUser _twitterUser;
        private OAuth1Authenticator _auth;
        private bool _isNetChecking;


        public LoginViewModel(IMvxNavigationService mvxNavigationService, ILoginService loginService)
        {
            _loginService = loginService;

            _loginService.OnLoggedInHandler = new Action(() =>
            {
                ShowViewPager.Execute();
            });

            _mvxNavigationService = mvxNavigationService;

            NetCheck();

            Connectivity.ConnectivityChanged += delegate { NetCheck(); };
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public IMvxCommand LoginCommand => new MvxCommand(_loginService.LoginTwitter);
        
        public OAuth1Authenticator Authenticator
        {
            get
            {
                return _loginService.Authenticator();
            }
        }

        public IMvxAsyncCommand ShowViewPager
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await _mvxNavigationService.Navigate<ViewPagerViewModel>();
                    await _mvxNavigationService.Close(this);
                });
            }
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
                IsNetChecking = true;
            }

            if (currentNetWork != NetworkAccess.Internet)
            {
                IsNetChecking = false;
            }
        }

    }
}
