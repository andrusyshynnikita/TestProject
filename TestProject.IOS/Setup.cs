using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Plugin.Json;
using MvvmCross.ViewModels;
using TestProject.Core;
using TestProject.IOS.CustomBinding;
using UIKit;

namespace TestProject.IOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var registry = Mvx.IoCProvider.Resolve<IMvxTargetBindingFactoryRegistry>();
            registry.RegisterCustomBindingFactory<UIActivityIndicatorView>("IsLoading", view => new ActivityIndicatorViewHiddenTargetBinding(view));
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }

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