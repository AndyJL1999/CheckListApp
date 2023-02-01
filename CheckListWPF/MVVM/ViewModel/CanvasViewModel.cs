using CheckListWPF.Resources;
using CheckListWPF.Resources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CheckListWPF.MVVM.ViewModel
{
    public class CanvasViewModel : ObservableObject, IPageViewModel
    {
        private ICommand _openAccountCommand;

        public event EventHandler<EventArgs<string>>? ViewChanged;

        public CanvasViewModel(string pageIndex = "2")
        {
            PageId = pageIndex;
            PageName = "CanvasView";
        }

        public string PageId { get; set; }
        public string PageName { get; set; }

        public ICommand OpenAccountCommand
        {
            get
            {
                if (_openAccountCommand is null)
                {
                    _openAccountCommand = new RelayCommand(p => { ViewChanged?.Invoke(this, new EventArgs<string>("1")); }, p => true);
                }

                return _openAccountCommand;
            }

        }

    }
}
