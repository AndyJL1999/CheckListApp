using CheckListWPF.MVVM.Model;
using CheckListWPF.Resources;
using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CheckListWPF.MVVM.ViewModel.ActionViewModels
{
    public class EditTaskBoardViewModel : ActionViewModel
    {
        #region ----------Fields----------
        private readonly ICheckListEndpoint _checkListEndpoint;
        private ICommand _editBoardCommand;
        private readonly TaskBoardDisplayModel _taskBoard;
        private string _title;
        #endregion

        public EditTaskBoardViewModel(ICheckListEndpoint checkListEndpoint, TaskBoardDisplayModel taskBoard)
        {
            _checkListEndpoint = checkListEndpoint;
            _taskBoard = taskBoard;

            Title = _taskBoard.Title;
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

        public ICommand EditBoardCommand
        {
            get
            {
                if(_editBoardCommand is null)
                {
                    _editBoardCommand = new RelayCommand(p => EditTaskBoard(), p => true);
                }

                return _editBoardCommand;
            }
        }
        #endregion

        #region ----------Methods----------
        private async void EditTaskBoard()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                await _checkListEndpoint.UpdateBoard(_taskBoard.Id, Title);

                _taskBoard.Title = Title;

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
