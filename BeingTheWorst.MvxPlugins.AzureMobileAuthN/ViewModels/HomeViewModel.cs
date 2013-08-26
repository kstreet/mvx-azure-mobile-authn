using Cirrious.MvvmCross.ViewModels;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.ViewModels

{
    public class HomeViewModel 
		: MvxViewModel
    {
		private string _hello = "Hello Azure Mobile AuthN In MvvmCross";
        public string Hello
		{ 
			get { return _hello; }
			set { _hello = value; RaisePropertyChanged(() => Hello); }
		}
    }
}
