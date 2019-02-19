using System.Globalization;
using MvvmCross.Plugin.Visibility;
using MvvmCross.UI;

namespace TestProject.IOS.Converters
{
    public class VisibilityButtonValueConverter : MvxVisibilityValueConverter
    {
        protected override MvxVisibility Convert(object value, object parameter, CultureInfo culture)
        {
            return base.Convert(value, parameter, culture);
        }
    }
}