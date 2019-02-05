using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using TestProject.Core.ViewModels;
using TestProject.IOS.Sources;
using UIKit;

namespace TestProject.IOS
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Not DoneTasks")]
    public partial class NotDoneListItemView : MvxViewController<NotDoneListItemViewModel>
    {
        private UIBarButtonItem _btnCAdd;
        private MvxUIRefreshControl _refreshControl;

        public NotDoneListItemView() : base(nameof(NotDoneListItemView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _refreshControl = new MvxUIRefreshControl();
            NotDoneTasksTableView.AddSubview(_refreshControl);

            _btnCAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_btnCAdd, false);

            var source = new TasksTableViewSource(NotDoneTasksTableView);
            NotDoneTasksTableView.Source = source;
            var set = this.CreateBindingSet<NotDoneListItemView, NotDoneListItemViewModel>();
            set.Bind(source).To(vm => vm.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskViewCommand);
            set.Bind(_btnCAdd).For("Clicked").To(vm => vm.ShowSecondPageCommand);
            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
            set.Apply();
            NotDoneTasksTableView.ReloadData();
        }
    }
}