using CheckListWPF.MVVM.Model;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckListWPF.MVVM.View.UserControls
{
    /// <summary>
    /// Interaction logic for TaskBoardControl.xaml
    /// </summary>
    public partial class TaskBoardControl : UserControl
    {
        public TaskBoardDisplayModel TaskBoard
        {
            get { return (TaskBoardDisplayModel)GetValue(TaskBoardProperty); }
            set { SetValue(TaskBoardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskBoardProperty =
            DependencyProperty.Register("TaskBoard", typeof(TaskBoardDisplayModel), typeof(TaskBoardControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskBoardControl canvasCtrl = d as TaskBoardControl;
            if (canvasCtrl != null)
            {
                canvasCtrl.DataContext = canvasCtrl.TaskBoard;
            }
        }

        public TaskBoardControl()
        {
            InitializeComponent();
        }
    }
}
