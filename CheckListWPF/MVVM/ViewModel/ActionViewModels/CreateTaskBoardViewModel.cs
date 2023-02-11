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
using System.Windows;
using CheckListWPF.MVVM.Model;

namespace CheckListWPF.MVVM.ViewModel.ActionViewModels
{
    public class CreateTaskBoardViewModel : ObservableObject
    {
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _closeWindowCommand;
        private ICommand _createTaskBoardCommand;
        private Visibility _errorVisibility;
        private string _errorMessage;
        private string _title;

        public CreateTaskBoardViewModel(ICheckListEndpoint checkListEndpoint, IEventAggregator eventAggregator, int canvasId)
        {
            _checkListEndpoint = checkListEndpoint;
            _eventAggregator = eventAggregator;

            ErrorVisibility = Visibility.Collapsed;
            CanvasId = canvasId;
        }

        public int CanvasId { get; set; }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public Visibility ErrorVisibility
        {
            get { return _errorVisibility; }
            set
            {
                _errorVisibility = value;
                OnPropertyChanged(nameof(ErrorVisibility));
            }
        }

        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand is null)
                {
                    _closeWindowCommand = new RelayCommand(p => CloseWindow(), p => true);
                }

                return _closeWindowCommand;
            }

        }

        public ICommand CreateTaskBoardCommand
        {
            get
            {
                if (_createTaskBoardCommand is null)
                {
                    _createTaskBoardCommand = new RelayCommand(p => CreateNewTaskBoard(), p => true);
                }

                return _createTaskBoardCommand;
            }

        }

        private void CloseWindow()
        {
            var w = Application.Current.MainWindow.OwnedWindows[0];
            w.Close();
        }

        private async void CreateNewTaskBoard()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                await _checkListEndpoint.AddTaskBoardToCanvas(Title, CanvasId);

                _eventAggregator.GetEvent<ResetTaskBoardsEvent>().Publish();

                CloseWindow();
            }
            else
            {
                ErrorMessage = "Title length must be more than 0 and less than 25 characters";
                ErrorVisibility = Visibility.Visible;
            }
        }
    }
}
