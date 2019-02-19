using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using TestProject.Core.Models;
using UIKit;

namespace TestProject.IOS.Views.Cells
{
    public partial class TasksViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("TasksViewCell");
        public static readonly UINib Nib = UINib.FromName("TasksViewCell", NSBundle.MainBundle);

        public static TasksViewCell Create()
        {
            return Nib.Instantiate(null, null)[0] as TasksViewCell;
        }

        protected TasksViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<TasksViewCell, TaskInfo>();
                set.Bind(TitleTask).To(m => m.Title);
                set.Bind(StatusTask).For(v => v.Image).To(vm => vm.Status).WithConversion("StatusToImage");
                set.Apply();
            });
        }
    }
}
