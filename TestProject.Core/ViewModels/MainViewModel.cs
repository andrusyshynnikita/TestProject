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
    public class MainViewModel : BaseViewModel<object>
    {
        #region Variables
        #endregion

        #region Constructors
        public MainViewModel(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            ShowCurrentViewModelCommand = new MvxAsyncCommand(ShowCurrentViewModel);
        }
        #endregion


        #region Commands
        public IMvxAsyncCommand ShowCurrentViewModelCommand { get; private set; }
        #endregion

        #region Methods
        private async Task ShowCurrentViewModel()
        {

            if (CrossSettings.Current.Contains("Twitter") == true)
            {
                await _mvxNavigationService.Navigate<TasksContainerViewModel>();
            }

            if (CrossSettings.Current.Contains("Twitter") == false)
            {
                await _mvxNavigationService.Navigate<LoginViewModel>();
            }


        }
        #endregion





        

        
    }
}
