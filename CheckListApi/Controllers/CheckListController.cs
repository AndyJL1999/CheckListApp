using AutoMapper;
using CheckListApi.DTOs;
using CheckListApi.Extentions;
using CheckListApi.Interfaces;
using CheckListApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckListApi.Controllers
{
    [Authorize]
    public class CheckListController : BaseApiController
    {
        private readonly ICheckListRepository _checkListRepo;
        private readonly IMapper _mapper;


        public CheckListController(ICheckListRepository checkListRepo, IMapper mapper)
        {
            _checkListRepo = checkListRepo;
            _mapper = mapper;
        }

        [HttpGet("GetCanvases")]
        public async Task<ActionResult<List<Canvas>>> GetCanvasList()
        {
            var userId = User.GetUserId();

            return Ok(await _checkListRepo.GetCanvasListForUser(userId));
        }

        [HttpGet("GetTaskBoards/{id}")]
        public async Task<ActionResult<List<Canvas>>> GetCanvasTaskBoardList(int id)
        {
            return Ok(await _checkListRepo.GetTaskBoardListForCanvas(id));
        }

        [HttpPost("AddCanvas")]
        public async Task<ActionResult> AddCanvas(AddCanvasDto canvasDto)
        {
            var canvas = _mapper.Map<Canvas>(canvasDto);

            canvas.UserId = User.GetUserId();

            if(canvas == null)
            {
                return BadRequest();
            }

            await _checkListRepo.AddCanvasToUser(canvas);

            return Ok("Canvas Added!");
        }

        [HttpPost("AddTaskBoard")]
        public async Task<ActionResult> AddTaskBoard(AddTaskBoardDto taskboardDto)
        {
            var taskBoard = _mapper.Map<TaskBoard>(taskboardDto);

            if (taskBoard == null)
            {
                return BadRequest();
            }

            await _checkListRepo.AddTaskBoardToCanvas(taskBoard);

            return Ok("TaskBoard Added!");
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult> AddTask(AddTaskDto taskDto)
        {
            var task = _mapper.Map<MyTask>(taskDto);

            if (task == null)
            {
                return BadRequest();
            }

            await _checkListRepo.AddTaskToBoard(task);

            return Ok("Task Added!");
        }

        [HttpDelete("DeleteCanvas/{canvasId}")]
        public async Task<ActionResult> DeleteCanvas(int canvasId)
        {
            var canvas = new Canvas
            {
                Id = canvasId,
                UserId = User.GetUserId(),
            };

            await _checkListRepo.DeleteCanvas(canvas);

            return Ok("Canvas Deleted!");
        }

        [HttpDelete("DeleteTaskBoard/{canvasId}/{boardId}")]
        public async Task<ActionResult> DeleteTaskBoard(int canvasId,int boardId)
        {
            var taskBoard = new TaskBoard
            {
                Id = boardId,
                CanvasId = canvasId
            };

            await _checkListRepo.DeleteTaskBoard(taskBoard);

            return Ok("TaskBoard Deleted!");
        }

        [HttpDelete("DeleteTask/{boardId}/{taskId}")]
        public async Task<ActionResult> DeleteTask(int boardId, int taskId)
        {
            var task = new MyTask
            {
                Id = taskId,
                BoardId = boardId
            };

            await _checkListRepo.DeleteTask(task);

            return Ok("Task Deleted!");
        }
    }
}
