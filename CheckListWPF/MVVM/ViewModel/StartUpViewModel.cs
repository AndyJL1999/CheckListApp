using CheckListWPF.Resources;
using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckListWPF.MVVM.ViewModel
{
    public class StartUpViewModel : ObservableObject, IPageViewModel
    {
        #region ----------Fields----------
        private string _username;                                // username container
        private string _email;                                   // email container
        private string _password;                                // password container
        private string _resultMessage;                           // the message that is returned after a call to the api
        private ICommand _goToAccountCommand;                    // command for login button
        private ICommand _showHiddenFormCommand;                 // command to switch between forms
        private Visibility _signUpVisibility;                    // used to change signUp form visibility
        private Visibility _loginVisibility;                     // used to change login form visibility
        private Visibility _spinnerVisibility;                   // used to change loading spinner visibility
        private bool _onLoginForm;                               // used to determine which form is currently shown
        private bool _enableButton;                              // used to disable login and register button 
        private SolidColorBrush _resultColor;                    // used to change ResultMessage color
        private readonly IApiHelper _apiHelper;                  // used to access api calls for user
        private readonly IEventAggregator _eventAggregator;      // used to access events to communicate with other view models
        #endregion

        public event EventHandler<EventArgs<string>>? ViewChanged;

        public StartUpViewModel(IApiHelper apiHelper, IEventAggregator eventAggregator, string pageIndex = "0")
        {
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;

            _onLoginForm = false;

            PageId = pageIndex;
            PageName = "StartUpView";

            SignUpVisibility = Visibility.Visible;
            LoginVisibility = Visibility.Collapsed;
            SpinnerVisibility = Visibility.Collapsed;

            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;

            EnableButton = true;
        }

        #region ----------Properties----------
        public string PageId { get; set; }
        public string PageName { get; set; }

        public SolidColorBrush ResultColor 
        {
            get { return _resultColor; }
            set
            {
                _resultColor = value;
                OnPropertyChanged(nameof(ResultColor));
            }
        }

        public string Username 
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Email 
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password { private get; set; }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ResultMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public string ResultMessage 
        {
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged(nameof(ResultMessage));
                OnPropertyChanged(nameof(IsErrorVisible));
            }
        }

        public bool EnableButton 
        { 
            get { return _enableButton; }
            set
            {
                _enableButton = value;
                OnPropertyChanged(nameof(EnableButton));
            }
        }

        public Visibility LoginVisibility
        {
            get { return _loginVisibility; }
            set
            {
                _loginVisibility = value;
                OnPropertyChanged(nameof(LoginVisibility));
            }
        }

        public Visibility SignUpVisibility
        {
            get { return _signUpVisibility; }
            set
            {
                _signUpVisibility = value;
                OnPropertyChanged(nameof(SignUpVisibility));
            }
        }

        public Visibility SpinnerVisibility
        {
            get { return _spinnerVisibility; }
            set
            {
                _spinnerVisibility = value;
                OnPropertyChanged(nameof(SpinnerVisibility));
            }
        }

        public ICommand ShowHiddenFormCommand
        {
            get
            {
                if (_showHiddenFormCommand is null)
                {
                    _showHiddenFormCommand = new RelayCommand(p => ShowForm(), p => true);
                }

                return _showHiddenFormCommand;
            }
        }

        public ICommand GoToAccountCommand 
        { 
            get
            {
                if(_goToAccountCommand is null)
                {
                    _goToAccountCommand = new RelayCommand(p => ChangeView(), p => true);
                }

                return _goToAccountCommand;
            }
        }
        #endregion

        #region ----------Methods----------
        private async Task ChangeView()
        {
            SpinnerVisibility = Visibility.Visible;
            EnableButton = false;

            try
            {

                if (_onLoginForm)
                {
                    ResultMessage = string.Empty;
                    var result = await _apiHelper.Authenticate(Email, Password);

                    await _apiHelper.GetUserInfo(result.Token);

                    _eventAggregator.GetEvent<LogOnEvent>().Publish();

                    Username = string.Empty;
                    Email = string.Empty;
                    Password = string.Empty;

                    ViewChanged?.Invoke(this, new EventArgs<string>("1"));
                }
                else //on register form
                {
                    var result = await _apiHelper.Register(Username, Password, Email);

                    ResultColor = new SolidColorBrush(Colors.AliceBlue);

                    ResultMessage = result.Trim('"');
                }

            }
            catch (Exception ex)
            {
                ResultColor = new SolidColorBrush(Colors.Red);

                if (ex.Message == "Unauthorized")
                {
                    ResultMessage = "Wrong Email or Password";
                }

                if (_onLoginForm == true)
                {
                    ResultMessage = ResultMessage.Trim('"');
                }
                else
                {
                    ResultMessage = ex.Message.Trim('"');
                }
            }

            SpinnerVisibility = Visibility.Collapsed;
            EnableButton = true;
        }

        private void ShowForm()
        {
            _onLoginForm = !_onLoginForm;

            if (_onLoginForm)
            {
                SignUpVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
                ResultMessage = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                Email = string.Empty;
            }
            else
            {
                SignUpVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
                ResultMessage = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                Email = string.Empty;
            }

        }
        #endregion

    }
}
