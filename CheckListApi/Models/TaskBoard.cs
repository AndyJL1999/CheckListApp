using System.ComponentModel.DataAnnotations;

namespace CheckListApi.Models
{
    public class TaskBoard
    {
        public int Id { get; set; }

        [Required]
        public int CanvasId { get; set; }
        public Canvas Canvas { get; set; }


        [MaxLength(25)]
        public string Title { get; set; }
        public List<MyTask> Tasks { get; set; }
    }
}
