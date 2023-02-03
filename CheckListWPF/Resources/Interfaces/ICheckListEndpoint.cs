using CheckListApi.Models;
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
        Task AddTaskBoardToCanvas(string title, int canvasId);
        Task<IEnumerable<TaskBoard>> GetCanvasTaskBoardList(int canvasId);
    }
}
