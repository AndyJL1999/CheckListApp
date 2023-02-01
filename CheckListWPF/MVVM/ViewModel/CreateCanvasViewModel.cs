using CheckListWPF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CheckListWPF.MVVM.ViewModel
{
    public class CreateCanvasViewModel : ObservableObject
    {
        private ICommand _closeWindowCommand;

        public CreateCanvasViewModel()
        {

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
            var w = App.Current.MainWindow.OwnedWindows[0];
            w.Close();
        }
    }
}
