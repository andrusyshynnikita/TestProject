using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;

namespace TestProject.IOS.Views
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {

        #region Constructors
        public LoginView() : base(nameof(LoginView), null)
        {
        }
        #endregion

        #region LifeCycle
        public override void ViewDidLoad()
        {

            base.ViewDidLoad();

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(LoginTwitter).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("Visibility");
            set.Bind(netWork_label).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("ReverseVisibility");
            set.Apply();
        }
        #endregion

        #region Methods
        partial void LoginButton_TouchUpInside(UIKit.UIButton sender)
        {
            ViewModel.LoginCommand.Execute(null);
            var ui = ViewModel.Authenticator.GetUI();
            PresentViewController(ui, true, null);
        }
        #endregion





    }
}