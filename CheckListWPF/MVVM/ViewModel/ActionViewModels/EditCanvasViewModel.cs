using CheckListWPF.MVVM.Model;
using CheckListWPF.Resources.Interfaces;
using CheckListWPF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace CheckListWPF.MVVM.ViewModel.ActionViewModels
{
    public class EditCanvasViewModel : ActionViewModel
    {
        #region ----------Fields----------
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly CanvasDisplayModel _canvas;
        private ICommand _editCanvasCommand;
        private string _title;
        #endregion

        public EditCanvasViewModel(ICheckListEndpoint checkListEndpoint, CanvasDisplayModel canvas)
        {
            _checkListEndpoint = checkListEndpoint;
            _canvas = canvas;

            Title = _canvas.Title;
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

        public ICommand EditCanvasCommand
        {
            get
            {
                if (_editCanvasCommand is null)
                {
                    _editCanvasCommand = new RelayCommand(p => EditCanvas(), p => true);
                }

                return _editCanvasCommand;
            }
        }
        #endregion

        #region ----------Methods----------
        private async void EditCanvas()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                await _checkListEndpoint.UpdateCanvas(_canvas.Id, Title);

                _canvas.Title = Title;

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
