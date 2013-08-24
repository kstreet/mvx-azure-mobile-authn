namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN
{
    public interface IAuthenticationProvider
    {
        // can't use Task<AuthenticationResult> and support WP 7.5/7.1 because
        // MS Task/Async NuGet support is restricted to Windows-only BCL license!
        // TODO: Should I use some xplat Action callback here instead or doesn't matter?
        AuthenticationResult Authenticate(AuthNProviderType providerType, AuthNProviderSettings providerSettings);
    }
}
