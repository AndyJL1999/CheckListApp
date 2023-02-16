using AutoMapper;
using CheckListApi.DTOs.PostDtos;
using CheckListApi.DTOs.PutDtos;
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

        public CheckListController(ICheckListRepository checkListRepo)
        {
            _checkListRepo = checkListRepo;
        }

        #region ----------GET----------
        [HttpGet("GetCanvases")]
        public async Task<ActionResult<List<Canvas>>> GetCanvasList()
        {
            var userId = User.GetUserId();

            return Ok(await _checkListRepo.GetCanvasListForUser(userId));
        }

        [HttpGet("GetTaskBoards/{canvasId}")]
        public async Task<ActionResult<List<Canvas>>> GetCanvasTaskBoardList(int canvasId)
        {
            return Ok(await _checkListRepo.GetTaskBoardListForCanvas(canvasId));
        }
        #endregion

        #region ----------POST----------
        [HttpPost("AddCanvas")]
        public async Task<ActionResult> AddCanvas(AddCanvasDto canvasDto)
        {
            canvasDto.UserId = User.GetUserId();

            await _checkListRepo.AddCanvasToUser(canvasDto);

            return Ok("Canvas Added!");
        }

        [HttpPost("AddTaskBoard")]
        public async Task<ActionResult<int>> AddTaskBoard(AddTaskBoardDto taskBoardDto)
        {
            return Ok(await _checkListRepo.AddTaskBoardToCanvas(taskBoardDto));
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult<int>> AddTask(AddTaskDto taskDto)
        {
            return Ok(await _checkListRepo.AddTaskToBoard(taskDto));
        }
        #endregion

        #region ----------PUT----------
        [HttpPut("UpdateCanvas")]
        public async Task<ActionResult> UpdateCanvas(UpdateCanvasDto canvasDto)
        {
            await _checkListRepo.UpdateCanvas(canvasDto);

            return Ok("Canvas Updated!");
        }

        [HttpPut("UpdateTaskBoard")]
        public async Task<ActionResult> UpdateTaskBoard(UpdateTaskBoardDto taskBoardDto)
        {
            await _checkListRepo.UpdateTaskBoard(taskBoardDto);

            return Ok("TaskBoard Updated!");
        }

        [HttpPut("UpdateTask")]
        public async Task<ActionResult> UpdateTask(UpdateTaskDto taskDto)
        {
            await _checkListRepo.UpdateTask(taskDto);

            return Ok("Task Updated!");
        }
        #endregion

        #region ----------DELETE----------
        [HttpDelete("DeleteCanvas/{canvasId}")]
        public async Task<ActionResult> DeleteCanvas(int canvasId)
        {
            var userId = User.GetUserId();

            await _checkListRepo.DeleteCanvas(userId, canvasId);

            return Ok("Canvas Deleted!");
        }

        [HttpDelete("DeleteTaskBoard/{canvasId}/{boardId}")]
        public async Task<ActionResult> DeleteTaskBoard(int canvasId,int boardId)
        {
            await _checkListRepo.DeleteTaskBoard(canvasId, boardId);

            return Ok("TaskBoard Deleted!");
        }

        [HttpDelete("DeleteTask/{boardId}/{taskId}")]
        public async Task<ActionResult> DeleteTask(int boardId, int taskId)
        {
            await _checkListRepo.DeleteTask(boardId, taskId);

            return Ok("Task Deleted!");
        }
        #endregion
    }
}
