using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckListWPF.Resources;

namespace CheckListWPF.Resources.Interfaces
{
    public interface IPageViewModel
    {
        event EventHandler<EventArgs<string>>? ViewChanged;
        string PageId { get; set; }
        string PageName { get; set; }
    }
}
