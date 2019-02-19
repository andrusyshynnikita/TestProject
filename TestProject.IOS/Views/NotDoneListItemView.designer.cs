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
    [Register ("NotDoneListItemView")]
    partial class NotDoneListItemView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint netWork_button_constraint { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel netWork_label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView NotDoneTasksTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (netWork_button_constraint != null) {
                netWork_button_constraint.Dispose ();
                netWork_button_constraint = null;
            }

            if (netWork_label != null) {
                netWork_label.Dispose ();
                netWork_label = null;
            }

            if (NotDoneTasksTableView != null) {
                NotDoneTasksTableView.Dispose ();
                NotDoneTasksTableView = null;
            }
        }
    }
}