using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using TestProject.Core.ViewModels;
using Xamarin.Forms;

namespace TestProjectForms.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = false, WrapInNavigationPage = false)]
    public partial class LoginPage : MvxContentPage<LoginViewModel>
    {
      //  NavigationPage navigationPage = new NavigationPage();

        public LoginPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Login.Clicked += LoginTwitter;
        }

        private void LoginTwitter(object sender, EventArgs e)
        {
            ViewModel.LoginCommand.Execute();
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(ViewModel.Authenticator);
        }
    }
}