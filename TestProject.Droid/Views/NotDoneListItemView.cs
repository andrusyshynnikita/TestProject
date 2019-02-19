using Android.OS;
using TestProject.Core.ViewModels;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using TestProject.Droid.Views;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Runtime;
using TestProject.Droid.Adapters;

namespace TestProject.Droid
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("TestProject.droid.view.DoneListItemView")]
    public class NotDoneListItemView : BaseFragment<NotDoneListItemViewModel>
    {
        #region Variables
        private Toolbar _mToolbar;
        private RecyclerView.LayoutManager _layoutManager;
        private TasksItemAdapter _mAdapter;
        private MvxRecyclerView _recyclerView;
        #endregion

        #region Constructors
        public NotDoneListItemView()
        {
        }
        #endregion

        #region LifeCycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _mToolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar1);
            ParentActivity.SetSupportActionBar(_mToolbar);
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recyclerView);
            _layoutManager = new LinearLayoutManager(this.Context);
            _recyclerView.SetLayoutManager(_layoutManager);
            _mAdapter = new TasksItemAdapter((IMvxAndroidBindingContext)BindingContext);
            _recyclerView.Adapter = _mAdapter;
            return view;
        }
        #endregion

        #region Properties
        protected override int FragmentId => Resource.Layout.DoneListItemsLayout;
        #endregion    
    }
}