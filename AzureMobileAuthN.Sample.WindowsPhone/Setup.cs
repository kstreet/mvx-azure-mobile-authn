using BeingTheWorst.MvxPlugins.AzureMobileAuthN;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.WindowsPhone;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;

namespace AzureMobileAuthN.Sample.WindowsPhone
{
    public class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            // this one is default. Try to use custom to see what error is
            // return new DebugTrace();

            //TODO: ok to keep this?
            return new MyDebugTrace();
        }

        // this Initialize method is the last one called in setup initialization
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            // now let's tell setup that whenever anyone asks for an IAuthenticationProvider
            // from this client application, give this same single instance (Singleton)
            // of AuthenticationProvider

            // original approach used in N+31 sample
            // TODO: but should I be using new instances for login code with dynamic syntax below?
            //Mvx.RegisterSingleton<IAuthenticationProvider>
            //    (new AuthenticationProvider());

            Mvx.RegisterSingleton<IAuthenticationProvider>
            (new AuthenticationProvider());

            // alternatively you can use Lazy loading of same thing
            //Mvx.RegisterSingleton<IAuthenticationProvider>
            //    (() => new AuthenticationProvider());

            // alternatively you can use dynamic loading of NEW INSTANCES (no instance sharing) of these things:
            // TODO: WHich MAY be what we actually want for LOGIN type code!  TBD.
            // Mvx.RegisterType<IAuthenticationProvider, AuthenticationProvider>();

            // Can also use the patterns with reflection over the 
            // whole DLL stuff for MANY registrations (as seen in Core's App.cs)
        }
    }
}