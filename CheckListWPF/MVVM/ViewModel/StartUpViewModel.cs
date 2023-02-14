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
        private string _username;
        private string _email;
        private string _password;
        private string _resultMessage;
        private ICommand _goToAccountCommand;
        private ICommand _showHiddenFormCommand;
        private Visibility _signUpVisibility;
        private Visibility _loginVisibility;
        private bool _onLoginForm;
        private SolidColorBrush _resultColor;
        private readonly IApiHelper _apiHelper;
        private readonly IEventAggregator _eventAggregator;

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

            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

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

        public string Password 
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

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

        private async Task ChangeView()
        {
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

    }
}
