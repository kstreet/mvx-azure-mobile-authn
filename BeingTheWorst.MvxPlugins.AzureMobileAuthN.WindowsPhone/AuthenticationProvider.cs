using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.WindowsPhone
{
    
    public class AuthenticationProvider : IAuthenticationProvider
    {
        // TODO: Many implementations have these two as static
        // TODO: Even when not static the phone is remebering last user auth even after Logout??
        // TODO: Maybe because of Singleton?  That would make sense...
        private MobileServiceClient _mobileSvcsClient;
        private UserProfile _userProfile;

        public async Task<AuthenticationResult> AuthenticateAsync(AuthNProviderType providerType,
                                                                  IAuthNProviderSettings providerSettings)
        {
            // try without sending AMS Application key for now
            _mobileSvcsClient = new 
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

            try
            {
                await _mobileSvcsClient.LoginAsync(azmobProvider);

                // TODO: How do I want to use User Profile Stuff here?
                // TODO: I will doouble set stff to see how it feels to use each

                var userProfile = await LoadUserProfile();

                var authResult = new AuthenticationResult
                {
                    ProviderName = _mobileSvcsClient.ToString(),
                    IdentityString = _mobileSvcsClient.CurrentUser.UserId,
                    MobileServicesUserToken = _mobileSvcsClient.CurrentUser.MobileServiceAuthenticationToken
                };

                return authResult;
            }
            catch (InvalidOperationException iop)
            {
                // TODO: if I try to login to Twitter and click back button on WP8
                // TODO: then System.InvalidOperationException is thrown but is NOT caught in Login VM!??  Why?
                // TODO: Must the try catch be inside here in the platform-specific authprovider?
                // TODO: Seems like that is the case, so leave it?

                // TODO: Do something useful
                //user cancelled so try again
                //MessageBox.Show("You must login to access the application.", "Please try again", MessageBoxButton.OK);
                Debug.WriteLine(
                    "AuthenticationProvider:InvalidOpException: You must login to access the application.");
            }
            catch (Exception ex)
            {
                // TODO: Do something useful
                //MessageBox.Show("We were not able to log you in, make sure you are connected to the internet and try again.", "Login failed", MessageBoxButton.OK);
                Debug.WriteLine(
                    "AuthenticationProvider:Exception: not able to log you in, make sure you are connected to the internet.");
            }
            finally
            {
                Debug.WriteLine("AuthenticationProvider:AuthenticateAsync: finally in try catch...got here!");
            }

            // TODO: how do I want to handle?
            // if I got here then something bad happened
            // TODO: Something besides null?

            return null;
        }

        public UserProfile UserProfile
        {
            get
            {
                if (_mobileSvcsClient.CurrentUser == null)
                {
                    return null;
                }
                else
                    return _userProfile;
            }
        }

        // TODO: This is where I can Load the UserProfile from local storage or server
        private async Task<UserProfile> LoadUserProfile()
        {

            // TODO: Temp fake user profile load from disk/service and or post login?

            _userProfile = new UserProfile
                {
                    DisplayName = "AuthProvider:TEMP-DisplayName",
                    Email = "AuthProvider:TEMP-Email",
                    PhoneNumber = "AuthProvider:TEMP-PhoneNumber",
                    UserName = "AuthProvider:TEMP-UserName"
                };

            // _userProfile = await Client.InvokeApiAsync<UserProfile>("profile", HttpMethod.Get, null);

            return _userProfile;
        }

        public void Logout()
        {
            if (_mobileSvcsClient != null)
            {
                _mobileSvcsClient.Logout();

                // TODO: Adding this null seemed to make Twitter
                // at least ask me to reauth app but remebered me and I was already logged in
                // TODO: static? not static? best way to setup is??
                // Mvx IoC setup issue?
                _mobileSvcsClient = null;
            }

            _userProfile = null;
        }
    }
}
