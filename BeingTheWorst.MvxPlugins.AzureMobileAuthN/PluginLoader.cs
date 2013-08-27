using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore.Plugins;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN
{
    // all Mvx Plugins have this similar boilerplate code using this naming convention
    public class PluginLoader : IMvxConfigurablePluginLoader
    {
        public static readonly  PluginLoader Instance = new PluginLoader();

        private bool _loaded;
        private AzureMobileAuthNConfiguration _azureMobileConfig;

        public void EnsureLoaded()
        {
            if (_loaded) return;

            // The AuthenticationProvider is provided by platform specific code
            // and the implementation of it is injected into this plugin's LoginService at runtime.
            // The LoginService needs to provide IAuthNProviderSettings to the platform-specific
            // Authentication Provider at runtime and needs an AuthNProviderSettings object.
            // I can create an instance of that here based on the config from Setup.cs that was passed in.

            var providerSettings = new AuthNProviderSettings
                {
                    UrlToAuthenticationProvider = _azureMobileConfig.AzureMobileServiceUrl
                };

            Mvx.RegisterSingleton<IAuthNProviderSettings>(providerSettings);

            var pluginMgr = Mvx.Resolve<IMvxPluginManager>();

            // find the associated platform-specific Mvx Plugin file based on my namespace name
            pluginMgr.EnsurePlatformAdaptionLoaded<PluginLoader>();

            _loaded = true;
        }

        public void Configure(IMvxPluginConfiguration configuration)
        {
            if (configuration != null && !(configuration is AzureMobileAuthNConfiguration))
                throw new MvxException(
                    "Plugin configuration only supports instances of AzureMobileAuthNConfiguration, you provided {0}",
                    configuration.GetType().Name);

            _azureMobileConfig = (AzureMobileAuthNConfiguration)configuration;
        }
    }
}
