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
            var user = await _context.Users
                .Include(u => u.Canvases)
                .SingleOrDefaultAsync(u => u.Id == canvas.UserId);

            user.Canvases.Add(canvas);

            _context.Canvases.Add(canvas);
            await _context.SaveChangesAsync();
        }

        public async Task AddTaskBoardToCanvas(TaskBoard taskBoard)
        {
            var canvas = await _context.Canvases
                .Include(c => c.TaskBoards)
                .SingleOrDefaultAsync(c => c.Id == taskBoard.CanvasId);

            canvas.TaskBoards.Add(taskBoard);

            _context.TaskBoards.Add(taskBoard);
            await _context.SaveChangesAsync();
        }

        public async Task AddTaskToBoard(MyTask task)
        {
            var taskBoard = await _context.TaskBoards
                .Include(t => t.Tasks)
                .SingleOrDefaultAsync(t => t.Id == task.BoardId);

            taskBoard.Tasks.Add(task);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Canvas>> GetCanvasListForUser(int userId)
        {
            return await _context.Canvases
                .Include(c => c.TaskBoards)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<TaskBoard>> GetTaskBoardListForCanvas(int canvasId)
        {
            return await _context.TaskBoards
                .Include(t => t.Tasks)
                .Where(t => t.CanvasId == canvasId)
                .ToListAsync();
        }
    }
}
