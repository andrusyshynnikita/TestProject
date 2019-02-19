using MvvmCross.Converters;
using MvvmCross.Plugin.Visibility;
using MvvmCross.UI;
using System;
using System.Globalization;

namespace TestProject.Droid.Converters
{
    public  class VisibilityValueConverter : MvxBaseVisibilityValueConverter<bool>
    {
        protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
        {
            if (value)
            {
                return MvxVisibility.Hidden;
            }

            return MvxVisibility.Visible;
        }
    }
}