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
        #region ----------Fields----------
        private ICommand _openAccountCommand;
        private ICommand _openAddTaskBoardCommand;
        private ICommand _openAddTaskCommand;
        private ICommand _renameBoardCommand;
        private ICommand _deleteBoardCommand;
        private ICommand _deleteTaskCommand;
        private Visibility _editVisibility;
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        private string _canvasTitle;
        private ObservableCollection<TaskBoardDisplayModel> _taskBoards;
        private ICommand _editTaskCommand;
        #endregion

        public event EventHandler<EventArgs<string>>? ViewChanged;

        public CanvasViewModel(ICheckListEndpoint checkListEndpoint, IMapper mapper, IEventAggregator eventAggregator, string pageIndex = "2")
        {
            _checkListEndpoint = checkListEndpoint;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            PageId = pageIndex;
            PageName = "CanvasView";

            EditVisibility = Visibility.Collapsed;

            _eventAggregator.GetEvent<CanvasCarrierEvent>().Subscribe(c =>
            {
                CanvasId = c.Id;
                CanvasTitle = c.Title;
                SetTaskBoardList();
            });

            _eventAggregator.GetEvent<AddTaskBoardEvent>().Subscribe(t =>
            {
                //Adding new board to collection
                TaskBoards.Add(t);
            });

            _eventAggregator.GetEvent<AddTaskEvent>().Subscribe(t =>
            {
                var board = TaskBoards.FirstOrDefault(b => b.Id == t.BoardId);

                if (board != null)
                {
                    board.Tasks.Add(t);

                    var boards = TaskBoards;
                    TaskBoards = new ObservableCollection<TaskBoardDisplayModel>(boards);
                }
            });
        }

        #region ----------Properties----------
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
                        TaskBoards = null;
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

        public ICommand OpenAddTaskCommand
        {
            get
            {
                if (_openAddTaskCommand is null)
                {
                    _openAddTaskCommand = new RelayCommand(p => OpenAddTask((int)p), p => true);
                }

                return _openAddTaskCommand;
            }

        }

        public ICommand RenameBoardCommand
        {
            get
            {
                if(_renameBoardCommand is null)
                {
                    _renameBoardCommand = new RelayCommand(p => RenameBoard((TaskBoardDisplayModel)p), p => true);
                }

                return _renameBoardCommand;
            }
        }

        public ICommand EditTaskCommand 
        {
            get
            {
                if(_editTaskCommand is null)
                {
                    _editTaskCommand = new RelayCommand(p => EditTask((TaskDisplayModel)p), p => true);
                }

                return _editTaskCommand;
            }
        }

        public ICommand DeleteBoardCommand
        {
            get
            {
                if(_deleteBoardCommand is null)
                {
                    _deleteBoardCommand = new RelayCommand(p => DeleteTaskBoard((TaskBoardDisplayModel)p), p => true);
                }

                return _deleteBoardCommand;
            }
        }

        public ICommand DeleteTaskCommand 
        {
            get
            {
                if (_deleteTaskCommand is null)
                {
                    _deleteTaskCommand = new RelayCommand(p => DeleteTask((TaskDisplayModel)p), p => true);
                }

                return _deleteTaskCommand;
            }
        }

        public Visibility EditVisibility 
        {
            get { return _editVisibility; }
            set
            {
                _editVisibility = value;
                OnPropertyChanged(nameof(EditVisibility));
            } 
        }
        #endregion

        #region ----------Methods----------
        private void OpenAddTaskBoard()
        {
            OpenWindow(new CreateTaskBoardViewModel(_checkListEndpoint, _eventAggregator, CanvasId));
        }

        private void OpenAddTask(int boardId)
        {
            OpenWindow(new CreateTaskViewModel(_checkListEndpoint, _eventAggregator, boardId));
        }

        private async void DeleteTaskBoard(TaskBoardDisplayModel taskBoard)
        {
            if (taskBoard != null)
            {
                await _checkListEndpoint.DeleteBoardFromCanvas(CanvasId, taskBoard.Id);
                TaskBoards.Remove(taskBoard);
            }
        }

        private async void DeleteTask(TaskDisplayModel task)
        {
            var board = TaskBoards.FirstOrDefault(t => t.Id == task.BoardId);

            if(board != null)
            {
                await _checkListEndpoint.DeleteTaskFromBoard(task.BoardId, task.Id);

                if (board.Tasks.Remove(task))
                {
                    var boards = TaskBoards;
                    TaskBoards = new ObservableCollection<TaskBoardDisplayModel>(boards);
                }
            } 
        }

        private void OpenWindow(ActionViewModel viewModel)
        {
            if (Application.Current.MainWindow.OwnedWindows.Count == 0)
            {
                var w = new Window();
                w.WindowStyle = WindowStyle.None;
                w.ResizeMode = ResizeMode.NoResize;
                w.AllowsTransparency = true;
                w.Background = new SolidColorBrush(Colors.Transparent);
                w.Owner = Application.Current.MainWindow;
                w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                w.Content = viewModel;
                w.ShowDialog();
            }
        }

        private async void SetTaskBoardList()
        {
            //If Taskboards is null -> call api for Canvas TaskBoard List
            if (TaskBoards == null)
            {
                var payload = await _checkListEndpoint.GetCanvasTaskBoardList(CanvasId);
                var taskBoards = _mapper.Map<IEnumerable<TaskBoardDisplayModel>>(payload);

                TaskBoards = new ObservableCollection<TaskBoardDisplayModel>(taskBoards);
            }
        }

        private void RenameBoard(TaskBoardDisplayModel board)
        {
            OpenWindow(new EditTaskBoardViewModel(_checkListEndpoint, board));
        }

        private void EditTask(TaskDisplayModel task)
        {
            OpenWindow(new EditTaskViewModel(_checkListEndpoint, task));
        }
        #endregion
    }
}
