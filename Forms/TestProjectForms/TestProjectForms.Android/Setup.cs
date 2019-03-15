using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace TestProjectForms.Droid
{
    public class Setup : MvxFormsAndroidSetup<TestProject.Core.App, App>
    {
        protected override IMvxApplication CreateApp()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            return base.CreateApp();
        }
    }
}