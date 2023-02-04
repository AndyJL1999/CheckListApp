using CheckListApi.DTOs;
using CheckListApi.Models;
using CheckListWPF.Resources.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + "CheckList"))
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

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "CheckList/Canvas", content))
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

        public async Task AddTaskBoardToCanvas(string title, int canvasId)
        {
            var newCanvas = new AddTaskBoardDto
            {
                Title = title,
                CanvasId = canvasId
            };

            var content = JsonContent.Create(newCanvas);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "CheckList/TaskBoard", content))
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

        public async Task AddTaskToBoard(string title, string description, int boardId)
        {
            var newCanvas = new AddTaskDto
            {
                Title = title,
                Description = description,
                IsDone = false,
                InProgress = false,
                BoardId = boardId,

            };

            var content = JsonContent.Create(newCanvas);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "CheckList/Task", content))
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
