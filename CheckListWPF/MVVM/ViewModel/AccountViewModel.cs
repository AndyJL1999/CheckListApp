using AutoMapper;
using CheckListWPF.MVVM.Model;
using CheckListWPF.MVVM.ViewModel.ActionViewModels;
using CheckListWPF.Resources;
using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckListWPF.MVVM.ViewModel
{
    public class AccountViewModel : ObservableObject, IPageViewModel
    {
        #region ----------Fields----------
        private ICommand _openEditCommand;
        private ICommand _openAddCanvasCommand;
        private ICommand _goToStartUpCommand;
        private ObservableCollection<CanvasDisplayModel> _canvasList;
        private CanvasDisplayModel _selectedCanvas;
        private readonly IApiHelper _apiHelper;
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        private string _userWelcome;
        #endregion

        public event EventHandler<EventArgs<string>>? ViewChanged;

        public AccountViewModel(IApiHelper apiHelper, ICheckListEndpoint checkListEndpoint, IMapper mapper,  
            IEventAggregator eventAggregator, string pageIndex = "1")
        {
            _apiHelper = apiHelper;
            _checkListEndpoint = checkListEndpoint;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            PageId = pageIndex;
            PageName = "AccountView";

            _eventAggregator.GetEvent<LogOnEvent>().Subscribe(() => {

                _userWelcome = _apiHelper.LoggedInUser.Username;
                SetCanvasList();
            });

            _eventAggregator.GetEvent<ResetCanvasListEvent>().Subscribe(() => {

                //Unselect canvas and refresh canvas list
                SelectedCanvas = null;
                SetCanvasList();
            });
        }

        #region ----------Properties----------
        public string PageId { get; set; }
        public string PageName { get; set; }

        public string UserWelcome 
        { 
            get { return $"Welcome {_userWelcome}"; }
            set
            {
                _userWelcome = value;
                OnPropertyChanged(nameof(UserWelcome));
            }
        }

        public ObservableCollection<CanvasDisplayModel> CanvasList 
        {
            get { return _canvasList; }
            set
            {
                _canvasList = value;
                OnPropertyChanged(nameof(CanvasList));
            } 
        }

        public CanvasDisplayModel? SelectedCanvas
        {
            get { return _selectedCanvas; }
            set
            {
                _selectedCanvas = value;
                OnPropertyChanged(nameof(SelectedCanvas));

                if (SelectedCanvas != null)
                {
                    ViewChanged?.Invoke(this, new EventArgs<string>("2"));

                    //Carry Canvas data to the CanvasViewModel subscriber
                    _eventAggregator.GetEvent<CanvasCarrierEvent>().Publish(SelectedCanvas);
                }
            }
        }

        public ICommand OpenEditCommand 
        {
            get
            {
                if(_openEditCommand is null)
                {
                    _openEditCommand = new RelayCommand(p => OpenEditAccount(), p => true);
                }

                return _openEditCommand;
            }

        }

        public ICommand OpenAddCanvasCommand
        {
            get
            {
                if (_openAddCanvasCommand is null)
                {
                    _openAddCanvasCommand = new RelayCommand(p => OpenAddCanvas(), p => true);
                }

                return _openAddCanvasCommand;
            }

        }

        public ICommand GoToStartUpCommand
        {
            get
            {
                if(_goToStartUpCommand is null)
                {
                    _goToStartUpCommand = new RelayCommand(p =>
                    {
                        _apiHelper.LoggedInUser.Email = string.Empty;
                        _apiHelper.LoggedInUser.Username = string.Empty;
                        _apiHelper.LoggedInUser.Token = string.Empty;
                        _apiHelper.LoggedInUser.Id = 0;

                        ViewChanged?.Invoke(this, new EventArgs<string>("0"));

                    }, p => true);
                }

                return _goToStartUpCommand;
            }
        }
        #endregion

        #region ----------Methods----------
        private void OpenAddCanvas()
        {
            OpenWindow(new CreateCanvasViewModel(_checkListEndpoint, _eventAggregator));
        }

        private void OpenEditAccount()
        {
            OpenWindow(new EditAccountViewModel(_apiHelper));
        }

        private void OpenWindow(ObservableObject viewModel)
        {
            if (App.Current.MainWindow.OwnedWindows.Count == 0)
            {
                var w = new Window();
                w.WindowStyle = WindowStyle.None;
                w.ResizeMode = ResizeMode.NoResize;
                w.AllowsTransparency = true;
                w.Background = new SolidColorBrush(Colors.Transparent);
                w.Owner = App.Current.MainWindow;
                w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                w.Content = viewModel;
                w.ShowDialog();
            }
        }

        private async void SetCanvasList()
        {
            var payload = await _checkListEndpoint.GetUserCanvasList();
            var canvasList = _mapper.Map<IEnumerable<CanvasDisplayModel>>(payload);

            CanvasList = new ObservableCollection<CanvasDisplayModel>(canvasList);
        }
        #endregion
    }
}
