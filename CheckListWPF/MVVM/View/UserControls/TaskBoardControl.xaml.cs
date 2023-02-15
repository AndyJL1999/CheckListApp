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
    /// Interaction logic for TaskBoardControl.xaml
    /// </summary>
    public partial class TaskBoardControl : UserControl, INotifyPropertyChanged
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



        public ICommand AddCommandProp
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("AddCommandProp", typeof(ICommand), typeof(TaskBoardControl), new PropertyMetadata(null));



        public ICommand DeleteCommandProp
        {
            get { return (ICommand)GetValue(DeleteProperty); }
            set { SetValue(DeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteProperty =
            DependencyProperty.Register("DeleteCommandProp", typeof(ICommand), typeof(TaskBoardControl), new PropertyMetadata(null));



        public ICommand DeleteTaskCommandProp
        {
            get { return (ICommand)GetValue(DeleteTaskCommandPropProperty); }
            set { SetValue(DeleteTaskCommandPropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteTaskCommandProp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteTaskCommandPropProperty =
            DependencyProperty.Register("DeleteTaskCommandProp", typeof(ICommand), typeof(TaskBoardControl), new PropertyMetadata(null));




        public ICommand EditTaskCommandProp
        {
            get { return (ICommand)GetValue(EditTaskCommandPropProperty); }
            set { SetValue(EditTaskCommandPropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditTaskCommandProp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTaskCommandPropProperty =
            DependencyProperty.Register("EditTaskCommandProp", typeof(ICommand), typeof(TaskBoardControl), new PropertyMetadata(null));


        private ICommand _collapseBoardCommand;
        private Visibility _boardVisibility;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TaskBoardControl()
        {
            InitializeComponent();
        }

        public ICommand CollapseBoardCommand 
        { 
            get {
                if (_collapseBoardCommand is null)
                {
                    _collapseBoardCommand = new RelayCommand(p => CollapseBoard(), p => true);
                }

                return _collapseBoardCommand;
            }
        }

        public Visibility BoardVisibility 
        { 
            get { return _boardVisibility; }
            set
            {
                _boardVisibility = value;
                OnPropertyChanged(nameof(BoardVisibility));
            }
        }

        private void CollapseBoard()
        {
            if (BoardVisibility == Visibility.Collapsed)
            {
                BoardVisibility = Visibility.Visible;
            }
            else
            {
                BoardVisibility = Visibility.Collapsed;
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
