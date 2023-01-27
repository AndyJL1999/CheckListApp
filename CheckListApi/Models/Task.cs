using System.ComponentModel.DataAnnotations;

namespace CheckListApi.Models
{
    public class Task
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public bool InProgress { get; set; } = false;
        public bool IsDone { get; set; } = false;
    }
}
