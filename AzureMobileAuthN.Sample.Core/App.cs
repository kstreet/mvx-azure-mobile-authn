using Cirrious.CrossCore.IoC;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.ViewModels;

namespace AzureMobileAuthN.Sample.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            // TODO: will this find the LoginService provided in the plugin?
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            RegisterAppStart<LoginViewModel>();
        }
    }
}