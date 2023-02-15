using CheckListApi.Models;
using CheckListWPF.MVVM.Model;
using CheckListWPF.Resources;
using CheckListWPF.Resources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CheckListWPF.MVVM.ViewModel.ActionViewModels
{
    public class EditTaskViewModel : ActionViewModel
    {
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly TaskDisplayModel _task;
        private string _title;
        private string _description;
        private bool _notStarted;
        private bool _inProgress;
        private bool _isDone;
        private ICommand _editTaskCommand;
        private ICommand _checkDoneCommand;
        private ICommand _checkInProgressCommand;
        private ICommand _checkNotStartedCommand;
        

        public EditTaskViewModel(ICheckListEndpoint checkListEndpoint, TaskDisplayModel task)
        {
            _checkListEndpoint = checkListEndpoint;
            _task = task;

            Title = task.Title;
            Description = task.Description;
            NotStarted = task.NotStarted;
            InProgress = task.InProgress;
            IsDone = task.IsDone;
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

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool NotStarted
        {
            get { return _notStarted; }
            set
            {
                _notStarted = value;
                OnPropertyChanged(nameof(NotStarted));
            }
        }

        public bool InProgress 
        {
            get { return _inProgress; }
            set
            {
                _inProgress = value;
                OnPropertyChanged(nameof(InProgress));
            }
        }

        public bool IsDone 
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }


        public ICommand EditTaskCommand
        {
            get
            {
                if (_editTaskCommand is null)
                {
                    _editTaskCommand = new RelayCommand(p => EditTask(), p => true);
                }

                return _editTaskCommand;
            }
        }

        public ICommand CheckDoneCommand
        {
            get
            {
                if (_checkDoneCommand is null)
                {
                    _checkDoneCommand = new RelayCommand(p =>
                    {
                        IsDone = true;
                        InProgress = false;
                        NotStarted = false;
                    }, p => true);
                }

                return _checkDoneCommand;
            }
        }

        public ICommand CheckInProgressCommand
        {
            get
            {
                if (_checkInProgressCommand is null)
                {
                    _checkInProgressCommand = new RelayCommand(p =>
                    {
                        IsDone = false;
                        InProgress = true;
                        NotStarted = false;
                    }, p => true);
                }

                return _checkInProgressCommand;
            }
        }

        public ICommand CheckNotStartedCommand
        {
            get
            {
                if (_checkNotStartedCommand is null)
                {
                    _checkNotStartedCommand = new RelayCommand(p =>
                    {
                        IsDone = false;
                        InProgress = false;
                        NotStarted = true;
                    }, p => true);
                }

                return _checkNotStartedCommand;
            }
        }

        private async void EditTask()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                _task.Title = Title;
                _task.Description = Description;
                _task.NotStarted = NotStarted;
                _task.InProgress = InProgress;
                _task.IsDone = IsDone;

                await _checkListEndpoint.UpdateTask(_task);

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
