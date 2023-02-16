using CheckListApi.Models;
using CheckListWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.Resources.Interfaces
{
    public interface ICheckListEndpoint
    {
        Task<IEnumerable<Canvas>> GetUserCanvasList();
        Task AddCanvasToList(string title);
        Task<int> AddTaskBoardToCanvas(string title, int canvasId);
        Task<IEnumerable<TaskBoard>> GetCanvasTaskBoardList(int canvasId);
        Task<int> AddTaskToBoard(string title, string description, int boardId);
        Task UpdateCanvas(int canvasId, string newTitle);
        Task UpdateBoard(int boardId, string newTitle);
        Task UpdateTask(TaskDisplayModel task);
        Task DeleteBoardFromCanvas(int canvasId, int boardId);
        Task DeleteTaskFromBoard(int boardId, int taskId);
    }
}
