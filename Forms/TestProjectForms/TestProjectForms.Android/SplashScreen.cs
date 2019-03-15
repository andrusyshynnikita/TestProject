using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Forms.Platforms.Android.Views;

namespace TestProjectForms.Droid
{
    [Activity(
        Label = "TestPRoject.Forms",
        MainLauncher = true,
        NoHistory = true,
        Theme = "@style/MainTheme.Splash",
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, TestProject.Core.App, App>
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(RootActivity));
            return Task.CompletedTask;
        }
    }
}