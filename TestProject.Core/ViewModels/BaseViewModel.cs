using MvvmCross.ViewModels;
using System;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class BaseViewModel<T> : MvxViewModel<T>
    {
        #region Variables
        private bool _isNetChecking;
        #endregion

        #region LifeCycle
        public override void Prepare(T parameter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
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
        #endregion

        #region Methods
        public void NetChecking()
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
        #endregion



    }
}
