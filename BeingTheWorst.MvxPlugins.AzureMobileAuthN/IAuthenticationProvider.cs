namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN
{
    public interface IAuthenticationProvider
    {
        AuthenticationResult Authenticate(AuthNProviderType providerType, AuthNProviderSettings providerSettings);
    }
}
