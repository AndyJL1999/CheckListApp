using CheckListApi.Models;
using CheckListWPF.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.MVVM.Model
{
    public class TaskDisplayModel : ObservableObject
    {
        private string _title;
        private string _description;
        private bool _notStarted;
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
                OnPropertyChanged(nameof(Board));
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Description 
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public bool NotStarted
        {
            get { return _notStarted; }
            set
            {
                _notStarted = value;
                OnPropertyChanged(nameof(NotStarted));
            }
        }
        public bool InProgress 
        {
            get { return _inProgress; }
            set
            {
                _inProgress = value;
                OnPropertyChanged(nameof(InProgress));
            }
        } 
        public bool IsDone 
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        } 

    }
}
