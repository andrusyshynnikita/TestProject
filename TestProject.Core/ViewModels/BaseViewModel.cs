using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using TestProject.Core.Interface;
using Xamarin.Essentials;

namespace TestProject.Core.ViewModels
{
    public class BaseViewModel<T> : MvxViewModel<T>
    {
        #region Variables
        private bool _isNetChecking;
        protected readonly IMvxNavigationService _mvxNavigationService;
        #endregion

        #region Constructors
        public BaseViewModel(IMvxNavigationService mvxNavigationService) : this()
        {
            _mvxNavigationService = mvxNavigationService;
        }
        public BaseViewModel()
        {
            CheckCurrentConnectivity();

            Connectivity.ConnectivityChanged += delegate { CheckCurrentConnectivity(); };
        }


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

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }
        #endregion

        #region Methods
        public void CheckCurrentConnectivity()
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
