using System.ComponentModel.DataAnnotations;

namespace CheckListApi.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<TaskBoard> TaskBoards { get; set; }
    }
}
