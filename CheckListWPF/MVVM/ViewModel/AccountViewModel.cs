using CheckListWPF.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckListWPF.MVVM.ViewModel
{
    public class AccountViewModel : ObservableObject
    {
        private ICommand _openWindowCommand;

        public AccountViewModel()
        {

        }

        public ICommand OpenWindowCommand 
        {
            get
            {
                if(_openWindowCommand is null)
                {
                    _openWindowCommand = new RelayCommand(p => OpenWindow(), p => true);
                }

                return _openWindowCommand;
            }

        }

        public void OpenWindow()
        {
            if (App.Current.MainWindow.OwnedWindows.Count == 0)
            {
                var w = new Window();
                w.WindowStyle = WindowStyle.None;
                w.ResizeMode = ResizeMode.NoResize;
                w.AllowsTransparency = true;
                w.Background = new SolidColorBrush(Colors.Transparent);
                w.MaxHeight = 300;
                w.MaxWidth = 300;
                w.Owner = App.Current.MainWindow;
                w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                w.Content = new EditAccountViewModel();
                w.ShowDialog();
            }
        }
    }
}
