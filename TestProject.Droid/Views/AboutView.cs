using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TestProject.Core.ViewModels;

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("TestProject.droid.views.AboutView")]
    public class AboutView : BaseFragment<AboutViewModel>
    {
 
        #region LifeCycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
        #endregion

        #region Properties
        protected override int FragmentId => Resource.Layout.Fragmentlayout;
        #endregion
    }
}