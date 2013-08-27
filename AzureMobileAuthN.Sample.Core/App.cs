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
            // the "LoginService" is NOT inside the App's Core like it usually is, it's in the Plugin
            CreatableTypes(typeof(LoginService).GetTypeInfo().Assembly)
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            // TODO: Only need this if there are Services to be found in this App's DLL
            //CreatableTypes()
            //    .EndingWith("Service")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();


            // Determine which ViewModel to start the App with using CustomAppStart
            RegisterAppStart(new CustomAppStart(Mvx.Resolve<ILoginService>()));
        }

        public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
        {
            private readonly ILoginService _loginService;

            // TODO: In my case I may want to be using IAuthenticationProvider instead? TBD.
            public CustomAppStart(ILoginService loginService)
            {
                _loginService = loginService;
            }

            public void Start(object hint = null)
            {
                // if (!_loginService.IsLoggedIn)
                if (true)
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