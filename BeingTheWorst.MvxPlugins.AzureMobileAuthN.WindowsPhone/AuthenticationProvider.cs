using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.WindowsPhone
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public AuthenticationResult Authenticate(AuthNProviderType providerType, AuthNProviderSettings providerSettings)
        {
            // for now this is the synch slow network call that the plugin's cosumer will
            // wrap in a Task so it can await it.  "LoginService" for example.
            // maybe this part of the platform-specific plugin INCLUDES that
            // service and Mvx Viewmodel?
        }
    }
}
