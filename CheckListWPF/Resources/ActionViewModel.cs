using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CheckListWPF.Resources
{
    public class ActionViewModel : ObservableObject
    {
        private ICommand _closeWindowCommand;
        private Visibility _errorVisibility;
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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

        public void CloseWindow()
        {
            var w = Application.Current.MainWindow.OwnedWindows[0];
            w.Close();
        }
    }
}
