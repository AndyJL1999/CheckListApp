using System.ComponentModel.DataAnnotations;

namespace CheckListApi.DTOs.PostDtos
{
    public class AddCanvasDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}
