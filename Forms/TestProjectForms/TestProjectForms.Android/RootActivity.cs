using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Forms.Platforms.Android.Views;
using OxyPlot.Xamarin.Forms.Platform.Android;
using TestProject.Core.ViewModels;

namespace TestProjectForms.Droid
{
    [Activity(
        Label = "StarWarsSample.Forms",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class RootActivity : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
               TabLayoutResource = Resource.Layout.Tabbar;
         //      ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            //  AnimationViewRenderer.Init();
            PlotViewRenderer.Init();
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);
            //  CachedImageRenderer.Init(true);
            // UserDialogs.Init(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}