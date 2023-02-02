using CheckListWPF.Resources;
using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CheckListWPF.MVVM.ViewModel
{
    public class StartUpViewModel : ObservableObject, IPageViewModel
    {
        private string _email;
        private string _password;
        private ICommand _goToAccountCommand;
        private readonly IApiHelper _apiHelper;
        private readonly IEventAggregator _eventAggregator;

        public event EventHandler<EventArgs<string>>? ViewChanged;

        public StartUpViewModel(IApiHelper apiHelper, IEventAggregator eventAggregator, string pageIndex = "0")
        {
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;

            PageId = pageIndex;
            PageName = "StartUpView";
        }

        public string PageId { get; set; }
        public string PageName { get; set; }

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

        public ICommand GoToAccountCommand 
        { 
            get
            {
                if(_goToAccountCommand is null)
                {
                    _goToAccountCommand = new RelayCommand(async p => 
                    {
                        var result = await _apiHelper.Authenticate(Email, Password);
                        await _apiHelper.GetUserInfo(result.Token);

                        _eventAggregator.GetEvent<LogOnEvent>().Publish();

                        ViewChanged?.Invoke(this, new EventArgs<string>("1")); 
                    }, 
                    p => true);
                }

                return _goToAccountCommand;
            }
        }

    }
}
