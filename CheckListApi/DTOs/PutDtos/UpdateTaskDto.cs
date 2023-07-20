using CheckListApi.Models;

namespace CheckListApi.DTOs.PutDtos
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
    }
}
