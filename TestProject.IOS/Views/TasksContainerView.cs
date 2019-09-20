using System;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using UIKit;

namespace TestProject.IOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public class TasksContainerView : MvxTabBarViewController<TasksContainerViewModel>
    {
        #region Variables
        private bool _firstTimePresented = true;
        #endregion

        #region LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var logOutHandler = new Action(() => ViewModel.LogoutCommand.Execute());

            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.ShowDoneListItemViewModelCommand.Execute(logOutHandler);
                ViewModel.ShowNotDoneListItemViewModelCommand.Execute(logOutHandler);
                ViewModel.ShowAboutViewModelCommand.Execute(logOutHandler);
                UITextAttributes txtAttributes = new UITextAttributes
                {
                    Font = UIFont.FromName("HelveticaNeue-Light", 16),

                };

                for (int i = 0; i < ChildViewControllers.Length; i++)
                {
                    var size2 = ChildViewControllers[i].TabBarController.TabBar.Frame.Size.Height / 2;
                    ChildViewControllers[i].TabBarItem.SetTitleTextAttributes(txtAttributes, UIControlState.Normal);
                    ChildViewControllers[i].TabBarItem.TitlePositionAdjustment = new UIOffset(0, -6);
                }
            }
        }
        #endregion
    }
}