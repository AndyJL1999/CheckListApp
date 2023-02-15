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
    public class CanvasDisplayModel : ObservableObject
    {
        private string _title;
        private List<TaskBoardDisplayModel> _taskBoards;

        public int Id { get; set; }

        public string Title 
        { 
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            } 
        }
        public List<TaskBoardDisplayModel> TaskBoards 
        { 
            get { return _taskBoards; }
            set
            {
                _taskBoards = value;
                OnPropertyChanged(nameof(TaskBoards));
            }
        }
    }
}
