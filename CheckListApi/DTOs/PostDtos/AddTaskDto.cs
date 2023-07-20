using CheckListApi.Models;

namespace CheckListApi.DTOs.PostDtos
{
    public class AddTaskDto
    {
        public int BoardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.NotStarted;
    }
}
