using Android.OS;
using Android.Widget;
using System;
using TestProject.Core.ViewModels;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Runtime;

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.login_frame, false)]
    [Register("TestProject.droid.views.LoginView")]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        #region Variables
        private Button _twitter_button;
        private Button _logout_button;
        public Action OnLoggedInHandler;
        #endregion

        #region LifeCycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _twitter_button = view.FindViewById<Button>(Resource.Id.twitterButton);
            _twitter_button.Click += LoginTwitter;
            return view;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _twitter_button.Click -= LoginTwitter;
        }
        #endregion

        #region Properties
        protected override int FragmentId => Resource.Layout.LoginLayout;
        #endregion

        #region Methods
        private void LoginTwitter(object sender, EventArgs e)
        {
            ViewModel.LoginCommand.Execute();
            StartActivity(ViewModel.Authenticator.GetUI(View.Context));
        }
        #endregion







        
    }
}