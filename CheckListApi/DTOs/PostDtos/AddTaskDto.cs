namespace CheckListApi.DTOs.PostDtos
{
    public class AddTaskDto
    {
        public int BoardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool NotStarted { get; set; } = true;
        public bool InProgress { get; set; } = false;
        public bool IsDone { get; set; } = false;
    }
}
