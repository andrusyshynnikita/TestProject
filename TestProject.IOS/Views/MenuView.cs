using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Sidebar;
using TestProject.Core.ViewModels;

namespace TestProject.IOS
{

   // [MvxSidebarPresentation(MvxPanelEnum.Right, MvxPanelHintType.PushPanel, false)]
    public partial class MenuView : MvxViewController<menuViewModel>
    {

        public MenuView() : base(nameof(MenuView), null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}