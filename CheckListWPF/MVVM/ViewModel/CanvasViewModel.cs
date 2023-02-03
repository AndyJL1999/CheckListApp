using AutoMapper;
using CheckListApi.Models;
using CheckListWPF.MVVM.Model;
using CheckListWPF.MVVM.ViewModel.ActionViewModels;
using CheckListWPF.Resources;
using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Helpers;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckListWPF.MVVM.ViewModel
{
    public class CanvasViewModel : ObservableObject, IPageViewModel
    {
        private ICommand _openAccountCommand;
        private ICommand _openAddTaskBoardCommand;
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IMapper _mapper;
        private IEventAggregator _eventAggregator;
        private string _canvasTitle;
        private ObservableCollection<TaskBoardDisplayModel> _taskBoards;
        
        public event EventHandler<EventArgs<string>>? ViewChanged;

        public CanvasViewModel(ICheckListEndpoint checkListEndpoint, IMapper mapper, IEventAggregator eventAggregator, string pageIndex = "2")
        {
            _checkListEndpoint = checkListEndpoint;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            PageId = pageIndex;
            PageName = "CanvasView";

            _eventAggregator.GetEvent<CanvasCarrierEvent>().Subscribe(c =>
            {
                CanvasId = c.Id;
                CanvasTitle = c.Title;
                SetTaskBoardList();
            });
        }

        public string PageId { get; set; }
        public string PageName { get; set; }
        public int CanvasId { get; set; }

        public string CanvasTitle 
        {
            get { return _canvasTitle; }
            set
            {
                _canvasTitle = value;
                OnPropertyChanged(nameof(CanvasTitle));
            }
        }

        public ObservableCollection<TaskBoardDisplayModel> TaskBoards 
        {
            get { return _taskBoards; }
            set
            {
                _taskBoards = value;
                OnPropertyChanged(nameof(TaskBoards));
            }
        }

        public ICommand OpenAccountCommand
        {
            get
            {
                if (_openAccountCommand is null)
                {
                    _openAccountCommand = new RelayCommand(p => 
                    {
                        _eventAggregator.GetEvent<ResetCanvasListEvent>().Publish();
                        ViewChanged?.Invoke(this, new EventArgs<string>("1")); 
                    }, 
                    p => true);
                }

                return _openAccountCommand;
            }
        }

        public ICommand OpenAddTaskBoardCommand
        {
            get
            {
                if (_openAddTaskBoardCommand is null)
                {
                    _openAddTaskBoardCommand = new RelayCommand(p => OpenAddTaskBoard(), p => true);
                }

                return _openAddTaskBoardCommand;
            }

        }

        private void OpenAddTaskBoard()
        {
            OpenWindow(new CreateTaskBoardViewModel(_checkListEndpoint, _eventAggregator, CanvasId));
        }

        private void OpenAddTask()
        {
            //OpenWindow();
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

        private async void SetTaskBoardList()
        {
            var payload = await _checkListEndpoint.GetCanvasTaskBoardList(CanvasId);
            var taskBoards = _mapper.Map<IEnumerable<TaskBoardDisplayModel>>(payload);

            TaskBoards = new ObservableCollection<TaskBoardDisplayModel>(taskBoards);
        }
    }
}
