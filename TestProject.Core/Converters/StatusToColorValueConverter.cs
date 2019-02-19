using MvvmCross.Plugin.Color;
using MvvmCross.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TestProject.Core.Converters
{
    public class StatusToColorValueConverter : MvxColorValueConverter<bool>
    {
        protected override MvxColor Convert(bool status, object parameter, CultureInfo culture)
        {
            if (status)
            {
                return new MvxColor(0, 100, 0);
            }

            return new MvxColor(255, 0, 0);
        }
    }
}

