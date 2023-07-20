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
        private StatusEnum _status;
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
        public StatusEnum Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

    }
}
