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
                .Where(c => c.UserId == userId)
                .Select(c => new Canvas 
                { 
                    Id = c.Id, 
                    TaskBoards = c.TaskBoards, 
                    Title = c.Title, 
                    UserId = c.UserId 
                })
                .ToListAsync();
        }

        public async Task<List<TaskBoard>> GetTaskBoardListForCanvas(int canvasId)
        {
            return await _context.TaskBoards
                .Where(t => t.CanvasId == canvasId)
                .Select(t => new TaskBoard
                {
                    Id = t.Id,
                    Tasks = t.Tasks,
                    Title = t.Title,
                    CanvasId = t.CanvasId
                })
                .ToListAsync();
        }

        public async Task DeleteCanvas(Canvas canvas)
        {
            var user = await _context.Users
                .Include(u => u.Canvases)
                .SingleOrDefaultAsync(u => u.Id == canvas.UserId);

            user.Canvases.Remove(canvas);

            _context.Canvases.Remove(canvas);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskBoard(TaskBoard taskBoard)
        {
            var canvas = await _context.Canvases
                .Include(u => u.TaskBoards)
                .SingleOrDefaultAsync(u => u.Id == taskBoard.CanvasId);

            var boardToDelete = canvas.TaskBoards
                .SingleOrDefault(t => t.Id == taskBoard.Id);

            canvas.TaskBoards.Remove(boardToDelete);

            _context.TaskBoards.Remove(boardToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(MyTask task)
        {
            var board = await _context.TaskBoards
                .Include(u => u.Tasks)
                .SingleOrDefaultAsync(u => u.Id == task.BoardId);

            var taskToDelete = board.Tasks
                .SingleOrDefault(t => t.Id == task.Id);

            board.Tasks.Remove(taskToDelete);

            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
