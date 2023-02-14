﻿using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Interfaces;
using CheckListWPF.Resources;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace CheckListWPF.MVVM.ViewModel.ActionViewModels
{
    public class CreateTaskViewModel : ActionViewModel
    {
        
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _createTaskCommand;
        private string _title;
        private string _description;
        private bool _inProgress;
        private bool _isDone;

        public CreateTaskViewModel(ICheckListEndpoint checkListEndpoint, IEventAggregator eventAggregator, int boardId)
        {
            _checkListEndpoint = checkListEndpoint;
            _eventAggregator = eventAggregator;

            ErrorVisibility = Visibility.Collapsed;
            BoardId = boardId;
        }

        public int BoardId { get; set; }

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


        public ICommand CreateTaskCommand
        {
            get
            {
                if (_createTaskCommand is null)
                {
                    _createTaskCommand = new RelayCommand(p => CreateNewTask(), p => true);
                }

                return _createTaskCommand;
            }

        }

        public async void CreateNewTask()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                await _checkListEndpoint.AddTaskToBoard(Title, Description, BoardId);

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
