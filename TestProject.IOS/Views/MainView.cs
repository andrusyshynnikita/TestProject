using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;

namespace StarWarsSample.iOS.Views
{
    [MvxRootPresentation]
    public class MainView : MvxViewController<MainViewModel>
    {
        #region Variables
        private bool _firstTimePresented = true;
        #endregion

        #region Constructors
        public MainView()
        {
        }
        #endregion

        #region LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.ShowCurrentViewModelCommand.Execute(null);

            }
        }
        #endregion
    }
}