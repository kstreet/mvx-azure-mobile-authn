using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.WindowsPhone
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public Task<AuthenticationResult> Authenticate(AuthNProviderType providerType, AuthNProviderSettings providerSettings)
        {
            throw new NotImplementedException();
        }
    }
}
