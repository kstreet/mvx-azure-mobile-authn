using System.Threading.Tasks;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(AuthNProviderType providerType);
        void Logout();
    }
}
