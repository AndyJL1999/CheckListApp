using AutoMapper;
using CheckListWPF.Resources;
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
    public class MainViewModel : ObservableObject
    {
        #region ----------Fields----------
        private IPageViewModel _viewModel;
        private readonly IApiHelper _apiHelper;
        private ICheckListEndpoint _checkListEndpoint;
        private IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly Dictionary<string, IPageViewModel> _pageViewModels = new();
        #endregion

        public MainViewModel(IDataModel pageViews, IApiHelper apiHelper, ICheckListEndpoint checkListEndpoint,
            IMapper mapper, IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            _checkListEndpoint = checkListEndpoint;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            //Setting all available views
            SetNavigation(pageViews);

        }

        #region ----------Properties----------
        public IPageViewModel ViewModel 
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }
        #endregion

        #region ----------Methods----------
        private void SetNavigation(IDataModel pageViews)
        {
            _pageViewModels["0"] = new StartUpViewModel(_apiHelper, _eventAggregator);
            _pageViewModels["0"].ViewChanged += (o, s) =>
            {
                ViewModel = _pageViewModels[s.Value];
                pageViews.Data = "Data: " + s.Value.ToString();
            };

            _pageViewModels["1"] = new AccountViewModel(_apiHelper, _checkListEndpoint, _mapper, _eventAggregator);
            _pageViewModels["1"].ViewChanged += (o, s) =>
            {
                ViewModel = _pageViewModels[s.Value];
                pageViews.Data = "Data: " + s.Value.ToString();
            };

            _pageViewModels["2"] = new CanvasViewModel(_checkListEndpoint, _mapper, _eventAggregator);
            _pageViewModels["2"].ViewChanged += (o, s) =>
            {
                ViewModel = _pageViewModels[s.Value];
                pageViews.Data = "Data: " + s.Value.ToString();
            };

            ViewModel = _pageViewModels["0"];
        }
        #endregion
    }
}
