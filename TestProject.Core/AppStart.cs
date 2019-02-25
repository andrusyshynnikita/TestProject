using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TestProject.Core.Interface;
using TestProject.Core.ViewModels;

namespace TestProject.Core
{


    public class AppStart : MvxAppStart
    {
        private IMvxNavigationService _mvxNavigationService;
        private ILoginService _loginService;

        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
        }

        protected override  Task NavigateToFirstViewModel(object hint = null)
        {
             return NavigationService.Navigate<MainViewModel>();
        }
    }

}
