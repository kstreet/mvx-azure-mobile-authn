using System.Threading.Tasks;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticationProvider _authNProvider;
        private readonly IAuthNProviderSettings _authNProviderSettings;

        // Use constructor DI from IoC container again to get the
        // platform-specific IAuthenticationProvider and IAuthNProviderSettings injected in.
        // the plugin registers which IAuthenticationProvider to inject based on platform executing on
        public LoginService(IAuthenticationProvider authenticationProvider, 
                            IAuthNProviderSettings authNProviderSettings)
        {
            _authNProvider = authenticationProvider;
            _authNProviderSettings = authNProviderSettings;
        }

        public async Task<LoginResult> LoginAsync(AuthNProviderType providerType)
        {
            // force a logout on the mobile services client and  clear user profile 
            _authNProvider.Logout();

            // TODO: should add some error checking of the provider we get sent from ViewModel/View
            // TODO: but for now will just use as is

            var authNResult = await _authNProvider.AuthenticateAsync(providerType, _authNProviderSettings);

            if (authNResult != null)
            {
                var loginResult = new LoginResult
                {
                    ProviderName = authNResult.ProviderName,
                    IdentityString = authNResult.IdentityString,
                    MobileServicesUserToken = authNResult.MobileServicesUserToken
                };

                return loginResult;
            }
            else
            {
                // TODO: Something bad happend
                return null;
            }

        }

        public void Logout()
        {
            _authNProvider.Logout();
        }
    }
}
