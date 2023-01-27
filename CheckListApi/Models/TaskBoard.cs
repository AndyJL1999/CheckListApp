using System.ComponentModel.DataAnnotations;

namespace CheckListApi.Models
{
    public class TaskBoard
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Title { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
