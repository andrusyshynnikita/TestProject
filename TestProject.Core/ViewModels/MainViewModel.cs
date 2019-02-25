using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        #region Variables
        private IMvxNavigationService _mvxNavigationService;
        private ILoginService _loginService;
        #endregion

        #region Constructors
        public MainViewModel(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            ShowCurrentViewModelCommand = new MvxAsyncCommand(ShowCurrentViewModel);
        }
        #endregion

        #region LifeCycle

        #endregion

        #region Commands
        public IMvxAsyncCommand ShowCurrentViewModelCommand { get; private set; }
        #endregion

        #region Methods
        private async Task ShowCurrentViewModel()
        {

            if (CrossSettings.Current.Contains("Twitter") == true)
            {
                _mvxNavigationService.Navigate<TasksContainerViewModel>();
            }

            if (CrossSettings.Current.Contains("Twitter") == false)
            {
                _mvxNavigationService.Navigate<LoginViewModel>();
            }


        }
        #endregion





        

        
    }
}
