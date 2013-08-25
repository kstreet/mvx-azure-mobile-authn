namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.Services
{
    public class LoginResult
    {
        // TODO: Mostly duplicating AuthenticationResult for now.
        // TODO: Do I need/want this extra services layer for LoginViewModel to use or not?

        public string ProviderName { get; set; }
        public string IdentityString { get; set; }
        public string MobileServicesUserToken { get; set; }
    }
}
