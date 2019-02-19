using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TestProject.Core.Converters
{
    public class StatusToTitleRecordButtonValueConverter : MvxValueConverter<bool>

    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value)
            {
                return "Record";
            }

            return "Stop";
        }
    }
}
