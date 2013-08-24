using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN
{
    // all Mvx Plugins have this same boilerplate code using this naming convention
    public class PluginLoader : IMvxPluginLoader
    {
        // another part of the Plugin convention is to add an instance of this class itself
        public static readonly PluginLoader Instance = new PluginLoader();

        public void EnsureLoaded()
        {
            var pluginMgr = Mvx.Resolve<IMvxPluginManager>();

            // find the associated platform-specific Mvx Plugin file based on my namespace name
            pluginMgr.EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
    }
}
