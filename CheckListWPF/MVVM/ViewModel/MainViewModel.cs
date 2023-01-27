using CheckListWPF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ObservableObject ViewModel { get; set; }

        public MainViewModel(ObservableObject viewModel)
        {
            ViewModel = viewModel;
        }

    }
}
