using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TestProject.Core.ViewModels;

namespace TestProject.Droid.Views
{
    [MvxActivityPresentation()]
    [Activity]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        #region LifeCicle
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);

            if (bundle == null)
            {
                ViewModel.ShowCurrentViewModelCommand.Execute(null);
            }
        }
        #endregion
    }
}