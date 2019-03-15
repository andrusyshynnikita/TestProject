using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TestProjectForms.CustomControls;
using TestProjectForms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(TestProjectForms.iOS.Renderers.ButtonRenderer))]
namespace TestProjectForms.iOS.Renderers
{
    public class ButtonRenderer : ViewRenderer<CustomButton, UIButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomButton> e)
        {
            base.OnElementChanged(e);
        }
    }
}