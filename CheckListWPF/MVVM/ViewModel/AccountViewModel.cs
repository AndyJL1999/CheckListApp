using CheckListWPF.Resources;
using CheckListWPF.Resources.Interfaces;
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
    public class AccountViewModel : ObservableObject, IPageViewModel
    {
        private ICommand _openEditCommand;
        private ICommand _openAddCanvasCommand;
        private ICommand _openCanvasCommand;

        public event EventHandler<EventArgs<string>>? ViewChanged;

        public AccountViewModel(string pageIndex = "1")
        {
            PageId = pageIndex;
            PageName = "AccountView";
        }

        public string PageId { get; set; }
        public string PageName { get; set; }

        public ICommand OpenEditCommand 
        {
            get
            {
                if(_openEditCommand is null)
                {
                    _openEditCommand = new RelayCommand(p => OpenEditAccount(), p => true);
                }

                return _openEditCommand;
            }

        }

        public ICommand OpenAddCanvasCommand
        {
            get
            {
                if (_openAddCanvasCommand is null)
                {
                    _openAddCanvasCommand = new RelayCommand(p => OpenAddCanvas(), p => true);
                }

                return _openAddCanvasCommand;
            }

        }

        public ICommand OpenCanvasCommand
        {
            get
            {
                if (_openCanvasCommand is null)
                {
                    _openCanvasCommand = new RelayCommand(p => { ViewChanged?.Invoke(this, new EventArgs<string>("2")); }, p => true);
                }

                return _openCanvasCommand;
            }
        }


        private void OpenAddCanvas()
        {
            OpenWindow(new CreateCanvasViewModel());
        }

        private void OpenEditAccount()
        {
            OpenWindow(new EditAccountViewModel());
        }

        private void OpenWindow(ObservableObject viewModel)
        {
            if (App.Current.MainWindow.OwnedWindows.Count == 0)
            {
                var w = new Window();
                w.WindowStyle = WindowStyle.None;
                w.ResizeMode = ResizeMode.NoResize;
                w.AllowsTransparency = true;
                w.Background = new SolidColorBrush(Colors.Transparent);
                w.Owner = App.Current.MainWindow;
                w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                w.Content = viewModel;
                w.ShowDialog();
            }
        }
    }
}
