using System.ComponentModel.DataAnnotations;

namespace CheckListApi.Models
{
    public class TaskBoard
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }


        [MaxLength(25)]
        public string Title { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
