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
    public partial class NotDoneListItemView : BaseView<NotDoneListItemViewModel>
    {
        #region Variables
        private UIBarButtonItem _btnCAdd;
        private MvxUIRefreshControl _refreshControl;
        #endregion

        #region LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.Title = "TaskyDrop";
            _refreshControl = new MvxUIRefreshControl();
            NotDoneTasksTableView.AddSubview(_refreshControl);

            SetUpNavigationBar();

            UIDevice.Notifications.ObserveOrientationDidChange(OrientationsHandler);
            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;

            var source = new TasksTableViewSource(NotDoneTasksTableView);
            NotDoneTasksTableView.Source = source;

            SetUpBinding(source);

            NotDoneTasksTableView.ReloadData();
        }
        #endregion

        #region Methods
        private void SetUpBinding(TasksTableViewSource source)
        {
            var set = this.CreateBindingSet<NotDoneListItemView, NotDoneListItemViewModel>();
            set.Bind(source).To(vm => vm.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskViewCommand);
            set.Bind(_btnCAdd).For("Clicked").To(vm => vm.ShowSecondPageCommand);
            set.Bind(_btnCAdd).For("Enabled").To(vm => vm.IsNetChecking);
            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
            set.Bind(netWork_label).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("ReverseVisibility");
            set.Apply();
        }

        private void OrientationsHandler(object sender, NSNotificationEventArgs e)
        {
            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;
        }
        #endregion







    }
}