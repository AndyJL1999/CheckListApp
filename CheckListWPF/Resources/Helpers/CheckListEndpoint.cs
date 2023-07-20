using CheckListApi.DTOs.PostDtos;
using CheckListApi.DTOs.PutDtos;
using CheckListApi.Models;
using CheckListWPF.MVVM.Model;
using CheckListWPF.Resources.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.Resources.Helpers
{
    public class CheckListEndpoint : ICheckListEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public CheckListEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IEnumerable<Canvas>> GetUserCanvasList()
        {
            List<Canvas> output;

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + "CheckList/GetCanvases"))
            {
                if (response.IsSuccessStatusCode)
                {
                    output = await response.Content.ReadFromJsonAsync<List<Canvas>>();

                    return output;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public async Task<IEnumerable<TaskBoard>> GetCanvasTaskBoardList(int canvasId)
        {
            List<TaskBoard> output;

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + $"CheckList/GetTaskBoards/{canvasId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    output = await response.Content.ReadFromJsonAsync<List<TaskBoard>>();

                    return output;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public async Task AddCanvasToList(string title)
        {
            var newCanvas = new AddCanvasDto
            {
                Title = title
            };

            var content = JsonContent.Create(newCanvas);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "CheckList/AddCanvas", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<int> AddTaskBoardToCanvas(string title, int canvasId)
        {
            var newCanvas = new AddTaskBoardDto
            {
                Title = title,
                CanvasId = canvasId
            };

            var content = JsonContent.Create(newCanvas);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "CheckList/AddTaskBoard", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<int>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<int> AddTaskToBoard(string title, string description, int boardId)
        {
            var newCanvas = new AddTaskDto
            {
                Title = title,
                Description = description,
                Status = StatusEnum.NotStarted,
                BoardId = boardId,
            };

            var content = JsonContent.Create(newCanvas);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "CheckList/AddTask", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<int>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateCanvas(int canvasId, string newTitle)
        {
            var newBoard = new UpdateCanvasDto
            {
                Id = canvasId,
                Title = newTitle
            };

            var content = JsonContent.Create(newBoard);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync(_apiHelper.ApiClient.BaseAddress + $"CheckList/UpdateCanvas", content))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateBoard(int boardId, string newTitle)
        {
            var newBoard = new UpdateTaskBoardDto
            {
                Id = boardId,
                Title = newTitle
            };

            var content = JsonContent.Create(newBoard);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync(_apiHelper.ApiClient.BaseAddress + $"CheckList/UpdateTaskBoard", content))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateTask(TaskDisplayModel task)
        {
            var newTask = new UpdateTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status
            };

            var content = JsonContent.Create(newTask);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync(_apiHelper.ApiClient.BaseAddress + $"CheckList/UpdateTask", content))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task DeleteCanvas(int canvasId)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync(_apiHelper.ApiClient.BaseAddress + $"CheckList/DeleteCanvas/{canvasId}"))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task DeleteBoardFromCanvas(int canvasId, int boardId)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync(_apiHelper.ApiClient.BaseAddress + $"CheckList/DeleteTaskBoard/{canvasId}/{boardId}"))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task DeleteTaskFromBoard(int boardId, int taskId)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync(_apiHelper.ApiClient.BaseAddress + $"CheckList/DeleteTask/{boardId}/{taskId}"))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
