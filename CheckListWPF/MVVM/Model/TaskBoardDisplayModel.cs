using CheckListApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.MVVM.Model
{
    public class TaskBoardDisplayModel :INotifyPropertyChanged
    {
        private string _title;
        private List<TaskDisplayModel> _tasks;

        public int Id { get; set; }

        public int CanvasId { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                INotifyPropertyChanged(nameof(Title));
            }
        }
        public List<TaskDisplayModel> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                INotifyPropertyChanged(nameof(Tasks));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void INotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
