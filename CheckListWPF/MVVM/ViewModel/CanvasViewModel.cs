using CheckListWPF.Resources;
using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
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
        private IEventAggregator _eventAggregator;

        public event EventHandler<EventArgs<string>>? ViewChanged;

        public CanvasViewModel(IEventAggregator eventAggregator, string pageIndex = "2")
        {
            _eventAggregator = eventAggregator;

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
                    _openAccountCommand = new RelayCommand(p => 
                    {
                        _eventAggregator.GetEvent<ResetSelectedCanvasEvent>().Publish();
                        ViewChanged?.Invoke(this, new EventArgs<string>("1")); 
                    }, 
                    p => true);
                }

                return _openAccountCommand;
            }

        }

    }
}
