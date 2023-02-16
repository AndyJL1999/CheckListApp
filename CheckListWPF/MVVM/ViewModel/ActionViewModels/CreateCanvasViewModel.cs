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
    public class CreateCanvasViewModel : ActionViewModel
    {
        #region ----------Fields----------
        private readonly ICheckListEndpoint _checkListEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _createCanvasCommand;
        private string _title;
        #endregion

        public CreateCanvasViewModel(ICheckListEndpoint checkListEndpoint, IEventAggregator eventAggregator)
        {
            _checkListEndpoint = checkListEndpoint;
            _eventAggregator = eventAggregator;

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

        public ICommand CreateCanvasCommand
        {
            get
            {
                if (_createCanvasCommand is null)
                {
                    _createCanvasCommand = new RelayCommand(p => CreateNewCanvas(), p => true);
                }

                return _createCanvasCommand;
            }

        }
        #endregion

        #region ----------Methods----------
        public async void CreateNewCanvas()
        {
            if (string.IsNullOrEmpty(Title) == false && Title.Length <= 25)
            {
                ErrorVisibility = Visibility.Collapsed;

                await _checkListEndpoint.AddCanvasToList(Title);
                _eventAggregator.GetEvent<ResetCanvasListEvent>().Publish();

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
