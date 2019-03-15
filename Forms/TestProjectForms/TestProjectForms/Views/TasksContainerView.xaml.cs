using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProjectForms.Views
{
	[MvxTabbedPagePresentation(TabbedPosition.Root, WrapInNavigationPage =true)]
	public partial class TasksContainerView : MvxTabbedPage<TasksContainerViewModel>
    {
        private bool _firstTime = true;

        public TasksContainerView ()
		{
            InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            if (_firstTime)
            {
                ViewModel.ShowDoneListItemViewModelCommand.Execute(null);
                ViewModel.ShowNotDoneListItemViewModelCommand.Execute(null);
                ViewModel.ShowAboutViewModelCommand.Execute(null);
                _firstTime = false;
                
            }
            
            base.OnAppearing();
        }
    }
}