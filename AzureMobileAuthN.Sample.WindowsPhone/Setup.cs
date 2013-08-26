using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AzureMobileAuthN.SampleApp.WindowsPhone.Views;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.ViewModels;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.WindowsPhone;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;

namespace AzureMobileAuthN.SampleApp.WindowsPhone
{
    public class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new SampleApp.Core.App();
        }

        // Help the system find the LoginView for the LoginViewModel in the plugin's core
        protected override void InitializeViewLookup()
        {
            // TODO: At some point it would be nice to have an "official" way
            // TODO: to tell the plugin WHICH one of the user-created Views
            // TODO: should be navigated to after a successful login
            // TODO: Hard code the names "HomeViewModel" and "HomeView" for now.

            var viewModelViewLookup = new Dictionary<Type, Type>
            {
                {typeof(LoginViewModel), typeof(LoginView)},
                {typeof(HomeViewModel), typeof(HomeView)}
            };

            var container = Mvx.Resolve<IMvxViewsContainer>();
            container.AddAll(viewModelViewLookup);
        }

        protected override Assembly[] GetViewModelAssemblies()
        {
            var toReturn = base.GetViewModelAssemblies().ToList();
            toReturn.Add(typeof(LoginViewModel).Assembly);
            toReturn.Add(typeof(HomeViewModel).Assembly);
            return toReturn.ToArray();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
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