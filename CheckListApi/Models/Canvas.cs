using System.ComponentModel.DataAnnotations;

namespace CheckListApi.Models
{
    public class Canvas
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [MaxLength(25)]
        public string Title { get; set; }
        public List<TaskBoard> TaskBoards { get; set; }
    }
}
