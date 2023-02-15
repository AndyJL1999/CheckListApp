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
    public class CreateTaskBoardViewModel : ActionViewModel
    {
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _createTaskBoardCommand;
        private string _title;

        public CreateTaskBoardViewModel(ICheckListEndpoint checkListEndpoint, IEventAggregator eventAggregator, int canvasId)
        {
            _checkListEndpoint = checkListEndpoint;
            _eventAggregator = eventAggregator;

            ErrorVisibility = Visibility.Collapsed;
            CanvasId = canvasId;
        }

        public int CanvasId { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
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

        private async void CreateNewTaskBoard()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                //await _checkListEndpoint.AddTaskBoardToCanvas(Title, CanvasId);

                _eventAggregator.GetEvent<AddTaskBoardEvent>().Publish(new TaskBoardDisplayModel
                {
                    Title = Title,
                    CanvasId = CanvasId,
                    Tasks = new List<TaskDisplayModel>()
                });

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
