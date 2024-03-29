﻿using AutoMapper;
using CheckListApi.DTOs.PostDtos;
using CheckListApi.DTOs.PutDtos;
using CheckListApi.Interfaces;
using CheckListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckListApi.Data
{
    public class CheckListRepository : ICheckListRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CheckListRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region ----------POST Methods----------
        public async Task AddCanvasToUser(AddCanvasDto canvasDto)
        {
            var canvas = _mapper.Map<Canvas>(canvasDto);

            var user = await _context.Users
                .Include(u => u.Canvases)
                .SingleOrDefaultAsync(u => u.Id == canvas.UserId);

            user.Canvases.Add(canvas);

            _context.Canvases.Add(canvas);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddTaskBoardToCanvas(AddTaskBoardDto taskBoardDto)
        {
            var taskBoard = _mapper.Map<TaskBoard>(taskBoardDto);

            var canvas = await _context.Canvases
                .Include(c => c.TaskBoards)
                .SingleOrDefaultAsync(c => c.Id == taskBoard.CanvasId);

            canvas.TaskBoards.Add(taskBoard);

            _context.TaskBoards.Add(taskBoard);
            await _context.SaveChangesAsync();

            return taskBoard.Id;
        }

        public async Task<int> AddTaskToBoard(AddTaskDto taskDto)
        {
            var task = _mapper.Map<MyTask>(taskDto);

            var taskBoard = await _context.TaskBoards
                .Include(t => t.Tasks)
                .SingleOrDefaultAsync(t => t.Id == task.BoardId);

            taskBoard.Tasks.Add(task);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task.Id;
        }
        #endregion

        #region ----------GET Methods----------
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
        #endregion

        #region ----------PUT Methods----------
        public async Task UpdateCanvas(UpdateCanvasDto canvasDto)
        {
            var canvasToUpdate = await _context.Canvases
                .SingleOrDefaultAsync(c => c.Id == canvasDto.Id);

            _mapper.Map(canvasDto, canvasToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskBoard(UpdateTaskBoardDto taskBoardDto)
        {
            var boardToUpdate = await _context.TaskBoards
                .SingleOrDefaultAsync(t => t.Id == taskBoardDto.Id);

            _mapper.Map(taskBoardDto, boardToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateTask(UpdateTaskDto taskDto)
        {
            var taskToUpdate = await _context.Tasks
                .SingleOrDefaultAsync(u => u.Id == taskDto.Id);

            _mapper.Map(taskDto, taskToUpdate);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region ----------DELETE Methods----------
        public async Task DeleteCanvas(int userId, int canvasId)
        {
            var user = await _context.Users
                .Include(u => u.Canvases)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if(user != null)
            {
                var canvasToDelete = user.Canvases
                .SingleOrDefault(c => c.Id == canvasId);

                user.Canvases.Remove(canvasToDelete);

                _context.Canvases.Remove(canvasToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskBoard(int canvasId, int boardId)
        {
            var canvas = await _context.Canvases
                .Include(c => c.TaskBoards)
                .SingleOrDefaultAsync(c => c.Id == canvasId);

            if(canvas != null)
            {
                var boardToDelete = canvas.TaskBoards
                .SingleOrDefault(t => t.Id == boardId);

                canvas.TaskBoards.Remove(boardToDelete);

                _context.TaskBoards.Remove(boardToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTask(int boardId, int taskId)
        {
            var board = await _context.TaskBoards
                .Include(t => t.Tasks)
                .SingleOrDefaultAsync(t => t.Id == boardId);

            if(board != null)
            {
                var taskToDelete = board.Tasks
                .SingleOrDefault(t => t.Id == taskId);

                board.Tasks.Remove(taskToDelete);

                _context.Tasks.Remove(taskToDelete);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
