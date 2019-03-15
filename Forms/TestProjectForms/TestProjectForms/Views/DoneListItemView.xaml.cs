using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProjectForms.Views
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab, Title ="Done Tasks",WrapInNavigationPage =true)]
    public partial class DoneListItemView : MvxContentPage<DoneListItemViewModel>
    {
		public DoneListItemView ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Purple;
            base.OnAppearing();
        }
    }
}