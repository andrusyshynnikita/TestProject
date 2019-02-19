using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Plugin.Visibility;
using MvvmCross.UI;
using UIKit;

namespace TestProject.IOS.Converters
{
    public class VisibilityValueConverter : MvxBaseVisibilityValueConverter<bool>
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