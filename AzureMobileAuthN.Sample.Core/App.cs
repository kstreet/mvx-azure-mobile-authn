using Cirrious.CrossCore.IoC;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.ViewModels;

namespace AzureMobileAuthN.Sample.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {

            // TODO:  What is correct Syntax of
            // CreatableTypes(Assembly) to that this "App Core PCL" can find the LoginService inside the plugin's core
            // the "Service" is NOT inside the App's COre like it usually is.

            // TODO: will this find the LoginService provided in the plugin?
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            RegisterAppStart<LoginViewModel>();
        }
    }
}