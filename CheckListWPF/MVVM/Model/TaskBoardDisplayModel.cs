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
    public class TaskBoardDisplayModel : ObservableObject
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
                OnPropertyChanged(nameof(Title));
            }
        }
        public List<TaskDisplayModel> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
    }
}
