using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using TestProject.IOS.Sources;
using UIKit;

namespace TestProject.IOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = " Done Tasks")]
    public partial class DoneListItemView : BaseView<DoneListItemViewModel>
    {
        #region Variables
        private MvxUIRefreshControl _refreshControl;
        #endregion

        #region LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            _refreshControl = new MvxUIRefreshControl();
            DoneTasksTableView.AddSubview(_refreshControl);

            UIDevice.Notifications.ObserveOrientationDidChange(OrientationsHandler);
            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;

            SetUpNavigationBar();

            var source = new TasksTableViewSource(DoneTasksTableView);
            DoneTasksTableView.Source = source;
            SetUpBinding(source);

            DoneTasksTableView.ReloadData();
        }
        #endregion

        #region Methods
        private void OrientationsHandler(object sender, NSNotificationEventArgs e)
        {
            netWork_button_constraint.Constant = TabBarController.TabBar.Frame.Size.Height;
        }

        private void SetUpBinding(TasksTableViewSource source)
        {


            var set = this.CreateBindingSet<DoneListItemView, DoneListItemViewModel>();
            set.Bind(source).To(vm => vm.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskViewCommand);
            set.Bind(_btnAdd).For("Clicked").To(vm => vm.ShowSecondPageCommand);
            set.Bind(_btnAdd).For("Enabled").To(vm => vm.IsNetChecking);
            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
            set.Bind(netWork_label).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("ReverseVisibility");
            set.Apply();
        }
        #endregion







    }
}