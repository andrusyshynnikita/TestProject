using MvvmCross.Converters;
using System;
using System.Globalization;

namespace TestProject.Core.Converters
{
    public class StatusToTitlePlayButtonValueConverter : MvxValueConverter<bool>

    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value)
            {
                return "Play";
            }

            return "Stop";
        }
    }
}
