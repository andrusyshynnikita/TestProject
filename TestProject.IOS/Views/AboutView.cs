using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using TestProject.Core.ViewModels;
using UIKit;

namespace TestProject.IOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = false, TabName = "About")]
    public partial class AboutView : MvxViewController<AboutViewModel>
    {

        #region Constructors
        public AboutView() : base(nameof(AboutView), null)
        {
        }
        #endregion

        #region LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;

            var set = this.CreateBindingSet<AboutView, AboutViewModel>();
            set.Bind(Logout_button).To(vm => vm.LogoutCommand);
            set.Bind(Logout_button).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("Visibility");
            set.Bind(netWork_label).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("ReverseVisibility");
            set.Apply();

            UIDevice.Notifications.ObserveOrientationDidChange(OrientationsHandler);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
        #endregion

        #region Methods
        private void OrientationsHandler(object sender, NSNotificationEventArgs e)
        {
            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;
        }
        #endregion







    }
}