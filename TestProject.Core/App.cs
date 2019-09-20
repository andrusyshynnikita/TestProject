using MvvmCross.ViewModels;
using MvvmCross.IoC;
using MvvmCross;
using TestProject.Core.Configuration.Interfaces;
using TestProject.Core.Configuration;

namespace TestProject.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton<IAPIConfiguration>(new LocalAPIConfiguration());

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterCustomAppStart<AppStart>();
        }
    }
}
