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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : MvxContentPage<ItemViewModel>
    {
        public ItemView()
        {
            InitializeComponent();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}