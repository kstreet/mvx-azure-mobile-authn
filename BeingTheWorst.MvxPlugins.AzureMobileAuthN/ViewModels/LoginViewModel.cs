using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using BeingTheWorst.MvxPlugins.AzureMobileAuthN.Services;
using Cirrious.MvvmCross.ViewModels;

namespace BeingTheWorst.MvxPlugins.AzureMobileAuthN.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly ILoginService _loginService;

        // TODO: not sure if best way to do this but I just want to store results for now
        private LoginResult _loginResult;

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // a simple flag to tell us if we are ALREADY making a request
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        private string _userIdentity;
        public string UserIdentity
        {
            get { return _userIdentity; }
            set { _userIdentity = value; RaisePropertyChanged(() => UserIdentity); }
        }

        private string _azureToken;
        public string AzureToken
        {
            get { return _azureToken; }
            set { _azureToken = value; RaisePropertyChanged(() => AzureToken); }
        }


        private MvxCommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            { 
                // TODO: Add back passing string CommandParama when this works
                //_loginCommand = _loginCommand ?? new MvxCommand(async () => await DoLogin());
                //return _loginCommand;

                return _loginCommand ?? (_loginCommand = new MvxCommand(() => DoLogin()));
            }
        }

        //private void DoLogin(string authenticationProvider)
        private void DoLogin()
        {
            // set our public IsBusy property (to fire our bounded events - progress bars etc)
            IsBusy = true;

            // var authNProvider = (AuthNProviderType) Enum.Parse(typeof (AuthNProviderType), authenticationProvider, true);

            // TODO: TEST Hard code AUth provider as View keeps sending me "Google"
            // Command Params not working?

            var authNProvider = AuthNProviderType.Twitter;


            // use the Login Service to do the client-specific login experience
            // when the LoginResult comes back, set the above UserIdentity property (TODO: for now)
            // TODO: may want to be setting/saving those results elsewhere because 
            // If login works we no longer want to be on the LoginViewModel/View
            // maybe that is where we put stuff in the "AppViewModel"??

            try
            {
                // TODO: this should really be using Async when it can i think?
                // TODO: this should be using the platform-specific injected IAuthenticationProvider
                // TODO: via the ILoginService
                // TODO:  await _loginService.LoginAsync  etc.  Not sure how to do that with Mvx yet though.
                // TODO: REALLY want this to be awaitable!


                // cheese:
                InvokeOnMainThread(async () => { _loginResult = await _loginService.LoginAsync(authNProvider); });

                // TODO: Seems like it skips right over this and I hit finally block with no stop here???
                // set some ViewModel properties with the results
                UserIdentity = _loginResult.IdentityString;
                AzureToken = _loginResult.MobileServicesUserToken;

                IsBusy = false;
            }
            catch (InvalidOperationException iop)
            {
                //user canceled so try again
                //MessageBox.Show("You must login to access the application.", "Please try again", MessageBoxButton.OK);
                System.Diagnostics.Debug.WriteLine(
                    "LoginViewModel:InvalidOpException: You must login to access the application.");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("We were not able to log you in, make sure you are connected to the internet and try again.", "Login failed", MessageBoxButton.OK);
                System.Diagnostics.Debug.WriteLine(
                    "LoginViewModel:Exception: not able to log you in, make sure you are connected to the internet.");
            }
            finally
            {
                Debug.WriteLine("LoginViewModel:DoLogin: finally in try catch...got here!");
            }

            // after login,
            // go to home page or what?
            // App.RootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            //return _loginResult;
        }
    }
}


