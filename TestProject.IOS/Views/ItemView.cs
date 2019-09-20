using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using TestProject.Core.ViewModels;
using UIKit;

namespace TestProject.IOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true, ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal)]
    public partial class ItemView : BaseView<ItemViewModel>
    {
        #region LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title_text.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;

            };

            this.Description_text.ShouldEndEditing += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));

            View.AddGestureRecognizer(g);
            SetUpCloseNavigationBar();
            SetUpBinding();
            SetUpTextView();
        }
        #endregion

        #region Methods
        private void SetUpTextView()
        {
            var Placeholder = "Description";

            if (string.IsNullOrEmpty(Description_text.Text) || Description_text.Text == Placeholder)
            {
                Description_text.Text = Placeholder;
                Description_text.TextColor = new UIColor(0.78f, 0.78f, 0.8f, 1.0f);
            }

            Description_text.ShouldBeginEditing = t =>
            {
                if (Description_text.Text == Placeholder)
                {
                    Description_text.Text = string.Empty;
                    Description_text.TextColor = UIColor.Black;
                }
                return true;
            };

            Description_text.ShouldEndEditing = t =>
            {
                if (string.IsNullOrEmpty(Description_text.Text))
                {
                    Description_text.TextColor = new UIColor(0.78f, 0.78f, 0.8f, 1.0f);
                    Description_text.Text = Placeholder;
                }

                return true;
            };
        }

        private void SetUpBinding()
        {
            var set = this.CreateBindingSet<ItemView, ItemViewModel>();
            set.Bind(Title_text).To(vm => vm.Title);
            set.Bind(Title_text).For(v => v.Enabled).To(vm => vm.IsTitleEnable);

            set.Bind(Description_text).To(vm => vm.Description);
            set.Bind(Status_switch1).To(vm => vm.Status);

            set.Bind(Save_button).To(vm => vm.SaveCommand);
            set.Bind(Save_button).For(v => v.Enabled).To(vm => vm.IsSavingTaskEnable);
            set.Bind(Save_button).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("Visibility");
            set.Bind(Save_button).For(btn => btn.Hidden).To(vm => vm.IsSavingProcessing);

            set.Bind(Delete_button).To(vm => vm.DeleteCommand);
            set.Bind(Delete_button).For(v => v.Enabled).To(vm => vm.IsDeletingTaskEnable);
            set.Bind(Delete_button).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("Visibility");
            set.Bind(Delete_button).For(btn => btn.Hidden).To(vm => vm.IsBusy);

            set.Bind(_btnBack).For("Clicked").To(vm => vm.CloseCommand);
            set.Bind(recording).To(vm => vm.StartRecordingCommand);
            set.Bind(recording).For(v => v.BackgroundColor).To(vm => vm.IsREcordChecking).WithConversion("StatusToColor");
            set.Bind(recording).For("Title").To(vm => vm.IsREcordChecking).WithConversion("StatusToTitleRecordButton");
            set.Bind(Play).To(vm => vm.PlayRecordingCommand);
            set.Bind(Play).For(v => v.Enabled).To(vm => vm.PermissionToPlay);
            set.Bind(Play).For(v => v.BackgroundColor).To(vm => vm.IsPlayChecking).WithConversion("StatusToColor");
            set.Bind(Play).For("Title").To(vm => vm.IsPlayChecking).WithConversion("StatusToTitlePlayButton");
            set.Bind(netWork_label).For("Visibility").To(vm => vm.IsNetChecking).WithConversion("ReverseVisibility");
            set.Bind(loader).For("IsLoading").To(vm => vm.IsBusy).WithConversion("ReverseBool");
            set.Bind(saveLoader).For("IsLoading").To(vm => vm.IsSavingProcessing).WithConversion("ReverseBool");
            set.Apply();
        }
        #endregion
    }
}