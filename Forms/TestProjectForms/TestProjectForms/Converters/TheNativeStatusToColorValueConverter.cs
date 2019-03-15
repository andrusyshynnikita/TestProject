using MvvmCross.Converters;
using MvvmCross.Forms.Converters;
using MvvmCross.Plugin.Color;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TestProject.Core.Converters;
using Xamarin.Forms;

namespace TestProjectForms.Converters
{
   public class TheNativeStatusToColorValueConverter : MvxNativeValueConverter<TheFormsStatusToColorValueConverter>
    {

    }

    public class TheFormsStatusToColorValueConverter : MvxValueConverter<bool>
    {
        
        protected override object Convert(bool status, Type targetType, object parameter, CultureInfo culture)
        {
            if (status)
            {
                return new Color(0, 100, 0);
            }

            return new Color(255, 0, 0);
        }
    }
}
