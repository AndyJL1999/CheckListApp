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

        [HttpGet]
        public async Task<ActionResult<List<Canvas>>> GetCanvasList()
        {
            var id = User.GetUserId();

            return Ok(await _checkListRepo.GetCanvasListForUser(id));
        }

        [HttpGet("GetTaskBoards/{id}")]
        public async Task<ActionResult<List<Canvas>>> GetCanvasTaskBoardList(int id)
        {
            return Ok(await _checkListRepo.GetTaskBoardListForCanvas(id));
        }

        [HttpPost("Canvas")]
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

        [HttpPost("TaskBoard")]
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

        [HttpPost("Task")]
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
    }
}
