using MvvmCross.Plugin.Visibility;
using MvvmCross.UI;
using System.Globalization;

namespace TestProject.Core.Converters
{
    public class ReverseVisibilityValueConverter : MvxBaseVisibilityValueConverter<bool>
    {
        protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
        {
            if (value)
            {
                return MvxVisibility.Collapsed;
            }

            return MvxVisibility.Visible;
        }
    }
}
