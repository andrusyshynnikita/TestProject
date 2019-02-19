using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using TestProject.Core.Models;

namespace TestProject.Droid.Views.Holders
{
    public class TaskItemHolder : MvxRecyclerViewHolder
    {
        public TextView Title { get; set; }
        public CheckBox Status { get; set; }

        public TaskItemHolder(View itemview, IMvxAndroidBindingContext context) : base(itemview, context)
        {
            Title = itemview.FindViewById<TextView>(Resource.Id.item_text_view);

            Status = itemview.FindViewById<CheckBox>(Resource.Id.statusInfo);
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<TaskItemHolder, TaskInfo>();
                set.Bind(this.Title).To(x => x.Title);
                set.Bind(this.Status).To(x => x.Status);
                set.Apply();
            });
        }
    }
}