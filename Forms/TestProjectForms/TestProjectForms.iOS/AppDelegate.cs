using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Core;
using UIKit;

namespace TestProjectForms.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, TestProject.Core.App, App>
    {

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            
            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            var result = base.FinishedLaunching(application, launchOptions);
            return result;
        }
    }
}
