using MvvmCross.Commands;
using System;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class AboutViewModel : BaseViewModel<Action>
    {
        #region Variables
        private bool _isNetChecking;
        #endregion

        #region Constructors
        public AboutViewModel()
        {
            NetChecking();

            Connectivity.ConnectivityChanged += delegate { NetChecking(); };
        }
        #endregion

        #region LifeCycle
        public override void Prepare(Action parameter)
        {
            OnLogOutHandler = parameter;
        }
        #endregion

        #region Commands
        public IMvxCommand LogoutCommand => new MvxCommand(LogOut);
        #endregion

        #region Properties
        public Action OnLogOutHandler
        {
            get; set;
        }
        public static Action OnLoggedInHandlerIOS
        {
            get; set;
        }
        #endregion

        #region Methods
        private void LogOut()
        {
            OnLogOutHandler();
        }
        #endregion
    }
}
