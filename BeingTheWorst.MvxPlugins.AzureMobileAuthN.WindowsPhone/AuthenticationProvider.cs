using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.WindowsPhone
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public async Task<AuthenticationResult> AuthenticateAsync(AuthNProviderType providerType,
                                                                  AuthNProviderSettings providerSettings)
        {
            // setup the real Azure Mobile Services client provided by MS SDK
            //var mobileSvcsClient = new
            //    MobileServiceClient(providerSettings.UrlToAuthenticationProvider,
            //                        providerSettings.ApplicationIdKeyFromProvider);

            // try without sending key
            var mobileSvcsClient = new 
                MobileServiceClient(providerSettings.UrlToAuthenticationProvider);

            // default to MS account if whatever passed in is a no go
            // TODO: See Azure Mobile GitHub to see how they check for invalid Enum values
            var azmobProvider = MobileServiceAuthenticationProvider.MicrosoftAccount;

            // see which Azure Mobile Services Authentication Provider they want to use
            switch (providerType)
            {
                case AuthNProviderType.Google:
                    azmobProvider = MobileServiceAuthenticationProvider.Google;
                    break;
                case AuthNProviderType.Facebook:
                    azmobProvider = MobileServiceAuthenticationProvider.Facebook;
                    break;
                case AuthNProviderType.Twitter:
                    azmobProvider = MobileServiceAuthenticationProvider.Twitter;
                    break;
                case AuthNProviderType.Microsoft:
                    azmobProvider = MobileServiceAuthenticationProvider.MicrosoftAccount;
                    break;
                default:
                    break;
            }

            await mobileSvcsClient.LoginAsync(azmobProvider);

            var authResult = new AuthenticationResult
            {
                ProviderName = mobileSvcsClient.ToString(),
                IdentityString = mobileSvcsClient.CurrentUser.UserId,
                MobileServicesUserToken = mobileSvcsClient.CurrentUser.MobileServiceAuthenticationToken
            };

            return authResult;
        }
    }
}
