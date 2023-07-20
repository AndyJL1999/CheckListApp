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
    public partial class TaskControl : UserControl
    {
        private ICommand _changeStatusCommand;


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
            TaskControl taskCtrl = d as TaskControl;
            if (taskCtrl != null)
            {
                taskCtrl.DataContext = taskCtrl.Task;
                taskCtrl.CheckStatus(taskCtrl.Task);
            }
        }


        public ICommand DeleteTaskProp
        {
            get { return (ICommand)GetValue(DeleteTaskProperty); }
            set { SetValue(DeleteTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteTaskProp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteTaskProperty =
            DependencyProperty.Register("DeleteTaskProp", typeof(ICommand), typeof(TaskControl), new PropertyMetadata(null));

        public ICommand ChangeStatusProp
        {
            get { return (ICommand)GetValue(ChangeStatusProperty); }
            set { SetValue(ChangeStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteTaskProp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeStatusProperty =
            DependencyProperty.Register("ChangeStatusProp", typeof(ICommand), typeof(TaskControl), new PropertyMetadata(null));

        public TaskControl()
        {
            InitializeComponent();
        }

        public bool NotStarted { get; set; }
        public bool InProgress { get; set; }
        public bool IsDone { get; set; }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;

            if (btn.BorderBrush == Brushes.Red)
            {
                Task.Status = CheckListApi.Models.StatusEnum.NotStarted;
            }
            else if (btn.BorderBrush == Brushes.Orange)
            {
                Task.Status = CheckListApi.Models.StatusEnum.InProgress;
            }
            else if (btn.BorderBrush == Brushes.Green)
            {
                Task.Status = CheckListApi.Models.StatusEnum.IsDone;
            }
        }

        private void CheckStatus(TaskDisplayModel task)
        {
            switch (task.Status)
            {
                case CheckListApi.Models.StatusEnum.NotStarted:
                    NotStarted = true;
                    InProgress = false;
                    IsDone = false;
                    break;
                case CheckListApi.Models.StatusEnum.InProgress:
                    InProgress = true;
                    NotStarted = false;
                    IsDone = false;
                    break;
                case CheckListApi.Models.StatusEnum.IsDone:
                    IsDone = true;
                    NotStarted = false;
                    InProgress = false;
                    break;
                default:
                    NotStarted = true;
                    InProgress = false;
                    IsDone = false;
                    break;
            }
        }
    }
}
