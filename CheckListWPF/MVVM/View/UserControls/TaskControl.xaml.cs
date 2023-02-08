using CheckListWPF.MVVM.Model;
using CheckListWPF.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for TaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl, INotifyPropertyChanged
    {
        public TaskDisplayModel Task
        {
            get { return (TaskDisplayModel)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(TaskDisplayModel), typeof(TaskControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskControl canvasCtrl = d as TaskControl;
            if (canvasCtrl != null)
            {
                canvasCtrl.DataContext = canvasCtrl.Task;
                canvasCtrl.GreenCheck = canvasCtrl.Task.IsDone == true ? true : false;
                canvasCtrl.OrangeCheck = canvasCtrl.Task.InProgress == true ? true : false;
                canvasCtrl.RedCheck = canvasCtrl.Task.InProgress == false ? true : false;
            }
        }


        public ICommand DeleteTaskProp
        {
            get { return (ICommand)GetValue(DeleteTaskPropProperty); }
            set { SetValue(DeleteTaskPropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteTaskProp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteTaskPropProperty =
            DependencyProperty.Register("DeleteTaskProp", typeof(ICommand), typeof(TaskControl), new PropertyMetadata(null));


        private bool _greenCheck;
        private bool _orangeCheck;
        private bool _redCheck;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TaskControl()
        {
            InitializeComponent();
        }

        public bool GreenCheck 
        {
            get { return _greenCheck; }
            set
            {
                _greenCheck = value;
                OnPropertyChanged(nameof(GreenCheck));
            }
        }
        public bool OrangeCheck
        {
            get { return _orangeCheck; }
            set
            {
                _orangeCheck = value;
                OnPropertyChanged(nameof(OrangeCheck));
            }
        }
        public bool RedCheck 
        {
            get { return _redCheck; }
            set
            {
                _redCheck = value;
                OnPropertyChanged(nameof(RedCheck));
            }
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
