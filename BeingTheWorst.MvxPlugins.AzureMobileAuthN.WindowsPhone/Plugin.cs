using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.WindowsPhone
{
    // Plugin boilerplate code for the Implementation-side
    public class Plugin : IMvxPlugin
    {
        // you can be certain that this
        // Plugin Load method will only be called once
        public void Load()
        {
            // can use the same IoC methods here that
            // you use in Setup and App

            // whenever someone asks for an IAuthenticationProvider on THIS platform
            // give them the AuthenticationProvider implemented in this DLL

            Mvx.RegisterType<IAuthenticationProvider, AuthenticationProvider>();
        }
    }
}
