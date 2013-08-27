namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN
{
    public class AuthNProviderSettings : IAuthNProviderSettings
    {
        public string UrlToAuthenticationProvider { get; set; }
        public string ApplicationIdKeyFromProvider { get; set; }
    }
}
