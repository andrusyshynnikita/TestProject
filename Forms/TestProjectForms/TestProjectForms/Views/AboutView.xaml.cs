using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TestProject.Core.ViewModels;
using Xamarin.Forms;

namespace TestProjectForms.Views
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab, Title = "About")]
    public partial class AboutView : MvxContentPage<AboutViewModel>
    {
		public AboutView ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }
    }
}