using CheckListApi.Models;

namespace CheckListApi.Interfaces
{
    public interface ICheckListRepository
    {
        Task AddCanvasToUser(Canvas canvas);
        Task AddTaskBoardToCanvas(TaskBoard taskBoard);
        Task AddTaskToBoard(MyTask task);
        Task<List<Canvas>> GetCanvasListForUser(int userId);
        Task<List<TaskBoard>> GetTaskBoardListForCanvas(int canvasId);
        Task DeleteCanvas(Canvas canvas);
        Task DeleteTaskBoard(TaskBoard taskBoard);
        Task DeleteTask(MyTask task);
    }
}
