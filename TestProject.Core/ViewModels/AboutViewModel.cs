using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class AboutViewModel : MvxViewModel<Action>
    {
        private bool _isNetChecking;

        public AboutViewModel()
        {
            NetCheck();

            Connectivity.ConnectivityChanged += delegate { NetCheck(); };
        }

        public IMvxCommand LogoutCommand => new MvxCommand(LogOut);

        private void LogOut()
        {
            OnLoggedInHandler();
        }

        public override void Prepare(Action parameter)
        {
            OnLoggedInHandler = parameter;
        }

        public Action OnLoggedInHandler
        {
            get; set;
        }

        public static Action OnLoggedInHandlerIOS
        {
            get; set;
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
