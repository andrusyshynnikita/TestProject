using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using TestProject.IOS.Sources;
using UIKit;

namespace TestProject.IOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Not Done Tasks")]
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
            NavigationItem.Title = "TaskyDrop";
            _refreshControl = new MvxUIRefreshControl();
            NotDoneTasksTableView.AddSubview(_refreshControl);

            _btnCAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_btnCAdd, false);
            NavigationController.NavigationBar.BarTintColor = UIColor.Purple;
            NavigationController.NavigationBar.TintColor = UIColor.Black;

            UIDevice.Notifications.ObserveOrientationDidChange(OrientationsHandler);
            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;

            var source = new TasksTableViewSource(NotDoneTasksTableView);
            NotDoneTasksTableView.Source = source;
            var set = this.CreateBindingSet<NotDoneListItemView, NotDoneListItemViewModel>();
            set.Bind(source).To(vm => vm.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskViewCommand);
            set.Bind(_btnCAdd).For("Clicked").To(vm => vm.ShowSecondPageCommand);
            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
            set.Bind(netWork_label).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("Visibility");
            set.Apply();
            NotDoneTasksTableView.ReloadData();
        }

        private void OrientationsHandler(object sender, NSNotificationEventArgs e)
        {
            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;
        }
    }
}