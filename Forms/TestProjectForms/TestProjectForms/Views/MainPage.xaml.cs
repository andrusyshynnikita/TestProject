using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TestProject.Core.ViewModels;

namespace TestProjectForms.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class MainPage : MvxMasterDetailPage<MainViewModel>
    {
        private bool _firstTime = true;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (_firstTime)
            {
                ViewModel.ShowCurrentViewModelCommand.Execute(null);

                _firstTime = false;
            }

            base.OnAppearing();
        }
    }
}