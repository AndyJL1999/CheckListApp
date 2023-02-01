using CheckListWPF.Resources;
using CheckListWPF.Resources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.MVVM.ViewModel
{
    public class StartUpViewModel : ObservableObject, IPageViewModel
    {
        public event EventHandler<EventArgs<string>>? ViewChanged;

        public StartUpViewModel(string pageIndex = "0")
        {
            PageId = pageIndex;
            PageName = "StartUpView";
        }

        public string PageId { get; set; }
        public string PageName { get; set; }
    }
}
