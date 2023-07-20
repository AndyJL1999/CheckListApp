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
        #region ----------Fields----------
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly TaskDisplayModel _task;
        private string _title;
        private string _description;
        private ICommand _editTaskCommand;
        private ICommand _checkDoneCommand;
        private ICommand _checkInProgressCommand;
        private ICommand _checkNotStartedCommand;
        #endregion

        public EditTaskViewModel(ICheckListEndpoint checkListEndpoint, TaskDisplayModel task)
        {
            _checkListEndpoint = checkListEndpoint;
            _task = task;

            Title = task.Title;
            Description = task.Description;

            ErrorVisibility = Visibility.Collapsed;
        }

        #region ----------Properties----------
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
        #endregion

        #region ----------Methods----------
        private async void EditTask()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                _task.Title = Title;
                _task.Description = Description;

                await _checkListEndpoint.UpdateTask(_task);

                CloseWindow();
            }
            else
            {
                ErrorMessage = "Title length must be more than 0 and less than 25 characters";
                ErrorVisibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
