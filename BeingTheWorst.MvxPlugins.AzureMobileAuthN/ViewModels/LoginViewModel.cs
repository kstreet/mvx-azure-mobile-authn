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

        private MvxCommand<string> _loginCommand;
        public ICommand LoginCommand
        {
            get
            { 
                // TODO: Old Param-less command and had async/await.  Not needed?
                //_loginCommand = _loginCommand ?? new MvxCommand(async () => await DoLogin());
                //return _loginCommand;


                // Mvx "CommandParameter" support and potential limitation info here:
                // http://slodge.blogspot.co.uk/2013/06/commandparameter-binding.html
                _loginCommand = _loginCommand ?? new MvxCommand<string>(authNProvider => DoLogin(authNProvider));
                return _loginCommand;

            }
        }

        private async void DoLogin(string authenticationProvider)
        {
            // set our public IsBusy property (to fire our bounded events - progress bars etc)
            IsBusy = true;

            // Parse the selected Azure Mobile authentication provider that was passed as a View CommandParameter
            var authNProvider = 
                (AuthNProviderType) Enum.Parse(typeof (AuthNProviderType), authenticationProvider, true);

            // use the Login Service to do the platform-specific login experience
            // uses the platform-specific injected IAuthenticationProvider
            // that is provided via the ILoginService passed into VM
            try
            {
                // TODO: Review Full WshLst approach

                // TODO: Review PS error handling approach and other things

                //  App.Azure.LoginAsync(platform.Provider).ContinueWith((task) => HandleLoginResult(task, platform));
                await _loginService.LoginAsync(authNProvider).ContinueWith((task) => HandleLoginResult(task));
                
            }
            catch (InvalidOperationException iop)
            {
                // TODO: if I try to login to Twitter and click back button on WP8
                // TODO: then System.InvalidOperationException is thrown but is NOT caught here!??  Why?
                // TODO: Must the try catch be in the platform-specific authprovider?

                //user cancelled so try again
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
        }

        void HandleLoginResult(Task<LoginResult> task)
        {

            if (task.Status == TaskStatus.RanToCompletion 
                && task.Result != null 
                && !string.IsNullOrEmpty(task.Result.IdentityString))
            {
                ////Save our app settings for next launch
                //var settings = this.GetService<ISettingsProvider>();

                //settings.UserId = task.Result.UserId;

                //if (platform != null)
                //    settings.AuthenticationProvider = (int)platform.Provider;

                //settings.Save();



                // when the LoginResult comes back, set the above UserIdentity property (TODO: for now)
                // TODO: may want to be setting/saving those results elsewhere because 
                // If login works we no longer want to be on the LoginViewModel/View
                // maybe that is where we put stuff in the "AppViewModel"??



                Debug.WriteLine("Hey Made it to Handle Login using Continue With!");

                UserIdentity = task.Result.IdentityString;
                AzureToken = task.Result.MobileServicesUserToken;

                IsBusy = false;


                // TODO: Older syntax or something different?
                //Navigate to the Lists view
                // RequestNavigate<WishListsViewModel>();

                // TODO: Want to include a default App "Home Page" that I can navigate to
                // TODO: post login and let user change it or?
                ShowViewModel<HomeViewModel>();

            }
            else
            {
                // TODO: Determine Error handling approach
                // TODO: What should be done when this happens? Just message and sit at login screen?
                //Show Error
                //ReportError("Login Failed!");
                Debug.WriteLine("Ooopps! Made it back to LoginViewModel but LoginResult was null!");

                // TODO: Seems to just be sitting on LoginView if back was hit
                // TODO: Should I just all Logout to try to clear things
                // TODO: after some kind of message and they are ready to try again with a provider?

                _loginService.Logout();

                IsBusy = false;
            }
        }
    }
}


