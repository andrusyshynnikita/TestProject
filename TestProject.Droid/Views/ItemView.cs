using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Widget;
using TestProject.Core.ViewModels;
using Android.Runtime;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;

namespace TestProject.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("TestProject.droid.views.ItemView")]
    public class ItemView : BaseFragment<ItemViewModel>
    {
        #region Variables
        private Android.Support.V7.Widget.Toolbar _mToolBar;
        private LinearLayout _linearLayout;
        private EditText _editText;
        private Button _recordingAudio;
        static readonly int REQUEST_STORAGE = 0;
        #endregion        

        #region LifeCycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _mToolBar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar2);
            ParentActivity.SetSupportActionBar(_mToolBar);

            _recordingAudio = view.FindViewById<Button>(Resource.Id.recording);
            _linearLayout = view.FindViewById<LinearLayout>(Resource.Id.item_Layout2);

            _recordingAudio.Click += CheckPermission;
            _linearLayout.Click += delegate { HideSoftKeyboard(); };
            _mToolBar.Click += delegate { HideSoftKeyboard(); };

            _editText = view.FindViewById<Android.Widget.EditText>(Resource.Id.name_text);
            Typeface type = Typeface.CreateFromAsset(Activity.Assets, "13159.otf");
            _editText.SetTypeface(type, TypefaceStyle.Normal);

            return view;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            _recordingAudio.Click -= CheckPermission;

            HideSoftKeyboard();
        }
        #endregion

        #region Properties
        protected override int FragmentId => Resource.Layout.ItemLayout;
        #endregion

        #region Methods
        private void CheckPermission(object sender, EventArgs e)
        {
            if (ActivityCompat.CheckSelfPermission(Context, Manifest.Permission.RecordAudio) == (int)Permission.Granted)
            {
                ViewModel.StartRecordingCommand.Execute();
            }

            else
            {
                RequestStoragePermission();
            }
        }

        public void HideSoftKeyboard()
        {
            InputMethodManager close = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            close.HideSoftInputFromWindow(_linearLayout.WindowToken, 0);
        }

        private void RequestStoragePermission()
        {
            ActivityCompat.RequestPermissions(ParentActivity, new String[] { Manifest.Permission.RecordAudio }, REQUEST_STORAGE);
        }
        #endregion







    }
}