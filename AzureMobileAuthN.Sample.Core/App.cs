using System.Reflection;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.Services;
using Cirrious.CrossCore.IoC;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.ViewModels;

namespace AzureMobileAuthN.SampleApp.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            // TODO:  What is correct Syntax of
            // CreatableTypes(Assembly) to that this "App Core PCL" can find the LoginService inside the plugin's core
            // the "Service" is NOT inside the App's Core like it usually is
            // http://stackoverflow.com/questions/16704224/mvvmcross-with-two-core-libraries

            // TODO: This syntax seem to work but is this the correct/best syntax for this for xplat?
            CreatableTypes(typeof(LoginService).GetTypeInfo().Assembly)
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            // TODO: will this find the LoginService provided in the plugin?
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<LoginViewModel>();
        }
    }
}