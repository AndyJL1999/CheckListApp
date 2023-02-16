using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckListWPF.MVVM.View.ActionViews
{
    /// <summary>
    /// Interaction logic for EditTaskBoardView.xaml
    /// </summary>
    public partial class EditTaskBoardView : UserControl
    {
        public EditTaskBoardView()
        {
            InitializeComponent();

            Storyboard sb = this.FindResource("ActionView_Anim") as Storyboard;

            if (sb != null)
            {
                sb.Begin();
            }
        }
    }
}
