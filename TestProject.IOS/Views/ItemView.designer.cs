// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TestProject.IOS.Views
{
    [Register ("ItemView")]
    partial class ItemView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Delete_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView Description_text { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView loader { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainUIView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel netWork_label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Play { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton recording { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Save_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView saveLoader { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView Scrollview { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch Status_switch1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField Title_text { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Delete_button != null) {
                Delete_button.Dispose ();
                Delete_button = null;
            }

            if (Description_text != null) {
                Description_text.Dispose ();
                Description_text = null;
            }

            if (loader != null) {
                loader.Dispose ();
                loader = null;
            }

            if (MainUIView != null) {
                MainUIView.Dispose ();
                MainUIView = null;
            }

            if (netWork_label != null) {
                netWork_label.Dispose ();
                netWork_label = null;
            }

            if (Play != null) {
                Play.Dispose ();
                Play = null;
            }

            if (recording != null) {
                recording.Dispose ();
                recording = null;
            }

            if (Save_button != null) {
                Save_button.Dispose ();
                Save_button = null;
            }

            if (saveLoader != null) {
                saveLoader.Dispose ();
                saveLoader = null;
            }

            if (Scrollview != null) {
                Scrollview.Dispose ();
                Scrollview = null;
            }

            if (Status_switch1 != null) {
                Status_switch1.Dispose ();
                Status_switch1 = null;
            }

            if (Title_text != null) {
                Title_text.Dispose ();
                Title_text = null;
            }
        }
    }
}