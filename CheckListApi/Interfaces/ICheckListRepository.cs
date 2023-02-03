using CheckListApi.Models;

namespace CheckListApi.Interfaces
{
    public interface ICheckListRepository
    {
        public Task AddCanvasToUser(Canvas canvas);
        public Task AddTaskBoardToCanvas(TaskBoard taskBoard);
        public Task AddTaskToBoard(MyTask task);
        public Task<List<Canvas>> GetCanvasListForUser(int id);
        Task<List<TaskBoard>> GetTaskBoardListForCanvas(int canvasId);
    }
}
