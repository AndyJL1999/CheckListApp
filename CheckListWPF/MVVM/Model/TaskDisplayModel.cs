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
    public class TaskDisplayModel : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private bool _inProgress;
        private bool _isDone;
        private TaskBoardDisplayModel _board;

        public int Id { get; set; }
        public int BoardId { get; set; }
        public TaskBoardDisplayModel Board 
        {
            get { return _board; }
            set
            {
                _board = value;
                INotifyPropertyChanged(nameof(Board));
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                INotifyPropertyChanged(nameof(Title));
            }
        }
        public string Description 
        {
            get { return _description; }
            set
            {
                _description = value;
                INotifyPropertyChanged(nameof(Description));
            }
        }
        public bool InProgress 
        {
            get { return _inProgress; }
            set
            {
                _inProgress = value;
                INotifyPropertyChanged(nameof(InProgress));
            }
        } 
        public bool IsDone 
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                INotifyPropertyChanged(nameof(IsDone));
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
