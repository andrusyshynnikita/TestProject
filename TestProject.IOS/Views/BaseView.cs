using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace TestProject.IOS.Views
{
  public  class BaseView<TViewModel> : MvxViewController where TViewModel : class, IMvxViewModel
    {
        #region Variebles
        protected UIBarButtonItem _btnAdd;
        protected UIBarButtonItem _btnBack;
        #endregion

        #region Constructors
        public new TViewModel ViewModel
        {
            get
            {
                return (TViewModel)base.ViewModel;
            }

            set
            {
                base.ViewModel = value;
            }
        }
        #endregion

        #region Methods
        protected void SetUpNavigationBar()
        {
            NavigationItem.Title = "TaskyDrop";

            _btnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_btnAdd, false);
            NavigationController.NavigationBar.BarTintColor = UIColor.Purple;
            NavigationController.NavigationBar.TintColor = UIColor.Black;
        }

        protected void SetUpCloseNavigationBar()
        {
            Title = "TaskyDrop";

            NavigationController.Toolbar.BackgroundColor = UIColor.Blue;
            NavigationController.NavigationBar.BarTintColor = UIColor.Purple;
            NavigationController.NavigationBar.TintColor = UIColor.Black;

            _btnBack = new UIBarButtonItem(UIBarButtonSystemItem.Reply, null);
            NavigationItem.SetLeftBarButtonItem(_btnBack, false);
        }
        #endregion
    }
}