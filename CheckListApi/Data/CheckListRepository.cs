using CheckListApi.Interfaces;
using CheckListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckListApi.Data
{
    public class CheckListRepository : ICheckListRepository
    {
        private readonly DataContext _context;

        public CheckListRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddCanvasToUser(Canvas canvas)
        {
            _context.CanvasList.Add(canvas);
            await _context.SaveChangesAsync();
        }

        public async Task AddTaskBoardToCanvas(TaskBoard taskBoard)
        {
            _context.TaskBoards.Add(taskBoard);
            await _context.SaveChangesAsync();
        }

        public async Task AddTaskToBoard(MyTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Canvas>> GetCanvasListForUser(int id)
        {
            return await _context.CanvasList.Where(c => c.UserId == id).ToListAsync();
        }

        public async Task<List<TaskBoard>> GetTaskBoardListForCanvas(int canvasId)
        {
            return await _context.TaskBoards.Where(t => t.CanvasId == canvasId).ToListAsync();
        }
    }
}
