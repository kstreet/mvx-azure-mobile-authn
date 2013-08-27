using Cirrious.CrossCore.Plugins;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN
{
    public class AzureMobileAuthNConfiguration : IMvxPluginConfiguration
    {
        public string AzureMobileServiceUrl { get; set; }

        // TODO: Is this needed for AuthN only?  Client doesn't seem to need to provide it?
        // TODO: Azure Application Key does not seem to be needed for AuthN to work...
        // TODO: Determine usage and how random ppl can't use my AMS to authN as "me"
        // public string AzureMobileServiceAppKey { get; set; }
    }
}
