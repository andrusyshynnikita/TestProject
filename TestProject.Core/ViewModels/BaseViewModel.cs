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
        protected readonly ILoginService _loginService;
        protected readonly ITaskService _taskService;
        protected readonly IAudioService _audioService;
        protected readonly IAPIService _apiService;
        #endregion

        #region Constructors
        public BaseViewModel()
        {
            CheckCurrentConnectivity();

            Connectivity.ConnectivityChanged += delegate { CheckCurrentConnectivity(); };
        }

        public BaseViewModel(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;

            CheckCurrentConnectivity();

            Connectivity.ConnectivityChanged += delegate { CheckCurrentConnectivity(); };
        }

        public BaseViewModel(IMvxNavigationService mvxNavigationService,ILoginService loginService) : this(mvxNavigationService)
        {
            _loginService = loginService;
           
        }

        public BaseViewModel(IMvxNavigationService mvxNavigationService, ITaskService taskService, IAudioService audioService, IAPIService apiService) : this(mvxNavigationService)
        {
            _taskService = taskService;
            _audioService = audioService;
            _apiService = apiService;
        }

        public BaseViewModel(IMvxNavigationService mvxNavigationService, ITaskService taskService, ILoginService loginService, IAPIService aPIService) : this(mvxNavigationService,loginService)
        {
            _taskService = taskService;
            _apiService = aPIService;
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
