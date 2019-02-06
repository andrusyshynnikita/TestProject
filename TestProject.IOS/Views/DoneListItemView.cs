using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using TestProject.IOS.Sources;
using UIKit;

namespace TestProject.IOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = " DoneTasks")]
    public partial class DoneListItemView : MvxViewController<DoneListItemViewModel>
    {
        private UIBarButtonItem _btnCAdd;
        private MvxUIRefreshControl _refreshControl;

        public DoneListItemView () : base(nameof(DoneListItemView), null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "TaskyDrop";

            _refreshControl = new MvxUIRefreshControl();
            DoneTasksTableView.AddSubview(_refreshControl);

            _btnCAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_btnCAdd, false);
            NavigationController.NavigationBar.BarTintColor = UIColor.Purple;
            NavigationController.NavigationBar.TintColor = UIColor.Black;

            var source = new TasksTableViewSource(DoneTasksTableView);
            DoneTasksTableView.Source = source;
            var set = this.CreateBindingSet<DoneListItemView, DoneListItemViewModel>();
            set.Bind(source).To(vm => vm.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.TaskViewCommand);
            set.Bind(_btnCAdd).For("Clicked").To(vm => vm.ShowSecondPageCommand);
            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
            set.Apply();
            DoneTasksTableView.ReloadData();
        }
    }
}