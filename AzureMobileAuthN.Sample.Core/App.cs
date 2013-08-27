using System.Reflection;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.Services;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.ViewModels;
using Cirrious.MvvmCross.ViewModels;

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

            // TODO:  Is this syntax right?
            // TODO:  Seems like it, it's working.
            // TODO: Also see http://stackoverflow.com/questions/17471084/using-nfc-with-mvx-wp8-application
            //RegisterAppStart<LoginViewModel>();
            RegisterAppStart(new CustomAppStart(Mvx.Resolve<ILoginService>()));
        }

        public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
        {
            private readonly ILoginService _service;


            // TODO: In my case I may want to be using IAuthenticationProvider instead? TBD.
            public CustomAppStart(ILoginService service)
            {
                _service = service;
            }

            public void Start(object hint = null)
            {
                // if (!_service.IsLoggedIn)
                if (false)
                {
                    ShowViewModel<LoginViewModel>();
                }
                else
                {
                    ShowViewModel<HomeViewModel>();
                }
            }
        }
    }
}