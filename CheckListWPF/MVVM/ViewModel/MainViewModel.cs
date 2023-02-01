using CheckListWPF.Resources;
using CheckListWPF.Resources.Interfaces;
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
        private IPageViewModel _viewModel;
        private readonly Dictionary<string, IPageViewModel> _pageViewModels = new();

        public MainViewModel(IDataModel pageViews)
        {
            //Setting all available views
            SetNavigation(pageViews);

        }


        public IPageViewModel ViewModel 
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }


        private void SetNavigation(IDataModel pageViews)
        {
            _pageViewModels["0"] = new StartUpViewModel();
            _pageViewModels["0"].ViewChanged += (o, s) =>
            {
                ViewModel = _pageViewModels[s.Value];
                pageViews.Data = "Data: " + s.Value.ToString();
            };

            _pageViewModels["1"] = new AccountViewModel();
            _pageViewModels["1"].ViewChanged += (o, s) =>
            {
                ViewModel = _pageViewModels[s.Value];
                pageViews.Data = "Data: " + s.Value.ToString();
            };

            _pageViewModels["2"] = new CanvasViewModel();
            _pageViewModels["2"].ViewChanged += (o, s) =>
            {
                ViewModel = _pageViewModels[s.Value];
                pageViews.Data = "Data: " + s.Value.ToString();
            };

            ViewModel = _pageViewModels["1"];
        }

    }
}
