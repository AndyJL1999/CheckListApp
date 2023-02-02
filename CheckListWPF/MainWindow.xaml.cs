using AutoMapper;
using CheckListWPF.MVVM.ViewModel;
using CheckListWPF.Resources;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckListWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IApiHelper _apiHelper;
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;

        public MainWindow(IApiHelper apiHelper, ICheckListEndpoint checkListEndpoint, IMapper mapper, 
            IEventAggregator eventAggregator)
        {
            InitializeComponent();

            _apiHelper = apiHelper;
            _checkListEndpoint = checkListEndpoint;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            DataContext = new MainViewModel(new DataModel{ Data = "Data: " }, _apiHelper, _checkListEndpoint, _mapper, _eventAggregator);
        }
    }
}
