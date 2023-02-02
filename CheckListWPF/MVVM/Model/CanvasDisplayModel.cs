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
    public class CanvasDisplayModel : INotifyPropertyChanged
    {
        private string _title;
        private List<TaskBoard> _taskBoards;

        public int Id { get; set; }

        public string Title 
        { 
            get { return _title; }
            set
            {
                _title = value;
                INotifyPropertyChanged(nameof(Title));
            } 
        }
        public List<TaskBoard> TaskBoards 
        { 
            get { return _taskBoards; }
            set
            {
                _taskBoards = value;
                INotifyPropertyChanged(nameof(TaskBoards));
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
