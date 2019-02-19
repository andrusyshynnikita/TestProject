using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Converters;
using UIKit;

namespace TestProject.IOS.Converters
{
   public class StatusToImageValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == false)
            {
                return UIImage.FromBundle("UncheckImage");
            }

                return  UIImage.FromBundle("CheckImage");

           
        }
    }
}