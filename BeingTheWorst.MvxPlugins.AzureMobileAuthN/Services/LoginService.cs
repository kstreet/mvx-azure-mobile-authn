﻿using System.Threading.Tasks;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticationProvider _authNProvider;

        // Use constructor DI from IoC container again to get the
        // platform-specific IAuthenticationProvider injected in.
        // the plugin registers which IAuthenticationProvider to inject based on platform executing on
        public LoginService(IAuthenticationProvider authenticationProvider)
        {
            _authNProvider = authenticationProvider;
        }

        public async Task<LoginResult> LoginAsync(AuthNProviderType providerType)
        {
            // TODO: determine best place to get the SPECIFICS per APP
            // TODO: of the settings of the authN provider to use
            // TODO: for now, hardcode
            // TODO: See the WshLst app as possible solution or see if Stuart has common settings approach xplat
            // TODO: thought I saw him post about xplat settings which would be nice but  thougth it was UI/user related

            var authNProviderSettings = new AuthNProviderSettings
                {
                    UrlToAuthenticationProvider = "ENTER YOUR AZURE MOBILE SERVICE URL",
                    ApplicationIdKeyFromProvider = "ENTER YOUR AZURE MOBILE SERVICE APPLICATION KEY"
                };

            // TODO: should add some error checking of the provider we get sent from ViewModel/View
            // TODO: but for now will just use as is

            var authNResult = await _authNProvider.AuthenticateAsync(providerType, authNProviderSettings);

            var loginResult = new LoginResult
            {
                ProviderName = authNResult.ProviderName,
                IdentityString = authNResult.IdentityString,
                MobileServicesUserToken = authNResult.MobileServicesUserToken
            };

            return loginResult;
        }
    }
}
