using CheckListApi.DTOs.PostDtos;
using CheckListApi.DTOs.PutDtos;
using CheckListApi.Models;

namespace CheckListApi.Interfaces
{
    public interface ICheckListRepository
    {
        Task AddCanvasToUser(AddCanvasDto canvas);
        Task AddTaskBoardToCanvas(AddTaskBoardDto taskBoard);
        Task AddTaskToBoard(AddTaskDto task);
        Task<List<Canvas>> GetCanvasListForUser(int userId);
        Task<List<TaskBoard>> GetTaskBoardListForCanvas(int canvasId);
        Task UpdateCanvas(UpdateCanvasDto canvas);
        Task UpdateTaskBoard(UpdateTaskBoardDto taskBoard);
        Task UpdateTask(UpdateTaskDto task);
        Task DeleteCanvas(int userId, int canvasId);
        Task DeleteTaskBoard(int canvasId, int boardId);
        Task DeleteTask(int boardId, int taskId);
    }
}
